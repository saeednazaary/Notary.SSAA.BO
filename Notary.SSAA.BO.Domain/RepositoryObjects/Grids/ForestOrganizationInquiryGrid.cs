namespace Notary.SSAA.BO.Domain.RepositoryObjects.Grids
{
    public class ForestOrganizationInquiryGrid
    {
        public ForestOrganizationInquiryGrid()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<ForestOrganizationInquiryGridItem> GridItems { get; set; }
        public List<ForestOrganizationInquiryGridItem> SelectedItems { get; set; }
    }
}
