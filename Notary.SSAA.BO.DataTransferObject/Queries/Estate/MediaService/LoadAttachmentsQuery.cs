using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Queries.Estate.MediaService
{
    public class LoadAttachmentsQuery : BaseQueryRequest<ApiResult<object>>
    {
        public List<string> RelatedRecordIds { get; set; }
        public string ClientId { get; set; }
    }
}
