using MediatR;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.DataTransferObject.Queries.Wrappers;
using Notary.SSAA.BO.SharedKernel.AppSettingModel;
using Notary.SSAA.BO.SharedKernel.Contracts.HttpEndPointCaller;
using Microsoft.Extensions.Configuration;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Wrappers;


namespace Notary.SSAA.BO.QueryHandler.Wrappers
{
    public class GetDocumentElectronicBookBaseInfoLastClassifyNoQueryHandler : BaseQueryHandler<ElectronicBookBaseInfoLastClassifyWrapperRequest, ApiResult<string>>
    {
        private protected ApiResult<string> apiResult;
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;
        public GetDocumentElectronicBookBaseInfoLastClassifyNoQueryHandler(IMediator mediator, IUserService userService,
            IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration)
            : base(mediator, userService)
        {
            _configuration = configuration;
            _httpEndPointCaller = httpEndPointCaller;
            apiResult = new ApiResult<string>();

        }
        protected override bool HasAccess(ElectronicBookBaseInfoLastClassifyWrapperRequest request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
        protected async override Task<ApiResult<string>> RunAsync(ElectronicBookBaseInfoLastClassifyWrapperRequest request, CancellationToken cancellationToken)
        {
            var userInfo = _configuration.GetSection("Wrappers_Api_User").Get<WrappersApiUser>();
            request.UserName = userInfo.UserName;
            request.Password = userInfo.Password;
            request.ScriptoriumId = _userService.UserApplicationContext.BranchAccess.BranchCode;

            string mainUrl = _configuration.GetValue<string>("InternalGatewayUrl") + "GeneralWrappers/v1/ElectronicBookBaseInfoLastClassifyNo";
            var httpRequest = new HttpEndpointRequest<ElectronicBookBaseInfoLastClassifyWrapperRequest>(mainUrl, _userService.UserApplicationContext.Token, request);
            var result = await _httpEndPointCaller.CallExternalPostApiAsync<ExternalApiResult<string>, ElectronicBookBaseInfoLastClassifyWrapperRequest>(httpRequest, cancellationToken);
            if (result.ResCode != "1")
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add(result.ResMessage);
            }
            else apiResult.Data = result.Data;
            return apiResult;
        }

    }
}
