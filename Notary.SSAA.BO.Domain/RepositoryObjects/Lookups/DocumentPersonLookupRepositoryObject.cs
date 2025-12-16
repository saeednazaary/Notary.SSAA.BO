namespace Notary.SSAA.BO.Domain.RepositoryObjects.Lookups
{
    public class DocumentPersonLookupRepositoryObject
    {
        public DocumentPersonLookupRepositoryObject()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<DocumentPersonLookupItem> GridItems { get; set; }
        public List<DocumentPersonLookupItem> SelectedItems { get; set; }
    }

    public class DocumentPersonLookupItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string FullName => Name + " " + Family;
        public string NationalNo { get; set; }
        public bool IsSelected { get; set; }
    }
    public class DocumentPersonExteraParams
    {
        public string DocumentId { get; set; }
        public string PersonId { get; set; }
        public bool IsLoadAllPerson { get; set; }
    }


}
