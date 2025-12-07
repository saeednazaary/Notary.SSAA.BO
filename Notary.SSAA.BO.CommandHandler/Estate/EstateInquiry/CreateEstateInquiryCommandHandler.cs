using Mapster;
using MediatR;
using Microsoft.Extensions.Configuration;
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
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.CommandHandler.Estate.EstateInquiry
{
    public class CreateEstateInquiryCommandHandler : BaseCommandHandler<CreateEstateInquiryCommand, ApiResult>
    {
        private readonly IEstateInquiryRepository _estateInquiryRepository;
        private readonly IDealSummaryRepository _dealSummaryRepository;
        private readonly IEstateSectionRepository _estateSectionRepository;
        private readonly IEstateSubSectionRepository _estateSubSectionRepository;
        private readonly IEstateSeriDaftarRepository _estateSeridaftarRepository;
        private readonly IRepository<ConfigurationParameter> _configurationParameterRepository;
        private readonly IDateTimeService _dateTimeService;
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;
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
        //private ExternalServiceHelper externalServiceHelper = null;
        private ConfigurationParameterHelper _configurationParameterHelper = null;
        private GeneralExternalServiceHelper _generalExternalServiceHelper = null;
        private BaseInfoServiceHelper _baseInfoServiceHelper = null;
        private EstateInquiryHelper _estateInquiryHelper = null;
        public CreateEstateInquiryCommandHandler(IMediator mediator, IDateTimeService dateTimeService, IUserService userService,
            IEstateInquiryRepository estateInquiryRepository,
            IDealSummaryRepository dealsummaryRepository,
                 IEstateSectionRepository estateSectionRepository,
        IEstateSubSectionRepository estateSubSectionRepository,
        IEstateSeriDaftarRepository estateSeridaftarRepository,
        IRepository<ConfigurationParameter> configurationParameterRepository,
        ILogger logger,
        IConfiguration configuration,
            IHttpEndPointCaller httpEndPointCaller)
            : base(mediator, userService, logger)
        {
            masterEntity = new();
            apiResult = new();
            _dateTimeService = dateTimeService;
            _estateInquiryRepository = estateInquiryRepository;
            _dealSummaryRepository = dealsummaryRepository;
            _estateSectionRepository = estateSectionRepository;
            _estateSeridaftarRepository = estateSeridaftarRepository;
            _estateSubSectionRepository = estateSubSectionRepository;
            _configurationParameterRepository = configurationParameterRepository;
            _configuration = configuration;
            _httpEndPointCaller = httpEndPointCaller;
            _configurationParameterHelper = new ConfigurationParameterHelper(configurationParameterRepository, this._mediator);
            _generalExternalServiceHelper = new GeneralExternalServiceHelper(configurationParameterRepository, this._mediator);
            _estateInquiryHelper = new EstateInquiryHelper(configurationParameterRepository, this._mediator,this._dateTimeService);
            _baseInfoServiceHelper = new BaseInfoServiceHelper(this._mediator);           
        }
        protected override bool HasAccess(CreateEstateInquiryCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
        protected override async Task<ApiResult> ExecuteAsync(CreateEstateInquiryCommand request, CancellationToken cancellationToken)
        {           
            await BusinessValidation(request, cancellationToken);
            if (apiResult.IsSuccess)
            {
                try
                {
                    await UpdateDatabase(request, cancellationToken);
                    if (apiResult.IsSuccess)
                    {

                        if (UpdateFollowedInquiry && followedInquiry != null)
                        {
                            await _estateInquiryRepository.UpdateAsync(followedInquiry, cancellationToken, false);
                        }
                        await _estateInquiryRepository.AddAsync(masterEntity, cancellationToken);                        
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
                catch (DbUpdateException ex)
                {
                    var exp = (Exception)ex;
                    while (exp.InnerException != null)
                        exp = exp.InnerException;
                    bool solved = false;
                    if (EstateInquiryNoUniqunessIsViolated(exp.Message))
                    {
                        do
                        {
                            try
                            {
                                masterEntity.No = (Convert.ToInt64(masterEntity.No) + 1).ToString();
                                await _estateInquiryRepository.AddAsync(masterEntity, cancellationToken);
                                solved = true;
                            }
                            catch (DbUpdateException ex1)
                            {
                                exp = ex1;
                                while (exp.InnerException != null)
                                    exp = exp.InnerException;
                            }
                        }
                        while (!EstateInquiryNoUniqunessIsViolated(exp.Message));
                    }
                    if (EstateInquiryUniqueNoUniqunessIsViolated(exp.Message))
                    {
                        do
                        {
                            try
                            {
                                masterEntity.No2 = (Convert.ToInt64(masterEntity.No2) + 1).ToString();
                                await _estateInquiryRepository.AddAsync(masterEntity, cancellationToken);
                                solved = true;
                            }
                            catch (DbUpdateException ex1)
                            {
                                exp = ex1;
                                while (exp.InnerException != null)
                                    exp = exp.InnerException;
                            }
                        }
                        while (!EstateInquiryUniqueNoUniqunessIsViolated(exp.Message));
                    }
                    if (EstateInquiryNoUniqunessIsViolated(exp.Message))
                    {
                        do
                        {
                            try
                            {
                                masterEntity.No = (Convert.ToInt64(masterEntity.No) + 1).ToString();
                                await _estateInquiryRepository.AddAsync(masterEntity, cancellationToken);
                                solved = true;
                            }
                            catch (DbUpdateException ex1)
                            {
                                exp = ex1;
                                while (exp.InnerException != null)
                                    exp = exp.InnerException;
                            }
                        }
                        while (!EstateInquiryNoUniqunessIsViolated(exp.Message));
                    }
                    if (!solved)
                    {
                        apiResult.IsSuccess = false;
                        apiResult.statusCode = ApiResultStatusCode.Success;
                        apiResult.message.Add("خطا در ثبت استعلام رخ داد ");
                    }
                }
            }
            return apiResult;
        }       
        private async Task<string> CreateEstateInquiryNo(string Scriptoriumid, CancellationToken cancellationToken)
        {
            string str = _dateTimeService.CurrentPersianDate[..4] + "700" + _userService.UserApplicationContext.BranchAccess.BranchCode;
            string No = await _estateInquiryRepository.GetMaxInquiryNo(Scriptoriumid, str, cancellationToken);           
            decimal numberReqNo = decimal.Parse(No);            
            numberReqNo++;
            No = numberReqNo.ToString();            
            return No;
        }
        private async Task UpdateDatabase(CreateEstateInquiryCommand request, CancellationToken cancellationToken)
        {
            if (request.IsNew)
            {
                masterEntity = EstateInquiryMapper.ViewModelToEntity(request);
                masterEntity.ScriptoriumId = _userService.UserApplicationContext.BranchAccess.BranchCode;
                masterEntity.WorkflowStatesId = EstateConstant.EstateInquiryStates.NotSended;
                masterEntity.CreateDate = _dateTimeService.CurrentPersianDate;
                masterEntity.CreateTime = _dateTimeService.CurrentTime;
                masterEntity.No = await CreateEstateInquiryNo(masterEntity.ScriptoriumId, cancellationToken);
                masterEntity.No2 = await CreateEstateInquiryNo2(masterEntity.ScriptoriumId, cancellationToken);
                masterEntity.Ilm = "1";
                masterEntity.Timestamp = 1;
                masterEntity.InquiryDate = _dateTimeService.CurrentPersianDate;
                masterEntity.InquiryKey = masterEntity.ScriptoriumId + ";" + masterEntity.InquiryDate + ";" + masterEntity.InquiryNo;
                if (masterEntity.EstateInquiryId != null && UpdateFollowedInquiry)
                {
                    followedInquiry = await _estateInquiryRepository.GetByIdAsync(cancellationToken, masterEntity.EstateInquiryId.Value);
                    _estateInquiryRepository.LoadReference(followedInquiry, x => x.WorkflowStates);
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
                var log = new EstateInquirySendreceiveLog
                {
                    EstateInquiryId = masterEntity.Id,
                    ActionDate = _dateTimeService.CurrentPersianDate,
                    ActionTime = _dateTimeService.CurrentTime,
                    EstateInquiryActionTypeId = "1",
                    WorkflowStatesId = masterEntity.WorkflowStatesId,
                    ScriptoriumId = masterEntity.ScriptoriumId,
                    Id = Guid.NewGuid()
                };
                masterEntity.EstateInquirySendreceiveLogs.Add(log);

                if (request.InqInquiryPerson != null)
                {
                    if (request.InqInquiryPerson.IsNew)
                    {
                        var inquiryPerson = EstateInquiryMapper.ViewModelToEntity(request.InqInquiryPerson);
                        inquiryPerson.Id = Guid.NewGuid();
                        inquiryPerson.EstateInquiryId = masterEntity.Id;
                        inquiryPerson.Ilm = "1";
                        inquiryPerson.Timestamp = 1;
                        inquiryPerson.ScriptoriumId = masterEntity.ScriptoriumId;
                        if (inquiryPerson.PersonType == EstateConstant.PersonTypeConstant.RealPerson)
                        {
                            if (request.InqInquiryPerson.PersonIsIrani)
                            {
                                inquiryPerson.IsSabtahvalCorrect = sabtAhvalCorrect.ToYesNo();
                                inquiryPerson.IsSabtahvalChecked = sabtAhvalChecked.ToYesNo();
                            }
                            else
                            {
                                inquiryPerson.IsForeignerssysCorrect = foreignerCorrect.ToYesNo();
                                inquiryPerson.IsForeignerssysChecked = foreignerChecked.ToYesNo();
                            }
                        }
                        else
                        {
                            if (request.InqInquiryPerson.PersonIsIrani)
                            {
                                inquiryPerson.IsIlencCorrect = ilencCorrect.ToYesNo();
                                inquiryPerson.IsIlencChecked = ilencChecked.ToYesNo();
                            }
                            else
                            {
                                inquiryPerson.IsForeignerssysCorrect = foreignerCorrect.ToYesNo();
                                inquiryPerson.IsForeignerssysChecked = foreignerChecked.ToYesNo();
                            }
                        }
                        if (request.InqInquiryPerson.PersonSanaState.HasValue && request.InqInquiryPerson.PersonSanaState.Value)
                        {
                            inquiryPerson.SanaInquiryDate = _dateTimeService.CurrentPersianDate;
                            inquiryPerson.SanaInquiryTime = _dateTimeService.CurrentTime;
                        }
                        masterEntity.EstateInquiryPeople.Add(inquiryPerson);
                    }
                    else
                    {
                        apiResult.statusCode = ApiResultStatusCode.Success;
                        apiResult.IsSuccess = false;
                        apiResult.message.Add("شخص استعلام در وضعیت جدید نمی باشد");
                    }
                }
                else
                {
                    apiResult.statusCode = ApiResultStatusCode.Success;
                    apiResult.IsSuccess = false;
                    apiResult.message.Add("استعلام اطلاعات شخص(مالک) را ندارد");
                }
            }
            else
            {
                apiResult.statusCode = ApiResultStatusCode.Success;
                apiResult.IsSuccess = false;
                apiResult.message.Add("استعلام در وضعیت جدید نمی باشد");
            }
        }

        private async Task<string> CreateEstateInquiryNo2(string scriptorumId, CancellationToken cancellationToken)
        {
            string str = _dateTimeService.CurrentPersianDate[..4] + "880" + _userService.UserApplicationContext.BranchAccess.BranchCode;
            string No = await _estateInquiryRepository.GetMaxInquiryNo2(scriptorumId, str, cancellationToken);            
            decimal numberReqNo = decimal.Parse(No);          
            numberReqNo++;
            No = numberReqNo.ToString();            
            return No;
        }
        private async Task BusinessValidation(CreateEstateInquiryCommand request, CancellationToken cancellationToken)
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
            string[] statusList = Array.Empty<string>();            
            var similarInquiryList = await _estateInquiryRepository.GetSimilarInquiryList(_userService.UserApplicationContext.BranchAccess.BranchCode, request.InqUnitId.First(), request.InqPageNo, request.InqEstateNoteNo, request.InqEstateSeridaftarId != null && request.InqEstateSeridaftarId.Count > 0 ? request.InqEstateSeridaftarId.First() : "", request.InqInquiryPerson.PersonNationalityCode, request.InqBasic, request.InqSecondary, request.InqId, request.InqInquiryPerson.PersonExecutiveTransfer, request.InqInquiryPerson.PersonName, request.InqInquiryPerson.PersonFamily, request.InqDocPrintNo, cancellationToken);
            List<string> list = new();
            Dictionary<string, string> unitGeoDictionary = null;
            Dictionary<string, string> scriptoriumGeoDictionary = null;            
            if (similarInquiryList != null && similarInquiryList.Count > 0)
            {
                list = similarInquiryList.Select(est => est.Id.ToString() + "|" + est.InquiryNo + "|" + est.InquiryDate).ToList();
                unitGeoDictionary = await _baseInfoServiceHelper.GetGeolocationOfRegistrationUnit(similarInquiryList.Select(x => x.UnitId).ToArray(), cancellationToken);
                scriptoriumGeoDictionary = await _baseInfoServiceHelper.GetGeolocationOfScriptorium(new string[] { _userService.UserApplicationContext.BranchAccess.BranchCode }, cancellationToken);               
                var(inquiryIdList, _UpdateFollowedInquiry)=await _estateInquiryHelper.CheckSimilarInquiries(request,similarInquiryList, scriptoriumGeoDictionary, unitGeoDictionary, _dealSummaryRepository,apiResult.message,cancellationToken);
                this.UpdateFollowedInquiry = _UpdateFollowedInquiry;
                foreach (string st in inquiryIdList)
                {
                    list.RemoveAll(str => str.StartsWith(st));
                }
            }
            if (list.Count > 0)
            {
                string[] stringArray = list[0].Split('|');
                string enquiryno = stringArray[1];
                string enquirydate = stringArray[2];
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
                    var sanaState = await _estateInquiryHelper.EstateInquiryCheckSana(request.InqInquiryPerson,  cancellationToken);
                    if (!sanaState)
                    {
                        apiResult.message.Add("شخص استعلام(مالک) ، در سامانه ثنا ثبت نام نکرده است");
                    }
                    var (shahkarState, shahkarServiceErrorMessages) = await _estateInquiryHelper.EstateInquiryCheckShahkar(request.InqInquiryPerson, cancellationToken);
                    if (!shahkarState)
                    {
                        if (!shahkarServiceErrorMessages.Remove("ShahkarServiceError"))
                            apiResult.message.Add("کد ملی مالک با شماره تلفن همراه مطابقت ندارد");
                    }
                    if (sabtAhvalMatchingIsRequired == "true" || request.InqInquiryPerson.PersonIsSabtahvalCorrect)
                    {
                        var response = await _generalExternalServiceHelper.CallSabtAhvalService(request.InqInquiryPerson.PersonBirthDate, request.InqInquiryPerson.PersonNationalityCode, cancellationToken);
                        if (response.IsSuccess)
                        {
                            List<string> lstErr = new();
                            _estateInquiryHelper.CompareEstateInquiryPersonBySabtAhval(request.InqInquiryPerson,response.Data, ref lstErr);
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
                    var response = await _generalExternalServiceHelper.CallForeignerRealPersonService(request.InqInquiryPerson.PersonNationalityCode, cancellationToken);
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
                            _estateInquiryHelper.CompareEstateInquiryPersonByIlenc(request.InqInquiryPerson, response.Data,ref lstErr);
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
            if (apiResult.message.Count > 0)
            {
                apiResult.IsSuccess = false;
                apiResult.statusCode = ApiResultStatusCode.Success;
            }
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
                var seriDaftar = await _estateSeridaftarRepository.GetByIdAsync(cancellationToken, estateSeriDaftarId);
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
        public static bool EstateInquiryUniqueNoUniqunessIsViolated(string msg)
        {
            if (string.IsNullOrWhiteSpace(msg)) return false;
            if (msg.StartsWith("ORA-") && msg.Contains("UIXTIUNIQUENO"))
                return true;
            return false;
        }
        public static bool EstateInquiryNoUniqunessIsViolated(string msg)
        {
            if (string.IsNullOrWhiteSpace(msg)) return false;
            if (msg.StartsWith("ORA-") && msg.Contains("UIXNO"))
                return true;
            return false;
        }
    }
}