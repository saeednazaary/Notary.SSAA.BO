using MediatR;
using Microsoft.Extensions.Configuration;
using Serilog;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.Media;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Contracts.HttpEndPointCaller;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.ServiceHandler.Base;
using Notary.SSAA.BO.SharedKernel.AppSettingModel;

namespace Notary.SSAA.BO.ServiceHandler.Media
{

    internal class RemoveAttachmentServiceHandler : BaseServiceHandler<MediaRemoveServiceInput, ApiResult>
    {
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;
        public RemoveAttachmentServiceHandler(IMediator mediator, IUserService userService, ILogger logger,
            IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration) : base(mediator, userService, logger)
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
        }

        protected async override Task<ApiResult> ExecuteAsync(MediaRemoveServiceInput request, CancellationToken cancellationToken)
        {
            string bo_Apis_prefix = _configuration.GetSection("BO_APIS_Prefix").Get<BOApisPrefix>().MEDIAMANAGER_APIS_Prefix;
            var apiResult = await _httpEndPointCaller.CallExternalPostApiAsync<ApiResult, MediaRemoveServiceInput>(new HttpEndpointRequest<MediaRemoveServiceInput>
                (_configuration.GetValue<string>("InternalGatewayUrl")  + "Media/Remove", _userService.UserApplicationContext.Token, request), cancellationToken);
            return apiResult;
        }

        protected override bool HasAccess(MediaRemoveServiceInput request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Admin) || userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
    }

}
