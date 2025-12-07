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
    public class ElzamArtSixEstateUsingRepository : Repository<SsrArticle6EstateUsing>, IElzamArtSixEstateUsingRepository
    {
        public ElzamArtSixEstateUsingRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public async Task<ElzamArtSixEstateUsingLookupRepositoryObject> GetElzamArtSixEstateUsingLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, ElzamArtSixEstateUsingLookupExtraParams extraParams, CancellationToken cancellationToken, bool isOrderBy = false)
        {
            ElzamArtSixEstateUsingLookupRepositoryObject result = new();
            IQueryable<SsrArticle6EstateUsing> initialQuery = TableNoTracking;

            if (extraParams is not null)
            {
            }

            var query = initialQuery.Select(y => new ElzamArtSixEstateUsingLookupItem()
            {
                Id = y.Id,
                Code = y.Code,
                Title = y.Title,
                State = y.State
            });

            string filterQueryString = LambdaString<ElzamArtSixEstateUsingLookupItem, SearchData>.CreateWhereLambdaString(new ElzamArtSixEstateUsingLookupItem(), GridSearchInput, GlobalSearch, FieldsNotInFilterSearch);

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