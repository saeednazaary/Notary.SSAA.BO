using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.BaseInfoService;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Queries.Estate.BaseInfoService
{
    public class GetScriptoriumByIdQuery : BaseQueryRequest<ApiResult<GetScriptoriumByIdViewModel>>
    {
        public GetScriptoriumByIdQuery(string[] idList)
        {
            IdList = idList;
        }
        public string[] IdList { get; set; }

    }
}
