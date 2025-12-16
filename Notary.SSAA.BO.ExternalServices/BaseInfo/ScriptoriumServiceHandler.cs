using MediatR;
using Microsoft.Extensions.Configuration;
using Serilog;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.BaseInfo;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.BaseInfo;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Contracts.HttpEndPointCaller;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.ServiceHandler.Base;


namespace Notary.SSAA.BO.ServiceHandler.BaseInfo
{
    public class ScriptoriumServiceHandler : BaseServiceHandler<ScriptoriumInput, ApiResult<ScriptoriumViewModel>>
    {
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;

        public ScriptoriumServiceHandler(IMediator mediator, IUserService userService, ILogger logger,
            IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration) : base(mediator, userService, logger)
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
        }

        protected async override Task<ApiResult<ScriptoriumViewModel>> ExecuteAsync(ScriptoriumInput request, CancellationToken cancellationToken)
        {
            GetScriptoriumByIdServiceInput scriptoriumQuery = new(new string[] { request.IdList[0] });
            var baseInfoUrl = _configuration.GetValue<string>("InternalGatewayUrl") + "api/v1/Specific/Ssar/";
            ApiResult<ScriptoriumViewModel> scriptoriumResult = new();

            scriptoriumResult = await _httpEndPointCaller.CallPostApiAsync<ScriptoriumViewModel, GetScriptoriumByIdServiceInput>(
           new HttpEndpointRequest<GetScriptoriumByIdServiceInput>(baseInfoUrl + ScriptoriumRequestConstant.Scriptorium,
           _userService.UserApplicationContext.Token, scriptoriumQuery), cancellationToken);

            return scriptoriumResult;
        }

        protected override bool HasAccess(ScriptoriumInput request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis) || userRoles.Contains(RoleConstants.SSAAAdmin);
        }
    }
}
