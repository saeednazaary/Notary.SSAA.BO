using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Commands.DocumentStandardContract
{
    public class UpdateDocumentStandardContractFingerprintStateCommand : BaseCommandRequest<ApiResult<DocumentStandardContractViewModel>>
    {
        public string DocumentId { get; set; }
    }
}
