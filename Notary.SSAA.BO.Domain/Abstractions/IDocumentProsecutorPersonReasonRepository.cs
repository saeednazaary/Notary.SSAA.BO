using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;

namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface IDocumentProsecutorPersonReasonRepository : IRepository<ReliablePersonReason>
    {
        Task<DocumentProsecutorPersonReasonLookupRepositoryObject> GetDocumentProsecutorPersonReasonLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, CancellationToken cancellationToken, bool isOrderBy = false);
    }
}
