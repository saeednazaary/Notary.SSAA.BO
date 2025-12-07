

namespace Notary.SSAA.BO.Domain.RepositoryObjects.Grids
{
    public class DealSummaryPersonGridItem
    {
        public string PersonId { get; set; }
        public string PersonRelationType { get; set; }
        public string PersonName { get; set; } = null!;
        public string PersonConditionText { get; set; }
        public string PersonOctantQuarter { get; set; }
        public string PersonOctantQuarterPart { get; set; }
        public string PersonOctantQuarterTotal { get; set; } = null!;
        public string PersonFatherName { get; set; }
        public string PersonIdentityNo { get; set; }
        public string PersonBirthDate { get; set; }
        public string PersonNationalityCode { get; set; }
        public string PersonSeri { get; set; }
        public string PersonSerialNo { get; set; }
        public string PersonSeriAlpha { get; set; }
        public decimal? PersonSharePart { get; set; }
        public string PersonShareText { get; set; }
        public decimal? PersonShareTotal { get; set; }
        public string PersonIssuePlace { get; set; }
        public string PersonSexType { get; set; }
        public string PersonType { get; set; }
        public string PersonPostalCode { get; set; } = null!;
        public string PersonAddress { get; set; } = null!;
        public bool PersonExecutiveTransfer { get; set; }
        public string PersonNationality { get; set; }
        public string PersonBirthPlace { get; set; }
        public string PersonCity { get; set; }
        public decimal PersonTimestamp { get; set; }
        public string PersonIlm { get; set; }
        public string PersonImage { get; set; }
    }
}
