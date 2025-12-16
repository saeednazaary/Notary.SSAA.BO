using MediatR;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.DataTransferObject.Queries.DocumentElectronicBook;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentElectronic;
using Notary.SSAA.BO.DataTransferObject.Mappers.DocumentElectronic;
using Microsoft.Extensions.Configuration;
using Notary.SSAA.BO.SharedKernel.Contracts.HttpEndPointCaller;
using Notary.SSAA.BO.SharedKernel.AppSettingModel;

namespace Notary.SSAA.BO.QueryHandler.DocumentElectronic
{
    public class DocumentElectronicBookQueryHandler : BaseQueryHandler<DocumentElectronicBookQuery, ApiResult<DocumentElectronicBookViewModel>>
    {
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;
        public DocumentElectronicBookQueryHandler(IMediator mediator, IUserService userService, IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration)
            : base(mediator, userService)
        {
            this._configuration = configuration;
            this._httpEndPointCaller = httpEndPointCaller;
        }
        protected override bool HasAccess(DocumentElectronicBookQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<DocumentElectronicBookViewModel>> RunAsync(DocumentElectronicBookQuery request, CancellationToken cancellationToken)
        {
            DocumentElectronicBookViewModel result = new();
            ApiResult<DocumentElectronicBookViewModel> apiResult = new();
            var userInfo = _configuration.GetSection("Wrappers_Api_User").Get<WrappersApiUser>();
            request.UserName = userInfo.UserName;
            request.Password = userInfo.Password;
            request.ScriptoriumId = _userService.UserApplicationContext.BranchAccess.BranchCode;

            string mainUrl = _configuration.GetValue<string>("InternalGatewayUrl") + "SpecificWrappers/v1/DocumentRelatedData";
            var httpRequest = new HttpEndpointRequest<DocumentElectronicBookQuery>(mainUrl, _userService.UserApplicationContext.Token, request);
            apiResult = await _httpEndPointCaller.CallPostApiAsync<DocumentElectronicBookViewModel, DocumentElectronicBookQuery>(httpRequest, cancellationToken);


            return apiResult;
        }
    }
}
