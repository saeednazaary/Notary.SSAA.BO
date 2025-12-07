using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.DataTransferObject.Queries.Estate.MediaService
{
    public class DownloadAttachmentQuery
    {
        public string AttachmentClientId { get; set; }
        public string AttachmentFileId { get; set; }
        public string AttachmentTypeId { get; set; }
        public string AttachmentRelatedRecordId { get; set; }
    }
}
