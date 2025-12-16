using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;

namespace Notary.SSAA.BO.Domain.RepositoryObjects.Lookups
{
    public class DocumentLookupRepositoryObject
    {
        public DocumentLookupRepositoryObject()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<DocumentLookupItem> GridItems { get; set; }
        public List<DocumentLookupItem> SelectedItems { get; set; }
    }
    public sealed class DocumentSearchExtraParams
    {
        public string DocumentTypeId { get; set; }
    }


}
