using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.Person;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.Person;
using Notary.SSAA.BO.ServiceHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Serilog;


namespace Notary.SSAA.BO.ServiceHandler.Person
{
    internal class GetPersonPhotoListQueryHandler : BaseServiceHandler<GetPersonPhotoListServiceInput, ApiResult<GetPersonPhotoListViewModel>>
    {
        private readonly IHttpExternalServiceCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;
        public GetPersonPhotoListQueryHandler(IMediator mediator, IUserService userService, ILogger logger, IHttpExternalServiceCaller httpEndPointCaller,
            IConfiguration configuration) : base(mediator, userService, logger)
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
        }
        protected async override Task<ApiResult<GetPersonPhotoListViewModel>> ExecuteAsync(GetPersonPhotoListServiceInput request, CancellationToken cancellationToken)
        {
            var apiResult=new ApiResult<GetPersonPhotoListViewModel>();
           var apiRes= await CallBOApiHelper.PostAsync<GetPersonPhotoListServiceInput, GetPersonPhotoListViewModel>(request, _configuration.GetValue<string>("InternalGatewayUrl") + "ExternalServices/v1/PhotoList",
                _userService.UserApplicationContext.Token, cancellationToken);

            if (apiRes.IsSuccess && apiRes.Data is not null)
            {
                apiResult.Data = new();
                apiResult.Data = apiRes.Data;
                apiRes.message = apiRes.message;
            }
            else if (!apiRes.IsSuccess && apiRes.statusCode == ApiResultStatusCode.Success)
            {
                apiResult.IsSuccess = false;
                apiResult.message = apiRes.message;

            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("خطایی در فرایند دریافت تصاویر اشخاص به وجود آمده است لطفا بعد از لحظاتی دوباره تلاش کنید.");
            }
            return apiResult;
        }
        protected override bool HasAccess(GetPersonPhotoListServiceInput request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.Admin) || userRoles.Contains(RoleConstants.SanadNevis);
        }
    }
}
