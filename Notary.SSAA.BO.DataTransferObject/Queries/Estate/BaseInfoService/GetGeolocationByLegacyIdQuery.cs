
using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.BaseInfoService;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.Estate.BaseInfoService
{
    public class GetGeolocationByLegacyIdQuery : BaseQueryRequest<ApiResult<GetGeolocationByIdViewModel>>
    {
        public GetGeolocationByLegacyIdQuery(string[] idList)
        {
            IdList = idList;            
        }
        public string[] IdList { get; set; }        
    }
}
