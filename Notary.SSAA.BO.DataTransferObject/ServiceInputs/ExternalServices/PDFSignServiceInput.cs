using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.ExternalServices;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices
{
    public class PDFSignServiceInput : BaseExternalRequest<ApiResult<PDFSignServiceViewModel>>
    {
        public string PdfFile { get; set; }
        public string ClientId { get; set; } = "SSAR";

    }
}
