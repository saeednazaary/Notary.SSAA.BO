using MediatR;
using Microsoft.Extensions.Configuration;
using Serilog;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices.Estate;
using Notary.SSAA.BO.ServiceHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Contracts.HttpEndPointCaller;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.ServiceHandler.ExternalService.Estate
{
    public class GetEstateGeoJsonServiceHandler : BaseServiceHandler<GetEstateGeoJsonServiceInput, ApiResult<GetEstateGeoJsonViewModel>>
    {
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;
        public GetEstateGeoJsonServiceHandler(IMediator mediator, IUserService userService, ILogger logger,
            IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration) : base(mediator, userService, logger)
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
        }

        protected async override Task<ApiResult<GetEstateGeoJsonViewModel>> ExecuteAsync(GetEstateGeoJsonServiceInput request, CancellationToken cancellationToken)
        {
            string mainUrl = _configuration.GetValue<string>("InternalGatewayUrl") + "ExternalServices/v1/";
            var receiveUrl = mainUrl + "EstateService/GetEstateGeoJson";
            return await _httpEndPointCaller.CallPostApiAsync<GetEstateGeoJsonViewModel, GetEstateGeoJsonServiceInput>(new HttpEndpointRequest<GetEstateGeoJsonServiceInput>
                (receiveUrl, _userService.UserApplicationContext.Token, request), cancellationToken);

        }

        protected override bool HasAccess(GetEstateGeoJsonServiceInput request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis) || userRoles.Contains(RoleConstants.Admin);
        }
    }
}
