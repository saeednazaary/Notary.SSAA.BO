using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Infrastructure.Contexts;
using System.Linq.Dynamic.Core;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.Base;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using System.Linq;

namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories
{
    public class SignRequestGetterRepository : Repository<SignRequestGetter>, ISignRequestGetterRepository
    {
        public SignRequestGetterRepository(ApplicationContext context) : base(context)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="GridSearchInput"> فیلترهای مخصوص هر فیلد </param>
        /// <param name="GlobalSearch"> فیلتر کلی بر روی تمام فیلدها</param>
        /// <param name="gridSortInput"></param>
        /// <param name="selectedItemsIds">شناسه هایی که قبلا انتخاب شده</param>
        /// <param name="FieldsNotInFilterSearch">لیستی از فیلدهایی که از انتیتی مدنظر که نمیخواهیم بر روی آنها فیلتر بزنیم </param>
        /// <param name="cancellationToken"></param>
        /// <param name="isOrderBy"></param>
        /// <returns></returns>
        public async Task<BaseLookupRepositoryObject> GetSignRequestGetterLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, CancellationToken cancellationToken, bool isOrderBy = false)
        {
            BaseLookupRepositoryObject result = new();

            var initialQuery = TableNoTracking.Where(x=>x.State=="1");

            var query = initialQuery.Select(y => new BaseLookupItem
            {
                Title = y.Title,
                Id = y.Id.ToString(),
                Code = y.Code
            });
            string filterQueryString = LambdaString<BaseLookupItem, SearchData>.CreateWhereLambdaString(new BaseLookupItem(), GridSearchInput, GlobalSearch, FieldsNotInFilterSearch);

            if (pageIndex == 1 && selectedItemsIds.Count > 0)
            {
                var selectedItemQuery = TableNoTracking.Where(x => x.State == "1");
                if (selectedItemsIds.Count == 1)
                {
                    selectedItemQuery = selectedItemQuery.Where(p => p.Id==selectedItemsIds.First());

                }
                else
                {
                    selectedItemQuery = selectedItemQuery.Where(p => selectedItemsIds.Contains(p.Id));
                }

                result.SelectedItems = await selectedItemQuery.Select(y => new BaseLookupItem
                {
                    Title = y.Title,
                    Id = y.Id.ToString(),
                    Code = y.Code
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
