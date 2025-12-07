using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices.Estate;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate
{
    public class ValidateEstateSeparationInquiriesInput : BaseExternalRequest<ApiResult<ValidateEstateSeparationInquiriesViewModel>>
    {
        public ValidateEstateSeparationInquiriesInput()
        {
            ClientId = "SSAR";
        }
        public string ClientId { get; set; }
        public string[] EstateInquiryId { get; set; }
    }
}
