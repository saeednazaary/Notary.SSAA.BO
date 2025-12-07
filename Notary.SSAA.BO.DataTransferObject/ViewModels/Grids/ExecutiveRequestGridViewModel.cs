using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Grids
{
    public class ExecutiveRequestGridViewModel
    {
        public ExecutiveRequestGridViewModel()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<ExecutiveRequestGridItem> GridItems { get; set; }
        public List<ExecutiveRequestGridItem> SelectedItems { get; set; }
    }
}
