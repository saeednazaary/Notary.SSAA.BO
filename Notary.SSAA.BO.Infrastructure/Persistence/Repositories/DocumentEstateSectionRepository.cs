using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Infrastructure.Contexts;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.Base;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories
{
    public class DocumentEstateSectionRepository:Repository<EstateSection> , IDocumentEstateSectionRepository
    {
        public DocumentEstateSectionRepository(ApplicationContext context) : base(context)
        {

        }
        public async Task<BaseLookupRepositoryObject> GetDocumentEstateSectionLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch,
            SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch
            , CancellationToken cancellationToken, bool isOrderBy = false)
        {

            BaseLookupRepositoryObject result = new();
            var initialQuery = TableNoTracking.Where(x => x.State == "1").
                           Select(y => new BaseLookupItem
                           {
                               Id = y.Id.ToString(),
                               Code = y.Code,
                               Title = y.Title,
                           });


            string filterQueryString = LambdaString<BaseLookupRepositoryObject, SearchData>.CreateWhereLambdaString(new BaseLookupRepositoryObject(), GridSearchInput, GlobalSearch, FieldsNotInFilterSearch);

            if (pageIndex == 1 && selectedItemsIds.Count > 0)
            {
                var selectedItemQuery = initialQuery.Where(p => selectedItemsIds.Contains(p.Id));
                result.SelectedItems = await selectedItemQuery.ToListAsync(cancellationToken);
            }


            if (!string.IsNullOrWhiteSpace(filterQueryString))
                initialQuery = initialQuery.Where(filterQueryString);

            if (isOrderBy)
                initialQuery = initialQuery.OrderBy($"{gridSortInput.Sort} {gridSortInput.SortType} ");

            result.GridItems = await initialQuery.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            result.TotalCount = await initialQuery.CountAsync(cancellationToken);

            return result;
        }

    }
}
