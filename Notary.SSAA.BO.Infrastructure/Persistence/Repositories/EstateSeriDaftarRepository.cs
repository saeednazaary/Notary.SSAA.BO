using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;
using Notary.SSAA.BO.Infrastructure.Contexts;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.Base;
using Notary.SSAA.BO.Utilities.Estate;
using Notary.SSAA.BO.Utilities.Extensions;
using System.Linq.Dynamic.Core;


namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories
{
    public class EstateSeriDaftarRepository:Repository<EstateSeridaftar>,IEstateSeriDaftarRepository
    {
        public EstateSeriDaftarRepository(ApplicationContext context) : base(context) 
        {
        }
        public async Task<EstateSeriDaftarLookupViewModel> GetEstateSeriDaftarItems(int pageIndex, int pageSize, ICollection<SearchData> gridSearchInput, string globalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> fieldsNotInFilterSearch,  string unitId, bool isOrderBy, CancellationToken cancellationToken)
        {
            EstateSeriDaftarLookupViewModel result = new();

            EstateSeriDaftarLookupItem lookupFilterItem = new();
            var state = "1";
            var query = TableNoTracking.
                Where(x => x.State == state && x.UnitId == unitId);

            foreach (SearchData filter in gridSearchInput)
            {
                if (string.IsNullOrWhiteSpace(filter.Value)) continue;
                var capitalCaseFilter = $"{filter.Filter[..1].ToUpper()}{filter.Filter[1..]}";
                switch (capitalCaseFilter)
                {
                    case nameof(EstateSeriDaftarLookupItem.Code):
                        query = query.Where(x => x.Code.Contains(filter.Value));
                        break;
                    case nameof(EstateSeriDaftarLookupItem.Title):
                        query = query.Where(x => x.Title.Contains(filter.Value.NormalizeTextChars(true)));
                        break;
                    case nameof(EstateSeriDaftarLookupItem.SSAACode):
                        query = query.Where(x => x.SsaaCode.Contains(filter.Value));
                        break;
                }
            }

            if (!string.IsNullOrWhiteSpace(globalSearch))
            {
                query = query.Where(x => (x.Code.Contains(globalSearch) ||
                                    x.SsaaCode.Contains(globalSearch) ||
                                    x.Title.Contains(globalSearch.NormalizeTextChars(true))
                                     )
                                  );

            }


            result.TotalCount = await query
               .CountAsync(cancellationToken);


            if (pageIndex == 1 && selectedItemsIds.Count > 0)
            {
                result.SelectedItems = await TableNoTracking.Where(p => selectedItemsIds.Contains(p.Id) || selectedItemsIds.Contains(p.LegacyId))
                .Select(y => new EstateSeriDaftarLookupItem() { Code = y.Code, Id = y.Id, Title = y.Title, SSAACode = y.SsaaCode })
                .ToListAsync(cancellationToken);
            }

            if (result.TotalCount > 0)
            {
                if (isOrderBy)
                {

                    result.GridItems = await query
                    .OrderBy($"{gridSortInput.Sort} {gridSortInput.SortType} ")
                    .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                    .Select(y => new EstateSeriDaftarLookupItem() { Code = y.Code, Id = y.Id, Title = y.Title, SSAACode = y.SsaaCode })
                        .ToListAsync(cancellationToken);

                }
                else
                {
                    result.GridItems = await query
                        .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                        .Select(y => new EstateSeriDaftarLookupItem() { Code = y.Code, Id = y.Id, Title = y.Title, SSAACode = y.SsaaCode })
                        .ToListAsync(cancellationToken);
                }
            }
            Helper.NormalizeStringValuesDeeply(result, false);
            return result;
        }
    }
}
