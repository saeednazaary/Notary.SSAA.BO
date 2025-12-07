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
    public class GetGeolocationByIdQueryHandler : BaseQueryHandler<GetGeolocationByIdQuery, ApiResult<GetGeolocationByIdViewModel>>
    {

        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;

        public GetGeolocationByIdQueryHandler(IMediator mediator, IUserService userService,
             IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration
            )
            : base(mediator, userService)
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
        }
        protected override bool HasAccess(GetGeolocationByIdQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<GetGeolocationByIdViewModel>> RunAsync(GetGeolocationByIdQuery request, CancellationToken cancellationToken)
        {
            if (!request.FetchGeolocationOfIran && ( request.IdList == null || request.IdList.Length == 0))
            {
                return new ApiResult<GetGeolocationByIdViewModel>(true, ApiResultStatusCode.Success, new GetGeolocationByIdViewModel());
            }
            string mainUrl = _configuration.GetValue<string>("InternalGatewayUrl") + "api/v1/Specific/Ssar/";
            var result = await _httpEndPointCaller.CallPostApiAsync<GetGeolocationByIdViewModel, GetGeolocationByIdQuery>(
              new HttpEndpointRequest<GetGeolocationByIdQuery>(string.Format("{0}Common/GetGeoLocationById", mainUrl), _userService.UserApplicationContext.Token, request), cancellationToken);
            return result;
        }

    }
}
