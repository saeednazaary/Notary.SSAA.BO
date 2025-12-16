using Mapster;
using MediatR;
using Microsoft.Extensions.Configuration;
using Serilog;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.DealSummary;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.DealSummary;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.DealSummary;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.CommandHandler.Estate.DealSummary
{
    public class UnRestrictDealSummaryCommandHandler : BaseCommandHandler<UnRestrictDealSummaryCommand, ApiResult>
    {
        private readonly IDealSummaryRepository _dealSummaryRepository;
        private protected Domain.Entities.DealSummary masterEntity;
        private protected ApiResult<DealSummaryViewModel> apiResult;
        private readonly IRepository<ConfigurationParameter> _configurationParameterRepository;
        private ExternalServiceHelper externalServiceHelper = null;
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IDateTimeService _dateTimeService;
        private readonly IConfiguration _configuration;
        private BO.Coordinator.Estate.Helpers.ConfigurationParameterHelper _configurationParameterHelper;
        public UnRestrictDealSummaryCommandHandler(IMediator mediator, IDealSummaryRepository dealSummaryRepository, IUserService userService,
            ILogger logger, IRepository<ConfigurationParameter> configurationParameterRepository, IHttpEndPointCaller httpEndPointCaller, IDateTimeService dateTimeService, IConfiguration configuration)
            : base(mediator, userService, logger)
        {
            apiResult = new();
            _dealSummaryRepository = dealSummaryRepository;
            _configurationParameterRepository = configurationParameterRepository;
            _httpEndPointCaller = httpEndPointCaller;
            _dateTimeService = dateTimeService;
            _configuration = configuration;
            this._configurationParameterHelper = new Coordinator.Estate.Helpers.ConfigurationParameterHelper(configurationParameterRepository,this._mediator);
            this.externalServiceHelper = new ExternalServiceHelper(_mediator,
                                                                  dateTimeService,                                                                
                                                                 userService,
                                                                 _configurationParameterHelper,
                                                                 null,
                                                                 null,
                                                                 null,
                                                                 _configuration,
                                                                 _httpEndPointCaller,
                                                                 null,
                                                                 null
                                                                 );
        }
        protected override async Task<ApiResult> ExecuteAsync(UnRestrictDealSummaryCommand request, CancellationToken cancellationToken)
        {

            masterEntity = await _dealSummaryRepository.GetByIdAsync(cancellationToken,request.DealSummaryId.ToGuid());
            await _dealSummaryRepository.LoadReferenceAsync(masterEntity, d => d.DealSummaryTransferType, cancellationToken);
            await _dealSummaryRepository.LoadReferenceAsync(masterEntity, d => d.EstateInquiry, cancellationToken);
            BusinessValidation(request);
            if (apiResult.IsSuccess)
            {
                var prevWorkflowStatesId = masterEntity.WorkflowStatesId;
                var prevRemoveRestrictionDate = masterEntity.RemoveRestrictionDate;
                var prevRemoveRestrictionNo = masterEntity.RemoveRestrictionNo;
                var prevUnrestrictionTypeId = masterEntity.UnrestrictionTypeId;
                var prevTimestamp = masterEntity.Timestamp;

                masterEntity.WorkflowStatesId = EstateConstant.DealSummaryStates.SendRemoveRestriction;
                masterEntity.RemoveRestrictionDate = request.RemoveRestrictionDate;
                masterEntity.RemoveRestrictionNo = request.RemoveRestrictionNo;
                masterEntity.UnrestrictionTypeId = request.RemoveRestrictionTypeId;
                masterEntity.Timestamp+=1;
                await _dealSummaryRepository.UpdateAsync(masterEntity, cancellationToken);

                if (await _configurationParameterHelper.IsDealSummaryRealSendEnabled(cancellationToken))
                {
                    
                    var sendResult = await SendToSabtEstateSystemByService(cancellationToken);
                    if (!string.IsNullOrWhiteSpace(sendResult))
                    {
                        masterEntity.WorkflowStatesId = prevWorkflowStatesId;
                        masterEntity.RemoveRestrictionDate = prevRemoveRestrictionDate;
                        masterEntity.RemoveRestrictionNo = prevRemoveRestrictionNo;
                        masterEntity.UnrestrictionTypeId = prevUnrestrictionTypeId;
                        masterEntity.Timestamp = prevTimestamp;
                        await _dealSummaryRepository.UpdateAsync(masterEntity, cancellationToken);                        
                        apiResult.IsSuccess = false;
                        apiResult.message.Add(sendResult);
                        apiResult.statusCode = ApiResultStatusCode.Success;
                    }
                    else
                    {
                        apiResult.IsSuccess = true;
                        apiResult.message = new()
                                              {
                                                "ارسال رفع محدودیت خلاصه معامله به سامانه املاک با موفقیت انجام شد"
                                              };
                        apiResult.statusCode = ApiResultStatusCode.Success;
                    }
                }
                else
                {
                    apiResult.IsSuccess = true;
                    apiResult.message = new()
                                              {
                                                "ارسال رفع محدودیت خلاصه معامله به سامانه املاک با موفقیت انجام شد"
                                              };
                    apiResult.statusCode = ApiResultStatusCode.Success;
                }
                var response = await _mediator.Send(new GetDealSummaryByIdQuery() { DealSummaryId = masterEntity.Id.ToString() }, cancellationToken);
                if (response.IsSuccess)
                {
                    apiResult.Data = response.Data.Adapt<DealSummaryViewModel>();
                }
                else
                {
                    apiResult.IsSuccess = false;
                    apiResult.statusCode = response.statusCode;
                    apiResult.message.Add("خطا  در بازیابی اطلاعات از پایگاه داده ..... ");
                    apiResult.message = response.message;
                }

            }
            return apiResult;
        }
        //bool needToRollback = false;       
        private async Task<string> SendToSabtEstateSystemByService(CancellationToken cancellationToken)
        {
            await _dealSummaryRepository.LoadReferenceAsync(masterEntity, x => x.UnrestrictionType, cancellationToken);            
            externalServiceHelper.DealSummary = masterEntity;
            await externalServiceHelper.GetDealSummaryBaseInfoData(cancellationToken);
            return await externalServiceHelper.SendDealSummaryToSabtEstateSystemByService(cancellationToken);
           
        }
        protected override bool HasAccess(UnRestrictDealSummaryCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
        private void BusinessValidation(UnRestrictDealSummaryCommand request)
        {
            if (masterEntity != null)
            {
                if (masterEntity.DealSummaryTransferType.Isrestricted != EstateConstant.BooleanConstant.True)
                {
                    apiResult.message.Add("خلاصه معامله از نوع محدودیت نمی باشد");
                }                
                else if(!string.IsNullOrWhiteSpace(masterEntity.RemoveRestrictionNo) ||
                    !string.IsNullOrWhiteSpace(masterEntity.RemoveRestrictionDate) ||
                    !string.IsNullOrWhiteSpace(masterEntity.UnrestrictionTypeId)
                    )
                {
                    apiResult.message.Add("خلاصه معامله قبلا رفع محدودیت شده است");
                }
                else if (masterEntity.WorkflowStatesId != EstateConstant.DealSummaryStates.Responsed)
                {
                    apiResult.message.Add("خلاصه معامله در وضعیت مجاز برای ارسال رفع محدودیت نمی باشد(هنوز پاسخ داده نشده است)");
                }
            }
            else
            {
                apiResult.message.Add("خلاصه معامله یافت نشد");
            }
            if (apiResult.message.Count > 0)
            {
                apiResult.statusCode = ApiResultStatusCode.Success;
                apiResult.IsSuccess = false;
            }
        }
    }
}
