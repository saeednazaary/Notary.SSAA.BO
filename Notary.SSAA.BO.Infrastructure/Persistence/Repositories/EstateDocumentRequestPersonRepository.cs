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
    public class EstateDocumentRequestPersonRepository : Repository<EstateDocumentRequestPerson>, IEstateDocumentRequestPersonRepository
    {
        public EstateDocumentRequestPersonRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<EstateDocumentRequestPersonLookupRepositoryObject> GetEstateDocumentRequestAgentPersons(int pageIndex, int pageSize, ICollection<SearchData> gridSearchInput, string globalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> fieldsNotInFilterSearch, string estateDocumentRequestId, bool isOrderBy, CancellationToken cancellationToken)
        {

            EstateDocumentRequestPersonLookupRepositoryObject result = new();

            EstateDocumentRequestPersonLookupItem lookupFilterItem = new();

            var query = TableNoTracking
                .Where(x => x.DocumentRequestId == estateDocumentRequestId.ToGuid() && x.IsRelated == EstateConstant.BooleanConstant.True);

            foreach (SearchData filter in gridSearchInput)
            {
                if (string.IsNullOrWhiteSpace(filter.Value)) continue;
                var capitalCaseFilter = $"{filter.Filter[..1].ToUpper()}{filter.Filter[1..]}";
                switch (capitalCaseFilter)
                {
                    case nameof(EstateDocumentRequestPersonLookupItem.IdentityNo):
                        query = query.Where(p => p.IdentityNo.Contains(filter.Value));
                        break;
                    case nameof(EstateDocumentRequestPersonLookupItem.NationalNo):
                        query = query.Where(p => p.NationalNo.Contains(filter.Value));
                        break;
                    case nameof(EstateDocumentRequestPersonLookupItem.BirthDate):
                        query = query.Where(p => p.BirthDate.Contains(filter.Value));
                        break;
                    case nameof(EstateDocumentRequestPersonLookupItem.Family):
                        query = query.Where(p => p.Family.Contains(filter.Value.NormalizeTextChars(true)));
                        break;
                    case nameof(EstateDocumentRequestPersonLookupItem.FatherName):
                        query = query.Where(p => p.FatherName.Contains(filter.Value.NormalizeTextChars(true)));
                        break;
                    case nameof(EstateDocumentRequestPersonLookupItem.Name):
                        query = query.Where(p => p.Name.Contains(filter.Value.NormalizeTextChars(true)));
                        break;
                }

            }

            if (!string.IsNullOrWhiteSpace(globalSearch))
            {

                query = query.Where(p => (p.IdentityNo.Contains(globalSearch))
                        || (p.NationalNo.Contains(globalSearch))
                        || (p.BirthDate.Contains(globalSearch))
                        || (p.Family.Contains(globalSearch.NormalizeTextChars(true)))
                        || (p.FatherName.Contains(globalSearch.NormalizeTextChars(true)))
                        || (p.Name.Contains(globalSearch.NormalizeTextChars(true)))
                        );
            }
            result.TotalCount = await query
               .CountAsync(cancellationToken);

            if (pageIndex == 1 && selectedItemsIds.Count > 0)
            {
                var guidlist = new List<Guid>();
                foreach (var item in selectedItemsIds)
                {
                    guidlist.Add(item.ToGuid());
                }
                result.SelectedItems = await TableNoTracking
                    .Where(p => guidlist.Contains(p.Id)).
                Select(y => new EstateDocumentRequestPersonLookupItem()
                {
                    BirthDate = y.BirthDate,
                    Family = y.Family,
                    Name = y.Name,
                    FatherName = y.FatherName,
                    Id = y.Id.ToString(),
                    IdentityNo = y.IdentityNo,
                    NationalNo = y.NationalNo
                })

                .ToListAsync(cancellationToken);
            }
            if (result.TotalCount > 0)
            {
                if (isOrderBy)
                {

                    result.GridItems = await query
                        .Select(y => new EstateDocumentRequestPersonLookupItem()
                        {
                            BirthDate = y.BirthDate,
                            Family = y.Family,
                            Name = y.Name,
                            FatherName = y.FatherName,
                            Id = y.Id.ToString(),
                            IdentityNo = y.IdentityNo,
                            NationalNo = y.NationalNo
                        })
                        .OrderBy($"{gridSortInput.Sort} {gridSortInput.SortType} ")
                        .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                            .ToListAsync(cancellationToken);

                }
                else
                {
                    result.GridItems = await query
                        .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                        .Select(y => new EstateDocumentRequestPersonLookupItem()
                        {
                            BirthDate = y.BirthDate,
                            Family = y.Family,
                            Name = y.Name,
                            FatherName = y.FatherName,
                            Id = y.Id.ToString(),
                            IdentityNo = y.IdentityNo,
                            NationalNo = y.NationalNo
                        })
                        .ToListAsync(cancellationToken);
                }
            }
            foreach (var item in result.GridItems)
            {
                if (!string.IsNullOrWhiteSpace(item.Name))
                    item.Name = item.Name.NormalizeTextChars(false);
                if (!string.IsNullOrWhiteSpace(item.FatherName))
                    item.FatherName = item.FatherName.NormalizeTextChars(false);
                if (!string.IsNullOrWhiteSpace(item.Family))
                    item.Family = item.Family.NormalizeTextChars(false);
            }
            foreach (var item in result.SelectedItems)
            {
                if (!string.IsNullOrWhiteSpace(item.Name))
                    item.Name = item.Name.NormalizeTextChars(false);
                if (!string.IsNullOrWhiteSpace(item.FatherName))
                    item.FatherName = item.FatherName.NormalizeTextChars(false);
                if (!string.IsNullOrWhiteSpace(item.Family))
                    item.Family = item.Family.NormalizeTextChars(false);
            }

            return result;

        }

        public async Task<EstateDocumentRequestPersonLookupRepositoryObject> GetEstateDocumentRequestOwnerPerson(int pageIndex, int pageSize, ICollection<SearchData> gridSearchInput, string globalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> fieldsNotInFilterSearch, string estateDocumentRequestId, bool isOrderBy, CancellationToken cancellationToken)
        {

            EstateDocumentRequestPersonLookupRepositoryObject result = new();

            EstateDocumentRequestPersonLookupItem lookupFilterItem = new();

            var query = TableNoTracking
                .Where(x => x.DocumentRequestId == estateDocumentRequestId.ToGuid() && x.IsOriginal == EstateConstant.BooleanConstant.True);

            foreach (SearchData filter in gridSearchInput)
            {
                if (string.IsNullOrWhiteSpace(filter.Value)) continue;
                var capitalCaseFilter = $"{filter.Filter[..1].ToUpper()}{filter.Filter[1..]}";
                switch (capitalCaseFilter)
                {
                    case nameof(EstateDocumentRequestPersonLookupItem.IdentityNo):
                        query = query.Where(p => p.IdentityNo.Contains(filter.Value));
                        break;
                    case nameof(EstateDocumentRequestPersonLookupItem.NationalNo):
                        query = query.Where(p => p.NationalNo.Contains(filter.Value));
                        break;
                    case nameof(EstateDocumentRequestPersonLookupItem.BirthDate):
                        query = query.Where(p => p.BirthDate.Contains(filter.Value));
                        break;
                    case nameof(EstateDocumentRequestPersonLookupItem.Family):
                        query = query.Where(p => p.Family.Contains(filter.Value.NormalizeTextChars(true)));
                        break;
                    case nameof(EstateDocumentRequestPersonLookupItem.FatherName):
                        query = query.Where(p => p.FatherName.Contains(filter.Value.NormalizeTextChars(true)));
                        break;
                    case nameof(EstateDocumentRequestPersonLookupItem.Name):
                        query = query.Where(p => p.Name.Contains(filter.Value.NormalizeTextChars(true)));
                        break;
                }

            }

            if (!string.IsNullOrWhiteSpace(globalSearch))
            {

                query = query.Where(p => (p.IdentityNo.Contains(globalSearch))
                        || (p.NationalNo.Contains(globalSearch))
                        || (p.BirthDate.Contains(globalSearch))
                        || (p.Family.Contains(globalSearch.NormalizeTextChars(true)))
                        || (p.FatherName.Contains(globalSearch.NormalizeTextChars(true)))
                        || (p.Name.Contains(globalSearch.NormalizeTextChars(true)))
                        );
            }
            result.TotalCount = await query
               .CountAsync(cancellationToken);

            if (pageIndex == 1 && selectedItemsIds.Count > 0)
            {
                var guidlist = new List<Guid>();
                foreach (var item in selectedItemsIds)
                {
                    guidlist.Add(item.ToGuid());
                }
                result.SelectedItems = await TableNoTracking
                    .Where(p => guidlist.Contains(p.Id)).
                Select(y => new EstateDocumentRequestPersonLookupItem()
                {
                    BirthDate = y.BirthDate,
                    Family = y.Family,
                    Name = y.Name,
                    FatherName = y.FatherName,
                    Id = y.Id.ToString(),
                    IdentityNo = y.IdentityNo,
                    NationalNo = y.NationalNo
                })

                .ToListAsync(cancellationToken);
            }
            if (result.TotalCount > 0)
            {
                if (isOrderBy)
                {

                    result.GridItems = await query
                        .Select(y => new EstateDocumentRequestPersonLookupItem()
                        {
                            BirthDate = y.BirthDate,
                            Family = y.Family,
                            Name = y.Name,
                            FatherName = y.FatherName,
                            Id = y.Id.ToString(),
                            IdentityNo = y.IdentityNo,
                            NationalNo = y.NationalNo
                        })
                        .OrderBy($"{gridSortInput.Sort} {gridSortInput.SortType} ")
                        .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                            .ToListAsync(cancellationToken);

                }
                else
                {
                    result.GridItems = await query
                        .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                        .Select(y => new EstateDocumentRequestPersonLookupItem()
                        {
                            BirthDate = y.BirthDate,
                            Family = y.Family,
                            Name = y.Name,
                            FatherName = y.FatherName,
                            Id = y.Id.ToString(),
                            IdentityNo = y.IdentityNo,
                            NationalNo = y.NationalNo
                        })
                        .ToListAsync(cancellationToken);
                }
            }

            foreach (var item in result.GridItems)
            {
                if (!string.IsNullOrWhiteSpace(item.Name))
                    item.Name = item.Name.NormalizeTextChars(false);
                if (!string.IsNullOrWhiteSpace(item.FatherName))
                    item.FatherName = item.FatherName.NormalizeTextChars(false);
                if (!string.IsNullOrWhiteSpace(item.Family))
                    item.Family = item.Family.NormalizeTextChars(false);
            }
            foreach (var item in result.SelectedItems)
            {
                if (!string.IsNullOrWhiteSpace(item.Name))
                    item.Name = item.Name.NormalizeTextChars(false);
                if (!string.IsNullOrWhiteSpace(item.FatherName))
                    item.FatherName = item.FatherName.NormalizeTextChars(false);
                if (!string.IsNullOrWhiteSpace(item.Family))
                    item.Family = item.Family.NormalizeTextChars(false);
            }

            return result;

        }
    }
}
