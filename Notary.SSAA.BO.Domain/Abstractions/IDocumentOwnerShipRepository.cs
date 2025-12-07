using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;

namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface IDocumentOwnerShipRepository : IRepository<DocumentEstateOwnershipDocument>
    {
        Task<DocumentOwnerShipLookupRepositoryObject> GetDocumentOwnerShipLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, DocumentOwnerShipExtraParams ExteraParams, CancellationToken cancellationToken, bool isOrderBy = false);
        Task<List<DocumentEstateOwnershipDocument>> GetOwnersFromRecentDeterministicRegServiceReq ( string scriptoriumId, string EstateInquiryId, CancellationToken cancellationToken );

    }
}
