using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;
using Notary.SSAA.BO.Infrastructure.Contexts;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.Utilities.Extensions;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories
{
    internal sealed class DocumentTemplateRepository : Repository<DocumentTemplate>, IDocumentTemplateRepository
    {
        public DocumentTemplateRepository(ApplicationContext context) : base(context)
        {

        }
        public async Task<DocumentTemplateGrid> GetDocumentTemplateGridItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch, SortData gridSortInput, IList<Guid> selectedItemsIds, DocumentTemplateExtraParams extraParams, List<string> FieldsNotInFilterSearch, string scriptoriumId, CancellationToken cancellationToken, bool isOrderBy = false)
        {
            DocumentTemplateGrid result = new();

            var initialQuery = TableNoTracking.Where(x =>
                    (x.ScriptoriumId == scriptoriumId || x.ScriptoriumId == null)
                    && x.State != "0"
                )
            .Include(x => x.DocumentType)
            .Select(x =>
                new DocumentTemplateGridItem
                {
                    DocumentTemplateId = x.Id.ToString(),
                    DocumentTemplateCode = x.Code,
                    DocumentTypeTitle = x.DocumentType.Title,
                    DocumentTemplateTitle = x.Title,
                    DocumentTemplateStateId = x.State,
                    DocumentTemplateCreateDate = x.CreateDate,
                    DocumentTypeId = x.DocumentTypeId,
                });


            if (GlobalSearch != null)
            {
                initialQuery = initialQuery.Where(x =>
                    x.DocumentTemplateCode.Contains(GlobalSearch) ||
                    //x.DocumentTypeTitle.Contains(GlobalSearch) ||
                    x.DocumentTemplateTitle.Contains(GlobalSearch) ||
                    x.DocumentTemplateStateId.Contains(GlobalSearch) ||
                    x.DocumentTemplateCreateDate.Contains(GlobalSearch) ||
                    x.DocumentTypeId.Contains(GlobalSearch) 
                    );
            }

            var selectedItemQuery = TableNoTracking.Where(x =>
                (x.ScriptoriumId == scriptoriumId || x.ScriptoriumId == null)
                && selectedItemsIds.Contains(x.Id)
                && x.State != "0"
            ).Include(x => x.DocumentType).Select(x =>
            new DocumentTemplateGridItem
            {
                DocumentTemplateId = x.Id.ToString(),
                DocumentTemplateCode = x.Code,
                DocumentTypeTitle = x.DocumentType.Title,
                DocumentTemplateTitle = x.Title,
                DocumentTemplateStateId = x.State,
                DocumentTemplateCreateDate = x.CreateDate,
                DocumentTypeId = x.DocumentTypeId,
            }
            );

            if (extraParams is not null)
            {
                if (!string.IsNullOrWhiteSpace(extraParams.StateId))
                    initialQuery = initialQuery.Where(x => x.DocumentTemplateStateId == extraParams.StateId);

                if (!string.IsNullOrWhiteSpace(extraParams.DocumentTypeId))
                    initialQuery = initialQuery.Where(x => x.DocumentTypeId == extraParams.DocumentTypeId);
            }

            if (isOrderBy)
                initialQuery = initialQuery.OrderBy($"{gridSortInput.Sort} {gridSortInput.SortType} ");
            else
                initialQuery = initialQuery
                    .OrderByDescending(x => x.DocumentTemplateCreateDate)
                    .ThenByDescending(x => x.DocumentTypeTitle)
                    .ThenBy(x => x.DocumentTemplateStateId)
                    .ThenByDescending(x => x.DocumentTemplateCode);


            if (pageIndex == 1 && selectedItemsIds.Count > 0)
                result.SelectedItems = await selectedItemQuery.ToListAsync(cancellationToken);

            result.GridItems = await initialQuery.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            result.TotalCount = await initialQuery.CountAsync(cancellationToken);

            return result;
        }

        public async Task<DocumentTemplateRepositoryObject> GetDocumentTemplateLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch,
    SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, DocumentTemplateLookupExtraParams extraParams, CancellationToken cancellationToken, bool isOrderBy = false)
        {
            DocumentTemplateRepositoryObject result = new();

            var initialQuery = TableNoTracking.Include(x => x.DocumentType).Where(x => x.State == State.Valid);
            if (extraParams is not null)
            {
                if (!string.IsNullOrWhiteSpace(extraParams.StateId))
                    initialQuery = initialQuery.Where(x => x.State == extraParams.StateId);

                if (!string.IsNullOrWhiteSpace(extraParams.DocumentTypeId))
                    initialQuery = initialQuery.Where(x => x.DocumentTypeId == extraParams.DocumentTypeId);
            }

            var query = initialQuery.Select(y => new DocumentTemplateLookupItem
            {

                DocumentTypeTitle = y.DocumentType.Title,

                Title = y.Title,
                Id = y.Id.ToString(),
                Code = y.Code
            });
            string filterQueryString = LambdaString<DocumentTemplateLookupItem, SearchData>.CreateWhereLambdaString(new DocumentTemplateLookupItem(), GridSearchInput, GlobalSearch, FieldsNotInFilterSearch);

            if (pageIndex == 1 && selectedItemsIds.Count > 0)
            {
                var selectedItemsIdsAsString = selectedItemsIds.Select(id => id.ToGuid()).ToList();
                var selectedItemQuery = initialQuery.Where(p => selectedItemsIdsAsString.Contains(p.Id)).Select(y => new DocumentTemplateLookupItem
                {

                    DocumentTypeTitle = y.DocumentType.Title,
                    Title = y.Title,
                    Id = y.Id.ToString(),
                    Code = y.Code
                }); ;
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

        public async Task<SignRequestDocumentTemplateRepositoryObject> GetSignRequestDocumentTemplateLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch, SortData gridSortInput, IList<string> selectedItemsIds, 
            List<string> FieldsNotInFilterSearch, DocumentTemplateLookupExtraParams extraParams, string scriptoriumId, CancellationToken cancellationToken, bool isOrderBy = false)
        {
            SignRequestDocumentTemplateRepositoryObject result = new();

            var initialQuery = TableNoTracking.Include(x => x.DocumentType).Where(x => x.State == State.Valid && x.DocumentTypeId == "0021" && x.ScriptoriumId==scriptoriumId);
            if (extraParams is not null)
            {
                if (!string.IsNullOrWhiteSpace(extraParams.StateId))
                    initialQuery = initialQuery.Where(x => x.State == extraParams.StateId);

                if (!string.IsNullOrWhiteSpace(extraParams.DocumentTypeId))
                    initialQuery = initialQuery.Where(x => x.DocumentTypeId == extraParams.DocumentTypeId);
            }

            var query = initialQuery.Select(y => new SignRequestDocumentTemplateLookupItem
            {

                DocumentTypeTitle = y.DocumentType.Title,

                Title = y.Title,
                Id = y.Id.ToString(),
                Code = y.Code
            });
            string filterQueryString = LambdaString<SignRequestDocumentTemplateLookupItem, SearchData>.CreateWhereLambdaString(new SignRequestDocumentTemplateLookupItem(), GridSearchInput, GlobalSearch, FieldsNotInFilterSearch);

            if (pageIndex == 1 && selectedItemsIds.Count > 0)
            {
                var selectedItemsIdsAsString = selectedItemsIds.Select(id => id.ToGuid()).ToList();
                var selectedItemQuery = initialQuery.Where(p => selectedItemsIdsAsString.Contains(p.Id)).Select(y => new SignRequestDocumentTemplateLookupItem
                {

                    DocumentTypeTitle = y.DocumentType.Title,
                    Title = y.Title,
                    Id = y.Id.ToString(),
                    Code = y.Code
                }); ;
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
    }
}
