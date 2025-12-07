using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.EstateTaxInquiry;
using Notary.SSAA.BO.DataTransferObject.Mappers.Estate.EstateTaxInquiry;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateTaxInquiry;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateTaxInquiry;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.Coordinator.Estate.Helpers;

namespace Notary.SSAA.BO.CommandHandler.Estate.EstateTaxInquiry
{
    public class CreateEstateTaxInquiryCommandHandler : BaseCommandHandler<CreateEstateTaxInquiryCommand, ApiResult>
    {
        private protected Domain.Entities.EstateTaxInquiry masterEntity;
        private protected ApiResult<EstateTaxInquiryViewModel> apiResult;
        private readonly IEstateTaxInquiryRepository _estateTaxInquiryRepository;        
        private readonly IDateTimeService _dateTimeService;
        Notary.SSAA.BO.Domain.Entities.EstateTaxInquiry prevEstateTaxInquiry = null;        
        private readonly BaseInfoServiceHelper _baseInfoServiceHelper = null;        
        public CreateEstateTaxInquiryCommandHandler(IMediator mediator, IUserService userService, ILogger logger, IEstateTaxInquiryRepository estateTaxInquiryRepository, IDateTimeService dateTimeService) : base(mediator, userService, logger)
        {
            _estateTaxInquiryRepository = estateTaxInquiryRepository;
            _dateTimeService = dateTimeService;
            masterEntity = new();
            apiResult = new();                    
            _baseInfoServiceHelper = new BaseInfoServiceHelper(mediator);            
        }
        protected override bool HasAccess(CreateEstateTaxInquiryCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
        protected override async Task<ApiResult> ExecuteAsync(CreateEstateTaxInquiryCommand request, CancellationToken cancellationToken)
        {            
            await BusinessValidation(request, cancellationToken);
            if (apiResult.IsSuccess)
            {
                try
                {
                    await UpdateDatabase(request, cancellationToken);
                    if (apiResult.IsSuccess)
                    {
                        if (prevEstateTaxInquiry != null)
                        {
                            await _estateTaxInquiryRepository.UpdateAsync(prevEstateTaxInquiry, cancellationToken, false);
                        }
                        await _estateTaxInquiryRepository.AddAsync(masterEntity, cancellationToken);                        
                        var response = await _mediator.Send(new GetEstateTaxInquiryByIdQuery() { EstateTaxInquiryId = masterEntity.Id.ToString() }, cancellationToken);
                        if (response.IsSuccess)
                        {
                            apiResult.Data = response.Data.Adapt<EstateTaxInquiryViewModel>();
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
                catch (Exception ex)
                {
                    var exp = ex;
                    while (exp.InnerException != null)
                        exp = exp.InnerException;
                    bool solved = false;
                    if (EstateTaxInquiryNoUniqunessIsViolated(exp.Message))
                    {
                        do
                        {
                            try
                            {
                                masterEntity.No = (Convert.ToInt64(masterEntity.No) + 1).ToString();
                                await _estateTaxInquiryRepository.AddAsync(masterEntity, cancellationToken);
                                solved = true;
                            }
                            catch (Exception ex1)
                            {
                                exp = ex1;
                                while (exp.InnerException != null)
                                    exp = exp.InnerException;
                            }
                        }
                        while (!EstateTaxInquiryNoUniqunessIsViolated(exp.Message));
                    }
                    if (EstateTaxInquiryUniqueNoUniqunessIsViolated(exp.Message))
                    {
                        do
                        {
                            try
                            {
                                masterEntity.No2 = (Convert.ToInt64(masterEntity.No2) + 1).ToString();
                                await _estateTaxInquiryRepository.AddAsync(masterEntity, cancellationToken);
                                solved = true;
                            }
                            catch (Exception ex1)
                            {
                                exp = ex1;
                                while (exp.InnerException != null)
                                    exp = exp.InnerException;
                            }
                        }
                        while (!EstateTaxInquiryUniqueNoUniqunessIsViolated(exp.Message));
                    }
                    if (EstateTaxInquiryNoUniqunessIsViolated(exp.Message))
                    {
                        do
                        {
                            try
                            {
                                masterEntity.No = (Convert.ToInt64(masterEntity.No) + 1).ToString();
                                await _estateTaxInquiryRepository.AddAsync(masterEntity, cancellationToken);
                                solved = true;
                            }
                            catch (Exception ex1)
                            {
                                exp = ex1;
                                while (exp.InnerException != null)
                                    exp = exp.InnerException;
                            }
                        }
                        while (!EstateTaxInquiryNoUniqunessIsViolated(exp.Message));
                    }
                    if (!solved)
                    {
                        apiResult.IsSuccess = false;
                        apiResult.statusCode = ApiResultStatusCode.Success;
                        apiResult.message.Add("خطا در ثبت درخواست رخ داد ");
                    }
                }
            }
            return apiResult;
        }
        private async Task BusinessValidation(CreateEstateTaxInquiryCommand request, CancellationToken cancellationToken)
        {
            prevEstateTaxInquiry = await _estateTaxInquiryRepository.Entities
                .Include(x => x.WorkflowStates)
                .Where(x => x.EstateInquiryId == request.EstateInquiryId.First().ToGuid() && x.IsActive == EstateConstant.BooleanConstant.True)
                .FirstOrDefaultAsync(cancellationToken);
            if (prevEstateTaxInquiry != null)
            {
                if (prevEstateTaxInquiry.WorkflowStates.State == "0" || prevEstateTaxInquiry.WorkflowStates.State == "8")
                {
                    apiResult.message.Add(string.Format("امکان ثبت وجود ندارد.استعلام مشابه  به شماره {0} و تاریخ {1} قبلا ثبت شده است که قابل ویرایش و ارسال  می باشد .لطفا از همان استعلام جهت ویرایش و ارسال به سازمان امور مالیاتی استفاده کنید ", prevEstateTaxInquiry.No, prevEstateTaxInquiry.CreateDate));
                }
            }
            var geoOfIran = await _baseInfoServiceHelper.GetGeoLocationOfIran(cancellationToken);
            if (geoOfIran != null)
            {
                if (request.TheInquiryOwner.PersonIsIrani)
                    request.TheInquiryOwner.PersonNationalityId = new List<string>() { geoOfIran.GeolocationList[0].Id };
                foreach (var buyer in request.TheInquiryBuyersList)
                {
                    if (buyer.PersonIsIrani)
                    {
                        buyer.PersonNationalityId = new List<string>() { geoOfIran.GeolocationList[0].Id };
                    }
                }
            }
            else
            {
                apiResult.message.Add("خطا در دریافت اطلاعات از سرویس اصلاعات پایه ....");
            }
            if (apiResult.message.Count > 0)
            {
                apiResult.IsSuccess = false;
                apiResult.statusCode = ApiResultStatusCode.Success;
            }
        }
        private async Task<string> GetNextNo2(string Scriptoriumid, CancellationToken cancellationToken)
        {
            string str = _dateTimeService.CurrentPersianDate[..4] + "072" + _userService.UserApplicationContext.BranchAccess.BranchCode;
            string No = await _estateTaxInquiryRepository.GetMaxInquiryNo2(Scriptoriumid, str, cancellationToken);
            decimal numberReqNo = decimal.Parse(No);
            numberReqNo++;
            No = numberReqNo.ToString();
            return No;
        }
        private async Task<string> GetNextNo(string Scriptoriumid, CancellationToken cancellationToken)
        {
            string str = _dateTimeService.CurrentPersianDate[..4] + "211" + _userService.UserApplicationContext.BranchAccess.BranchCode;
            string No = await _estateTaxInquiryRepository.GetMaxInquiryNo(Scriptoriumid, str, cancellationToken);
            decimal numberReqNo = decimal.Parse(No);
            numberReqNo++;
            No = numberReqNo.ToString();
            return No;
        }
        private async Task UpdateDatabase(CreateEstateTaxInquiryCommand request, CancellationToken cancellationToken)
        {
            if (request.IsNew)
            {
                if (prevEstateTaxInquiry != null)
                {
                    prevEstateTaxInquiry.IsActive = EstateConstant.BooleanConstant.False;
                }
                masterEntity = EstateTaxInquiryMapper.ViewModelToEntity(request);
                masterEntity.ScriptoriumId = _userService.UserApplicationContext.BranchAccess.BranchCode;
                masterEntity.WorkflowStatesId = "27";
                masterEntity.CreateDate = _dateTimeService.CurrentPersianDate;
                masterEntity.CreateTime = _dateTimeService.CurrentTime;
                masterEntity.No = await GetNextNo(masterEntity.ScriptoriumId, cancellationToken);
                masterEntity.No2 = await GetNextNo2(masterEntity.ScriptoriumId, cancellationToken);
                masterEntity.Ilm = "1";
                masterEntity.Timestamp = 1;
                masterEntity.Id = Guid.NewGuid();
                masterEntity.TrackingCode = "";
                masterEntity.LegacyId = "";
                masterEntity.IsActive = EstateConstant.BooleanConstant.True;
                if (masterEntity.EstateTaxInquiryBuildingStatusId == "01")
                {
                    masterEntity.EstateTaxInquiryBuildingConstructionStepId = null;
                }
                if (masterEntity.IsGroundLevel == EstateConstant.BooleanConstant.True)
                {
                    masterEntity.FloorNo = null;
                }
                if (request.TheInquiryOwner != null)
                {
                    if (request.TheInquiryOwner.IsNew && !request.TheInquiryOwner.IsDelete && request.TheInquiryOwner.IsValid)
                    {
                        var requestPerson = EstateTaxInquiryMapper.ViewModelToEntity(request.TheInquiryOwner);
                        requestPerson.Timestamp = 1;
                        requestPerson.Ilm = "1";
                        requestPerson.ScriptoriumId = masterEntity.ScriptoriumId;
                        requestPerson.Id = Guid.NewGuid();
                        requestPerson.LegacyId = "";
                        requestPerson.ChangeState = "1";
                        masterEntity.EstateTaxInquiryPeople.Add(requestPerson);
                    }
                }
                foreach (var person in request.TheInquiryBuyersList)
                {
                    if (person.IsValid && !person.IsDelete && person.IsNew)
                    {
                        var requestPerson = EstateTaxInquiryMapper.ViewModelToEntity(person);
                        requestPerson.Timestamp = 1;
                        requestPerson.Ilm = "1";
                        requestPerson.ScriptoriumId = masterEntity.ScriptoriumId;
                        requestPerson.Id = Guid.NewGuid();
                        requestPerson.LegacyId = "";
                        requestPerson.ChangeState = "1";
                        masterEntity.EstateTaxInquiryPeople.Add(requestPerson);
                    }
                }
                foreach (var estateAttach in request.TheInquiryEstateAttachList)
                {
                    if (estateAttach.IsValid && !estateAttach.IsDelete && estateAttach.IsNew)
                    {
                        var estateTaxInquiryAttach = EstateTaxInquiryMapper.ViewModelToEntity(estateAttach);
                        estateTaxInquiryAttach.Timestamp = 1;
                        estateTaxInquiryAttach.Ilm = "1";
                        estateTaxInquiryAttach.ScriptoriumId = masterEntity.ScriptoriumId;
                        estateTaxInquiryAttach.Id = Guid.NewGuid();
                        estateTaxInquiryAttach.LegacyId = "";
                        estateTaxInquiryAttach.ChangeState = "1";
                        masterEntity.EstateTaxInquiryAttaches.Add(estateTaxInquiryAttach);
                    }
                }
            }
            else
            {
                apiResult.statusCode = ApiResultStatusCode.Success;
                apiResult.IsSuccess = false;
                apiResult.message.Add("درخواست در وضعیت جدید نمی باشد");
            }
        }            
        public static bool EstateTaxInquiryUniqueNoUniqunessIsViolated(string msg)
        {
            if (string.IsNullOrWhiteSpace(msg)) return false;
            if (msg.StartsWith("ORA-") && msg.Contains("UIXTIUNIQUENO"))
                return true;
            return false;
        }
        public static bool EstateTaxInquiryNoUniqunessIsViolated(string msg)
        {
            if (string.IsNullOrWhiteSpace(msg)) return false;
            if (msg.StartsWith("ORA-") && msg.Contains("UIXNO"))
                return true;
            return false;
        }
    }
}
