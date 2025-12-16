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
    public class GetUnitByLegacyIdQueryHandler : BaseQueryHandler<GetUnitByLegacyIdQuery, ApiResult<GetUnitByIdViewModel>>
    {

        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;

        public GetUnitByLegacyIdQueryHandler(IMediator mediator, IUserService userService,
             IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration
            )
            : base(mediator, userService)
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
        }
        protected override bool HasAccess(GetUnitByLegacyIdQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<GetUnitByIdViewModel>> RunAsync(GetUnitByLegacyIdQuery request, CancellationToken cancellationToken)
        {
            if (request.LegacyIdList == null || request.LegacyIdList.Length == 0)
            {
                return new ApiResult<GetUnitByIdViewModel>(true, ApiResultStatusCode.Success, new GetUnitByIdViewModel());
            }
            string mainUrl = _configuration.GetValue<string>("InternalGatewayUrl") + "api/v1/Specific/Ssar/";
            var result = await _httpEndPointCaller.CallPostApiAsync<GetUnitByIdViewModel, GetUnitByLegacyIdQuery>(
              new HttpEndpointRequest<GetUnitByLegacyIdQuery>(string.Format("{0}Common/GetUnitByLegacyId", mainUrl), _userService.UserApplicationContext.Token, new GetUnitByLegacyIdQuery(request.LegacyIdList.Distinct().ToArray())), cancellationToken);
            return result;
        }

    }
}
