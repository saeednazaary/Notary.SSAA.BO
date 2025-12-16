using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Queries.Estate
{
    public class ParseConfigValueQuery : BaseQueryRequest<ApiResult<ParseConfigValueViewModel>>
    {
        public ParseConfigValueQuery()
        {
            
        }       
        public string Value { get; set; }

    }
}
