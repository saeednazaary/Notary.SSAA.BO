using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.SignRequest
{
    public class SignRequestElectronicBookPageQuery : BaseQueryRequest<ApiResult<SignRequestElectronicBookPageViewModel>>
    {
        public int PageNumber { get; set; }
        public string SignRequestNationalNo { get; set; }
        public string PersonSignClassifyNo { get; set; }
    }
}
