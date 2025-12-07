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
    internal class EstatePieceTypeRepository : Repository<EstatePieceType>, IEstatePieceTypeRepository
    {
        public EstatePieceTypeRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<BaseLookupRepositoryObject> GetEstatePieceTypeForEstateTaxInquiryItems(int pageIndex, int pageSize,ICollection<SearchData> GridSearchInput, string GlobalSearch, 
            SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch,bool isOrderBy, CancellationToken cancellationToken)
        {
            BaseLookupRepositoryObject result = new();
            BaseLookupItem lookupFilterItem = new();
            string[] Id = new string[] { "3e03160390fa4741a8f7fe86e078756f", "b54676c5f9204d7680c488fd6166ffe1" };
            var state = "1";
            var query = TableNoTracking.
                Where(x => x.State == state && Id.Contains(x.LegacyId));

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
                GlobalSearch = GlobalSearch.NormalizeTextChars(true).Trim();
                query = query.Where(x => (
                                   EF.Functions.Like(x.Code, $"%{GlobalSearch}%") ||
                                   EF.Functions.Like(x.Title, $"%{GlobalSearch}%")
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

        public async Task<BaseLookupRepositoryObject> GetEstatePieceTypeItems(int pageIndex, int pageSize, ICollection<SearchData> gridSearchInput, string globalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> fieldsNotInFilterSearch, bool isOrderBy, CancellationToken cancellationToken)
        {
            BaseLookupRepositoryObject result = new();

            var initialQuery = TableNoTracking.Where(p => p.State == "1");

            var query = initialQuery.Select(y => new BaseLookupItem
            {
                Title = y.Title,
                Id = y.Id.ToString(),
                Code = y.Code
            });
            string filterQueryString = LambdaString<BaseLookupItem, SearchData>.CreateWhereLambdaString(new BaseLookupItem(), gridSearchInput, globalSearch, fieldsNotInFilterSearch);

            if (pageIndex == 1 && selectedItemsIds.Count > 0)
            {
                var selectedItemQuery = TableNoTracking.Where(p => selectedItemsIds.Contains(p.Id) ).
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
