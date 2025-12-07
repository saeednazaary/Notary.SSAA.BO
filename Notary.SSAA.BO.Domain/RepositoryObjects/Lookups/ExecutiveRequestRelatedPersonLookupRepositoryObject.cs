namespace Notary.SSAA.BO.Domain.RepositoryObjects.Lookups
{
    public class ExecutiveRequestRelatedPersonLookupRepositoryObject
    {
        public ExecutiveRequestRelatedPersonLookupRepositoryObject()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<ExecutiveRequestRelatedPersonSelectableLookupItem> GridItems { get; set; }
        public List<ExecutiveRequestRelatedPersonSelectableLookupItem> SelectedItems { get; set; }
    }
    public class ExecutiveRequestRelatedPersonSelectableLookupItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public bool IsSelected { get; set; }
    }
}
