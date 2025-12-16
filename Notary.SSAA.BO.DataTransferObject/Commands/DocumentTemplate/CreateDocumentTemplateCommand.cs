using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Commands.DocumentTemplate
{
    public sealed class CreateDocumentTemplateCommand : BaseCommandRequest<ApiResult>
    {
        public CreateDocumentTemplateCommand()
        {
            DocumentTypeId=new List<string>();
        }
        public bool IsValid { get; set; }
        public bool IsNew { get; set; }
        public bool IsDelete { get; set; }
        public bool IsDirty { get; set; }
        public string DocumentTemplateCode { get; set; }
        public IList<string> DocumentTypeId { get; set; }
        public string DocumentTemplateTitle { get; set; }
        public string DocumentTemplateStateId { get; set; }
        public string DocumentTemplateText { get; set; }
    }
}
