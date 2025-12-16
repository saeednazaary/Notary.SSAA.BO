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
    internal class SanaFromSabtServiceHandler : BaseServiceHandler<SanaFromSabtServiceInput, ApiResult<SanaFromSabtServiceViewModel>>
    {
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;
        public SanaFromSabtServiceHandler(IMediator mediator, IUserService userService, ILogger logger,
            IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration) : base(mediator, userService, logger)
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
        }

        protected async override Task<ApiResult<SanaFromSabtServiceViewModel>> ExecuteAsync(SanaFromSabtServiceInput request, CancellationToken cancellationToken)
        {
            var queryParameters = new Dictionary<string, string>();
            queryParameters.Add("NationalNo", request.NationalNo);
            queryParameters.Add("ClientId", request.ClientId);
            queryParameters.Add("PersonType", "1");
            return await _httpEndPointCaller.CallGetApiAsync<SanaFromSabtServiceViewModel>(new HttpEndpointRequest
                (_configuration.GetValue<string>("InternalGatewayUrl") + "ExternalServices/v1/" + "SanaForSabtService", _userService.UserApplicationContext.Token, queryParameters), cancellationToken);

        }

        protected override bool HasAccess(SanaFromSabtServiceInput request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
    }
}
