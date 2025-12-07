using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Commands.Fingerprint
{
    public class CreateFakePersonFingerprintCommand : BaseCommandRequest<ApiResult>
    {
        public string DocumentRequestNo { get; set; }
        public string PersonNationalNo { get; set; }

    }

}
