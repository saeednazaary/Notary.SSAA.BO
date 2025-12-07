

namespace Notary.SSAA.BO.Domain.RepositoryObjects.Estate
{
    
    public class EstateInquirySpecialFields
    {
        public Guid Id { get; set; }
        public string State { get; set; }

        public string InquiryDate { get; set; }
        public string InquiryNo { get; set; }
        public string FirstSendDate { get; set; }
        public string LastSendDate { get; set; }
        public string ResponseDate { get; set; }
        public string TrtsReadDate { get; set; }

        public string ResponseResult { get; set; }

        public string ScriptoriumId { get; set; }
        public string UnitId { get; set; }
    }
}
