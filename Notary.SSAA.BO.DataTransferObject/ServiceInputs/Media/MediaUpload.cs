namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.Media
{
    public class MediaUpload
    {

        public string AttachmentId { get; set; }
        public string MediaId { get; set; }
        public string AllowEditFile { get; set; }
        public string AllowMultiple { get; set; }
        public string AllowFileTypeValidation { get; set; }
        public string AllowFileSizeValidation { get; set; }
        public string ValidFileTypes { get; set; }
        public string MaximumFileSize { get; set; }
        public string MaximumFileCount { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }


    }
}
