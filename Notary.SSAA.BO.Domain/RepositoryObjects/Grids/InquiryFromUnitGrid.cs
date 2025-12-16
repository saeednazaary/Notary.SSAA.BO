
namespace Notary.SSAA.BO.Domain.RepositoryObjects.Grids
{
    public class InquiryFromUnitGrid
    {
        public InquiryFromUnitGrid()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<InquiryFromUnitGridItem> GridItems { get; set; }
        public List<InquiryFromUnitGridItem> SelectedItems { get; set; }
    }

    public record InquiryFromUnitGridItem
    {
        public string Id { get; set; }
        public string InquiryNo { get; set; }
        public string InquiryDate { get; set; }
        public string InquiryTypeTitle { get; set; }
        public string UnitId { get; set; }
        public string State { get; set; }
        public bool IsSelected { get; set; }
    }
}
