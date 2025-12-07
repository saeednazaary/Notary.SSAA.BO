using MediatR;
using Microsoft.Extensions.Configuration;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.Kateb;
using Notary.SSAA.BO.ServiceHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Contracts.HttpEndPointCaller;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Serilog;



namespace Notary.SSAA.BO.ServiceHandler.Kateb
{
    internal class UpdateRemoteRequestStateServiceHandler : BaseServiceHandler<UpdateRemoteRequestStateServiceInput, ApiResult>
    {
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;
        public UpdateRemoteRequestStateServiceHandler ( IMediator mediator, IUserService userService, ILogger logger,
            IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration ) : base ( mediator, userService, logger )
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
        }

        protected async override Task<ApiResult> ExecuteAsync ( UpdateRemoteRequestStateServiceInput request, CancellationToken cancellationToken )
        {
            return await _httpEndPointCaller.CallPostApiAsync<ApiResult, UpdateRemoteRequestStateServiceInput> ( new HttpEndpointRequest<UpdateRemoteRequestStateServiceInput>
                ( _configuration.GetValue<string> ( "InternalGatewayUrl" ) + "KatebSSAR/v1/DocumentRequest/UpdateState", _userService.UserApplicationContext.Token, request ), cancellationToken );

        }

        protected override bool HasAccess ( UpdateRemoteRequestStateServiceInput request, IList<string> userRoles )
        {
            return userRoles.Contains ( RoleConstants.Sardaftar ) || userRoles.Contains ( RoleConstants.Daftaryar ) || userRoles.Contains ( RoleConstants.SanadNevis );
        }
    }
}
