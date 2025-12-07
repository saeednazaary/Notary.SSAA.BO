using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;

namespace Notary.SSAA.BO.Domain.RepositoryObjects.Lookups
{
    public class DocumentAgentTypeLookupRepositoryObject
    {
        public DocumentAgentTypeLookupRepositoryObject()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<BaseLookupItem> GridItems { get; set; }
        public List<BaseLookupItem> SelectedItems { get; set; }
    }

}
