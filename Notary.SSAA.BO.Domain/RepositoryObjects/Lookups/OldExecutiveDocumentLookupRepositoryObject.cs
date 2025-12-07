using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;

namespace Notary.SSAA.BO.Domain.RepositoryObjects.Lookups
{
    public class OldExecutiveDocumentLookupRepositoryObject
    {
        public OldExecutiveDocumentLookupRepositoryObject()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<OldExecutiveDocumentLookupItem> GridItems { get; set; }
        public List<OldExecutiveDocumentLookupItem> SelectedItems { get; set; }
    }
    public sealed class OldExecutiveDocumentSearchExtraParams
    {
        public string DocumentTypeId { get; set; }
        public string State { get; set; }
    }


}
