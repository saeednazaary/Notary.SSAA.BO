using MediatR;
using Microsoft.Extensions.Configuration;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.BaseInfoService;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.BaseInfoService;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Contracts.HttpEndPointCaller;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.QueryHandler.Estate.BaseInfoService
{
    public class GetOrganizationByIdQueryHandler : BaseQueryHandler<GetOrganizationByIdQuery, ApiResult<OrganizationViewModel>>
    {

        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;

        public GetOrganizationByIdQueryHandler(IMediator mediator, IUserService userService,
             IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration
            )
            : base(mediator, userService)
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
        }
        protected override bool HasAccess(GetOrganizationByIdQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<OrganizationViewModel>> RunAsync(GetOrganizationByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.OrganizationIdList == null || request.OrganizationIdList.Length == 0)
            {
                return new ApiResult<OrganizationViewModel>(true, ApiResultStatusCode.Success, new OrganizationViewModel());
            }
            request.OrganizationIdList = request.OrganizationIdList.Distinct().ToArray();
            string mainUrl = _configuration.GetValue<string>("InternalGatewayUrl") + "api/v1/Generic/Ssar/";
            var result = await _httpEndPointCaller.CallPostApiAsync<OrganizationViewModel, GetOrganizationByIdQuery>(
              new HttpEndpointRequest<GetOrganizationByIdQuery>(string.Format("{0}Common/GetOrganizationByOrganizationId", mainUrl), _userService.UserApplicationContext.Token, request), cancellationToken);
            return result;
        }

    }
}
