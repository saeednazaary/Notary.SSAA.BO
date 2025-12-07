using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;

namespace Notary.SSAA.BO.Domain.RepositoryObjects.Lookups
{
    public class EstateSubSectionLookupViewModel
    {
        public EstateSubSectionLookupViewModel()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<EstateSubSectionLookupItem> GridItems { get; set; }
        public List<EstateSubSectionLookupItem> SelectedItems { get; set; }
    }
    public class EstateSubSectionLookupItem : BaseLookupItem
    {
        public string SSAACode { get; set; }

    }
}
