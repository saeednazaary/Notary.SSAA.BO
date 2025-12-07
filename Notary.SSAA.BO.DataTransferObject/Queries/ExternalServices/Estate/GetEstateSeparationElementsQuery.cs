using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices.Estate;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices.Estate
{
    public class GetEstateSeparationElementsQuery : BaseQueryRequest<ApiResult<EstateSeparationDocumentRelatedData>>
    {
        public GetEstateSeparationElementsQuery()
        {

        }
        public string[] EstateInquiryId { get; set; }

    }
}
