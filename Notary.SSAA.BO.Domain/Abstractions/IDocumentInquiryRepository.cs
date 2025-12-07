using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Document;
using Notary.SSAA.BO.Domain.RepositoryObjects.Estate;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;


namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface IDocumentInquiryRepository : IRepository<DocumentInquiry>
    {
          Task<List<DocumentInquiryInformation>> GetDocumentInquiriesInformation ( Guid documentId);

          Task<List<DocumentInquiry>> CollectSignedDSUDealSummaries(Guid documentId, string scriptoriumID,
              List<string> estestateInquiryIDsCollection, bool IsUnRegisteredEstateInquiryForced,
              string DSUInitializationDate);

          Task<List<DocumentInquiry>> GetRecentDeterministicRegisterServiceReqs(string scriptoriumId,
              string estateInquiryId, CancellationToken cancellationToken);




    }
}
