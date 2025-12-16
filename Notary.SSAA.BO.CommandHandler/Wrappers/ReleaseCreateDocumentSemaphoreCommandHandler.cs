using MediatR;
using Microsoft.Extensions.Configuration;
using Serilog;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAR.Wrappers.WebApi.Models.RequestsModel;
using Notary.SSAA.BO.SharedKernel.AppSettingModel;
using Notary.SSAA.BO.SharedKernel.Contracts.HttpEndPointCaller;

namespace Notary.SSAA.BO.CommandHandler.Estate.EstateTaxInquiry
{
    public class ReleaseCreateDocumentSemaphoreCommandHandler : BaseCommandHandler<ReleaseCreateDocumentSemaphoreWrapperCommand, ApiResult>
    {        
        private protected ApiResult apiResult;               
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;

        public ReleaseCreateDocumentSemaphoreCommandHandler(IMediator mediator, IUserService userService, ILogger logger, IEstateTaxInquiryRepository estateTaxInquiryRepository, IDateTimeService dateTimeService, IWorkfolwStateRepository workfolwStateRepository, IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration, IRepository<ConfigurationParameter> configurationParameterRepository) : base(mediator, userService, logger)
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
            apiResult = new();
        }
        protected override bool HasAccess(ReleaseCreateDocumentSemaphoreWrapperCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
        protected override async Task<ApiResult> ExecuteAsync(ReleaseCreateDocumentSemaphoreWrapperCommand request, CancellationToken cancellationToken)
        {
            var userInfo = _configuration.GetSection("Wrappers_Api_User").Get<WrappersApiUser>();
            request.UserName = userInfo.UserName;
            request.Password = userInfo.Password;
            request.ScriptoriumId = _userService.UserApplicationContext.BranchAccess.BranchCode;
            
            string mainUrl = _configuration.GetValue<string>("InternalGatewayUrl") + "GeneralWrappers/v1/ReleaseCreateDocumentSemaphore";
            var httpRequest = new HttpEndpointRequest<ReleaseCreateDocumentSemaphoreWrapperCommand>(mainUrl, _userService.UserApplicationContext.Token, request);
            var result = await _httpEndPointCaller.CallExternalPostApiAsync<ExternalApiResult, ReleaseCreateDocumentSemaphoreWrapperCommand>(httpRequest, cancellationToken);
            if (result.ResCode != "1")
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add(result.ResMessage);
            }
            return apiResult;
        }
        
    }
}
