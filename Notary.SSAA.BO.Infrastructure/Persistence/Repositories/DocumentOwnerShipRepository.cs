using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;
using Notary.SSAA.BO.Domain.RichDomain.Document;
using Notary.SSAA.BO.Infrastructure.Contexts;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.Base;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using Notary.SSAA.BO.Utilities.Extensions;
using System.Linq.Dynamic.Core;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories
{
    internal class DocumentOwnerShipRepository : Repository<DocumentEstateOwnershipDocument>, IDocumentOwnerShipRepository
    {
        public DocumentOwnerShipRepository(ApplicationContext context) : base(context)
        {
        }



        public async Task<DocumentOwnerShipLookupRepositoryObject> GetDocumentOwnerShipLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch,
SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, DocumentOwnerShipExtraParams ExtraParams, CancellationToken cancellationToken, bool isOrderBy = false)
        {

            DocumentOwnerShipLookupRepositoryObject result = new();
            var initialQuery = TableNoTracking;
            initialQuery = initialQuery.Include(x => x.DocumentPerson).ThenInclude(x => x.DocumentPersonType);
            if (ExtraParams is not null && !string.IsNullOrWhiteSpace(ExtraParams.DocumentEstateId))
            {
                Guid documentEstateId = Guid.Parse(ExtraParams.DocumentEstateId);
                initialQuery = initialQuery.Where(x => x.DocumentEstateId == documentEstateId);
            }
            else
            {
                return result;
            }

            var query = initialQuery.Select(y => new DocumentOwnerShipLookupItem
            {
                Family = y.DocumentPerson.Family,
                Id = y.Id.ToString(),
                Name = y.DocumentPerson.Name,
                OwnershipDocumentType = ((NotaryOwnershipDocumentType)y.OwnershipDocumentType.ToRequiredInt()).GetDescription(),
               // OwnershipDocTitle = y.OwnershipDocTitle(),
                HasSmartCard = y.DocumentPerson.HasSmartCard.ToYesNoTitle(),
                NationalNo = y.DocumentPerson.NationalNo,
                IsOriginalPerson = y.DocumentPerson.IsOriginal.ToYesNoTitle(),
                TfaState = ((TfaState)y.DocumentPerson.TfaState.ToRequiredInt()).GetDescription(),
                TheONotaryRegServicePersonType = y.DocumentPerson.DocumentPersonType.SingularTitle

            });
            string filterQueryString = LambdaString<DocumentOwnerShipLookupItem, SearchData>.CreateWhereLambdaString(new DocumentOwnerShipLookupItem(), GridSearchInput, GlobalSearch, FieldsNotInFilterSearch);

            if (pageIndex == 1 && selectedItemsIds.Count > 0)
            {

                var selectedItems = await initialQuery.ToListAsync(cancellationToken);

                // Filter and project the data in memory
                var selectedItemQuery = selectedItems
                    .Where(p => selectedItemsIds.Contains(p.Id.ToString()))
                    .Select(y => new DocumentOwnerShipLookupItem
                    {
                        Family = y.DocumentPerson.Family,
                        Id = y.Id.ToString(),
                        Name = y.DocumentPerson==null?"":  y.DocumentPerson.Name,
                        OwnershipDocumentType =((NotaryOwnershipDocumentType)y.OwnershipDocumentType.ToNullableInt()).GetDescription(),
                       // OwnershipDocTitle = y.OwnershipDocTitle(),
                        HasSmartCard = y.DocumentPerson==null?"": y.DocumentPerson.HasSmartCard.ToYesNoTitle(),
                        NationalNo = y.DocumentPerson == null ? "" : y.DocumentPerson.NationalNo,
                        IsOriginalPerson = y.DocumentPerson == null ? "" : y.DocumentPerson.IsOriginal.ToYesNoTitle(),
                       // TfaState = y.DocumentPerson == null ? "" : ((TfaState)y.DocumentPerson.TfaState.ToNullableInt()).GetDescription(),
                        TheONotaryRegServicePersonType = y.DocumentPerson == null ? "" :(y.DocumentPerson.DocumentPersonType == null ? "" : y.DocumentPerson.DocumentPersonType.SingularTitle)
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


        public async Task<List<DocumentEstateOwnershipDocument>> GetOwnersFromRecentDeterministicRegServiceReq ( string scriptoriumId, string EstateInquiryId, CancellationToken cancellationToken )
        {
            List<string> codes = new List<string>() { "111", "115" };
            List<string> states = new List<string>() { "5", "6", "7" };

            return await TableNoTracking.Include ( t => t.DocumentEstate ).ThenInclude ( t => t.Document )
                .Where ( t => t.ScriptoriumId == scriptoriumId && t.EstateInquiriesId == EstateInquiryId
                                                               && codes.Contains ( t.DocumentEstate.Document
                                                                   .DocumentTypeId ) &&
                                                               states.Contains ( t.DocumentEstate.Document.State ) )
                .OrderByDescending ( t => t.DocumentEstate.Document.DocumentDate ).ToListAsync ();

            // criteria.AddOrderBy ( Rad.CMS.NotaryOfficeQuery.ONotaryPersonOwnershipDoc.TheONotaryRegCase.TheONotaryRegisterServiceReq.DocDate, false );


        }

    }
}
