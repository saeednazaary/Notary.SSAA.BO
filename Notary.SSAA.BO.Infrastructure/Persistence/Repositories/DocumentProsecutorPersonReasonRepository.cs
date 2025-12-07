using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;
using Notary.SSAA.BO.Infrastructure.Contexts;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.Utilities.Extensions;
using System.Linq.Dynamic.Core;

namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories
{
    internal class DocumentProsecutorPersonReasonRepository : Repository<ReliablePersonReason>, IDocumentProsecutorPersonReasonRepository
    {
        public DocumentProsecutorPersonReasonRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<DocumentProsecutorPersonReasonLookupRepositoryObject> GetDocumentProsecutorPersonReasonLookupItems(int pageIndex, int pageSize,ICollection<SearchData> GridSearchInput, string GlobalSearch, 
            SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, CancellationToken cancellationToken, bool isOrderBy = false)
        {
            DocumentProsecutorPersonReasonLookupRepositoryObject result = new();
            var initialQuery = TableNoTracking.Where(x=>x.State==State.Valid).
                           Select(y => new BaseLookupItem 
                           { 
                               Title = y.Title, 
                               Id = y.Id.ToString(), 
                               Code = y.Code 
                           });
            var selectedItemQuery = initialQuery.Where(p => selectedItemsIds.Contains(p.Id));
            string filterQueryString = LambdaString<BaseLookupItem, SearchData>.CreateWhereLambdaString(new BaseLookupItem(), GridSearchInput, GlobalSearch, FieldsNotInFilterSearch);
            if (pageIndex == 1 && selectedItemsIds.Count > 0)
                result.SelectedItems = await selectedItemQuery.ToListAsync(cancellationToken);
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
