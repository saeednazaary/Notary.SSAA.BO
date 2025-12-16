

using Notary.SSAA.BO.Domain.Entities;

namespace Notary.SSAA.BO.Domain.RepositoryObjects.Lookups
{
    public class DocumentLookupItem
    {
        public string Id { get; set; }
        public string RequestNo { get; set; }
        public string DocumentTypeTitle { get; set; }
        public string ClassifyNo { get; set; }
        public string NationalNo { get; set; }
        public string DocumentDate { get; set; }
        public string RequestDate { get; set; }
        public string WriteInBookDate { get; set; }
        public string ScriptorumTitle { get; set; }
        public string DocumentPersons { get; set; }
        public string StateId { get; set; }
        public string DocumentCases { get; set; }
        public List<string> DocumentPersonList { get; set; }
        public List<string> DocumentCaseList { get; set; }

        public bool IsSelected { get; set; }
    }
}
