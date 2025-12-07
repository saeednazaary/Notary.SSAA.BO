using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Grids
{
    public class ExecutiveSupportGridViewModel
    {
        public ExecutiveSupportGridViewModel()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<ExecutiveSupportiGridItem> SelectedItems { get; set; }
        public List<ExecutiveSupportiGridItem> GridItems { get; set; }
    }
    
}
