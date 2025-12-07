using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.EPayment;
using Notary.SSAA.BO.SharedKernel.AppSettingModel;
using Notary.SSAA.BO.SharedKernel.Result;
using System.ComponentModel;

namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.EPayment
{
    public class InquiryPaymentServiceInput: BaseExternalRequest<ApiResult<InquiryPaymentViewModel>>
    {
        public InquiryPaymentServiceInput()
        {
            Credentials = new();
        }
        public string NationalNo { get; set; }

        [Description("اطلاعات امنیتی پرداخت")]
        public EPaymentCredentialsModel Credentials { get; set; }
    }
}
