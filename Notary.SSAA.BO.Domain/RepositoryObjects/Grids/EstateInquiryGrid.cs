
namespace Notary.SSAA.BO.Domain.RepositoryObjects.Grids
{
    public class EstateInquiryGrid
    {
        public EstateInquiryGrid()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<EstateInquiryGridItem> GridItems { get; set; }
        public List<EstateInquiryGridItem> SelectedItems { get; set; }
    }

    public class EstateInquiryGrid2
    {
        public EstateInquiryGrid2()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<EstateInquiryGridItem2> GridItems { get; set; }
        public List<EstateInquiryGridItem2> SelectedItems { get; set; }
    }
}
