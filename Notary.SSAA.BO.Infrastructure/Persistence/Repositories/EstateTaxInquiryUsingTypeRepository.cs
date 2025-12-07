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
    internal class EstateTaxInquiryUsingTypeRepository : Repository<EstateTaxInquiryUsingType>, IEstateTaxInquiryUsingTypeRepository
    {
        public EstateTaxInquiryUsingTypeRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<BaseLookupRepositoryObject> GetFieldUsingTypeItems(int pageIndex, int pageSize,ICollection<SearchData> GridSearchInput, string GlobalSearch, 
            SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch,bool isOrderBy, CancellationToken cancellationToken)
        {
            BaseLookupRepositoryObject result = new();
            BaseLookupItem lookupFilterItem = new();
            string[] id = new string[] { "005", "006", "007", "008", "009", "010", "011", "012" };
            var state = "1";
            var query = TableNoTracking.
                Where(x => x.State == state && id.Contains(x.Id));

            foreach (SearchData filter in GridSearchInput)
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

            if (!string.IsNullOrWhiteSpace(GlobalSearch))
            {
                query = query.Where(x => (x.Code.Contains(GlobalSearch) ||
                                   x.Title.Contains(GlobalSearch.NormalizeTextChars(true))
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

        public async Task<BaseLookupRepositoryObject> GetBuildingUsingTypeItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch,
         SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, bool isOrderBy, CancellationToken cancellationToken)
        {
            BaseLookupRepositoryObject result = new();
            BaseLookupItem lookupFilterItem = new();
            string[] id = new string[] {"001","002","003","004","013"};
            var state = "1";
            var query = TableNoTracking.
                Where(x => x.State == state && id.Contains(x.Id));

            foreach (SearchData filter in GridSearchInput)
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

            if (!string.IsNullOrWhiteSpace(GlobalSearch))
            {
                query = query.Where(x => (x.Code.Contains(GlobalSearch) ||
                                   x.Title.Contains(GlobalSearch.NormalizeTextChars(true))
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
