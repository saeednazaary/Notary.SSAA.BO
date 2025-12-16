

using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.SignRequest
{
    public class SignRequestStatisticReportQuery : BaseQueryRequest<ApiResult<ReportSignRequestViewModel>>
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public IList<string> Getter {  get; set; }
        public IList<string> Subjects { get; set; }
    }
}
