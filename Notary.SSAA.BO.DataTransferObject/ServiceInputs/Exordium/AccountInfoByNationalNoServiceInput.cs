using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.Exordium;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.Exordium
{
    public class AccountInfoByNationalNoServiceInput : BaseExternalRequest<ApiResult<AccountInfoByNationalNoViewModel>>
    {
        public string NationalNo { get; set; }
    }
}
