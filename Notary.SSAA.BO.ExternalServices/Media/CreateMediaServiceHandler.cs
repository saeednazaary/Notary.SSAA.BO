using MediatR;
using Microsoft.Extensions.Configuration;
using Serilog;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.Media;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.ServiceHandler.Base;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using Notary.SSAA.BO.SharedKernel.AppSettingModel;
using System.Net.Http.Headers;

namespace Notary.SSAA.BO.ServiceHandler.Media
{
    internal class CreateMediaServiceHandler : BaseServiceHandler<CreateMediaServiceInput, ApiResult>
    {
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;
        public CreateMediaServiceHandler(IMediator mediator, IUserService userService, ILogger logger,
            IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration) : base(mediator, userService, logger)
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
        }

        protected async override Task<ApiResult> ExecuteAsync(CreateMediaServiceInput request, CancellationToken cancellationToken)
        {
            ApiResult result = new();
            using MultipartFormDataContent formData = new()
            {
                { new StringContent(""), "Media.MediaFile" },
                { new StringContent(""), "Media.MediaThumbNail" },
                { new StringContent(""), "Media.MediaId" },
                { new StringContent(""), "Media.MediaExtension" },
                { new StringContent(""), "Media.RowNo" },
                { new StringContent(""), "Media.FileName" },
                { new StringContent(""), "Media.FileType" },
                { new StringContent(""), "DocId" },
                { new StringContent(request.DocumentDescription), "DocDescription" },
                { new StringContent(""), "DocOtherData" },
                { new StringContent(""), "DocCreateDate" },
                { new StringContent(request.DocumentNumber), "DocNo" },
                { new StringContent(request.DocumentTypeId), "DocType" },
                { new StringContent(request.DocumentTitle), "DocTitle" },
                { new StringContent(request.ClientId), "ClientId" },
                { new StringContent(request.RelatedRecordId), "RelatedRecordIds" }
            };
            string bo_Apis_prefix = _configuration.GetSection("BO_APIS_Prefix").Get<BOApisPrefix>().MEDIAMANAGER_APIS_Prefix;

            var attachmentBaseUrl = _configuration.GetValue<string>("InternalGatewayUrl")  + "Media/"; ;

            var apiResult = await _httpEndPointCaller.CallExternalPostApiAsync<MediaUpload>(new SharedKernel.Contracts.HttpEndPointCaller.HttpEndpointRequest(attachmentBaseUrl + "Upload",
                _userService.UserApplicationContext.Token, formData), cancellationToken);
            if (!apiResult.IsSuccess)
            {
                result.IsSuccess = false;
                result.message.Add("ایجاد مدرک با مشکل مواجه شده است");
            }
            return result;
        }

        protected override bool HasAccess(CreateMediaServiceInput request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Admin) || userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
    }

}
