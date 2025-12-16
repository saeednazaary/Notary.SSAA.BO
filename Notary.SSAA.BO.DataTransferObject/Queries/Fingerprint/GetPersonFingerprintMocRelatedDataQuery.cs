using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Fingerprint;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.Fingerprint
{
    public class GetPersonFingerprintMocRelatedDataQuery : BaseQueryRequest<ApiResult<GetPersonFingerprintMocRelatedDataViewModel>>
    {
        public GetPersonFingerprintMocRelatedDataQuery(string fingerPrintId)
        {
           FingerPrintId = fingerPrintId;
        }

        
        public string FingerPrintId { get; set; }
        
    }
}
