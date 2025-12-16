
namespace Notary.SSAA.BO.Domain.RepositoryObjects.Grids
{
    public sealed class DocumentCaseGrid
    {
        public DocumentCaseGrid()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<DocumentCaseGridItem> GridItems { get; set; }
        public List<DocumentCaseGridItem> SelectedItems { get; set; }
    }
    public class DocumentCaseGridItem
    {
        public string Id { get; set; }
        public string DocumentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string scriptorumId { get; set; }
        public bool IsSelected { get; set; }
    }
    public class DocumentCaseExtraParams
    {

    }
}
