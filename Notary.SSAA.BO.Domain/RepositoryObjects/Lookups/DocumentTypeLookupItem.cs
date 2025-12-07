

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Notary.SSAA.BO.Domain.RepositoryObjects.Lookups
{
    public class DocumentTypeLookupItem
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public bool? IsSupportive { get; set; }
        public string State { get; set; }
        public string DocumentTypeGroupOneId { get; set; }
        public string DocumentTypeGroupTwoId { get; set; }
        public bool? EstateInquiryIsRequired { get; set; }
        public bool? HasEstateInquiry { get; set; }
        public bool? WealthType { get; set; }
        public bool? SubjectIsRequired { get; set; }
        public bool? HasSubject { get; set; }
        public bool? HasAssetType { get; set; }
        public bool? AssetTypeIsRequired { get; set; }
        public bool IsSelected { get; set; }

    }
}
