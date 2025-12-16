using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Commands.DocumentStandardContract
{
    public class UpdateDocumentStandardContractAfterPaidCommand : BaseCommandRequest<ApiResult<UpdateDocumentStandardContractAfterPaidViewModel>>
    {
        public string DocumentNo { get; set; }
    }
}
