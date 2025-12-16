

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Notary.SSAA.BO.Domain.RepositoryObjects.Lookups
{
    public class DocumentPersonTypeLookupItem
    {
        public string Id { get; set; }
        public string SingularTitle { get; set; }
        public string Code { get; set; }
        public bool? IsRequired { get; set; }
        public bool? IsProhibitionCheckRequired { get; set; }
        public bool? IsOwner { get; set; }
        public bool? IsSanaRequired { get; set; }
        public bool? IsShahkarRequired { get; set; }
        public bool IsSelected { get; set; }
    }
}
