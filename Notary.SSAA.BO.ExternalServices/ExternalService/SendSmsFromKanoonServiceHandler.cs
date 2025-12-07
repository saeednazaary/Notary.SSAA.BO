using MediatR;
using Microsoft.Extensions.Configuration;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.ExternalServices;
using Notary.SSAA.BO.ServiceHandler.Base;
using Notary.SSAA.BO.SharedKernel.AppSettingModel;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Contracts.HttpEndPointCaller;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Serilog;


namespace Notary.SSAA.BO.ServiceHandler.ExternalService
{
    internal class SendSMSFromKanoonServiceHandler : BaseServiceHandler<SendSmsFromKanoonServiceInput, ApiResult<SMSFromKanoonServiceViewModel>>
    {
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;
        public SendSMSFromKanoonServiceHandler(IMediator mediator, IUserService userService, ILogger logger,
            IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration) : base(mediator, userService, logger)
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
        }

        protected async override Task<ApiResult<SMSFromKanoonServiceViewModel>> ExecuteAsync(SendSmsFromKanoonServiceInput request, CancellationToken cancellationToken)
        {
            string bo_Apis_prefix = _configuration.GetSection("BO_APIS_Prefix").Get<BOApisPrefix>().EXTERNAL_BO_APIS_Prefix;
            return await _httpEndPointCaller.CallPostApiAsync<SMSFromKanoonServiceViewModel, SendSmsFromKanoonServiceInput>(new HttpEndpointRequest<SendSmsFromKanoonServiceInput>
                (_configuration.GetValue<string>("InternalGatewayUrl") + bo_Apis_prefix + "ExternalServices/v1/" + "SMSService", _userService.UserApplicationContext.Token, request), cancellationToken);

        }

        protected override bool HasAccess(SendSmsFromKanoonServiceInput request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
    }
}
