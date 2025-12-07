namespace Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices
{
    public sealed class CheckCircularViewModel
    {
        public CheckCircularViewModel()
        {

            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<CheckCirculartem> GridItems { get; set; }
        public List<CheckCirculartem> SelectedItems { get; set; }
    }
    public sealed class CheckCirculartem
    {
        public string CircularId { get; set; }
        public string CircularTypeTitle { get; set; }
        public string PersonNationalNo { get; set; }
        public string PersonName { get; set; }
        public string PersonFamily { get; set; }
        public bool IsFamilyIncluded { get; set; }
        public string PersonBirthDate { get; set; }
        public string PersonFatherName { get; set; }
        public string PersonSexType { get; set; }
        public string CircularNo { get; set; }
        public string CircularIssueDate { get; set; }
        public string PersonIdentityNo { get; set; }
        public string PersonIdentityIssueLocation { get; set; }
        public string PersonType { get; set; }
        public string CircularItemTypeTitle { get; set; }
        public string CircularProhibitedCaseType { get; set; }
        public bool CircularAllAsset { get; set; }
        public string CircularBaseClaimerNo { get; set; }
        public string CircularBaseClaimerDate { get; set; }
        public string CircularBaseClaimerTitle { get; set; }
        public string CircularCourtNo { get; set; }
        public string CircularCourtDate { get; set; }
        public string CircularCourtTitle { get; set; }
        public string CircularCourtText { get; set; }
        public string CircularDescription { get; set; }
        public string CircularFollowingNo { get; set; }
        public string CircularIncommingLetterNo { get; set; }
        public string CircularFollowingDate { get; set; }
        public string CircularIncommingLetterDate { get; set; }
        public bool IsSelected { get; set; }
    }

    public sealed class CheckCircularExtraParams
    {
        public string PersonType { get; set; }
        public string PersonNationalNo { get; set; }
        public string PersonBirthDate { get; set; }
        public string PersonName { get; set; }
        public string PersonFamily { get; set; }
        public string PersonFatherName { get; set; }
        public string PersonIdentityNo { get; set; }
        public string PersonIdentityIssueLocation { get; set; }
        public string PersonSeri { get; set; }
        public string PersonSerial { get; set; }
        public string PersonSexType { get; set; }
        public string PersonNationalityId { get; set; }
        public string PersonAlphabetSeri { get; set; }
        public string LegalPersonType { get; set; }
        public string CompanyRegisterNo { get; set; }
        public string CompanyRegisterDate { get; set; }
        public string CompanyRegisterLocation { get; set; }
        public string LastLegalPaperDate { get; set; }
        public string LastLegalPaperNo { get; set; }
    }

}
