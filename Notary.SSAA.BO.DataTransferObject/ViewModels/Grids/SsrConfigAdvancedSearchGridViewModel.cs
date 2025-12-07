
namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Grids
{
    public sealed class SsrConfigAdvancedSearchGridViewModel
    {
        public SsrConfigAdvancedSearchGridViewModel()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<SsrConfigGridItemViewModel> GridItems { get; set; }
        public List<SsrConfigGridItemViewModel> SelectedItems { get; set; }
    }

    public record SsrConfigGridItemViewModel
    {
        public string Id { get; set; }
        public string ConfigMainSubjectTitle { get; set; }
        public string ConfigSubjectTitle { get; set; }
        public string ConfigStartDate { get; set; }
        public string ConfigEndDate { get; set; }
        public bool IsSelected { get; set; }

    }
}
