using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Commands.Fingerprint
{
    public class SetPersonMocStateCommand : BaseCommandRequest<ApiResult>
    {
        public string FingerprintId { get; set; }
        public string MocState { get; set; }
        public string MocStateDescription { get; set; }
        public string NationalityCode { get; set; }
        public string UseCaseId { get; set; }
    }
}
