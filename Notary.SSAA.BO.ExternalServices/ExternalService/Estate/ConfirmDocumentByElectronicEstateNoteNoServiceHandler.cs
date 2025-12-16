using MediatR;
using Microsoft.Extensions.Configuration;
using Serilog;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices.Estate;
using Notary.SSAA.BO.ServiceHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Contracts.HttpEndPointCaller;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.ServiceHandler.ExternalService.Estate
{
    public class ConfirmDocumentByElectronicEstateNoteNoServiceHandler : BaseServiceHandler<ConfirmDocumentByElectronicEstateNoteNoServiceInput, ApiResult<ConfirmDocumentByElectronicEstateNoteNoViewModel>>
    {
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;
        public ConfirmDocumentByElectronicEstateNoteNoServiceHandler(IMediator mediator, IUserService userService, ILogger logger,
            IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration) : base(mediator, userService, logger)
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
        }

        protected async override Task<ApiResult<ConfirmDocumentByElectronicEstateNoteNoViewModel>> ExecuteAsync(ConfirmDocumentByElectronicEstateNoteNoServiceInput request, CancellationToken cancellationToken)
        {
            string mainUrl = _configuration.GetValue<string>("InternalGatewayUrl") + "ExternalServices/v1/";
            var receiveUrl = mainUrl + "EstateService/ConfirmDocumentByElectronicEstateNoteNo";
            return await _httpEndPointCaller.CallPostApiAsync<ConfirmDocumentByElectronicEstateNoteNoViewModel, ConfirmDocumentByElectronicEstateNoteNoServiceInput>(new HttpEndpointRequest<ConfirmDocumentByElectronicEstateNoteNoServiceInput>
                (receiveUrl, _userService.UserApplicationContext.Token, request), cancellationToken);

        }

        protected override bool HasAccess(ConfirmDocumentByElectronicEstateNoteNoServiceInput request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis) || userRoles.Contains(RoleConstants.Admin);
        }
    }
}
