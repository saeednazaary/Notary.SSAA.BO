

using MediatR;
using Microsoft.Extensions.Configuration;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.ExternalServices.EDM;
using Notary.SSAA.BO.ServiceHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Contracts.HttpEndPointCaller;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Serilog;
using SNotary.SSAA.BO.DataTransferObject.ServiceInputs.Edm;

namespace Notary.SSAA.BO.ServiceHandler.EDM
{
    internal class ImportEdmServiceHandler : BaseServiceHandler<ImportEdmServiceInput, ApiResult<ImportEdmServiceResponse>>
    {
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;
        public ImportEdmServiceHandler(IMediator mediator, IUserService userService, ILogger logger, IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration) : base(mediator, userService, logger)
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
        }

        protected override async Task<ApiResult<ImportEdmServiceResponse>> ExecuteAsync(ImportEdmServiceInput request, CancellationToken cancellationToken)
        {
            ApiResult<ImportEdmServiceResponse> _apiResult = await _httpEndPointCaller.CallPostApiAsync<ImportEdmServiceResponse, ImportEdmServiceInput>(new HttpEndpointRequest<ImportEdmServiceInput>
               (_configuration.GetValue<string>("InternalGatewayUrl") + "ExternalServices/v1/Import", _userService.UserApplicationContext.Token, request), cancellationToken);
            return _apiResult;
        }

        protected override bool HasAccess(ImportEdmServiceInput request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar);
        }
    }
}
