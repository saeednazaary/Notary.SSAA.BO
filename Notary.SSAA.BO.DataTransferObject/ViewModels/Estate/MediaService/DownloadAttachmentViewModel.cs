using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.MediaService
{
    public class DownloadAttachmentViewModel
    {
        public string MediaFile { get; set; }
        public string MediaThumbNail { get; set; }
        public string MediaId { get; set; }
        public string MediaExtension { get; set; }
        public string MediaFileType { get; set; }
        public string RowNo { get; set; }
        public string AttachmentID { get; set; }
        public string AttachmentTypeID { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
