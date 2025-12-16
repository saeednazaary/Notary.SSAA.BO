using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate
{   
    public class SendDealSummaryInput<T>:BaseExternalRequest<ApiResult<T>> where T : class
    {        
        public List<DSUDealSummaryObject> DsuDealSummary { get; set; }        
        public object Tag { get; set; }
        public string ClientId { get; set; }
        public SendDealSummaryInput()
        {
            this.ClientId = "SSAR";
        }
        public bool? IsRemoveRestrictionDealSummary { get; set; }
    }
    public class DSUDealSummaryObject
    {
        public int InputCorrectionIsDone { get; set; }
        public DSUDealSummaryObject()
        {
            this.TheDSURealLegalPersonList = new List<DSURealLegalPersonObject>();
            InputCorrectionIsDone = 0;

        }
        public string Address { get; set; }//Address;

        public long? Amount { get; set; }//Amount;


        public string AmountUnitId { get; set; }//AmountUnitId;


        public decimal? Area { get; set; }//Area;


        public string Attached { get; set; }//Attached;


        public string Basic { get; set; }//Basic;


        public string BasicAppendant { get; set; }//BasicAppendant;

        public string BSTSeriDaftarId { get; set; }//BSTSeriDaftarId;

        public string CertificateBase64 { get; set; }//CertificateBase64;

        public long? Counter { get; set; }//Counter;

        public string CREATEDATETIME { get; set; }//CREATEDATETIME;

        public byte[] DataSignature { get; set; }//DataSignature;

        public string DataSignatureBase64String { get; set; }//DataSignatureBase64String;

        public string DealDate { get; set; }//DealDate;

        public string DealMainDate { get; set; }//DealMainDate;

        public string DealNo { get; set; }//DealNo;

        public string DealSummeryIssueeId { get; set; }//DealSummeryIssueeId;

        public string DealSummeryIssuerId { get; set; }//DealSummeryIssuerId;

        public string Description { get; set; }//Description;

        public string DSUOwnerShipTypeId { get; set; }//DSUOwnerShipTypeId;

        public string DSURemoveRestirctionTypeId { get; set; }//DSURemoveRestirctionTypeId;

        public string DSUTransferTypeId { get; set; }//DSUTransferTypeId;
        string _DSUTransitionCaseId;

        public string DSUTransitionCaseId
        {
            get
            {
                return this._DSUTransitionCaseId;
            }
            set
            {
                this._DSUTransitionCaseId = value;
            }
        }//DSUTransitionCaseId;

        public long? Duration { get; set; }//Duration;

        public string Id { get; set; }

        public string ESTEstateInquiryId { get; set; }//ESTEstateInquiryId;

        public string GeoLocationId { get; set; }//GeoLocationId;

        public string HasFollowingDealSummary { get; set; }//HasFollowingDealSummary;

        public string NotebookSeri { get; set; }//NotebookSeri;

        public string NotebookSupplement { get; set; }//NotebookSupplement;

        public string OfficeNo { get; set; }//OfficeNo;

        public string PageNo { get; set; }//PageNo;

        public string PiceNo { get; set; }//PiceNo;

        public string PostalCode { get; set; }//PostalCode;

        public string PrintNumberDoc { get; set; }//PrintNumberDoc;

        public string ProfitTo { get; set; }//ProfitTo;

        public double? PropertyPrice { get; set; }//PropertyPrice;

        public string RegisterNo { get; set; }//RegisterNo;

        public string RegistrationTime { get; set; }//RegistrationTime;

        public string RemoveRestrictionDate { get; set; }//RemoveRestrictionDate;

        public string RemoveRestrictionNo { get; set; }//RemoveRestrictionNo;

        public string RemoveRestrictRegulatory { get; set; }//RemoveRestrictRegulatory;

        public string Response { get; set; }//Response;

        public string ResponseCode { get; set; }//ResponseCode;

        public string ResponseDate { get; set; }//ResponseDate;

        public byte[] ResponseDigitalSignature { get; set; }//ResponseDigitalSignature;

        public double? ResponseResult { get; set; }//ResponseResult;

        public string saleInstatnce { get; set; }//saleInstatnce;

        public string Secondary { get; set; }//Secondary;

        public string SecondaryAppendant { get; set; }//SecondaryAppendant;

        public string SectionId { get; set; }//SectionId;

        public string SendDate { get; set; }//SendDate;

        public long? SENDINGSTATUS { get; set; }//SENDINGSTATUS;

        public string SignerUserId { get; set; }//SignerUserId;

        public string SubjectDn { get; set; }//SubjectDn;

        public string SubsectionId { get; set; }//SubsectionId;

        public string Surplus { get; set; }//Surplus;

        public string SystemDealNo { get; set; }//SystemDealNo;

        public string TimeUnitId { get; set; }//TimeUnitId;

        public string TrtsReadTime { get; set; }//TrtsReadTime;

        public string UnitMessage { get; set; }//UnitMessage;

        public string UnitSubjectDn { get; set; }//UnitSubjectDn;

        public string unrestrictedOrganizationId { get; set; }//unrestrictedOrganizationId;


        public List<DSURealLegalPersonObject> TheDSURealLegalPersonList { get; set; }


        public DsuTransferTypeObject TheDSUTransferType { get; set; }


        public string NotaryDocumentNo { get; set; }

        //internal static DSUDealSummaryObject GetObjectByXml(string xml)
        //{

        //    System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(DSUDealSummaryObject));
        //    System.IO.TextReader textReader = new System.IO.StringReader(xml);
        //    object logObject = serializer.Deserialize(textReader);
        //    DSUDealSummaryObject logMaster = (DSUDealSummaryObject)logObject;

        //    return logMaster;

        //}


        public string SignDate { get; set; }


        public List<DSUDealSummaryAttachObject> TheDSUDealSummaryAttachList
        {
            get;
            set;
        }


        public string AttachSecondary { get; set; }


        public string PageNoteSystemNo { get; set; }
    }

    public class DSURealLegalPersonObject
    {

        public string Address { get; set; } //.Address;

        public string BirthDate { get; set; } //.BirthDate;

        public string BirthdateId { get; set; } //.BirthdateId;

        public string CityId { get; set; } //.CityId;

        public string Description { get; set; } //.Description;

        public string DSUDealSummaryId { get; set; } //.DSUDealSummaryId;

        public string DSURelationKindId { get; set; } //.DSURelationKindId;

        public bool? ExecutiveTransfer { get; set; } //.ExecutiveTransfer;

        public string Family { get; set; } //.Family;

        public string FatherName { get; set; } //.FatherName;

        public string IdentificationNo { get; set; } //.IdentificationNo;

        public string IssuePlaceId { get; set; } //.IssuePlaceId;

        public string Name { get; set; } //.Name;

        public string NationalCode { get; set; } //.NationalCode;

        public string NationalityId { get; set; } //.NationalityId;

        public string OctantQuarter { get; set; } //.OctantQuarter;

        public string OctantQuarterPart { get; set; } //.OctantQuarterPart;

        public string OctantQuarterTotal { get; set; } //.OctantQuarterTotal;

        public long? personType { get; set; } //.personType;

        public string PostalCode { get; set; } //.PostalCode;

        public string Role { get; set; } //.Role;

        public string Seri { get; set; } //.Seri;

        public long? Serial { get; set; } //.Serial;

        public int? sex { get; set; } //.sex;

        public string ShareContext { get; set; } //.ShareContext;

        public decimal? SharePart { get; set; } //.SharePart;

        public decimal? ShareTotal { get; set; } //.ShareTotal;

        public string Id { get; set; }


        public string RelatedPersonId { get; set; }


        public long? IsInquiryPerson { get; set; }

        //
        //public string  SabtAhvalIsuuePlace { get; set; }
    }

    public class DsuTransferTypeObject
    {

        public string Code { get; set; } //Code;

        public string IsRestricted { get; set; } //IsRestricted;

        public string State { get; set; } //State;

        public string Title { get; set; } //Title;
    }

    public class DSUDealSummaryAttachObject
    {

        public decimal? Area
        {
            get;
            set;
        }




        public string Block
        {
            get;
            set;
        }




        public string DSUDealSummaryId
        {
            get;
            set;
        }



        public string EPieceTypeId
        {
            get;
            set;
        }



        public string Floor
        {
            get;
            set;
        }




        public string Id
        {
            get;
            set;
        }



        public string Piece
        {
            get;
            set;
        }




        public string Side
        {
            get;
            set;
        }

        public long TimeStamp
        {
            get;
            set;
        }

    }
}
