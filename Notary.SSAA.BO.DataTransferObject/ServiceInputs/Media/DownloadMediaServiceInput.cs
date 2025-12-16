using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.Media
{
    public class DownloadMediaServiceInput : BaseExternalRequest<ApiResult<DownloadAttachmentViewModel>>
    {
        public string AttachmentClientId { get; set; }
        public string AttachmentFileId { get; set; }
        public string AttachmentRelatedRecordId { get; set; }
        public string AttachmentTypeId { get; set; }
    }
}
