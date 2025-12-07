namespace Notary.SSAA.BO.Domain.RepositoryObjects.Lookups
{
    public class DocumentDetailPersonLookupRepositoryObject
    {
        public DocumentDetailPersonLookupRepositoryObject()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<DocumentDetailPersonLookupItem> GridItems { get; set; }
        public List<DocumentDetailPersonLookupItem> SelectedItems { get; set; }
    }

    public class DocumentDetailPersonLookupItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string NationalNo { get; set; }
        public string FullName => Name + " " + Family;
        public string DocumentPersonTypeTitle { get; set; }
        public string DocumentPersonTypeId { get; set; }
        public bool IsSelected { get; set; }
    }
    public class DocumentDetailPersonExteraParams
    {
        public string DocumentId { get; set; }
        public string DocumentPersonTypeId { get; set; }
        public string IsOwner { get; set; }
        public string IsPersonOriginal { get; set; }
        public string IsPersonAlive { get; set; }
        public List<string>? DontIncludePeopleId { get; set; }

    }


}
