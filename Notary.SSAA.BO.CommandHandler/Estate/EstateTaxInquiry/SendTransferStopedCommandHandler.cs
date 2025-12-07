using Mapster;
using MediatR;
using Microsoft.Extensions.Configuration;
using Serilog;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.EstateTaxInquiry;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateTaxInquiry;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateTaxInquiry;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Coordinator.Estate.Helpers;

namespace Notary.SSAA.BO.CommandHandler.Estate.EstateTaxInquiry
{
    public class SendTransferStopedCommandHandler : BaseCommandHandler<SendTransferStopedCommand, ApiResult>
    {
        private protected Domain.Entities.EstateTaxInquiry masterEntity;
        private protected ApiResult<EstateTaxInquiryViewModel> apiResult;
        private readonly IEstateTaxInquiryRepository _estateTaxInquiryRepository;
        private readonly IWorkfolwStateRepository _workfolwStateRepository;
        private readonly IDateTimeService _dateTimeService;
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;
        private readonly IRepository<ConfigurationParameter> _configurationParameterRepository;
        private ConfigurationParameterHelper _configurationParameterHelper = null;
        private readonly ExternalServiceHelper externalServiceHelper = null;
        public SendTransferStopedCommandHandler(IMediator mediator, IUserService userService, ILogger logger, IEstateTaxInquiryRepository estateTaxInquiryRepository, IDateTimeService dateTimeService, IWorkfolwStateRepository workfolwStateRepository, IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration, IRepository<ConfigurationParameter> configurationParameterRepository) : base(mediator, userService, logger)
        {
            _estateTaxInquiryRepository = estateTaxInquiryRepository;
            _workfolwStateRepository = workfolwStateRepository;
            _httpEndPointCaller = httpEndPointCaller;
            _dateTimeService = dateTimeService;
            _configuration = configuration;
            masterEntity = new();
            apiResult = new();
            _configurationParameterRepository = configurationParameterRepository;
            _configurationParameterHelper = new ConfigurationParameterHelper(_configurationParameterRepository, mediator);
            externalServiceHelper = new ExternalServiceHelper(_mediator, _dateTimeService, _userService, _configurationParameterHelper, null, null, null, _configuration, _httpEndPointCaller, _workfolwStateRepository, _estateTaxInquiryRepository);
        }
        protected override bool HasAccess(SendTransferStopedCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
        protected override async Task<ApiResult> ExecuteAsync(SendTransferStopedCommand request, CancellationToken cancellationToken)
        {
            await BusinessValidation(request, cancellationToken);           
            if (apiResult.IsSuccess)
            {
                try
                {
                    var sendResult = await externalServiceHelper.SendTransferStopedToTaxAffairs(request.EstateTaxInquiryId, cancellationToken);
                    if (string.IsNullOrWhiteSpace(sendResult))
                    {
                        await externalServiceHelper.SendEstateTaxInquiryToSabtTerminalServerByService(cancellationToken);
                        var response = await _mediator.Send(new GetEstateTaxInquiryByIdQuery() { EstateTaxInquiryId = request.EstateTaxInquiryId }, cancellationToken);
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
                    else
                    {
                        apiResult.IsSuccess = false;
                        apiResult.statusCode = ApiResultStatusCode.Success;
                        apiResult.message.Add(sendResult);
                    }

                }
                catch (Exception)
                {
                    apiResult.IsSuccess = false;
                    apiResult.statusCode = ApiResultStatusCode.Success;
                    apiResult.message.Add("خطا در ارسال استعلام مالیاتی رخ داد ");

                }
            }
            return apiResult;
        }
        private async Task BusinessValidation(SendTransferStopedCommand request, CancellationToken cancellationToken)
        {
            var masterEntity = await _estateTaxInquiryRepository.GetEstateTaxInquiryById(request.EstateTaxInquiryId, cancellationToken);
            if (masterEntity != null)
            {
                if (masterEntity.IsActive == EstateConstant.BooleanConstant.False)
                {
                    apiResult.message.Add("استعلام  مالیاتی غیر فعال می باشد و امکان اجرای عملیات روی آن وجود ندارد");
                }
                else
                {
                    string[] states = new string[] { "40" };
                    if (!states.Contains(masterEntity.WorkflowStates.State))
                    {
                        apiResult.message.Add("استعلام در وضعیت مجاز برای تمدید گواهی نمی باشد");
                    }
                }
            }
            else
            {
                apiResult.message.Add("استعلام مالیاتی یافت نشد");
            }
            if (apiResult.message.Count > 0)
            {
                apiResult.IsSuccess = false;
                apiResult.statusCode = ApiResultStatusCode.Success;
            }
        }
    }
}
