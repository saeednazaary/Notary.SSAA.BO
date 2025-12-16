using static Stimulsoft.Base.Drawing.StiFontReader;

namespace Notary.SSAA.BO.Domain.RepositoryObjects.Lookups
{
    public class DocumentOwnerShipLookupRepositoryObject
    {
        public DocumentOwnerShipLookupRepositoryObject()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<DocumentOwnerShipLookupItem> GridItems { get; set; }
        public List<DocumentOwnerShipLookupItem> SelectedItems { get; set; }
    }

    public class DocumentOwnerShipLookupItem
    {
        public string Id { get; set; }
        public string OwnershipDocTitle { get; set; }
        public string OwnershipDocumentType { get; set; }
        public string FullName => Name + " " + Family;
        public string Name { get; set; }
        public string Family { get; set; }
        public string TfaState { get; set; }
        public string HasSmartCard { get; set; }
        public string TheONotaryRegServicePersonType { get; set; }
        public string IsOriginalPerson { get; set; }
        public string NationalNo { get; set; }

        public bool IsSelected { get; set; }
    }
    public class DocumentOwnerShipExtraParams
    {
        public string DocumentEstateId { get; set; }

    }


}
