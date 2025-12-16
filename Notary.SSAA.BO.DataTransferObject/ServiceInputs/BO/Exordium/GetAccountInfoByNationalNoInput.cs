using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExordiumAccount;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.BO.Exordium
{
    public class GetAccountInfoByNationalNoInput : BaseExternalRequest<ApiResult<GetAccountInfoByNationalNoViewModel>>
    {
        public string NationalNo { get; set; }
    }
}
