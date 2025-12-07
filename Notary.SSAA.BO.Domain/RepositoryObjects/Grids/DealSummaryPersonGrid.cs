

namespace Notary.SSAA.BO.Domain.RepositoryObjects.Grids
{
    public class DealSummaryPersonGrid
    {
        public DealSummaryPersonGrid()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<DealSummaryPersonGridItem> GridItems { get; set; }
        public List<DealSummaryPersonGridItem> SelectedItems { get; set; }
    }
}
