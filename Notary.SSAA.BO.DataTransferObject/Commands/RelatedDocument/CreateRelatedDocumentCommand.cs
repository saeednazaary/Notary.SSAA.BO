using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.RelatedDocument;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Commands.RelatedDocument
{
    public class CreateRelatedDocumentCommand : BaseCommandRequest<ApiResult>
    {
        public bool IsNew { get; set; }
        public bool IsDelete { get; set; }
        public bool IsDirty { get; set; }
        public string RelatedDocumentNo { get; set; }
        public string RelatedDocumentId { get; set; }
        public string RelatedDocumentDate { get; set; }
        public string RelatedDocumentSecretCode { get; set; }
        public string DocumentSecretCode { get; set; }
        public string ScriptoriumId { get; set; }
        public string RelatedScriptoriumId { get; set; }
        public string BookVolumeNo { get; set; }
        public string BookPapersNo { get; set; }
        public string ClassifyNo { get; set; }
        public string WriteInBookDate { get; set; }
        public string Price { get; set; }
        public string RegisterCount { get; set; }
        public string RequestNo { get; set; }
        public string DocumentDate { get; set; }
        public string DocumentTypeTitle { get; set; }
        public string NationalNo { get; set; }
        public IList<string> DocumentEstateId { get; set; }
        public IList<string> DocumentTypeId { get; set; }
        public IList<string> RelatedDocumentTypeId { get; set; }
        public string EvacuatedDate { get; set; }
        public bool? IsRequestInSsar { get; set; }
        public RelatedDocumentInfoTextViewModel DocumentInfoText { get; set; }
        public RelatedDocumentInfoJudgmentViewModel DocumentInfoJudgment { get; set; }
        public RelatedDocumentPersonViewModel DocumentPerson { get; set; }
    }
}
