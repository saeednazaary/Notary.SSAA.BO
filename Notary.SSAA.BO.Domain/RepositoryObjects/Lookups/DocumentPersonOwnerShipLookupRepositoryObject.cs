namespace Notary.SSAA.BO.Domain.RepositoryObjects.Lookups
{
    public class DocumentPersonOwnerShipLookupRepositoryObject
    {
        public DocumentPersonOwnerShipLookupRepositoryObject()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<DocumentPersonOwnerShipLookupItem> GridItems { get; set; }
        public List<DocumentPersonOwnerShipLookupItem> SelectedItems { get; set; }
    }

    public class DocumentPersonOwnerShipLookupItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string NationalNo { get; set; }
        public string HasSmartCard { get; set; }
        public string IsOriginalPerson { get; set; }
        public string TfaState { get; set; }
        public string TheONotaryRegServicePersonType { get; set; }
        public string FullName => Name + " " + Family;
        public string State { get; set; }
        public string ProhibitionCheckingRequired { get; set; }
        public string IsRequired { get; set; }
        public bool IsSelected { get; set; }
    }
    public class DocumentPersonOwnerShipExtraParams
    {
        public string DocumentId { get; set; }

    }


}
