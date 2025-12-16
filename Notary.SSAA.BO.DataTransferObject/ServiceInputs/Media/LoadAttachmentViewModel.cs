namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.Media
{
    public class AttachmentViewModel
    {
        public List<Media> Medias { get; set; }
        public string DocId { get; set; }
        public string DocType { get; set; }
        public string DocTypeId { get; set; }
        public string DocTypeTitle { get; set; }
        public string DocNo { get; set; }
        public string DocTitle { get; set; }
        public string DocRowNo { get; set; }
        public string DocCreateDate { get; set; }
        public string DocDescription { get; set; }
        public string DocOtherData { get; set; }
    }

    public class Media
    {
        public string MediaId { get; set; }
        public string MediaExtension { get; set; }
        public string RowNo { get; set; }
        public string AttachmentID { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
    }


    public class LoadAttachmentViewModel
    {
        public List<AttachmentViewModel> AttachmentViewModels { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }




}
