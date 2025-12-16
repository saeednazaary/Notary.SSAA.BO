using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.BO.SignRequest;
public class GetSignRequestByIdServiceInput : BaseExternalRequest<ApiResult<SignRequestViewModel>>
{
    public GetSignRequestByIdServiceInput(string signrequestId)
    {
        SignRequestId = signrequestId;
    }
    public string SignRequestId { get; set; }
}

