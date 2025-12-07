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
    public class GetGeolocationByNationalityCodeQueryHandler : BaseQueryHandler<GetGeolocationByNationalityCodeQuery, ApiResult<GetGeolocationByNationalityCodeViewModel>>
    {

        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;

        public GetGeolocationByNationalityCodeQueryHandler(IMediator mediator, IUserService userService,
             IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration
            )
            : base(mediator, userService)
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
        }
        protected override bool HasAccess(GetGeolocationByNationalityCodeQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<GetGeolocationByNationalityCodeViewModel>> RunAsync(GetGeolocationByNationalityCodeQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.NationalityCode)) return new  ApiResult<GetGeolocationByNationalityCodeViewModel>(true, ApiResultStatusCode.Success, new GetGeolocationByNationalityCodeViewModel()); 
            string mainUrl = _configuration.GetValue<string>("InternalGatewayUrl") + "api/v1/Specific/Ssar/";
            var result = await _httpEndPointCaller.CallPostApiAsync<GetGeolocationByNationalityCodeViewModel, GetGeolocationByNationalityCodeQuery>(
              new HttpEndpointRequest<GetGeolocationByNationalityCodeQuery>(string.Format("{0}Common/GetGeoLocationByNationalityCode", mainUrl), _userService.UserApplicationContext.Token, request), cancellationToken);
            return result;

        }

    }
}
