using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;

namespace Notary.SSAA.BO.Domain.RepositoryObjects.Lookups
{

    public sealed class DocumentTypeExtraParams
    {
        public DocumentTypeExtraParams()
        {
            IsSupportive = false;
        }
        public string DocumentTypeGroupOneId { get; set; }
        public string DocumentTypeGroupTwoId { get; set; }
        public bool?  IsSupportive { get; set; }
    }
    public class DocumentTypeRepositoryObject
    {
        public DocumentTypeRepositoryObject()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<DocumentTypeLookupItem> GridItems { get; set; }
        public List<DocumentTypeLookupItem> SelectedItems { get; set; }
    }
}
