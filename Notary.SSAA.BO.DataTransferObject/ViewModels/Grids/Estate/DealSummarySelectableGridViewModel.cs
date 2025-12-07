using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;


namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Grids.Estate
{
    public class DealSummarySelectableGridViewModel
    {
        public DealSummarySelectableGridViewModel()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<DealSummaryGridItem> GridItems { get; set; }
        public List<DealSummaryGridItem> SelectedItems { get; set; }
    }

   
}
