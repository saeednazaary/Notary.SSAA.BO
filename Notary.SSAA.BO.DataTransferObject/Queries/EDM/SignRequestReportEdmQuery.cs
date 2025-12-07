using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Queries.EDM
{
    public class SignRequestReportEdmQuery : BaseQueryRequest<ApiResult>
    {
        public SignRequestReportEdmQuery(string signRequestId)
        {
            SignRequestId = signRequestId;
        }

        public string SignRequestId { get; set; }
    }
}