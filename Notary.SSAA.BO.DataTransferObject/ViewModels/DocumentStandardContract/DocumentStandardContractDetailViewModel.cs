using EntityState = Notary.SSAA.BO.DataTransferObject.Bases.EntityState;


namespace Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract
{
    public class DocumentStandardContractDetailViewModel : EntityState
    {
        public DocumentStandardContractDetailViewModel() 
        {
            DocumentInfoText = new();
            DocumentInfoJudgment = new();
            DocumentPerson = new();
        }
        public string RequestDocumentId { get; set; }
        public string RelatedDocumentNo { get; set; }
        public string RelatedDocumentId { get; set; }
        public string RelatedScriptoriumName { get; set; }
        public string RelatedDocumentDate { get; set; }
        public string RelatedDocumentSecretCode { get; set; }
        public string DocumentTypeId { get; set; }
        public string ScriptoriumId { get; set; }
        public string RelatedScriptoriumId { get; set; }
        public string RelatedDocumentTypeId { get; set; }
        public string RelatedDocumentTypeTitle { get; set; }
        public string DocumentTypeTitle { get; set; }
        public string DocumentTypeTitleOther { get; set; }
        public string BookVolumeNo { get; set; }
        public string BookPapersNo { get; set; }
        public int ClassifyNo { get; set; }
        public string WriteInBookDate { get; set; }
        public string DocumentEstateId { get; set; }
        public int Price { get; set; }
        public int RegisterCount { get; set; }
        public DocumentStandardContractInfoTextViewModel DocumentInfoText { get; set; }
        public DocumentStandardContractInfoJudgmentViewModel DocumentInfoJudgment { get; set; }
        public RelatedDocumentStandardContractPersonViewModel DocumentPerson { get; set; }
    }
}
