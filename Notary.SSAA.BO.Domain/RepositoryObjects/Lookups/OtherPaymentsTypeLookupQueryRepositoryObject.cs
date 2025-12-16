namespace Notary.SSAA.BO.Domain.RepositoryObjects.Lookups
{
    public class OtherPaymentsTypeLookupQueryRepositoryObject
    {
        public OtherPaymentsTypeLookupQueryRepositoryObject()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<OtherPaymentsTypeLookupItem> GridItems { get; set; }
        public List<OtherPaymentsTypeLookupItem> SelectedItems { get; set; }
    }

    public class OtherPaymentsTypeLookupItem
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string Fee { get; set; }
        public bool IsSelected { get; set; }
    }
}
