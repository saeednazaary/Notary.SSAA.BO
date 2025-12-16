

namespace Notary.SSAA.BO.Domain.RepositoryObjects.Grids
{
    public class SsrConfigAdvancedSearchGrid
    {
        public SsrConfigAdvancedSearchGrid()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<SsrConfigGridItem> GridItems { get; set; }
        public List<SsrConfigGridItem> SelectedItems { get; set; }
    }

    public record SsrConfigGridItem
    {
        public string Id { get; set; }
        public string ConfigMainSubjectTitle { get; set; }
        public string ConfigSubjectTitle { get; set; }
        public Guid ConfigSubjectId { get; set; }
        public string ConfigStartDate { get; set; }
        public string ConfigEndDate { get; set; }
        public string ConfigState { get; set; }
        public bool IsDisabled { get; set; }
        public string ConfigEndTime { get; set; }
        public string ConfigStartTime { get; set; }
        public string Confirmer { get; set; }
        public string ConditionType { get; set; }
    }

    public class SsrConfigGridExtraParams
    {

    }

}
