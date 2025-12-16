
namespace Notary.SSAA.BO.Domain.RepositoryObjects.Grids
{
    public class SignRequestAdvancedSearch
    {
        public SignRequestAdvancedSearch()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<SignRequestGridItem> GridItems { get; set; }
        public List<SignRequestGridItem> SelectedItems { get; set; }
    }

    public class SignRequestSearchExtraParams
    {
        public string SignRequestReqNo { get; set; }
        public string SignRequestNationalNo { get; set; }
        public string SignRequestStateId { get; set; }
        public string SignRequestReqDateFrom { get; set; }
        public string SignRequestReqDateTo { get; set; }
        public string SignRequestSignDateFrom { get; set; }
        public string SignRequestSignDateTo { get; set; }
        public string PersonNationalNo { get; set; }
        public string PersonName { get; set; }
        public string PersonFamily { get; set; }
        public string PersonFatherName { get; set; }
        public string PersonIdentityNo { get; set; }
        public string PersonSignClassifyNo { get; set; }
        public string PersonBirthDate { get; set; }
        public string PersonSeri { get; set; }
        public string PersonSerial { get; set; }
        public string PersonPostalCode { get; set; }
        public string PersonMobileNo { get; set; }
        public string PersonAddress { get; set; }
        public string PersonTel { get; set; }
        public string PersonSexType { get; set; }
        public bool? IsPersonOriginal { get; set; }
        public bool? IsPersonIranian { get; set; }
        public bool? IsPersonRelated { get; set; }
        public string PersonAlphabetSeri { get; set; }
    }
    public class SignRequestAdminSearchExtraParams
    {
        public string SignRequestReqNo { get; set; }
        public string SignRequestScriptoriumCode { get; set; }
        public string SignRequestNationalNo { get; set; }
      
    }
}
