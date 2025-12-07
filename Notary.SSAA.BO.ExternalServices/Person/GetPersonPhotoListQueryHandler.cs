using MediatR;
using Microsoft.Extensions.Configuration;
using Serilog;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.ServiceHandler.Base;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.Person;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.Person;


namespace Notary.SSAA.BO.ServiceHandler.Person
{
    internal class GetPersonPhotoListQueryHandler : BaseServiceHandler<GetPersonPhotoListServiceInput, ApiResult<GetPersonPhotoListViewModel>>
    {
        private readonly IHttpExternalServiceCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;
        public GetPersonPhotoListQueryHandler(IMediator mediator, IUserService userService, ILogger logger, IHttpExternalServiceCaller httpEndPointCaller,
            IConfiguration configuration) : base(mediator, userService, logger)
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
        }
        protected async override Task<ApiResult<GetPersonPhotoListViewModel>> ExecuteAsync(GetPersonPhotoListServiceInput request, CancellationToken cancellationToken)
        {
           return await CallBOApiHelper.PostAsync<GetPersonPhotoListServiceInput, GetPersonPhotoListViewModel>(request, _configuration.GetValue<string>("InternalGatewayUrl") + "ExternalServices/v1/PhotoList",
                _userService.UserApplicationContext.Token, cancellationToken);

        }
        protected override bool HasAccess(GetPersonPhotoListServiceInput request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.Admin) || userRoles.Contains(RoleConstants.SanadNevis);
        }
    }
}
