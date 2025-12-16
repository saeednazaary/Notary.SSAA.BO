using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices
{
    public class IsMatchedByILENCServiceQuery : BaseQueryRequest<ApiResult<ILENCServiceViewModel>>
    {
        public string NationalNo { get; set; }
        public string Name { get; set; }
        public string RegisterNo { get; set; }
        public string RegisterDate { get; set; }

    }
}
