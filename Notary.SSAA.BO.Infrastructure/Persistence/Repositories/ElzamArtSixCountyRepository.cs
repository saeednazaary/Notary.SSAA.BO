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
    public class ElzamArtSixCountyRepository : Repository<SsrArticle6County>, IElzamArtSixCountyRepository
    {
        public ElzamArtSixCountyRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public async Task<ElzamArtSixCountyLookupRepositoryObject> GetElzamArtSixCountyLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, ElzamArtSixCountyLookupExtraParams extraParams, CancellationToken cancellationToken, bool isOrderBy = false)
        {
            ElzamArtSixCountyLookupRepositoryObject result = new();
            IQueryable<SsrArticle6County> initialQuery = TableNoTracking;

            if (!string.IsNullOrEmpty(extraParams.provinceId))
            {
                initialQuery = initialQuery.Where(x => x.Code.StartsWith(extraParams.provinceId));
            }
            if (extraParams is not null)
            {
            }

            var query = initialQuery.Select(y => new ElzamArtSixCountyLookupItem()
            {
                Id = y.Id,
                Code = y.Code,
                Title = y.Title,
                State = y.State,
                Nid = y.Nid
            });

            string filterQueryString = LambdaString<ElzamArtSixCountyLookupItem, SearchData>.CreateWhereLambdaString(new ElzamArtSixCountyLookupItem(), GridSearchInput, GlobalSearch, FieldsNotInFilterSearch);

            if (pageIndex == 1 && selectedItemsIds.Count > 0)
                result.SelectedItems = await query.Where(x => selectedItemsIds.Contains(x.Id)).ToListAsync(cancellationToken);

            if (!string.IsNullOrWhiteSpace(filterQueryString))
                query = query.Where(filterQueryString.ArabicTopersian());

            if (isOrderBy)
                query = query.OrderBy($"{gridSortInput.Sort} {gridSortInput.SortType} ");

            result.GridItems = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            result.TotalCount = await query.CountAsync(cancellationToken);

            return result;
        }
    }
}