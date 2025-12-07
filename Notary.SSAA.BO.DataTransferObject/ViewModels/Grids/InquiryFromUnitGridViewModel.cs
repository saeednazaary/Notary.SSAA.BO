namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Grids
{
    public class InquiryFromUnitGridViewModel
    {
        public InquiryFromUnitGridViewModel()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<InquiryFromUnitViewModeltem> GridItems { get; set; }
        public List<InquiryFromUnitViewModeltem> SelectedItems { get; set; }
    }

    public record InquiryFromUnitViewModeltem
    {
        public string Id { get; set; }
        public string InquiryNo { get; set; }
        public string InquiryDate { get; set; }
        public string InquiryTypeTitle { get; set; }
        public string UnitTitle { get; set; }
        public string StateTitle { get; set; }
        public bool IsSelected { get; set; }
    }
}
