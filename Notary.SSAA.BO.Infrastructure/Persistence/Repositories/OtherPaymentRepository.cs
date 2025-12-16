using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;
using Notary.SSAA.BO.Infrastructure.Contexts;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.Base;
using Notary.SSAA.BO.Utilities.Extensions;
using System.Linq.Dynamic.Core;

namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories
{
    public class OtherPaymentRepository : Repository<OtherPayment>, IOtherPaymentRepository
    {
        public OtherPaymentRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<string> GetMaxNationalNo(string beginNationalNo, CancellationToken cancellationToken)
        {
            return await TableNoTracking.Where(x => x.NationalNo.StartsWith(beginNationalNo)).Select(x => x.NationalNo).MaxAsync(cancellationToken);
        }

        public async Task<OtherPaymentGrid> GetOtherPaymentGridItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, List<SortData> gridSortInputDefaults, SortData gridSortInput, string GlobalSearch, IList<Guid> selectedItemsIds, List<string> FieldsNotInFilterSearch, string scriptoriumId, CancellationToken cancellationToken, bool isOrderBy = false)
        {
            OtherPaymentGrid result = new();
            var initialQuery = TableNoTracking.Where(x => x.ScriptoriumId == scriptoriumId).Include(x => x.OtherPaymentsType).Select(x => new OtherPaymentGridItem
            {
                Id = x.Id.ToString(),
                CreateDate = x.CreateDate,
                CreateTime = x.CreateTime,
                Description = x.Description,
                Fee = x.Fee != null ? x.Fee.ToString() : null,
                ItemCount = x.ItemCount != null ? x.ItemCount.ToString() : null,
                NationalNo = x.NationalNo,
                OtherPaymentsTypeId = x.OtherPaymentsTypeId,
                OtherPaymentsTypeTitle = x.OtherPaymentsType.Title,
                SumPrices =  x.SumPrices.ToString()
            });
            if (pageIndex == 1 && selectedItemsIds.Any())
            {
                var selectedItemQuery = TableNoTracking.Where(x => x.ScriptoriumId == scriptoriumId && selectedItemsIds.Contains(x.Id)).Include(x => x.OtherPaymentsType).Select(x => new OtherPaymentGridItem
                {
                    Id = x.Id.ToString(),
                    CreateDate = x.CreateDate,
                    CreateTime = x.CreateTime,
                    Description = x.Description,
                    Fee = x.Fee != null ? x.Fee.ToString() : null,
                    ItemCount = x.ItemCount != null ? x.ItemCount.ToString() : null,
                    NationalNo = x.NationalNo,
                    OtherPaymentsTypeId = x.OtherPaymentsTypeId,
                    OtherPaymentsTypeTitle = x.OtherPaymentsType.Title,
                    SumPrices = x.SumPrices.ToString(),
                    IsSelected = true
                });
                result.SelectedItems = await selectedItemQuery.ToListAsync(cancellationToken);
            }
            string filterQueryString = LambdaString<OtherPaymentGridItem, SearchData>.CreateWhereLambdaString(new OtherPaymentGridItem(), GridSearchInput, GlobalSearch, FieldsNotInFilterSearch);
            if (!string.IsNullOrWhiteSpace(filterQueryString))
                initialQuery = initialQuery.Where(filterQueryString.PersianToArabic());
            if (isOrderBy)
            {
                if (gridSortInput != null)
                {
                    initialQuery = initialQuery.OrderBy($"{gridSortInput.Sort} {gridSortInput.SortType}");
                }
                else if (gridSortInputDefaults != null)
                {
                    initialQuery = initialQuery.OrderBy($"{gridSortInputDefaults[0].Sort} {gridSortInputDefaults[0].SortType}, {gridSortInputDefaults[1].Sort} {gridSortInputDefaults[1].SortType}");
                }
            }
            result.GridItems = await initialQuery.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
            result.TotalCount = await initialQuery.CountAsync(cancellationToken);
            return result;
        }
    }
}
