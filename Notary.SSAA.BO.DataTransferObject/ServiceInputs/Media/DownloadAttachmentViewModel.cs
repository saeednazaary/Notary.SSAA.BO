namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.Media
{
    public class DownloadAttachmentViewModel
    {
        public byte[] MediaFile { get; set; }
        public byte[] MediaThumbNail { get; set; }
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
