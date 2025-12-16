namespace Notary.SSAA.BO.Domain.RepositoryObjects.Grids
{
    public class EstateTaxInquiryGrid
    {
        public EstateTaxInquiryGrid()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<EstateTaxInquiryGridItem> GridItems { get; set; }
        public List<EstateTaxInquiryGridItem> SelectedItems { get; set; }
    }
}
