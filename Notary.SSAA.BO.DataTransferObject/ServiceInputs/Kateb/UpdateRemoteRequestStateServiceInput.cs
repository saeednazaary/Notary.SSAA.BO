using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.Kateb
{
    public class UpdateRemoteRequestStateServiceInput : BaseExternalRequest<ApiResult>
    {
        public UpdateRemoteRequestStateServiceInput(string id, string code)
        {
            Id = id;
            Code = code;
            Message = null;
            ReasonId = null;
        }
        public string Id { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public string ReasonId { get; set; }
    }
}
