

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Grids
{
    public sealed class DocumentCaseGridViewModel
    {
        public DocumentCaseGridViewModel()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<DocumentCaseGridItemViewModel> GridItems { get; set; }
        public List<DocumentCaseGridItemViewModel> SelectedItems { get; set; }
    }

    public record  DocumentCaseGridItemViewModel
    {
        public string Id { get; set; }
        public string DocumentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string scriptorumId { get; set; }
        public bool IsSelected { get; set; }
    }
}
