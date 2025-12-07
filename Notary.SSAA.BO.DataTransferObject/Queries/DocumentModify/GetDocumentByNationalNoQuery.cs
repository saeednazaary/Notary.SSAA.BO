using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentModify;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Queries.DocumentModify
{
    public class GetDocumentByNationalNoQuery : BaseQueryRequest<ApiResult<DocumentModifyViewModel>>
    {
        public GetDocumentByNationalNoQuery(string nationalNo)
        {
            NationalNo = nationalNo;
        }
        public string NationalNo { get; set; }
    }
}
