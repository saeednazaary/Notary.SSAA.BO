using MediatR;
using Microsoft.Extensions.Configuration;
using Serilog;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ReportTools;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.ReportTools;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Contracts.HttpEndPointCaller;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.ServiceHandler.Base;

namespace Notary.SSAA.BO.ServiceHandler.ReportTools
{
    internal class SignRequestGenerateReportServiceHandler : BaseServiceHandler<SignRequestGenerateReportServiceInput, ApiResult<ReportResult>>
    {
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;
        public SignRequestGenerateReportServiceHandler(IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration, IMediator mediator, 
            IUserService userService, ILogger logger) : base(mediator, userService, logger)
        {
            _configuration = configuration;
            _httpEndPointCaller = httpEndPointCaller;
        }

        protected async override Task<ApiResult<ReportResult>> ExecuteAsync(SignRequestGenerateReportServiceInput request, CancellationToken cancellationToken)
        {
            var apiResult = await _httpEndPointCaller.CallPostApiAsync<ReportResult, SignRequestGenerateReportServiceInput>(new HttpEndpointRequest<SignRequestGenerateReportServiceInput>
                (_configuration.GetValue<string>("InternalGatewayUrl") + "ReportTools/v1/SignRequest/Report", _userService.UserApplicationContext.Token, request), cancellationToken);

            return apiResult;

        }

        protected override bool HasAccess(SignRequestGenerateReportServiceInput request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
    }
}
