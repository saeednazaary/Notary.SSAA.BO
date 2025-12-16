using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Commands.DocumentTemplate;

public sealed class DeleteDocumentTemplateCommand : BaseCommandRequest<ApiResult>
{
    public DeleteDocumentTemplateCommand()
    {
    }
    public string DocumentTemplateId { get; set; }
    public string DocumentTemplateStateId { get; set; }
}
