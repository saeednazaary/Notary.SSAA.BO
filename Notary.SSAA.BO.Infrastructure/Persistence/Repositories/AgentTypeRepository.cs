using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Infrastructure.Contexts;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.Base;
using Notary.SSAA.BO.Utilities.Extensions;
using System.Linq.Dynamic.Core;

namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories
{
    public class AgentTypeRepository : Repository<AgentType>, IAgentTypeRepository
    {
        public AgentTypeRepository(ApplicationContext context) : base(context)
        {

        }

        public async Task<BaseLookupRepositoryObject> GetSignRequestAgentTypeLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch,
            SortData gridSortInput, IList<string> selectedItemsIds, List<string> ResultFields, List<string> FieldsNotInFilterSearch, CancellationToken cancellationToken,
            bool isOrderBy = false)
        {
            //در حال حاضر کدهای : مدیر، متولی، وارث، مورث، موصی، شاهد، وصی، امین نمایش داده نشود.
            IList<string> DontShowCodesForTest = new List<string>() { "04", "06", "08", "09", "13", "15", "16", "17" };


            BaseLookupRepositoryObject result = new();

            var initialQuery = TableNoTracking.Where(y => y.Code != "01").Where(y => !DontShowCodesForTest.Contains(y.Code));

            var query = initialQuery.Select(y => new BaseLookupItem
            {
                Title = y.Title,
                Id = y.Id.ToString(),
                Code = y.Code
            });
            string filterQueryString = LambdaString<BaseLookupItem, SearchData>.CreateWhereLambdaString(new BaseLookupItem(), GridSearchInput, GlobalSearch, FieldsNotInFilterSearch);

            if (pageIndex == 1 && selectedItemsIds.Count > 0)
            {
                var selectedItemQuery = TableNoTracking.Where(p => selectedItemsIds.Contains(p.Id)).Where(y => y.Code != "01").Where(y => !DontShowCodesForTest.Contains(y.Code)).
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



    }
}
