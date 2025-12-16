using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Infrastructure.Contexts;
using System.Linq.Dynamic.Core;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.Base;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;

namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories
{
    internal class SsrConfigSubjectRepository : Repository<SsrConfigSubject>, ISsrConfigSubjectRepository
    {
        public SsrConfigSubjectRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<SsrConfigSubjectLookupRepositoryObject> GetSsrConfigSubjectLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, SsrConfigSubjectExtraParams extraParams, CancellationToken cancellationToken, bool isOrderBy = false)
        {
            SsrConfigSubjectLookupRepositoryObject result = new();

            var initialQuery = TableNoTracking.Where(x => x.State == "1");
            if (!string.IsNullOrWhiteSpace(extraParams?.SsrConfigMainSubjectId))
            {
                initialQuery = initialQuery.Where(x => x.SsrConfigMainSubjectId == extraParams.SsrConfigMainSubjectId.ToGuid());
            }
            var query = initialQuery.Select(y => new SsrConfigSubjectLookupObject
            {
                Title = y.Title,
                Id = y.Id.ToString(),
                ConfigType=y.ConfigType
            });
            string filterQueryString = LambdaString<SsrConfigSubjectLookupObject, SearchData>.CreateWhereLambdaString(new SsrConfigSubjectLookupObject(), GridSearchInput, GlobalSearch, FieldsNotInFilterSearch);

            if (pageIndex == 1 && selectedItemsIds.Count > 0)
            {
                var selectedItemQuery = TableNoTracking.Where(x => x.State == "1");
                if (selectedItemsIds.Count == 1)
                {
                    selectedItemQuery = selectedItemQuery.Where(p => p.Id == selectedItemsIds.First().ToGuid());

                }
                else
                {
                    selectedItemQuery = selectedItemQuery.Where(p => selectedItemsIds.Contains(p.Id.ToString()));
                }

                result.SelectedItems = await selectedItemQuery.Select(y => new SsrConfigSubjectLookupObject
                {
                    Title = y.Title,
                    Id = y.Id.ToString(),
                    ConfigType = y.ConfigType
                }).ToListAsync(cancellationToken);
            }


            if (!string.IsNullOrWhiteSpace(filterQueryString))
                query = query.Where(filterQueryString);

            if (isOrderBy)
            {
                query = query.OrderBy($"{gridSortInput.Sort} {gridSortInput.SortType} ");
            }
            else
            {
                query = query.OrderBy(x => x.Title);
            }

            result.GridItems = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            result.TotalCount = await query.CountAsync(cancellationToken);

            return result;
        }
    }
}
