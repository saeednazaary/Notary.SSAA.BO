






//THIS API IS INCOMPELETE PLS DONT USE IT 












//using MediatR;
//using Microsoft.Extensions.Configuration;
//using Serilog;
//using Notary.SSAA.BO.CommandHandler.Base;
//using Notary.SSAA.BO.DataTransferObject.ServiceInputs.Media;
//using Notary.SSAA.BO.SharedKernel.Constants;
//using Notary.SSAA.BO.SharedKernel.Interfaces;
//using Notary.SSAA.BO.SharedKernel.Result;

//namespace Notary.SSAA.BO.ServiceHandler.Media
//{
//    internal class AddMediaFileServiceHandler : BaseServiceHandler<CreateMediaServiceInput, ApiResult>
//    {
//        private readonly IHttpEndPointCaller _httpEndPointCaller;
//        private readonly IConfiguration _configuration;
//        public AddMediaFileServiceHandler(IMediator mediator, IUserService userService, ILogger logger,
//            IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration) : base(mediator, userService, logger)
//        {
//            _httpEndPointCaller = httpEndPointCaller;
//            _configuration = configuration;
//        }

//        protected async override Task<ApiResult> ExecuteAsync(CreateMediaServiceInput request, CancellationToken cancellationToken)
//        {
//            ApiResult result = new();
//            var mediaFileContent = new ByteArrayContent(generatedReport.Data);
//            formData.Add(mediaFileContent, "Media.MediaFile", "sina");

//            formData.Add(new StringContent("jpg"), "Media.MediaExtension");
//            formData.Add(new StringContent("Desktop Wallpapers_[www"), "Media.FileName");
//            formData.Add(new StringContent("image/jpeg"), "Media.FileType");
//            formData.Add(new StringContent("14861e0a-dc44-423a-803a-bf1ddf495b31"), "DocId");

//            using MultipartFormDataContent formData = new()
//            {
//                { new ByteArrayContent(""), "Media.MediaFile" },
//                { new StringContent(""), "Media.MediaThumbNail" },
//                { new StringContent(""), "Media.MediaId" },
//                { new StringContent(""), "Media.MediaExtension" },
//                { new StringContent(""), "Media.RowNo" },
//                { new StringContent(""), "Media.FileName" },
//                { new StringContent(""), "Media.FileType" },
//                { new StringContent(""), "DocId" },
//                { new StringContent(request.DocumentDescription), "DocDescription" },
//                { new StringContent(""), "DocOtherData" },
//                { new StringContent(""), "DocCreateDate" },
//                { new StringContent(request.DocumentNumber), "DocNo" },
//                { new StringContent(request.DocumentTypeId), "DocType" },
//                { new StringContent(request.DocumentTitle), "DocTitle" },
//                { new StringContent(request.ClientId), "ClientId" },
//                { new StringContent(request.RelatedRecordId), "RelatedRecordIds" }
//            };

//            var attachmentBaseUrl = _configuration.GetValue<string>("InternalGatewayUrl") + "Media/"; ;

//            var apiResult = await _httpEndPointCaller.CallExternalPostApiAsync<MediaUpload>(new SharedKernel.Contracts.HttpEndPointCaller.HttpEndpointRequest(attachmentBaseUrl + "Upload",
//                _userService.UserApplicationContext.Token, formData), cancellationToken);
//            if (!apiResult.IsSuccess)
//            {
//                result.IsSuccess = false;
//                result.message.Add("ایجاد مدرک با مشکل مواجه شده است");
//            }
//            return result;
//        }

//        protected override bool HasAccess(CreateMediaServiceInput request, IList<string> userRoles)
//        {
//            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
//        }
//    }
//}
