using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.Kateb
{
    public class DocumentRequestPaymentInformationServiceInput : BaseExternalRequest<ApiResult>
    {
        public DocumentRequestPaymentInformationServiceInput(string id)
        {
                Id = id;
        }
        public string Id { get; set; }

    }
}
