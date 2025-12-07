using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry;
using Notary.SSAA.BO.Domain.RepositoryObjects.Estate;
using Notary.SSAA.BO.SharedKernel.Result;
using System.Text.Json.Serialization;

namespace Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateInquiry
{
    public class ValidateEstateInquiryItemsQuery : BaseQueryRequest<ApiResult>
    {
        public ValidateEstateInquiryItemsQuery()
        {
            
        }
        public string InquiryDate { get; set; }
        public string InquiryNo { get; set; }
        public string InquiryId { get; set; }
        public string ScriptoriumId { get; set; }
        public string UnitId { get; set; }
        public string PageNumber { get; set; }
        public string NoteBookNo { get; set; }
        public string SeriDaftarId { get; set; }
        public string PersonMelliCode { get; set; }
        public string Basic { get; set; }
        public string Secondary { get; set; }     
        public bool IsExecuteTransfer { get; set; }
        public string PersonName { get; set; }
        public string PersonFamily { get; set; }
        public string DocPrintNo { get; set; }
        public string FollowedInquiryId { get; set; }
        
    }
}
