using MediatR;
using Notary.SSAA.BO.ServiceHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.EPayment;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.EPayment;
using Notary.SSAA.BO.SharedKernel.Contracts.HttpEndPointCaller;
using Notary.SSAA.BO.DataTransferObject.Validators.EpaymentCostCalculator;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Notary.SSAA.BO.QueryHandler.EUseCaseMethodVendor
{
    public class EpaymentCostCalculatorServiceHandler : BaseServiceHandler<EpaymentCostCalculatorServiceInput, ApiResult<EpaymentCostCalculatorViewModel>>
    {
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;
        private ApiResult<EpaymentCostCalculatorViewModel> apiResult;

        public EpaymentCostCalculatorServiceHandler(IMediator mediator, IUserService userService, ILogger logger, IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration) : base(mediator, userService, logger)
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
            apiResult = new ApiResult<EpaymentCostCalculatorViewModel>();
        }

        protected override bool HasAccess(EpaymentCostCalculatorServiceInput request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Admin) || userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar);
        }

        protected override async Task<ApiResult<EpaymentCostCalculatorViewModel>> ExecuteAsync(EpaymentCostCalculatorServiceInput request, CancellationToken cancellationToken)
        {
            var validator = new EpaymentCostCalculatorValidator();
            ApiResult<EpaymentCostCalculatorViewModel> apiResult = new ApiResult<EpaymentCostCalculatorViewModel>();
            var validateResult = await validator.ValidateAsync(request, cancellationToken);

            if (validateResult.IsValid)
            {
                var PaymentUrl = _configuration.GetValue<string>("InternalGatewayUrl") + "controlPayment/v1/";
                HttpEndpointRequest<EpaymentCostCalculatorServiceInput> httpRequest = new HttpEndpointRequest<EpaymentCostCalculatorServiceInput>(PaymentUrl + "EpaymentCostCalculator/EpaymentCostCalculator", _userService.UserApplicationContext.Token, request);
                apiResult = await _httpEndPointCaller.CallPostApiAsync<EpaymentCostCalculatorViewModel, EpaymentCostCalculatorServiceInput>(httpRequest, cancellationToken);
            }

            return apiResult;
        }
    }
}