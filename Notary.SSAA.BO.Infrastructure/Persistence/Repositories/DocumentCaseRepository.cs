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
    internal sealed class DocumentCaseRepository : Repository<DocumentCase>, IDocumentCaseRepository
    {
        public DocumentCaseRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }
        public async Task<DocumentCaseGrid> GetDocumentCaseGridItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch, SortData gridSortInput, IList<Guid> selectedItemsIds, List<string> FieldsNotInFilterSearch, string scriptoriumId, DocumentCaseExtraParams extraParams, CancellationToken cancellationToken, bool isOrderBy = false)
        {
            DocumentCaseGrid result = new();
            var initialQuery = TableNoTracking
                        .Where(x => x.ScriptoriumId == scriptoriumId)
                        .Select(y =>
                        new DocumentCaseGridItem()
                        {
                            Id = y.Id.ToString(),
                            Title = y.Title,
                            DocumentId = y.DocumentId.ToString(),
                            Description = y.Description,
                        });

            var selectedItemQuery = TableNoTracking
                        .Where(x => x.ScriptoriumId == scriptoriumId && selectedItemsIds.Contains(x.Id))
                        .Select(y =>
                        new DocumentCaseGridItem()
                        {
                            Id = y.Id.ToString(),
                            Title = y.Title,
                            DocumentId = y.DocumentId.ToString(),
                            Description = y.Description,
                        });

            string filterQueryString = LambdaString<DocumentGridItem, SearchData>.CreateWhereLambdaString(new DocumentGridItem(), GridSearchInput, GlobalSearch, FieldsNotInFilterSearch);

            if (pageIndex == 1 && selectedItemsIds.Count > 0)
                result.SelectedItems = await selectedItemQuery.ToListAsync(cancellationToken);

            if (!string.IsNullOrWhiteSpace(filterQueryString))
                initialQuery = initialQuery.Where(filterQueryString.PersianToArabic());

            if (isOrderBy)
                initialQuery = initialQuery.OrderBy($"{gridSortInput.Sort} {gridSortInput.SortType} ");


            result.GridItems = await initialQuery.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            result.TotalCount = await initialQuery.CountAsync(cancellationToken);

            return result;

        }

       
    }
}
