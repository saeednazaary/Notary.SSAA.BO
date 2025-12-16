
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;

namespace Notary.SSAA.BO.Domain.RepositoryObjects.SignRequest
{
    public class SignRequestSubjectLookupRepositoryObject
    {
        public SignRequestSubjectLookupRepositoryObject()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<SignRequestSubjectLookupItem> GridItems { get; set; }
        public List<SignRequestSubjectLookupItem> SelectedItems { get; set; }
    }

    public class SignRequestSubjectLookupItem
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string SubjectGroupTitle { get; set; }
        public string Code { get; set; }
        public bool IsSelected { get; set; }
    }
}
