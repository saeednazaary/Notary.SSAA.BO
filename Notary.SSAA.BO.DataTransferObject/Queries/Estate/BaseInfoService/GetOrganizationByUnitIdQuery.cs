
using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.BaseInfoService;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.Estate.BaseInfoService
{
    public class GetOrganizationByUnitIdQuery : BaseQueryRequest<ApiResult<OrganizationViewModel>>
    {
        public GetOrganizationByUnitIdQuery(string[] unitIdList)
        {
            UnitIdList = unitIdList;
        }
        public string[] UnitIdList { get; set; }
    }
}
