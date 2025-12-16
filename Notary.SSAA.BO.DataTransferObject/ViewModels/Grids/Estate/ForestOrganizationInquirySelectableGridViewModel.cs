using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Grids.Estate
{
    public class ForestOrganizationInquirySelectableGridViewModel
    {
        public ForestOrganizationInquirySelectableGridViewModel()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<ForestOrganizationInquiryGridItem> GridItems { get; set; }
        public List<ForestOrganizationInquiryGridItem> SelectedItems { get; set; }
    }
}
