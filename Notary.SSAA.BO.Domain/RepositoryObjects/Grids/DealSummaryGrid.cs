

namespace Notary.SSAA.BO.Domain.RepositoryObjects.Grids
{
    public class DealSummaryGrid
    {
        public DealSummaryGrid()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<DealSummaryGridItem> GridItems { get; set; }
        public List<DealSummaryGridItem> SelectedItems { get; set; }
    }
    public class RestrictionDealSummaryListViewModel
    {
        public RestrictionDealSummaryListViewModel()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<RestrictionDealSummaryListItem> GridItems { get; set; }
        public List<RestrictionDealSummaryListItem> SelectedItems { get; set; }
    }
}
