using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.SignRequest;
using Notary.SSAA.BO.Infrastructure.Contexts;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.Base;
using Notary.SSAA.BO.Utilities.Extensions;
using System.Linq.Dynamic.Core;

namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories
{
    public class SignRequestSubjectRepository : Repository<SignRequestSubject>, ISignRequestSubjectRepository
    {
        public SignRequestSubjectRepository(ApplicationContext context) : base(context)
        {

        }

        public async Task<SignRequestSubjectLookupRepositoryObject> GetSignRequestSubjectLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput,
            string GlobalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch,
            CancellationToken cancellationToken, bool isOrderBy = false)
        {
            SignRequestSubjectLookupRepositoryObject result = new();

            var initialQuery = TableNoTracking.Include(x=>x.SignRequestSubjectGroup).Where(x=>x.State=="1");

            var query = initialQuery.Select(y => new SignRequestSubjectLookupItem
            {
                Title = y.Title,
                Id = y.Id.ToString(),
                Code = y.Code,
                SubjectGroupTitle = y.SignRequestSubjectGroup.Title
            });
            string filterQueryString = LambdaString<SignRequestSubjectLookupItem, SearchData>.CreateWhereLambdaString(new SignRequestSubjectLookupItem(), GridSearchInput, GlobalSearch, FieldsNotInFilterSearch);

            if (pageIndex == 1 && selectedItemsIds.Count > 0)
            {
                var selectedItemQuery = TableNoTracking.Where(x => x.State == "1");
                if (selectedItemsIds.Count == 1)
                {
                    selectedItemQuery = selectedItemQuery.Where(p => p.Id == selectedItemsIds.First());

                }
                else
                {
                    selectedItemQuery = selectedItemQuery.Where(p => selectedItemsIds.Contains(p.Id));
                }

                result.SelectedItems = await selectedItemQuery.Select(y => new SignRequestSubjectLookupItem
                {
                    Title = y.Title,
                    Id = y.Id.ToString(),
                    Code = y.Code,
                    SubjectGroupTitle=y.SignRequestSubjectGroup.Title
                }).ToListAsync(cancellationToken);
            }


            if (!string.IsNullOrWhiteSpace(filterQueryString))
                query = query.Where(filterQueryString);

            if (isOrderBy)
            {
                query = query.OrderBy($"{gridSortInput.Sort} {gridSortInput.SortType} ");
            }
            else
            {
                query = query.OrderBy(x => x.Title);
            }

            result.GridItems = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            result.TotalCount = await query.CountAsync(cancellationToken);

            return result;
        }



    }
}
