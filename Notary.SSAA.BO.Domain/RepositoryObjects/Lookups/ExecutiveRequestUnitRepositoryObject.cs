namespace Notary.SSAA.BO.Domain.RepositoryObjects.Lookups
{
    public class ExecutiveRequestUnitRepositoryObject
    {
        public ExecutiveRequestUnitRepositoryObject()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<ExecutiveRequestUnitSelectableLookupItem> GridItems { get; set; }
        public List<ExecutiveRequestUnitSelectableLookupItem> SelectedItems { get; set; }
    }

    public class ExecutiveRequestUnitSelectableLookupItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsSelected { get; set; }
    }
}
