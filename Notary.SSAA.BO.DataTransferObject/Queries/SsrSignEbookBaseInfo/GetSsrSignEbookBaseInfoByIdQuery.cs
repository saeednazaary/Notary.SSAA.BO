using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SsrSignEbookBaseInfo;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.SsrSignEbookBaseInfo
{
    public class GetSsrSignEbookBaseInfoByIdQuery : BaseQueryRequest<ApiResult<SsrSignEbookBaseInfoViewModel>>
    {
        public GetSsrSignEbookBaseInfoByIdQuery()
        {
        }
    }
}
