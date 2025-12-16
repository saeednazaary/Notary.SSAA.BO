using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices
{
    public class SabtAhvalPhotoListServiceQuery : BaseQueryRequest<ApiResult<SabtAhvalPhotoListServiceViewModel>>
    {
        public IList<string> NationalNos { get; set; }
    }
}
