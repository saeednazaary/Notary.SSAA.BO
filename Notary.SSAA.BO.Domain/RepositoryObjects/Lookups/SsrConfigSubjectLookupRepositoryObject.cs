

namespace Notary.SSAA.BO.Domain.RepositoryObjects.Lookups
{
    public class SsrConfigSubjectLookupRepositoryObject
    {
        public SsrConfigSubjectLookupRepositoryObject()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<SsrConfigSubjectLookupObject> GridItems { get; set; }
        public List<SsrConfigSubjectLookupObject> SelectedItems { get; set; }
    }
    public class SsrConfigSubjectLookupObject
    {
            
        public string Id { get; set; }
        public string Title { get; set; }
        public string ConfigType { get; set; }
        public bool IsSelected { get; set; }
    }
}
