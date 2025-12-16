using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;


namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface IReusedDocumentPaymentRepository : IRepository<DocumentPayment>
    {
        Task<ReusedDocumentPaymentLookupRepositoryObject> GetReusedDocumentPaymentLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch,
        SortData gridSortInput, IList<Guid> selectedItemsIds, List<string> FieldsNotInFilterSearch, ReusedDocumentPaymentLookupExtraParams extraParams,
        CancellationToken cancellationToken, bool isOrderBy = false);
    }
}