using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Commands.Document
{
    public class UpdateDocumentPaymentStateCommand : BaseCommandRequest<ApiResult<UpdateDocumentPaymentStateViewModel>>
    {
        public string DocumentNo { get; set; }
        public bool InquiryMode { get; set; }

    }
}
