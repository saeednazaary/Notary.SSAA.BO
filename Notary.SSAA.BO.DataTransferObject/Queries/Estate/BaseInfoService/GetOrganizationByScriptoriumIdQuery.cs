
using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.BaseInfoService;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.Estate.BaseInfoService
{
    public class GetOrganizationByScriptoriumIdQuery : BaseQueryRequest<ApiResult<OrganizationViewModel>>
    {
        public GetOrganizationByScriptoriumIdQuery(string[] scriptoriumIdList)
        {
            ScriptoriumIdList = scriptoriumIdList;
        }
        public string[] ScriptoriumIdList { get; set; }
    }
}
