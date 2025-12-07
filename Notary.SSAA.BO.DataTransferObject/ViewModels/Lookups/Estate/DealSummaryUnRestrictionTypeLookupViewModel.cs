using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;


namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Lookups.Estate
{
    public class DealSummaryUnRestrictionTypeLookupViewModel
    {
        public DealSummaryUnRestrictionTypeLookupViewModel()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<BaseLookupItem> GridItems { get; set; }
        public List<BaseLookupItem> SelectedItems { get; set; }
    }
}
