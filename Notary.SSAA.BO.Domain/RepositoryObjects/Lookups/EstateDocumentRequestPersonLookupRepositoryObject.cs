namespace Notary.SSAA.BO.Domain.RepositoryObjects.Lookups
{
    public class EstateDocumentRequestPersonLookupRepositoryObject
    {
        public EstateDocumentRequestPersonLookupRepositoryObject()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<EstateDocumentRequestPersonLookupItem> GridItems { get; set; }
        public List<EstateDocumentRequestPersonLookupItem> SelectedItems { get; set; }
    }

    public class EstateDocumentRequestPersonLookupItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string NationalNo { get; set; }
        public string IdentityNo { get; set; }
        public string BirthDate { get; set; }
        public string FatherName { get; set; }
        public bool IsSelected { get; set; }
    }
    
}
