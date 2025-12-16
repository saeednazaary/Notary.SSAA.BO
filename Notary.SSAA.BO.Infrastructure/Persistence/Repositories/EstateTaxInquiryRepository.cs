using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Infrastructure.Contexts;
using System.Linq.Dynamic.Core;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.Base;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.Utilities.Estate;

namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories
{
    public class EstateTaxInquiryRepository : Repository<EstateTaxInquiry>, IEstateTaxInquiryRepository
    {
        public EstateTaxInquiryRepository(ApplicationContext context) : base(context)
        {
        }
        public async Task<string> GetMaxInquiryNo(string scriptorumId,string no, CancellationToken cancellationToken)
        {
            
            return await TableNoTracking.Where(x => x.ScriptoriumId == scriptorumId && x.No.StartsWith(no)).Select(x => x.No).MaxAsync(cancellationToken);
        }
        public async Task<string> GetMaxInquiryNo2(string scriptorumId,string no, CancellationToken cancellationToken)
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
        public async Task<EstateTaxInquiryGrid> GetEstateTaxInquiryGridItems(int pageIndex, int pageSize, ICollection<SearchData> gridSearchInput, string globalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> fieldsNotInFilterSearch, string scriptoriumId, bool isOrderBy,  Dictionary<string, object> extraParam, CancellationToken cancellationToken)
        {
            EstateTaxInquiryGrid result = new();

            EstateTaxInquiryGridItem lookupFilterItem = new();

            var query = TableNoTracking
                .Where(x => x.ScriptoriumId == scriptoriumId && x.IsActive == EstateConstant.BooleanConstant.True);

            
            if (extraParam != null)
            {

                var noFrom = GetValue<string>(extraParam, "NoFrom");
                var noTo = GetValue<string>(extraParam, "NoTo");
                var dateFrom = GetValue<string>(extraParam, "CreateDateFrom");
                var dateTo = GetValue<string>(extraParam, "CreateDateTo");
                var sendDateFrom = GetValue<string>(extraParam, "SendDateFrom");
                var sendDateTo = GetValue<string>(extraParam, "SendDateTo");
                var responseDateFrom = GetValue<string>(extraParam, "ResponseDateFrom");
                var responseDateTo = GetValue<string>(extraParam, "ResponseDateTo");                
                var unitId = GetValue<string>(extraParam, "EstateUnitId");
                var state = GetValue<string>(extraParam, "Status");
                var sectionId = GetValue<string>(extraParam, "EstateSectionId");
                var subSectionId = GetValue<string>(extraParam, "EstateSubSectionId");
                var basic = GetValue<string>(extraParam, "EstateBasic");
                var secondary = GetValue<string>(extraParam, "EstateSecondary");                
                var personName = GetValue<string>(extraParam, "PersonName");                
                var personFamily = GetValue<string>(extraParam, "PersonFamily");
                var personNationalityCode = GetValue<string>(extraParam, "PersonNationalityCode");
                var states = GetValue<string[]>(extraParam, "States");

                query = query.Where(x => (string.IsNullOrWhiteSpace(noFrom) || x.No.CompareTo(noFrom) >= 0));
                query = query.Where(x => (string.IsNullOrWhiteSpace(noTo) || x.No.CompareTo(noTo) <= 0));
                query = query.Where(x => (string.IsNullOrWhiteSpace(dateFrom) || x.CreateDate.CompareTo(dateFrom) >= 0));
                query = query.Where(x => (string.IsNullOrWhiteSpace(dateTo) || x.CreateDate.CompareTo(dateTo) <= 0));

                query = query.Where(x => (string.IsNullOrWhiteSpace(sendDateFrom) || x.LastSendDate.CompareTo(sendDateFrom) >= 0));
                query = query.Where(x => (string.IsNullOrWhiteSpace(sendDateTo) || x.LastSendDate.CompareTo(sendDateTo) <= 0));

                query = query.Where(x => (string.IsNullOrWhiteSpace(responseDateFrom) || x.LastReceiveStatusDate.CompareTo(responseDateFrom) >= 0));
                query = query.Where(x => (string.IsNullOrWhiteSpace(responseDateTo) || x.LastReceiveStatusDate.CompareTo(responseDateTo) <= 0));

                
                query = query.Where(x => (string.IsNullOrWhiteSpace(unitId) || x.EstateUnitId == unitId));
                if (state != "-1")
                {
                    query = query.Where(x => (x.WorkflowStatesId == state || x.WorkflowStates.State == state));
                }
                else
                {
                    if (states != null && states.Length > 0)
                    {
                        query = query.Where(x => states.Contains(x.WorkflowStates.State));
                    }
                }
                
                query = query.Where(x => (string.IsNullOrWhiteSpace(sectionId) || x.EstateSectionId == sectionId));
                query = query.Where(x => (string.IsNullOrWhiteSpace(subSectionId) || x.EstateSubsectionId == subSectionId));
                query = query.Where(x => (string.IsNullOrWhiteSpace(basic) || x.Estatebasic == basic));
                query = query.Where(x => (string.IsNullOrWhiteSpace(secondary) || x.Estatesecondary == secondary));

                query = query.Where(x => (string.IsNullOrWhiteSpace(personName) || x.EstateTaxInquiryPeople.Any(p => p.ChangeState!="3" && p.Name.Contains(personName.NormalizeTextChars(true)))));
                query = query.Where(x => (string.IsNullOrWhiteSpace(personFamily) || x.EstateTaxInquiryPeople.Any(p => p.ChangeState != "3"  && p.Family.Contains(personFamily.NormalizeTextChars(true)))));
                query = query.Where(x => (string.IsNullOrWhiteSpace(personNationalityCode) || x.EstateTaxInquiryPeople.Any(p => p.ChangeState != "3" && p.NationalityCode.Contains(personNationalityCode))));
            }
            foreach (SearchData filter in gridSearchInput)
            {
                if (string.IsNullOrWhiteSpace(filter.Value)) continue;
                var capitalCaseFilter = $"{filter.Filter[..1].ToUpper()}{filter.Filter[1..]}";
                switch (capitalCaseFilter)
                {
                    case nameof(EstateTaxInquiryGridItem.InquiryNo):
                        query = query.Where(x => x.No.Contains(filter.Value));
                        break;
                    case nameof(EstateTaxInquiryGridItem.InquiryDate):
                        query = query.Where(x => x.CreateDate.Contains(filter.Value) || x.CreateTime.Contains(filter.Value));
                        break;
                    case nameof(EstateTaxInquiryGridItem.InquirySendDate):
                        query = query.Where(x => x.LastSendDate.Contains(filter.Value) || x.LastSendTime.Contains(filter.Value));
                        break;
                    case nameof(EstateTaxInquiryGridItem.InquiryResponseDate):
                        query = query.Where(x => x.LastReceiveStatusDate.Contains(filter.Value) || x.LastReceiveStatusTime.Contains(filter.Value));
                        break;
                    case nameof(EstateTaxInquiryGridItem.EstateBasic):
                        query = query.Where(x => x.Estatebasic.Contains(filter.Value));
                        break;
                    case nameof(EstateTaxInquiryGridItem.EstateSecondary):
                        query = query.Where(x => x.Estatesecondary.Contains(filter.Value));
                        break;
                    case nameof(EstateTaxInquiryGridItem.OwnerName):
                        query = query.Where(x => x.EstateTaxInquiryPeople.Any(p => p.ChangeState != "3" && p.DealsummaryPersonRelateTypeId=="108" && (p.Name.Contains(filter.Value.NormalizeTextChars(true)) || p.Family.Contains(filter.Value.NormalizeTextChars(true)))));
                        break;
                    case nameof(EstateTaxInquiryGridItem.OwnerNationalityCode):
                        query = query.Where(x => x.EstateTaxInquiryPeople.Any(p => p.ChangeState != "3" && p.DealsummaryPersonRelateTypeId == "108" && p.NationalityCode.Contains(filter.Value)));
                        break;
                    case nameof(EstateTaxInquiryGridItem.EstateSectionTitle):
                        query = query.Where(x => x.EstateSection.Title.Contains(filter.Value));
                        break;
                    case nameof(EstateTaxInquiryGridItem.EstateSubSectionTitle):
                        query = query.Where(x => x.EstateSubsection.Title.Contains(filter.Value));
                        break;
                    case nameof(EstateTaxInquiryGridItem.StatusTitle):
                        query = query.Where(x => x.WorkflowStates.Title.Contains(filter.Value.NormalizeTextChars(true)));
                        break;
                }

            }

            if (!string.IsNullOrWhiteSpace(globalSearch))
            {
                globalSearch = globalSearch.NormalizeTextChars(true).Trim();

                query = query.Where(x => (EF.Functions.Like(x.WorkflowStates.Title, $"%{globalSearch}%") ||
                                      EF.Functions.Like(x.CreateDate, $"%{globalSearch}%") ||
                                      EF.Functions.Like(x.CreateTime, $"%{globalSearch}%") ||
                                      EF.Functions.Like(x.LastSendDate, $"%{globalSearch}%") ||
                                      EF.Functions.Like(x.LastSendTime, $"%{globalSearch}%") ||
                                      EF.Functions.Like(x.LastReceiveStatusDate, $"%{globalSearch}%") ||
                                      EF.Functions.Like(x.LastReceiveStatusTime, $"%{globalSearch}%") ||
                                      EF.Functions.Like(x.No, $"%{globalSearch}%") ||
                                      EF.Functions.Like(x.Estatebasic, $"%{globalSearch}%") ||
                                      EF.Functions.Like(x.Estatesecondary, $"%{globalSearch}%") ||
                                      EF.Functions.Like(x.EstateSection.Title, $"%{globalSearch}%") ||
                                      EF.Functions.Like(x.EstateSubsection.Title, $"%{globalSearch}%") ||
                                      x.EstateTaxInquiryPeople.Any(p => p.ChangeState != "3" && p.DealsummaryPersonRelateTypeId == "108" && EF.Functions.Like(p.Name, $"%{globalSearch}%")) ||
                                      x.EstateTaxInquiryPeople.Any(p => p.ChangeState != "3" && p.DealsummaryPersonRelateTypeId == "108" && EF.Functions.Like(p.Family, $"%{globalSearch}%"))
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
                Select(y => new EstateTaxInquiryGridItem()
                {
                    InquiryId = y.Id.ToString(),
                    InquiryDate = y.CreateDate + "-" + y.CreateTime,
                    InquiryNo = y.No,
                    EstateBasic = y.Estatebasic,
                    Status = y.WorkflowStates.State,
                    EstateSecondary = y.Estatesecondary,
                    OwnerName = y.EstateTaxInquiryPeople.Where(p => p.DealsummaryPersonRelateTypeId == "108").Select(p => p.Name + " " + p.Family).First(),
                    EstateUnitTitle = y.EstateUnitId,
                    StatusTitle = y.WorkflowStates.Title,
                    EstateSectionTitle = y.EstateSection.Title,
                    EstateSubSectionTitle = y.EstateSubsection.Title,
                    OwnerNationalityCode = y.EstateTaxInquiryPeople.Where(p => p.DealsummaryPersonRelateTypeId == "108").Select(p => p.NationalityCode).First(),
                    InquirySendDate = (!string.IsNullOrWhiteSpace(y.LastSendDate)) ? y.LastSendDate + "-" + y.LastSendTime : "",
                    InquiryResponseDate = (!string.IsNullOrWhiteSpace(y.LastReceiveStatusDate)) ? y.LastReceiveStatusDate + "-" + y.LastReceiveStatusTime : ""
                })

                .ToListAsync(cancellationToken);
            }
            if (result.TotalCount > 0)
            {
                if (isOrderBy)
                {
                    
                    if(gridSortInput.Sort.Equals("inquirydate", StringComparison.OrdinalIgnoreCase))
                    {
                        result.GridItems = await query
                           .OrderBy($"CreateDate {gridSortInput.SortType} ,CreateTime {gridSortInput.SortType}")
                           .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                           .Select(y => new EstateTaxInquiryGridItem()
                           {
                               InquiryId = y.Id.ToString(),
                               InquiryDate = y.CreateDate + "-" + y.CreateTime,
                               InquiryNo = y.No,
                               EstateBasic = y.Estatebasic,
                               Status = y.WorkflowStates.State,
                               EstateSecondary = y.Estatesecondary,
                               OwnerName = y.EstateTaxInquiryPeople.Where(p => p.DealsummaryPersonRelateTypeId == "108").Select(p => p.Name + " " + p.Family).First(),
                               EstateUnitTitle = y.EstateUnitId,
                               StatusTitle = y.WorkflowStates.Title,
                               EstateSectionTitle = y.EstateSection.Title,
                               EstateSubSectionTitle = y.EstateSubsection.Title,
                               OwnerNationalityCode = y.EstateTaxInquiryPeople.Where(p => p.DealsummaryPersonRelateTypeId == "108").Select(p => p.NationalityCode).First(),
                               InquirySendDate = (!string.IsNullOrWhiteSpace(y.LastSendDate)) ? y.LastSendDate + "-" + y.LastSendTime : "",
                               InquiryResponseDate = (!string.IsNullOrWhiteSpace(y.LastReceiveStatusDate)) ? y.LastReceiveStatusDate + "-" + y.LastReceiveStatusTime : ""
                           })
                           
                               .ToListAsync(cancellationToken);
                    }
                    else if (gridSortInput.Sort.Equals("inquirysenddate", StringComparison.OrdinalIgnoreCase))
                    {
                        result.GridItems = await query
                            .OrderBy($"LastSendDate {gridSortInput.SortType} ,LastSendTime {gridSortInput.SortType}")
                           .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                           .Select(y => new EstateTaxInquiryGridItem()
                           {
                               InquiryId = y.Id.ToString(),
                               InquiryDate = y.CreateDate + "-" + y.CreateTime,
                               InquiryNo = y.No,
                               EstateBasic = y.Estatebasic,
                               Status = y.WorkflowStates.State,
                               EstateSecondary = y.Estatesecondary,
                               OwnerName = y.EstateTaxInquiryPeople.Where(p => p.DealsummaryPersonRelateTypeId == "108").Select(p => p.Name + " " + p.Family).First(),
                               EstateUnitTitle = y.EstateUnitId,
                               StatusTitle = y.WorkflowStates.Title,
                               EstateSectionTitle = y.EstateSection.Title,
                               EstateSubSectionTitle = y.EstateSubsection.Title,
                               OwnerNationalityCode = y.EstateTaxInquiryPeople.Where(p => p.DealsummaryPersonRelateTypeId == "108").Select(p => p.NationalityCode).First(),
                               InquirySendDate = (!string.IsNullOrWhiteSpace(y.LastSendDate)) ? y.LastSendDate + "-" + y.LastSendTime : "",
                               InquiryResponseDate = (!string.IsNullOrWhiteSpace(y.LastReceiveStatusDate)) ? y.LastReceiveStatusDate + "-" + y.LastReceiveStatusTime : ""
                           })
                           
                               .ToListAsync(cancellationToken);
                    }
                    else if (gridSortInput.Sort.Equals("inquiryresponsedate", StringComparison.OrdinalIgnoreCase))
                    {
                        result.GridItems = await query
                            .OrderBy($"LastReceiveStatusDate {gridSortInput.SortType} ,LastReceiveStatusTime {gridSortInput.SortType}")
                           .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                           .Select(y => new EstateTaxInquiryGridItem()
                           {
                               InquiryId = y.Id.ToString(),
                               InquiryDate = y.CreateDate + "-" + y.CreateTime,
                               InquiryNo = y.No,
                               EstateBasic = y.Estatebasic,
                               Status = y.WorkflowStates.State,
                               EstateSecondary = y.Estatesecondary,
                               OwnerName = y.EstateTaxInquiryPeople.Where(p => p.DealsummaryPersonRelateTypeId == "108").Select(p => p.Name + " " + p.Family).First(),
                               EstateUnitTitle = y.EstateUnitId,
                               StatusTitle = y.WorkflowStates.Title,
                               EstateSectionTitle = y.EstateSection.Title,
                               EstateSubSectionTitle = y.EstateSubsection.Title,
                               OwnerNationalityCode = y.EstateTaxInquiryPeople.Where(p => p.DealsummaryPersonRelateTypeId == "108").Select(p => p.NationalityCode).First(),
                               InquirySendDate = (!string.IsNullOrWhiteSpace(y.LastSendDate)) ? y.LastSendDate + "-" + y.LastSendTime : "",
                               InquiryResponseDate = (!string.IsNullOrWhiteSpace(y.LastReceiveStatusDate)) ? y.LastReceiveStatusDate + "-" + y.LastReceiveStatusTime : ""
                           })
                           
                               .ToListAsync(cancellationToken);
                    }
                    else                        
                    {
                        result.GridItems = await query
                            .Select(y => new EstateTaxInquiryGridItem()
                            {
                                InquiryId = y.Id.ToString(),
                                InquiryDate = y.CreateDate + "-" + y.CreateTime,
                                InquiryNo = y.No,
                                EstateBasic = y.Estatebasic,
                                Status = y.WorkflowStates.State,
                                EstateSecondary = y.Estatesecondary,
                                OwnerName = y.EstateTaxInquiryPeople.Where(p => p.DealsummaryPersonRelateTypeId == "108").Select(p => p.Name + " " + p.Family).First(),
                                EstateUnitTitle = y.EstateUnitId,
                                StatusTitle = y.WorkflowStates.Title,
                                EstateSectionTitle = y.EstateSection.Title,
                                EstateSubSectionTitle =y.EstateSubsection.Title,
                                OwnerNationalityCode = y.EstateTaxInquiryPeople.Where(p => p.DealsummaryPersonRelateTypeId == "108").Select(p => p.NationalityCode).First(),
                                InquirySendDate = (!string.IsNullOrWhiteSpace(y.LastSendDate)) ? y.LastSendDate + "-" + y.LastSendTime : "",
                                InquiryResponseDate = (!string.IsNullOrWhiteSpace(y.LastReceiveStatusDate)) ? y.LastReceiveStatusDate + "-" + y.LastReceiveStatusTime : ""
                            })
                            .OrderBy($"{gridSortInput.Sort} {gridSortInput.SortType} ")
                            .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                                .ToListAsync(cancellationToken);
                    }
                }
                else
                {
                    result.GridItems = await query
                        .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                        .Select(y => new EstateTaxInquiryGridItem()
                        {
                            InquiryId = y.Id.ToString(),
                            InquiryDate = y.CreateDate + "-" + y.CreateTime,
                            InquiryNo = y.No,
                            EstateBasic = y.Estatebasic,
                            Status = y.WorkflowStates.State,
                            EstateSecondary = y.Estatesecondary,
                            OwnerName = y.EstateTaxInquiryPeople.Where(p => p.DealsummaryPersonRelateTypeId == "108").Select(p => p.Name + " " + p.Family).First(),
                            EstateUnitTitle = y.EstateUnitId,
                            StatusTitle = y.WorkflowStates.Title,
                            EstateSectionTitle = y.EstateSection.Title,
                            EstateSubSectionTitle = y.EstateSubsection.Title,
                            OwnerNationalityCode = y.EstateTaxInquiryPeople.Where(p => p.DealsummaryPersonRelateTypeId == "108").Select(p => p.NationalityCode).First(),
                            InquirySendDate = (!string.IsNullOrWhiteSpace(y.LastSendDate)) ? y.LastSendDate + "-" + y.LastSendTime : "",
                            InquiryResponseDate = (!string.IsNullOrWhiteSpace(y.LastReceiveStatusDate)) ? y.LastReceiveStatusDate + "-" + y.LastReceiveStatusTime : ""
                        })
                        .ToListAsync(cancellationToken);
                }
            }

            Helper.NormalizeStringValuesDeeply(result, false);
            return result;
        }

        public async Task<EstateTaxInquiry> GetEstateTaxInquiryById(string id,CancellationToken cancellationToken)
        {
            return await this.Table
                         .Include(x=>x.EstateTaxInquiryDocumentRequestType)
                         .Include(x => x.BuildingUsingType)
                         .Include(x => x.EstateInquiry)
                         .ThenInclude(x => x.EstateSection)
                         .Include(x => x.EstateInquiry)
                         .ThenInclude(x => x.EstateInquiryPeople)
                         .Include(x => x.EstateTaxCity)
                         .Include(x => x.EstateTaxCounty)
                         .Include(x => x.EstateTaxInquiryAttaches)
                         .ThenInclude(x => x.EstatePieceType)
                         .Include(x => x.EstateTaxInquiryAttaches)
                         .ThenInclude(x => x.EstateSideType)
                         .Include(x => x.EstateTaxInquiryBuildingConstructionStep)
                         .Include(x => x.EstateTaxInquiryBuildingStatus)
                         .Include(x => x.EstateTaxInquiryBuildingType)
                         .Include(x => x.EstateTaxInquiryFieldType)
                         .Include(x => x.EstateTaxInquiryFiles)
                         .Include(x => x.EstateTaxInquiryPeople)
                         .ThenInclude(x => x.DealsummaryPersonRelateType)
                         .Include(x => x.EstateTaxInquiryTransferType)
                         .Include(x => x.EstateTaxUnit)
                         .Include(x => x.FieldUsingType)
                         .Include(x => x.FkEstateTaxProvince)
                         .Include(x => x.LocationAssignRigthOwnershipType)
                         .Include(x => x.LocationAssignRigthUsingType)
                         .Include(x => x.WorkflowStates)
                         .Include(x => x.EstateTaxInquirySendedSms)
                         .Include(x=>x.EstateSection)
                         .Where(e => e.Id == id.ToGuid())
                         .FirstOrDefaultAsync(cancellationToken);
        }
        
    }
}
