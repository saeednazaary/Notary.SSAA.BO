using System.ComponentModel.DataAnnotations.Schema;
using Notary.SSAA.BO.DataTransferObject.Bases;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Document
{
    public class DocumentInquiryViewModel:EntityState
    {
       
           
            public string DocumentInquiryId { get; set; }
            public string EstateInquiriesId { get; set; }
        public string DocumentId { get; set; }
            public string[] DocumentInquiryOrganizationId { get; set; }
           
            public string RequestNo { get; set; }

            public string RequestDate { get; set; }

            public string RequestText { get; set; }

            public string ReplyNo { get; set; }

            public string ReplyDate { get; set; }

            public string ReplyType { get; set; }

            public string ReplyText { get; set; }

            public decimal? ReplyDetailQuota { get; set; }

            public decimal? ReplyTotalQuota { get; set; }

            public string ReplyQuotaText { get; set; }
           
            public string ExpireDate { get; set; }

            public string Conditions { get; set; }

            public string InquiryIssuer { get; set; }            
           
            public string State { get; set; }

           public string DocumentInquiryOrganizationTitle{ get; set; }


    }
}
