using MediatR;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Microsoft.Extensions.Configuration;
using Notary.SSAA.BO.SharedKernel.Contracts.HttpEndPointCaller;
using Notary.SSAA.BO.SharedKernel.AppSettingModel;

namespace Notary.SSAA.BO.QueryHandler.ExternalServices
{
    public class SabtAhvalPhotoListServiceQueryHandler : BaseQueryHandler<SabtAhvalPhotoListServiceQuery, ApiResult<SabtAhvalPhotoListServiceViewModel>>
    {
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;

        public SabtAhvalPhotoListServiceQueryHandler(IMediator mediator, IUserService userService,
            IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration)
            : base(mediator, userService)
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
        }
        protected override bool HasAccess(SabtAhvalPhotoListServiceQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis)
                || userRoles.Contains(RoleConstants.Customer);
        }

        protected async override Task<ApiResult<SabtAhvalPhotoListServiceViewModel>> RunAsync(SabtAhvalPhotoListServiceQuery request, CancellationToken cancellationToken)
        {
            string bo_Apis_prefix = _configuration.GetSection("BO_APIS_Prefix").Get<BOApisPrefix>().BASEINFO_APIS_Prefix;

            string mainUrl = _configuration.GetValue<string>("InternalGatewayUrl") + bo_Apis_prefix + "api/v1/Specific/Ssar/";
            
            var httpRequest = new HttpEndpointRequest<SabtAhvalPhotoListServiceQuery>(mainUrl + "Person/PhotoList", _userService.UserApplicationContext.Token,request);
            var result = await _httpEndPointCaller.CallPostApiAsync<SabtAhvalPhotoListServiceViewModel, SabtAhvalPhotoListServiceQuery>(httpRequest
              , cancellationToken);
            return result;
        }

    }
}


