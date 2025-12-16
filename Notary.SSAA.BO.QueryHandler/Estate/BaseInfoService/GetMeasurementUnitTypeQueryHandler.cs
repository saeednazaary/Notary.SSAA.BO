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
    public class GetMeasurementUnitTypeQueryHandler : BaseQueryHandler<MeasurementUnitTypeByIdQuery, ApiResult<MeasurementUnitTypeByIdViewModel>>
    {

        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;

        public GetMeasurementUnitTypeQueryHandler(IMediator mediator, IUserService userService,
             IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration
            )
            : base(mediator, userService)
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
        }
        protected override bool HasAccess(MeasurementUnitTypeByIdQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<MeasurementUnitTypeByIdViewModel>> RunAsync(MeasurementUnitTypeByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.IdList == null || request.IdList.Length == 0)
            {
                return new ApiResult<MeasurementUnitTypeByIdViewModel>(true, ApiResultStatusCode.Success, new MeasurementUnitTypeByIdViewModel());
            }
            string mainUrl = _configuration.GetValue<string>("InternalGatewayUrl") + "api/v1/Specific/Ssar/";
            var result = await _httpEndPointCaller.CallPostApiAsync<MeasurementUnitTypeByIdViewModel, MeasurementUnitTypeByIdQuery>(
              new HttpEndpointRequest<MeasurementUnitTypeByIdQuery>(string.Format("{0}Common/GetMeasurementUnitTypeById", mainUrl), _userService.UserApplicationContext.Token, new MeasurementUnitTypeByIdQuery(request.IdList.Distinct().ToArray())), cancellationToken);
            return result;
        }

    }
}
