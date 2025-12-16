using Mapster;
using MediatR;
using Newtonsoft.Json;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.SignRequest;
using Notary.SSAA.BO.DataTransferObject.Mappers.SignRequest;
using Notary.SSAA.BO.DataTransferObject.Queries.SignRequest;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.BaseInfo;
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
using System.Threading;


namespace Notary.SSAA.BO.CommandHandler.SignRequest.Handlers
{
    public class UpdateSignRequestCommandHandler : BaseCommandHandler<UpdateSignRequestCommand, ApiResult>
    {
        private readonly ISignRequestRepository _signRequestRepository;
        private readonly IDateTimeService _dateTimeService;
        private readonly ISsrConfigRepository _ssrConfigRepository;
        private readonly IApplicationIdGeneratorService _applicationIdGeneratorService;
        private Domain.Entities.SignRequest masterEntity;
        private readonly ApiResult<SignRequestViewModel> apiResult;


        public UpdateSignRequestCommandHandler(IMediator mediator, IDateTimeService dateTimeService, IUserService userService, ISignRequestRepository signRequestRepository,
            ILogger logger, IApplicationIdGeneratorService applicationIdGeneratorService, ISsrConfigRepository ssrConfigRepository)
            : base(mediator, userService, logger)
        {
            apiResult = new();
            masterEntity = new();
            _signRequestRepository = signRequestRepository ?? throw new ArgumentNullException(nameof(signRequestRepository));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
            _applicationIdGeneratorService = applicationIdGeneratorService ?? throw new ArgumentNullException(nameof(applicationIdGeneratorService));
            _ssrConfigRepository = ssrConfigRepository ?? throw new ArgumentNullException(nameof(ssrConfigRepository));
        }
        protected override bool HasAccess(UpdateSignRequestCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
        protected override async Task<ApiResult> ExecuteAsync(UpdateSignRequestCommand request, CancellationToken cancellationToken)
        {
            Guid signRequestId = _applicationIdGeneratorService.DecryptGuid(request.SignRequestId);
            if (signRequestId == Guid.Empty)
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("شناسه وارد شده معتبر نیست.");
                return apiResult;
            }
            masterEntity = await _signRequestRepository.GetByIdAsync(cancellationToken, signRequestId);
            await BusinessValidation(request, cancellationToken);

            if (apiResult.IsSuccess)
            {
                await UpdateDatabase(request, cancellationToken);

                await _signRequestRepository.UpdateAsync(masterEntity, cancellationToken);
            }

            return apiResult;

        }
        private async Task UpdateDatabase(UpdateSignRequestCommand request, CancellationToken cancellationToken)
        {
            if (request.IsDirty)
            {
                masterEntity.Modifier = _userService.UserApplicationContext.User.Name + " " + _userService.UserApplicationContext.User.Family;
                masterEntity.ModifyDate = _dateTimeService.CurrentPersianDate;
                masterEntity.ModifyTime = _dateTimeService.CurrentTime;
            }

            SignRequestMapper.ToEntity(ref masterEntity, request);


            short maxRowNo = (short)masterEntity.SignRequestPeople.Count > 0 ? masterEntity.SignRequestPeople.Max(x => x.RowNo) : (short)0;
            var nationalNosForCheck = request.SignRequestPersons.Where(x => (x.IsPersonSanaChecked == true || x.IsPersonSabteAhvalChecked == true || x.PersonMobileNoState == true) &&
            (x.IsDirty || x.IsNew)).Select(x => x.PersonNationalNo).ToList();
            var serviceCheck = new GetPersonServiceListServiceInput();
            serviceCheck.NationalNos = nationalNosForCheck.ToList();
            serviceCheck.MainObjectId = request.SignRequestId;
            var serviceCheckRes = await _mediator.Send(serviceCheck, cancellationToken);

            foreach (SignRequestPersonViewModel signRequestPersonViewModel in request.SignRequestPersons)
            {
                if (signRequestPersonViewModel.IsNew)
                {
                    maxRowNo++;
                    SignRequestPerson newSignRequestPerson = new();
                    SignRequestMapper.ToEntity(ref newSignRequestPerson, signRequestPersonViewModel);
                    newSignRequestPerson.ScriptoriumId = masterEntity.ScriptoriumId;
                    newSignRequestPerson.RowNo = maxRowNo;
                    newSignRequestPerson.RecordDate = _dateTimeService.CurrentDateTime;
                    newSignRequestPerson.Ilm = SignRequestConstants.UpdateIlm;
                    newSignRequestPerson.IsFingerprintGotten = SignRequestConstants.IsFingerprintGotten;
                    newSignRequestPerson.PersonType = SignRequestConstants.PersonType;

                    if (newSignRequestPerson.IsIranian == "2")
                    {
                        newSignRequestPerson.IsAlive = "1";
                    }
                    if (serviceCheckRes?.Data != null && serviceCheckRes.IsSuccess)
                    {
                        var personServiceRes = serviceCheckRes.Data.PersonsData.FirstOrDefault(x => x.NationalNo == signRequestPersonViewModel.PersonNationalNo);
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
                                newSignRequestPerson.AmlakEskanState = null;
                            }
                            newSignRequestPerson.AmlakEskanState = signRequestPersonViewModel.AmlakEskanState.ToYesNo();

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
                        }

                    }
                    else
                    {
                        newSignRequestPerson.IsAlive = null;
                        newSignRequestPerson.IsSabtahvalChecked = null;
                        newSignRequestPerson.IsSabtahvalCorrect = null;
                        newSignRequestPerson.MobileNoState = null;
                        newSignRequestPerson.SanaState = null;
                    }

                    masterEntity.SignRequestPeople.Add(newSignRequestPerson);

                }
                else if (signRequestPersonViewModel.IsDelete)
                {
                    _ = masterEntity.SignRequestPeople.Remove(masterEntity.SignRequestPeople.First(x => x.Id == signRequestPersonViewModel.PersonId.ToGuid()));


                }
                else if (signRequestPersonViewModel.IsDirty)
                {
                    SignRequestPerson updatingSignRequestPerson = masterEntity.SignRequestPeople.First(x => x.Id == signRequestPersonViewModel.PersonId.ToGuid());
                    SignRequestMapper.ToEntity(ref updatingSignRequestPerson, signRequestPersonViewModel);
                    if (serviceCheckRes?.Data != null && serviceCheckRes.IsSuccess)
                    {
                        var personServiceRes = serviceCheckRes.Data.PersonsData.FirstOrDefault(x => x.NationalNo == signRequestPersonViewModel.PersonNationalNo);
                        if (personServiceRes != null)
                        {
                            if ((updatingSignRequestPerson.IsSabtahvalChecked != "1" || updatingSignRequestPerson?.IsSabtahvalCorrect!="1") 
                                && signRequestPersonViewModel.IsPersonSabteAhvalChecked == true)
                            {
                                if (personServiceRes.HasSabteAhval == "1")
                                {
                                    updatingSignRequestPerson.IsSabtahvalChecked = SignRequestConstants.SabtahvalChecked;
                                    updatingSignRequestPerson.IsSabtahvalCorrect = SignRequestConstants.SabtahvalChecked;
                                    updatingSignRequestPerson.Name = personServiceRes.Name;
                                    updatingSignRequestPerson.Family = personServiceRes.Family;
                                    updatingSignRequestPerson.IdentityNo = personServiceRes.ShenasnameNo;
                                    updatingSignRequestPerson.Seri = personServiceRes.ShenasnameSeri;
                                    updatingSignRequestPerson.SeriAlpha = personServiceRes.AlphabetSeri;
                                    updatingSignRequestPerson.SexType = personServiceRes.SexType;
                                    updatingSignRequestPerson.IdentityIssueLocation = personServiceRes.IdentityIssueLocation;
                                    updatingSignRequestPerson.Serial = personServiceRes.ShenasnameSerial;
                                    updatingSignRequestPerson.BirthDate = personServiceRes.BirthDate;
                                    updatingSignRequestPerson.FatherName = personServiceRes.FatherName;
                                    updatingSignRequestPerson.IsAlive = personServiceRes.IsDead ? "2" : "1";

                                }
                                else if (personServiceRes.HasSabteAhval == "2")
                                {
                                    updatingSignRequestPerson.IsAlive = null;
                                    updatingSignRequestPerson.IsSabtahvalChecked = "2";
                                    updatingSignRequestPerson.IsSabtahvalCorrect = "2";
                                }
                                else
                                {
                                    updatingSignRequestPerson.IsAlive = null;
                                    updatingSignRequestPerson.IsSabtahvalChecked = null;
                                    updatingSignRequestPerson.IsSabtahvalCorrect = null;
                                }
                            }
                            if (updatingSignRequestPerson.MobileNoState != "1" && signRequestPersonViewModel.PersonMobileNoState == true)
                            {

                                if (personServiceRes.HasShahkar == "1")
                                {
                                    updatingSignRequestPerson.MobileNoState = "1";
                                }
                                else if (personServiceRes.HasShahkar == "2")
                                {
                                    updatingSignRequestPerson.MobileNoState = "2";
                                }
                                else
                                {
                                    updatingSignRequestPerson.MobileNoState = null;
                                }
                            }
                            if (updatingSignRequestPerson.SanaState != "1" && signRequestPersonViewModel.IsPersonSanaChecked == true)
                            {

                                if (personServiceRes.HasSana == "1")
                                {
                                    updatingSignRequestPerson.SanaState = "1";
                                }
                                else if (personServiceRes.HasSana == "2")
                                {
                                    updatingSignRequestPerson.SanaState = "2";
                                }
                                else
                                {
                                    updatingSignRequestPerson.SanaState = null;
                                }
                                if (personServiceRes.HasSana == "1" && personServiceRes.HasShahkar == "1")
                                {
                                    updatingSignRequestPerson.MobileNo = personServiceRes.MobileNo;
                                }
                            }
                            if (updatingSignRequestPerson.AmlakEskanState != "1" && signRequestPersonViewModel.AmlakEskanState == true)
                            {

                                if (personServiceRes.HasAmlakEskan == "1")
                                {
                                    updatingSignRequestPerson.AmlakEskanState = "1";
                                }
                                else if (personServiceRes.HasAmlakEskan == "2")
                                {
                                    updatingSignRequestPerson.AmlakEskanState = "2";
                                }
                                else
                                {
                                    updatingSignRequestPerson.AmlakEskanState = null;
                                }
                                updatingSignRequestPerson.AmlakEskanState = signRequestPersonViewModel.AmlakEskanState.ToYesNo();

                            }
                        }
                        else
                        {
                            updatingSignRequestPerson.IsAlive = null;
                            updatingSignRequestPerson.IsSabtahvalChecked = null;
                            updatingSignRequestPerson.IsSabtahvalCorrect = null;
                            updatingSignRequestPerson.MobileNoState = null;
                            updatingSignRequestPerson.SanaState = null;
                            updatingSignRequestPerson.AmlakEskanState = null;
                        }

                    }
                    else
                    {
                        updatingSignRequestPerson.IsAlive = null;
                        updatingSignRequestPerson.IsSabtahvalChecked = null;
                        updatingSignRequestPerson.IsSabtahvalCorrect = null;
                        updatingSignRequestPerson.MobileNoState = null;
                        updatingSignRequestPerson.SanaState = null;
                        updatingSignRequestPerson.AmlakEskanState = null;
                    }


                    if (updatingSignRequestPerson.IsIranian == "2")
                    {
                        updatingSignRequestPerson.IsAlive = "1";
                    }
                }
                else
                {

                }

            }
            maxRowNo = (short)masterEntity.SignRequestPersonRelateds.Count > 0 ? masterEntity.SignRequestPersonRelateds.Max(x => x.RowNo) : (short)0;

