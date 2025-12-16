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
    internal class DocumentEstateTypeRepository : Repository<DocumentEstateType>, IDocumentEstateTypeRepository
    {
        public DocumentEstateTypeRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<BaseLookupRepositoryObject> GetDocumentEstateTypeLookupItems(int pageIndex, int pageSize,ICollection<SearchData> GridSearchInput, string GlobalSearch, 
            SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, DocumentEstateTypeExtraParams extraParams, CancellationToken cancellationToken, bool isOrderBy = false)
        {
            BaseLookupRepositoryObject result = new();
            var initialQuery = TableNoTracking.Where(x => x.State == "1");

            if (extraParams is not null)
            {
                if (!extraParams.DocumentEstateTypeGroupId.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => x.DocumentEstateTypeGroupId == extraParams.DocumentEstateTypeGroupId);
            }
            var query = initialQuery.Select(y => new BaseLookupItem
            {
                Title = y.Title,
                Id = y.Id.ToString(),
                Code = y.Code
            });
            string filterQueryString = LambdaString<BaseLookupItem, SearchData>.CreateWhereLambdaString(new BaseLookupItem(), GridSearchInput, GlobalSearch, FieldsNotInFilterSearch);

            if (pageIndex == 1 && selectedItemsIds.Count > 0)
            {
                var selectedItemQuery = TableNoTracking.Where(p => selectedItemsIds.Contains(p.Id) && p.State=="1").
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
