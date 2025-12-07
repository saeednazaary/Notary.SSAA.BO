using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Infrastructure.Contexts;
using System.Linq.Dynamic.Core;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.Base;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.Utilities.Estate;

namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories
{
    public class WorkflowStateRepository:Repository<WorkflowState>,IWorkfolwStateRepository
    {
        public WorkflowStateRepository(ApplicationContext context) : base(context)
        {
        }
        public async Task<BaseLookupRepositoryObject> GetEstateInquiryStatusLookupItems(int pageIndex, int pageSize, ICollection<SearchData> gridSearchInput, string globalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> fieldsNotInFilterSearch,  string scriptoriumId, bool isOrderBy , CancellationToken cancellationToken)
        {
            BaseLookupRepositoryObject result = new();

            BaseLookupItem lookupFilterItem = new();

            var tableName = "ESTATE_INQUIRY";
            var query = TableNoTracking.
                Where(x => x.TableName == tableName && x.State != "-3");
            foreach (SearchData filter in gridSearchInput)
            {
                if (string.IsNullOrWhiteSpace(filter.Value)) continue;
                var capitalCaseFilter = $"{filter.Filter[..1].ToUpper()}{filter.Filter[1..]}";
                switch (capitalCaseFilter)
                {
                    case nameof(BaseLookupItem.Code):
                        query = query.Where(x => x.State.Contains(filter.Value));
                        break;
                    case nameof(BaseLookupItem.Title):
                        query = query.Where(x => x.Title.Contains(filter.Value.NormalizeTextChars(true)));
                        break;

                }

            }

            if (!string.IsNullOrWhiteSpace(globalSearch))
            {
                query = query.Where(x => (x.State.Contains(globalSearch) ||
                                   x.Title.Contains(globalSearch.NormalizeTextChars(true))
                                    )
                                 );

            }

            result.TotalCount = await query.CountAsync(cancellationToken);

            if (pageIndex == 1 && selectedItemsIds.Count > 0)
            {
                result.SelectedItems = await TableNoTracking.Where(p => selectedItemsIds.Contains(p.Id.ToString()))
                .Select(y => new BaseLookupItem() { Code = y.State, Id = y.Id, Title = y.Title })
                .ToListAsync(cancellationToken);
            }
            if (result.TotalCount > 0)
            {
                if (isOrderBy)
                {

                    result.GridItems = await query
                    .OrderBy($"{gridSortInput.Sort} {gridSortInput.SortType} ")
                    .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                    .Select(y => new BaseLookupItem() { Code = y.State, Id = y.Id, Title = y.Title })
                        .ToListAsync(cancellationToken);

                }
                else
                {
                    result.GridItems = await query
                        .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                        .Select(y => new BaseLookupItem() { Code = y.State, Id = y.Id, Title = y.Title })
                        .ToListAsync(cancellationToken);
                }
            }
            Helper.NormalizeStringValuesDeeply(result, false);
            return result;
        }

        public async Task<string> GetEstateInquiryWorkflowStateId(string state, CancellationToken cancellationToken)
        {
            var tableName = "ESTATE_INQUIRY";
            var wfstate = await TableNoTracking.Where(x => x.TableName == tableName && x.State == state).FirstAsync(cancellationToken);
            return wfstate.Id;
        }
        public async Task<string> GetForestOrganizationInquiryWorkflowStateId(string state, CancellationToken cancellationToken)
        {
            var tableName = "FORESTORG_INQUIRY";
            var ws = await TableNoTracking.Where(x => x.TableName == tableName && x.State == state).FirstAsync(cancellationToken);
            return ws.Id;
        }

        
        public async Task<BaseLookupRepositoryObject> GetDealSummaryStatusLookupItems(int pageIndex, int pageSize, ICollection<SearchData> gridSearchInput, string globalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> fieldsNotInFilterSearch, bool isOrderBy, CancellationToken cancellationToken)
        {
            BaseLookupRepositoryObject result = new();

            BaseLookupItem lookupFilterItem = new();

            var tableName = "DEAL_SUMMARY";
            var query = TableNoTracking.
                Where(x => x.TableName == tableName);
            foreach (SearchData filter in gridSearchInput)
            {
                if (string.IsNullOrWhiteSpace(filter.Value)) continue;
                var capitalCaseFilter = $"{filter.Filter[..1].ToUpper()}{filter.Filter[1..]}";
                switch (capitalCaseFilter)
                {
                    case nameof(BaseLookupItem.Code):
                        query = query.Where(x => x.State.Contains(filter.Value));
                        break;
                    case nameof(BaseLookupItem.Title):
                        query = query.Where(x => x.Title.Contains(filter.Value.NormalizeTextChars(true)));
                        break;

                }

            }

            if (!string.IsNullOrWhiteSpace(globalSearch))
            {
                query = query.Where(x => (x.State.Contains(globalSearch) ||
                                   x.Title.Contains(globalSearch.NormalizeTextChars(true))
                                    )
                                 );

            }

            result.TotalCount = await query.CountAsync(cancellationToken);

            if (pageIndex == 1 && selectedItemsIds.Count > 0)
            {
                result.SelectedItems = await TableNoTracking.Where(p => selectedItemsIds.Contains(p.Id.ToString()))
                .Select(y => new BaseLookupItem() { Code = y.State, Id = y.Id, Title = y.Title })
                .ToListAsync(cancellationToken);
            }
            if (result.TotalCount > 0)
            {
                if (isOrderBy)
                {

                    result.GridItems = await query
                    .OrderBy($"{gridSortInput.Sort} {gridSortInput.SortType} ")
                    .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                    .Select(y => new BaseLookupItem() { Code = y.State, Id = y.Id, Title = y.Title })
                        .ToListAsync(cancellationToken);

                }
                else
                {
                    result.GridItems = await query
                        .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                        .Select(y => new BaseLookupItem() { Code = y.State, Id = y.Id, Title = y.Title })
                        .ToListAsync(cancellationToken);
                }
            }
            Helper.NormalizeStringValuesDeeply(result, false);
            return result;
        }

        public async Task<BaseLookupRepositoryObject> GetEstateDocumentRequestStatusLookupItems(int pageIndex, int pageSize, ICollection<SearchData> gridSearchInput, string globalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> fieldsNotInFilterSearch, bool isOrderBy, CancellationToken cancellationToken)
        {
            BaseLookupRepositoryObject result = new();

            BaseLookupItem lookupFilterItem = new();

            var tableName = "ESTATE_DOCUMENT_REQUEST";
            var query = TableNoTracking.
                Where(x => x.TableName == tableName);
            foreach (SearchData filter in gridSearchInput)
            {
                if (string.IsNullOrWhiteSpace(filter.Value)) continue;
                var capitalCaseFilter = $"{filter.Filter[..1].ToUpper()}{filter.Filter[1..]}";
                switch (capitalCaseFilter)
                {
                    case nameof(BaseLookupItem.Code):
                        query = query.Where(x => x.State.Contains(filter.Value));
                        break;
                    case nameof(BaseLookupItem.Title):
                        query = query.Where(x => x.Title.Contains(filter.Value.NormalizeTextChars(true)));
                        break;

                }

            }

            if (!string.IsNullOrWhiteSpace(globalSearch))
            {
                query = query.Where(x => (x.State.Contains(globalSearch) ||
                                   x.Title.Contains(globalSearch.NormalizeTextChars(true))
                                    )
                                 );

            }

            result.TotalCount = await query.CountAsync(cancellationToken);

            if (pageIndex == 1 && selectedItemsIds.Count > 0)
            {
                result.SelectedItems = await TableNoTracking.Where(p => selectedItemsIds.Contains(p.Id.ToString()))
                .Select(y => new BaseLookupItem() { Code = y.State, Id = y.Id, Title = y.Title })
                .ToListAsync(cancellationToken);
            }
            if (result.TotalCount > 0)
            {
                if (isOrderBy)
                {

                    result.GridItems = await query
                    .OrderBy($"{gridSortInput.Sort} {gridSortInput.SortType} ")
                    .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                    .Select(y => new BaseLookupItem() { Code = y.State, Id = y.Id, Title = y.Title })
                        .ToListAsync(cancellationToken);

                }
                else
                {
                    result.GridItems = await query
                        .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                        .Select(y => new BaseLookupItem() { Code = y.State, Id = y.Id, Title = y.Title })
                        .ToListAsync(cancellationToken);
                }
            }
            Helper.NormalizeStringValuesDeeply(result, false);
            return result;
        }

        public async Task<BaseLookupRepositoryObject> GetEstateTaxInquiryStatusLookupItems(int pageIndex, int pageSize, ICollection<SearchData> gridSearchInput, string globalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> fieldsNotInFilterSearch, string scriptoriumId, bool isOrderBy, CancellationToken cancellationToken)
        {
            BaseLookupRepositoryObject result = new();

            BaseLookupItem lookupFilterItem = new();

            var tableName = "ESTATE_TAX_INQUIRY";
            var query = TableNoTracking.
                Where(x => x.TableName == tableName);
            foreach (SearchData filter in gridSearchInput)
            {
                if (string.IsNullOrWhiteSpace(filter.Value)) continue;
                var capitalCaseFilter = $"{filter.Filter[..1].ToUpper()}{filter.Filter[1..]}";
                switch (capitalCaseFilter)
                {
                    case nameof(BaseLookupItem.Code):
                        query = query.Where(x => x.State.Contains(filter.Value));
                        break;
                    case nameof(BaseLookupItem.Title):
                        query = query.Where(x => x.Title.Contains(filter.Value.NormalizeTextChars(true)));
                        break;

                }

            }

            if (!string.IsNullOrWhiteSpace(globalSearch))
            {
                query = query.Where(x => (x.State.Contains(globalSearch) ||
                                   x.Title.Contains(globalSearch.NormalizeTextChars(true))
                                    )
                                 );

            }

            result.TotalCount = await query.CountAsync(cancellationToken);

            if (pageIndex == 1 && selectedItemsIds.Count > 0)
            {
                result.SelectedItems = await TableNoTracking.Where(p => selectedItemsIds.Contains(p.Id.ToString()))
                .Select(y => new BaseLookupItem() { Code = y.State, Id = y.Id, Title = y.Title })
                .ToListAsync(cancellationToken);
            }
            if (result.TotalCount > 0)
            {
                if (isOrderBy)
                {

                    result.GridItems = await query
                    .OrderBy($"{gridSortInput.Sort} {gridSortInput.SortType} ")
                    .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                    .Select(y => new BaseLookupItem() { Code = y.State, Id = y.Id, Title = y.Title })
                        .ToListAsync(cancellationToken);

                }
                else
                {
                    result.GridItems = await query
                        .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                        .Select(y => new BaseLookupItem() { Code = y.State, Id = y.Id, Title = y.Title })
                        .ToListAsync(cancellationToken);
                }
            }
            Helper.NormalizeStringValuesDeeply(result, false);
            return result;
        }

    }
}
