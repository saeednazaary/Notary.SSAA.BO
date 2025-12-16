using MediatR;
using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.SignRequest;
using Notary.SSAA.BO.DataTransferObject.Mappers.SignRequest;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.BaseInfo;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.Media;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.Person;
using Notary.SSAA.BO.DataTransferObject.Validators.SignRequest;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.BusinessRules.SignRequestRules;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.SsrConfig;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using Serilog;


namespace Notary.SSAA.BO.CommandHandler.SignRequest.Handlers
{
    public class CreateSignRequestCommandHandler : BaseCommandHandler<CreateSignRequestCommand, ApiResult>
    {
        private readonly ISignRequestRepository _signRequestRepository;
        private readonly ISsrConfigRepository _ssrConfigRepository;
        private readonly IDateTimeService _dateTimeService;
        private readonly IApplicationIdGeneratorService _applicationIdGeneratorService;
        private Domain.Entities.SignRequest masterEntity;
        private ApiResult<SignRequestViewModel> apiResult;

        public CreateSignRequestCommandHandler(IMediator mediator, IDateTimeService dateTimeService, IUserService userService, ISignRequestRepository signRequestRepository,
             ILogger logger, IApplicationIdGeneratorService applicationIdGeneratorService, ISsrConfigRepository ssrConfigRepository)
            : base(mediator, userService, logger)
        {
            masterEntity = new();
            apiResult = new();
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
            _signRequestRepository = signRequestRepository ?? throw new ArgumentNullException(nameof(signRequestRepository));
            _applicationIdGeneratorService = applicationIdGeneratorService ?? throw new ArgumentNullException(nameof(applicationIdGeneratorService));
            _ssrConfigRepository = ssrConfigRepository ?? throw new ArgumentNullException(nameof(ssrConfigRepository));
        }

