using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.SignRequest
{
    public class SignRequestVerificationWithImportantAnnexTextQuery : BaseExternalQueryRequest<ExternalApiResult>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string SignRequestNationalNo { get; set; }
        public string SignRequestScriptoriumNo { get; set; }
        public string SignRequestSecretCode { get; set; }
        public string HasPermissionToPdf { get; set; }
    }
}
