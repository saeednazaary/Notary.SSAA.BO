using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Commands.ElzamArtSix
{
    public class CreateElzamArtSixCommand : BaseCommandRequest<ApiResult>
    {
        public CreateElzamArtSixCommand()
        {
        }
        public bool IsDirty { get; set; }
        public bool IsValid { get; set; }
        public bool IsInEditMode { get; set; }
        public bool IsSaved { get; set; }
        public bool IsDelete { get; set; }
        public bool IsLoaded { get; set; }
        public bool IsNew { get; set; }
        public string Id { get; set; }
        public string No { get; set; }
        public string CreateDate { get; set; }
        public string CreateTime { get; set; }
        public string Type { get; set; }
        public string[] ProvinceId { get; set; }
        public string[] CountyId { get; set; }
        public string EstateMap { get; set; }
        public string EstatePersonId { get; set; }
        public CreateElzamArtSixPersonCommand ElzamArtSixSellerPerson { get; set; } = null;
        public CreateElzamArtSixPersonCommand ElzamArtSixBuyerPerson { get; set; } = null;
        public object PersonalImage { get; set; }
        public List<string> EstateUnitId { get; set; }
        public string EstateUnitTitle { get; set; }
        public List<string> EstateSectionId { get; set; }
        public string EstateSectionTitle { get; set; }
        public List<string> EstateSubsectionId { get; set; }
        public string EstateSubsectionTitle { get; set; }
        public string EstateMainPlaque { get; set; }
        public string EstateSecondaryPlaque { get; set; }
        public bool? EstRelatedInfoLoadBySvc { get; set; }
        public long EstateArea { get; set; }
        public string EstatePostCode { get; set; }
        public string EstatePieceTypeId { get; set; }
        public string Attachments { get; set; }
        public string SendDate { get; set; }
        public string SendTime { get; set; }
        public string ScriptoriumId { get; set; }
        public string WorkflowStatesId { get; set; }
        public string WorkflowStatesTitle { get; set; }
        public string EstateDocElectronicNoteNo { get; set; }
        public string Ilm { get; set; }
        public string TrackingCode { get; set; }
        public string Address { get; set; }
        public List<string> EstateUsingId { get; set; }
        public List<string> EstateTypeId { get; set; }
        public List<string> ElzamArtSixOrganId { get; set; } = new();
    }
    public class CreateElzamArtSixPersonCommand
    {
        public string Id { get; set; }
        public object AlImage { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string BirthDate { get; set; }
        public string NationalityCode { get; set; }
        public string MobileNo { get; set; }
        public string FatherName { get; set; }
        public string PostalCode { get; set; }
        public string IdentityNo { get; set; }
        public string Post { get; set; }
        public string IssuePlaceText { get; set; }
        public string Address { get; set; }
        public bool? IsIlencChecked { get; set; }
        public bool? IsSanaChecked { get; set; }
        public bool? AmlakEskanState { get; set; }
        public bool? IsSabteAhvalChecked { get; set; }
        public bool? IsAlive { get; set; }
        public bool? MobileNoState { get; set; }
        public bool? IsRelated { get; set; }
        public string AlphabetSeri { get; set; }
        public string Seri { get; set; }
        public string SerialNo { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string SexType { get; set; }
        public string PersonType { get; set; }
        public string RelationType { get; set; }
        public List<string> NationalityId { get; set; }
        public bool? IsOriginal { get; set; }
        public bool? IsIranian { get; set; }
        public string ElzamArtSixId { get; set; }
        public string TfaState { get; set; }
        public bool? TFARequired { get; set; }
        public string ManagerNationalNo { get; set; }
        public string ManagerName { get; set; }
        public string ManagerFamily { get; set; }
        public string LawyerNationalId { get; set; }
        public string LawyerName { get; set; }
        public string LawyerMobile { get; set; }
        public string LawyerPostalCode { get; set; }
        public string LawyerBirthDate { get; set; }
        public string LawyerFatherName { get; set; }
        public bool? HasLawyer { get; set; }
        public string ShareTotal { get; set; }
        public string SharePart { get; set; }
    }

}




