using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;


namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Lookups.Estate
{
    public class EstateFollowedInquiryLookupViewModel
    {
        public EstateFollowedInquiryLookupViewModel()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<EstateInquiryGridItem> GridItems { get; set; }
        public List<EstateInquiryGridItem> SelectedItems { get; set; }
    }
}
