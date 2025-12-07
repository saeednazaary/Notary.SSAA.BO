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
    internal class InquiryPaymentServiceHandler : BaseServiceHandler<InquiryPaymentServiceInput, ApiResult<InquiryPaymentViewModel>>
    {
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;

        public InquiryPaymentServiceHandler ( IMediator mediator, IUserService userService, ILogger logger,
            IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration ) : base ( mediator, userService, logger )
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
        }

        protected async override Task<ApiResult<InquiryPaymentViewModel>> ExecuteAsync ( InquiryPaymentServiceInput request, CancellationToken cancellationToken )
        {
            var validator = new InquiryPaymentServiceValidator ();
            ApiResult<InquiryPaymentViewModel> apiResult = new();
            var validateResult = await validator.ValidateAsync(request,cancellationToken);


            if ( validateResult.IsValid )
            {
                var createInvoiceEndpointInput = EPaymentMapper.ToInput(request);
                string bo_Apis_prefix = _configuration.GetSection("BO_APIS_Prefix").Get<BOApisPrefix>().EXTERNAL_BO_APIS_Prefix;

                apiResult = await CallBOApiHelper.PostAsync<InquiryPaymentEndpointInput, InquiryPaymentViewModel>(createInvoiceEndpointInput, _configuration.GetValue<string>("InternalGatewayUrl") + "ExternalServices/v1/InquiryPayment",
                    _userService.UserApplicationContext.Token, cancellationToken);
                apiResult.message.Add(JsonConvert.SerializeObject(createInvoiceEndpointInput));
                apiResult.message.AddRange(apiResult.message);

            }

            return apiResult;
        }

        protected override bool HasAccess ( InquiryPaymentServiceInput request, IList<string> userRoles )
        {
            return userRoles.Contains(RoleConstants.Admin) || userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains ( RoleConstants.Daftaryar ) || userRoles.Contains ( RoleConstants.SanadNevis );
        }
    }
}
