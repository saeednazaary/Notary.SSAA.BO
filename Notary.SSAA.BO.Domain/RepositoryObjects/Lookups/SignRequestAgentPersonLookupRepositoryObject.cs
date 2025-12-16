namespace Notary.SSAA.BO.Domain.RepositoryObjects.Lookups
{
    public class SignRequestAgentPersonLookupRepositoryObject
    {
        public SignRequestAgentPersonLookupRepositoryObject()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<SignRequestAgentPersonLookupItem> GridItems { get; set; }
        public List<SignRequestAgentPersonLookupItem> SelectedItems { get; set; }
    }

    public class SignRequestAgentPersonLookupItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string NationalNo { get; set; }
        public string FullName  => Name + " " + Family;
        public bool IsSelected { get; set; }
    }
    public class SignRequestAgentPersonExtraParams
    {
        public string SignRequestId { get; set; }
        public string PersonId { get; set; }
    }


}
