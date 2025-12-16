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
    public class SignRequestPersonRepository : Repository<SignRequestPerson>, ISignRequestPersonRepository
    {
        public SignRequestPersonRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<SignRequestAgentPersonLookupRepositoryObject> GetSignRequestRelatedPersonLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch,
      SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, SignRequestAgentPersonExtraParams ExtraParams,string scriptoriumId, CancellationToken cancellationToken, bool isOrderBy = false)
        {
            SignRequestAgentPersonLookupRepositoryObject result = new();

            if (ExtraParams is not null)
            {
                if (!string.IsNullOrWhiteSpace(ExtraParams.SignRequestId))
                {
                    var initialQuery = TableNoTracking.Where(x=>x.ScriptoriumId==scriptoriumId);
                    initialQuery = initialQuery.Where(x => x.SignRequestId == Guid.Parse(ExtraParams.SignRequestId));
                    initialQuery = initialQuery.Where(x => x.IsRelated == "1");
                    if (!string.IsNullOrWhiteSpace(ExtraParams.PersonId))
                        initialQuery = initialQuery.Where(p => p.Id != Guid.Parse(ExtraParams.PersonId));






                    var query = initialQuery.Select(y => new SignRequestAgentPersonLookupItem
                    {
                        Family = y.Family,
                        Id = y.Id.ToString(),
                        Name = y.Name,
                        NationalNo = y.NationalNo,
                    });
                    string filterQueryString = LambdaString<SignRequestAgentPersonLookupItem, SearchData>.CreateWhereLambdaString(new SignRequestAgentPersonLookupItem(), GridSearchInput, GlobalSearch, FieldsNotInFilterSearch);

                    if (pageIndex == 1 && selectedItemsIds.Count > 0)
                    {



                        var selectedItems = await initialQuery.ToListAsync(cancellationToken);

                        // Filter and project the data in memory
                        var selectedItemQuery = selectedItems
                            .Where(p => selectedItemsIds.Contains(p.Id.ToString()))
                            .Select(y => new SignRequestAgentPersonLookupItem
                            {
                                Family = y.Family,
                                Id = y.Id.ToString(),
                                Name = y.Name,
                                NationalNo = y.NationalNo,
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


                }
            }
            return result;


        }


        public async Task<SignRequestAgentPersonLookupRepositoryObject> GetSignRequestOriginalPersonLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch,
          SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, SignRequestAgentPersonExtraParams ExtraParams, CancellationToken cancellationToken, bool isOrderBy = false)
        {

            SignRequestAgentPersonLookupRepositoryObject result = new();

            if (ExtraParams is not null)
            {

                if (!string.IsNullOrWhiteSpace(ExtraParams.SignRequestId))
                {
                    var initialQuery = TableNoTracking;

                    initialQuery = initialQuery.Where(x => x.SignRequestId == Guid.Parse(ExtraParams.SignRequestId));

                    initialQuery = initialQuery.Where(x => x.IsOriginal == "1");

                    if (!string.IsNullOrWhiteSpace(ExtraParams.PersonId))

                        initialQuery = initialQuery.Where(p => p.Id != Guid.Parse(ExtraParams.PersonId));

                    var query = initialQuery.Select(y => new SignRequestAgentPersonLookupItem
                    {
                        Family = y.Family,
                        Id = y.Id.ToString(),
                        Name = y.Name,
                        NationalNo = y.NationalNo,
                    });
                    string filterQueryString = LambdaString<SignRequestAgentPersonLookupItem, SearchData>.CreateWhereLambdaString(new SignRequestAgentPersonLookupItem(), GridSearchInput, GlobalSearch, FieldsNotInFilterSearch);

                    if (pageIndex == 1 && selectedItemsIds.Count > 0)
                    {



                        var selectedItems = await initialQuery.ToListAsync(cancellationToken);

                        // Filter and project the data in memory
                        var selectedItemQuery = selectedItems
                            .Where(p => selectedItemsIds.Contains(p.Id.ToString()))
                            .Select(y => new SignRequestAgentPersonLookupItem
                            {
                                Family = y.Family,
                                Id = y.Id.ToString(),
                                Name = y.Name,
                                NationalNo = y.NationalNo,
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

                }
            }
            return result;

        }

    }
}
