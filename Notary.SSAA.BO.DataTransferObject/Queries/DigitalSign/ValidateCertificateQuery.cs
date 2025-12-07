using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DigitalSign;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Queries.DigitalSign
{
    public class ValidateCertificateQuery : BaseQueryRequest<ApiResult<ValidateCertificateViewModel>>
    {
        public string Certificate { get; set; }        
    }
}
