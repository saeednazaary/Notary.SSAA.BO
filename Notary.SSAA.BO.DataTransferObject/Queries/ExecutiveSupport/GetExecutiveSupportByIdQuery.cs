using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExecutiveSupport;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.ExecutiveSupport
{
    public class GetExecutiveSupportByIdQuery : BaseQueryRequest<ApiResult<ExecutiveSupportViewModel>>
    {
        public GetExecutiveSupportByIdQuery(string executiveSupportId)
        {
            ExecutiveSupportId = executiveSupportId;
        }
        public string ExecutiveSupportId { get; set; }
    }
}
