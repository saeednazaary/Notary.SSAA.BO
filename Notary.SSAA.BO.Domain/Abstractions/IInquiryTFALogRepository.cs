using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;


namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface IInquiryTFALogRepository : IRepository<InquiryTfaLog>
    {

        Task<InquiryTfaLog> GetInquiryTfaLog(string objectId, string formId, string scriptorumId, string currentDate, CancellationToken cancellationToken);
    }
}
