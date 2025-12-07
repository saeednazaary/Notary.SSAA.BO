namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.MediaService
{
    public class LoadAttachmentsViewModel
    {
        public List<AttachmentViewModel> AttachmentViewModels { get; set; }      
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
    public class AttachmentViewModel
    {
        public List<Media> Medias { get; set; }
        public string DocId { get; set; }
        public string DocType { get; set; }
        public string DocTypeTitle { get; set; }
        public string DocNo { get; set; }
        public string DocTitle { get; set; }
        public string DocRowNo { get; set; }
        public string DocCreateDate { get; set; }
        public string DocDescription { get; set; }
        public string DocOtherData { get; set; }
        public string AllowEditFile { get; set; }
        public string AllowMultiple { get; set; }
        public string AllowFileTypeValidation { get; set; }
        public string AllowFileSizeValidation { get; set; }
        public string ValidFileTypes { get; set; }
        public string MaximumFileSize { get; set; }
        public string MaximumFileCount { get; set; }
    }

    public class Media
    {
        public object MediaFile { get; set; }
        public string MediaThumbNail { get; set; }
        public string MediaId { get; set; }
        public string MediaExtension { get; set; }
        public string RowNo { get; set; }
        public string AttachmentID { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
    }

}
