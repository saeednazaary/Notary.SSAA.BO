using MediatR;
using Microsoft.Extensions.Configuration;
using Serilog;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.ExternalServices;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Contracts.HttpEndPointCaller;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.ServiceHandler.Base;
using Notary.SSAA.BO.SharedKernel.AppSettingModel;


namespace Notary.SSAA.BO.ServiceHandler.ExternalService
{
    internal class FindLastFingerprintServiceHandler : BaseServiceHandler<FindLastFingerprintServiceInput, ApiResult<FindLastFingerprintServiceViewModel>>
    {
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;
        public FindLastFingerprintServiceHandler(IMediator mediator, IUserService userService, ILogger logger,
            IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration) : base(mediator, userService, logger)
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
        }

        protected async override Task<ApiResult<FindLastFingerprintServiceViewModel>> ExecuteAsync(FindLastFingerprintServiceInput request, CancellationToken cancellationToken)
        {
            var userInfo = _configuration.GetSection("Wrappers_Api_User").Get<WrappersApiUser>();
            request.UserName = userInfo.UserName;
            request.Password = userInfo.Password;
            try
            {
                var result = await _httpEndPointCaller.CallPostApiAsync<FindLastFingerprintServiceViewModel, FindLastFingerprintServiceInput>(new HttpEndpointRequest<FindLastFingerprintServiceInput>
                    (_configuration.GetValue<string>("InternalGatewayUrl") + "GeneralWrappers/v1/PersonLastFingerPrint", _userService.UserApplicationContext.Token, request), cancellationToken);
                return result;
            }
            catch (TaskCanceledException)
            {
                var result = new ApiResult<FindLastFingerprintServiceViewModel>();
                result.IsSuccess = false;
                result.message.Add("دریافت آخرین اثر انگشت ثبت شده ی متقاضی از سیستم با خطا مواجه شد :به دلیل ترافیک بالای شبکه و یا ازدسترس خارج شدن موقتی سرویس دهنده ، پاسخی در زمان مقرر دریافت نشد .لطفا لحظاتی دیگر عملیات اخذ اثر انگشت را تکرار کنید");
                return result;
            }

        }

        protected override bool HasAccess(FindLastFingerprintServiceInput request, IList<string> userRoles)
        {
            return true;
        }
    }
}
