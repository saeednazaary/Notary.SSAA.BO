namespace Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentElectronic
{
    //public class DocumentElectronicBookViewModel
    //{
    //    public DocumentElectronicBookViewModel()
    //    {
    //        DocumentElectronicBookPersons = new List<DocumentElectronicPersonViewModel>();
    //    }

    //    public string DocumentId { get; set; }
    //    public string DocumentClassifyNo { get; set; }
    //    public string DocumentReqNo { get; set; }
    //    // شناسه یکتا سند
    //    public string DocumentNationalNo { get; set; }
    //    //تاریخ سند
    //    public string DocumentSignDate { get; set; }
    //    public string DocumentTypeTitle { get; set; }
    //    public string DocumentTypeId { get; set; }
    //    public string DocumentTypeGroup1Title { get; set; }
    //    public string DocumentTypeGroup1Id { get; set; }
    //    public string DocumentTypeGroup2Title { get; set; }
    //    public string DocumentTypeGroup2Id { get; set; }
    //    //رمز تصدیق
    //    public string DocumentSecretCode { get; set; }
    //    public string DocumentDate { get; set; }
    //    //آخرین تصویر
    //    public string DocumentImageFile { get; set; }
    //    public int PageNumber { get; set; }

    //    public IList<DocumentElectronicPersonViewModel> DocumentElectronicBookPersons { get; set; }
    //}

    public class DocumentElectronicBookViewModel
    {
        public bool IsInLagacySystem { get; set; }
        public string Id { get; set; }
        public string NationalNo { get; set; }
        public string ClassifyNo { get; set; }
        public string DocumentDate { get; set; }
        public string DocumentTypeTitle { get; set; }
        public List<DocPerson> DocumentPersons { get; set; }
        public List<DocCase> DocumentCases { get; set; }
        public List<DocOwnership> DocumentOwnerships { get; set; }
        public string DocumentText { get; set; }
        public DocumentElectronicBookViewModel()
        {
            this.DocumentCases = new List<DocCase>();
            this.DocumentPersons = new List<DocPerson>();
            this.DocumentOwnerships = new List<DocOwnership>();
        }
        public bool HasNextClassifyNo { get; set; }
        public bool HasPrevClassifyNo { get; set; }
    }

    public class DocPerson
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string NationalNo { get; set; }
        public string DopcumentPersonTypeTitle { get; set; }
    }
    public class DocCase
    {
        public string Id { get; set; }
        public string Title { get; set; }

    }
    public class DocOwnership
    {
        public string Id { get; set; }
        public string Title { get; set; }

    }

}
