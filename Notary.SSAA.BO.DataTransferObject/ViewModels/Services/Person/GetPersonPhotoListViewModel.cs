namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Services.Person
{
    public class GetPersonPhotoListViewModel
    {
        public GetPersonPhotoListViewModel()
        {
            PersonsData = new List<PersonPhotoServiceViewModel>();
        }
        public IList<PersonPhotoServiceViewModel> PersonsData { get; set; }
    }

    public class PersonPhotoServiceViewModel
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public string NationalNo { get; set; }
        public byte[] PersonalImage { get; set; }
        public string IdentityNo { get; set; }
        public bool IsAlive { get; set; }
        public string Seri { get; set; }
        public string SeriAlpha { get; set; }
        public string SexType { get; set; }
        public string IdentityIssueLocation { get; set; }
        public string Serial { get; set; }
        public string BirthDate { get; set; }
        public string FatherName { get; set; }
    }
}
