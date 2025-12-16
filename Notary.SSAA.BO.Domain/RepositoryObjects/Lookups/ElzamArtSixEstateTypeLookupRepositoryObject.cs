namespace Notary.SSAA.BO.Domain.RepositoryObjects.Lookups
{
    public class ElzamArtSixEstateTypeLookupRepositoryObject
    {
        public ElzamArtSixEstateTypeLookupRepositoryObject()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<ElzamArtSixEstateTypeLookupItem> GridItems { get; set; }
        public List<ElzamArtSixEstateTypeLookupItem> SelectedItems { get; set; }
    }

    public class ElzamArtSixEstateTypeLookupExtraParams
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string State { get; set; }
    }
}