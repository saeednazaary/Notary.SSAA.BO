using Notary.SSAA.BO.SharedKernel.Contracts.Coordinator.Document;
using System.Drawing;
using System.Reflection.Metadata;

namespace Notary.SSAA.BO.SharedKernel.SharedModels.DocumentReport
{
    public class ONotaryRegiserServiceReqEntity
    {
        public ONotaryRegiserServiceReqEntity()
        {
            SeparationPieces = new List<ONotarySeparationEntity>();
            AnnotationList = new List<AnnotationPack>();
            SignablePersons = new List<SignablePerson>();
            DocPersons = new List<ONotaryDocPersonEntity>();
            RegCases = new List<ONotaryRegCaseEntity>();
            Inquiries = new List<ONotaryRegServiceInquiryEntity>();
            RegServiceCosts = new List<ONotaryRegServiceCostEntity>();
        }
        public string ClassifyNo { get; set; }
        public string RelatedDocumentNo { get; set; }
        public string DaftaryarConfirmDate { get; set; }
        public string DaftaryarNameFamily { get; set; }
        public string DocDate { get; set; }
        public string DocumentSecretCode { get; set; }
        public decimal? DocumentTextFontSize { get; set; }
        public string LegalText { get; set; }
        public string GetDocNoDate { get; set; }
        public string Id { get; set; }
        public string NationalNo { get; set; }
        public string ONotaryDocumentTypeId { get; set; }
        public string ONotaryDocumentTypeTitle { get; set; }
        public string ONotaryDocumentCode { get; set; }
        public string ONotaryRegisterServiceTypeId { get; set; }
        public string ONotaryRegisterServiceTypeTitle { get; set; }
        public string WealthType { get; set; }
        public string RegisterPapersNo { get; set; }
        public string RegisterVolumeNo { get; set; }
        public string SardaftarConfirmDate { get; set; }
        public string SardaftarNameFamily { get; set; }
        public string ScriptoriumId { get; set; }
        public string ScriptoriumTitle { get; set; }
        public string ExordiumNameFamily { get; set; }
        public string ScriptoriumAddress { get; set; }
        public string MatrixBarcode { get; set; }
        public int? State { get; set; }
        public string StateDesc { get; set; }
        public decimal? Price { get; set; }
        public string HowToPay { get; set; }
        public string Description { get; set; }
        public string DocumentDescription { get; set; }
        public IList<ONotaryDocPersonEntity> DocPersons { get; set; }
        public IList<ONotaryRegCaseEntity> RegCases { get; set; }
        public IList<ONotaryRegServiceInquiryEntity> Inquiries { get; set; }
        public IList<ONotaryRegServiceCostEntity> RegServiceCosts { get; set; }
        public IList<SignablePerson> SignablePersons { get; set; }//-
        public string RelatedRequestId { get; set; }
        public ONotaryRegiserServiceReqEntity RelatedRequest { get; set; }
        public ONotaryRequestOtherInfo RequestOtherInfo { get; set; }
        public string LetterOfPrice { get; set; }

        public string ConfirmDate { get; set; }
        public string ConfirmerNameFamily { get; set; }
        public string ConfirmPayCost { get; set; }
        public string CreateDate { get; set; }
        public string CreatorNameFamily { get; set; }
        public string DocText { get; set; }

        public string FactorDate { get; set; }
        public string FactorNo { get; set; }
        public string HasMultipleAdvocacyPermit { get; set; }
        public string IsDocumentBrief { get; set; }
        public string RelatedRegCaseId { get; set; }
        public string RelatedRegServiceReqId { get; set; }
        public string ReqDate { get; set; }
        public string ReqNo { get; set; }
        public string ReqTime { get; set; }
        public decimal? SabtPrice { get; set; }
        public string SignDate { get; set; }
        public string SignTime { get; set; }
        public string SayerDocTitle { get; set; }
        public string RelatedDocInfo { get; set; }
        public string BailsmanNameFamily { get; set; }
        public decimal SignablePersonCount { get; set; }
        public string SignTitle { get; set; }
        public string WriteInBookDate { get; set; }
        public string PrintMode { get; set; }

        public string CurrencyTitle { get; set; }
        public List<AnnotationPack> AnnotationList { get; set; }

        public string ScriptoriumCode { get; set; }
        public string ConfDate { get; set; }

        public IList<ONotarySeparationEntity> SeparationPieces { get; set; }

        public bool IsMain { get; set; }
        public bool IsRooNevesht { get; set; }
        public bool IsSabtServices { get; set; }
        public bool IsReduplicatePDF { get; set; }
        public bool IsMoaref { get; set; }
        public string PageHeaderTitle { get; set; }



    }
}
