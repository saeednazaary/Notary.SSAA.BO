
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Notary.SSAA.BO.Domain.RepositoryObjects.Estate
{
    public class DealSummaryPersonSpecialFields
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public string FatherName { get; set; }
        public string NationalityCode { get; set; }
        public string IdentityNo { get; set; }
        public string BirthDate { get; set; }
        public string ConditionText { get; set; }      
        public string OctantQuarter { get; set; }
        public string OctantQuarterPart { get; set; }
        public string OctantQuarterTotal { get; set; }        
        public string PersonType { get; set; }
        public string Seri { get; set; }        
        public string SerialNo { get; set; }       
        public string SexType { get; set; }       
        public decimal? SharePart { get; set; }       
        public string ShareText { get; set; }       
        public decimal? ShareTotal { get; set; }       
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public int? IssuePlaceId { get; set; }                   
        public int? BirthPlaceId { get; set; }
        public int? CityId { get; set; }
        public int? NationalityId { get; set; }
        public string RelationTypeId { get; set; }
        public string ScriptoriumId { get; set; }
        public string SeriAlpha { get; set; }        
        public Guid? RelatedPersonId { get; set; }
        public string IsInquiryPerson { get; set; }
    }
}
