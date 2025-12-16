namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Wrappers
{   
    public class WrappersDocument
    {
        public string Id { get; set; }
        public string ScriptoriumId { get; set; }
        public string ReqNo { get; set; }
        public string ReqDate { get; set; }
        public string ReqTime { get; set; }
        public string NationalNo { get; set; }
        public string ClassifyNo { get; set; }
        public string SignDate { get; set; }
        public string SignTime { get; set; }
        public string DocDate { get; set; }
        public WrapperDocumentType TheONotaryDocumentType { get; set; }
        public int? IsRelatedDocAbroad { get; set; }
        public int? RelatedDocumentIsInSSAR { get; set; }
        public string RelatedDocumentNo { get; set; }
        public string RelatedDocumentDate { get; set; }
        public string RelatedScriptoriumId { get; set; }
        public string RelatedDocumentTypeId { get; set; }
        public string RelatedDocumentScriptorium { get; set; }
        public string RelatedDocumentSecretCode { get; set; }
        public string RelatedRegServiceReqId { get; set; }
        public byte[] DocImage2 { get; set; }
        public List<WrapperDocumentPersonFingerPrint> personFingerPrints { get; set; }
        public string State { get; set; }
        public string ScriptoriumCode { get; set; }
        public string TypeGroup2Code { get; set; }
        public string TypeGroup1Code { get; set; }
        public string WriteInBookDate { get; set; }
        public string AdvocacyEndDate { get; set; }
        public int? HasAdvocacy2OthersPermit { get; set; }
        public int? HasTime { get; set; }
        public string LegacyId { get; set; }
        public bool ExtractFromNewSystsm { get; set; }
    }
    public class WrapperDocumentPersonFingerPrint
    {
        public byte[] FingerPrintRawImage { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
    }
    public class WrapperDocumentType
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public int Is4RegisterService { get; set; }
    }


}
