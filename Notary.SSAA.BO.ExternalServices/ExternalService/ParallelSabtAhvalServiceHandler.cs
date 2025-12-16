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
    internal class ParallelSabtAhvalServiceHandler : BaseServiceHandler<ParallelSabtAhvalServiceInput, ApiResult<List<SabtAhvalServiceViewModel>>>
    {
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;
        public ParallelSabtAhvalServiceHandler(IMediator mediator, IUserService userService, ILogger logger,
            IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration) : base(mediator, userService, logger)
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
        }

        protected async override Task<ApiResult<List<SabtAhvalServiceViewModel>>> ExecuteAsync(ParallelSabtAhvalServiceInput request, CancellationToken cancellationToken)
        {
            return await _httpEndPointCaller.CallPostApiAsync<List<SabtAhvalServiceViewModel>, ParallelSabtAhvalServiceInput>(new HttpEndpointRequest<ParallelSabtAhvalServiceInput>
                (_configuration.GetValue<string>("InternalGatewayUrl") + "ExternalServices/v1/" + "ParallelSabtAhvalService", _userService.UserApplicationContext.Token, request), cancellationToken);

        }

        protected override bool HasAccess(ParallelSabtAhvalServiceInput request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
    }
}
