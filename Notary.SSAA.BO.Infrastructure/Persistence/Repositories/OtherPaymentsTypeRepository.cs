using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;
using Notary.SSAA.BO.Infrastructure.Contexts;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.Base;
using Notary.SSAA.BO.Utilities.Extensions;
using System.Linq.Dynamic.Core;

namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories
{
    public class OtherPaymentsTypeRepository : Repository<OtherPaymentsType>, IOtherPaymentsTypeRepository
    {
        public OtherPaymentsTypeRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<OtherPaymentsTypeLookupQueryRepositoryObject> GetOtherPaymentsTypeLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, SortData gridSortInput, string GlobalSearch, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, CancellationToken cancellationToken, bool isOrderBy = false)
        {
            OtherPaymentsTypeLookupQueryRepositoryObject result = new();
            var initialQuery = TableNoTracking.Select(x => new OtherPaymentsTypeLookupItem { Title = x.Title, Id = x.Id.ToString(), Code = x.Code, Fee = x.Fee != null ? x.Fee.ToString() : null });
            if (pageIndex == 1 && selectedItemsIds.Any())
            {
                var selectedItemQuery = TableNoTracking.Where(p => selectedItemsIds.Contains(p.Id.ToString())).
                Select(x => new OtherPaymentsTypeLookupItem() { Title = x.Title, Id = x.Id.ToString(), Code = x.Code, Fee = x.Fee != null ? x.Fee.ToString() : null});
                result.SelectedItems = await selectedItemQuery.ToListAsync(cancellationToken);
            }
            string filterQueryString = LambdaString<OtherPaymentsTypeLookupItem, SearchData>.CreateWhereLambdaString(new OtherPaymentsTypeLookupItem(), GridSearchInput, GlobalSearch, FieldsNotInFilterSearch);
            if (!string.IsNullOrWhiteSpace(filterQueryString))
            {
                initialQuery = initialQuery.Where(filterQueryString.PersianToArabic());
            }
            if (isOrderBy)
            {
                initialQuery = initialQuery.OrderBy($"{gridSortInput.Sort} {gridSortInput.SortType} ");
            }
            result.GridItems = await initialQuery.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
            result.TotalCount = await initialQuery.CountAsync(cancellationToken);
            return result;
        }
    }
}
