namespace Notary.SSAA.BO.Domain.RepositoryObjects.Lookups
{
    public sealed class SignRequestDocumentTemplateLookupExtraParams
    {
        public string StateId { get; set; }
        public string DocumentTypeId { get; set; }
    }

    public class SignRequestDocumentTemplateRepositoryObject
    {
        public SignRequestDocumentTemplateRepositoryObject()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<SignRequestDocumentTemplateLookupItem> GridItems { get; set; }
        public List<SignRequestDocumentTemplateLookupItem> SelectedItems { get; set; }
    }
}
