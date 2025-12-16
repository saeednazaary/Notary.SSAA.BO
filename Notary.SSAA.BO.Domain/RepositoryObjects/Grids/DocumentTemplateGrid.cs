namespace Notary.SSAA.BO.Domain.RepositoryObjects.Grids
{
    public sealed class DocumentTemplateGrid
    {
        public DocumentTemplateGrid()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<DocumentTemplateGridItem> GridItems { get; set; }
        public List<DocumentTemplateGridItem> SelectedItems { get; set; }
    }

    public class DocumentTemplateGridItem
    {
        public string DocumentTemplateId { get; set; }
        public string DocumentTemplateCode { get; set; }
        public string DocumentTypeTitle { get; set; }
        public string DocumentTypeId { get; set; }
        public string DocumentTemplateTitle { get; set; }
        public string DocumentTemplateStateId { get; set; }
        public string DocumentTemplateStateTitle { get; set; }
        public string DocumentTemplateCreateDate { get; set; }
        public bool IsSelected { get; set; }
    }

    public class DocumentTemplateExtraParams
    {
        public string StateId { get; set; }
        public string DocumentTypeId { get; set; }
    }
}
