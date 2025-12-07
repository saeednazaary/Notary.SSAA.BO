using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentEbookBaseInfo;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SsrSignEbookBaseInfo;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.DocumentEbookBaseInfo
{
    public class GetDocumentEbookBaseInfoByIdQuery : BaseQueryRequest<ApiResult<DocumentEbookBaseInfoViewModel>>
    {
        public GetDocumentEbookBaseInfoByIdQuery()
        {
        }
    }
}
