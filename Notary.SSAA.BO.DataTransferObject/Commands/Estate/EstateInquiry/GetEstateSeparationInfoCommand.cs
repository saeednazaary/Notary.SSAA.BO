using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Commands.Estate.EstateInquiry
{
    public class GetEstateSeparationInfoCommand : BaseCommandRequest<ApiResult<EstateInquiryViewModel>>
    {
        public GetEstateSeparationInfoCommand(string estateInquiryId)
        {
            EstateInquiryId = estateInquiryId;
            ReturnEstateInquiry = true;
        }
        public string EstateInquiryId { get; set; }
        public bool ReturnEstateInquiry { get; set; } 

    }
}
