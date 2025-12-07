using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;

namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface IDocumentCaseRepository : IRepository<DocumentCase>
    {
        Task<DocumentCaseGrid> GetDocumentCaseGridItems(
            int pageIndex,
            int pageSize,
            ICollection<SearchData> GridSearchInput,
            string GlobalSearch,
            SortData gridSortInput,
            IList<Guid> selectedItemsIds,
            List<string> FieldsNotInFilterSearch,
            string scriptoriumId,
            DocumentCaseExtraParams extraParams,
            CancellationToken cancellationToken,
            bool isOrderBy = false
        );


    }
}
