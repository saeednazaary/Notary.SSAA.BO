using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.BO.SignRequest;
public class GetSignRequestReportServiceInput : BaseExternalRequest<ApiResult<ReportSignRequestViewModel>>
{
    public string SignRequestId { get; set; }
    public string SignRequestNo { get; set; }
}

