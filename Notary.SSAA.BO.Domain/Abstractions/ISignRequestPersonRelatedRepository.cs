using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;


namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface ISignRequestPersonRelatedRepository : IRepository<SignRequestPersonRelated>
    {

        public Task<List<SignRequestPersonRelated>> LoadSignRequestPersonRelated(Guid signRequestId,string scriptoriumId,CancellationToken cancellationToken);

    }

}

