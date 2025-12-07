using Notary.SSAA.BO.DataTransferObject.Bases;

using Notary.SSAA.BO.Domain.RepositoryObjects.Fingerprint;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Queries.Fingerprint
{

    public sealed class GetPersonFingerTypeQuery : BaseQueryRequest<ApiResult<List<GetPersonFingerType>>>
    {

    }

}
