

using MediatR;
using Microsoft.Extensions.Configuration;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.ExternalServices.EDM;
using Notary.SSAA.BO.ServiceHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Contracts.HttpEndPointCaller;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Serilog;
using SNotary.SSAA.BO.DataTransferObject.ServiceInputs.Edm;

namespace Notary.SSAA.BO.ServiceHandler.EDM
{
    internal class SignerEdmServiceHandler : BaseServiceHandler<SignEdmServiceInput, ApiResult<SignEdmServiceResponse>>
    {
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;
        public SignerEdmServiceHandler(IMediator mediator, IUserService userService, ILogger logger, IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration) : base(mediator, userService, logger)
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
        }

        protected override async Task<ApiResult<SignEdmServiceResponse>> ExecuteAsync(SignEdmServiceInput request, CancellationToken cancellationToken)
        {
            ApiResult<SignEdmServiceResponse> _apiResult = await _httpEndPointCaller.CallExternalPostApiAsync<ApiResult<SignEdmServiceResponse>, SignEdmServiceInput>(new HttpEndpointRequest<SignEdmServiceInput>
                (_configuration.GetValue<string>("InternalGatewayUrl") + "ExternalServices/v1/Signer", _userService.UserApplicationContext.Token, request), cancellationToken);

            return _apiResult;
        }

        protected override bool HasAccess(SignEdmServiceInput request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar);
        }
    }
}
