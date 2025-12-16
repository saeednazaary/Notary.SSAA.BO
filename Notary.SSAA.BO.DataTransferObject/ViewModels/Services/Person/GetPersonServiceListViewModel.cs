namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Services.Person
{
    public class GetPersonServiceListViewModel
    {
        public GetPersonServiceListViewModel()
        {
            PersonsData = new List<GetPersonServiceViewModel>();
        }
        public IList<GetPersonServiceViewModel> PersonsData { get; set; }
    }

    public class GetPersonServiceViewModel
    {
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
        public string PostalCode { get; set; }
        public string PostAddress { get; set; }
        public string IdentityIssueLocation { get; set; }
        public string AlphabetSeri { get; set; }
        public string HasSana { get; set; }
        public string MobileNo { get; set; }
        public string HasShahkar { get; set; }
        public string HasSabteAhval { get; set; }
        public string HasAmlakEskan { get; set; }
    }
}
