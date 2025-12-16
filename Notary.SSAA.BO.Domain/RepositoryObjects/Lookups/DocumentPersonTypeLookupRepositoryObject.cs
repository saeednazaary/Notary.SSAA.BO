using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;

namespace Notary.SSAA.BO.Domain.RepositoryObjects.Lookups
{
    public class DocumentPersonTypeLookupRepositoryObject
    {
        public DocumentPersonTypeLookupRepositoryObject()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<DocumentPersonTypeLookupItem> GridItems { get; set; }
        public List<DocumentPersonTypeLookupItem> SelectedItems { get; set; }
    }

}
