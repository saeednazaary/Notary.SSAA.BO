using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;
using Notary.SSAA.BO.Infrastructure.Contexts;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.Base;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using Notary.SSAA.BO.Utilities.Extensions;
using System.Linq.Dynamic.Core;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories
{
    internal class DocumentPersonRepository : Repository<DocumentPerson>, IDocumentPersonRepository
    {
        public DocumentPersonRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<DocumentPersonLookupRepositoryObject> GetDocumentRelatedPersonLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch,
            SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, DocumentPersonExteraParams ExtraParams, CancellationToken cancellationToken, bool isOrderBy = false)
        {



            DocumentPersonLookupRepositoryObject result = new();

            var initialQuery = TableNoTracking;

            if (ExtraParams is not null)
            {
                if (!string.IsNullOrWhiteSpace(ExtraParams.DocumentId))
                {
                    Guid documentId = Guid.Parse(ExtraParams.DocumentId);
                    initialQuery = initialQuery.Where(x => x.DocumentId == documentId);

                }
                else
                    return result;
                //List<Guid> extraParamsId = ExtraParams.Fields.Select(Guid.Parse).ToList();
                //initialQuery = initialQuery.Where(x => extraParamsId.Contains(x.Id)); 
                if (!ExtraParams.IsLoadAllPerson)
                {
                    initialQuery = initialQuery.Where(x => x.IsRelated == "1");

                }
                //if (ExtraParams.IsMainPersonLookup)
                //{
                if (!string.IsNullOrWhiteSpace(ExtraParams.PersonId))
                {
                    initialQuery = initialQuery.Where(p => p.Id != Guid.Parse(ExtraParams.PersonId));
                }

                //}
                //else
                //{
                //    if (!string.IsNullOrWhiteSpace(ExtraParams.MainPersonId))
                //    {
                //        initialQuery = initialQuery.Where(p => p.Id != Guid.Parse(ExtraParams.MainPersonId));
                //    }
                //}
            }
            else
            {
                return result;
            }

            var query = initialQuery.Select(y => new DocumentPersonLookupItem
            {
                Family = y.Family,
                Id = y.Id.ToString(),
                Name = y.Name,
                NationalNo = y.NationalNo,

            });
            string filterQueryString = LambdaString<DocumentPersonLookupItem, SearchData>.CreateWhereLambdaString(new DocumentPersonLookupItem(), GridSearchInput, GlobalSearch, FieldsNotInFilterSearch);

            if (pageIndex == 1 && selectedItemsIds.Count > 0)
            {



                var selectedItems = await initialQuery.ToListAsync(cancellationToken);

                // Filter and project the data in memory
                var selectedItemQuery = selectedItems
                    .Where(p => selectedItemsIds.Contains(p.Id.ToString()))
                    .Select(y => new DocumentPersonLookupItem
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

            return result;






        }


        public async Task<DocumentPersonLookupRepositoryObject> GetDocumentOriginalPersonLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch,
          SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, DocumentPersonExteraParams ExtraParams, CancellationToken cancellationToken, bool isOrderBy = false)
        {



            DocumentPersonLookupRepositoryObject result = new();

            var initialQuery = TableNoTracking;

            if (ExtraParams is not null)
            {
                if (!string.IsNullOrWhiteSpace(ExtraParams.DocumentId))
                {
                    Guid documentId = Guid.Parse(ExtraParams.DocumentId);
                    initialQuery = initialQuery.Where(x => x.DocumentId == documentId);

                }
                else
                    return result;
                //List<Guid> extraParamsId = ExtraParams.Fields.Select(Guid.Parse).ToList();
                //initialQuery = initialQuery.Where(x => extraParamsId.Contains(x.Id)); 
                initialQuery = initialQuery.Where(x => (x.IsOriginal == "1"));
                //if (ExtraParams.IsMainPersonLookup)
                //{
                if (!string.IsNullOrWhiteSpace(ExtraParams.PersonId))
                {
                    initialQuery = initialQuery.Where(p => p.Id != Guid.Parse(ExtraParams.PersonId));
                }

                //}
                //else
                //{
                //    if (!string.IsNullOrWhiteSpace(ExtraParams.MainPersonId))
                //    {
                //        initialQuery = initialQuery.Where(p => p.Id != Guid.Parse(ExtraParams.MainPersonId));
                //    }
                //}
            }
            else
            {
                return result;
            }

            var query = initialQuery.Select(y => new DocumentPersonLookupItem
            {
                Family = y.Family,
                Id = y.Id.ToString(),
                Name = y.Name,
                NationalNo = y.NationalNo,
            });
            string filterQueryString = LambdaString<DocumentPersonLookupItem, SearchData>.CreateWhereLambdaString(new DocumentPersonLookupItem(), GridSearchInput, GlobalSearch, FieldsNotInFilterSearch);

            if (pageIndex == 1 && selectedItemsIds.Count > 0)
            {



                var selectedItems = await initialQuery.ToListAsync(cancellationToken);

                // Filter and project the data in memory
                var selectedItemQuery = selectedItems
                    .Where(p => selectedItemsIds.Contains(p.Id.ToString()))
                    .Select(y => new DocumentPersonLookupItem
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

            return result;






        }
        public async Task<DocumentDetailPersonLookupRepositoryObject> GetDocumentPersonLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch,
          SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, DocumentDetailPersonExteraParams ExtraParams, CancellationToken cancellationToken, bool isOrderBy = false)
        {

            DocumentDetailPersonLookupRepositoryObject result = new();

            var initialQuery = TableNoTracking;
            initialQuery = initialQuery.Include(x => x.DocumentPersonType);
            if (ExtraParams is not null)
            {
                if (!string.IsNullOrWhiteSpace(ExtraParams.DocumentId))
                {
                    Guid documentId = Guid.Parse(ExtraParams.DocumentId);
                    initialQuery = initialQuery.Where(x => x.DocumentId == documentId);

                }
                else
                    return result;
                if (!string.IsNullOrWhiteSpace(ExtraParams.DocumentPersonTypeId))
                {
                    initialQuery = initialQuery.Where(p => p.DocumentPersonType.Id == ExtraParams.DocumentPersonTypeId);
                }
                if (!string.IsNullOrWhiteSpace(ExtraParams.IsOwner))
                {
                    initialQuery = initialQuery.Where(p => p.DocumentPersonType.IsOwner == ExtraParams.IsOwner);
                }
                if (!string.IsNullOrWhiteSpace(ExtraParams.IsPersonOriginal))
                {
                    initialQuery = initialQuery.Where(p => p.IsOriginal == ExtraParams.IsPersonOriginal);
                }
                if (!string.IsNullOrWhiteSpace(ExtraParams.IsPersonAlive))
                {
                    initialQuery = initialQuery.Where(p => p.IsAlive == ExtraParams.IsPersonAlive);
                }
                    if (ExtraParams.DontIncludePeopleId != null && ExtraParams.DontIncludePeopleId.Count > 0)
                {
                    List<Guid> guidList = ExtraParams.DontIncludePeopleId
                        .Select(id => Guid.TryParse(id, out var guid) ? guid : (Guid?)null)
                        .Where(guid => guid.HasValue)
                        .Select(guid => guid.Value)
                        .ToList();

                    if (guidList.Count > 0)
                    {
                        initialQuery = initialQuery.Where(person => !guidList.Contains(person.Id));
                    }
                }

            }
            else
            {
                return result;
            }

            var query = initialQuery.Select(y => new DocumentDetailPersonLookupItem
            {
                Family = y.Family,
                Id = y.Id.ToString(),
                Name = y.Name,
                NationalNo = y.NationalNo,
                DocumentPersonTypeId = y.DocumentPersonType.Id,
                DocumentPersonTypeTitle = y.DocumentPersonType.SingularTitle,
            });
            string filterQueryString = LambdaString<DocumentDetailPersonLookupItem, SearchData>.CreateWhereLambdaString(new DocumentDetailPersonLookupItem(), GridSearchInput, GlobalSearch, FieldsNotInFilterSearch);

            if (pageIndex == 1 && selectedItemsIds.Count > 0)
            {

                var selectedItems = await initialQuery.ToListAsync(cancellationToken);

                // Filter and project the data in memory
                var selectedItemQuery = selectedItems
          .Where(y => y != null && selectedItemsIds.Contains(y.Id.ToString()))
          .Select(y => new DocumentDetailPersonLookupItem
          {
              Family = y.Family,
              Id = y.Id.ToString(),
              Name = y.Name,
              NationalNo = y.NationalNo,
              DocumentPersonTypeId = y.DocumentPersonType?.Id,
              DocumentPersonTypeTitle = y.DocumentPersonType?.SingularTitle,
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


        public async Task<DocumentPersonOwnerShipLookupRepositoryObject> GetDocumentPersonOwnerShipLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch,
  SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, DocumentPersonOwnerShipExtraParams ExtraParams, CancellationToken cancellationToken, bool isOrderBy = false)
        {

            DocumentPersonOwnerShipLookupRepositoryObject result = new();

            var initialQuery = TableNoTracking;
            initialQuery = initialQuery.Include(x => x.DocumentPersonType);
            if (ExtraParams is not null && !string.IsNullOrWhiteSpace(ExtraParams.DocumentId))
            {
                Guid documentId = Guid.Parse(ExtraParams.DocumentId);
                initialQuery = initialQuery.Where(x => x.DocumentId == documentId);
            }
            else
            {
                return result;
            }

            var query = initialQuery.Select(y => new DocumentPersonOwnerShipLookupItem
            {
                Family = y.Family,
                Id = y.Id.ToString(),
                Name = y.Name,
                NationalNo = y.NationalNo,
                TheONotaryRegServicePersonType = y.DocumentPersonType.SingularTitle,
                HasSmartCard = y.HasSmartCard.ToYesNoTitle(),
                IsOriginalPerson = y.IsOriginal.ToYesNoTitle(),
                TfaState =((TfaState)y.TfaState.ToRequiredInt()).GetDescription(),
                State =((State)y.DocumentPersonType.State.ToRequiredInt()).GetDescription(),
                IsRequired = y.DocumentPersonType.IsRequired.ToYesNoTitle(),
                ProhibitionCheckingRequired = y.DocumentPersonType.IsProhibitionCheckRequired.ToYesNoTitle(),

            });
            string filterQueryString = LambdaString<DocumentPersonOwnerShipLookupItem, SearchData>.CreateWhereLambdaString(new DocumentPersonOwnerShipLookupItem(), GridSearchInput, GlobalSearch, FieldsNotInFilterSearch);

            if (pageIndex == 1 && selectedItemsIds.Count > 0)
            {

                var selectedItems = await initialQuery.ToListAsync(cancellationToken);

                // Filter and project the data in memory
                var selectedItemQuery = selectedItems
                .Where(p => p != null && selectedItemsIds.Contains(p.Id.ToString()))
                .Select(y => new DocumentPersonOwnerShipLookupItem
                {
                    Family = y.Family ?? "",
                    Id = y.Id.ToString(),
                    Name = y.Name ?? "",
                    NationalNo = y.NationalNo ?? "",
                    TheONotaryRegServicePersonType = y.DocumentPersonType != null ? y.DocumentPersonType.SingularTitle ?? "" : "",
                    HasSmartCard = y.HasSmartCard.ToYesNoTitle(),
                    IsOriginalPerson = y.IsOriginal.ToYesNoTitle(),
                    TfaState = ((TfaState)y.TfaState.ToRequiredInt()).GetDescription(),
                    State = y.DocumentPersonType != null ? ((State)y.DocumentPersonType.State.ToRequiredInt()).GetDescription() : "",
                    IsRequired = y.DocumentPersonType != null ? y.DocumentPersonType.IsRequired.ToYesNoTitle() : "",
                    ProhibitionCheckingRequired = y.DocumentPersonType != null ? y.DocumentPersonType.IsProhibitionCheckRequired.ToYesNoTitle() : "",
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



        public async Task<List<DocumentPerson>> GetDocumentPersonPostTypeAsync(Guid documentId, string personTypeId, CancellationToken cancellationToken)
        {
            return await TableNoTracking.Include(x => x.DocumentPersonType).Include(x => x.Document).Where(x => x.DocumentId == documentId && x.DocumentPersonTypeId == personTypeId).ToListAsync(cancellationToken);
        }

        public async Task<DocumentPerson> GetDocumentPersonById(Guid id, List<string> details, CancellationToken cancellationToken)
        {
            var query = TableNoTracking.Where(x => x.Id == id).AsQueryable();

            foreach (var item in details)
            {
                if (item == "DocumentPersonRelatedAgentPeople")
                {
                    query = query.Include(x => x.DocumentPersonRelatedAgentPeople);
                }
            }

            DocumentPerson documentPerson = await query.FirstOrDefaultAsync(cancellationToken);
            return documentPerson;
        }
    }
}
