namespace Notary.SSAA.BO.Domain.RepositoryObjects.Lookups
{
    public class ElzamArtSixEstateUsingLookupRepositoryObject
    {
        public ElzamArtSixEstateUsingLookupRepositoryObject()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<ElzamArtSixEstateUsingLookupItem> GridItems { get; set; }
        public List<ElzamArtSixEstateUsingLookupItem> SelectedItems { get; set; }
    }

    public class ElzamArtSixEstateUsingLookupExtraParams
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string State { get; set; }
    }
}