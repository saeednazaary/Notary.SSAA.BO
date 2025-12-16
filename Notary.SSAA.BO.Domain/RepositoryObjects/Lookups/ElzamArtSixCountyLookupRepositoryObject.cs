namespace Notary.SSAA.BO.Domain.RepositoryObjects.Lookups
{
    public class ElzamArtSixCountyLookupRepositoryObject
    {
        public ElzamArtSixCountyLookupRepositoryObject()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<ElzamArtSixCountyLookupItem> GridItems { get; set; }
        public List<ElzamArtSixCountyLookupItem> SelectedItems { get; set; }
    }

    public class ElzamArtSixCountyLookupExtraParams
    {
        public string provinceId { get; set; }
    }
}