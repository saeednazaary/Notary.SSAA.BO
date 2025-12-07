using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;

namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface IDocumentTypeRepository:IRepository<DocumentType>
    {
        Task<DocumentTypeRepositoryObject> GetDocumentTypeLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch,
            SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, DocumentTypeExtraParams extraParams, CancellationToken cancellationToken, bool isOrderBy = false);
        Task<BaseLookupRepositoryObject> GetRelatedDocumentTypeLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch,
        SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, RelatedDocumentTypeExtraParams extraParams, CancellationToken cancellationToken, bool isOrderBy = false);
        Task<BaseLookupRepositoryObject> GetEstateDocumentTypeLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch,
            SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, CancellationToken cancellationToken, bool isOrderBy = false);
        Task<IList<DocumentType>> GetDocumentTypes(IList<string> documentTypeId, CancellationToken cancellationToken);
        Task<BaseLookupRepositoryObject> GetDetailDocumentTypeLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch,
            SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, CancellationToken cancellationToken, bool isOrderBy = false);
        Task<BaseLookupRepositoryObject> GetDocumentSupportiveTypeLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch,
            SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, CancellationToken cancellationToken, bool isOrderBy = false);
        Task<DocumentType> GetDocumentType ( string documentType,CancellationToken cancellationToken );
    }
}
