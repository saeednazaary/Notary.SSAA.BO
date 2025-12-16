using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.BaseInfoService;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.Estate.BaseInfoService
{
    public class GetOrganizationByIdQuery : BaseQueryRequest<ApiResult<OrganizationViewModel>>
    {
        public GetOrganizationByIdQuery(string[] organizationIdList)
        {
            OrganizationIdList = organizationIdList;
        }
        public string[] OrganizationIdList { get; set; }
    }
}

