using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;

namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface IDocumentTemplateRepository : IRepository<DocumentTemplate>
    {
        Task<DocumentTemplateGrid> GetDocumentTemplateGridItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch, SortData gridSortInput,
            IList<Guid> selectedItemsIds, DocumentTemplateExtraParams extraParams, List<string> FieldsNotInFilterSearch, string scriptoriumId, CancellationToken cancellationToken,
            bool isOrderBy = false);
        Task<DocumentTemplateRepositoryObject> GetDocumentTemplateLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch,
    SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, DocumentTemplateLookupExtraParams extraParams, CancellationToken cancellationToken, bool isOrderBy = false);
        Task<SignRequestDocumentTemplateRepositoryObject> GetSignRequestDocumentTemplateLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch,
SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, DocumentTemplateLookupExtraParams extraParams, string scripturiomId, CancellationToken cancellationToken, bool isOrderBy = false);

    }
}
