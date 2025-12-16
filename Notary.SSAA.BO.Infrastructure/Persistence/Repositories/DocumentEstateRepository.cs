using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Infrastructure.Contexts;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.Base;
using Notary.SSAA.BO.Utilities.Extensions;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;
using Notary.SSAA.BO.Domain.RichDomain.Document;

namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories
{
    public class DocumentEstateRepository : Repository<DocumentEstate>, IDocumentEstateRepository
    {
        public DocumentEstateRepository(ApplicationContext context) : base(context)
        {
        }
        public async Task<BaseLookupRepositoryObject> GetDocumentEstateLookupItems(int pageIndex, int pageSize, ICollection<SearchData> gridFilterInput, string globalSearch, SortData gridSortInput, IList<string> selectedItems, DocumentEstateLookupExtraParams extraParams, List<string> fieldsNotInFilterSearch, CancellationToken cancellationToken, bool isOrderBy = false)
        {
            BaseLookupRepositoryObject result = new();
            var initialQuery = TableNoTracking.Where(x => x.DocumentId == extraParams.DocumentId.ToGuid()).
                           Select(y => new BaseLookupItem
                           {
                               Id = y.Id.ToString(),
                               Code = y.PostalCode,
                               Title = y.RegCaseText(),
                           });


            string filterQueryString = LambdaString<BaseLookupRepositoryObject, SearchData>.CreateWhereLambdaString(new BaseLookupRepositoryObject(), gridFilterInput, globalSearch, fieldsNotInFilterSearch);

            if (pageIndex == 1 && selectedItems.Count > 0)
            {
                var selectedItemQuery = initialQuery.Where(p => selectedItems.Contains(p.Id));
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