        protected override bool HasAccess(CreateSignRequestCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
        protected override async Task<ApiResult> ExecuteAsync(CreateSignRequestCommand request, CancellationToken cancellationToken)
        {
            Guid signRequestId = _applicationIdGeneratorService.DecryptGuid(request.SignRequestId);
            if (signRequestId == Guid.Empty)
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("شناسه وارد شده معتبر نیست.");
                return apiResult;
            }
            await BusinessValidation(request, cancellationToken);
            if (apiResult.IsSuccess)
            {
                await UpdateDatabase(request, cancellationToken);
                masterEntity.Id = signRequestId;
                if (await _signRequestRepository.TableNoTracking.CountAsync(x => x.Id == signRequestId && x.ScriptoriumId== masterEntity.ScriptoriumId, cancellationToken)>0)
                {
                    apiResult.HasAllarmMessage= true;
                    apiResult.IsSuccess= false;
                    apiResult.message.Add("این گواهی امضا قبلا ثبت شده است.");
                    return apiResult;
                }

                    await _signRequestRepository.AddAsync(masterEntity, cancellationToken);

                CreateMediaServiceInput generateScanFile = new()
                {
                    DocumentTitle = " تصوبر برگه پرشده و امضا شده گواهی امضا",
                    DocumentTypeId = SignRequestConstants.ScanFileDocumentType,
                    ClientId = SignRequestConstants.AttachmentClientId,
                    CreateDateTime = _dateTimeService.CurrentPersianDateTime,
                    RelatedRecordId = masterEntity.Id.ToString()
                };
                _ = await _mediator.Send(generateScanFile, cancellationToken);
            }
            return apiResult;
        }
        private async Task<string> CreateSignRequestReqNo(string beginNationalNo, CancellationToken cancellationToken)
        {
            string beginReqNo = _dateTimeService.CurrentPersianDate[..4];
            beginReqNo += "444";
            beginReqNo += beginNationalNo;
            string reqNo = await _signRequestRepository.GetMaxReqNo(beginReqNo, cancellationToken);
            if (string.IsNullOrWhiteSpace(reqNo))
            {
                reqNo = _dateTimeService.CurrentPersianDate[..4];
                reqNo += "444";
                reqNo += beginNationalNo;
                reqNo += "000001";
            }
            else
            {
                decimal numberReqNo = decimal.Parse(reqNo);
                numberReqNo++;
                reqNo = numberReqNo.ToString();
            }
            return reqNo;
        }
        private async Task UpdateDatabase(CreateSignRequestCommand request, CancellationToken cancellationToken)
        {
            masterEntity = new()
            {
                ReqDate = _dateTimeService.CurrentPersianDate,
                ReqTime = _dateTimeService.CurrentTime,
                RecordDate = _dateTimeService.CurrentDateTime,
                ScriptoriumId = _userService.UserApplicationContext.BranchAccess.BranchCode,
                IsCostPaid = SignRequestConstants.IsCostPaid,
                Modifier = _userService.UserApplicationContext.User.Name + " " + _userService.UserApplicationContext.User.Family,
                ModifyDate = _dateTimeService.CurrentPersianDate,
                ModifyTime = _dateTimeService.CurrentTime,
                Ilm = SignRequestConstants.CreateIlm,
                ReqNo = await CreateSignRequestReqNo(_userService.UserApplicationContext.BranchAccess.BranchCode, cancellationToken),
                State = "1",
            };

            var nationalNosForCheck = request.SignRequestPersons.Where(x => x.IsPersonSanaChecked == true || x.IsPersonSabteAhvalChecked == true || x.PersonMobileNoState == true).Select(x => x.PersonNationalNo);
            var serviceCheck = new GetPersonServiceListServiceInput();
            serviceCheck.NationalNos = nationalNosForCheck.ToList();
            serviceCheck.MainObjectId = request.SignRequestId;
            var serviceCheckRes = await _mediator.Send(serviceCheck, cancellationToken);
            SignRequestMapper.ToEntity(ref masterEntity, request);
            for (int i = 0; i < request.SignRequestPersons.Count; i++)
            {
                if (request.SignRequestPersons[i].IsNew)
                {
                    SignRequestPerson newSignRequestPerson = new();
                    SignRequestMapper.ToEntity(ref newSignRequestPerson, request.SignRequestPersons[i]);
                    newSignRequestPerson.SignRequestId = masterEntity.Id;
                    newSignRequestPerson.RecordDate = _dateTimeService.CurrentDateTime;
                    newSignRequestPerson.RowNo = (short)(request.SignRequestPersons.Count - i);
                    newSignRequestPerson.ScriptoriumId = masterEntity.ScriptoriumId;
                    newSignRequestPerson.Ilm = SignRequestConstants.CreateIlm;
                    newSignRequestPerson.IsFingerprintGotten = SignRequestConstants.IsFingerprintGotten;
                    newSignRequestPerson.PersonType = SignRequestConstants.PersonType;


                    if (newSignRequestPerson.IsIranian == "2")
                    {
                        newSignRequestPerson.IsAlive = "1";
                    }
                    if (serviceCheckRes?.Data != null && serviceCheckRes.IsSuccess)
                    {
                        var personServiceRes = serviceCheckRes.Data.PersonsData.FirstOrDefault(x => x.NationalNo == request.SignRequestPersons[i].PersonNationalNo);
                        if (personServiceRes != null)
                        {
                            if (personServiceRes.HasSabteAhval == "1")
                            {
                                newSignRequestPerson.IsSabtahvalChecked = SignRequestConstants.SabtahvalChecked;
                                newSignRequestPerson.IsSabtahvalCorrect = SignRequestConstants.SabtahvalChecked;
                                newSignRequestPerson.Name = personServiceRes.Name;
                                newSignRequestPerson.Family = personServiceRes.Family;
                                newSignRequestPerson.IdentityNo = personServiceRes.ShenasnameNo;
                                newSignRequestPerson.Seri = personServiceRes.ShenasnameSeri;
                                newSignRequestPerson.SeriAlpha = personServiceRes.AlphabetSeri;
                                newSignRequestPerson.SexType = personServiceRes.SexType;
                                newSignRequestPerson.IdentityIssueLocation = personServiceRes.IdentityIssueLocation;
                                newSignRequestPerson.Serial = personServiceRes.ShenasnameSerial;
                                newSignRequestPerson.BirthDate = personServiceRes.BirthDate;
                                newSignRequestPerson.FatherName = personServiceRes.FatherName;
                                newSignRequestPerson.IsAlive = personServiceRes.IsDead ? "2" : "1";

                            }
                            else if (personServiceRes.HasSabteAhval == "2")
                            {
                                newSignRequestPerson.IsAlive = null;
                                newSignRequestPerson.IsSabtahvalChecked = "2";
                                newSignRequestPerson.IsSabtahvalCorrect = "2";
                            }
                            else
                            {
                                newSignRequestPerson.IsAlive = null;

                                newSignRequestPerson.IsSabtahvalChecked = null;
                                newSignRequestPerson.IsSabtahvalCorrect = null;
                            }
                            if (personServiceRes.HasShahkar == "1")
                            {
                                newSignRequestPerson.MobileNoState = "1";
                            }
                            else if (personServiceRes.HasShahkar == "2")
                            {
                                newSignRequestPerson.MobileNoState = "2";
                            }
                            else
                            {
                                newSignRequestPerson.MobileNoState = null;
                            }
                            if (personServiceRes.HasSana == "1")
                            {
                                newSignRequestPerson.SanaState = "1";
                            }
                            else if (personServiceRes.HasSana == "2")
                            {
                                newSignRequestPerson.SanaState = "2";
                            }
                            else
                            {
                                newSignRequestPerson.SanaState = null;
                            }
                            if (personServiceRes.HasAmlakEskan == "1")
                            {
                                newSignRequestPerson.AmlakEskanState = "1";
                            }
                            else if (personServiceRes.HasAmlakEskan == "2")
                            {
                                newSignRequestPerson.AmlakEskanState = "2";
                            }
                            else
                            {
                                newSignRequestPerson.AmlakEskanState = request.SignRequestPersons[i].AmlakEskanState.ToYesNo();
                            }
                            if (personServiceRes.HasSana == "1" && personServiceRes.HasShahkar == "1")
                            {
                                newSignRequestPerson.MobileNo = personServiceRes.MobileNo;
                            }
                        }
                        else
                        {
                            newSignRequestPerson.IsAlive = null;
                            newSignRequestPerson.IsSabtahvalChecked = null;
                            newSignRequestPerson.IsSabtahvalCorrect = null;
                            newSignRequestPerson.MobileNoState = null;
                            newSignRequestPerson.SanaState = null;
                            newSignRequestPerson.AmlakEskanState = null;

                        }

                    }
                    else
                    {
                        newSignRequestPerson.IsAlive = null;
                        newSignRequestPerson.IsSabtahvalChecked = null;
                        newSignRequestPerson.IsSabtahvalCorrect = null;
                        newSignRequestPerson.MobileNoState = null;
                        newSignRequestPerson.SanaState = null;
                        newSignRequestPerson.AmlakEskanState = null;

                    }
                    masterEntity.SignRequestPeople.Add(newSignRequestPerson);
                }

            }


            foreach (ToRelatedPersonViewModel signRequestRelatedPersonViewModel in request.SignRequestRelatedPersons)
            {
                if (signRequestRelatedPersonViewModel.IsNew)
                {
                    SignRequestPersonRelated newRelatedPerson = new();
                    SignRequestMapper.ToEntity(ref newRelatedPerson, signRequestRelatedPersonViewModel);
                    newRelatedPerson.SignRequestId = masterEntity.Id;
                    newRelatedPerson.RecordDate = _dateTimeService.CurrentDateTime;

                    newRelatedPerson.Ilm = SignRequestConstants.CreateIlm;
                    masterEntity.SignRequestPersonRelateds.Add(newRelatedPerson);
                }
            }
        }
        private async Task BusinessValidation(CreateSignRequestCommand request, CancellationToken cancellationToken)
        {
            if (masterEntity != null)
            {
                var scriptoriumInfo = new SignRequestBasicInfoServiceInput();
                scriptoriumInfo.ScriptoriumId = _userService.UserApplicationContext.BranchAccess.BranchCode;
                scriptoriumInfo.CurrentDateTime = _dateTimeService.CurrentPersianDateTime;
                var baseInfoApiResult = await _mediator.Send(scriptoriumInfo, cancellationToken);
                if (!baseInfoApiResult.IsSuccess || baseInfoApiResult.Data is null)
                {
                    apiResult.IsSuccess = false;
                    apiResult.message.Add("ارتباط با اطلاعات پایه برقرار نشد.");
                    return;
                }
                SsrConfigRepositoryInput configRepositoryInput = new SsrConfigRepositoryInput();
                configRepositoryInput.CurrentScriptoriumId = baseInfoApiResult.Data.ScriptoriumId;
                configRepositoryInput.CurrentGeoLocationId = baseInfoApiResult.Data.GeoLocationId;
                configRepositoryInput.UnitLevelCode = baseInfoApiResult.Data.UnitLevelCode;
                configRepositoryInput.CurrentDayOfWeek = baseInfoApiResult.Data.DayOfWeek;
                configRepositoryInput.CurrentDateTime = _dateTimeService.CurrentPersianDateTime;

                var configs = await _ssrConfigRepository.GetBusinessConfig(configRepositoryInput, cancellationToken);
                if (!SignRequestBusinessRule.CheckSignRequestConfig(configs))
                {
                    apiResult.IsSuccess = false;

                    apiResult.message.Add("خطا در پیکربندی سرویس های گواهی امضا. لفطا با راهبر سیستم تماس بگیرید");
                    return;
                }
                if (!SignRequestBusinessRule.CheckWorkPermit(configs, baseInfoApiResult.Data.ScriptoriumId))
                {
                    apiResult.IsSuccess = false;

                    apiResult.message.Add("دفترخانه مجوز ثبت گواهی امضا ندارد. لفطا با راهبر سیستم تماس بگیرید");
                    return;
                }

                foreach (ToRelatedPersonViewModel item in request.SignRequestRelatedPersons)
                {
                    if (item.IsNew)
                    {
                        if (item.RelatedAgentTypeId.First() != "10")
                        {
                            if (item.RelatedReliablePersonReasonId.Count < 1)
                            {
                                apiResult.message.Add("فیلد دلیل نیاز به معتمد اجباری است .");
                            }
                        }

                        if (item.RelatedAgentTypeId.First() == "1")
                        {
                            apiResult.message.Add("در گواهی امضا امکان ایجاد وابستگی وکیل وجود ندارد.");
                        }

                        if (item.RelatedAgentTypeId.First() != "3" && item.RelatedAgentTypeId.First() != "1")
                        {
                            if (!ValidatorHelper.BeValidPersianDate(item.RelatedAgentDocumentDate) ||
                                item.RelatedAgentDocumentDate?.GetDateTimeDistance(_dateTimeService.CurrentPersianDateTime) > TimeSpan.FromDays(1))
                            {
                                apiResult.message.Add("مقدار تاریخ وکالتنامه غیر مجاز است ");
                            }
                            if (string.IsNullOrWhiteSpace(item.RelatedAgentDocumentNo) || item.RelatedAgentDocumentNo.Length > 50)
                            {
                                apiResult.message.Add("فیلد شماره وکالتنامه اجباری است .");
                            }

                            if (string.IsNullOrWhiteSpace(item.RelatedAgentDocumentIssuer))
                            {
                                apiResult.message.Add("فیلد مرجع صدور اجباری میباشد . ");
                            }
                        }

                        if (!masterEntity.SignRequestPeople.Any(x => x.Id == item.MainPersonId.First().ToGuid()))
                        {

                            apiResult.message.Add("شخص اصیل شخص وابسته حذف شده است");
                        }

                        if (!masterEntity.SignRequestPeople.Any(x => x.Id == item.RelatedAgentPersonId.First().ToGuid()))
                        {

                            apiResult.message.Add("شخص نماینده شخص وابسته حذف شده است ");
                        }

                        if (masterEntity.SignRequestPersonRelateds.Any(x => x.MainPersonId == item.MainPersonId.First().ToGuid()))
                        {

                            apiResult.message.Add("برای این شخص اصیل ، شخص وابسته وجود دارد");
                        }

                        if (!masterEntity.SignRequestPeople.Any(x => x.Id == item.RelatedAgentPersonId.First().ToGuid()))
                        {

                            apiResult.message.Add("برای این شخص ،نماینده ، شخص وابسته وجود دارد ");
                        }
                    }

                }

                string message = string.Empty;
                message = UpdateSignRequestConfigValidator.CheckPersonAlive(request.SignRequestPersons);
                if (!string.IsNullOrWhiteSpace(message))
                {
                    apiResult.message.Add(message);
                }
                message = UpdateSignRequestConfigValidator.CheckSabteAhval(request.SignRequestPersons, configs);
                if (!string.IsNullOrWhiteSpace(message))
                {
                    apiResult.message.Add(message);
                }
            }
            else
            {
                apiResult.message.Add("گواهی امضا مربوطه یافت نشد ");
            }

            if (apiResult.message.Count > 0)
            {
                apiResult.statusCode = ApiResultStatusCode.Success;
                apiResult.IsSuccess = false;
            }
        }
    }
}