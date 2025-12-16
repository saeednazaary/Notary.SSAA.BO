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
using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
using Notary.SSAA.BO.SharedKernel.AppSettingModel;


namespace Notary.SSAA.BO.ServiceHandler.ExternalService
{
    internal class PersonDocumentCartableServiceHandler : BaseServiceHandler<PersonDocumentCartableInput, ApiResult<PersonDocumentCartableViewModel>>
    {
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;
        public PersonDocumentCartableServiceHandler(IMediator mediator, IUserService userService, ILogger logger,
            IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration) : base(mediator, userService, logger)
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
        }

        protected async override Task<ApiResult<PersonDocumentCartableViewModel>> ExecuteAsync(PersonDocumentCartableInput request, CancellationToken cancellationToken)
        {
            var userInfo = _configuration.GetSection("Wrappers_Api_User").Get<WrappersApiUser>();
            request.UserName = userInfo.UserName;
            request.Password = userInfo.Password;
            return await _httpEndPointCaller.CallPostApiAsync<PersonDocumentCartableViewModel, PersonDocumentCartableInput>(new HttpEndpointRequest<PersonDocumentCartableInput>
                (_configuration.GetValue<string>("InternalGatewayUrl") + "SpecificWrappers/v1/DocumentCartable", _userService.UserApplicationContext.Token, request), cancellationToken);

        }

        protected override bool HasAccess(PersonDocumentCartableInput request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
    }
}
