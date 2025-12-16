using MediatR;
using Microsoft.Extensions.Configuration;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.BaseInfo;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ClientLogin;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.BaseInfo;
using Notary.SSAA.BO.ServiceHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Contracts.HttpEndPointCaller;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Serilog;


namespace Notary.SSAA.BO.ServiceHandler.BaseInfo
{
    public class SignRequestBasicInfoServiceHandler : BaseServiceHandler<SignRequestBasicInfoServiceInput, ApiResult<SignRequestBasicInfoViewModel>>
    {
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;

        public SignRequestBasicInfoServiceHandler(IMediator mediator, IUserService userService, ILogger logger,
            IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration) : base(mediator, userService, logger)
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
        }

        protected async override Task<ApiResult<SignRequestBasicInfoViewModel>> ExecuteAsync(SignRequestBasicInfoServiceInput request, CancellationToken cancellationToken)
        {
            var baseInfoUrl = _configuration.GetValue<string>("InternalGatewayUrl") + "api/v1/Specific/Ssar/";
            ApiResult<SignRequestBasicInfoViewModel> scriptoriumResult = new();

            scriptoriumResult = await _httpEndPointCaller.CallPostApiAsync<SignRequestBasicInfoViewModel, SignRequestBasicInfoServiceInput>(
           new HttpEndpointRequest<SignRequestBasicInfoServiceInput>(baseInfoUrl + ScriptoriumRequestConstant.SignRequestBasicInfo,
           _userService.UserApplicationContext.Token, request), cancellationToken);

            return scriptoriumResult;
        }

        protected override bool HasAccess(SignRequestBasicInfoServiceInput request, IList<string> userRoles)
        {
            return true;
        }
    }
}
