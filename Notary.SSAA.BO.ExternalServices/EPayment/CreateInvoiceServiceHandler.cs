using MediatR;
using Microsoft.Extensions.Configuration;
using Serilog;
using Notary.SSAA.BO.DataTransferObject.Mappers.EPayment;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.EPayment;
using Notary.SSAA.BO.DataTransferObject.Validators.EPayment;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.EPayment;
using Notary.SSAA.BO.ServiceHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Newtonsoft.Json;
using Notary.SSAA.BO.SharedKernel.AppSettingModel;



namespace Notary.SSAA.BO.ServiceHandler.EPayment
{
    internal class CreateInvoiceServiceHandler : BaseServiceHandler<CreateInvoiceServiceInput, ApiResult<CreateInvoiceViewModel>>
    {
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;

        public CreateInvoiceServiceHandler ( IMediator mediator, IUserService userService, ILogger logger,
            IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration ) : base ( mediator, userService, logger )
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
        }

        protected async override Task<ApiResult<CreateInvoiceViewModel>> ExecuteAsync ( CreateInvoiceServiceInput request, CancellationToken cancellationToken )
        {
            var validator = new CreateInvoiceServiceValidator();
            ApiResult<CreateInvoiceViewModel> apiResult = new();
            var validateResult = await validator.ValidateAsync(request,cancellationToken);

            if ( validateResult.IsValid )
            {
                var createInvoiceEndpointInput = EPaymentMapper.ToInput(request);
                string bo_Apis_prefix = _configuration.GetSection("BO_APIS_Prefix").Get<BOApisPrefix>().EXTERNAL_BO_APIS_Prefix;

                var paymentApiResult = await CallBOApiHelper.PostAsync<CreateInvoiceEndpointInput, CreateInvoiceEndpointViewModel>(createInvoiceEndpointInput, _configuration.GetValue<string>("InternalGatewayUrl")  + "ExternalServices/v1/CreateInvoice",
                    _userService.UserApplicationContext.Token, cancellationToken);

                if ( paymentApiResult.IsSuccess && paymentApiResult.Data is not null && paymentApiResult.Data?.RefNumber is not null)
                {
                    apiResult.Data = new ();
                    apiResult.Data.PaymentNo = paymentApiResult.Data.RefNumber;
                    apiResult.Data.RedirectLink = _configuration.GetValue<string> ( "EPaymentFrontendUrl" ) + "?requestNo=" + paymentApiResult.Data.RefNumber;
                    apiResult.message.AddRange(paymentApiResult.message);
                }
                else if(!paymentApiResult.IsSuccess && paymentApiResult.statusCode== ApiResultStatusCode.Success)
                {
                    apiResult.IsSuccess = false;
                    apiResult.message = paymentApiResult.message;

                }
                else
                {
                    apiResult.IsSuccess = false;
                    apiResult.message.Add("خطایی در فرایند پرداخت به وجود آمده است لطفا بعد از لحظاتی دوباره تلاش کنید.");
                }
            }
            else
            {
                apiResult.IsSuccess=false;
                apiResult.message.Add("ورودی های سرویس نا معتبر میباشد.");
            }

            return apiResult;
        }

        protected override bool HasAccess ( CreateInvoiceServiceInput request, IList<string> userRoles )
        {
            return userRoles.Contains(RoleConstants.Admin) || userRoles.Contains ( RoleConstants.Sardaftar ) || userRoles.Contains ( RoleConstants.Daftaryar ) || userRoles.Contains ( RoleConstants.SanadNevis );
        }
    }
}
