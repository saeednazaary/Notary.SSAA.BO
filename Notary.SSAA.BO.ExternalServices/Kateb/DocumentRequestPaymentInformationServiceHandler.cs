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
    internal class DocumentRequestPaymentInformationServiceHandler : BaseServiceHandler<DocumentRequestPaymentInformationServiceInput, ApiResult>
    {
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;
        public DocumentRequestPaymentInformationServiceHandler( IMediator mediator, IUserService userService, ILogger logger,
            IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration ) : base ( mediator, userService, logger )
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
        }

        protected async override Task<ApiResult> ExecuteAsync (DocumentRequestPaymentInformationServiceInput request, CancellationToken cancellationToken )
        {
            return await _httpEndPointCaller.CallPostApiAsync<ApiResult, DocumentRequestPaymentInformationServiceInput> ( new HttpEndpointRequest<DocumentRequestPaymentInformationServiceInput>
                ( _configuration.GetValue<string> ( "InternalGatewayUrl" ) + "KatebSSAR/v1/DocumentRequest/DocumentRequestPaymentInformation", _userService.UserApplicationContext.Token, request ), cancellationToken );

        }

        protected override bool HasAccess (DocumentRequestPaymentInformationServiceInput request, IList<string> userRoles )
        {
            return userRoles.Contains ( RoleConstants.Sardaftar ) || userRoles.Contains(RoleConstants.Admin) || userRoles.Contains ( RoleConstants.Daftaryar ) || userRoles.Contains ( RoleConstants.SanadNevis );
        }
    }
}
