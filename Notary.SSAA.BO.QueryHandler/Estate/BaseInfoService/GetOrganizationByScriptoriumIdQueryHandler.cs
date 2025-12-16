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
    public class GetOrganizationByScriptoriumIdQueryHandler : BaseQueryHandler<GetOrganizationByScriptoriumIdQuery, ApiResult<OrganizationViewModel>>
    {

        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;

        public GetOrganizationByScriptoriumIdQueryHandler(IMediator mediator, IUserService userService,
             IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration
            )
            : base(mediator, userService)
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
        }
        protected override bool HasAccess(GetOrganizationByScriptoriumIdQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<OrganizationViewModel>> RunAsync(GetOrganizationByScriptoriumIdQuery request, CancellationToken cancellationToken)
        {
            if (request.ScriptoriumIdList == null || request.ScriptoriumIdList.Length == 0)
            {
                return new ApiResult<OrganizationViewModel>(true, ApiResultStatusCode.Success, new OrganizationViewModel());
            }
            string mainUrl = _configuration.GetValue<string>("InternalGatewayUrl") + "api/v1/Specific/Ssar/";
            var result = await _httpEndPointCaller.CallPostApiAsync<OrganizationViewModel, GetOrganizationByScriptoriumIdQuery>(
              new HttpEndpointRequest<GetOrganizationByScriptoriumIdQuery>(string.Format("{0}Common/GetOrganizationByScriptoriumId", mainUrl), _userService.UserApplicationContext.Token, new GetOrganizationByScriptoriumIdQuery(request.ScriptoriumIdList.Distinct().ToArray())), cancellationToken);
            return result;
        }

    }
}
