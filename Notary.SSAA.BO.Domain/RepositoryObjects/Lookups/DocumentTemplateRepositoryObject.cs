namespace Notary.SSAA.BO.Domain.RepositoryObjects.Lookups
{
    public sealed class DocumentTemplateLookupExtraParams
    {
        public string StateId { get; set; }
        public string DocumentTypeId { get; set; }
    }

    public class DocumentTemplateRepositoryObject
    {
        public DocumentTemplateRepositoryObject()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<DocumentTemplateLookupItem> GridItems { get; set; }
        public List<DocumentTemplateLookupItem> SelectedItems { get; set; }
    }
}
