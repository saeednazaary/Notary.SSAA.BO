namespace Notary.SSAA.BO.Domain.RepositoryObjects.Bases
{
    public class BaseLookupRepositoryObject
    {
        public BaseLookupRepositoryObject()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<BaseLookupItem> GridItems { get; set; }
        public List<BaseLookupItem> SelectedItems { get; set; }
    }
}
