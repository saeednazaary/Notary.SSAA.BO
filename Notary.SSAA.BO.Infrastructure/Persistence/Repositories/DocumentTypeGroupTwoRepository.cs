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
    internal sealed class DocumentTypeGroupTwoRepository : Repository<DocumentTypeGroup2>, IDocumentTypeGroupTwoRepository
    {
        public DocumentTypeGroupTwoRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<BaseLookupRepositoryObject> GetDocumentTypeLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch,
            SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, DocumentTypegroupTwoExtraParams extraParams, CancellationToken cancellationToken, bool isOrderBy = false)
        {
            BaseLookupRepositoryObject result = new();

            var initialQuery = TableNoTracking.Where(x => x.State == State.Valid );

            if (extraParams is not null)
            {
                if (!extraParams.DocumentTypeGroupOneId.IsNullOrWhiteSpace())
                    initialQuery = initialQuery.Where(x => x.DocumentTypeGroup1Id == extraParams.DocumentTypeGroupOneId);
                if (extraParams.IsSupportive!=null)
                {
                initialQuery = initialQuery.Where(x => x.IsSupportive == extraParams.IsSupportive.ToYesNo());

                }
                else
                {
                    initialQuery = initialQuery.Where(x => x.IsSupportive == "2");
                }
            }
            else
            {
                initialQuery = initialQuery.Where(x => x.IsSupportive == "2");
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
                var selectedItemQuery = initialQuery.Where(x => x.State == State.Valid  ).Where(p => selectedItemsIds.Contains(p.Id)).
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
