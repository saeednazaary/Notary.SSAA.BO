using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Notary.ExternalServices.WebApi.Models.RequestsModel.Person;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.BaseInfo;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.Person;
using Notary.SSAA.BO.ServiceHandler.Base;
using Notary.SSAA.BO.SharedKernel.AppSettingModel;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Serilog;


namespace Notary.SSAA.BO.ServiceHandler.BaseInfo
{
    internal class GetPersonPhotoListFromSabteAhvalServiceHandler : BaseServiceHandler<GetPersonPhotoListFromSabteAhvalServiceInput, ApiResult<GetPersonPhotoListViewModel>>
    {
        private readonly IHttpExternalServiceCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;
        public GetPersonPhotoListFromSabteAhvalServiceHandler(IMediator mediator, IUserService userService, ILogger logger, IHttpExternalServiceCaller httpEndPointCaller,
            IConfiguration configuration) : base(mediator, userService, logger)
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
        }
        protected async override Task<ApiResult<GetPersonPhotoListViewModel>> ExecuteAsync(GetPersonPhotoListFromSabteAhvalServiceInput request, CancellationToken cancellationToken)
        {
            var apiResult = new ApiResult<GetPersonPhotoListViewModel>();
            string bo_Apis_prefix = _configuration.GetSection("BO_APIS_Prefix").Get<BOApisPrefix>().EXTERNAL_BO_APIS_Prefix;
            var apiRes= await CallBOApiHelper.PostAsync<GetPersonPhotoListFromSabteAhvalServiceInput, GetPersonPhotoListViewModel>(request, _configuration.GetValue<string>("InternalGatewayUrl") + bo_Apis_prefix + "ExternalServices/v1/PhotoListFromSabteAhval",
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
                apiResult.message.Add("خطایی در فرایند دریافت تصاویر اشخاص  به وجود آمده است لطفا بعد از لحظاتی دوباره تلاش کنید.");
            }
            return apiResult;

        }
        protected override bool HasAccess(GetPersonPhotoListFromSabteAhvalServiceInput request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis) || userRoles.Contains(RoleConstants.Admin) || userRoles.Contains(RoleConstants.SSAAAdmin);
        }
    }
}
