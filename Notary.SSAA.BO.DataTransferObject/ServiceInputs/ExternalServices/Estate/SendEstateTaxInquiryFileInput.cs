using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices.Estate;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate
{
    public class SendEstateTaxInquiryFileInput : BaseExternalRequest<ApiResult<EstateTaxInquiyServiceResultObject>>
    {
        public string ServiceID { get; set; }
        public string FollowCode { get; set; }
        public string FileName { get; set; }
        public string FileComment { get; set; }
        public byte[] FileByte { get; set; }
        public string ClientId { get; set; }
        public SendEstateTaxInquiryFileInput()
        {
            ClientId = "SSAR";
        }
    }
}
