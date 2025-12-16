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
    internal class DocumentPersonTypeRepository : Repository<DocumentPersonType>, IDocumentPersonTypeRepository
    {
        public DocumentPersonTypeRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<DocumentPersonTypeLookupRepositoryObject> GetDocumentPersonTypeLookupItems(int pageIndex, int pageSize,ICollection<SearchData> GridSearchInput, string GlobalSearch, 
            SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, DocumentPersonTypeExtraParams extraParams, CancellationToken cancellationToken, bool isOrderBy = false)
        {




            DocumentPersonTypeLookupRepositoryObject result = new();

            var initialQuery = TableNoTracking.Where(x => x.State==State.Valid);

            if (extraParams is not null)
            {
              

                if (!string.IsNullOrWhiteSpace(extraParams.RequestTypeId))
                    initialQuery = initialQuery.Where(x => x.DocumentType.Id == extraParams.RequestTypeId);
                if (extraParams.IsOwner!=null)
                {
                    string extreaParamIsOwner = extraParams.IsOwner.ToYesNo();
                    initialQuery = initialQuery.Where(x => x.IsOwner == extreaParamIsOwner);
                }
            }

            var query = initialQuery.Select(y => new DocumentPersonTypeLookupItem
            {
                SingularTitle = y.SingularTitle,
                Id = y.Id.ToString(),
                Code = y.RowNoInForm.ToString(),
                IsOwner = y.IsOwner.ToNullabbleBoolean(),
                IsProhibitionCheckRequired = y.IsProhibitionCheckRequired.ToNullabbleBoolean(),
                IsRequired = y.IsRequired.ToNullabbleBoolean(),
                IsShahkarRequired = y.IsShahkarRequired.ToNullabbleBoolean(),
                IsSanaRequired = y.IsSanaRequired.ToNullabbleBoolean()
            });
            string filterQueryString = LambdaString<DocumentPersonTypeLookupItem, SearchData>.CreateWhereLambdaString(new DocumentPersonTypeLookupItem(), GridSearchInput, GlobalSearch, FieldsNotInFilterSearch);

            if (pageIndex == 1 && selectedItemsIds.Count > 0)
            {



                var selectedItems = await initialQuery.Where(x => x.State == State.Valid).ToListAsync(cancellationToken);

                // Filter and project the data in memory
                var selectedItemQuery = selectedItems
                    .Where(p => selectedItemsIds.Contains(p.Id.ToString()))
                    .Select(y => new DocumentPersonTypeLookupItem
                    {
                        SingularTitle = y.SingularTitle,
                        Id = y.Id.ToString(),
                        Code = y.RowNoInForm.ToString(),
                        IsOwner = y.IsOwner.ToNullabbleBoolean(),
                        IsProhibitionCheckRequired = y.IsProhibitionCheckRequired.ToNullabbleBoolean(),
                        IsRequired = y.IsRequired.ToNullabbleBoolean(),
                        IsShahkarRequired = y.IsShahkarRequired.ToNullabbleBoolean(),
                        IsSanaRequired = y.IsSanaRequired.ToNullabbleBoolean()
                    })
                    .ToList();

                result.SelectedItems = selectedItemQuery;
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
