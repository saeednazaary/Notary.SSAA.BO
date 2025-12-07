using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DigitalSign;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Queries.DigitalSign
{
    public class GetSignValueQuery : BaseQueryRequest<ApiResult<GetSignValueViewModel>>
    {        
        public string Id { get; set; }
    }
    public class GetSignValueListQuery : BaseQueryRequest<ApiResult<List<GetSignValueViewModel>>>
    {
        public List<string> IdList { get; set; }
    }

}
