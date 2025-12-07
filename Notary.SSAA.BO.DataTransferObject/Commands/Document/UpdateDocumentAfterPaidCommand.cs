using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Commands.Document
{
    public class UpdateDocumentAfterPaidCommand : BaseCommandRequest<ApiResult<UpdateDocumentAfterPaidViewModel>>
    {
        public string DocumentNo { get; set; }
    }
}
