using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;


namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Grids.Estate
{
    public class EstateInquirySelectableGridViewModel
    {
        public EstateInquirySelectableGridViewModel()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<EstateInquiryGridItem> GridItems { get; set; }
        public List<EstateInquiryGridItem> SelectedItems { get; set; }
    }
}
