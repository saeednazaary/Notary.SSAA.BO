using MediatR;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.SharedKernel.Contracts.HttpEndPointCaller;
using Microsoft.Extensions.Configuration;

namespace Notary.SSAA.BO.QueryHandler.ExternalServices
{
    public class PostServiceQueryHandler : BaseQueryHandler<PostServiceQuery, ApiResult<PostServiceViewModel>>
    {
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;

        public PostServiceQueryHandler(IMediator mediator, IUserService userService,
            IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration)
              : base(mediator, userService)

        {

            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
        }
        protected override bool HasAccess(PostServiceQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<PostServiceViewModel>> RunAsync(PostServiceQuery request, CancellationToken cancellationToken)
        {
            string mainUrl = _configuration.GetValue(typeof(string), "InternalGatewayUrl").ToString() + "ExternalServices/v1/";
            var queryParameters = new Dictionary<string, string>();
            queryParameters.Add("PostalCode", request.PostalCode);
            queryParameters.Add("ClientId", request.ClientId);
            var httpRequest = new HttpEndpointRequest(mainUrl + "PostService", _userService.UserApplicationContext.Token, queryParameters);

            var result = await _httpEndPointCaller.CallGetApiAsync<PostServiceViewModel>(httpRequest
              , cancellationToken);


            return result;
        }

    }
}