            foreach (ToRelatedPersonViewModel signRequestRelatedPersonViewModel in request.SignRequestRelatedPersons)
            {
                if (signRequestRelatedPersonViewModel.IsNew)
                {
                    maxRowNo++;
                    SignRequestPersonRelated newRelatedPerson = new();
                    SignRequestMapper.ToEntity(ref newRelatedPerson, signRequestRelatedPersonViewModel);
                    newRelatedPerson.SignRequestScriptoriumId = masterEntity.ScriptoriumId;
                    newRelatedPerson.Ilm = "1";
                    newRelatedPerson.RowNo = maxRowNo;
                    newRelatedPerson.RecordDate = _dateTimeService.CurrentDateTime;

                    masterEntity.SignRequestPersonRelateds.Add(newRelatedPerson);

                }
                else if (signRequestRelatedPersonViewModel.IsDelete)
                {
                    _ = masterEntity.SignRequestPersonRelateds.Remove(masterEntity.SignRequestPersonRelateds.First(x => x.Id == signRequestRelatedPersonViewModel.RelatedPersonId.ToGuid()));
                }
                else if (signRequestRelatedPersonViewModel.IsDirty)
                {

                    SignRequestPersonRelated updatingSignRequestRelatedPerson = masterEntity.SignRequestPersonRelateds.First(x => x.Id == signRequestRelatedPersonViewModel.RelatedPersonId.ToGuid());
                    SignRequestMapper.ToEntity(ref updatingSignRequestRelatedPerson, signRequestRelatedPersonViewModel);

                }
                else
                {

                }
            }

        }
        private async Task BusinessValidation(UpdateSignRequestCommand request, CancellationToken cancellationToken)
        {
            if (masterEntity != null)
            {
                await _signRequestRepository.LoadCollectionAsync(masterEntity, q => q.SignRequestPeople, cancellationToken);
                await _signRequestRepository.LoadCollectionAsync(masterEntity, q => q.SignRequestPersonRelateds, cancellationToken);
                SsrConfigRepositoryInput configRepositoryInput = new SsrConfigRepositoryInput();

                var scriptoriumInfo = new SignRequestBasicInfoServiceInput();
                scriptoriumInfo.ScriptoriumId = masterEntity.ScriptoriumId;
                scriptoriumInfo.CurrentDateTime = _dateTimeService.CurrentPersianDateTime;
                var baseInfoApiResult = await _mediator.Send(scriptoriumInfo, cancellationToken);

                if (!baseInfoApiResult.IsSuccess || baseInfoApiResult.Data is null)
                {
                    apiResult.IsSuccess = false;
                    apiResult.message.Add("ارتباط با اطلاعات پایه برقرار نشد.");
                    return;
                }
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
                        if (masterEntity.SignRequestPersonRelateds.Any(x => x.MainPersonId == item.MainPersonId.First().ToGuid()
                        && x.AgentPersonId == item.RelatedAgentPersonId.First().ToGuid() && x.AgentTypeId == item.RelatedAgentTypeId.First()))
                        {
                            apiResult.message.Add("شخص وابسته تکراری است .");
                        }
                    }

                    if (!item.IsDelete && item.IsDirty)
                    {
                        if (!masterEntity.SignRequestPeople.Any(x => x.Id == item.MainPersonId.First().ToGuid()))
                        {

                            apiResult.message.Add("شخص اصیل شخص وابسته حذف شده است");
                        }
                        if (!masterEntity.SignRequestPeople.Any(x => x.Id == item.RelatedAgentPersonId.First().ToGuid()))
                        {

                            apiResult.message.Add("شخص نماینده شخص وابسته حذف شده است ");
                        }

                        if (item.RelatedAgentTypeId.First() == "10")
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

                        if (item.RelatedAgentTypeId.First() is not "3" and not "1" and not "10" and not "11" and not "12")
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


                        if (item.IsDirty && item.IsNew == false)
                        {
                            if (!masterEntity.SignRequestPersonRelateds.Any(x => x.Id == item.RelatedPersonId.ToGuid()))
                            {
                                apiResult.message.Add(" شخص وابسته حذف شده است ");
                            }
                        }
                    }
                }

                foreach (SignRequestPersonViewModel personItems in request.SignRequestPersons)
                {
                    if (personItems.IsDelete)
                    {
                        if (masterEntity.SignRequestPersonRelateds.Any(x => x.MainPersonId == personItems.PersonId.ToGuid())
                            || masterEntity.SignRequestPersonRelateds.Any(x => x.AgentPersonId == personItems.PersonId.ToGuid()))
                        {
                            apiResult.message.Add($"شخص با کد ملی {personItems.PersonNationalNo} دارای فرد وابسته میباشد ");
                        }
                    }

                    if (personItems.IsDirty && !personItems.IsNew && !personItems.IsDelete)
                    {
                        if (!masterEntity.SignRequestPeople.Any(x => x.Id == personItems.PersonId.ToGuid()))
                        {
                            apiResult.message.Add($"شخص با شماره ملی {personItems.PersonNationalNo} حذف شده است ");
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
