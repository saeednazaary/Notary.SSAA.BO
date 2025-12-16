using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DigitalSign;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.Document
{
    public class GetElectronicBookDataToSignQuery : BaseQueryRequest<ApiResult<SignDataViewModel>>
    {
        public GetElectronicBookDataToSignQuery(string documentId/*, string enterBookDateTime*/)
        {
            DocumentId = documentId;
            // EnterBookDateTime = enterBookDateTime;
        }

        public string DocumentId { get; set; }
        ///public string EnterBookDateTime { get; set; }

    }
}