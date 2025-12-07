using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;
using Notary.SSAA.BO.Infrastructure.Contexts;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.Utilities.Extensions;
using System.Linq.Dynamic.Core;

namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories
{
    internal sealed class DocumentTypeRepository : Repository<DocumentType>, IDocumentTypeRepository
    {
        public DocumentTypeRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<DocumentTypeRepositoryObject> GetDocumentTypeLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch,
            SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, DocumentTypeExtraParams extraParams, CancellationToken cancellationToken, bool isOrderBy = false)
        {
            DocumentTypeRepositoryObject result = new();

            var initialQuery = TableNoTracking.Where(x => x.State == State.Valid);
            var IsSupportiveCodeList = new string[] { "111", "115", "112", "211", "225", "212", "711", "971", "981", "611", "612" };
            if (extraParams is not null)
            {
                if (!extraParams.DocumentTypeGroupOneId.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => x.DocumentTypeGroup1Id == extraParams.DocumentTypeGroupOneId);

                if (!extraParams.DocumentTypeGroupTwoId.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => x.DocumentTypeGroup2Id == extraParams.DocumentTypeGroupTwoId);
              
                if (extraParams.IsSupportive!=null)
                {
                    initialQuery= initialQuery.Where(x => !IsSupportiveCodeList.Contains(x.Code) && x.IsSupportive=="1");
                }
                else
                {
                    initialQuery = initialQuery.Where(x => x.IsSupportive=="2");
                }
            }
            else
            {
                initialQuery = initialQuery.Where(x => x.IsSupportive == "2");
            }
            var query = initialQuery.Select(y => new DocumentTypeLookupItem
            {
                IsSupportive = y.IsSupportive.ToNullabbleBoolean(),
                State = y.State,
                DocumentTypeGroupOneId = y.DocumentTypeGroup1Id,
                DocumentTypeGroupTwoId = y.DocumentTypeGroup2Id,
                EstateInquiryIsRequired = y.EstateInquiryIsRequired.ToNullabbleBoolean(),
                HasEstateInquiry = y.HasEstateInquiry.ToNullabbleBoolean(),
                WealthType = y.WealthType.ToNullabbleBoolean(),
                AssetTypeIsRequired = y.AssetTypeIsRequired.ToNullabbleBoolean(),
                HasAssetType = y.HasAssetType.ToNullabbleBoolean(),
                SubjectIsRequired = y.SubjectIsRequired.ToNullabbleBoolean(),
                HasSubject = y.HasSubject.ToNullabbleBoolean(),
                Title = y.Title,
                Id = y.Id.ToString(),
                Code = y.Code
            });
            string filterQueryString = LambdaString<DocumentTypeLookupItem, SearchData>.CreateWhereLambdaString(new DocumentTypeLookupItem(), GridSearchInput, GlobalSearch, FieldsNotInFilterSearch);

            if (pageIndex == 1 && selectedItemsIds.Count > 0)
            {
                var selectedItemQuery = query.Where(p => selectedItemsIds.Contains(p.Id));
                result.SelectedItems = await selectedItemQuery.ToListAsync(cancellationToken);
            }


            if (!string.IsNullOrWhiteSpace(filterQueryString))
                query = query.Where(filterQueryString);

            if (isOrderBy)
                query = query.OrderBy($"{gridSortInput.Sort} {gridSortInput.SortType} ");

            result.GridItems = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            result.TotalCount = await query.CountAsync(cancellationToken);

            return result;
        }


        public async Task<BaseLookupRepositoryObject> GetRelatedDocumentTypeLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch,
       SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, RelatedDocumentTypeExtraParams extraParams, CancellationToken cancellationToken, bool isOrderBy = false)
        {
            BaseLookupRepositoryObject result = new();

            var initialQuery = TableNoTracking.Where(x => x.State == State.Valid && x.IsSupportive == "2");

            if (extraParams is not null)
            {
                if (!extraParams.DocumentTypeGroupOneId.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => x.DocumentTypeGroup1Id == extraParams.DocumentTypeGroupOneId);

                if (!extraParams.DocumentTypeGroupTwoId.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => x.DocumentTypeGroup2Id == extraParams.DocumentTypeGroupTwoId);

            }

            var query = initialQuery.Select(y => new BaseLookupItem
            {
                Title = y.Title,
                Id = y.Id.ToString(),
                Code = y.Code
            });
            string filterQueryString = LambdaString<BaseLookupItem, SearchData>.CreateWhereLambdaString(new BaseLookupItem(), GridSearchInput, GlobalSearch, FieldsNotInFilterSearch);

            if (pageIndex == 1 && selectedItemsIds.Count > 0)
            {
                var selectedItemQuery = query.Where(p => selectedItemsIds.Contains(p.Id));
                result.SelectedItems = await selectedItemQuery.ToListAsync(cancellationToken);
            }


            if (!string.IsNullOrWhiteSpace(filterQueryString))
                query = query.Where(filterQueryString);

            if (isOrderBy)
                query = query.OrderBy($"{gridSortInput.Sort} {gridSortInput.SortType} ");

            result.GridItems = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            result.TotalCount = await query.CountAsync(cancellationToken);

            return result;
        }

        public async Task<IList<DocumentType>> GetDocumentTypes(IList<string> documentTypeId, CancellationToken cancellationToken)
        {
            return await TableNoTracking
               .Where(x => documentTypeId.Contains(x.Id)).ToListAsync(cancellationToken);
        }

        public async Task<BaseLookupRepositoryObject> GetEstateDocumentTypeLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch,
            SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, CancellationToken cancellationToken, bool isOrderBy = false)
        {
            BaseLookupRepositoryObject result = new();
            var codeList = new string[] { "111", "115", "112", "211", "225", "212", "711", "971", "981", "611", "612" };
            var initialQuery = TableNoTracking
                               .Where(x => codeList.Contains(x.Code) && x.State == State.Valid);
            var query = initialQuery.Select(y => new BaseLookupItem
            {
                Title = y.Title,
                Id = y.Id.ToString(),
                Code = y.Code
            });
            string filterQueryString = LambdaString<BaseLookupItem, SearchData>.CreateWhereLambdaString(new BaseLookupItem(), GridSearchInput, GlobalSearch, FieldsNotInFilterSearch);

            if (pageIndex == 1 && selectedItemsIds.Count > 0)
            {
                var selectedItemQuery = TableNoTracking.Where(p => selectedItemsIds.Contains(p.Id) && p.State == State.Valid).
                           Select(y => new BaseLookupItem
                           {
                               Title = y.Title,
                               Id = y.Id.ToString(),
                               Code = y.Code
                           });
                result.SelectedItems = await selectedItemQuery.ToListAsync(cancellationToken);
            }


            if (!string.IsNullOrWhiteSpace(filterQueryString))
                query = query.Where(filterQueryString);

            if (isOrderBy)
                query = query.OrderBy($"{gridSortInput.Sort} {gridSortInput.SortType} ");

            result.GridItems = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            result.TotalCount = await query.CountAsync(cancellationToken);

            return result;
        }

        public async Task<BaseLookupRepositoryObject> GetDetailDocumentTypeLookupItems(int pageIndex, int pageSize, ICollection<SearchData> gridFilterInput, string globalSearch, SortData gridSortInput, IList<string> selectedItems, List<string> fieldsNotInFilterSearch, CancellationToken cancellationToken, bool isOrderBy = false)
        {
            BaseLookupRepositoryObject result = new();
            var code = new string[] { "0028", "0030", "0035", "0023", "0024", "0025", "0027", "0026", "0031", "0050", "0029", "0033", "0032" };
            var initialQuery = TableNoTracking.Where(x => code.Contains(x.Code) && x.State == State.Valid).
                           Select(y => new BaseLookupItem
                           {
                               Title = y.Title,
                               Id = y.Id.ToString(),
                               Code = y.Code
                           });


            string filterQueryString = LambdaString<BaseLookupRepositoryObject, SearchData>.CreateWhereLambdaString(new BaseLookupRepositoryObject(), gridFilterInput, globalSearch, fieldsNotInFilterSearch);

            if (pageIndex == 1 && selectedItems.Count > 0)
            {
                var selectedItemQuery = initialQuery.Where(p => selectedItems.Contains(p.Id));
                result.SelectedItems = await selectedItemQuery.ToListAsync(cancellationToken);
            }


            if (!string.IsNullOrWhiteSpace(filterQueryString))
                initialQuery = initialQuery.Where(filterQueryString);

            if (isOrderBy)
                initialQuery = initialQuery.OrderBy($"{gridSortInput.Sort} {gridSortInput.SortType} ");

            result.GridItems = await initialQuery.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            result.TotalCount = await initialQuery.CountAsync(cancellationToken);

            return result;
        }

        public async Task<BaseLookupRepositoryObject> GetDocumentSupportiveTypeLookupItems(int pageIndex, int pageSize, ICollection<SearchData> gridFilterInput, string globalSearch, SortData gridSortInput, IList<string> selectedItems, List<string> fieldsNotInFilterSearch, CancellationToken cancellationToken, bool isOrderBy = false)
        {
            BaseLookupRepositoryObject result = new();
            var initialQuery = TableNoTracking.Where(x => x.IsSupportive == "1" && x.State == State.Valid).
                           Select(y => new BaseLookupItem
                           {
                               Title = y.Title,
                               Id = y.Id.ToString(),
                               Code = y.Code
                           });


            string filterQueryString = LambdaString<BaseLookupRepositoryObject, SearchData>.CreateWhereLambdaString(new BaseLookupRepositoryObject(), gridFilterInput, globalSearch, fieldsNotInFilterSearch);

            if (pageIndex == 1 && selectedItems.Count > 0)
            {
                var selectedItemQuery = initialQuery.Where(p => selectedItems.Contains(p.Id));
                result.SelectedItems = await selectedItemQuery.ToListAsync(cancellationToken);
            }


            if (!string.IsNullOrWhiteSpace(filterQueryString))
                initialQuery = initialQuery.Where(filterQueryString);

            if (isOrderBy)
                initialQuery = initialQuery.OrderBy($"{gridSortInput.Sort} {gridSortInput.SortType} ");

            result.GridItems = await initialQuery.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            result.TotalCount = await initialQuery.CountAsync(cancellationToken);

            return result;
        }

        public async Task<DocumentType> GetDocumentType ( string documentType, CancellationToken cancellationToken )
        {
           return await TableNoTracking.Where ( x => x.Code==documentType ).FirstOrDefaultAsync(cancellationToken);

        }

    }
}
