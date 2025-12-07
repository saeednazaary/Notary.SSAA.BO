using MediatR;
using Microsoft.Extensions.Configuration;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.Exordium;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExordiumAccount;
using Notary.SSAA.BO.ServiceHandler.Base;
using Notary.SSAA.BO.SharedKernel.AppSettingModel;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Serilog;



namespace Notary.SSAA.BO.ServiceHandler.Exordium
{
    internal class GetAccountInfoByScriptoriumCodeServiceHandler : BaseServiceHandler<GetAccountInfoByScriptoriumCodeInput, ApiResult<GetAccountInfoByScriptoriumCodeViewModel>>
    {
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;
        private ApiResult<GetAccountInfoByScriptoriumCodeViewModel> apiResult;
        public GetAccountInfoByScriptoriumCodeServiceHandler(IMediator mediator, IUserService userService, ILogger logger, IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration) : base(mediator, userService, logger)
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
            apiResult = new();
            apiResult.Data = new();
        }

        protected override async Task<ApiResult<GetAccountInfoByScriptoriumCodeViewModel>> ExecuteAsync(GetAccountInfoByScriptoriumCodeInput request, CancellationToken cancellationToken)
        {
            string bo_Apis_prefix = _configuration.GetSection("BO_APIS_Prefix").Get<BOApisPrefix>().EXORDIUM_BO_APIS_Prefix;
            var url = _configuration.GetValue<string>("InternalGatewayUrl") + "ExordiumWebApis/api/v1/NotaryAccountExordium/AccountInfoByNationalNo";
            Console.WriteLine(url);
            Dictionary<string, string> queryParams = new()
            {{"ScriptoriumCode",request.ScriptoriumCode}
            };
            apiResult = await CallBOApiHelper.GetAsync<GetAccountInfoByScriptoriumCodeViewModel>(url, queryParams, _userService.UserApplicationContext.Token, cancellationToken);
            Console.WriteLine(apiResult);
            return apiResult;
        }

        protected override bool HasAccess(GetAccountInfoByScriptoriumCodeInput request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Admin) || userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
    }
}
