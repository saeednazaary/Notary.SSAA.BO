using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Commands.DocumentStandardContract
{
    public class CreateDocumentStandardContractCostsCommand : BaseCommandRequest<ApiResult>
    {
        public string DocumentId { get; set; }

    }
}
