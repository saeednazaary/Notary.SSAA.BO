using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.BaseInfoService;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Queries.Estate.BaseInfoService
{
    public class GetGeolocationByIdQuery : BaseQueryRequest<ApiResult<GetGeolocationByIdViewModel>>
    {
        public GetGeolocationByIdQuery(int[] idList)
        {
            IdList = idList;
        }
        public int[] IdList { get; set; }
        public bool FetchGeolocationOfIran { get; set; }

    }
}
