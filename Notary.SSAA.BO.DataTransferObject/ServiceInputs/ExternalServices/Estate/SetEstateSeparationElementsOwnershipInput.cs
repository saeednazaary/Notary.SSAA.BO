using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices.Estate;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices.Estate;
using Notary.SSAA.BO.SharedKernel.Result;



namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate
{
    public class SetEstateSeparationElementsOwnershipInput : BaseExternalRequest<ApiResult<SetEstateSeparationElementsOwnershipViewModel>>
    {
        public SetEstateSeparationElementsOwnershipQuery SetEstateSeparationElementsOwnershipQuery { get; set; }
    }

}
