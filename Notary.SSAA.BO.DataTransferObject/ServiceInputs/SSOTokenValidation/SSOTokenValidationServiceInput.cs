

using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Result;


namespace SSAA.Notary.DataTransferObject.ServiceInputs.SSOTokenValidation
{
    public class SSOTokenValidationServiceInput : BaseExternalRequest<ApiResult<string>>
    {
        public string Token {  get; set; }
    }
}
