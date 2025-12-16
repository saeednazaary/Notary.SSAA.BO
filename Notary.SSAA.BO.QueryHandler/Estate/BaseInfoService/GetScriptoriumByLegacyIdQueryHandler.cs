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
    public class GetScriptoriumByLegacyIdQueryHandler : BaseQueryHandler<GetScriptoriumByLegacyIdQuery, ApiResult<GetScriptoriumByIdViewModel>>
    {

        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;

        public GetScriptoriumByLegacyIdQueryHandler(IMediator mediator, IUserService userService,
             IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration
            )
            : base(mediator, userService)
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
        }
        protected override bool HasAccess(GetScriptoriumByLegacyIdQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<GetScriptoriumByIdViewModel>> RunAsync(GetScriptoriumByLegacyIdQuery request, CancellationToken cancellationToken)
        {
            if (request.IdList == null || request.IdList.Length == 0)
            {
                return new ApiResult<GetScriptoriumByIdViewModel>(true, ApiResultStatusCode.Success, new GetScriptoriumByIdViewModel());
            }
            request.IdList = request.IdList.Distinct().ToArray();
            string mainUrl = _configuration.GetValue<string>("InternalGatewayUrl") + "api/v1/Specific/Ssar/";
            var result = await _httpEndPointCaller.CallPostApiAsync<GetScriptoriumByIdViewModel, GetScriptoriumByLegacyIdQuery>(
              new HttpEndpointRequest<GetScriptoriumByLegacyIdQuery>(string.Format("{0}Common/GetScriptoriumByLegacyId", mainUrl), _userService.UserApplicationContext.Token, request), cancellationToken);
            return result;
        }

    }
}
