using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;

namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface IDocumentEstateRepository : IRepository<DocumentEstate>
    {
        Task<BaseLookupRepositoryObject> GetDocumentEstateLookupItems(int pageIndex, int pageSize, ICollection<SearchData> gridFilterInput, string globalSearch, SortData gridSortInput, IList<string> selectedItems, DocumentEstateLookupExtraParams extraParams, List<string> fieldsNotInFilterSearch, CancellationToken cancellationToken, bool isOrderBy = false);
    }
}
