
namespace Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices.Estate
{
    public class ConfirmDocumentByElectronicEstateNoteNoViewModel
    {
        public decimal Area { get; set; }
        public string EPieceTypeTitle { get; set; }
        public string EstateTypeTitle { get; set; }
        public string EstateUsingType { get; set; }
        public bool IsArrested { get; set; }
        public bool HasRahn { get; set; }
        public bool HasOtherRestriction { get; set; }
        public string OwnerName { get; set; }
        public string OwnerFamily { get; set; }
        public string OwnerIdentityNo { get; set; }
        public string PrintedDocNo { get; set; }
        public string JamCode { get; set; }
        public string Section { get; set; }
        public string SubSection { get; set; }
        public string EstatePostCode { get; set; }
        public string Basic { get; set; }
        public string Secondary { get; set; }
        public string ProvinceName { get; set; }
        public string UnitName { get; set; }
        public string OwnerNationalityCode { get; set; }
        public string ShareOf { get; set; }
        public string ShareTotal { get; set; }
        public string ShareText { get; set; }
        public string OwnershipType { get; set; }
        public string AreaUnit { get; set; }
        public byte[] Map { get; set; }
        public string Map_Base64String { get; set; }
        public bool Successful
        {
            get;
            set;
        }
        public string ErrorMessage
        {
            get;
            set;
        }
        public string ResponseNo { get; set; }
        public string ResponseDateTime { get; set; }
        public string RequestDateTime { get; set; }
        public string UnitId { get; set; }
        public string SectionID { get; set; }
        public string SubSectionID { get; set; }
        public string EstateTypeId { get; set; }
        public string EstateUsingTypeId { get; set; }
        public string ProvinceId { get; set; }
        public string CountyId { get; set; }
    }

}
