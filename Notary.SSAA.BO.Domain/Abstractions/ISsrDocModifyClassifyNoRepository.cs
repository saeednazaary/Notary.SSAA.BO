using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;

namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface ISsrDocModifyClassifyNoRepository : IRepository<SsrDocModifyClassifyNo>
    {
        Task<DocumentModifyGrid> GetDocumentModifyGridItems(
            int pageIndex,
            int pageSize,
            ICollection<SearchData> GridSearchInput,
            string GlobalSearch,
            SortData gridSortInput,
            IList<Guid> selectedItemsIds,
            List<string> FieldsNotInFilterSearch,
            string scriptoriumId,
            CancellationToken cancellationToken,
            bool isOrderBy = false
        );

        Task<SsrDocModifyClassifyNo> GetDocumentModify(Guid signRequestId, string ScriptoriumId, CancellationToken cancellationToken);


    }
}
