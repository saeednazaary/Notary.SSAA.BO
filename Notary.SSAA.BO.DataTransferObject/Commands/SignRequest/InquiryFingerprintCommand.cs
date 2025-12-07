using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Commands.SignRequest
{
    public class InquiryFingerprintCommand : BaseCommandRequest<ApiResult>
    {
        public string NationalNo { get; set; }
    }
}
