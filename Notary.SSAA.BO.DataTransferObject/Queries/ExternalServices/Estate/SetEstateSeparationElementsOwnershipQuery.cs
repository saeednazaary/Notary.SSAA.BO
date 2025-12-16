using   Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices.Estate;
using Notary.SSAA.BO.SharedKernel.Result;



namespace Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices.Estate
{
    public class SetEstateSeparationElementsOwnershipQuery : BaseQueryRequest<ApiResult>
    {
        public string ClientId {  get; set; }   
        public List<ElementOwnershipInfo> TheElementOwnershipInfoList { get; set; }

        public List<ElementsRelationInfo> TheElementsRelationInfoList { get; set; }


        public string DocumentNo { get; set; }

        public string DocumentDate { get; set; }


        public string NotaryOfficeCmsId { get; set; }


        public string InquiryId { get; set; }


        public string Certificate { get; set; }


        public string DigitalSignatureOfData { get; set; }


        public List<string> OtherRelatedInquiryList { get; set; }

        public SetEstateSeparationElementsOwnershipQuery()
        {
            ClientId = "SSAR";
        }

    }
    public class ElementOwnershipInfo
    {

        public SeparationElement TheElementInfo { get; set; }

        public string OwnerFirstName { get; set; }

        public string OwnerLastName { get; set; }

        public string OwnerNationalityCode { get; set; }

        public string OwnerBirthDate { get; set; }

        public string OwnerIdentityNo { get; set; }

        public string OwnerFatherName { get; set; }

        public int PersonType { get; set; }


        public int? Sex { get; set; }


        public decimal SharePart { get; set; }


        public decimal ShareTotal { get; set; }


        public string ShareText { get; set; }


        public string TextBetting { get; set; }


        public string PostCode { get; set; }


        public string IssueLocationId { get; set; }


        public string BirthLocationId { get; set; }


        public string NationalityId { get; set; }


        public string EMailAddress { get; set; }


        public string Address { get; set; }


        public string MobileNumber4SMS { get; set; }


        public string pseudonym { get; set; }
    }

    public class SeparationElement
    {


        public bool HasOwnership { get; set; }


        public string PlaqueOriginal { get; set; }

        public string SidewayPlaque { get; set; }

        public string Sector { get; set; }

        public decimal? Area { get; set; }

        public string Class { get; set; }

        public string Side { get; set; }

        public string AreaUnitId { get; set; }

        public string SouthLimit { get; set; }

        public string NorthLimit { get; set; }

        public string EastLimit { get; set; }

        public string WestLimit { get; set; }

        public string EPieceType { get; set; }

        public string EPieceTypeId { get; set; }

        public string EKindPieceCode { get; set; }

        public string Separate { get; set; }

        public string SectionId { get; set; }


        public string SubSectionId { get; set; }


        public string UnitId { get; set; }


        public string OtherDescription { get; set; }


        public string ProfitLaw { set; get; }


        public string EstateHightLaw { get; set; }


        public string Block { get; set; }


        public bool IsAssigned { get; set; }
    }
    public class ElementsRelationInfo
    {

        public SeparationElement ParentElement { get; set; }


        public SeparationElement ChildElement { get; set; }
    }

}
