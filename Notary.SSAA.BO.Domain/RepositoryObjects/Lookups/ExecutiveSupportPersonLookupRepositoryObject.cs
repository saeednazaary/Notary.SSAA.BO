namespace Notary.SSAA.BO.Domain.RepositoryObjects.Lookups
{
    public class ExecutiveSupportPersonLookupRepositoryObject
    {
        public ExecutiveSupportPersonLookupRepositoryObject()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<ExecutiveSupportPersonLookupItem> GridItems { get; set; }
        public List<ExecutiveSupportPersonLookupItem> SelectedItems { get; set; }
    }
    public class ExecutiveSupportPersonLookupItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public bool IsSelected { get; set; }
    }
}
