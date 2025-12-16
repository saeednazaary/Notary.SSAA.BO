using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExecutiveRequest;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.ExecutiveSupport
{
    public class ExecutiveDetailsByIdQuery : BaseQueryRequest<ApiResult<ExecutiveDetailsByIdViewModel>>
    {
        public ExecutiveDetailsByIdQuery()
        {
            ObjectId = new List<string>();
        }
        public IList<string> ObjectId { get; set; }

    }
}
