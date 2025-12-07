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
    public class GetMeasurementUnitTypeByLegacyIdQueryHandler : BaseQueryHandler<MeasurementUnitTypeByLegacyIdQuery, ApiResult<MeasurementUnitTypeByIdViewModel>>
    {

        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;

        public GetMeasurementUnitTypeByLegacyIdQueryHandler(IMediator mediator, IUserService userService,
             IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration
            )
            : base(mediator, userService)
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
        }
        protected override bool HasAccess(MeasurementUnitTypeByLegacyIdQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<MeasurementUnitTypeByIdViewModel>> RunAsync(MeasurementUnitTypeByLegacyIdQuery request, CancellationToken cancellationToken)
        {
            if (request.IdList == null || request.IdList.Length == 0)
            {
                return new ApiResult<MeasurementUnitTypeByIdViewModel>(true, ApiResultStatusCode.Success, new MeasurementUnitTypeByIdViewModel());
            }
            string mainUrl = _configuration.GetValue<string>("InternalGatewayUrl") + "api/v1/Specific/Ssar/";
            var result = await _httpEndPointCaller.CallPostApiAsync<MeasurementUnitTypeByIdViewModel, MeasurementUnitTypeByLegacyIdQuery>(
              new HttpEndpointRequest<MeasurementUnitTypeByLegacyIdQuery>(string.Format("{0}Common/GetMeasurementUnitTypeByLegacyId", mainUrl), _userService.UserApplicationContext.Token, new MeasurementUnitTypeByLegacyIdQuery(request.IdList.Distinct().ToArray())), cancellationToken);
            return result;
        }

    }
}
