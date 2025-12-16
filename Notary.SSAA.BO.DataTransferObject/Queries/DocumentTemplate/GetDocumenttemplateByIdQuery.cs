using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentTemplate;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.DocumentTemplate
{
    public sealed class GetDocumenttemplateByIdQuery : BaseQueryRequest<ApiResult<DocumentTemplateViewModel>>
    {
        public string DocumentTemplateId { get; set; }
    }
}
