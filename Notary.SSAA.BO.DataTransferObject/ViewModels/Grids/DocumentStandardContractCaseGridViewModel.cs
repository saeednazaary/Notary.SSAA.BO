

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Grids
{
    public sealed class DocumentStandardContractCaseGridViewModel
    {
        public DocumentStandardContractCaseGridViewModel()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<DocumentStandardContractCaseGridItemViewModel> GridItems { get; set; }
        public List<DocumentStandardContractCaseGridItemViewModel> SelectedItems { get; set; }
    }

    public record DocumentStandardContractCaseGridItemViewModel
    {
        public string Id { get; set; }
        public string DocumentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string scriptorumId { get; set; }
        public bool IsSelected { get; set; }
    }
}
