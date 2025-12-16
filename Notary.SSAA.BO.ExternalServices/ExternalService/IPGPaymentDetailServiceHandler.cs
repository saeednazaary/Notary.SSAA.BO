using MediatR;
using Microsoft.Extensions.Configuration;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices;
using Notary.SSAA.BO.ServiceHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Contracts.HttpEndPointCaller;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Serilog;


namespace Notary.SSAA.BO.ServiceHandler.ExternalService
{
    internal class IPGPaymentDetailServiceHandler : BaseServiceHandler<IPGPaymentDetailServiceInput, ApiResult<IPGPaymentDetailServiceViewModel>>
    {
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;
        public IPGPaymentDetailServiceHandler(IMediator mediator, IUserService userService, ILogger logger,
            IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration) : base(mediator, userService, logger)
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
        }

        protected async override Task<ApiResult<IPGPaymentDetailServiceViewModel>> ExecuteAsync(IPGPaymentDetailServiceInput request, CancellationToken cancellationToken)
        {
            return await _httpEndPointCaller.CallPostApiAsync<IPGPaymentDetailServiceViewModel, IPGPaymentDetailServiceInput>(new HttpEndpointRequest<IPGPaymentDetailServiceInput>
                (_configuration.GetValue<string>("InternalGatewayUrl") + "ExternalServices/v1/" + "PaymentDetail", _userService.UserApplicationContext.Token, request), cancellationToken);

        }

        protected override bool HasAccess(IPGPaymentDetailServiceInput request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Admin) || userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
    }
}
