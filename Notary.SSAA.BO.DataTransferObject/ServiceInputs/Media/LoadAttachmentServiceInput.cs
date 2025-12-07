using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.Media
{
    public class LoadAttachmentServiceInput : BaseExternalRequest<ApiResult<LoadAttachmentViewModel>>
    {
        public List<string> RelatedRecordIds { get; set; }
        public string ClientId { get; set; }
    }
}
