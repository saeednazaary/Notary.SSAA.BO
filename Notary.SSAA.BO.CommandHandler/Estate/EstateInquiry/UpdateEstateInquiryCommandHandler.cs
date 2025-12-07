using Mapster;
using MediatR;
using Serilog;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.EstateInquiry;
using Notary.SSAA.BO.DataTransferObject.Mappers.Estate.EstateInquiry;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateInquiry;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.Coordinator.Estate.Helpers;

namespace Notary.SSAA.BO.CommandHandler.Estate.EstateInquiry
{
    public class UpdateEstateInquiryCommandHandler : BaseCommandHandler<UpdateEstateInquiryCommand, ApiResult>
    {
        private readonly IEstateInquiryRepository _estateInquiryRepository;
        private readonly IDealSummaryRepository _dealSummaryRepository;
        private readonly IEstateSectionRepository _estateSectionRepository;
        private readonly IEstateSubSectionRepository _estateSubSectionRepository;
        private readonly IEstateSeriDaftarRepository _estateSeriDaftarRepository;
        private readonly IRepository<ConfigurationParameter> _configurationParameterRepository;
        private readonly IDateTimeService _dateTimeService;       
        private protected Domain.Entities.EstateInquiry masterEntity;
        private protected ApiResult<EstateInquiryViewModel> apiResult;
        private bool UpdateFollowedInquiry = false;
        private Domain.Entities.EstateInquiry followedInquiry = null;
        private bool sabtAhvalChecked = false;
        private bool sabtAhvalCorrect = false;
        private bool ilencChecked = false;
        private bool ilencCorrect = false;
        private bool foreignerChecked = false;
        private bool foreignerCorrect = false;
        private bool hasFollowedInquiry = false;
        private Guid? followedInquiryId = null;
        private Domain.Entities.EstateInquiry fI = null;
        private ConfigurationParameterHelper _configurationParameterHelper = null;
        private GeneralExternalServiceHelper _generalExternalServiceHelper = null;
        private BaseInfoServiceHelper _baseInfoServiceHelper = null;
        private EstateInquiryHelper _estateInquiryHelper = null;
        public UpdateEstateInquiryCommandHandler(IMediator mediator,
            IDateTimeService dateTimeService,
            IEstateInquiryRepository estateInquiryRepository,
            IDealSummaryRepository dealSummaryRepository,
            IEstateSectionRepository estateSectionRepository,
            IEstateSubSectionRepository estateSubSectionRepository,
            IEstateSeriDaftarRepository estateSeriDaftarRepository,
            IUserService userService,
            IRepository<ConfigurationParameter> configurationParameterRepository,
            ILogger logger)
            : base(mediator, userService, logger)
        {
            apiResult = new();
            masterEntity = new();
            _estateInquiryRepository = estateInquiryRepository;
            _dateTimeService = dateTimeService;
            _dealSummaryRepository = dealSummaryRepository;
            _estateSectionRepository = estateSectionRepository;
            _estateSeriDaftarRepository = estateSeriDaftarRepository;
            _estateSubSectionRepository = estateSubSectionRepository;
            _configurationParameterRepository = configurationParameterRepository;
            _configurationParameterHelper = new ConfigurationParameterHelper(configurationParameterRepository, this._mediator);
            _generalExternalServiceHelper = new GeneralExternalServiceHelper(configurationParameterRepository, this._mediator);
            _estateInquiryHelper = new EstateInquiryHelper(configurationParameterRepository, this._mediator, this._dateTimeService);
            _baseInfoServiceHelper = new BaseInfoServiceHelper(this._mediator);
        }
        protected override bool HasAccess(UpdateEstateInquiryCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }       
        protected override async Task<ApiResult> ExecuteAsync(UpdateEstateInquiryCommand request, CancellationToken cancellationToken)
        {            
            bool flag = false;
            masterEntity = await _estateInquiryRepository.GetByIdAsync(cancellationToken, request.InqId.ToGuid());
            await _estateInquiryRepository.LoadCollectionAsync(masterEntity, e => e.EstateInquiryPeople, cancellationToken);
            await _estateInquiryRepository.LoadCollectionAsync(masterEntity, e => e.EstateInquirySendreceiveLogs, cancellationToken);
            if (masterEntity.EstateInquiryId.HasValue && masterEntity.IsFollowedInquiryUpdated == EstateConstant.BooleanConstant.True)
            {
                hasFollowedInquiry = true;
                followedInquiryId = masterEntity.EstateInquiryId;
                fI = await _estateInquiryRepository.GetByIdAsync(cancellationToken, followedInquiryId.Value);
                await _estateInquiryRepository.LoadCollectionAsync(fI, x => x.EstateInquiryPeople, cancellationToken);
            }
            await BusinessValidation(request, cancellationToken);
            if (apiResult.IsSuccess)
            {
                var copiedEstateInquiry = MakeCopy(masterEntity);
                UpdateDatabase(request, cancellationToken);
                if (apiResult.IsSuccess)
                {
                    if (followedInquiry != null && UpdateFollowedInquiry)
                    {
                        await _estateInquiryRepository.UpdateAsync(followedInquiry, cancellationToken, false);
                    }
                    if (hasFollowedInquiry)
                    {
                        if (fI != null && (followedInquiry == null ||  followedInquiry.Id != fI.Id))
                        {
                            masterEntity.PrevFollowedInquiryId = fI.Id;
                            flag = true;
                            if (!string.IsNullOrWhiteSpace(fI.ResponseResult) && fI.ResponseResult == "False")
                            {
                                if (fI.WorkflowStatesId == EstateConstant.EstateInquiryStates.RejectResponse)
                                    fI.WorkflowStatesId = EstateConstant.EstateInquiryStates.ConfirmResponse;
                                fI.ResponseResult = "True";
                                fI.SystemMessage = null;
                                if (followedInquiry == null)
                                    masterEntity.IsFollowedInquiryUpdated = null;
                                fI.Timestamp++;

                                await _estateInquiryRepository.UpdateAsync(fI, cancellationToken, false);
                            }
                        }
                    }
                    await _estateInquiryRepository.UpdateAsync(masterEntity, cancellationToken);

                    var response = await _mediator.Send(new GetEstateInquiryByIdQuery() { EstateInquiryId = masterEntity.Id.ToString() }, cancellationToken);
                    if (response.IsSuccess)
                    {
                        apiResult.Data = response.Data.Adapt<EstateInquiryViewModel>();
                    }
                    else
                    {
                        apiResult.IsSuccess = false;
                        apiResult.statusCode = response.statusCode;
                        apiResult.message.Add("خطا  در بازیابی اطلاعات از پایگاه داده ..... ");
                        apiResult.message = response.message;
                    }
                }
            }
            return apiResult;
        }
        private static Domain.Entities.EstateInquiry MakeCopy(Domain.Entities.EstateInquiry masterEntity)
        {
            Domain.Entities.EstateInquiry estateInquiry = new Domain.Entities.EstateInquiry();
            var type = typeof(Domain.Entities.EstateInquiry);
            var pl = type.GetProperties();
            var sa = new string[] { "DealSummaries"
                ,"DocumentEstateInquiries"
                ,"EstateInquiryNavigation"
                ,"EstateInquiryPeople"
                ,"EstateInquirySendreceiveLogs"
                ,"EstateInquiryType"
                ,"EstateSection"
                ,"EstateSeridaftar"
                ,"EstateSubsection"
                ,"EstateTaxInquiries"
                ,"ForestorgInquiries"
                ,"InverseEstateInquiryNavigation"
                ,"WorkflowStates"
                ,"EstateInquirySendedSms"};
            foreach (var prop in pl)
            {
                if (!sa.Contains(prop.Name))
                    prop.SetValue(estateInquiry, prop.GetValue(masterEntity));
            }
            var person = masterEntity.EstateInquiryPeople.First();
            pl = person.GetType().GetProperties();
            sa = new string[] { "EstateInquiry" };
            var copiedPerson = new Domain.Entities.EstateInquiryPerson();
            foreach (var prop in pl)
            {
                if (!sa.Contains(prop.Name))
                    prop.SetValue(copiedPerson, prop.GetValue(person));
            }
            estateInquiry.EstateInquiryPeople.Add(copiedPerson);

            type = typeof(Domain.Entities.EstateInquirySendreceiveLog);
            pl = type.GetProperties();
            sa = new string[] { "EstateInquiry", "EstateInquiryActionType", "WorkflowStates" };
            foreach (var sendreceiveLog in masterEntity.EstateInquirySendreceiveLogs)
            {
                var newSendreceiveLog = new Domain.Entities.EstateInquirySendreceiveLog();
                foreach (var prop in pl)
                {
                    if (!sa.Contains(prop.Name))
                        prop.SetValue(newSendreceiveLog, prop.GetValue(sendreceiveLog));
                }
                estateInquiry.EstateInquirySendreceiveLogs.Add(newSendreceiveLog);
            }

            type = typeof(Domain.Entities.EstateInquirySendedSm);
            pl = type.GetProperties();
            sa = new string[] { "WorkflowStates", "EstateInquiry" };
            foreach (var sendedSms in masterEntity.EstateInquirySendedSms)
            {
                var newSendedSms = new Domain.Entities.EstateInquirySendedSm();
                foreach (var prop in pl)
                {
                    if (!sa.Contains(prop.Name))
                        prop.SetValue(newSendedSms, prop.GetValue(sendedSms));
                }
                estateInquiry.EstateInquirySendedSms.Add(newSendedSms);
            }
            return estateInquiry;
        }
        private async void UpdateDatabase(UpdateEstateInquiryCommand request, CancellationToken cancellationToken)
        {
            bool timestampAdded = false;
            if (request.IsDirty)
            {
                EstateInquiryMapper.SetEntityValues(request, ref masterEntity);
                masterEntity.InquiryKey = masterEntity.ScriptoriumId + ";" + masterEntity.InquiryDate + ";" + masterEntity.InquiryNo;
                if (masterEntity.EstateInquiryId != null && UpdateFollowedInquiry)
                {
                    followedInquiry = await _estateInquiryRepository.GetByIdAsync(cancellationToken, masterEntity.EstateInquiryId.Value);
                    if (followedInquiry != null)
                    {
                        if (followedInquiry.WorkflowStatesId != EstateConstant.EstateInquiryStates.Invisible && !string.IsNullOrWhiteSpace(followedInquiry.ResponseResult) && followedInquiry.ResponseResult == "True")
                        {
                            if (followedInquiry.WorkflowStatesId != EstateConstant.EstateInquiryStates.Archived)
                                followedInquiry.WorkflowStatesId = EstateConstant.EstateInquiryStates.RejectResponse;
                            followedInquiry.ResponseResult = "False";
                            followedInquiry.SystemMessage = string.Format("استعلام پیرو به شماره {0} و به تاریخ {1} برای این استعلام ثبت شده است", masterEntity.InquiryNo, masterEntity.InquiryDate);
                            followedInquiry.Timestamp++;
                        }
                        masterEntity.IsFollowedInquiryUpdated = EstateConstant.BooleanConstant.True;
                    }
                }
                masterEntity.Timestamp = masterEntity.Timestamp + 1;
                timestampAdded = true;
            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.statusCode = ApiResultStatusCode.Success;
                apiResult.message.Add("استعلام در وضعیت ویرایش شده نمی باشد");
            }
            if (request.InqInquiryPerson != null)
            {
                if (request.InqInquiryPerson.IsDirty)
                {
                    var person = masterEntity.EstateInquiryPeople.First();
                    var previousSanaState = person.SanaState.ToBoolean();
                    EstateInquiryMapper.SetEntityValues(request.InqInquiryPerson, ref person);
                    if (!timestampAdded)
                        masterEntity.Timestamp = masterEntity.Timestamp + 1;
                    person.Timestamp = person.Timestamp + 1;
                    person.ScriptoriumId = masterEntity.ScriptoriumId;
                    person.IsSabtahvalCorrect = null;
                    person.IsSabtahvalChecked = null;
                    person.IsIlencCorrect = null;
                    person.IsIlencChecked = null;
                    person.IsForeignerssysChecked = null;
                    person.IsForeignerssysCorrect = null;
                    if (person.PersonType == EstateConstant.PersonTypeConstant.RealPerson)
                    {
                        if (request.InqInquiryPerson.PersonIsIrani)
                        {
                            person.IsSabtahvalCorrect = sabtAhvalCorrect.ToYesNo();
                            person.IsSabtahvalChecked = sabtAhvalChecked.ToYesNo();
                        }
                        else
                        {
                            person.IsForeignerssysCorrect = foreignerCorrect.ToYesNo();
                            person.IsForeignerssysChecked = foreignerChecked.ToYesNo();
                        }
                    }
                    else
                    {
                        if (request.InqInquiryPerson.PersonIsIrani)
                        {
                            person.IsIlencCorrect = ilencCorrect.ToYesNo();
                            person.IsIlencChecked = ilencChecked.ToYesNo();
                        }
                        else
                        {
                            person.IsForeignerssysCorrect = foreignerCorrect.ToYesNo();
                            person.IsForeignerssysChecked = foreignerChecked.ToYesNo();
                        }
                    }
                    if (request.InqInquiryPerson.PersonSanaState.HasValue)
                    {
                        if (request.InqInquiryPerson.PersonSanaState.Value)
                        {
                            if (string.IsNullOrWhiteSpace(person.SanaInquiryDate))
                                person.SanaInquiryDate = _dateTimeService.CurrentPersianDate;
                            if (string.IsNullOrWhiteSpace(person.SanaInquiryTime))
                                person.SanaInquiryTime = _dateTimeService.CurrentTime;
                        }
                        else if (previousSanaState)
                        {
                            person.SanaInquiryDate = null;
                            person.SanaInquiryTime = null;
                        }
                    }
                }
            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.statusCode = ApiResultStatusCode.Success;
                apiResult.message.Add("استعلام اطلاعات شخص(مالک) را ندارد");
            }
        }        
        private async Task BusinessValidation(UpdateEstateInquiryCommand request, CancellationToken cancellationToken)
        {
            if (masterEntity != null)
            {
                string[] states = new string[] { EstateConstant.EstateInquiryStates.NotSended,EstateConstant.EstateInquiryStates.NeedDocument,EstateConstant.EstateInquiryStates.NeedCorrection };
                if (string.IsNullOrWhiteSpace(masterEntity.WorkflowStatesId))
                {
                    apiResult.message.Add("استعلام غیر قابل ویرایش می باشد");
                }
                else if (!states.Contains(masterEntity.WorkflowStatesId))
                {
                    apiResult.message.Add("استعلام غیر قابل ویرایش می باشد");

                }
                else
                {
                    if (request.InqInquiryPerson.PersonIsIrani)
                    {
                        var geoOfIran = await _baseInfoServiceHelper.GetGeoLocationOfIran(cancellationToken);
                        if (geoOfIran != null)
                        {
                            request.InqInquiryPerson.PersonNationalityId = new List<string>() { geoOfIran.GeolocationList[0].Id };
                        }
                        else
                        {
                            apiResult.message.Add("خطا در دریافت اطلاعات از سرویس اصلاعات پایه ....");
                        }
                    }
                    if (!await CheckSectionAndSubSectionAndSeridaftar(request.InqUnitId.First(), request.InqEstateSectionId.First(), request.InqEstateSubsectionId.First(), request.InqEstateSeridaftarId != null && request.InqEstateSeridaftarId.Count > 0 ? request.InqEstateSeridaftarId.First() : "", cancellationToken))
                        apiResult.message.Add("سری دفتر ، بخش  یا ناحیه با هم تطابق ندارند لطفا آنها را درست انتخاب نمایید");
                    if (await _estateInquiryRepository.IsExistsInquiry(!string.IsNullOrWhiteSpace(request.InqInquiryDate) ? request.InqInquiryDate : _dateTimeService.CurrentPersianDate, request.InqInquiryNo, request.InqId, _userService.UserApplicationContext.BranchAccess.BranchCode, cancellationToken))
                    {
                        apiResult.message.Add("یک دفتر خانه در  یک سال ، دو استعلام با یک شماره نمی تواند ثبت کند");
                    }
                    if (hasFollowedInquiry && (request.InqEstateInquiryId == null || request.InqEstateInquiryId.Count == 0 ||  request.InqEstateInquiryId.First().ToGuid() != fI.Id))
                    {
                        if (await CompareInquiry(fI, _userService.UserApplicationContext.BranchAccess.BranchCode, request.InqUnitId.First(), request.InqPageNo, request.InqEstateNoteNo, request.InqEstateSeridaftarId != null && request.InqEstateSeridaftarId.Count > 0 ? request.InqEstateSeridaftarId.First() : "", request.InqInquiryPerson.PersonNationalityCode, request.InqBasic, request.InqSecondary, request.InqId, request.InqInquiryPerson.PersonExecutiveTransfer, request.InqInquiryPerson.PersonName, request.InqInquiryPerson.PersonFamily, request.InqDocPrintNo, cancellationToken))
                        {
                            apiResult.message.Add(string.Format("شما قبلا استعلامی را با شماره {0}  در تاریخ {1} با این مشخصات ثبت کرده اید ", fI.InquiryNo, fI.InquiryDate));
                        }
                    }
                    List<string> list = new();                    
                    var similarInquiryList = await _estateInquiryRepository.GetSimilarInquiryList(_userService.UserApplicationContext.BranchAccess.BranchCode, request.InqUnitId.First(), request.InqPageNo, request.InqEstateNoteNo, request.InqEstateSeridaftarId != null && request.InqEstateSeridaftarId.Count > 0 ? request.InqEstateSeridaftarId.First() : "", request.InqInquiryPerson.PersonNationalityCode, request.InqBasic, request.InqSecondary, request.InqId, request.InqInquiryPerson.PersonExecutiveTransfer, request.InqInquiryPerson.PersonName, request.InqInquiryPerson.PersonFamily, request.InqDocPrintNo, cancellationToken);
                    Dictionary<string, string> unitGeoDictionary = null;
                    Dictionary<string, string> scriptoriumGeoDictionary = null;
                    if (similarInquiryList != null && similarInquiryList.Count > 0)
                    {
                        list = similarInquiryList.Select(est => est.Id.ToString() + "|" + est.InquiryNo + "|" + est.InquiryDate).ToList();
                        unitGeoDictionary = await _baseInfoServiceHelper.GetGeolocationOfRegistrationUnit(similarInquiryList.Select(x => x.UnitId).ToArray(), cancellationToken);
                        scriptoriumGeoDictionary = await _baseInfoServiceHelper.GetGeolocationOfScriptorium(new string[] { _userService.UserApplicationContext.BranchAccess.BranchCode }, cancellationToken);
                        var (inquiryIdList, _UpdateFollowedInquiry) = await _estateInquiryHelper.CheckSimilarInquiries(request, similarInquiryList, scriptoriumGeoDictionary, unitGeoDictionary, _dealSummaryRepository, apiResult.message, cancellationToken);
                        this.UpdateFollowedInquiry = _UpdateFollowedInquiry;
                        foreach (string st in inquiryIdList)
                        {
                            list.RemoveAll(str => str.StartsWith(st));
                        }
                    }
                    if (list.Count > 0)
                    {
                        string[] sarr = list[0].Split('|');
                        string enquiryno = sarr[1];
                        string enquirydate = sarr[2];
                        apiResult.message.Add(string.Format("شما قبلا استعلامی را با شماره {0}  در تاریخ {1} با این مشخصات ثبت کرده اید ", enquiryno, enquirydate));
                    }                   
                    if (request.InqEstateInquiryTypeId[0] == "2")
                    {                        
                        var result = await _configurationParameterHelper.CurrentEstateInquiryIsEnabled(cancellationToken);
                        if (result != "true")
                        {
                            apiResult.message.Add("امکان ثبت استعلام ملک جاری غیر فعال شده است");
                        }
                    }
                    if (request.InqInquiryPerson.PersonType == EstateConstant.PersonTypeConstant.RealPerson && !request.InqInquiryPerson.PersonExecutiveTransfer)
                    {                        
                        var sabtAhvalMatchingIsRequired = await _configurationParameterHelper.EstateInquirySabtAhvalMatchingIsRequired(cancellationToken);
                        if (request.InqInquiryPerson.PersonIsIrani)
                        {
                            var sanaState = await _estateInquiryHelper.EstateInquiryCheckSana(request.InqInquiryPerson, cancellationToken);
                            if (!sanaState)
                            {
                                apiResult.message.Add("شخص استعلام ، در سامانه ثنا ثبت نام نکرده است");
                            }
                            var (shahkarState,shahkarErrors) = await _estateInquiryHelper.EstateInquiryCheckShahkar(request.InqInquiryPerson, cancellationToken);
                            if (!shahkarState)
                            {
                                if (!shahkarErrors.Remove("ShahkarServiceError"))
                                    apiResult.message.Add("کد ملی شخص با شماره تلفن همراه مطابقت ندارد");
                            }
                            if (sabtAhvalMatchingIsRequired == "true" || request.InqInquiryPerson.PersonIsSabtahvalCorrect)
                            {
                                var response = await _generalExternalServiceHelper.CallSabtAhvalService(request.InqInquiryPerson.PersonBirthDate,request.InqInquiryPerson.PersonNationalityCode , cancellationToken);
                                if (response.IsSuccess)
                                {
                                    List<string> lstErr = new();
                                    _estateInquiryHelper.CompareEstateInquiryPersonBySabtAhval(request.InqInquiryPerson,response.Data,ref lstErr);
                                    sabtAhvalChecked = true;
                                    if (lstErr.Count == 0)
                                    {
                                        sabtAhvalCorrect = true;
                                    }
                                    if (lstErr.Count > 0 && !request.InqInquiryPerson.PersonExecutiveTransfer)
                                    {
                                        apiResult.message.AddRange(lstErr);
                                    }                                    
                                }
                                else
                                {
                                    sabtAhvalChecked = true;
                                    if (!request.InqInquiryPerson.PersonExecutiveTransfer)
                                    {
                                        apiResult.message.Add("اطلاعات مالک در ثبت احوال یافت نشد");
                                    }
                                }
                            }
                        }
                        else
                        {
                            var response = await _generalExternalServiceHelper.CallForeignerRealPersonService(request.InqInquiryPerson.PersonNationalityCode , cancellationToken);
                            if (response.Data.IsForeignerServiceEnabled)
                            {
                                if (response.IsSuccess)
                                {
                                    List<string> lstErr = new();
                                    _estateInquiryHelper.CompareEstateInquiryPersonByForeignerService(request.InqInquiryPerson, response.Data, ref lstErr);
                                    foreignerChecked = true;
                                    if (lstErr.Count == 0)
                                    {
                                        foreignerCorrect = true;
                                    }
                                    if (lstErr.Count > 0 && !request.InqInquiryPerson.PersonExecutiveTransfer)
                                    {
                                        apiResult.message.AddRange(lstErr);
                                    }
                                }
                                else
                                {
                                    foreignerChecked = true;
                                    if (!request.InqInquiryPerson.PersonExecutiveTransfer)
                                    {
                                        apiResult.message.Add("اطلاعات مالک در سامانه اتباع خارجی یافت نشد");
                                    }
                                }
                            }
                        }
                    }
                    else if (request.InqInquiryPerson.PersonType == EstateConstant.PersonTypeConstant.LegalPerson && !request.InqInquiryPerson.PersonExecutiveTransfer)
                    {                        
                        var ilencMatchingIsRequired = await _configurationParameterHelper.EstateInquiryIlencMatchingIsEnabled(cancellationToken);
                        if (request.InqInquiryPerson.PersonIsIrani)
                        {
                            if (ilencMatchingIsRequired == "true" || request.InqInquiryPerson.PersonIsIlencCorrect)
                            {
                                var response = await _generalExternalServiceHelper.CallIlencService(request.InqInquiryPerson.PersonNationalityCode, cancellationToken);
                                if (response.IsSuccess && response.Data != null && response.Data.Result.Successful)
                                {
                                    List<string> lstErr = new();
                                    _estateInquiryHelper.CompareEstateInquiryPersonByIlenc(request.InqInquiryPerson, response.Data, ref lstErr);
                                    ilencChecked = true;
                                    if (lstErr.Count == 0)
                                    {
                                        ilencCorrect = true;
                                    }
                                    if (lstErr.Count > 0 && !request.InqInquiryPerson.PersonExecutiveTransfer)
                                    {
                                        apiResult.message.AddRange(lstErr);
                                    }
                                }
                                else
                                {
                                    ilencChecked = true;
                                    if (!request.InqInquiryPerson.PersonExecutiveTransfer)
                                    {
                                        if (response.Data != null && !response.Data.Result.Successful)
                                            apiResult.message.Add(response.Data.Result.Message);
                                        else
                                            apiResult.message.Add(string.Format("اطلاعات شخص ({0}) در سامانه اشخاص حقوقی یافت نشد", request.InqInquiryPerson.PersonName));
                                    }
                                }
                            }
                        }
                        else
                        {
                            var response = await _generalExternalServiceHelper.CallForeignerLegalPersonService(request.InqInquiryPerson.PersonNationalityCode, cancellationToken);
                            if (response.Data.IsForeignerServiceEnabled)
                            {
                                if (response.IsSuccess)
                                {
                                    List<string> lstErr = new();
                                    _estateInquiryHelper.CompareEstateInquiryPersonByForeignerService(request.InqInquiryPerson, response.Data, ref lstErr);
                                    foreignerChecked = true;
                                    if (lstErr.Count == 0)
                                    {
                                        foreignerCorrect = true;
                                    }
                                    if (lstErr.Count > 0 && !request.InqInquiryPerson.PersonExecutiveTransfer)
                                    {
                                        apiResult.message.AddRange(lstErr);
                                    }
                                }
                                else
                                {
                                    foreignerChecked = true;
                                    if (!request.InqInquiryPerson.PersonExecutiveTransfer)
                                    {
                                        apiResult.message.Add("اطلاعات مالک در سامانه اتباع خارجی یافت نشد");
                                    }
                                }
                            }
                        }
                    }

                }
            }
            if (apiResult.message.Count > 0)
            {
                apiResult.statusCode = ApiResultStatusCode.Success;
                apiResult.IsSuccess = false;
            }
        }        
        private async Task<bool> CompareInquiry(Domain.Entities.EstateInquiry fi, string scriptoriumId, string unitId, string pageNumber, string notebookNo, string seriDaftarId, string melliCode, string basic, string secondary, string inquiryId, bool isExecuteTransfer, string name, string family, string docPrintNo, CancellationToken cancellationToken)
        {
            bool predicate(Domain.Entities.EstateInquiry e)
            {
                bool r = e.Id.ToString() != inquiryId && e.ScriptoriumId == scriptoriumId && e.UnitId == unitId && e.Basic == basic && e.DocPrintNo == docPrintNo;
                if (!string.IsNullOrWhiteSpace(secondary))
                    r = r && e.Secondary == secondary;
                else
                    r = string.IsNullOrWhiteSpace(e.Secondary);
                if (!string.IsNullOrWhiteSpace(pageNumber))
                    r = r && e.PageNo == pageNumber;
                if (!string.IsNullOrWhiteSpace(notebookNo))
                    r = r && e.EstateNoteNo == notebookNo;
                if (!string.IsNullOrWhiteSpace(seriDaftarId))
                    r = r && e.EstateSeridaftarId == seriDaftarId;
                if (isExecuteTransfer)
                {

                    r = r && (e.EstateInquiryPeople.First().Name == name.PersianToArabic().Trim() || e.EstateInquiryPeople.First().Name == name.ArabicTopersian().Trim() || e.EstateInquiryPeople.First().Name == name);
                    if (!string.IsNullOrWhiteSpace(family))
                    {

                        r = r && (e.EstateInquiryPeople.First().Family == family.PersianToArabic().Trim() || e.EstateInquiryPeople.First().Family == family.ArabicTopersian().Trim() || e.EstateInquiryPeople.First().Family == family);
                    }
                }
                else
                {
                    r = r && e.EstateInquiryPeople.First().NationalityCode == melliCode ;
                }
                return r;
            }

            string date;
            if (fi.ResponseDate != null && fi.ResponseDate != string.Empty)
            {
                date = fi.ResponseDate;
            }
            else if (fi.TrtsReadDate != null && fi.TrtsReadDate != string.Empty)
            {
                date = fi.TrtsReadDate;
            }
            else if (fi.LastSendDate != null && fi.LastSendDate != string.Empty)
            {
                date = fi.LastSendDate;
            }
            else
            {
                date = fi.FirstSendDate;
            }

            if (!string.IsNullOrWhiteSpace(date))
            {
                TimeSpan ts = _dateTimeService.CurrentPersianDate.GetDateTimeDistance(date);
                var scriptoriumGeoDictionary = await _baseInfoServiceHelper.GetGeolocationOfScriptorium(new string[] { fi.ScriptoriumId }, cancellationToken);
                var unitGeoDictionary = await _baseInfoServiceHelper.GetGeolocationOfRegistrationUnit(new string[] { fi.UnitId }, cancellationToken);
                var unitGeolocationId = unitGeoDictionary[fi.UnitId];
                var ScriptoriumGeolocationId = scriptoriumGeoDictionary[fi.ScriptoriumId];
                if (unitGeolocationId == ScriptoriumGeolocationId)
                {
                    if (Convert.ToInt32(Math.Ceiling(ts.TotalDays)) > 37)
                    {
                        return false;
                    }
                }
                else
                {
                    if (Convert.ToInt32(Math.Ceiling(ts.TotalDays)) > 50)
                    {
                        return false;
                    }
                }
            }
            return predicate(fi);
        }                   
        public async Task<bool> CheckSectionAndSubSectionAndSeridaftar(string unitId, string estateSectionId, string estateSubSectionId, string estateSeriDaftarId, CancellationToken cancellationToken)
        {
            bool r = true;
            var section = await _estateSectionRepository.GetByIdAsync(cancellationToken, estateSectionId);
            var subSection = await _estateSubSectionRepository.GetByIdAsync(cancellationToken, estateSubSectionId);
            if (section != null)
            {
                if (section.UnitId != unitId)
                    r = false;
                if (subSection != null)
                {
                    if (subSection.EstateSectionId != section.Id)
                        r = false;
                }
                else
                    r = false;
            }
            else
                r = false;

            if (!string.IsNullOrWhiteSpace(estateSeriDaftarId))
            {
                var seriDaftar = await _estateSeriDaftarRepository.GetByIdAsync(cancellationToken, estateSeriDaftarId);
                if (seriDaftar != null)
                {
                    if (seriDaftar.UnitId != unitId)
                        r = false;
                }
                else
                    r = false;
            }

            return r;
        }

    }
}
