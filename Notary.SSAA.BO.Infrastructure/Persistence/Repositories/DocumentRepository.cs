using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.IdentityModel.Tokens;
    using Notary.SSAA.BO.Domain.Abstractions;
    using Notary.SSAA.BO.Domain.Entities;
    using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
    using Notary.SSAA.BO.Domain.RepositoryObjects.Document;
    using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;
    using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;
    using Notary.SSAA.BO.Infrastructure.Contexts;
    using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.Base;
    using Notary.SSAA.BO.SharedKernel.Contracts.Coordinator.Document;
    using Notary.SSAA.BO.SharedKernel.Enumerations;
    using Notary.SSAA.BO.SharedKernel.Interfaces;
    using Notary.SSAA.BO.Utilities.Extensions;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading;
    using DocumentType = Notary.SSAA.BO.Domain.Entities.DocumentType;
    using Enumerations = Notary.SSAA.BO.SharedKernel.Enumerations;
    using State = Notary.SSAA.BO.SharedKernel.Constants.State;

    /// <summary>
    /// Defines the <see cref="DocumentRepository" />
    /// </summary>
    internal sealed class DocumentRepository : Repository<Document>, IDocumentRepository
    {
        /// <summary>
        /// Defines the dateTimeService
        /// </summary>
        internal IDateTimeService dateTimeService;
        internal static readonly string[] sourceArray = new[] { "5", "7" };

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The dbContext<see cref="ApplicationContext"/></param>
        /// <param name="_dateTimeService">The _dateTimeService<see cref="IDateTimeService"/></param>
        public DocumentRepository(ApplicationContext dbContext, IDateTimeService _dateTimeService) : base(dbContext)
        {
            dateTimeService = _dateTimeService;
        }

        /// <summary>
        /// The GetDocumentGridItems
        /// </summary>
        /// <param name="pageIndex">The pageIndex<see cref="int"/></param>
        /// <param name="pageSize">The pageSize<see cref="int"/></param>
        /// <param name="GridSearchInput">The GridSearchInput<see cref="ICollection{SearchData}"/></param>
        /// <param name="GlobalSearch">The GlobalSearch<see cref="string"/></param>
        /// <param name="gridSortInput">The gridSortInput<see cref="SortData"/></param>
        /// <param name="selectedItemsIds">The selectedItemsIds<see cref="IList{Guid}"/></param>
        /// <param name="FieldsNotInFilterSearch">The FieldsNotInFilterSearch<see cref="List{string}"/></param>
        /// <param name="scriptoriumId">The scriptoriumId<see cref="string"/></param>
        /// <param name="extraParams">The extraParams<see cref="DocumentExtraParams"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <param name="isOrderBy">The isOrderBy<see cref="bool"/></param>
        /// <returns>The <see cref="Task{DocumentGrid}"/></returns>
        public async Task<DocumentGrid> GetDocumentGridItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch, SortData gridSortInput,
            IList<Guid> selectedItemsIds, List<string> FieldsNotInFilterSearch, string scriptoriumId, DocumentExtraParams extraParams, CancellationToken cancellationToken, bool isOrderBy = false)
        {
            DocumentGrid result = new();
            var initialQuery = TableNoTracking
                        .Include(x => x.DocumentType)
                        .Include(x => x.DocumentCases)
                        .Include(x => x.DocumentPeople)
                        .Where(x => x.ScriptoriumId == scriptoriumId);
            var selectedItemQuery = TableNoTracking
                        .Where(x => x.ScriptoriumId == scriptoriumId && selectedItemsIds.Contains(x.Id))
                        .Include(x => x.DocumentCases)
                        .Include(x => x.DocumentPeople)
                        .Select(y =>
                        new DocumentGridItem()
                        {
                            Id = y.Id.ToString(),
                            RequestNo = y.RequestNo,
                            ClassifyNo = y.ClassifyNo.ToString(),
                            RequestDate = y.RequestDate,
                            StateId = y.State,
                            DocumentTypeGroupOneId = y.DocumentType.DocumentTypeGroup1Id,
                            DocumentTypeGroupTwoId = y.DocumentType.DocumentTypeGroup2Id,
                            DocumentTypeId = y.DocumentTypeId,
                            RelatedDocumentTypeId = y.RelatedDocumentTypeId,
                            DocumentTypeTitle = y.DocumentType.Title,
                            DocumentPersons = string.Join(",", y.DocumentPeople.Select(p => p.Name + " " + p.Family).ToList()),
                            DocumentCases = string.Join(",", y.DocumentCases.Select(p => p.Title).ToList()),
                        });

            SearchData personsFilter = GridSearchInput.Where(s => s.Filter.ToLower() == "DocumentPersons".ToLower()).FirstOrDefault();
            SearchData casesFilter = GridSearchInput.Where(s => s.Filter.ToLower() == "DocumentCases".ToLower()).FirstOrDefault();

            string filterQueryString = LambdaString<DocumentGridItem, SearchData>.CreateWhereLambdaString(new DocumentGridItem(), GridSearchInput, GlobalSearch, FieldsNotInFilterSearch);

            if (personsFilter is not null)
                filterQueryString = filterQueryString.AddLambdaQueryForEntityFieldList(GlobalSearch, "DocumentPersonList", personsFilter.Value);

            if (casesFilter is not null)
                filterQueryString = filterQueryString.AddLambdaQueryForEntityFieldList(GlobalSearch, "DocumentCaseList", casesFilter.Value);

            if (!string.IsNullOrWhiteSpace(GlobalSearch))
            {
                if (personsFilter is null)
                    filterQueryString = filterQueryString.AddLambdaQueryForEntityFieldList(GlobalSearch, "DocumentPersonList");

                if (casesFilter is null)
                    filterQueryString = filterQueryString.AddLambdaQueryForEntityFieldList(GlobalSearch, "DocumentCaseList");
            }

            if (gridSortInput.Sort.ToLower() == "DocumentCases".ToLower())
                gridSortInput.Sort = "DocumentCaseList.FirstOrDefault() ";

            if (gridSortInput.Sort.ToLower() == "DocumentPersons".ToLower())
                gridSortInput.Sort = "DocumentPersonList.FirstOrDefault() ";

            if (pageIndex == 1 && selectedItemsIds.Count > 0)
                result.SelectedItems = await selectedItemQuery.ToListAsync(cancellationToken);

            if (extraParams is not null)
            {
                if (!string.IsNullOrWhiteSpace(extraParams.StateId))
                    initialQuery = initialQuery.Where(x => x.State == extraParams.StateId);

                if (!string.IsNullOrWhiteSpace(extraParams.DocumentTypeId))
                    initialQuery = initialQuery.Where(x => x.DocumentTypeId == extraParams.DocumentTypeId);

                if (!string.IsNullOrWhiteSpace(extraParams.DocumentTypeGroupOneId))
                    initialQuery = initialQuery.Where(x => x.DocumentType.DocumentTypeGroup1Id == extraParams.DocumentTypeGroupOneId);

                if (!string.IsNullOrWhiteSpace(extraParams.DocumentTypeGroupTwoId))
                    initialQuery = initialQuery.Where(x => x.DocumentType.DocumentTypeGroup2Id == extraParams.DocumentTypeGroupTwoId);

                    if (extraParams.advancedSearch != null)
                    {
                        if (extraParams.advancedSearch.DocumentTypeId.Count > 0)
                            initialQuery = initialQuery.Where(x => x.DocumentTypeId == extraParams.advancedSearch.DocumentTypeId.FirstOrDefault());

                        if (extraParams.advancedSearch.DocumentScriptorumId.Count > 0)
                            initialQuery = initialQuery.Include(x => x.DocumentInfoOther).Where(x => x.DocumentInfoOther.ScriptoriumId == extraParams.advancedSearch.DocumentScriptorumId.FirstOrDefault());

                        if (!extraParams.advancedSearch.DocumentState.IsNullOrEmpty())
                            initialQuery = initialQuery.Where(x => x.State == extraParams.advancedSearch.DocumentState);
                        if (!extraParams.advancedSearch.RequestNo.IsNullOrEmpty())
                            initialQuery = initialQuery.Where(x => x.RequestNo == extraParams.advancedSearch.RequestNo);
                        if (!extraParams.advancedSearch.FromDocumentDate.IsNullOrEmpty())
                            initialQuery = initialQuery.Where(x => x.DocumentDate.CompareTo(extraParams.advancedSearch.FromDocumentDate) >= 0);
                        if (!extraParams.advancedSearch.ToDocumentDate.IsNullOrEmpty())
                            initialQuery = initialQuery.Where(x => x.DocumentDate.CompareTo(extraParams.advancedSearch.ToDocumentDate) <= 0);
                        if (!extraParams.advancedSearch.FromRecordDate.IsNullOrEmpty())
                            initialQuery = initialQuery.Where(x => x.RecordDate.ToPersianDate().CompareTo(extraParams.advancedSearch.FromRecordDate) >= 0);
                        if (!extraParams.advancedSearch.ToRecordDate.IsNullOrEmpty())
                            initialQuery = initialQuery.Where(x => x.RecordDate.ToPersianDate().CompareTo(extraParams.advancedSearch.ToRecordDate) <= 0);
                        if (!extraParams.advancedSearch.DocumentBookVolumeNo.IsNullOrEmpty())
                            initialQuery = initialQuery.Where(x => x.BookVolumeNo == extraParams.advancedSearch.DocumentBookVolumeNo);
                        if (!extraParams.advancedSearch.DocumentBookPapersNo.IsNullOrEmpty())
                            initialQuery = initialQuery.Where(x => x.BookPapersNo == extraParams.advancedSearch.DocumentBookPapersNo);
                        if (!extraParams.advancedSearch.DocumentClassifyNo.IsNullOrEmpty())
                            initialQuery = initialQuery.Where(x => x.ClassifyNo == extraParams.advancedSearch.DocumentClassifyNo.ToNullableInt());
                        if (!extraParams.advancedSearch.DocumentNationalNo.IsNullOrEmpty())
                            initialQuery = initialQuery.Where(x => x.NationalNo == extraParams.advancedSearch.DocumentNationalNo);
                        if (!extraParams.advancedSearch.DocumentWriteInBookDate.IsNullOrEmpty())
                            initialQuery = initialQuery.Where(x => x.WriteInBookDate == extraParams.advancedSearch.DocumentWriteInBookDate);
                        if (!extraParams.advancedSearch.DocumentSignDate.IsNullOrEmpty())
                            initialQuery = initialQuery.Where(x => x.SignDate == extraParams.advancedSearch.DocumentSignDate);
                        if (!extraParams.advancedSearch.DocumentIsBasedJudgment.IsNullOrEmpty())
                            initialQuery = initialQuery.Where(x => x.IsBasedJudgment == extraParams.advancedSearch.DocumentIsBasedJudgment);

                        if (extraParams.advancedSearch.documentPersonAdvancedSearch != null)
                        {
                            if (!extraParams.advancedSearch.documentPersonAdvancedSearch.IsPersonOriginal != null)
                                initialQuery = initialQuery.Where(x => x.DocumentPeople.Any(x => x.IsOriginal == extraParams.advancedSearch.documentPersonAdvancedSearch.IsPersonOriginal.ToYesNo()));

                            if (!extraParams.advancedSearch.documentPersonAdvancedSearch.IsPersonIranian != null)
                                initialQuery = initialQuery.Where(x => x.DocumentPeople.Any(x => x.IsIranian == extraParams.advancedSearch.documentPersonAdvancedSearch.IsPersonIranian.ToYesNo()));

                            if (!extraParams.advancedSearch.documentPersonAdvancedSearch.PersonSexType.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentPeople.Any(x => x.SexType == extraParams.advancedSearch.documentPersonAdvancedSearch.PersonSexType));
                            if (!extraParams.advancedSearch.documentPersonAdvancedSearch.PersonNationalNo.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentPeople.Any(x => x.NationalNo == extraParams.advancedSearch.documentPersonAdvancedSearch.PersonNationalNo));
                            if (!extraParams.advancedSearch.documentPersonAdvancedSearch.PersonName.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentPeople.Any(x => x.Name == extraParams.advancedSearch.documentPersonAdvancedSearch.PersonName));
                            if (!extraParams.advancedSearch.documentPersonAdvancedSearch.PersonFamily.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentPeople.Any(x => x.Family == extraParams.advancedSearch.documentPersonAdvancedSearch.PersonFamily));
                            if (!extraParams.advancedSearch.documentPersonAdvancedSearch.PersonBirthDate.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentPeople.Any(x => x.BirthDate == extraParams.advancedSearch.documentPersonAdvancedSearch.PersonBirthDate));
                            if (!extraParams.advancedSearch.documentPersonAdvancedSearch.PersonFatherName.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentPeople.Any(x => x.FatherName == extraParams.advancedSearch.documentPersonAdvancedSearch.PersonFatherName));
                            if (!extraParams.advancedSearch.documentPersonAdvancedSearch.PersonFatherName.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentPeople.Any(x => x.FatherName == extraParams.advancedSearch.documentPersonAdvancedSearch.PersonFatherName));
                            if (!extraParams.advancedSearch.documentPersonAdvancedSearch.PersonIdentityNo.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentPeople.Any(x => x.IdentityNo == extraParams.advancedSearch.documentPersonAdvancedSearch.PersonIdentityNo));
                            if (!extraParams.advancedSearch.documentPersonAdvancedSearch.PersonSeri.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentPeople.Any(x => x.Seri == extraParams.advancedSearch.documentPersonAdvancedSearch.PersonSeri));
                            if (!extraParams.advancedSearch.documentPersonAdvancedSearch.PersonAlphabetSeri.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentPeople.Any(x => x.SeriAlpha == extraParams.advancedSearch.documentPersonAdvancedSearch.PersonAlphabetSeri));
                            if (!extraParams.advancedSearch.documentPersonAdvancedSearch.PersonSerial.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentPeople.Any(x => x.Serial == extraParams.advancedSearch.documentPersonAdvancedSearch.PersonSerial));
                            if (!extraParams.advancedSearch.documentPersonAdvancedSearch.PersonMobileNo.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentPeople.Any(x => x.MobileNo == extraParams.advancedSearch.documentPersonAdvancedSearch.PersonMobileNo));
                            if (!extraParams.advancedSearch.documentPersonAdvancedSearch.PersonEmail.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentPeople.Any(x => x.Email == extraParams.advancedSearch.documentPersonAdvancedSearch.PersonEmail));
                            if (!extraParams.advancedSearch.documentPersonAdvancedSearch.PersonPostalCode.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentPeople.Any(x => x.PostalCode == extraParams.advancedSearch.documentPersonAdvancedSearch.PersonPostalCode));
                            if (!extraParams.advancedSearch.documentPersonAdvancedSearch.PersonTel.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentPeople.Any(x => x.Tel == extraParams.advancedSearch.documentPersonAdvancedSearch.PersonTel));
                            if (!extraParams.advancedSearch.documentPersonAdvancedSearch.LastLegalPaperNo.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentPeople.Any(x => x.LastLegalPaperNo == extraParams.advancedSearch.documentPersonAdvancedSearch.LastLegalPaperNo));
                            if (!extraParams.advancedSearch.documentPersonAdvancedSearch.LastLegalPaperDate.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentPeople.Any(x => x.LastLegalPaperDate == extraParams.advancedSearch.documentPersonAdvancedSearch.LastLegalPaperDate));
                            if (!extraParams.advancedSearch.documentPersonAdvancedSearch.PersonType.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentPeople.Any(x => x.PersonType == extraParams.advancedSearch.documentPersonAdvancedSearch.PersonType));
                            if (!extraParams.advancedSearch.documentPersonAdvancedSearch.PersonAddress.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentPeople.Any(x => x.Address == extraParams.advancedSearch.documentPersonAdvancedSearch.PersonAddress));
                            if (extraParams.advancedSearch.documentPersonAdvancedSearch.DocumentPersonType.Count > 0)
                                initialQuery = initialQuery.Where(x => x.DocumentPeople.Any(x => x.DocumentPersonTypeId == extraParams.advancedSearch.documentPersonAdvancedSearch.DocumentPersonType.FirstOrDefault()));
                        }
                        if (extraParams.advancedSearch.documentEstateAdvancedSearch != null)
                        {
                            initialQuery = initialQuery.Include(x => x.DocumentEstates);
                            if (!extraParams.advancedSearch.documentEstateAdvancedSearch.EstateDirection.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentEstates.Any(x => x.Direction == extraParams.advancedSearch.documentEstateAdvancedSearch.EstateDirection));
                            if (extraParams.advancedSearch.documentEstateAdvancedSearch.DocumentEstateTypeId.Count > 0)
                                initialQuery = initialQuery.Where(x => x.DocumentEstates.Any(x => x.DocumentEstateTypeId == extraParams.advancedSearch.documentEstateAdvancedSearch.DocumentEstateTypeId.FirstOrDefault()));
                            if (!extraParams.advancedSearch.documentEstateAdvancedSearch.EstateBlock.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentEstates.Any(x => x.Block == extraParams.advancedSearch.documentEstateAdvancedSearch.EstateBlock));
                            if (extraParams.advancedSearch.documentEstateAdvancedSearch.UnitId.Count > 0)
                                initialQuery = initialQuery.Where(x => x.DocumentEstates.Any(x => x.UnitId == extraParams.advancedSearch.documentEstateAdvancedSearch.UnitId.FirstOrDefault()));
                            if (!extraParams.advancedSearch.documentEstateAdvancedSearch.Limits.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentEstates.Any(x => x.Limits == extraParams.advancedSearch.documentEstateAdvancedSearch.Limits));
                            if (extraParams.advancedSearch.documentEstateAdvancedSearch.SecondaryPlaqueHasRemain != null)
                                initialQuery = initialQuery.Where(x => x.DocumentEstates.Any(x => x.SecondaryPlaqueHasRemain == extraParams.advancedSearch.documentEstateAdvancedSearch.SecondaryPlaqueHasRemain.ToYesNo()));
                            if (extraParams.advancedSearch.documentEstateAdvancedSearch.Floor.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentEstates.Any(x => x.Floor == extraParams.advancedSearch.documentEstateAdvancedSearch.Floor));

                            if (extraParams.advancedSearch.documentEstateAdvancedSearch.SecondaryPlaque.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentEstates.Any(x => x.SecondaryPlaque == extraParams.advancedSearch.documentEstateAdvancedSearch.SecondaryPlaque));
                            if (extraParams.advancedSearch.documentEstateAdvancedSearch.LocationType.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentEstates.Any(x => x.LocationType == extraParams.advancedSearch.documentEstateAdvancedSearch.LocationType));
                            if (extraParams.advancedSearch.documentEstateAdvancedSearch.BasicPlaqueHasRemain != null)
                                initialQuery = initialQuery.Where(x => x.DocumentEstates.Any(x => x.BasicPlaqueHasRemain == extraParams.advancedSearch.documentEstateAdvancedSearch.BasicPlaqueHasRemain.ToYesNo()));
                            if (extraParams.advancedSearch.documentEstateAdvancedSearch.ImmovaleType.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentEstates.Any(x => x.ImmovaleType == extraParams.advancedSearch.documentEstateAdvancedSearch.ImmovaleType));
                            if (extraParams.advancedSearch.documentEstateAdvancedSearch.BasicPlaque.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentEstates.Any(x => x.BasicPlaque == extraParams.advancedSearch.documentEstateAdvancedSearch.BasicPlaque));
                            if (extraParams.advancedSearch.documentEstateAdvancedSearch.Area.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentEstates.Any(x => x.Area == extraParams.advancedSearch.documentEstateAdvancedSearch.Area.ToDecimal()));

                            if (extraParams.advancedSearch.documentEstateAdvancedSearch.EstateSubSectionId.Count > 0)
                                initialQuery = initialQuery.Where(x => x.DocumentEstates.Any(x => x.EstateSubsectionId == extraParams.advancedSearch.documentEstateAdvancedSearch.EstateSubSectionId.FirstOrDefault()));

                            if (extraParams.advancedSearch.documentEstateAdvancedSearch.AreaDescription.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentEstates.Any(x => x.AreaDescription == extraParams.advancedSearch.documentEstateAdvancedSearch.AreaDescription));

                            if (extraParams.advancedSearch.documentEstateAdvancedSearch.PlaqueText.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentEstates.Any(x => x.PlaqueText == extraParams.advancedSearch.documentEstateAdvancedSearch.PlaqueText));

                            if (extraParams.advancedSearch.documentEstateAdvancedSearch.PostalCode.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentEstates.Any(x => x.PostalCode == extraParams.advancedSearch.documentEstateAdvancedSearch.PostalCode));

                            if (extraParams.advancedSearch.documentEstateAdvancedSearch.DivFromSecondaryPlaque.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentEstates.Any(x => x.DivFromSecondaryPlaque == extraParams.advancedSearch.documentEstateAdvancedSearch.DivFromSecondaryPlaque));

                            if (extraParams.advancedSearch.documentEstateAdvancedSearch.GeoLocationId.Count > 0)
                                initialQuery = initialQuery.Where(x => x.DocumentEstates.Any(x => x.GeoLocationId == extraParams.advancedSearch.documentEstateAdvancedSearch.GeoLocationId.FirstOrDefault().ToNullableInt()));

                            if (extraParams.advancedSearch.documentEstateAdvancedSearch.Piece.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentEstates.Any(x => x.Piece == extraParams.advancedSearch.documentEstateAdvancedSearch.Piece));

                            if (extraParams.advancedSearch.documentEstateAdvancedSearch.FieldOrGrandee.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentEstates.Any(x => x.FieldOrGrandee == extraParams.advancedSearch.documentEstateAdvancedSearch.FieldOrGrandee));

                            if (extraParams.advancedSearch.documentEstateAdvancedSearch.DivFromBasicPlaque.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentEstates.Any(x => x.DivFromBasicPlaque == extraParams.advancedSearch.documentEstateAdvancedSearch.DivFromBasicPlaque));

                            if (extraParams.advancedSearch.documentEstateAdvancedSearch.Address.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentEstates.Any(x => x.Address == extraParams.advancedSearch.documentEstateAdvancedSearch.Address));

                            if (extraParams.advancedSearch.documentEstateAdvancedSearch.Commons.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentEstates.Any(x => x.Commons == extraParams.advancedSearch.documentEstateAdvancedSearch.Commons));

                            if (extraParams.advancedSearch.documentEstateAdvancedSearch.Rights.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentEstates.Any(x => x.Rights == extraParams.advancedSearch.documentEstateAdvancedSearch.Rights));

                            if (extraParams.advancedSearch.documentEstateAdvancedSearch.OldSaleDescription.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentEstates.Any(x => x.OldSaleDescription == extraParams.advancedSearch.documentEstateAdvancedSearch.OldSaleDescription));
                        }
                        if (extraParams.advancedSearch.documentInquiryAdvancedSearch != null)
                        {
                            initialQuery = initialQuery.Include(x => x.DocumentInquiries);

                            if (extraParams.advancedSearch.documentInquiryAdvancedSearch.DocumentInquiryOrganizationId.Count > 0)
                                initialQuery = initialQuery.Where(x => x.DocumentInquiries.Any(x => x.DocumentInquiryOrganizationId == extraParams.advancedSearch.documentInquiryAdvancedSearch.DocumentInquiryOrganizationId.FirstOrDefault()));

                            if (extraParams.advancedSearch.documentInquiryAdvancedSearch.RequestNo.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentInquiries.Any(x => x.RequestNo == extraParams.advancedSearch.documentInquiryAdvancedSearch.RequestNo));

                            if (!extraParams.advancedSearch.documentInquiryAdvancedSearch.RequestFromDate.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentInquiries.Any(x => x.RequestDate.CompareTo(extraParams.advancedSearch.documentInquiryAdvancedSearch.RequestFromDate) >= 0));
                            if (!extraParams.advancedSearch.documentInquiryAdvancedSearch.RequestToDate.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentInquiries.Any(x => x.RequestDate.CompareTo(extraParams.advancedSearch.documentInquiryAdvancedSearch.RequestToDate) <= 0));
                            if (!extraParams.advancedSearch.documentInquiryAdvancedSearch.ReplyFromNo.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentInquiries.Any(x => x.ReplyNo.CompareTo(extraParams.advancedSearch.documentInquiryAdvancedSearch.ReplyFromNo) >= 0));
                            if (!extraParams.advancedSearch.documentInquiryAdvancedSearch.ReplyToNo.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentInquiries.Any(x => x.ReplyNo.CompareTo(extraParams.advancedSearch.documentInquiryAdvancedSearch.ReplyToNo) <= 0));

                        }
                        if (extraParams.advancedSearch.documentVehicleAdvancedSearch != null)
                        {
                            initialQuery = initialQuery.Include(x => x.DocumentVehicles);
                            if (!extraParams.advancedSearch.documentVehicleAdvancedSearch.MadeInIran.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentVehicles.Any(x => x.MadeInIran == extraParams.advancedSearch.documentVehicleAdvancedSearch.MadeInIran));
                            if (!extraParams.advancedSearch.documentVehicleAdvancedSearch.MadeInIran.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentVehicles.Any(x => x.MadeInIran == extraParams.advancedSearch.documentVehicleAdvancedSearch.MadeInIran));
                            if (!extraParams.advancedSearch.documentVehicleAdvancedSearch.DocumentVehicleSystemId.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentVehicles.Any(x => extraParams.advancedSearch.documentVehicleAdvancedSearch.DocumentVehicleSystemId.Contains(x.DocumentVehicleSystemId)));
                            if (!extraParams.advancedSearch.documentVehicleAdvancedSearch.DocumentVehicleTipId.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentVehicles.Any(x => extraParams.advancedSearch.documentVehicleAdvancedSearch.DocumentVehicleTipId.Contains(x.DocumentVehicleTipId)));
                            if (!extraParams.advancedSearch.documentVehicleAdvancedSearch.DocumentVehicleTypeId.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentVehicles.Any(x => extraParams.advancedSearch.documentVehicleAdvancedSearch.DocumentVehicleTypeId.Contains(x.DocumentVehicleTypeId)));
                            if (!extraParams.advancedSearch.documentVehicleAdvancedSearch.Model.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentVehicles.Any(x => x.Model == extraParams.advancedSearch.documentVehicleAdvancedSearch.Model));
                            if (!extraParams.advancedSearch.documentVehicleAdvancedSearch.ChassisNo.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentVehicles.Any(x => x.ChassisNo == extraParams.advancedSearch.documentVehicleAdvancedSearch.ChassisNo));
                            if (!extraParams.advancedSearch.documentVehicleAdvancedSearch.OldDocumentIssuer.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentVehicles.Any(x => x.OldDocumentIssuer == extraParams.advancedSearch.documentVehicleAdvancedSearch.OldDocumentIssuer));
                            if (!extraParams.advancedSearch.documentVehicleAdvancedSearch.Color.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentVehicles.Any(x => x.Color == extraParams.advancedSearch.documentVehicleAdvancedSearch.Color));
                            if (!extraParams.advancedSearch.documentVehicleAdvancedSearch.InssuranceCo.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentVehicles.Any(x => x.InssuranceCo == extraParams.advancedSearch.documentVehicleAdvancedSearch.InssuranceCo));
                            if (!extraParams.advancedSearch.documentVehicleAdvancedSearch.FuelCardNo.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentVehicles.Any(x => x.FuelCardNo == extraParams.advancedSearch.documentVehicleAdvancedSearch.FuelCardNo));
                            if (!extraParams.advancedSearch.documentVehicleAdvancedSearch.DutyFicheNo.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentVehicles.Any(x => x.DutyFicheNo == extraParams.advancedSearch.documentVehicleAdvancedSearch.DutyFicheNo));
                            if (!extraParams.advancedSearch.documentVehicleAdvancedSearch.CylinderCount.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentVehicles.Any(x => x.CylinderCount == extraParams.advancedSearch.documentVehicleAdvancedSearch.CylinderCount.ToDecimal()));
                            if (!extraParams.advancedSearch.documentVehicleAdvancedSearch.InssuranceNo.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentVehicles.Any(x => x.InssuranceNo == extraParams.advancedSearch.documentVehicleAdvancedSearch.InssuranceNo));
                            if (!extraParams.advancedSearch.documentVehicleAdvancedSearch.OldDocumentNo.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentVehicles.Any(x => x.OldDocumentNo == extraParams.advancedSearch.documentVehicleAdvancedSearch.OldDocumentNo));
                            if (!extraParams.advancedSearch.documentVehicleAdvancedSearch.PlaqueNo1Seller.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentVehicles.Any(x => x.PlaqueNo1Seller == extraParams.advancedSearch.documentVehicleAdvancedSearch.PlaqueNo1Seller.ToInt()));
                            if (!extraParams.advancedSearch.documentVehicleAdvancedSearch.PlaqueNo2Seller.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentVehicles.Any(x => x.PlaqueNo2Seller == extraParams.advancedSearch.documentVehicleAdvancedSearch.PlaqueNo2Seller.ToInt()));
                            if (!extraParams.advancedSearch.documentVehicleAdvancedSearch.PlaqueSeriSeller.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentVehicles.Any(x => x.PlaqueSeriSeller == extraParams.advancedSearch.documentVehicleAdvancedSearch.PlaqueSeriSeller.ToInt()));
                            if (!extraParams.advancedSearch.documentVehicleAdvancedSearch.PlaqueNoAlphaSeller.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentVehicles.Any(x => x.PlaqueNoAlphaSeller == extraParams.advancedSearch.documentVehicleAdvancedSearch.PlaqueNoAlphaSeller));
                            if (!extraParams.advancedSearch.documentVehicleAdvancedSearch.PlaqueNo1Buyer.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentVehicles.Any(x => x.PlaqueNo1Buyer == extraParams.advancedSearch.documentVehicleAdvancedSearch.PlaqueNo1Buyer.ToInt()));
                            if (!extraParams.advancedSearch.documentVehicleAdvancedSearch.PlaqueNo2Buyer.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentVehicles.Any(x => x.PlaqueNo2Buyer == extraParams.advancedSearch.documentVehicleAdvancedSearch.PlaqueNo2Buyer.ToInt()));
                            if (!extraParams.advancedSearch.documentVehicleAdvancedSearch.PlaqueSeriBuyer.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentVehicles.Any(x => x.PlaqueSeriBuyer == extraParams.advancedSearch.documentVehicleAdvancedSearch.PlaqueSeriBuyer.ToInt()));
                            if (!extraParams.advancedSearch.documentVehicleAdvancedSearch.PlaqueNoAlphaBuyer.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentVehicles.Any(x => x.PlaqueNoAlphaBuyer == extraParams.advancedSearch.documentVehicleAdvancedSearch.PlaqueNoAlphaBuyer));
                            if (!extraParams.advancedSearch.documentVehicleAdvancedSearch.EngineCapacity.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentVehicles.Any(x => x.EngineCapacity == extraParams.advancedSearch.documentVehicleAdvancedSearch.EngineCapacity));
                            if (!extraParams.advancedSearch.documentVehicleAdvancedSearch.EngineNo.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentVehicles.Any(x => x.EngineNo == extraParams.advancedSearch.documentVehicleAdvancedSearch.EngineNo));
                            if (!extraParams.advancedSearch.documentVehicleAdvancedSearch.OldDocumentDate.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentVehicles.Any(x => x.OldDocumentDate == extraParams.advancedSearch.documentVehicleAdvancedSearch.OldDocumentDate));
                        }
                        if (extraParams.advancedSearch.documentPaymentAdvancedSearch != null)
                        {
                            initialQuery = initialQuery.Include(x => x.DocumentCosts);
                            initialQuery = initialQuery.Include(x => x.DocumentPayments);

                            if (!extraParams.advancedSearch.documentPaymentAdvancedSearch.FromPrice.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.Price >= extraParams.advancedSearch.documentPaymentAdvancedSearch.FromPrice.ToDecimal());

                            if (!extraParams.advancedSearch.documentPaymentAdvancedSearch.ToPrice.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.Price <= extraParams.advancedSearch.documentPaymentAdvancedSearch.ToPrice.ToDecimal());

                            if (!extraParams.advancedSearch.documentPaymentAdvancedSearch.FromSabtPrice.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.SabtPrice >= extraParams.advancedSearch.documentPaymentAdvancedSearch.FromSabtPrice.ToDecimal());

                            if (!extraParams.advancedSearch.documentPaymentAdvancedSearch.ToSabtPrice.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.SabtPrice <= extraParams.advancedSearch.documentPaymentAdvancedSearch.ToSabtPrice.ToDecimal());

                            if (!extraParams.advancedSearch.documentPaymentAdvancedSearch.Price.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentCosts.Any(x => x.Price == extraParams.advancedSearch.documentPaymentAdvancedSearch.Price.ToDecimal()));

                            if (!extraParams.advancedSearch.documentPaymentAdvancedSearch.CostTypeId.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentCosts.Any(x => extraParams.advancedSearch.documentPaymentAdvancedSearch.CostTypeId.Contains(x.CostTypeId)));

                            if (!extraParams.advancedSearch.documentPaymentAdvancedSearch.HowToPay.IsNullOrEmpty())
                                initialQuery = initialQuery.Where(x => x.DocumentPayments.Any(x => x.HowToPay == extraParams.advancedSearch.documentPaymentAdvancedSearch.HowToPay));

                            //if (!extraParams.advancedSearch.documentPaymentAdvancedSearch.FactorNo.IsNullOrEmpty())
                            //    initialQuery = initialQuery.Where(x => x.DocumentPayments.Any(x => x.FactorNo == extraParams.advancedSearch.documentPaymentAdvancedSearch.FactorNo));

                            //if (!extraParams.advancedSearch.documentPaymentAdvancedSearch.FactorDate.IsNullOrEmpty())
                            //    initialQuery = initialQuery.Where(x => x.DocumentPayments.Any(x => x.FactorDate == extraParams.advancedSearch.documentPaymentAdvancedSearch.FactorDate));

                        }
                        if (extraParams.advancedSearch.documentCaseAdvancedSearch != null)
                        {
                            if (extraParams.advancedSearch.documentVehicleAdvancedSearch == null)
                            {
                                initialQuery = initialQuery.Include(x => x.DocumentVehicles);
                            }
                            if (extraParams.advancedSearch.documentEstateAdvancedSearch == null)
                            {
                                initialQuery = initialQuery.Include(x => x.DocumentEstates);
                            }

                            if (!extraParams.advancedSearch.documentCaseAdvancedSearch.OwnershipDetailQuota.IsNullOrEmpty())
                            {
                                initialQuery = initialQuery.Where(x => x.DocumentVehicles.Any(x => x.OwnershipDetailQuota == extraParams.advancedSearch.documentCaseAdvancedSearch.OwnershipDetailQuota.ToDecimal()));
                                initialQuery = initialQuery.Where(x => x.DocumentEstates.Any(x => x.OwnershipDetailQuota == extraParams.advancedSearch.documentCaseAdvancedSearch.OwnershipDetailQuota.ToDecimal()));
                            }
                            if (!extraParams.advancedSearch.documentCaseAdvancedSearch.OwnershipTotalQuota.IsNullOrEmpty())
                            {
                                initialQuery = initialQuery.Where(x => x.DocumentVehicles.Any(x => x.OwnershipTotalQuota == extraParams.advancedSearch.documentCaseAdvancedSearch.OwnershipTotalQuota.ToDecimal()));
                                initialQuery = initialQuery.Where(x => x.DocumentEstates.Any(x => x.OwnershipTotalQuota == extraParams.advancedSearch.documentCaseAdvancedSearch.OwnershipTotalQuota.ToDecimal()));
                            }
                            if (!extraParams.advancedSearch.documentCaseAdvancedSearch.SellDetailQuota.IsNullOrEmpty())
                            {
                                initialQuery = initialQuery.Where(x => x.DocumentVehicles.Any(x => x.SellDetailQuota == extraParams.advancedSearch.documentCaseAdvancedSearch.SellDetailQuota.ToDecimal()));
                                initialQuery = initialQuery.Where(x => x.DocumentEstates.Any(x => x.SellDetailQuota == extraParams.advancedSearch.documentCaseAdvancedSearch.SellDetailQuota.ToDecimal()));
                            }
                            if (!extraParams.advancedSearch.documentCaseAdvancedSearch.SellTotalQuota.IsNullOrEmpty())
                            {
                                initialQuery = initialQuery.Where(x => x.DocumentVehicles.Any(x => x.SellTotalQuota == extraParams.advancedSearch.documentCaseAdvancedSearch.SellTotalQuota.ToDecimal()));
                                initialQuery = initialQuery.Where(x => x.DocumentEstates.Any(x => x.SellTotalQuota == extraParams.advancedSearch.documentCaseAdvancedSearch.SellTotalQuota.ToDecimal()));
                            }
                            if (!extraParams.advancedSearch.documentCaseAdvancedSearch.GrandeeExceptionDetailQuota.IsNullOrEmpty())
                            {
                                initialQuery = initialQuery.Where(x => x.DocumentEstates.Any(x => x.GrandeeExceptionDetailQuota == extraParams.advancedSearch.documentCaseAdvancedSearch.GrandeeExceptionDetailQuota.ToDecimal()));
                            }
                            if (!extraParams.advancedSearch.documentCaseAdvancedSearch.GrandeeExceptionTotalQuota.IsNullOrEmpty())
                            {
                                initialQuery = initialQuery.Where(x => x.DocumentEstates.Any(x => x.GrandeeExceptionTotalQuota == extraParams.advancedSearch.documentCaseAdvancedSearch.GrandeeExceptionTotalQuota.ToDecimal()));
                            }
                            if (!extraParams.advancedSearch.documentCaseAdvancedSearch.QuotaText.IsNullOrEmpty())
                            {
                                initialQuery = initialQuery.Where(x => x.DocumentVehicles.Any(x => x.QuotaText == extraParams.advancedSearch.documentCaseAdvancedSearch.QuotaText));
                                initialQuery = initialQuery.Where(x => x.DocumentEstates.Any(x => x.QuotaText == extraParams.advancedSearch.documentCaseAdvancedSearch.QuotaText));
                            }
                        }
                    }
         

            }
            var query = initialQuery.Select(y =>
                       new DocumentGridItem()
                       {
                           Id = y.Id.ToString(),
                           RequestNo = y.RequestNo,
                           ClassifyNo = y.ClassifyNo.ToString(),
                           RequestDate = y.RequestDate,
                           StateId = y.State,
                           DocumentTypeGroupOneId = y.DocumentType.DocumentTypeGroup1Id,
                           DocumentTypeGroupTwoId = y.DocumentType.DocumentTypeGroup2Id,
                           DocumentTypeId = y.DocumentTypeId,
                           RelatedDocumentTypeId = y.RelatedDocumentTypeId,
                           DocumentTypeTitle = y.DocumentType.Title,
                           DocumentPersons = string.Join(",", y.DocumentPeople.Select(p => p.Name + " " + p.Family).ToList()),
                           DocumentPersonList = y.DocumentPeople.Select(p => p.Name + " " + p.Family).ToList(),
                           DocumentCases = string.Join(",", y.DocumentCases.Select(p => p.Title).ToList()),
                           DocumentCaseList = y.DocumentCases.Select(p => p.Title).ToList()
                       });
            if (!string.IsNullOrWhiteSpace(filterQueryString))
                query = query.Where(filterQueryString);

            if (isOrderBy)
                query = query.OrderBy($"{gridSortInput.Sort} {gridSortInput.SortType} ");

            result.GridItems = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            result.TotalCount = await query.CountAsync(cancellationToken);

            return result;
        }

        /// <summary>
        /// The GetDocument
        /// </summary>
        /// <param name="documentId">The documentId<see cref="Guid"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{Document}"/></returns>
        public async Task<Document> GetDocument(Guid documentId, CancellationToken cancellationToken)
        {
            return await TableNoTracking.Where(x => x.Id == documentId)
                .Include(x => x.DocumentPeople)
                .ThenInclude(x => x.DocumentPersonType)
                .Include(x => x.DocumentPersonRelatedDocuments)
                .ThenInclude(x => x.AgentType)
                .Include(x => x.DocumentType)
                //.Include(x => x.DocumentType.DocumentTypeGroup1)
                //.Include(x => x.DocumentType.DocumentTypeGroup2)
                .Include(x => x.DocumentInfoConfirm)
                .FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// The GetDocument
        /// </summary>
        /// <param name="documentId">The documentId<see cref="Guid"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{Document}"/></returns>
        public async Task<Document> GetDocumentModify(Guid documentId, CancellationToken cancellationToken)
        {
            return await TableNoTracking.Where(x => x.Id == documentId)
                .Include(x => x.DocumentInfoText)
                .Include(x => x.SsrDocModifyClassifyNos)
                .FirstOrDefaultAsync(cancellationToken);
        }
        /// <summary>
        /// The GetDocumentRelatedPeople
        /// </summary>
        /// <param name="documentId">The documentId<see cref="Guid"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{Document}"/></returns>
        public async Task<Document> GetDocumentRelatedPeople(Guid documentId, CancellationToken cancellationToken)
        {
            return await TableNoTracking.Include(x => x.DocumentPersonRelatedDocuments).ThenInclude(x => x.AgentType).Include(x => x.DocumentType).Include(x => x.DocumentInfoConfirm)
              .Where(x => x.Id == documentId).FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// The GetDocumentRelations
        /// </summary>
        /// <param name="documentId">The documentId<see cref="Guid"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{Document}"/></returns>
        public async Task<Document> GetDocumentRelations(Guid documentId, CancellationToken cancellationToken)
        {
            return await TableNoTracking.Include(x => x.DocumentRelationDocuments).Include(x => x.DocumentType).Include(x => x.DocumentInfoConfirm)
              .Where(x => x.Id == documentId).FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// The GetDocumentInquiries
        /// </summary>
        /// <param name="documentId">The documentId<see cref="Guid"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{Document}"/></returns>
        public async Task<Document> GetDocumentInquiries(Guid documentId, CancellationToken cancellationToken)
        {
            return await TableNoTracking.Include(x => x.DocumentInquiries).ThenInclude(x => x.DocumentInquiryOrganization).Include(x => x.DocumentType).Include(x => x.DocumentInfoConfirm)
             .Where(x => x.Id == documentId).FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// The GetDocumentPayments
        /// </summary>
        /// <param name="documentId">The documentId<see cref="Guid"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{Document}"/></returns>
        public async Task<Document> GetDocumentPayments(Guid documentId, CancellationToken cancellationToken)
        {
            return await TableNoTracking
                .Include(x => x.DocumentPayments).ThenInclude(x => x.ReusedDocumentPayment)
                .Include(x => x.DocumentPayments).ThenInclude(x => x.CostType)
                .Include(x => x.DocumentType).Include(x => x.DocumentInfoConfirm)
                .Where(x => x.Id == documentId).FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// The GetDocumentInfoTextById
        /// </summary>
        /// <param name="DocumentId">The DocumentId<see cref="string"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{Document}"/></returns>
        public async Task<Document> GetDocumentInfoTextById(string DocumentId, CancellationToken cancellationToken)
        {
            return await TableNoTracking.Include(x => x.DocumentInfoText).Include(x => x.DocumentType).Include(x => x.DocumentInfoConfirm).Where(x => x.Id == Guid.Parse(DocumentId)).FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// The GetDocumentInfoConfirmById
        /// </summary>
        /// <param name="DocumentId">The DocumentId<see cref="string"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{Document}"/></returns>
        public async Task<Document> GetDocumentInfoConfirmById(string DocumentId, CancellationToken cancellationToken)
        {
            return await TableNoTracking.Include(x => x.DocumentInfoConfirm).Include(x => x.DocumentType).Where(x => x.Id == Guid.Parse(DocumentId)).FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// The GetDocumentCostQuestionById
        /// </summary>
        /// <param name="DocumentId">The DocumentId<see cref="string"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{Document}"/></returns>
        public async Task<Document> GetDocumentCostQuestionById(string DocumentId, CancellationToken cancellationToken)
        {
            return await TableNoTracking.Include(x => x.DocumentInfoOther).Include(x => x.DocumentCosts).ThenInclude(x => x.CostType).Include(x => x.DocumentCostUnchangeds).Include(x => x.DocumentType).Where(x => x.Id == Guid.Parse(DocumentId)).FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// The GetDocumentInfoJudgmentById
        /// </summary>
        /// <param name="DocumentId">The DocumentId<see cref="string"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{Document}"/></returns>
        public async Task<Document> GetDocumentInfoJudgmentById(string DocumentId, CancellationToken cancellationToken)
        {
            return await TableNoTracking.Include(x => x.DocumentInfoJudgement).Include(x => x.DocumentInfoConfirm).Include(x => x.DocumentType).Where(x => x.Id == Guid.Parse(DocumentId)).FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// The GetDocumentInfoOther
        /// </summary>
        /// <param name="documentId">The documentId<see cref="Guid"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{Document}"/></returns>
        public async Task<Document> GetDocumentInfoOther(Guid documentId, CancellationToken cancellationToken)
        {
            return await TableNoTracking.Include(x => x.DocumentInfoOther).ThenInclude(x => x.DocumentTypeSubject).Include(x => x.DocumentType).Include(x => x.DocumentInfoText).Include(x => x.DocumentInfoConfirm).Where(x => x.Id == documentId).FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// The GetDocumentCases
        /// </summary>
        /// <param name="documentId">The documentId<see cref="Guid"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{Document}"/></returns>
        public async Task<Document> GetDocumentCases(Guid documentId, CancellationToken cancellationToken)
        {
            return await TableNoTracking.Include(x => x.DocumentCases).Include(x => x.DocumentType).Include(x => x.DocumentInfoConfirm)
              .Where(x => x.Id == documentId).FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// The GetDocumentCostsAndCostUnchange
        /// </summary>
        /// <param name="documentId">The documentId<see cref="Guid"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{Document}"/></returns>
        public async Task<Document> GetDocumentCostsAndCostUnchange(Guid documentId, CancellationToken cancellationToken)
        {
            return await TableNoTracking.Include(x => x.DocumentCosts).ThenInclude(x => x.CostType).Include(x => x.DocumentCostUnchangeds).Include(x => x.DocumentType).Include(x => x.DocumentInfoConfirm).Include(x => x.DocumentInfoOther)
              .Where(x => x.Id == documentId).FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// The GetDocumentRegisterReqPrices
        /// </summary>
        /// <param name="documentId">The documentId<see cref="Guid"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{Document}"/></returns>
        public async Task<Document> GetDocumentRegisterReqPrices(Guid documentId, CancellationToken cancellationToken)
        {
            return await TableNoTracking.Include(x => x.DocumentCosts).ThenInclude(x => x.CostType).Include(x => x.DocumentCostUnchangeds)
                .Include(x => x.DocumentEstates).ThenInclude(x => x.DocumentEstateOwnershipDocuments).ThenInclude(x => x.DocumentPerson)
                .Include(x => x.DocumentEstates).ThenInclude(x => x.DocumentEstateAttachments)
                .Include(x => x.DocumentEstates).ThenInclude(x => x.DocumentEstateSeparationPieces).ThenInclude(x => x.InverseAnbari)
                .Include(x => x.DocumentEstates).ThenInclude(x => x.DocumentEstateSeparationPieces).ThenInclude(x => x.InverseParking)
                .Include(x => x.DocumentEstates).ThenInclude(x => x.DocumentEstateSeparationPieces).ThenInclude(x => x.DocumentEstateSeparationPieceKind)
                .Include(x => x.DocumentEstates).ThenInclude(x => x.DocumentEstateSeparationPieces).ThenInclude(x => x.EstatePieceType)
                .Include(x => x.DocumentEstates).ThenInclude(x => x.DocumentEstateSeparationPieces).ThenInclude(x => x.DocumentEstateSeparationPiecesQuota)
                .Include(x => x.DocumentEstates).ThenInclude(y => y.DocumentEstateQuota).ThenInclude(y => y.DocumentPerson)
                .Include(x => x.DocumentEstates).ThenInclude(y => y.DocumentEstateQuotaDetails).ThenInclude(y => y.DocumentPersonBuyer)
                .Include(x => x.DocumentEstates).ThenInclude(y => y.DocumentEstateQuotaDetails).ThenInclude(y => y.DocumentPersonSeller)
                .Include(x => x.DocumentInquiries).Include(x => x.DocumentType).Include(x => x.DocumentInfoConfirm).Include(x => x.DocumentInfoOther)
             .Where(x => x.Id == documentId).FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// The GetDocumentCostunchanges
        /// </summary>
        /// <param name="documentId">The documentId<see cref="Guid"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{Document}"/></returns>
        public async Task<Document> GetDocumentCostunchanges(Guid documentId, CancellationToken cancellationToken)
        {
            return await TableNoTracking.Include(x => x.DocumentCostUnchangeds).ThenInclude(x => x.CostType).Include(x => x.DocumentType).Include(x => x.DocumentInfoConfirm)
              .Where(x => x.Id == documentId).FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// The GetMaxDocNo
        /// </summary>
        /// <param name="beginReqNo">The beginReqNo<see cref="string"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{string}"/></returns>
        public async Task<string> GetMaxDocNo(string beginReqNo, CancellationToken cancellationToken)
        {
            return await TableNoTracking.Where(x => x.RequestNo.StartsWith(beginReqNo)).Select(x => x.RequestNo).MaxAsync(cancellationToken);
        }

        /// <summary>
        /// The GetDocumentEstates
        /// </summary>
        /// <param name="documentId">The documentId<see cref="Guid"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{Document}"/></returns>
        public async Task<Document> GetDocumentEstates(Guid documentId, CancellationToken cancellationToken)
        {

            return await TableNoTracking.Include(x => x.DocumentEstates).ThenInclude(x => x.DocumentEstateOwnershipDocuments).ThenInclude(x => x.DocumentPerson)
                .Include(x => x.DocumentEstates).ThenInclude(x => x.DocumentEstateAttachments)
                .Include(x => x.DocumentEstates).ThenInclude(x => x.DocumentEstateSeparationPieces).ThenInclude(x => x.InverseAnbari)
                .Include(x => x.DocumentEstates).ThenInclude(x => x.DocumentEstateSeparationPieces).ThenInclude(x => x.InverseParking)
                .Include(x => x.DocumentEstates).ThenInclude(x => x.DocumentEstateSeparationPieces).ThenInclude(x => x.DocumentEstateSeparationPieceKind)
                .Include(x => x.DocumentEstates).ThenInclude(x => x.DocumentEstateSeparationPieces).ThenInclude(x => x.EstatePieceType)
                .Include(x => x.DocumentEstates).ThenInclude(x => x.DocumentEstateSeparationPieces).ThenInclude(x => x.DocumentEstateSeparationPiecesQuota)
                .Include(x => x.DocumentEstates).ThenInclude(y => y.DocumentEstateQuota).ThenInclude(y => y.DocumentPerson)
                .Include(x => x.DocumentEstates).ThenInclude(y => y.DocumentEstateQuotaDetails).ThenInclude(y => y.DocumentPersonBuyer)
                .Include(x => x.DocumentEstates).ThenInclude(y => y.DocumentEstateQuotaDetails).ThenInclude(y => y.DocumentPersonSeller)
                .Include(x => x.DocumentInquiries)
                .Include(x => x.DocumentType).Include(x => x.DocumentInfoConfirm).Where(x => x.Id == documentId).FirstOrDefaultAsync(cancellationToken);
        }
        /// <summary>
        /// The GetDocumentSms
        /// </summary>
        /// <param name="documentId">The documentId<see cref="Guid"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{Document}"/></returns>
        public async Task<Document> GetDocumentSms(Guid documentId, CancellationToken cancellationToken)
        {

            return await TableNoTracking.Include(x => x.DocumentSms)
                .Include(x => x.DocumentInquiries)
                .Include(x => x.DocumentType).Include(x => x.DocumentInfoConfirm).Where(x => x.Id == documentId).FirstOrDefaultAsync(cancellationToken);
        }
        /// <summary>
        /// The GetDocumentVehicles
        /// </summary>
        /// <param name="documentId">The documentId<see cref="Guid"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{Document}"/></returns>
        public async Task<Document> GetDocumentVehicles(Guid documentId, CancellationToken cancellationToken)
        {

            return await TableNoTracking.Include(x => x.DocumentVehicles).ThenInclude(z => z.DocumentVehicleType)
                .Include(x => x.DocumentVehicles).ThenInclude(y => y.DocumentVehicleTip)
                 .Include(x => x.DocumentVehicles).ThenInclude(y => y.DocumentVehicleQuota)
                 .Include(x => x.DocumentVehicles).ThenInclude(y => y.DocumentVehicleQuotaDetails)
                .Include(x => x.DocumentVehicles).ThenInclude(z => z.DocumentVehicleSystem)
                .Include(x => x.DocumentType)
                .Include(x => x.DocumentInfoConfirm).Where(x => x.Id == documentId)
                .FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// The GetDocumentForConfirmationOfAuthenticity
        /// </summary>
        /// <param name="NationalCode">The NationalCode<see cref="string"/></param>
        /// <param name="SecretCode">The SecretCode<see cref="int"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{Document}"/></returns>
        public async Task<Document> GetDocumentForConfirmationOfAuthenticity(string NationalCode, int SecretCode, CancellationToken cancellationToken)
        {
            var result = (await TableNoTracking.Where(y => y.NationalNo == NationalCode && y.ClassifyNo == SecretCode).
               Include(y => y.DocumentInfoJudgement).
               Include(y => y.DocumentPeople).ThenInclude(x => x.DocumentPersonType).
               Include(y => y.DocumentInfoOther).
               Include(y => y.DocumentInfoText).
               Include(y => y.DocumentCases).
               Include(y => y.DocumentVehicles).
               Include(x => x.DocumentType).
               Include(x => x.DocumentInfoConfirm).

               FirstOrDefaultAsync(cancellationToken));

            return result;
        }

        /// <summary>
        /// The GetDocumentLookupItems
        /// </summary>
        /// <param name="pageIndex">The pageIndex<see cref="int"/></param>
        /// <param name="pageSize">The pageSize<see cref="int"/></param>
        /// <param name="GridSearchInput">The GridSearchInput<see cref="ICollection{SearchData}"/></param>
        /// <param name="GlobalSearch">The GlobalSearch<see cref="string"/></param>
        /// <param name="gridSortInput">The gridSortInput<see cref="SortData"/></param>
        /// <param name="selectedItemsIds">The selectedItemsIds<see cref="IList{string}"/></param>
        /// <param name="scriptoriumId">The scriptoriumId<see cref="string"/></param>
        /// <param name="ScriptoriumTitle">The ScriptoriumTitle<see cref="string"/></param>
        /// <param name="FieldsNotInFilterSearch">The FieldsNotInFilterSearch<see cref="List{string}"/></param>
        /// <param name="extraParams">The extraParams<see cref="DocumentSearchExtraParams"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <param name="isOrderBy">The isOrderBy<see cref="bool"/></param>
        /// <returns>The <see cref="Task{DocumentLookupRepositoryObject}"/></returns>
        public async Task<DocumentLookupRepositoryObject> GetDocumentLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch,
SortData gridSortInput, IList<string> selectedItemsIds, string scriptoriumId, string ScriptoriumTitle, List<string> FieldsNotInFilterSearch, DocumentSearchExtraParams extraParams, CancellationToken cancellationToken, bool isOrderBy = false)
        {
            DocumentLookupRepositoryObject result = new();
            var initialQuery = TableNoTracking.
                Include(x => x.DocumentType)
                .Where(x => x.ScriptoriumId == scriptoriumId);

            if (extraParams is not null)
            {
                if (!string.IsNullOrWhiteSpace(extraParams.DocumentTypeId))
                    initialQuery = initialQuery.Where(x => x.DocumentTypeId == extraParams.DocumentTypeId);

            }

            var query = initialQuery.Select(y => new DocumentLookupItem
            {
                RequestNo = y.RequestNo,
                ClassifyNo = y.ClassifyNo == 0 ? "" : y.ClassifyNo.ToString(),
                DocumentDate = y.DocumentDate,
                DocumentTypeTitle = y.DocumentType.Title,
                NationalNo = y.NationalNo,
                StateId = y.State,
                ScriptorumTitle = ScriptoriumTitle,
                RequestDate = y.RequestDate,
                WriteInBookDate = y.WriteInBookDate,
                DocumentPersons = string.Join(",", y.DocumentPeople.Select(p => p.Name + " " + p.Family).ToList()),
                DocumentPersonList = y.DocumentPeople.Select(p => p.Name + " " + p.Family).ToList(),
                DocumentCases = string.Join(",", y.DocumentCases.Select(p => p.Title).ToList()),
                DocumentCaseList = y.DocumentCases.Select(p => p.Title).ToList(),
                Id = y.Id.ToString(),
            });
            SearchData personsFilter = GridSearchInput.Where(s => s.Filter.ToLower() == "DocumentPersons".ToLower()).FirstOrDefault();
            SearchData casesFilter = GridSearchInput.Where(s => s.Filter.ToLower() == "DocumentCases".ToLower()).FirstOrDefault();
            string filterQueryString = LambdaString<DocumentLookupItem, SearchData>.CreateWhereLambdaString(new DocumentLookupItem(), GridSearchInput, GlobalSearch, FieldsNotInFilterSearch);

            if (personsFilter is not null)
                filterQueryString = filterQueryString.AddLambdaQueryForEntityFieldList(GlobalSearch, "DocumentPersonList", personsFilter.Value);

            if (casesFilter is not null)
                filterQueryString = filterQueryString.AddLambdaQueryForEntityFieldList(GlobalSearch, "DocumentCaseList", casesFilter.Value);

            if (!string.IsNullOrWhiteSpace(GlobalSearch))
            {
                if (personsFilter is null)
                    filterQueryString = filterQueryString.AddLambdaQueryForEntityFieldList(GlobalSearch, "DocumentPersonList");

                if (casesFilter is null)
                    filterQueryString = filterQueryString.AddLambdaQueryForEntityFieldList(GlobalSearch, "DocumentCaseList");
            }

            if (gridSortInput.Sort.ToLower() == "DocumentCases".ToLower())
                gridSortInput.Sort = "DocumentCaseList.FirstOrDefault() ";

            if (gridSortInput.Sort.ToLower() == "DocumentPersons".ToLower())
                gridSortInput.Sort = "DocumentPersonList.FirstOrDefault() ";

            if (pageIndex == 1 && selectedItemsIds.Count > 0)
            {

                var selectedItems = await TableNoTracking.Include(x => x.DocumentType).Where(x => x.ScriptoriumId == scriptoriumId).ToListAsync(cancellationToken);

                // Filter and project the data in memory
                var selectedItemQuery = selectedItems
                    .Where(p => selectedItemsIds.Contains(p.Id.ToString()))
                    .Select(y => new DocumentLookupItem
                    {
                        RequestNo = y.RequestNo,
                        ClassifyNo = y.ClassifyNo.ToString(),
                        DocumentDate = y.DocumentDate,
                        DocumentTypeTitle = y.DocumentType.Title,
                        NationalNo = y.NationalNo,
                        RequestDate = y.RequestDate,
                        WriteInBookDate = y.WriteInBookDate,
                        DocumentPersons = string.Join(",", y.DocumentPeople.Select(p => p.Name + " " + p.Family).ToList()),
                        DocumentPersonList = y.DocumentPeople.Select(p => p.Name + " " + p.Family).ToList(),
                        DocumentCases = string.Join(",", y.DocumentCases.Select(p => p.Title).ToList()),
                        DocumentCaseList = y.DocumentCases.Select(p => p.Title).ToList(),
                        Id = y.Id.ToString(),
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

        /// <summary>
        /// The GetOldExecutiveDocumentLookupItems
        /// </summary>
        /// <param name="pageIndex">The pageIndex<see cref="int"/></param>
        /// <param name="pageSize">The pageSize<see cref="int"/></param>
        /// <param name="GridSearchInput">The GridSearchInput<see cref="ICollection{SearchData}"/></param>
        /// <param name="GlobalSearch">The GlobalSearch<see cref="string"/></param>
        /// <param name="gridSortInput">The gridSortInput<see cref="SortData"/></param>
        /// <param name="selectedItemsIds">The selectedItemsIds<see cref="IList{string}"/></param>
        /// <param name="scriptoriumId">The scriptoriumId<see cref="string"/></param>
        /// <param name="FieldsNotInFilterSearch">The FieldsNotInFilterSearch<see cref="List{string}"/></param>
        /// <param name="extraParams">The extraParams<see cref="OldExecutiveDocumentSearchExtraParams"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <param name="isOrderBy">The isOrderBy<see cref="bool"/></param>
        /// <returns>The <see cref="Task{OldExecutiveDocumentLookupRepositoryObject}"/></returns>
        public async Task<OldExecutiveDocumentLookupRepositoryObject> GetOldExecutiveDocumentLookupItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch,
SortData gridSortInput, IList<string> selectedItemsIds, string scriptoriumId, List<string> FieldsNotInFilterSearch, OldExecutiveDocumentSearchExtraParams extraParams, CancellationToken cancellationToken, bool isOrderBy = false)
        {
            OldExecutiveDocumentLookupRepositoryObject result = new();
            var initialQuery = TableNoTracking.
                Include(x => x.DocumentType)
                .Where(x => x.ScriptoriumId == scriptoriumId && x.State == "1");

            if (extraParams is not null)
            {
                if (!string.IsNullOrWhiteSpace(extraParams.DocumentTypeId))
                    initialQuery = initialQuery.Where(x => x.DocumentTypeId == extraParams.DocumentTypeId);

            }

            var query = initialQuery.Select(y => new OldExecutiveDocumentLookupItem
            {
                Title = y.DocumentType.Title + (string.IsNullOrEmpty(y.NationalNo) ? " " : " - شناسه یکتا ثبت: ") + y.NationalNo + (y.ClassifyNo == null ? " " : " - شماره ترتیب: ") + y.ClassifyNo.ToString() + (string.IsNullOrEmpty(y.DocumentDate) ? " " : " - به تاریخ: ") + y.DocumentDate,
                Id = y.Id.ToString(),
            });
            string filterQueryString = LambdaString<OldExecutiveDocumentLookupItem, SearchData>.CreateWhereLambdaString(new OldExecutiveDocumentLookupItem(), GridSearchInput, GlobalSearch, FieldsNotInFilterSearch);

            if (pageIndex == 1 && selectedItemsIds.Count > 0)
            {

                var selectedItems = await TableNoTracking.Include(x => x.DocumentType).Where(x => x.ScriptoriumId == scriptoriumId && x.State == "1").ToListAsync(cancellationToken);

                // Filter and project the data in memory
                var selectedItemQuery = selectedItems
                    .Where(p => selectedItemsIds.Contains(p.Id.ToString()))
                    .Select(y => new OldExecutiveDocumentLookupItem
                    {
                        Title = y.DocumentType.Title + (string.IsNullOrEmpty(y.NationalNo) ? " " : " - شناسه یکتا ثبت: ") + y.NationalNo + (y.ClassifyNo == null ? " " : " - شماره ترتیب: ") + y.ClassifyNo.ToString() + (string.IsNullOrEmpty(y.DocumentDate) ? " " : " - به تاریخ: ") + y.DocumentDate,
                        Id = y.Id.ToString(),
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

        /// <summary>
        /// The GetDocumentInformation
        /// </summary>
        /// <param name="documentId">The documentId<see cref="Guid"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{Document}"/></returns>
        public async Task<Document> GetDocumentInformation(Guid documentId, CancellationToken cancellationToken)
        {
            return await TableNoTracking
                .Include(x => x.DocumentType)
                .Include(x => x.DocumentInfoOther)
                .Include(x => x.DocumentInfoJudgement)
                .Include(x => x.DocumentPeople)
                .Include(x => x.DocumentInfoText)
                .Include(x => x.DocumentEstates)
                .Where(x => x.Id == documentId).FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// The GetDocumentBook
        /// </summary>
        /// <param name="scriptorumId">The scriptorumId<see cref="string"/></param>
        /// <param name="currentPageNumber">The currentPageNumber<see cref="int"/></param>
        /// <param name="NationalNo">The NationalNo<see cref="string"/></param>
        /// <param name="documentClassifyNo">The documentClassifyNo<see cref="int?"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{(Document, int)}"/></returns>
        public async Task<(Document, int)> GetDocumentBook(string scriptorumId, int currentPageNumber, string NationalNo, int? documentClassifyNo, CancellationToken cancellationToken)
        {
            Document document;
            var initialQuery = TableNoTracking.Include(x => x.DocumentType).ThenInclude(x => x.DocumentTypeGroup2).Include(x => x.DocumentType).ThenInclude(x => x.DocumentTypeGroup1).Include(x => x.DocumentPeople).OrderByDescending(x => x.NationalNo).Where(x => x.State == State.EliminationFaults && x.ScriptoriumId == scriptorumId && x.ClassifyNo == documentClassifyNo);
            if (!string.IsNullOrEmpty(NationalNo))
            {
                int FindPageNumber = initialQuery.ToList().FindIndex(x => (x.NationalNo == NationalNo || string.IsNullOrEmpty(NationalNo))) + 1;
                document = await initialQuery.Where(x => (x.NationalNo == NationalNo || string.IsNullOrEmpty(NationalNo))).FirstOrDefaultAsync(cancellationToken);

                return (document, FindPageNumber);

            }
            return (await initialQuery.Where(x => (x.NationalNo == NationalNo || string.IsNullOrEmpty(NationalNo))).Skip(currentPageNumber - 1).FirstOrDefaultAsync(cancellationToken), currentPageNumber);
        }

        /// <summary>
        /// The GetDocumentElectronicBookTotalCount
        /// </summary>
        /// <param name="scriptorumId">The scriptorumId<see cref="string"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{int}"/></returns>
        public async Task<int> GetDocumentElectronicBookTotalCount(string scriptorumId, CancellationToken cancellationToken)
        {
            return await TableNoTracking
        .Where(x => x.ScriptoriumId == scriptorumId && x.State == State.EliminationFaults).CountAsync(cancellationToken);
        }

        /// <summary>
        /// The GetDocumentByNoPrint
        /// </summary>
        /// <param name="classifyNo">The classifyNo<see cref="string"/></param>
        /// <param name="relatedDocumentNo">The relatedDocumentNo<see cref="string"/></param>
        /// <param name="scriptoriumId">The scriptoriumId<see cref="string"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{IEnumerable{Document}}"/></returns>
        public async Task<IEnumerable<Document>> GetDocumentByNoPrint(string classifyNo, string relatedDocumentNo, string scriptoriumId, CancellationToken cancellationToken)
        {
            return await TableNoTracking.Include(x => x.DocumentType).Include(x => x.DocumentVehicles).ThenInclude(x => x.DocumentVehicleType)
                .Include(x => x.DocumentVehicles).ThenInclude(x => x.DocumentVehicleTip)
                .Include(x => x.DocumentVehicles).ThenInclude(x => x.DocumentVehicleSystem)
                .Include(x => x.DocumentEstates).ThenInclude(x => x.DocumentEstateType)
                .Where(x => x.RelatedDocumentNo == classifyNo || x.RelatedDocumentNo == relatedDocumentNo).ToListAsync(cancellationToken);
        }

        /// <summary>
        /// The GetDocumentPrintRelatedPeople
        /// </summary>
        /// <param name="documentId">The documentId<see cref="Guid"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{Document}"/></returns>
        public async Task<Document> GetDocumentPrintRelatedPeople(Guid documentId, CancellationToken cancellationToken)
        {
            return await TableNoTracking.Include(x => x.DocumentPersonRelatedDocuments).ThenInclude(x => x.AgentType).Include(x => x.DocumentType)
             .Where(x => x.Id == documentId).FirstOrDefaultAsync(cancellationToken);
        }


        /// <summary>
        /// The RemoveVehicleQuota
        /// </summary>
        /// <param name="ids">The ids<see cref="List{Guid}"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task RemoveVehicleQuota(List<Guid> ids)
        {
            await DbContext.DocumentVehicleQuota.Where(q => ids.Contains(q.Id)).ExecuteDeleteAsync();


        }

        /// <summary>
        /// The RemoveVehicleQuotaDetails
        /// </summary>
        /// <param name="ids">The ids<see cref="List{Guid}"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task RemoveVehicleQuotaDetails(List<Guid> ids)
        {


            await DbContext.DocumentVehicleQuotaDetails.Where(q => ids.Contains(q.Id)).ExecuteDeleteAsync();
        }

        public void ClearTracking()
        {
            DbContext.ChangeTracker.Clear();// Clears all tracked entities

        }



        /// <summary>
        /// The RemoveEstateQuota
        /// </summary>
        /// <param name="ids">The ids<see cref="List{Guid}"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task RemoveEstateQuota(List<Guid> ids)
        {

            await DbContext.DocumentEstateQuota.Where(q => ids.Contains(q.Id)).ExecuteDeleteAsync();
        }

        /// <summary>
        /// The RemoveEstateQuotaDetails
        /// </summary>
        /// <param name="ids">The ids<see cref="List{Guid}"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task RemoveEstateQuotaDetails(List<Guid> ids)
        {


            await DbContext.DocumentEstateQuotaDetails.Where(q => ids.Contains(q.Id)).ExecuteDeleteAsync();
        }



        /// <summary>
        /// The RemoveEstateAttchments
        /// </summary>
        /// <param name="ids">The ids<see cref="List{Guid}"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task RemoveEstateAttchments(List<Guid> ids)
        {


            await DbContext.DocumentEstateAttachments.Where(q => ids.Contains(q.Id)).ExecuteDeleteAsync();
        }

        /// <summary>
        /// The RemoveEstateAttchments
        /// </summary>
        /// <param name="ids">The ids<see cref="List{Guid}"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task RemoveEstateSeparations(List<Guid> ids)
        {

            await DbContext.DocumentEstateSeparationPieces.Where(q => ids.Contains(q.Id)).ExecuteDeleteAsync();
        }
        /// <summary>
        /// The GetRequestType
        /// </summary>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <param name="documentId">The documentId<see cref="string"/></param>
        /// <param name="nationalNo">The nationalNo<see cref="string"/></param>
        /// <returns>The <see cref="Task{RequsetType}"/></returns>
        public async Task<RequsetType> GetRequestType(CancellationToken cancellationToken, string documentId = null, string nationalNo = null)
        {
            if (documentId != null)
            {
                return await TableNoTracking
                .Where(d => d.Id == Guid.Parse(documentId))
                .Include(d => d.DocumentType)
                .Select(d => new Notary.SSAA.BO.SharedKernel.Contracts.Coordinator.Document.RequsetType(d.DocumentType.Id,
                        d.DocumentType.DocumentTypeGroup1Id, d.DocumentType.DocumentTypeGroup2Id, d.State,
                        d.DocumentType.Title, d.DocumentType.IsSupportive,
                        d.DocumentType.HasAsset, d.DocumentType.AssetIsRequired,
                        d.DocumentType.WealthType, d.DocumentType.DocumentTextWritingAllowed,
                        d.DocumentType.HasRelatedDocument, d.DocumentType.HasCount,
                        d.DocumentType.HasSubject, d.DocumentType.SubjectIsRequired,
                        d.DocumentType.HasEstateInquiry, d.DocumentType.EstateInquiryIsRequired,
                        d.DocumentType.HasNonregisteredEstate, d.DocumentType.HasEstateAttachments,
                        d.DocumentType.HasAssetType, d.DocumentType.AssetIsRequired, d.DocumentType.GeneralPersonPostTitle, d.Id.ToString())
                    ).FirstOrDefaultAsync();
            }
            else
            if (nationalNo != null)
            {
                return await TableNoTracking
                .Where(d => d.NationalNo == nationalNo)
                .Include(d => d.DocumentType)
                .Select(d => new Notary.SSAA.BO.SharedKernel.Contracts.Coordinator.Document.RequsetType(d.DocumentType.Id,
                        d.DocumentType.DocumentTypeGroup1Id, d.DocumentType.DocumentTypeGroup2Id, d.State,
                        d.DocumentType.Title, d.DocumentType.IsSupportive,
                        d.DocumentType.HasAsset, d.DocumentType.AssetIsRequired,
                        d.DocumentType.WealthType, d.DocumentType.DocumentTextWritingAllowed,
                        d.DocumentType.HasRelatedDocument, d.DocumentType.HasCount,
                        d.DocumentType.HasSubject, d.DocumentType.SubjectIsRequired,
                        d.DocumentType.HasEstateInquiry, d.DocumentType.EstateInquiryIsRequired,
                        d.DocumentType.HasNonregisteredEstate, d.DocumentType.HasEstateAttachments,
                        d.DocumentType.HasAssetType, d.DocumentType.AssetIsRequired, d.DocumentType.GeneralPersonPostTitle, d.Id.ToString())

                ).FirstOrDefaultAsync();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// The GetRequestTypes
        /// </summary>
        /// <param name="state">The state<see cref="string"/></param>
        /// <param name="relatedScriptoriumId">The relatedScriptoriumId<see cref="string"/></param>
        /// <param name="relatedDocumentNoes">The relatedDocumentNoes<see cref="List{string}"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{List{RequsetType}}"/></returns>
        public async Task<List<RequsetType>> GetRequestTypes(string state, string relatedScriptoriumId, List<string> relatedDocumentNoes, CancellationToken cancellationToken)
        {
            IQueryable<object> query;
            if (state != null)
            {
                query = TableNoTracking.Where(x => x.State == state);
            }
            if (relatedScriptoriumId != null)
            {
                query = TableNoTracking.Where(x => x.RelatedScriptoriumId == relatedScriptoriumId);
            }
            if (relatedDocumentNoes != null)
            {
                query = TableNoTracking.Where(x => relatedDocumentNoes.Contains(x.RelatedDocumentNo));
            }
            return await TableNoTracking
            .Include(d => d.DocumentType)
            .Select(d => new Notary.SSAA.BO.SharedKernel.Contracts.Coordinator.Document.RequsetType(d.DocumentType.Id,
                    d.DocumentType.DocumentTypeGroup1Id, d.DocumentType.DocumentTypeGroup2Id, d.State,
                    d.DocumentType.Title, d.DocumentType.IsSupportive,
                    d.DocumentType.HasAsset, d.DocumentType.AssetIsRequired,
                    d.DocumentType.WealthType, d.DocumentType.DocumentTextWritingAllowed,
                    d.DocumentType.HasRelatedDocument, d.DocumentType.HasCount,
                    d.DocumentType.HasSubject, d.DocumentType.SubjectIsRequired,
                    d.DocumentType.HasEstateInquiry, d.DocumentType.EstateInquiryIsRequired,
                    d.DocumentType.HasNonregisteredEstate, d.DocumentType.HasEstateAttachments,
                    d.DocumentType.HasAssetType, d.DocumentType.AssetIsRequired, d.DocumentType.GeneralPersonPostTitle, d.Id.ToString())
                ).ToListAsync(cancellationToken);
        }

        /// <summary>
        /// The GetAnnotationsForOnePerson
        /// </summary>
        /// <param name="natonalno">The natonalno<see cref="string"/></param>
        /// <param name="personId">The personId<see cref="string"/></param>
        /// <param name="documentId">The documentId<see cref="string"/></param>
        /// <returns>The <see cref="Task{List{DocumentPerson}}"/></returns>
        public async Task<List<DocumentPerson>> GetAnnotationsForOnePerson(string natonalno, string personId, string documentId)
        {
            var persons = await DbContext.DocumentPeople.Where(p => p.DocumentId == Guid.Parse(documentId) && p.NationalNo == natonalno && p.Id != Guid.Parse(personId) && p.IsRelated == Enumerations.YesNo.Yes.GetString())?.ToListAsync();
            return persons;
        }

        /// <summary>
        /// The GetDocumentById
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/></param>
        /// <param name="details">The details<see cref="List{string}"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{Document}"/></returns>
        public async Task<Document> GetDocumentById(Guid id, List<string> details, CancellationToken cancellationToken)
        {

            IQueryable<Document> query = Table.Where(x => x.Id == id).Include(x => x.DocumentType).Include(x => x.DocumentInfoConfirm);

            foreach (var item in details)
            {
                if (item == "DocumentInfoConfirm")
                {
                    query = query.Include(x => x.DocumentInfoConfirm);
                }
                if (item == "DocumentCostQuestion")
                {
                    query = query.Include(x => x.DocumentInfoOther);
                }
                if (item == "DocumentCases")
                {
                    query = query.Include(x => x.DocumentCases);
                }
                if (item == "DocumentPeople")
                {
                    query = query
                    .Include(x => x.DocumentPeople)
                    .ThenInclude(x => x.DocumentPersonRelatedAgentPeople)

                    .Include(x => x.DocumentPeople).ThenInclude(x => x.DocumentPersonType)
                    .Include(x => x.DocumentPersonRelatedDocuments)
                   .ThenInclude(x => x.AgentType)
                    .Include(x => x.DocumentInfoConfirm)
;

                }
                if (item == "DocumentVehicles")
                {
                    query = query.Include(x => x.DocumentVehicles).ThenInclude(x => x.DocumentVehicleQuota)
                        .Include(x => x.DocumentVehicles).ThenInclude(x => x.DocumentVehicleQuotaDetails)
                        .Include(x => x.DocumentVehicles).ThenInclude(x => x.DocumentVehicleType)
                .Include(x => x.DocumentVehicles).ThenInclude(y => y.DocumentVehicleTip)
                .Include(x => x.DocumentVehicles).ThenInclude(z => z.DocumentVehicleSystem);

                }
                if (item == "DocumentEstates")
                {
                    query = query.Include(x => x.DocumentEstates).ThenInclude(x => x.DocumentEstateType)
                        .Include(x => x.DocumentEstates).ThenInclude(x => x.DocumentEstateOwnershipDocuments)
                        .ThenInclude(x => x.DocumentPerson)
                        .Include(x => x.DocumentEstates).ThenInclude(x => x.DocumentEstateAttachments)
                        .Include(x => x.DocumentEstates).ThenInclude(x => x.DocumentEstateSeparationPieces)
                        .ThenInclude(x => x.InverseAnbari)
                        .Include(x => x.DocumentEstates).ThenInclude(x => x.DocumentEstateSeparationPieces)
                        .ThenInclude(x => x.InverseParking)
                        .Include(x => x.DocumentEstates).ThenInclude(x => x.DocumentEstateSeparationPieces)
                        .ThenInclude(x => x.DocumentEstateSeparationPieceKind)
                        .Include(x => x.DocumentEstates).ThenInclude(x => x.DocumentEstateSeparationPieces)
                        .ThenInclude(x => x.EstatePieceType)
                        .Include(x => x.DocumentEstates).ThenInclude(x => x.DocumentEstateSeparationPieces)
                        .ThenInclude(x => x.DocumentEstateSeparationPiecesQuota)
                        .Include(x => x.DocumentEstates).ThenInclude(y => y.DocumentEstateQuota)
                        .ThenInclude(y => y.DocumentPerson)
                        .Include(x => x.DocumentEstates).ThenInclude(y => y.DocumentEstateQuotaDetails)
                        .ThenInclude(y => y.DocumentPersonBuyer)
                        .Include(x => x.DocumentEstates).ThenInclude(y => y.DocumentEstateQuotaDetails)
                        .ThenInclude(y => y.DocumentPersonSeller);
                }
                if (item == "DocumentRelatedPeople")
                {
                    query = query.Include(x => x.DocumentType).Include(x => x.DocumentPersonRelatedDocuments).Include(x => x.DocumentPeople)/*.ThenInclude(x=>x.MainPerson)
                        .Include(x=>x.DocumentPersonRelatedDocuments ).ThenInclude(x=>x.AgentPerson)
                        .Include(x=>x.DocumentPersonRelatedAgentDocuments)*/;
                }
                if (item == "DocumentRelationDocuments")
                {
                    query = query.Include(x => x.DocumentRelationDocuments);

                }
                if (item == "DocumentCosts")
                {
                    query = query.Include(x => x.DocumentCosts).Include(x => x.DocumentCostUnchangeds);
                }
                if (item == "DocumentPayments")
                {
                    query = query.Include(x => x.DocumentPayments).ThenInclude(x => x.CostType)
                        .Include(x => x.DocumentPayments).ThenInclude(x => x.ReusedDocumentPayment);
                }
                if (item == "DocumentSms")
                {
                    query = query.Include(x => x.DocumentSms);
                }
                if (item == "DocumentInfoJudgment")
                {
                    query = query.Include(x => x.DocumentInfoJudgement);

                }
                if (item == "DocumentInfoOther")
                {
                    query = query.Include(x => x.DocumentInfoJudgement).Include(x => x.DocumentInfoOther).Include(x => x.DocumentInfoText).Include(x => x.DocumentInfoConfirm);

                }
                if (item == "DocumentInfoText")
                {
                    query = query.Include(x => x.DocumentInfoText);

                }
                if (item == "DocumentCostQuestion")
                {
                    query = query.Include(x => x.DocumentInfoOther);

                }
                if (item == "DocumentInquiries")
                {
                    query = query.Include(x => x.DocumentInquiries);

                }
                if (item == "DocumentInfoOther")
                {
                    query = query.Include(x => x.DocumentInfoOther);

                }
            }

            return await query.FirstOrDefaultAsync(cancellationToken);
            //return document;
        }

        /// <summary>
        /// The GetDocuments
        /// </summary>
        /// <param name="details">The details<see cref="List{string}"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <param name="id">The id<see cref="string"/></param>
        /// <param name="state">The state<see cref="string"/></param>
        /// <param name="relatedScriptoriumId">The relatedScriptoriumId<see cref="string"/></param>
        /// <param name="relatedDocumentNo">The relatedDocumentNo<see cref="string [ ]"/></param>
        /// <param name="documentTypes">The documentTypes<see cref="string[]"/></param>
        /// <param name="relatedDocumentId">The relatedDocumentId<see cref="string"/></param>
        /// <param name="nationalNoes">The nationalNoes<see cref="List{string}"/></param>
        /// <param name="agentParams">The agentParams<see cref="List{AgentParam}"/></param>
        /// <param name="scriptoriumId">The scriptoriumId<see cref="string"/></param>
        /// <returns>The <see cref="Task{List{Document}}"/></returns>
        public async Task<List<Document>> GetDocuments(List<string> details, CancellationToken cancellationToken
            , string id = null, string state = null,
            string relatedScriptoriumId = null, string[] relatedDocumentNo = null,
            string[] documentTypes = null, string relatedDocumentId = null
            , List<string> nationalNoes = null, List<AgentParam> agentParams = null, string scriptoriumId = null)
        {

            IQueryable<Document> query = TableNoTracking.Include(x => x.DocumentInfoConfirm);
            if (id != null)
            {
                query = TableNoTracking.Where(x => x.Id == Guid.Parse(id));
            }
            if (state != null)
            {
                query = query.Where(x => x.State == state);
            }
            if (relatedScriptoriumId != null)
            {
                query = query.Where(x => x.RelatedScriptoriumId == relatedScriptoriumId);
            }
            if (relatedDocumentId != null)
            {
                query = query.Where(x => x.RelatedDocumentId == Guid.Parse(relatedDocumentId));
            }
            if (relatedDocumentNo != null)
            {
                query = query.Where(x => relatedDocumentNo.Contains(x.RelatedDocumentNo));
            }
            if (documentTypes != null)
            {
                query = query.Where(x => documentTypes.Contains(x.DocumentTypeId));
            }
            if (nationalNoes != null)
            {
                query = query.Where(x => nationalNoes.Contains(x.NationalNo));
            }
            if (agentParams != null)
            {

                query = query.Where(x => agentParams.Where(d => d.AgentDocumentNationalNo == x.NationalNo && d.AgentDocumentScriptoriumId == x.ScriptoriumId).Count() > 0);

            }
            if (scriptoriumId != null)
            {

                query = query.Where(x => x.ScriptoriumId == scriptoriumId);

            }
            query = query.Include(x => x.DocumentType);
            foreach (var item in details)
            {
                if (item == "DocumentCostQuestion")
                {
                    query = query.Include(x => x.DocumentInfoOther);
                }
                if (item == "DocumentCases")
                {
                    query = query.Include(x => x.DocumentCases);
                }
                if (item == "DocumentPeople")
                {
                    query = query.Include(x => x.DocumentPeople);

                }
                if (item == "DocumentVehicles")
                {
                    query = query.Include(x => x.DocumentVehicles).ThenInclude(x => x.DocumentVehicleQuota).Include(x => x.DocumentVehicles).ThenInclude(x => x.DocumentVehicleQuotaDetails);

                }
                if (item == "DocumentEstates")
                {
                    query = query.Include(x => x.DocumentEstates);
                }
                if (item == "DocumentRelatedPeople")
                {
                    query = query.Include(x => x.DocumentPeople).Include(x => x.DocumentPersonRelatedDocuments).ThenInclude(x => x.AgentType);
                }
                if (item == "DocumentRelationDocuments")
                {
                    query = query.Include(x => x.DocumentRelationDocuments);

                }
                if (item == "DocumentCosts")
                {
                    query = query.Include(x => x.DocumentCosts).ThenInclude(x => x.CostType).Include(x => x.DocumentCostUnchangeds);
                }
                if (item == "DocumentInfoJudgment")
                {
                    query = query.Include(x => x.DocumentInfoJudgement);

                }
                if (item == "DocumentInfoOther")
                {
                    query = query.Include(x => x.DocumentInfoOther).Include(x => x.DocumentInfoText);

                }
                if (item == "DocumentInfoText")
                {
                    query = query.Include(x => x.DocumentInfoText);

                }
                if (item == "DocumentCostQuestion")
                {
                    query = query.Include(x => x.DocumentInfoOther);

                }
                if (item == "DocumentInquiries")
                {
                    query = query.Include(x => x.DocumentInquiries);

                }
                if (item == "DocumentInfoOther")
                {
                    query = query.Include(x => x.DocumentInfoOther);

                }
            }
            var documents = await query.ToListAsync(cancellationToken);
            return documents;
        }

        /// <summary>
        /// The GetDocumentByNationalNo
        /// </summary>
        /// <param name="details">The details<see cref="List{string}"/></param>
        /// <param name="nationalNo">The nationalNo<see cref="string"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{Document}"/></returns>
        public async Task<Document> GetDocumentByNationalNo(List<string> details, string nationalNo, CancellationToken cancellationToken)
        {
            IQueryable<Document> query = TableNoTracking.Where(x => x.NationalNo == nationalNo).Include(x => x.DocumentInfoConfirm);

            foreach (var item in details)
            {
                if (item == "DocumentTypeGroup2")
                {
                    query = query.Include(x => x.DocumentType).ThenInclude(x => x.DocumentTypeGroup2);

                }

                if (item == "DocumentCostQuestion")
                {
                    query = query.Include(x => x.DocumentInfoOther);
                }
                if (item == "DocumentCases")
                {
                    query = query.Include(x => x.DocumentCases);
                }
                if (item == "DocumentPeople")
                {
                    query = query.Include(x => x.DocumentPeople);

                }
                if (item == "DocumentVehicles")
                {
                    query = query.Include(x => x.DocumentVehicles).ThenInclude(x => x.DocumentVehicleQuota).Include(x => x.DocumentVehicles).ThenInclude(x => x.DocumentVehicleQuotaDetails);

                }
                if (item == "DocumentEstates")
                {
                    query = query.Include(x => x.DocumentEstates).ThenInclude(x => x.DocumentEstateOwnershipDocuments)
                                  .Include(x => x.DocumentEstates).ThenInclude(x => x.DocumentEstateSeparationPieces)
                                  .Include(x => x.DocumentEstates).ThenInclude(x => x.DocumentEstateAttachments);
                }
                if (item == "DocumentRelatedPeople")
                {
                    query = query.Include(x => x.DocumentPersonRelatedDocuments);
                }
                if (item == "DocumentRelationDocuments")
                {
                    query = query.Include(x => x.DocumentRelationDocuments);

                }
                if (item == "DocumentCosts")
                {
                    query = query.Include(x => x.DocumentCosts).ThenInclude(x => x.CostType).Include(x => x.DocumentCostUnchangeds);



                }
                if (item == "DocumentInfoJudgment")
                {
                    query = query.Include(x => x.DocumentInfoJudgement);

                }
                if (item == "DocumentInfoOther")
                {
                    query = query.Include(x => x.DocumentInfoOther).Include(x => x.DocumentInfoText);

                }
                if (item == "DocumentInfoText")
                {
                    query = query.Include(x => x.DocumentInfoText);

                }
                if (item == "DocumentCostQuestion")
                {
                    query = query.Include(x => x.DocumentInfoOther);

                }
                if (item == "DocumentInquiries")
                {
                    query = query.Include(x => x.DocumentInquiries);

                }
                if (item == "DocumentInfoOther")
                {
                    query = query.Include(x => x.DocumentInfoOther);

                }
            }
            Document document = await query.FirstOrDefaultAsync(cancellationToken);
            return document;
        }

        /// <summary>
        /// The GetDocumentTypeById
        /// </summary>
        /// <param name="documentTypeID">The documentTypeID<see cref="string"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{DocumentType}"/></returns>
        public async Task<DocumentType> GetDocumentTypeById(string documentTypeID, CancellationToken cancellationToken)
        {
            IQueryable<DocumentType> query = DbContext.DocumentTypes.Where(x => x.Id == documentTypeID);

            DocumentType documentType = await query.FirstOrDefaultAsync(cancellationToken);
            return documentType;
        }

        /// <summary>
        /// The GetDocumentLimitation
        /// </summary>
        /// <param name="nationalNo">The nationalNo<see cref="string"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{DocumentLimit}"/></returns>
        public async Task<DocumentLimit> GetDocumentLimitation(string nationalNo, CancellationToken cancellationToken)
        {
            DocumentLimit documentLimit = null;
            string id = TableNoTracking
                .Where(x => x.NationalNo == nationalNo)
                .Select(x => x.Id.ToString())
                .FirstOrDefault();

            if (id != null)
            {
                documentLimit = await DbContext.DocumentLimits
                    .Where(d => d.DocumentId == Guid.Parse(id) &&
                                (
                                    (d.ToDate != null && d.ToDate.CompareTo(dateTimeService.CurrentPersianDate) >= 0
                                                        && d.FromDate.CompareTo(dateTimeService.CurrentPersianDate) <= 0)
                                    || d.ToDate == null
                                    || d.ToDate == "9999/99/99-99:9"
                                ))
                    .FirstOrDefaultAsync(cancellationToken);
            }

            return documentLimit;
        }

        public async Task<Document> GetDocument(string documentRequestNo, CancellationToken cancellationToken)
        {
            return await Table.Include(x => x.DocumentCosts).FirstOrDefaultAsync(x => x.RequestNo == documentRequestNo, cancellationToken);
        }

        public async Task<string> GetMaxDocumentNationalNo(string beginNationalNo, CancellationToken cancellationToken)
        {
            return await TableNoTracking.Where(x => x.NationalNo.StartsWith(beginNationalNo)).Select(x => x.NationalNo).MaxAsync(cancellationToken);
        }

        public async Task<Document> GetDocumentPeople(Guid documentId, CancellationToken cancellationToken)
        {

            return await TableNoTracking.Where(t => t.Id == documentId).Include(t => t.DocumentPeople).ThenInclude(t => t.DocumentPersonType).Include(x => x.DocumentInfoConfirm).Include(x => x.DocumentInfoOther)
                .Include(t => t.DocumentPersonRelatedDocuments).Include(t => t.DocumentType).FirstOrDefaultAsync(cancellationToken);
        }
        public async Task RemoveEstateOwnerShips(List<Guid> ids)
        {


            var documentEstateOwnershipDocumentList = DbContext.ChangeTracker.Entries<DocumentEstateOwnershipDocument>().Where(q => ids.Contains(q.Entity.Id)).ToList();
            foreach (var item in documentEstateOwnershipDocumentList)
            {
                item.State = EntityState.Detached;
            }

        }

        public async Task<int?> GetClassifyNoDocument(string nationalNo, string scriptoriumId, CancellationToken cancellationToken)
        {
            return await TableNoTracking.Where(t => t.ScriptoriumId == scriptoriumId && t.NationalNo == nationalNo)
                .Select(t => t.ClassifyNo).FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<int> GetLastClassifyNoFromDocs( string scriptoriumId, CancellationToken cancellationToken)
        {
            var LastClassifyNo = await TableNoTracking.Include(t => t.DocumentType).Where(t => t.ScriptoriumId == scriptoriumId && (t.State != "8" && t.State != "9") && t.ClassifyNo != null && t.DocumentType.IsSupportive == "2")
                .MaxAsync(t => t.ClassifyNo, cancellationToken);

            return LastClassifyNo!=null? (int)LastClassifyNo : 0;
        }

        public async Task<List<Document>> FindRelatedDeterministicRegServices(string scriptoriumId, List<string> eSTEStateIdCollection,
            List<string> deterministicDocumentTypeCodes, Guid documentId, CancellationToken cancellationToken)
        {



            return await TableNoTracking.Include(t => t.DocumentType).Include(t => t.DocumentInquiries)
                .Where(t => t.ScriptoriumId == scriptoriumId &&
                              (t.DocumentInquiries.Any(s => eSTEStateIdCollection.Contains(s.EstateInquiriesId))) &&
                              deterministicDocumentTypeCodes.Contains(t.DocumentTypeId) && t.Id != documentId
                              && t.State != NotaryRegServiceReqState.CanceledAfterGetCode.GetString()
                              && t.State != NotaryRegServiceReqState.CanceledBeforeGetCode.GetString())
                .ToListAsync(cancellationToken);
        }

        public async Task<int?> GetMaxClassifyNo(string scriptoriumId, string documentTypeId, CancellationToken cancellationToken)
        {
            if (documentTypeId == "0021")
            {
                return await TableNoTracking
                    .Where(t => t.ScriptoriumId == scriptoriumId && t.DocumentTypeId == documentTypeId)
                    .MaxAsync(t => t.ClassifyNo, cancellationToken);
            }
            else
            {

                return await TableNoTracking
                    .Where(t => t.ScriptoriumId == scriptoriumId)
                    .MaxAsync(t => t.ClassifyNo, cancellationToken);
            }

        }

        public async Task<bool> ValidateCurrentClassifyNo(int classifyNo, string scriptoriumId,
            CancellationToken cancellationToken)
        {

            return !await TableNoTracking.Include(t => t.DocumentType).AnyAsync(t => t.ScriptoriumId == scriptoriumId &&
                t.ClassifyNo == classifyNo && t.DocumentType.IsSupportive == "2"
                && (t.State == Enumerations.NotaryRegServiceReqState.SetNationalDocumentNo.GetString()
                    || t.State == Enumerations.NotaryRegServiceReqState.FinalPrinted.GetString()
                    || t.State == Enumerations.NotaryRegServiceReqState.Finalized.GetString()), cancellationToken);
        }
        //public async Task<int?> MaxClassifyNo ( string scriptoriumId, string documentTypeId, CancellationToken cancellationToken )
        //{
        //    var max = TableNoTracking.Include(t => t.DocumentType).Include(t => t.DocumentInfoConfirm)
        //        .Where(t => t.ScriptoriumId == scriptoriumId && t.DocumentType.IsSupportive == "2" &&
        //                    t.DocumentTypeId == documentTypeId
        //                    && t.DocumentInfoConfirm.ConfirmDate != null)
        //        .Select(t => t.DocumentInfoConfirm.ConfirmDate + "-" + t.DocumentInfoConfirm.ConfirmTime).MaxAsync(cancellationToken);

        //    return await TableNoTracking.Include(t=>t.DocumentType).Include(t=>t.DocumentInfoConfirm)
        //            .Where ( t => t.ScriptoriumId == scriptoriumId && t.DocumentType.IsSupportive=="2" && t.DocumentTypeId == documentTypeId 
        //             && t.DocumentInfoConfirm.ConfirmDate!=null )
        //            .MaxAsync ( t => t.ClassifyNo, cancellationToken );



        //}

        public async Task<Document> GetDocumentForDigitalSign(Guid documentId, CancellationToken cancellationToken)
        {
            return await this.Table
                .Include(x => x.DocumentPeople)
                .ThenInclude(x => x.DocumentPersonType)                
                .Include(x => x.DocumentPeople)
                .ThenInclude(x => x.DocumentEstateQuotaDetailDocumentPersonSellers)
                .ThenInclude(x => x.DocumentEstate)              
                .Include(x => x.DocumentPeople)
                .ThenInclude(x => x.DocumentVehicleQuotaDetailDocumentPersonSellers)
                .ThenInclude(x => x.DocumentVehicle)                
                .Include(x => x.DocumentPeople)
                .ThenInclude(x => x.DocumentPersonRelatedMainPeople)
                .ThenInclude(x => x.AgentType)
                .Include(x => x.DocumentInfoConfirm)
                .Include(x => x.DocumentType)
                .Include(x => x.RelatedDocument)
                .Include(x => x.DocumentInfoJudgement)
                .Include(x => x.DocumentInfoOther)
                .Include(x => x.DocumentInfoText)
                .Include(x => x.DocumentEstates)                
                .Include(x => x.DocumentVehicles)
                .ThenInclude(x => x.DocumentVehicleSystem)
                .Include(x => x.DocumentVehicles)
                .ThenInclude(x => x.DocumentVehicleTip)
                .Include(x => x.DocumentVehicles)
                .ThenInclude(x => x.DocumentVehicleType)
                .Include(x => x.DocumentVehicles)                
                .Include(x => x.DocumentCases)                
                .Include(x => x.DocumentCosts)
                .ThenInclude(x => x.CostType)
                .Include(x => x.DocumentCosts)                
                .Where(x => x.Id == documentId)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<DocumentSelselehAyadiRepositoryObject>> GetSelselehAyadi(string DocumentEstateSubSectionId, string DocumentEstateSectionId, string DocumentUnitId, string SecondaryPlaque, string BasicPlaque, IList<string> ExcludedScriptoriumIds, CancellationToken cancellationToken)
        {
            var result = await TableNoTracking
                .Include(x => x.DocumentType)
                .Include(x => x.DocumentEstates)
                .Where(x =>
                    x.DocumentEstates.Any(e =>
                        e.BasicPlaque == BasicPlaque
                        &&
                        e.SecondaryPlaque == SecondaryPlaque &&
                        e.UnitId == DocumentUnitId &&
                        e.EstateSubsectionId == DocumentEstateSubSectionId &&
                        e.EstateSectionId == DocumentEstateSectionId
                        )
                    &&
                    !ExcludedScriptoriumIds.Contains(x.ScriptoriumId) &&
                    (x.State == "6" ||
                     (string.Compare(x.DocumentDate, "1395/06/10") < 0 &&
                      sourceArray.Contains(x.State)))
                      )
                .OrderByDescending(x => x.DocumentDate ?? x.RequestDate)
                .Select(x => new
                {
                    x.DocumentDate,
                    x.NationalNo,
                    x.RequestNo,
                    x.State,
                    x.ScriptoriumId,
                    OnotaryDocumentTypeTitle = x.DocumentType.Title,
                    x.Id
                })
                .ToListAsync(cancellationToken);

            var finalResult = result.Select(x => new DocumentSelselehAyadiRepositoryObject
            {
                CreateDate = x.DocumentDate,
                NationalNo = x.NationalNo,
                ReqNo = x.RequestNo,
                DocStateTitle = Enum.TryParse<NotaryRegServiceReqState>(x.State, out var stateEnum)
                    ? stateEnum.GetEnumDescription()
                    : null,
                ScriptoriumId = x.ScriptoriumId,
                OnotaryDocumentTypeTitle = x.OnotaryDocumentTypeTitle,
                DocumentId = x.Id.ToString()
            }).ToList();

            return finalResult;
        }
    }
}
