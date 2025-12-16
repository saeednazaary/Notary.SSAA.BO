

using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.SignRequest
{
    public class SignRequestFingerPrintReportQuery : BaseQueryRequest<ApiResult<ReportSignRequestViewModel>>
    {
        public string signRequestId { get; set; }
    }
}
