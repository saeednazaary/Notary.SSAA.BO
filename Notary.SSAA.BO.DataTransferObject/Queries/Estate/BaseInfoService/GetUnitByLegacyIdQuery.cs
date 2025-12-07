using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.BaseInfoService;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Queries.Estate.BaseInfoService
{
    public class GetUnitByLegacyIdQuery : BaseQueryRequest<ApiResult<GetUnitByIdViewModel>>
    {
        public GetUnitByLegacyIdQuery(string[] legacyIdList)
        {
            LegacyIdList = legacyIdList;
        }
        public string[] LegacyIdList { get; set; }

    }
}
