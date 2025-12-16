using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DigitalSign;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.SignRequest
{
    public class GetSignRequestSignDataQuery : BaseQueryRequest<ApiResult<SignDataViewModel>>
    {
        public GetSignRequestSignDataQuery(string signRequestId)
        {
            SignRequestId = signRequestId;
        }

        public string SignRequestId { get; set; }

    }
}
