using MediatR;
using Microsoft.Extensions.Configuration;
using Serilog;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.ExternalServices;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Contracts.HttpEndPointCaller;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.ServiceHandler.Base;


namespace Notary.SSAA.BO.ServiceHandler.ExternalService
{
    internal class SendSmsFromSabtServiceHandler : BaseServiceHandler<SendSmsFromSabtServiceInput, ApiResult<SendSmsFromSabtViewModel>>
    {
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;
        public SendSmsFromSabtServiceHandler(IMediator mediator, IUserService userService, ILogger logger,
            IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration) : base(mediator, userService, logger)
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
        }

        protected async override Task<ApiResult<SendSmsFromSabtViewModel>> ExecuteAsync(SendSmsFromSabtServiceInput request, CancellationToken cancellationToken)
        {
            return await _httpEndPointCaller.CallPostApiAsync<SendSmsFromSabtViewModel, SendSmsFromSabtServiceInput>(new HttpEndpointRequest<SendSmsFromSabtServiceInput>
                (_configuration.GetValue<string>("InternalGatewayUrl") + "ExternalServices/v1/" + "SMSSabtPeerToPeerService", _userService.UserApplicationContext.Token, request), cancellationToken);

        }

        protected override bool HasAccess(SendSmsFromSabtServiceInput request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
    }
}
