using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;


namespace Notary.SSAA.BO.Domain.RepositoryObjects.Lookups
{
    public class EstateSctionLookupViewModel
    {
        public EstateSctionLookupViewModel()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<EstateSectionLookupItem> GridItems { get; set; }
        public List<EstateSectionLookupItem> SelectedItems { get; set; }
    }
    public class EstateSectionLookupItem : BaseLookupItem
    {
        public string SSAACode { get; set; }
        
    }
}
