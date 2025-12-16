using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Commands.Document
{
    public class UpdateDocumentFingerprintStateCommand : BaseCommandRequest<ApiResult<DocumentViewModel>>
    {
        public string DocumentId { get; set; }
    }
}
