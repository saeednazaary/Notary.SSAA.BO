using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Infrastructure.Contexts;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.Base;
using Notary.SSAA.BO.Utilities.Estate;
using Notary.SSAA.BO.Utilities.Extensions;
using System.Linq.Dynamic.Core;

namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories
{
    public class EstateReliablePersonReasonRepository : Repository<ReliablePersonReason>, IEstateReliablePersonReasonRepository
    {
        public EstateReliablePersonReasonRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<BaseLookupRepositoryObject> GetNeedingProsecutorReasonItems(int pageIndex, int pageSize, ICollection<SearchData> gridSearchInput, string globalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> fieldsNotInFilterSearch, bool isOrderBy, CancellationToken cancellationToken)
        {
            BaseLookupRepositoryObject result = new();

            BaseLookupItem lookupFilterItem = new();
            var state = "1";
            var codeList = new string[] { "03", "05", "06" };
            var query = TableNoTracking.
                Where(x => x.State == state && codeList.Contains(x.Code));

            foreach (SearchData filter in gridSearchInput)
            {
                if (string.IsNullOrWhiteSpace(filter.Value)) continue;
                var capitalCaseFilter = $"{filter.Filter[..1].ToUpper()}{filter.Filter[1..]}";
                switch (capitalCaseFilter)
                {
                    case nameof(BaseLookupItem.Code):
                        query = query.Where(x => x.Code.Contains(filter.Value));
                        break;
                    case nameof(BaseLookupItem.Title):
                        query = query.Where(x => x.Title.Contains(filter.Value.NormalizeTextChars(true)));
                        break;

                }

            }

            if (!string.IsNullOrWhiteSpace(globalSearch))
            {
                query = query.Where(x => (x.Code.Contains(globalSearch) ||
                                   x.Title.Contains(globalSearch.NormalizeTextChars(true))
                                    )
                                 );

            }

            result.TotalCount = await query.CountAsync(cancellationToken);

            if (pageIndex == 1 && selectedItemsIds.Count > 0)
            {
                result.SelectedItems = await TableNoTracking.Where(p => selectedItemsIds.Contains(p.Id.ToString()))
                .Select(y => new BaseLookupItem() { Code = y.Code, Id = y.Id, Title = y.Title })
                .ToListAsync(cancellationToken);
            }
            if (result.TotalCount > 0)
            {
                if (isOrderBy)
                {

                    result.GridItems = await query
                    .OrderBy($"{gridSortInput.Sort} {gridSortInput.SortType} ")
                    .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                    .Select(y => new BaseLookupItem() { Code = y.Code, Id = y.Id, Title = y.Title })
                        .ToListAsync(cancellationToken);

                }
                else
                {
                    result.GridItems = await query
                        .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                        .Select(y => new BaseLookupItem() { Code = y.Code, Id = y.Id, Title = y.Title })
                        .ToListAsync(cancellationToken);
                }
            }
            Helper.NormalizeStringValuesDeeply(result, false);
            return result;
        }

        public async Task<BaseLookupRepositoryObject> GetNeedingReliablePersonReasonItems(int pageIndex, int pageSize, ICollection<SearchData> gridSearchInput, string globalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> fieldsNotInFilterSearch, bool isOrderBy, CancellationToken cancellationToken)
        {
            BaseLookupRepositoryObject result = new();

            BaseLookupItem lookupFilterItem = new();
            var state = "1";
            var codeList = new string[] { "01","02","03", "04", "07" };
            var query = TableNoTracking.
                Where(x => x.State == state && codeList.Contains(x.Code));

            foreach (SearchData filter in gridSearchInput)
            {
                if (string.IsNullOrWhiteSpace(filter.Value)) continue;
                var capitalCaseFilter = $"{filter.Filter[..1].ToUpper()}{filter.Filter[1..]}";
                switch (capitalCaseFilter)
                {
                    case nameof(BaseLookupItem.Code):
                        query = query.Where(x => x.Code.Contains(filter.Value));
                        break;
                    case nameof(BaseLookupItem.Title):
                        query = query.Where(x => x.Title.Contains(filter.Value.NormalizeTextChars(true)));
                        break;

                }

            }

            if (!string.IsNullOrWhiteSpace(globalSearch))
            {
                query = query.Where(x => (x.Code.Contains(globalSearch) ||
                                   x.Title.Contains(globalSearch.NormalizeTextChars(true))
                                    )
                                 );

            }

            result.TotalCount = await query.CountAsync(cancellationToken);

            if (pageIndex == 1 && selectedItemsIds.Count > 0)
            {
                result.SelectedItems = await TableNoTracking.Where(p => selectedItemsIds.Contains(p.Id.ToString()))
                .Select(y => new BaseLookupItem() { Code = y.Code, Id = y.Id, Title = y.Title })
                .ToListAsync(cancellationToken);
            }
            if (result.TotalCount > 0)
            {
                if (isOrderBy)
                {

                    result.GridItems = await query
                    .OrderBy($"{gridSortInput.Sort} {gridSortInput.SortType} ")
                    .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                    .Select(y => new BaseLookupItem() { Code = y.Code, Id = y.Id, Title = y.Title })
                        .ToListAsync(cancellationToken);

                }
                else
                {
                    result.GridItems = await query
                        .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                        .Select(y => new BaseLookupItem() { Code = y.Code, Id = y.Id, Title = y.Title })
                        .ToListAsync(cancellationToken);
                }
            }
            Helper.NormalizeStringValuesDeeply(result, false);
            return result;
        }
    }
}
