
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Notary.SSAA.BO.ServiceHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Serilog;
using SSAA.Notary.DataTransferObject.ServiceInputs.SSOTokenValidation;
using SSAA.Notary.DataTransferObject.ViewModels.Services.ValidateToken;
using System.Net;
using System.Net.Http.Json;

namespace SSAA.Notary.ServiceHandler.SSOTokenValidation
{
    internal class SSOTokenValidationServiceHandler : BaseServiceHandler<SSOTokenValidationServiceInput, ApiResult<string>>
    {
        private readonly IConfiguration _configuration;
        private ApiResult<string> _apiResult;
        public SSOTokenValidationServiceHandler(IMediator mediator, IUserService userService, ILogger logger, IConfiguration configuration) : base(mediator, userService, logger)
        {
            _configuration = configuration;
            _apiResult = new();
        }

        protected override async Task<ApiResult<string>> ExecuteAsync(SSOTokenValidationServiceInput request, CancellationToken cancellationToken)
        {
            var httpClient = new HttpClient();
            var validateUserAddress = _configuration.GetValue<string>("KatebIdentityServerAddress") + "/api/Authorization/ValidateUser";
            var tokenValidateResponse = await httpClient.PostAsJsonAsync(validateUserAddress, request, cancellationToken);

            if (tokenValidateResponse.StatusCode == HttpStatusCode.OK)
            {
                var resultString = await tokenValidateResponse.Content.ReadAsStringAsync(cancellationToken);
                ValidateTokenResponse validateTokenResponse = JsonConvert.DeserializeObject<ValidateTokenResponse>(resultString);

                if (validateTokenResponse.Succeeded == false)
                {
                    _apiResult.statusCode = (ApiResultStatusCode)validateTokenResponse.HttpStatusCode;
                    _apiResult.IsSuccess = false;
                    return _apiResult;
                }
                else
                {
                    _apiResult.Data = validateTokenResponse.TokenData.username;
                    _apiResult.IsSuccess = true;
                    return _apiResult;
                }
            }
            _apiResult.IsSuccess = false;
            _apiResult.statusCode = (ApiResultStatusCode)tokenValidateResponse.StatusCode;
            return _apiResult;
        }

        protected override bool HasAccess(SSOTokenValidationServiceInput request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Admin) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
    }
}
