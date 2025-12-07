
namespace Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices
{
    public class SabtAhvalServiceViewModel
    {
        public SabtAhvalServiceViewModel()
        {
            this.IssueGeolocationId = new List<string>();
        }
        public string DeathDate { get; set; }
        public bool IsDead { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string SexType { get; set; }
        public string ShenasnameNo { get; set; }
        public string ShenasnameSeri { get; set; }
        public string ShenasnameSerial { get; set; }
        public string BirthDate { get; set; }
        public string NationalNo { get; set; }
        public byte[] PersonalImage { get; set; }
        public string PostalCode { get; set; }
        public string PostAddress { get; set; }
        public string IdentityIssueLocation { get; set; }
        public string AlphabetSeri { get; set; }
        public string[] Messages { get; set; }
        public IList<string> IssueGeolocationId { get; set; }
        public string IdentityIssueLocation2 { get; set; }
    }
}
