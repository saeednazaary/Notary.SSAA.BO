using MediatR;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Contracts.HttpEndPointCaller;
using Microsoft.Extensions.Configuration;

namespace Notary.SSAA.BO.QueryHandler.ExternalServices
{
    public class ShahkarServiceQueryHandler : BaseQueryHandler<ShahkarServiceQuery, ApiResult<ShahkarServiceViewModel>>
    {

        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;
        public ShahkarServiceQueryHandler(IMediator mediator, IUserService userService, IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration)
            : base(mediator, userService)
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
        }
        protected override bool HasAccess(ShahkarServiceQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<ShahkarServiceViewModel>> RunAsync(ShahkarServiceQuery request, CancellationToken cancellationToken)
        {
            string mainUrl = _configuration.GetValue(typeof(string), "InternalGatewayUrl").ToString() + "ExternalServices/v1/";
            var queryParameters = new Dictionary<string, string>();
            queryParameters.Add("NationalNo", request.NationalNo);
            queryParameters.Add("MobileNumber", request.MobileNumber);
            queryParameters.Add("ClientId", request.ClientId);
            var httpRequest = new HttpEndpointRequest(mainUrl + "ShahkarService", _userService.UserApplicationContext.Token, queryParameters);

            var result = await _httpEndPointCaller.CallGetApiAsync<ShahkarServiceViewModel>(httpRequest
              , cancellationToken);

            
            return result;
        }

    }
}
