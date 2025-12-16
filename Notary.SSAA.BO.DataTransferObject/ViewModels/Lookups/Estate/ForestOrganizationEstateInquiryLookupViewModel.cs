using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;


namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Lookups.Estate
{
    public class ForestOrganizationEstateInquiryLookupViewModel
    {
        public ForestOrganizationEstateInquiryLookupViewModel()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<EstateInquiryGridItem> GridItems { get; set; }
        public List<EstateInquiryGridItem> SelectedItems { get; set; }
    }
}
