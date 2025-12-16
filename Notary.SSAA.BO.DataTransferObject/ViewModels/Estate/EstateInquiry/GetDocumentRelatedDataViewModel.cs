using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry
{
    

    public class GetDocumentRelatedDataViewModel
    {
        public GetDocumentRelatedDataViewModel()
        {
            this.HasAlarmMessage = false;
            this.AlarmMessage = string.Empty;
        }
        public DocumentRelatedData DocumentRelatedData { get; set; }
        public bool HasAlarmMessage { get; set; }
        public string AlarmMessage { get; set; }
    }

    public class DocumentRelatedData
    {
        
        public DocumentRelatedData()
        {
            this.DocumentEstateList = new List<DocumentEstate>();
            this.DocumentEstateQuotaList = new List<DocumentEstateQuota>();
            this.DocumentEstateQuotaDetailsList = new List<DocumentEstateQuotaDetails>();
            this.DocumentPersonList = new List<DocumentPerson>();
            this.DocumentEstateOwnershipDocumentList = new List<DocumentEstateOwnershipDocument>();
            this.DocumentInquiryList = new List<DocumentInquiry>();
            this.DocumentEstateInquiryList=new List<DocumentEstateInquiry>();
        }
        public List<DocumentInquiry> DocumentInquiryList { get; set; }
        public List<DocumentEstate> DocumentEstateList { get; set; }
        public List<DocumentPerson> DocumentPersonList { get; set; }
        public List<DocumentEstateQuota> DocumentEstateQuotaList { get; set; }
        public List<DocumentEstateOwnershipDocument> DocumentEstateOwnershipDocumentList { get; set; }
        public List<DocumentEstateQuotaDetails> DocumentEstateQuotaDetailsList { get; set; }
        public List<DocumentEstateInquiry> DocumentEstateInquiryList { get; set; }
    }
    public class DocumentInquiry
    {
        public Guid Id { get; set; }
        public string DocumentInquiryOrganizationId { get; set; }
        public string EstateInquiriesId { get; set; }
        public Guid DocumentId { get; set; }       
        public string RequestNo { get; set; }
        public string RequestDate { get; set; }        
        public string ReplyNo { get; set; }
        public string ReplyDate { get; set; }        
        public string ReplyText { get; set; }
        public decimal? ReplyDetailQuota { get; set; }
        public decimal? ReplyTotalQuota { get; set; }
        public string ReplyQuotaText { get; set; }
        public long? Price { get; set; }
        public string State { get; set; }
        public string ScriptoriumId { get; set; }
        public string Ilm { get; set; }
        public Guid? DocumentPersonId { get; set; }
        public Guid? DocumentEstateId { get; set; }
        public Guid? DocumentEstateOwnershipDocumentId { get; set; }

    }
    public class DocumentEstate
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public decimal? Area { get; set; }
        public int? GeoLocationId { get; set; }
        public string IsAttachment { get; set; }
        public string IsRegistered { get; set; }
        public string IsProportionateQuota { get; set; }
        public string BasicPlaque { get; set; }
        public string BasicPlaqueHasRemain { get; set; }
        public string SecondaryPlaqueHasRemain { get; set; }
        public string SecondaryPlaque { get; set; }
        public string UnitId { get; set; }
        public string EstateSectionId { get; set; }
        public string EstateSubsectionId { get; set; }
        public string PostalCode { get; set; }
        public string EstateInquiryId { get; set; }
        public string Ilm { get; set; }
        public string ScriptoriumId { get; set; }
        public decimal? OwnershipDetailQuota { get; set; }
        public decimal? OwnershipTotalQuota { get; set; }
    }
    public class DocumentPerson
    {
        public Guid Id { get; set; }
        public string BirthDate { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string NationalNo { get; set; }
        public int? NationalityId { get; set; }
        public string IsRelated { get; set; }
        public string PersonType { get; set; }
        public string IsIranian { get; set; }
        public string IsOriginal { get; set; }
        public string SexType { get; set; }
        public string FatherName { get; set; }
        public string IdentityIssueLocation { get; set; }
        public int? IdentityIssueGeoLocationId { get; set; }
        public string IdentityNo { get; set; }
        public string SeriAlpha { get; set; }
        public string Seri { get; set; }
        public string Serial { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string CompanyRegisterNo { get; set; }
        public string CompanyRegisterDate { get; set; }
        public string IsSabtahvalChecked { get; set; }
        public string IsSabtahvalCorrect { get; set; }
        public string MobileNo { get; set; }
        public string MobileNoState { get; set; }
        public string SanaState { get; set; }
        public string ScriptoriumId { get; set; }
        public string Ilm { get; set; }
        public string EstateInquiryId { get; set; }
    }
    public class DocumentEstateQuota
    {
        public Guid Id { get; set; }
        public Guid DocumentEstateId { get; set; }
        public Guid DocumentPersonId { get; set; }
        public decimal? DetailQuota { get; set; }
        public decimal? TotalQuota { get; set; }
        public string QuotaText { get; set; }
        public string ScriptoriumId { get; set; }
        public string Ilm { get; set; }

    }
    public class DocumentEstateOwnershipDocument
    {
        public Guid Id { get; set; }
        public Guid DocumentEstateId { get; set; }
        public Guid DocumentPersonId { get; set; }
        public string OwnershipDocumentType { get; set; }
        public string EstateSabtNo { get; set; }
        public string EstateDocumentNo { get; set; }
        public string EstateBookNo { get; set; }
        public string EstateBookPageNo { get; set; }
        public string EstateBookType { get; set; }
        public string EstateElectronicPageNo { get; set; }
        public string EstateSeridaftarId { get; set; }
        public string EstateIsReplacementDocument { get; set; }
        public string MortgageText { get; set; }
        public string EstateDocumentType { get; set; }
        public string ScriptoriumId { get; set; }
        public string Ilm { get; set; }
        public string EstateInquiriesId { get; set; }
    }
    public class DocumentEstateQuotaDetails
    {
        public Guid Id { get; set; }
        public Guid DocumentPersonSellerId { get; set; }
        public Guid DocumentEstateId { get; set; }
        public Guid? DocumentEstateOwnershipDocumentId { get; set; }
        public decimal? OwnershipDetailQuota { get; set; }
        public decimal? OwnershipTotalQuota { get; set; }
        public decimal? SellTotalQuota { get; set; }
        public string QuotaText { get; set; }
        public string ScriptoriumId { get; set; }
        public string Ilm { get; set; }
        public string EstateInquiriesId { get; set; }

    }
    public class DocumentEstateInquiry
    {
        public Guid Id { get; set; }
        public Guid DocumentEstateId { get; set; }
        public Guid EstateInquiryId { get; set; }
        public string ScriptoriumId { get; set; }
        public string Ilm { get; set; }
    }

}
