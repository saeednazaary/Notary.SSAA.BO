using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.SignRequest
{
    public class ReportForFOSignRequestQuery : BaseQueryRequest<ApiResult<ReportSignRequestViewModel>>
    {
        public string SignRequestId { get; set; }
        public string SignRequestNo { get; set; }
    }
}
