using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices.Estate;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate
{
    public class GetEstateSeparationElementsInput : BaseExternalRequest<ApiResult<GetEstateSeparationElementsViewModel>>
    {
        public GetEstateSeparationElementsInput()
        {
            ClientId = "SSAR";
        }
        public string[] EstateInquiryId { get; set; }
        public string ClientId { get; set; }
    }
}
