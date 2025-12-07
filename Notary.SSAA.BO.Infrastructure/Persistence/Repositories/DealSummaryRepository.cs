using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;
using Notary.SSAA.BO.Infrastructure.Contexts;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.Utilities.Extensions;
using System.Linq.Dynamic.Core;

namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories
{
    public class DealSummaryRepository:Repository<DealSummary>,IDealSummaryRepository
    {
        public DealSummaryRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }
        public async Task<bool> IsAttachedToDealSummary(string inquiryId,CancellationToken cancellationToken)
        {
            var dealSummary = await TableNoTracking                
                .Where(d => d.EstateInquiryId == inquiryId.ToGuid() && d.WorkflowStatesId != EstateConstant.DealSummaryStates.NotSended).FirstOrDefaultAsync(cancellationToken);
            if (dealSummary == null)
                return false;
            else
                return true;
        }
        private static T GetValue<T>(Dictionary<string, object> extraParam, string key)
        {
            if (extraParam.TryGetValue(key, out object value))
            {
                return (T)value;
            }
            return default;
        }
        public async Task<DealSummaryGrid> GetDealSummaryGridItems(int pageIndex, int pageSize, ICollection<SearchData> gridSearchInput, string globalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> fieldsNotInFilterSearch, string scriptoriumId, bool isOrderBy, string[] statusId, Dictionary<string, object> extraParam, CancellationToken cancellationToken)
        {
            DealSummaryGrid result = new();

            DealSummaryGridItem lookupFilterItem = new();

            var query = TableNoTracking
                .Where(x => x.ScriptoriumId == scriptoriumId);

            if (statusId != null && statusId.Length > 0)
            {
                query = query.Where(x => statusId.Contains(x.WorkflowStatesId));
            }
            if (extraParam != null)
            {

                var noFrom = GetValue<string>(extraParam, "DS_NoFrom");
                var noTo = GetValue<string>(extraParam, "DS_NoTo");
                var dateFrom = GetValue<string>(extraParam, "DS_DateFrom");
                var dateTo = GetValue<string>(extraParam, "DS_DateTo");
                var systemNo = GetValue<string>(extraParam, "DS_SystemNo");
                var unitId = GetValue<string>(extraParam, "DS_UnitId");
                var state = GetValue<string>(extraParam, "DS_Status");
                var sectionId = GetValue<string>(extraParam, "DS_SectionId");
                var subSectionId = GetValue<string>(extraParam, "DS_SubSectionId");
                var basic = GetValue<string>(extraParam, "DS_Basic");
                var secondary = GetValue<string>(extraParam, "DS_Secondary");
                var docPrintNo = GetValue<string>(extraParam, "DS_PrintDocNo");
                var personName = GetValue<string>(extraParam, "PersonName");
                var estateElectronicNoteNo = GetValue<string>(extraParam, "DS_EstateElectronicNoteNo");
                var personFamily = GetValue<string>(extraParam, "PersonFamily");
                var personNationalityCode = GetValue<string>(extraParam, "PersonNationalityCode");
                var notaryDocumentNo = GetValue<string>(extraParam, "NotaryDocumentNo");

                query = query.Where(x => (string.IsNullOrWhiteSpace(noFrom) || x.DealNo.CompareTo(noFrom) >= 0));
                query = query.Where(x => (string.IsNullOrWhiteSpace(noTo) || x.DealNo.CompareTo(noTo) <= 0));
                query = query.Where(x => (string.IsNullOrWhiteSpace(dateFrom) || x.DealDate.CompareTo(dateFrom) >= 0));
                query = query.Where(x => (string.IsNullOrWhiteSpace(dateTo) || x.DealDate.CompareTo(dateTo) <= 0));
                query = query.Where(x => (string.IsNullOrWhiteSpace(systemNo) || x.No.Contains(systemNo)));
                query = query.Where(x => (string.IsNullOrWhiteSpace(unitId) || x.EstateInquiry.UnitId == unitId));
                if (state != "-1")
                {
                    query = query.Where(x => (x.WorkflowStatesId == state || x.WorkflowStates.State == state));
                }
                else
                {
                    var archiveState = EstateConstant.DealSummaryStates.Archived;
                    var notSendedState = EstateConstant.DealSummaryStates.NotSended;
                    query = query.Where(x => x.WorkflowStatesId != archiveState && x.WorkflowStatesId != notSendedState);
                }
                query = query.Where(x => (string.IsNullOrWhiteSpace(sectionId) || x.EstateInquiry.EstateSectionId == sectionId));
                query = query.Where(x => (string.IsNullOrWhiteSpace(subSectionId) || x.EstateInquiry.EstateSubsectionId == subSectionId));
                query = query.Where(x => (string.IsNullOrWhiteSpace(basic) || x.EstateInquiry.Basic == basic));
                query = query.Where(x => (string.IsNullOrWhiteSpace(secondary) || x.EstateInquiry.Secondary == secondary));
                query = query.Where(x => (string.IsNullOrWhiteSpace(docPrintNo) || x.EstateInquiry.DocPrintNo == docPrintNo));
                query = query.Where(x => (string.IsNullOrWhiteSpace(personName) || x.DealSummaryPeople.Any(p => p.Name.Contains(personName.NormalizeTextChars(true)))));
                query = query.Where(x => (string.IsNullOrWhiteSpace(estateElectronicNoteNo) || x.EstateInquiry.ElectronicEstateNoteNo == estateElectronicNoteNo));
                query = query.Where(x => (string.IsNullOrWhiteSpace(personFamily) || x.DealSummaryPeople.Any(p => p.Family.Contains(personFamily.NormalizeTextChars(true)))));
                query = query.Where(x => (string.IsNullOrWhiteSpace(personNationalityCode) || x.DealSummaryPeople.Any(p => p.NationalityCode == personNationalityCode)));
                query = query.Where(x => (string.IsNullOrWhiteSpace(notaryDocumentNo) || (x.NewNotaryDocument.NationalNo == notaryDocumentNo || x.NewNotaryDocument.RequestNo == notaryDocumentNo)));
            }
            foreach (SearchData filter in gridSearchInput)
            {
                if (string.IsNullOrWhiteSpace(filter.Value)) continue;
                var capitalCaseFilter = $"{filter.Filter[..1].ToUpper()}{filter.Filter[1..]}";
                switch (capitalCaseFilter)
                {
                    case nameof(DealSummaryGridItem.DealSummaryNumber):
                        query = query.Where(x => x.DealNo.Contains(filter.Value));
                        break;
                    case nameof(DealSummaryGridItem.DealSummaryDate):
                        query = query.Where(x => x.DealDate.Contains(filter.Value));
                        break;
                    case nameof(DealSummaryGridItem.EstateBasic):
                        query = query.Where(x => x.EstateInquiry.Basic.Contains(filter.Value));
                        break;
                    case nameof(DealSummaryGridItem.EstateSecondary):
                        query = query.Where(x => x.EstateInquiry.Secondary.Contains(filter.Value));
                        break;
                    case nameof(DealSummaryGridItem.EstateSection):
                        query = query.Where(x => x.EstateInquiry.EstateSection.Title.Contains(filter.Value.NormalizeTextChars(true)));
                        break;
                    case nameof(DealSummaryGridItem.EstateSubSection):
                        query = query.Where(x => x.EstateInquiry.EstateSubsection.Title.Contains(filter.Value.NormalizeTextChars(true)));
                        break;
                    case nameof(DealSummaryGridItem.EstateDocPrintNo):
                        query = query.Where(x => x.EstateInquiry.DocPrintNo.Contains(filter.Value));
                        break;
                    case nameof(DealSummaryGridItem.StatusTitle):
                        query = query.Where(x => x.WorkflowStates.Title.Contains(filter.Value.NormalizeTextChars(true)));
                        break;
                }

            }

            if (!string.IsNullOrWhiteSpace(globalSearch))
            {

                query = query.Where(x => (x.WorkflowStates.Title.Contains(globalSearch.NormalizeTextChars(true)) ||
                                      x.DealDate.Contains(globalSearch) ||
                                      x.DealNo.Contains(globalSearch) ||
                                      x.EstateInquiry.Basic.Contains(globalSearch) ||
                                      x.EstateInquiry.Secondary.Contains(globalSearch) ||
                                      x.EstateInquiry.DocPrintNo.Contains(globalSearch) ||
                                      x.EstateInquiry.EstateSection.Title.Contains(globalSearch.NormalizeTextChars(true)) ||
                                      x.EstateInquiry.EstateSubsection.Title.Contains(globalSearch.NormalizeTextChars(true))
                                      ));

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
                Select(y => new DealSummaryGridItem()
                {
                    DealSummaryId = y.Id.ToString(),
                    DealSummaryDate = y.DealDate,
                    DealSummaryNumber = y.DealNo,
                    EstateBasic = y.EstateInquiry.Basic,
                    EstateDocPrintNo = y.EstateInquiry.DocPrintNo,
                    Status = y.WorkflowStates.State,
                    EstateSecondary = y.EstateInquiry.Secondary,
                    EstateSection = y.EstateInquiry.EstateSection.Title,
                    EstateSubSection = y.EstateInquiry.EstateSubsection.Title,
                    DealSummaryUnitName = y.EstateInquiry.UnitId,
                    StatusTitle = y.WorkflowStates.Title,
                    IsRestricted = y.DealSummaryTransferType.Isrestricted.ToBoolean(),
                    TransferType = y.DealSummaryTransferType.Title
                })

                .ToListAsync(cancellationToken);
            }
            if (result.TotalCount > 0)
            {
                if (isOrderBy)
                {

                    result.GridItems = await query
                        .Select(y => new DealSummaryGridItem()
                        {
                            DealSummaryId = y.Id.ToString(),
                            DealSummaryDate = y.DealDate,
                            DealSummaryNumber = y.DealNo,
                            EstateBasic = y.EstateInquiry.Basic,
                            EstateDocPrintNo = y.EstateInquiry.DocPrintNo,
                            Status = y.WorkflowStates.State,
                            EstateSecondary = y.EstateInquiry.Secondary,
                            EstateSection = y.EstateInquiry.EstateSection.Title,
                            EstateSubSection = y.EstateInquiry.EstateSubsection.Title,
                            DealSummaryUnitName = y.EstateInquiry.UnitId,
                            StatusTitle = y.WorkflowStates.Title,
                            IsRestricted = y.DealSummaryTransferType.Isrestricted.ToBoolean(),
                            TransferType = y.DealSummaryTransferType.Title
                        })
                        .OrderBy($"{gridSortInput.Sort} {gridSortInput.SortType} ")
                        .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                            .ToListAsync(cancellationToken);

                }
                else
                {
                    result.GridItems = await query
                        .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                        .Select(y => new DealSummaryGridItem()
                        {
                            DealSummaryId = y.Id.ToString(),
                            DealSummaryDate = y.DealDate,
                            DealSummaryNumber = y.DealNo,
                            EstateBasic = y.EstateInquiry.Basic,
                            EstateDocPrintNo = y.EstateInquiry.DocPrintNo,
                            Status = y.WorkflowStates.State,
                            EstateSecondary = y.EstateInquiry.Secondary,
                            EstateSection = y.EstateInquiry.EstateSection.Title,
                            EstateSubSection = y.EstateInquiry.EstateSubsection.Title,
                            DealSummaryUnitName = y.EstateInquiry.UnitId,
                            StatusTitle = y.WorkflowStates.Title,
                            IsRestricted = y.DealSummaryTransferType.Isrestricted.ToBoolean(),
                            TransferType = y.DealSummaryTransferType.Title
                        })
                        .ToListAsync(cancellationToken);
                }
            }
            foreach(var item in result.GridItems)
            {
                item.EstateSubSection = item.EstateSubSection.NormalizeTextChars(false);
                item.EstateSection = item.EstateSection.NormalizeTextChars(false);
                item.TransferType = item.TransferType.NormalizeTextChars(false);
                item.StatusTitle = item.StatusTitle.NormalizeTextChars(false);
            }
            if (result.SelectedItems != null && result.SelectedItems.Count > 0)
            {
                foreach (var item in result.SelectedItems)
                {
                    item.EstateSubSection = item.EstateSubSection.NormalizeTextChars(false);
                    item.EstateSection = item.EstateSection.NormalizeTextChars(false);
                    item.TransferType = item.TransferType.NormalizeTextChars(false);
                    item.StatusTitle = item.StatusTitle.NormalizeTextChars(false);
                }
            }
            return result;
        }

        public async Task<DealSummary> GetDealSummaryById(string dealSummaryId, CancellationToken cancellationToken)
        {
            return await this.TableNoTracking
                .Include(d => d.WorkflowStates)
                .Include(d => d.EstateOwnershipType)
                .Include(d => d.EstateTransitionType)
                .Include(d => d.UnrestrictionType)
                .Include(d => d.DealSummaryTransferType)
                .Include(d => d.EstateInquiry)
                .ThenInclude(e => e.EstateInquiryPeople)
                .Include(d => d.EstateInquiry.EstateSection)
                .Include(d => d.EstateInquiry.EstateSubsection)
                .Include(d => d.EstateInquiry.EstateSeridaftar)
                .Include(d => d.DealSummaryPeople)
                .ThenInclude(p => p.RelationType)
                .Include(d=>d.DealSummaryPeople)
                .Include(d => d.EstateInquiry)
                .ThenInclude(e=>e.EstateInquiryType)
                .Where(d => d.Id == Guid.Parse(dealSummaryId)).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<DealSummary> GetDealSummaryByLegacyId(string legacyId, CancellationToken cancellationToken)
        {
            return await this.TableNoTracking
                .Include(d => d.WorkflowStates)
                .Include(d => d.EstateOwnershipType)
                .Include(d => d.EstateTransitionType)
                .Include(d => d.UnrestrictionType)
                .Include(d => d.DealSummaryTransferType)
                .Include(d => d.EstateInquiry)
                .ThenInclude(e => e.EstateInquiryPeople)
                .Include(d => d.EstateInquiry.EstateSection)
                .Include(d => d.EstateInquiry.EstateSubsection)
                .Include(d => d.EstateInquiry.EstateSeridaftar)
                .Include(d => d.DealSummaryPeople)
                .ThenInclude(p => p.RelationType)
                .Include(d => d.DealSummaryPeople)
                .Include(d => d.EstateInquiry)
                .ThenInclude(e => e.EstateInquiryType)
                .Where(d => d.LegacyId == legacyId).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<RestrictionDealSummaryListViewModel> GetRestrictedDealSummaryList(int pageIndex, int pageSize, ICollection<SearchData> gridSearchInput, string globalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> fieldsNotInFilterSearch, string scriptoriumId, bool isOrderBy, string[] statusId, List<string> estateInquiryIdList,List<string> dealSummaryIdList,string documentClassifyNo,string documentSignDate, CancellationToken cancellationToken)
        {
            RestrictionDealSummaryListViewModel result = new();

            RestrictionDealSummaryListItem lookupFilterItem = new();

            var query = TableNoTracking
                .Where(x => x.ScriptoriumId == scriptoriumId && x.DealSummaryTransferType.Isrestricted == EstateConstant.BooleanConstant.True && x.UnrestrictionTypeId == null);

            if (statusId != null && statusId.Length > 0)
            {
                query = query.Where(x => statusId.Contains(x.WorkflowStatesId));
            }
            if (dealSummaryIdList != null && dealSummaryIdList.Count > 0)
            {
                var lst = dealSummaryIdList.Select(x => x.ToGuid());
                query = query.Where(x => lst.Contains(x.Id));
            }
            else if (estateInquiryIdList != null && estateInquiryIdList.Count > 0)
            {
                var lst = estateInquiryIdList.Select(x => x.ToGuid());
                query = query.Where(x => lst.Contains(x.EstateInquiryId));
            }            
            else if(!string.IsNullOrWhiteSpace( documentClassifyNo) && !string.IsNullOrWhiteSpace (documentSignDate))
            {
                query = query.Where(x => x.DealNo == documentClassifyNo && x.TransactionDate == documentSignDate);
            }
            foreach (SearchData filter in gridSearchInput)
            {
                if (string.IsNullOrWhiteSpace(filter.Value)) continue;
                var capitalCaseFilter = $"{filter.Filter[..1].ToUpper()}{filter.Filter[1..]}";
                switch (capitalCaseFilter)
                {
                    case nameof(RestrictionDealSummaryListItem.DealSummaryNumber):
                        query = query.Where(x => x.DealNo.Contains(filter.Value));
                        break;
                    case nameof(RestrictionDealSummaryListItem.DealSummaryDate):
                        query = query.Where(x => x.DealDate.Contains(filter.Value));
                        break;
                    case nameof(RestrictionDealSummaryListItem.EstateBasic):
                        query = query.Where(x => x.EstateInquiry.Basic.Contains(filter.Value));
                        break;
                    case nameof(RestrictionDealSummaryListItem.EstateSecondary):
                        query = query.Where(x => x.EstateInquiry.Secondary.Contains(filter.Value));
                        break;
                    case nameof(RestrictionDealSummaryListItem.EstateSection):
                        query = query.Where(x => x.EstateInquiry.EstateSection.SsaaCode.Contains(filter.Value));
                        break;
                    case nameof(RestrictionDealSummaryListItem.EstateSubSection):
                        query = query.Where(x => x.EstateInquiry.EstateSubsection.SsaaCode.Contains(filter.Value));
                        break;
                    case nameof(RestrictionDealSummaryListItem.DealSummaryMainDate):
                        query = query.Where(x => x.TransactionDate.Contains(filter.Value));
                        break;
                    case nameof(RestrictionDealSummaryListItem.EstateElectronicNoteNo):
                        query = query.Where(x => x.EstateInquiry.ElectronicEstateNoteNo.Contains(filter.Value));
                        break;
                    case nameof(RestrictionDealSummaryListItem.EstateNoteNo):
                        query = query.Where(x => x.EstateInquiry.EstateNoteNo.Contains(filter.Value));
                        break;
                    case nameof(RestrictionDealSummaryListItem.EstatePageNo):
                        query = query.Where(x => x.EstateInquiry.PageNo.Contains(filter.Value));
                        break;
                    case nameof(RestrictionDealSummaryListItem.EstateSeridaftar):
                        query = query.Where(x => x.EstateInquiry.EstateSeridaftar.SsaaCode.Contains(filter.Value));
                        break;
                    case nameof(RestrictionDealSummaryListItem.TransferType):
                        query = query.Where(x => x.DealSummaryTransferType.Title.Contains(filter.Value.NormalizeTextChars(true)));
                        break;
                    case nameof(RestrictionDealSummaryListItem.TransitionCaseTitle):
                        query = query.Where(x => x.EstateTransitionType.Title.Contains(filter.Value.NormalizeTextChars(true)));
                        break;
                    case nameof(RestrictionDealSummaryListItem.FirstReceiveDate):
                        query = query.Where(x => x.FirstReceiveDate.Contains(filter.Value));
                        break;
                    case nameof(RestrictionDealSummaryListItem.LastEditReceiveDate):
                        query = query.Where(x => x.LasteditReceiveDate.Contains(filter.Value));
                        break;
                }

            }

            if (!string.IsNullOrWhiteSpace(globalSearch))
            {

                query = query.Where(x => (x.DealSummaryTransferType.Title.Contains(globalSearch.NormalizeTextChars(true)) ||
                                      x.DealDate.Contains(globalSearch) ||
                                      x.DealNo.Contains(globalSearch) ||
                                      x.TransactionDate.Contains(globalSearch) ||
                                      x.FirstReceiveDate.Contains(globalSearch) ||
                                      x.LasteditReceiveDate.Contains(globalSearch) ||
                                      (x.EstateTransitionTypeId!=null && x.EstateTransitionType.Title.Contains(globalSearch.NormalizeTextChars(true))) ||
                                      x.EstateInquiry.Basic.Contains(globalSearch) ||
                                      x.EstateInquiry.Secondary.Contains(globalSearch) ||
                                      x.EstateInquiry.EstateNoteNo.Contains(globalSearch) ||
                                      x.EstateInquiry.ElectronicEstateNoteNo.Contains(globalSearch) ||
                                      x.EstateInquiry.PageNo.Contains(globalSearch) ||
                                      (x.EstateInquiry.EstateSeridaftarId != null && x.EstateInquiry.EstateSeridaftar.SsaaCode.Contains(globalSearch)) ||
                                      x.EstateInquiry.EstateSection.SsaaCode.Contains(globalSearch) ||
                                      x.EstateInquiry.EstateSubsection.SsaaCode.Contains(globalSearch)
                                      ));

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
                Select(y => new RestrictionDealSummaryListItem()
                {
                    DealSummaryId = y.Id.ToString(),
                    DealSummaryDate = y.DealDate,
                    DealSummaryNumber = y.DealNo,
                    DealSummaryMainDate = y.TransactionDate,
                    EstateBasic = y.EstateInquiry.Basic,
                    EstateNoteNo = y.EstateInquiry.ElectronicEstateNoteNo,
                    EstateElectronicNoteNo = y.EstateInquiry.ElectronicEstateNoteNo,
                    EstatePageNo = y.EstateInquiry.PageNo,
                    TransitionCaseTitle = y.EstateTransitionType != null ? y.EstateTransitionType.Title : null,
                    EstateSecondary = y.EstateInquiry.Secondary,
                    EstateSection = y.EstateInquiry.EstateSection.SsaaCode,
                    EstateSubSection = y.EstateInquiry.EstateSubsection.SsaaCode,
                    EstateSeridaftar = y.EstateInquiry.EstateSeridaftar != null ? y.EstateInquiry.EstateSeridaftar.SsaaCode : null,
                    TransferType = y.DealSummaryTransferType.Title,
                    FirstReceiveDate = !string.IsNullOrWhiteSpace(y.FirstReceiveDate) ? y.FirstReceiveDate + "-" + y.FirstReceiveTime : null,
                    LastEditReceiveDate = !string.IsNullOrWhiteSpace(y.LasteditReceiveDate) ? y.LasteditReceiveDate + "-" + y.LasteditReceiveTime : null,
                    RelatedServer = "BO"
                })
                .ToListAsync(cancellationToken);
            }
            if (result.TotalCount > 0)
            {
                if (isOrderBy)
                {
                    if (gridSortInput.Sort.Equals(nameof(RestrictionDealSummaryListItem.FirstReceiveDate), StringComparison.OrdinalIgnoreCase))
                    {
                        result.GridItems = await query
                            .Select(y => new RestrictionDealSummaryListItem()
                            {
                                DealSummaryId = y.Id.ToString(),
                                DealSummaryDate = y.DealDate,
                                DealSummaryNumber = y.DealNo,
                                DealSummaryMainDate = y.TransactionDate,
                                EstateBasic = y.EstateInquiry.Basic,
                                EstateNoteNo = y.EstateInquiry.ElectronicEstateNoteNo,
                                EstateElectronicNoteNo = y.EstateInquiry.ElectronicEstateNoteNo,
                                EstatePageNo = y.EstateInquiry.PageNo,
                                TransitionCaseTitle = y.EstateTransitionType != null ? y.EstateTransitionType.Title : null,
                                EstateSecondary = y.EstateInquiry.Secondary,
                                EstateSection = y.EstateInquiry.EstateSection.SsaaCode,
                                EstateSubSection = y.EstateInquiry.EstateSubsection.SsaaCode,
                                EstateSeridaftar = y.EstateInquiry.EstateSeridaftar != null ? y.EstateInquiry.EstateSeridaftar.SsaaCode : null,
                                TransferType = y.DealSummaryTransferType.Title,
                                FirstReceiveDate = !string.IsNullOrWhiteSpace(y.FirstReceiveDate) ? y.FirstReceiveDate + "-" + y.FirstReceiveTime : null,
                                LastEditReceiveDate = !string.IsNullOrWhiteSpace(y.LasteditReceiveDate) ? y.LasteditReceiveDate + "-" + y.LasteditReceiveTime : null,
                                RelatedServer = "BO"
                            })
                            .OrderBy($"{gridSortInput.Sort} {gridSortInput.SortType},FirstReceiveTime {gridSortInput.SortType}")
                            .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                                .ToListAsync(cancellationToken);
                    }
                    else if (gridSortInput.Sort.Equals(nameof(RestrictionDealSummaryListItem.LastEditReceiveDate), StringComparison.OrdinalIgnoreCase))
                    {
                        result.GridItems = await query
                            .Select(y => new RestrictionDealSummaryListItem()
                            {
                                DealSummaryId = y.Id.ToString(),
                                DealSummaryDate = y.DealDate,
                                DealSummaryNumber = y.DealNo,
                                DealSummaryMainDate = y.TransactionDate,
                                EstateBasic = y.EstateInquiry.Basic,
                                EstateNoteNo = y.EstateInquiry.ElectronicEstateNoteNo,
                                EstateElectronicNoteNo = y.EstateInquiry.ElectronicEstateNoteNo,
                                EstatePageNo = y.EstateInquiry.PageNo,
                                TransitionCaseTitle = y.EstateTransitionType != null ? y.EstateTransitionType.Title : null,
                                EstateSecondary = y.EstateInquiry.Secondary,
                                EstateSection = y.EstateInquiry.EstateSection.SsaaCode,
                                EstateSubSection = y.EstateInquiry.EstateSubsection.SsaaCode,
                                EstateSeridaftar = y.EstateInquiry.EstateSeridaftar != null ? y.EstateInquiry.EstateSeridaftar.SsaaCode : null,
                                TransferType = y.DealSummaryTransferType.Title,
                                FirstReceiveDate = !string.IsNullOrWhiteSpace(y.FirstReceiveDate) ? y.FirstReceiveDate + "-" + y.FirstReceiveTime : null,
                                LastEditReceiveDate = !string.IsNullOrWhiteSpace(y.LasteditReceiveDate) ? y.LasteditReceiveDate + "-" + y.LasteditReceiveTime : null,
                                RelatedServer = "BO"
                            })
                            .OrderBy($"{gridSortInput.Sort} {gridSortInput.SortType},LasteditReceiveTime {gridSortInput.SortType}")
                            .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                                .ToListAsync(cancellationToken);
                    }
                    else
                        result.GridItems = await query
                            .Select(y => new RestrictionDealSummaryListItem()
                            {
                                DealSummaryId = y.Id.ToString(),
                                DealSummaryDate = y.DealDate,
                                DealSummaryNumber = y.DealNo,
                                DealSummaryMainDate = y.TransactionDate,
                                EstateBasic = y.EstateInquiry.Basic,
                                EstateNoteNo = y.EstateInquiry.ElectronicEstateNoteNo,
                                EstateElectronicNoteNo = y.EstateInquiry.ElectronicEstateNoteNo,
                                EstatePageNo = y.EstateInquiry.PageNo,
                                TransitionCaseTitle = y.EstateTransitionType != null ? y.EstateTransitionType.Title : null,
                                EstateSecondary = y.EstateInquiry.Secondary,
                                EstateSection = y.EstateInquiry.EstateSection.SsaaCode,
                                EstateSubSection = y.EstateInquiry.EstateSubsection.SsaaCode,
                                EstateSeridaftar = y.EstateInquiry.EstateSeridaftar != null ? y.EstateInquiry.EstateSeridaftar.SsaaCode : null,
                                TransferType = y.DealSummaryTransferType.Title,
                                FirstReceiveDate = !string.IsNullOrWhiteSpace(y.FirstReceiveDate) ? y.FirstReceiveDate + "-" + y.FirstReceiveTime : null,
                                LastEditReceiveDate = !string.IsNullOrWhiteSpace(y.LasteditReceiveDate) ? y.LasteditReceiveDate + "-" + y.LasteditReceiveTime : null,
                                RelatedServer = "BO"
                            })
                            .OrderBy($"{gridSortInput.Sort} {gridSortInput.SortType} ")
                            .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                                .ToListAsync(cancellationToken);

                }
                else
                {
                    result.GridItems = await query
                        .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                        .Select(y => new RestrictionDealSummaryListItem()
                        {
                            DealSummaryId = y.Id.ToString(),
                            DealSummaryDate = y.DealDate,
                            DealSummaryNumber = y.DealNo,
                            DealSummaryMainDate = y.TransactionDate,
                            EstateBasic = y.EstateInquiry.Basic,
                            EstateNoteNo = y.EstateInquiry.ElectronicEstateNoteNo,
                            EstateElectronicNoteNo = y.EstateInquiry.ElectronicEstateNoteNo,
                            EstatePageNo = y.EstateInquiry.PageNo,
                            TransitionCaseTitle = y.EstateTransitionType != null ? y.EstateTransitionType.Title : null,
                            EstateSecondary = y.EstateInquiry.Secondary,
                            EstateSection = y.EstateInquiry.EstateSection.SsaaCode,
                            EstateSubSection = y.EstateInquiry.EstateSubsection.SsaaCode,
                            EstateSeridaftar = y.EstateInquiry.EstateSeridaftar != null ? y.EstateInquiry.EstateSeridaftar.SsaaCode : null,
                            TransferType = y.DealSummaryTransferType.Title,
                            FirstReceiveDate = !string.IsNullOrWhiteSpace(y.FirstReceiveDate) ? y.FirstReceiveDate + "-" + y.FirstReceiveTime : null,
                            LastEditReceiveDate = !string.IsNullOrWhiteSpace(y.LasteditReceiveDate) ? y.LasteditReceiveDate + "-" + y.LasteditReceiveTime : null,
                            RelatedServer = "BO"
                        })
                        .ToListAsync(cancellationToken);
                }
            }
            foreach (var item in result.GridItems)
            {
                
                item.TransitionCaseTitle = item.TransitionCaseTitle.NormalizeTextChars(false);
                item.TransferType = item.TransferType.NormalizeTextChars(false);
                
            }
            if (result.SelectedItems != null && result.SelectedItems.Count > 0)
            {
                foreach (var item in result.SelectedItems)
                {
                    item.TransitionCaseTitle = item.TransitionCaseTitle.NormalizeTextChars(false);
                    item.TransferType = item.TransferType.NormalizeTextChars(false);                    
                }
            }
            return result;
        }

        public async Task<string> GetMaxInquiryNo(string scriptorumId, string no, CancellationToken cancellationToken)
        {

            return await TableNoTracking.Where(x => x.ScriptoriumId == scriptorumId && x.No.StartsWith(no)).Select(x => x.No).MaxAsync(cancellationToken);
        }
    }
}
