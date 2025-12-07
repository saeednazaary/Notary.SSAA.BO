using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;

namespace Notary.SSAA.BO.Domain.Abstractions
{
    public interface IDocumentPersonRepository : IRepository<DocumentPerson>
    {
        Task<DocumentPersonLookupRepositoryObject> GetDocumentRelatedPersonLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, DocumentPersonExteraParams ExteraParams, CancellationToken cancellationToken, bool isOrderBy = false);
        Task<DocumentPersonLookupRepositoryObject> GetDocumentOriginalPersonLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, DocumentPersonExteraParams ExteraParams, CancellationToken cancellationToken, bool isOrderBy = false);
        Task<DocumentDetailPersonLookupRepositoryObject> GetDocumentPersonLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, DocumentDetailPersonExteraParams ExteraParams, CancellationToken cancellationToken, bool isOrderBy = false);
        Task<DocumentPersonOwnerShipLookupRepositoryObject> GetDocumentPersonOwnerShipLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, DocumentPersonOwnerShipExtraParams ExteraParams, CancellationToken cancellationToken, bool isOrderBy = false);
        Task<List<DocumentPerson>> GetDocumentPersonPostTypeAsync(Guid documentId, string personTypeId, CancellationToken cancellationToken);
        Task<DocumentPerson> GetDocumentPersonById ( Guid id, List<string> details, CancellationToken cancellationToken );

    }
}
