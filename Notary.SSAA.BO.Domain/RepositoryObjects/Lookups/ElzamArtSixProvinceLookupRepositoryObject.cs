namespace Notary.SSAA.BO.Domain.RepositoryObjects.Lookups
{
    public class ElzamArtSixProvinceLookupRepositoryObject
    {
        public ElzamArtSixProvinceLookupRepositoryObject()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<ElzamArtSixProvinceLookupItem> GridItems { get; set; }
        public List<ElzamArtSixProvinceLookupItem> SelectedItems { get; set; }
    }

    public class ElzamArtSixProvinceLookupExtraParams
    {public string Id { get; set; }
public string Code { get; set; }
public string Title { get; set; }
public string State { get; set; }
public string Nid { get; set; }
 }
    }