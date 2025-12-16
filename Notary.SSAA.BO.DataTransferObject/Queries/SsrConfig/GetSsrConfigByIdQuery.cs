using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SsrConfig;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.SsrConfig
{
    public class GetSsrConfigByIdQuery : BaseQueryRequest<ApiResult<SsrConfigViewModel>>
    {
        public GetSsrConfigByIdQuery(string ssrConfigId)
        {
                SsrConfigId = ssrConfigId;
        }
        public string SsrConfigId { get; set; }
    }
}
