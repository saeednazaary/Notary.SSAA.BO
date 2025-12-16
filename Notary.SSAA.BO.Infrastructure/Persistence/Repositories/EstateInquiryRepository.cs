using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Infrastructure.Contexts;
using System.Linq.Dynamic.Core;
using StackExchange.Profiling.Internal;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.Base;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Estate;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.Utilities.Estate;

namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories
{
    public class EstateInquiryRepository : Repository<EstateInquiry>, IEstateInquiryRepository
    {
        public EstateInquiryRepository(ApplicationContext context) : base(context)
        {
        }
        public async Task<string> GetMaxInquiryNo(string scriptorumId, string no, CancellationToken cancellationToken)
        {

            return await TableNoTracking.Where(x => x.ScriptoriumId == scriptorumId && x.No.StartsWith(no)).Select(x => x.No).MaxAsync(cancellationToken);
        }
        public async Task<string> GetMaxInquiryNo2(string scriptorumId, string no, CancellationToken cancellationToken)
        {
            return await TableNoTracking.Where(x => x.ScriptoriumId == scriptorumId && !string.IsNullOrWhiteSpace(x.No2) && x.No2.StartsWith(no)).Select(x => x.No2).MaxAsync(cancellationToken);
        }
        private static T GetValue<T>(Dictionary<string, object> extraParam, string key)
        {
            if (extraParam.TryGetValue(key, out object value))
            {
                return (T)value;
            }
            return default;
        }
        public async Task<EstateInquiryGrid> GetEstateInquiryGridItems(int pageIndex, int pageSize, ICollection<SearchData> gridSearchInput, string globalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> fieldsNotInFilterSearch, string scriptoriumId, bool isOrderBy, string[] statusId, Dictionary<string, object> extraParam, CancellationToken cancellationToken)
        {
            EstateInquiryGrid result = new();

            EstateInquiryGridItem lookupFilterItem = new();

            var query = TableNoTracking
                .Where(x => x.ScriptoriumId == scriptoriumId);

            if (statusId != null && statusId.Length > 0)
            {
                query = query.Where(x => statusId.Contains(x.WorkflowStatesId));
            }
            if (extraParam != null)
            {

                var noFrom = GetValue<string>(extraParam, "InqNoFrom");
                var noTo = GetValue<string>(extraParam, "InqNoTo");
                var dateFrom = GetValue<string>(extraParam, "InqDateFrom");
                var dateTo = GetValue<string>(extraParam, "InqDateTo");
                var systemNo = GetValue<string>(extraParam, "InqSystemNo");
                var unitId = GetValue<string>(extraParam, "InqUnitId");
                var state = GetValue<string>(extraParam, "InqStatus");
                var sectionId = GetValue<string>(extraParam, "InqSectionId");
                var subSectionId = GetValue<string>(extraParam, "InqSubSectionId");
                var basic = GetValue<string>(extraParam, "InqBasic");
                var secondary = GetValue<string>(extraParam, "InqSecondary");
                var docPrintNo = GetValue<string>(extraParam, "InqPrintDocNo");
                var personName = GetValue<string>(extraParam, "PersonName");
                var estateElectronicNoteNo = GetValue<string>(extraParam, "InqEstateElectronicNoteNo");
                var personFamily = GetValue<string>(extraParam, "PersonFamily");
                var personNationalityCode = GetValue<string>(extraParam, "PersonNationalityCode");

                query = query.Where(x => (string.IsNullOrWhiteSpace(noFrom) || x.InquiryNo.CompareTo(noFrom) >= 0));
                query = query.Where(x => (string.IsNullOrWhiteSpace(noTo) || x.InquiryNo.CompareTo(noTo) <= 0));
                query = query.Where(x => (string.IsNullOrWhiteSpace(dateFrom) || x.InquiryDate.CompareTo(dateFrom) >= 0));
                query = query.Where(x => (string.IsNullOrWhiteSpace(dateTo) || x.InquiryDate.CompareTo(dateTo) <= 0));
                query = query.Where(x => (string.IsNullOrWhiteSpace(systemNo) || x.No.Contains(systemNo)));
                query = query.Where(x => (string.IsNullOrWhiteSpace(unitId) || x.UnitId == unitId));
                if (state != "-1")
                {
                    query = query.Where(x => (x.WorkflowStatesId == state || x.WorkflowStates.State == state));
                }
                else
                {
                    var archiveState = EstateConstant.EstateInquiryStates.Archived;
                    var notSendedState = EstateConstant.EstateInquiryStates.NotSended;
                    var invisibleState = EstateConstant.EstateInquiryStates.Invisible;
                    query = query.Where(x => x.WorkflowStatesId != archiveState && x.WorkflowStatesId != notSendedState && x.WorkflowStatesId != invisibleState);
                }
                query = query.Where(x => (string.IsNullOrWhiteSpace(sectionId) || x.EstateSectionId == sectionId));
                query = query.Where(x => (string.IsNullOrWhiteSpace(subSectionId) || x.EstateSubsectionId == subSectionId));
                query = query.Where(x => (string.IsNullOrWhiteSpace(basic) || x.Basic == basic));
                query = query.Where(x => (string.IsNullOrWhiteSpace(secondary) || x.Secondary == secondary));
                query = query.Where(x => (string.IsNullOrWhiteSpace(docPrintNo) || x.DocPrintNo == docPrintNo));
                query = query.Where(x => (string.IsNullOrWhiteSpace(personName) || x.EstateInquiryPeople.Any(p => p.Name.Contains(personName.NormalizeTextChars(true)))));
                query = query.Where(x => (string.IsNullOrWhiteSpace(estateElectronicNoteNo) || x.ElectronicEstateNoteNo == estateElectronicNoteNo));
                query = query.Where(x => (string.IsNullOrWhiteSpace(personFamily) || x.EstateInquiryPeople.Any(p => p.Family.Contains(personFamily.NormalizeTextChars(true)))));
                query = query.Where(x => (string.IsNullOrWhiteSpace(personNationalityCode) || x.EstateInquiryPeople.Any(p => p.NationalityCode == personNationalityCode)));
            }
            foreach (SearchData filter in gridSearchInput)
            {
                if (string.IsNullOrWhiteSpace(filter.Value)) continue;
                var capitalCaseFilter = $"{filter.Filter[..1].ToUpper()}{filter.Filter[1..]}";
                switch (capitalCaseFilter)
                {
                    case nameof(EstateInquiryGridItem.InquiryNnumber):
                        query = query.Where(x => x.InquiryNo.Contains(filter.Value));
                        break;
                    case nameof(EstateInquiryGridItem.InquiryDate):
                        query = query.Where(x => x.InquiryDate.Contains(filter.Value));
                        break;
                    case nameof(EstateInquiryGridItem.EstateBasic):
                        query = query.Where(x => x.Basic.Contains(filter.Value));
                        break;
                    case nameof(EstateInquiryGridItem.EstateSecondary):
                        query = query.Where(x => x.Secondary.Contains(filter.Value));
                        break;
                    case nameof(EstateInquiryGridItem.OwnerName):
                        query = query.Where(x => x.EstateInquiryPeople.Any(p => (p.Name.Contains(filter.Value.NormalizeTextChars(true)) || p.Family.Contains(filter.Value.NormalizeTextChars(true)))));
                        break;
                    case nameof(EstateInquiryGridItem.EstateDocPrintNo):
                        query = query.Where(x => x.DocPrintNo.Contains(filter.Value));
                        break;
                    case nameof(EstateInquiryGridItem.StatusTitle):
                        query = query.Where(x => x.WorkflowStates.Title.Contains(filter.Value.NormalizeTextChars(true)));
                        break;
                }

            }

            if (!string.IsNullOrWhiteSpace(globalSearch))
            {

                query = query.Where(x => (x.WorkflowStates.Title.Contains(globalSearch.NormalizeTextChars(true)) ||
                                      x.InquiryDate.Contains(globalSearch) ||
                                      x.InquiryNo.Contains(globalSearch) ||
                                      x.Basic.Contains(globalSearch) ||
                                      x.Secondary.Contains(globalSearch) ||
                                      x.DocPrintNo.Contains(globalSearch) ||
                                      x.EstateInquiryPeople.Any(p => p.Name.Contains(globalSearch.NormalizeTextChars(true))) ||
                                      x.EstateInquiryPeople.Any(p => p.Family.Contains(globalSearch.NormalizeTextChars(true)))
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
                Select(y => new EstateInquiryGridItem()
                {
                    InquiryId = y.Id.ToString(),
                    InquiryDate = y.InquiryDate,
                    InquiryNnumber = y.InquiryNo,
                    EstateBasic = y.Basic,
                    EstateDocPrintNo = y.DocPrintNo,
                    Status = y.WorkflowStates.State,
                    EstateSecondary = y.Secondary,
                    OwnerName = y.EstateInquiryPeople.Select(p => p.Name + " " + p.Family).First(),
                    InquiryUnitName = y.UnitId,
                    StatusTitle = y.WorkflowStates.Title,
                    InquiryTitle = "استعلام شماره " + y.InquiryNo + " به تاریخ " + y.InquiryDate,
                    CreateTime = y.CreateTime,
                    InquiryResponseDate = y.TrtsReadDate + "-" + y.TrtsReadTime,
                    ScriptoriumId = y.ScriptoriumId
                })
                .ToListAsync(cancellationToken);
            }
            if (result.TotalCount > 0)
            {
                if (isOrderBy)
                {
                    if (!gridSortInput.Sort.Equals("inquirydate", StringComparison.OrdinalIgnoreCase))
                    {
                        result.GridItems = await query
                            .Select(y => new EstateInquiryGridItem()
                            {
                                InquiryId = y.Id.ToString(),
                                InquiryDate = y.InquiryDate,
                                InquiryNnumber = y.InquiryNo,
                                EstateBasic = y.Basic,
                                EstateDocPrintNo = y.DocPrintNo,
                                Status = y.WorkflowStates.State,
                                EstateSecondary = y.Secondary,
                                OwnerName = y.EstateInquiryPeople.Select(p => p.Name + " " + p.Family).First(),
                                InquiryUnitName = y.UnitId,
                                StatusTitle = y.WorkflowStates.Title,
                                InquiryTitle = "استعلام شماره " + y.InquiryNo + " به تاریخ " + y.InquiryDate,
                                CreateTime = y.CreateTime,
                                InquiryResponseDate = y.TrtsReadDate + "-" + y.TrtsReadTime,
                                ScriptoriumId = y.ScriptoriumId
                            })
                            .OrderBy($"{gridSortInput.Sort} {gridSortInput.SortType} ")
                            .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                                .ToListAsync(cancellationToken);
                    }
                    else
                    {
                        result.GridItems = await query
                           .Select(y => new EstateInquiryGridItem()
                           {
                               InquiryId = y.Id.ToString(),
                               InquiryDate = y.InquiryDate,
                               InquiryNnumber = y.InquiryNo,
                               EstateBasic = y.Basic,
                               EstateDocPrintNo = y.DocPrintNo,
                               Status = y.WorkflowStates.State,
                               EstateSecondary = y.Secondary,
                               OwnerName = y.EstateInquiryPeople.Select(p => p.Name + " " + p.Family).First(),
                               InquiryUnitName = y.UnitId,
                               StatusTitle = y.WorkflowStates.Title,
                               InquiryTitle = "استعلام شماره " + y.InquiryNo + " به تاریخ " + y.InquiryDate,
                               CreateTime = y.CreateTime,
                               InquiryResponseDate = y.TrtsReadDate + "-" + y.TrtsReadTime,
                               ScriptoriumId = y.ScriptoriumId
                           })
                           .OrderBy($"{gridSortInput.Sort} {gridSortInput.SortType} ,CreateTime {gridSortInput.SortType}")
                           .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                               .ToListAsync(cancellationToken);
                    }
                }
                else
                {
                    result.GridItems = await query
                        .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                        .Select(y => new EstateInquiryGridItem()
                        {
                            InquiryId = y.Id.ToString(),
                            InquiryDate = y.InquiryDate,
                            InquiryNnumber = y.InquiryNo,
                            EstateBasic = y.Basic,
                            EstateDocPrintNo = y.DocPrintNo,
                            Status = y.WorkflowStates.State,
                            EstateSecondary = y.Secondary,
                            OwnerName = y.EstateInquiryPeople.Select(p => p.Name + " " + p.Family).First(),
                            InquiryUnitName = y.UnitId,
                            StatusTitle = y.WorkflowStates.Title,
                            InquiryTitle = "استعلام شماره " + y.InquiryNo + " به تاریخ " + y.InquiryDate,
                            CreateTime = y.CreateTime,
                            InquiryResponseDate = y.TrtsReadDate + "-" + y.TrtsReadTime,
                            ScriptoriumId = y.ScriptoriumId
                        })
                        .ToListAsync(cancellationToken);
                }
            }

            foreach (var item in result.GridItems)
            {
                if (!string.IsNullOrWhiteSpace(item.CreateTime))
                    item.InquiryDate = item.InquiryDate + "-" + item.CreateTime;
            }
            foreach (var item in result.SelectedItems)
            {
                if (!string.IsNullOrWhiteSpace(item.CreateTime))
                    item.InquiryDate = item.InquiryDate + "-" + item.CreateTime;
            }
            Helper.NormalizeStringValuesDeeply(result, false);
            return result;
        }
        public async Task<bool> IsExistsInquiry(string inquiryDate, string inquiryNo, string inquiryId, string scriptoriumId, CancellationToken cancellationToken)
        {
            var guid = !string.IsNullOrWhiteSpace(inquiryId) ? inquiryId.ToGuid() : Guid.Empty;
            var inquiry = await TableNoTracking
                .Where(e => e.InquiryDate.StartsWith(inquiryDate.Substring(0, 4)) && e.InquiryNo == inquiryNo && e.ScriptoriumId == scriptoriumId && e.Id != guid)
                .FirstOrDefaultAsync(cancellationToken);
            if (inquiry != null)
                return true;
            else
                return false;
        }
        public async Task<List<EstateInquirySpecialFields>> GetSimilarInquiryList(string scriptoriumId, string unitId, string pageNumber, string noteBookNo, string seriDaftarId, string melliCode, string basic, string secondary, string inquiryId, bool isExecuteTransfer, string name, string family, string docPrintNo, CancellationToken cancellationToken)
        {
            string[] states = new string[] { EstateConstant.EstateInquiryStates.ConfirmResponse,
                                             EstateConstant.EstateInquiryStates.RejectResponse,
                                             EstateConstant.EstateInquiryStates.Archived };
            var guid = (!string.IsNullOrWhiteSpace(inquiryId)) ? Guid.Parse(inquiryId) : Guid.Empty;
            var queryResult = await TableNoTracking.
                Include(e => e.EstateInquiryPeople).
                Where(e => e.Id != guid &&
                e.ScriptoriumId == scriptoriumId &&
                e.UnitId == unitId &&
                e.Basic == basic &&
                e.DocPrintNo == docPrintNo &&
                ((e.Secondary == null && (secondary == null || secondary.Trim() == string.Empty)) || (e.Secondary != null && e.Secondary == secondary)) &&
                ((e.PageNo == null && (pageNumber == null || pageNumber.Trim() == string.Empty)) || (e.PageNo != null && e.PageNo == pageNumber)) &&
                ((e.EstateNoteNo == null && (noteBookNo == null || noteBookNo.Trim() == string.Empty)) || (e.EstateNoteNo != null && e.EstateNoteNo == noteBookNo)) &&
                ((e.EstateSeridaftarId == null && (seriDaftarId == null || seriDaftarId.Trim() == string.Empty)) || (e.EstateSeridaftarId != null && e.EstateSeridaftarId == seriDaftarId)) &&
                (!states.Contains(e.WorkflowStatesId) || (states.Contains(e.WorkflowStatesId) && e.ResponseResult == "True"))
                )
               .ToListAsync(cancellationToken);
            List<EstateInquiry> result = new();
            if (queryResult.Count > 0)
            {
                if (isExecuteTransfer)
                {
                    result = queryResult.Where(e => e.EstateInquiryPeople != null && (e.EstateInquiryPeople.First().Name == name.NormalizeTextChars(true).Trim() || e.EstateInquiryPeople.First().Name == name.NormalizeTextChars(false).Trim() || e.EstateInquiryPeople.First().Name == name)).ToList();
                    if (!string.IsNullOrWhiteSpace(family))
                    {
                        result = result.Where(e => e.EstateInquiryPeople != null && (e.EstateInquiryPeople.First().Family == family.NormalizeTextChars(true).Trim() || e.EstateInquiryPeople.First().Family == family.NormalizeTextChars(false).Trim() || e.EstateInquiryPeople.First().Family == family)).ToList();
                    }
                }
                else
                {
                    result = queryResult.Where(e => e.EstateInquiryPeople != null && e.EstateInquiryPeople.First().NationalityCode == melliCode).ToList();
                }
            }
            if (result.Count > 0)
                return result.Select(e => new EstateInquirySpecialFields()
                {
                    Id = e.Id,
                    FirstSendDate = e.FirstSendDate,
                    InquiryDate = e.InquiryDate,
                    InquiryNo = e.InquiryNo,
                    LastSendDate = e.LastSendDate,
                    ResponseDate = e.ResponseDate,
                    TrtsReadDate = e.TrtsReadDate,
                    State = e.WorkflowStatesId,
                    ResponseResult = e.ResponseResult,
                    UnitId = e.UnitId,
                    ScriptoriumId = e.ScriptoriumId
                }).ToList();
            return new List<EstateInquirySpecialFields>();
        }

       
    }
}
