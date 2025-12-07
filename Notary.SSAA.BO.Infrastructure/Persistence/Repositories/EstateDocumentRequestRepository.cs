using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Estate;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;
using Notary.SSAA.BO.Infrastructure.Contexts;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.Utilities.Estate;
using Notary.SSAA.BO.Utilities.Extensions;
using System.Linq.Dynamic.Core;

namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories
{
    public class EstateDocumentRequestRepository:Repository<EstateDocumentRequest>,IEstateDocumentRequestRepository
    {
        public EstateDocumentRequestRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }
        public async Task<string> GetMaxNo(string scriptorumId,string no, CancellationToken cancellationToken)
        {
            return await TableNoTracking.Where(x => x.ScriptoriumId == scriptorumId && x.No.StartsWith(no)).Select(x => x.No).MaxAsync(cancellationToken);
        }

        public async Task<List<EstateDocumentRequestSpecialFields>> GetSimilarRequestList(string scriptorumId,string personNationalNo,string estateUnitId,string estateBasic,bool estateBasicIsRemaining,string estateSecondary, bool estateSecondaryIsRemaining, string estateSectionId,string estateSubSectionId,string currentRequestId,string rejectedRequestId, CancellationToken cancellationToken)
        {
            
            var rejectedRequestGuid = !string.IsNullOrWhiteSpace(rejectedRequestId) ? rejectedRequestId.ToGuid() : Guid.Empty;
            var currentRequestGuid = !string.IsNullOrWhiteSpace(currentRequestId) ? currentRequestId.ToGuid() : Guid.Empty;
            var states = new string[] {
                EstateConstant.EstateDocumentRequestStates.Canceled,
                EstateConstant.EstateDocumentRequestStates.Rejected
            };
            var estateDocumentRequests = await TableNoTracking
                .Where(r => r.ScriptoriumId == scriptorumId
                      && r.UnitId == estateUnitId
                      && r.Basic == estateBasic
                      && r.BasicRemaining == estateBasicIsRemaining.ToYesNo()
                      && r.Secondary == estateSecondary
                      && r.SecondaryRemaining == estateSecondaryIsRemaining.ToYesNo()
                      && r.EstateSectionId == estateSectionId
                      && r.EstateSubsectionId == estateSubSectionId
                      && r.EstateDocumentRequestPeople.Any(p => p.NationalNo == personNationalNo && p.IsOriginal == EstateConstant.BooleanConstant.True)
                      && (!states.Contains(r.WorkflowStatesId) || (r.WorkflowStatesId == EstateConstant.EstateDocumentRequestStates.Rejected && r.Id != rejectedRequestGuid))
                      && r.Id != currentRequestGuid
                      )
                .Select(r=>new EstateDocumentRequestSpecialFields()
                {
                    CreateDate = r.CreateDate,
                    CreateTime = r.CreateTime,
                    Id = r.Id,
                    No = r.No,
                    Status = r.WorkflowStates.State,
                    StatusTitle = r.WorkflowStates.Title
                })
                .ToListAsync(cancellationToken);
            return estateDocumentRequests;

        }

        private static T GetValue<T>(Dictionary<string, object> extraParam, string key)
        {
            if (extraParam.TryGetValue(key, out object value))
            {
                return (T)value;
            }
            return default;
        }

        public async Task<EstateDocumentRequestGrid> GetEstateDocumentRequestGridItems(int pageIndex, int pageSize, ICollection<SearchData> gridSearchInput, string globalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> fieldsNotInFilterSearch, string scriptoriumId, bool isOrderBy, string[] statusId, Dictionary<string, object> extraParam, CancellationToken cancellationToken)
        {
            EstateDocumentRequestGrid result = new();

            EstateDocumentRequestGridItem lookupFilterItem = new();

            var query = TableNoTracking
                .Where(x => x.ScriptoriumId == scriptoriumId);

            if (statusId != null && statusId.Length > 0)
            {
                query = query.Where(x => statusId.Contains(x.WorkflowStatesId));
            }
            if (extraParam != null)
            {

                var dateFrom = GetValue<string>(extraParam, "RequestDateFrom");
                var dateTo = GetValue<string>(extraParam, "RequestDateTo");
                var no = GetValue<string>(extraParam, "RequestNo");
                var requestTypeId = GetValue<string>(extraParam, "RequestTypeId");
                var estatePieceNo = GetValue<string>(extraParam, "RequestEstatePieceNo");
                var unitId = GetValue<string>(extraParam, "RequestEstateUnitId");
                var state = GetValue<string>(extraParam, "RequestStatusId");
                var sectionId = GetValue<string>(extraParam, "RequestEstateSectionId");
                var subSectionId = GetValue<string>(extraParam, "RequestEstateSubSectionId");
                var basic = GetValue<string>(extraParam, "RequestEstateBasic");
                var secondary = GetValue<string>(extraParam, "RequestEstateSecondary");
                var estateBlockNo = GetValue<string>(extraParam, "RequestEstateBlockNo");
                var personName = GetValue<string>(extraParam, "RequestPersonName");
                var estatePostCode = GetValue<string>(extraParam, "RequestEstatePostCode");
                var personFamily = GetValue<string>(extraParam, "RequestPersonFamily");
                var personNationalityCode = GetValue<string>(extraParam, "RequestPersonNationalityCode");
                var requestEstateAddress = GetValue<string>(extraParam, "RequestEstateAddress");
                var requestEstateTypeId = GetValue<string>(extraParam, "RequestEstateTypeId");
                var requestEstateOwnershipTypeId = GetValue<string>(extraParam, "RequestEstateOwnershipTypeId");
                var transferIsInSSAR = GetValue<bool?>(extraParam, "TransferIsInSSAR");
                var transferDate = GetValue<string>(extraParam, "TransferDate");
                var transferNo = GetValue<string>(extraParam, "TransferNo");
                var transferSecretCode = GetValue<string>(extraParam, "TransferSecretCode");
                var transferScriptoriumId = GetValue<string>(extraParam, "TransferScriptoriumId");
                var currentDocumentTypeId = GetValue<string>(extraParam, "CurrentDocumentTypeId");
                

                query = query.Where(x => (string.IsNullOrWhiteSpace(no) || x.No.Contains(no)));                
                query = query.Where(x => (string.IsNullOrWhiteSpace(dateFrom) || x.CreateDate.CompareTo(dateFrom) >= 0));
                query = query.Where(x => (string.IsNullOrWhiteSpace(dateTo) || x.CreateDate.CompareTo(dateTo) <= 0));
                query = query.Where(x => (string.IsNullOrWhiteSpace(estatePieceNo) || x.PieceNo.Contains(estatePieceNo)));
                query = query.Where(x => (string.IsNullOrWhiteSpace(unitId) || x.UnitId == unitId));
                if (state != "-1" && !string.IsNullOrWhiteSpace(state))
                {
                    query = query.Where(x => (x.WorkflowStatesId == state || x.WorkflowStates.State == state));
                }                
                query = query.Where(x => (string.IsNullOrWhiteSpace(sectionId) || x.EstateSectionId == sectionId));
                query = query.Where(x => (string.IsNullOrWhiteSpace(subSectionId) || x.EstateSubsectionId == subSectionId));
                query = query.Where(x => (string.IsNullOrWhiteSpace(basic) || x.Basic == basic));
                query = query.Where(x => (string.IsNullOrWhiteSpace(secondary) || x.Secondary == secondary));
                query = query.Where(x => (string.IsNullOrWhiteSpace(estateBlockNo) || x.BlockNo.Contains(estateBlockNo)));
                query = query.Where(x => (string.IsNullOrWhiteSpace(requestTypeId) || x.DocumentRequestTypeId == requestTypeId));
                query = query.Where(x => (string.IsNullOrWhiteSpace(personName) || x.EstateDocumentRequestPeople.Any(p => p.Name.Contains(personName.NormalizeTextChars(true)))));
                query = query.Where(x => (string.IsNullOrWhiteSpace(estatePostCode) || x.PostalCode.Contains(estatePostCode)));
                query = query.Where(x => (string.IsNullOrWhiteSpace(personFamily) || x.EstateDocumentRequestPeople.Any(p => p.Family.Contains(personFamily.NormalizeTextChars(true)))));
                query = query.Where(x => (string.IsNullOrWhiteSpace(personNationalityCode) || x.EstateDocumentRequestPeople.Any(p => p.NationalNo == personNationalityCode)));
                query = query.Where(x => (string.IsNullOrWhiteSpace(requestEstateAddress) || x.Address.Contains(requestEstateAddress)));
                query = query.Where(x => (string.IsNullOrWhiteSpace(requestEstateTypeId) || x.EstateTypeId == requestEstateTypeId));
                query = query.Where(x => (string.IsNullOrWhiteSpace(requestEstateOwnershipTypeId) || x.EstateOwnershipTypeId == requestEstateOwnershipTypeId));
                query = query.Where(x => (string.IsNullOrWhiteSpace(transferScriptoriumId) || x.TransferDocumentScriptoriumId == transferScriptoriumId));
                query = query.Where(x => (transferIsInSSAR != true || x.TransferDocumentIsInSsar == EstateConstant.BooleanConstant.True));
                query = query.Where(x => (string.IsNullOrWhiteSpace(transferNo) || x.TransferDocumentNo.Contains(transferNo)));
                query = query.Where(x => (string.IsNullOrWhiteSpace(transferDate) || x.TransferDocumentDate.Contains(transferDate)));
                query = query.Where(x => (string.IsNullOrWhiteSpace(transferSecretCode) || x.TransferDocumentVerificationCode.Contains(transferSecretCode)));
                query = query.Where(x => (string.IsNullOrWhiteSpace(currentDocumentTypeId) || x.DocumentCurrentTypeId == currentDocumentTypeId));
            }
            foreach (SearchData filter in gridSearchInput)
            {
                if (string.IsNullOrWhiteSpace(filter.Value)) continue;
                var capitalCaseFilter = $"{filter.Filter[..1].ToUpper()}{filter.Filter[1..]}";
                switch (capitalCaseFilter)
                {
                    case nameof(EstateDocumentRequestGridItem.RequestNo):
                        query = query.Where(x => x.No.Contains(filter.Value));
                        break;
                    case nameof(EstateDocumentRequestGridItem.RequestDate):
                        query = query.Where(x => x.CreateDate.Contains(filter.Value) || x.CreateTime.Contains(filter.Value));
                        break;
                    case nameof(EstateDocumentRequestGridItem.EstateBasic):
                        query = query.Where(x => x.Basic.Contains(filter.Value));
                        break;
                    case nameof(EstateDocumentRequestGridItem.EstateSecondary):
                        query = query.Where(x => x.Secondary.Contains(filter.Value));
                        break;
                    case nameof(EstateDocumentRequestGridItem.OwnerName):
                        query = query.Where(x => x.EstateDocumentRequestPeople.Any(p => p.IsOriginal == EstateConstant.BooleanConstant.True && (p.Name.Contains(filter.Value.NormalizeTextChars(true)) || p.Family.Contains(filter.Value.NormalizeTextChars(true)))));
                        break;
                    
                    case nameof(EstateDocumentRequestGridItem.EstateSectionTitle):
                        query = query.Where(x => x.EstateSection.Title.Contains(filter.Value.NormalizeTextChars(true)));
                        break;
                    case nameof(EstateDocumentRequestGridItem.EstateSubSectionTitle):
                        query = query.Where(x => x.EstateSubsection.Title.Contains(filter.Value.NormalizeTextChars(true)));
                        break;
                    case nameof(EstateDocumentRequestGridItem.EstateTypeTitle):
                        query = query.Where(x => x.EstateType.Title.Contains(filter.Value.NormalizeTextChars(true)));
                        break;
                    case nameof(EstateDocumentRequestGridItem.OwnershipDocumentTypeTitle):
                        query = query.Where(x => x.DocumentCurrentType.Title.Contains(filter.Value.NormalizeTextChars(true)));
                        break;
                    case nameof(EstateDocumentRequestGridItem.PreviousRequestNo):
                        query = query.Where(x => x.DefectiveRequest.No.Contains(filter.Value));
                        break;
                    case nameof(EstateDocumentRequestGridItem.RequestTypeTitle):
                        query = query.Where(x => x.DocumentRequestType.Title.Contains(filter.Value.NormalizeTextChars(true)));
                        break;
                    case nameof(EstateDocumentRequestGridItem.StatusTitle):
                        query = query.Where(x => x.WorkflowStates.Title.Contains(filter.Value.NormalizeTextChars(true)));
                        break;
                    case nameof(EstateDocumentRequestGridItem.Hamesh):
                        query = query.Where(x => x.Hamesh.Contains(filter.Value.NormalizeTextChars(true)));
                        break;
                }

            }

            if (!string.IsNullOrWhiteSpace(globalSearch))
            {

                query = query.Where(x => (x.WorkflowStates.Title.Contains(globalSearch.NormalizeTextChars(true)) ||
                                      x.CreateDate.Contains(globalSearch) ||
                                      x.CreateTime.Contains(globalSearch) ||
                                      x.No.Contains(globalSearch) ||
                                      x.Basic.Contains(globalSearch) ||
                                      x.Secondary.Contains(globalSearch) ||
                                      x.DocumentRequestType.Title.Contains(globalSearch.NormalizeTextChars(true)) ||
                                      (x.DefectiveRequestId != null && x.DefectiveRequest.No.Contains(globalSearch)) ||
                                      x.DocumentCurrentType.Title.Contains(globalSearch.NormalizeTextChars(true)) ||
                                      x.EstateType.Title.Contains(globalSearch.NormalizeTextChars(true)) ||
                                      x.EstateSection.Title.Contains(globalSearch.NormalizeTextChars(true)) ||
                                      x.EstateSubsection.Title.Contains(globalSearch.NormalizeTextChars(true)) ||
                                      x.EstateDocumentRequestPeople.Any(p => p.Name.Contains(globalSearch.NormalizeTextChars(true))) ||
                                      x.EstateDocumentRequestPeople.Any(p => p.Family.Contains(globalSearch.NormalizeTextChars(true))) ||
                                      x.Hamesh.Contains(globalSearch.NormalizeTextChars(true))
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
                Select(y => new EstateDocumentRequestGridItem()
                {
                    RequestId = y.Id.ToString(),
                    RequestDate = y.CreateDate,
                    RequestNo = y.No,
                    EstateBasic = y.Basic,
                    EstateSectionTitle = y.EstateSection.Title,
                    EstateSubSectionTitle = y.EstateSubsection.Title,
                    Status = y.WorkflowStates.State,
                    EstateSecondary = y.Secondary,
                    EstateTypeTitle = y.EstateType.Title,
                    OwnershipDocumentTypeTitle = y.DocumentCurrentType.Title,
                    OwnerName = y.EstateDocumentRequestPeople.Where(p => p.IsOriginal == EstateConstant.BooleanConstant.True).Select(p => p.Name + " " + p.Family).First(),
                    EstateUnitTitle = y.UnitId,
                    StatusTitle = y.WorkflowStates.Title,
                    PreviousRequestNo = (y.DefectiveRequestId != null) ? y.DefectiveRequest.No : "",
                    RequestTypeTitle = y.DocumentRequestType.Title,
                    Hamesh = y.Hamesh,
                    RequestTime = y.CreateTime
                })

                .ToListAsync(cancellationToken);
            }
            if (result.TotalCount > 0)
            {
                if (isOrderBy)
                {
                    if (gridSortInput.Sort.Equals("requestdate", StringComparison.OrdinalIgnoreCase))
                    {
                        result.GridItems = await query
                            .Select(y => new EstateDocumentRequestGridItem()
                            {
                                RequestId = y.Id.ToString(),
                                RequestDate = y.CreateDate,
                                RequestNo = y.No,
                                EstateBasic = y.Basic,
                                EstateSectionTitle = y.EstateSection.Title,
                                EstateSubSectionTitle = y.EstateSubsection.Title,
                                Status = y.WorkflowStates.State,
                                EstateSecondary = y.Secondary,
                                EstateTypeTitle = y.EstateType.Title,
                                OwnershipDocumentTypeTitle = y.DocumentCurrentType.Title,
                                OwnerName = y.EstateDocumentRequestPeople.Where(p => p.IsOriginal == EstateConstant.BooleanConstant.True).Select(p => p.Name + " " + p.Family).First(),
                                EstateUnitTitle = y.UnitId,
                                StatusTitle = y.WorkflowStates.Title,
                                PreviousRequestNo = (y.DefectiveRequestId != null) ? y.DefectiveRequest.No : "",
                                RequestTypeTitle = y.DocumentRequestType.Title,
                                Hamesh = y.Hamesh,
                                RequestTime = y.CreateTime
                            })
                            .OrderBy($"{gridSortInput.Sort} {gridSortInput.SortType},RequestTime  {gridSortInput.SortType}")
                            .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                                .ToListAsync(cancellationToken);
                    }
                    else
                    {
                        
                        result.GridItems = await query
                            .Select(y => new EstateDocumentRequestGridItem()
                            {
                                RequestId = y.Id.ToString(),
                                RequestDate = y.CreateDate,
                                RequestNo = y.No,
                                EstateBasic = y.Basic,
                                EstateSectionTitle = y.EstateSection.Title,
                                EstateSubSectionTitle = y.EstateSubsection.Title,
                                Status = y.WorkflowStates.State,
                                EstateSecondary = y.Secondary,
                                EstateTypeTitle = y.EstateType.Title,
                                OwnershipDocumentTypeTitle = y.DocumentCurrentType.Title,
                                OwnerName = y.EstateDocumentRequestPeople.Where(p => p.IsOriginal == EstateConstant.BooleanConstant.True).Select(p => p.Name + " " + p.Family).First(),
                                EstateUnitTitle = y.UnitId,
                                StatusTitle = y.WorkflowStates.Title,
                                PreviousRequestNo = (y.DefectiveRequestId != null) ? y.DefectiveRequest.No : "",
                                RequestTypeTitle = y.DocumentRequestType.Title,
                                Hamesh = y.Hamesh,
                                RequestTime = y.CreateTime
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
                        .Select(y => new EstateDocumentRequestGridItem()
                        {
                            RequestId = y.Id.ToString(),
                            RequestDate = y.CreateDate,
                            RequestNo = y.No,
                            EstateBasic = y.Basic,
                            EstateSectionTitle = y.EstateSection.Title,
                            EstateSubSectionTitle = y.EstateSubsection.Title,
                            Status = y.WorkflowStates.State,
                            EstateSecondary = y.Secondary,
                            EstateTypeTitle = y.EstateType.Title,
                            OwnershipDocumentTypeTitle = y.DocumentCurrentType.Title,
                            OwnerName = y.EstateDocumentRequestPeople.Where(p => p.IsOriginal == EstateConstant.BooleanConstant.True).Select(p => p.Name + " " + p.Family).First(),
                            EstateUnitTitle = y.UnitId,
                            StatusTitle = y.WorkflowStates.Title,
                            PreviousRequestNo = (y.DefectiveRequestId != null) ? y.DefectiveRequest.No : "",
                            RequestTypeTitle = y.DocumentRequestType.Title,
                            Hamesh = y.Hamesh,
                            RequestTime = y.CreateTime
                        })
                        .ToListAsync(cancellationToken);
                }
            }

            foreach (var item in result.GridItems)
            {
                if (!string.IsNullOrWhiteSpace(item.RequestTime))
                    item.RequestDate = item.RequestDate + "-" + item.RequestTime;
            }
            Helper.NormalizeStringValuesDeeply(result,false);
            return result;
        }

        public async Task<EstateDocumentRequestGrid> GetRejectedEstateDocumentRequestGridItems(int pageIndex, int pageSize, ICollection<SearchData> gridSearchInput, string globalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> fieldsNotInFilterSearch, string scriptoriumId, bool isOrderBy,  CancellationToken cancellationToken)
        {
            EstateDocumentRequestGrid result = new();

            EstateDocumentRequestGridItem lookupFilterItem = new();

            var query = TableNoTracking
                .Where(x => x.ScriptoriumId == scriptoriumId
                && x.WorkflowStatesId == EstateConstant.EstateDocumentRequestStates.Rejected
                && !x.InverseDefectiveRequest.Where(r => r.WorkflowStatesId != EstateConstant.EstateDocumentRequestStates.Canceled).Any());
            
            foreach (SearchData filter in gridSearchInput)
            {
                if (string.IsNullOrWhiteSpace(filter.Value)) continue;
                var capitalCaseFilter = $"{filter.Filter[..1].ToUpper()}{filter.Filter[1..]}";
                switch (capitalCaseFilter)
                {
                    case nameof(EstateDocumentRequestGridItem.RequestNo):
                        query = query.Where(x => x.No.Contains(filter.Value));
                        break;
                    case nameof(EstateDocumentRequestGridItem.RequestDate):
                        query = query.Where(x => x.CreateDate.Contains(filter.Value) || x.CreateTime.Contains(filter.Value));
                        break;
                    case nameof(EstateDocumentRequestGridItem.EstateBasic):
                        query = query.Where(x => x.Basic.Contains(filter.Value));
                        break;
                    case nameof(EstateDocumentRequestGridItem.EstateSecondary):
                        query = query.Where(x => x.Secondary.Contains(filter.Value));
                        break;
                    case nameof(EstateDocumentRequestGridItem.OwnerName):
                        query = query.Where(x => x.EstateDocumentRequestPeople.Any(p => p.IsOriginal == EstateConstant.BooleanConstant.True && ( p.Name.Contains(filter.Value.NormalizeTextChars(true)) || p.Family.Contains(filter.Value.NormalizeTextChars(true)))));
                        break;                   
                    case nameof(EstateDocumentRequestGridItem.EstateSectionTitle):
                        query = query.Where(x => x.EstateSection.Title.Contains(filter.Value.NormalizeTextChars(true)));
                        break;
                    case nameof(EstateDocumentRequestGridItem.EstateSubSectionTitle):
                        query = query.Where(x => x.EstateSubsection.Title.Contains(filter.Value.NormalizeTextChars(true)));
                        break;
                    case nameof(EstateDocumentRequestGridItem.EstateTypeTitle):
                        query = query.Where(x => x.EstateType.Title.Contains(filter.Value.NormalizeTextChars(true)));
                        break;
                    case nameof(EstateDocumentRequestGridItem.OwnershipDocumentTypeTitle):
                        query = query.Where(x => x.DocumentCurrentType.Title.Contains(filter.Value.NormalizeTextChars(true)));
                        break;
                    case nameof(EstateDocumentRequestGridItem.PreviousRequestNo):
                        query = query.Where(x => x.DefectiveRequest.No.Contains(filter.Value));
                        break;
                    case nameof(EstateDocumentRequestGridItem.RequestTypeTitle):
                        query = query.Where(x => x.DocumentRequestType.Title.Contains(filter.Value.NormalizeTextChars(true)));
                        break;
                    case nameof(EstateDocumentRequestGridItem.StatusTitle):
                        query = query.Where(x => x.WorkflowStates.Title.Contains(filter.Value.NormalizeTextChars(true)));
                        break;
                    case nameof(EstateDocumentRequestGridItem.Hamesh):
                        query = query.Where(x => x.Hamesh.Contains(filter.Value.NormalizeTextChars(true)));
                        break;
                }

            }

            if (!string.IsNullOrWhiteSpace(globalSearch))
            {

                query = query.Where(x => (x.WorkflowStates.Title.Contains(globalSearch.NormalizeTextChars(true)) ||
                                      x.CreateDate.Contains(globalSearch) ||
                                      x.CreateTime.Contains(globalSearch) ||
                                      x.No.Contains(globalSearch) ||
                                      x.Basic.Contains(globalSearch) ||
                                      x.Secondary.Contains(globalSearch) ||
                                      x.DocumentRequestType.Title.Contains(globalSearch.NormalizeTextChars(true)) ||
                                      (x.DefectiveRequestId != null && x.DefectiveRequest.No.Contains(globalSearch)) ||
                                      x.DocumentCurrentType.Title.Contains(globalSearch.NormalizeTextChars(true)) ||
                                      x.EstateType.Title.Contains(globalSearch.NormalizeTextChars(true)) ||
                                      x.EstateSection.Title.Contains(globalSearch.NormalizeTextChars(true)) ||
                                      x.EstateSubsection.Title.Contains(globalSearch.NormalizeTextChars(true)) ||
                                      x.EstateDocumentRequestPeople.Any(p => p.Name.Contains(globalSearch.NormalizeTextChars(true))) ||
                                      x.EstateDocumentRequestPeople.Any(p => p.Family.Contains(globalSearch.NormalizeTextChars(true))) ||
                                      x.Hamesh.Contains(globalSearch.NormalizeTextChars(true))
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
                Select(y => new EstateDocumentRequestGridItem()
                {
                    RequestId = y.Id.ToString(),
                    RequestDate = y.CreateDate,
                    RequestNo = y.No,
                    EstateBasic = y.Basic,
                    EstateSectionTitle = y.EstateSection.Title,
                    EstateSubSectionTitle = y.EstateSubsection.Title,
                    Status = y.WorkflowStates.State,
                    EstateSecondary = y.Secondary,
                    EstateTypeTitle = y.EstateType.Title,
                    OwnershipDocumentTypeTitle = y.DocumentCurrentType.Title,
                    OwnerName = y.EstateDocumentRequestPeople.Where(p => p.IsOriginal == EstateConstant.BooleanConstant.True).Select(p => p.Name + " " + p.Family).First(),
                    EstateUnitTitle = y.UnitId,
                    StatusTitle = y.WorkflowStates.Title,
                    PreviousRequestNo = (y.DefectiveRequestId != null) ? y.DefectiveRequest.No : "",
                    RequestTypeTitle = y.DocumentRequestType.Title,
                    Hamesh = y.Hamesh,
                    RequestTime = y.CreateTime
                })

                .ToListAsync(cancellationToken);
            }
            if (result.TotalCount > 0)
            {
                if (isOrderBy)
                {
                    if (gridSortInput.Sort.Equals("requestdate", StringComparison.OrdinalIgnoreCase))
                    {
                        result.GridItems = await query
                            .Select(y => new EstateDocumentRequestGridItem()
                            {
                                RequestId = y.Id.ToString(),
                                RequestDate = y.CreateDate,
                                RequestNo = y.No,
                                EstateBasic = y.Basic,
                                EstateSectionTitle = y.EstateSection.Title,
                                EstateSubSectionTitle = y.EstateSubsection.Title,
                                Status = y.WorkflowStates.State,
                                EstateSecondary = y.Secondary,
                                EstateTypeTitle = y.EstateType.Title,
                                OwnershipDocumentTypeTitle = y.DocumentCurrentType.Title,
                                OwnerName = y.EstateDocumentRequestPeople.Where(p => p.IsOriginal == EstateConstant.BooleanConstant.True).Select(p => p.Name + " " + p.Family).First(),
                                EstateUnitTitle = y.UnitId,
                                StatusTitle = y.WorkflowStates.Title,
                                PreviousRequestNo = (y.DefectiveRequestId != null) ? y.DefectiveRequest.No : "",
                                RequestTypeTitle = y.DocumentRequestType.Title,
                                Hamesh = y.Hamesh,
                                RequestTime = y.CreateTime
                            })
                            .OrderBy($"{gridSortInput.Sort} {gridSortInput.SortType},RequestTime  {gridSortInput.SortType}")
                            .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                                .ToListAsync(cancellationToken);
                    }
                    else
                    {
                        result.GridItems = await query
                            .Select(y => new EstateDocumentRequestGridItem()
                            {
                                RequestId = y.Id.ToString(),
                                RequestDate = y.CreateDate,
                                RequestNo = y.No,
                                EstateBasic = y.Basic,
                                EstateSectionTitle = y.EstateSection.Title,
                                EstateSubSectionTitle = y.EstateSubsection.Title,
                                Status = y.WorkflowStates.State,
                                EstateSecondary = y.Secondary,
                                EstateTypeTitle = y.EstateType.Title,
                                OwnershipDocumentTypeTitle = y.DocumentCurrentType.Title,
                                OwnerName = y.EstateDocumentRequestPeople.Where(p => p.IsOriginal == EstateConstant.BooleanConstant.True).Select(p => p.Name + " " + p.Family).First(),
                                EstateUnitTitle = y.UnitId,
                                StatusTitle = y.WorkflowStates.Title,
                                PreviousRequestNo = (y.DefectiveRequestId != null) ? y.DefectiveRequest.No : "",
                                RequestTypeTitle = y.DocumentRequestType.Title,
                                Hamesh = y.Hamesh,
                                RequestTime = y.CreateTime
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
                        .Select(y => new EstateDocumentRequestGridItem()
                        {
                            RequestId = y.Id.ToString(),
                            RequestDate = y.CreateDate,
                            RequestNo = y.No,
                            EstateBasic = y.Basic,
                            EstateSectionTitle = y.EstateSection.Title,
                            EstateSubSectionTitle = y.EstateSubsection.Title,
                            Status = y.WorkflowStates.State,
                            EstateSecondary = y.Secondary,
                            EstateTypeTitle = y.EstateType.Title,
                            OwnershipDocumentTypeTitle = y.DocumentCurrentType.Title,
                            OwnerName = y.EstateDocumentRequestPeople.Where(p => p.IsOriginal == EstateConstant.BooleanConstant.True).Select(p => p.Name + " " + p.Family).First(),
                            EstateUnitTitle = y.UnitId,
                            StatusTitle = y.WorkflowStates.Title,
                            PreviousRequestNo = (y.DefectiveRequestId != null) ? y.DefectiveRequest.No : "",
                            RequestTypeTitle = y.DocumentRequestType.Title,
                            Hamesh = y.Hamesh,
                            RequestTime = y.CreateTime
                        })
                        .ToListAsync(cancellationToken);
                }
            }
            foreach (var item in result.GridItems)
            {
                if (!string.IsNullOrWhiteSpace(item.RequestTime))
                    item.RequestDate = item.RequestDate + "-" + item.RequestTime;
            }

            Helper.NormalizeStringValuesDeeply(result, false);
            return result;
        }

        public async Task<EstateDocumentRequestGrid> GetCanceldEstateDocumentRequestGridItems(int pageIndex, int pageSize, ICollection<SearchData> gridSearchInput, string globalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> fieldsNotInFilterSearch, string scriptoriumId, bool isOrderBy, CancellationToken cancellationToken)
        {
            EstateDocumentRequestGrid result = new();

            EstateDocumentRequestGridItem lookupFilterItem = new();

            var query = TableNoTracking
                .Where(x => x.ScriptoriumId == scriptoriumId
                && x.WorkflowStatesId == EstateConstant.EstateDocumentRequestStates.Canceled
                && x.RevokedRequestId == null
                && !x.InverseRevokedRequest.Where(r => r.WorkflowStatesId != EstateConstant.EstateDocumentRequestStates.Canceled).Any());

            foreach (SearchData filter in gridSearchInput)
            {
                if (string.IsNullOrWhiteSpace(filter.Value)) continue;
                var capitalCaseFilter = $"{filter.Filter[..1].ToUpper()}{filter.Filter[1..]}";
                switch (capitalCaseFilter)
                {
                    case nameof(EstateDocumentRequestGridItem.RequestNo):
                        query = query.Where(x => x.No.Contains(filter.Value));
                        break;
                    case nameof(EstateDocumentRequestGridItem.RequestDate):
                        query = query.Where(x => x.CreateDate.Contains(filter.Value) || x.CreateTime.Contains(filter.Value));
                        break;
                    case nameof(EstateDocumentRequestGridItem.EstateBasic):
                        query = query.Where(x => x.Basic.Contains(filter.Value));
                        break;
                    case nameof(EstateDocumentRequestGridItem.EstateSecondary):
                        query = query.Where(x => x.Secondary.Contains(filter.Value));
                        break;
                    case nameof(EstateDocumentRequestGridItem.OwnerName):
                        query = query.Where(x => x.EstateDocumentRequestPeople.Any(p => p.IsOriginal == EstateConstant.BooleanConstant.True && (p.Name.Contains(filter.Value.NormalizeTextChars(true)) || p.Family.Contains(filter.Value.NormalizeTextChars(true)))));
                        break;                    
                    case nameof(EstateDocumentRequestGridItem.EstateSectionTitle):
                        query = query.Where(x => x.EstateSection.Title.Contains(filter.Value.NormalizeTextChars(true)));
                        break;
                    case nameof(EstateDocumentRequestGridItem.EstateSubSectionTitle):
                        query = query.Where(x => x.EstateSubsection.Title.Contains(filter.Value.NormalizeTextChars(true)));
                        break;
                    case nameof(EstateDocumentRequestGridItem.EstateTypeTitle):
                        query = query.Where(x => x.EstateType.Title.Contains(filter.Value.NormalizeTextChars(true)));
                        break;
                    case nameof(EstateDocumentRequestGridItem.OwnershipDocumentTypeTitle):
                        query = query.Where(x => x.DocumentCurrentType.Title.Contains(filter.Value.NormalizeTextChars(true)));
                        break;
                    case nameof(EstateDocumentRequestGridItem.PreviousRequestNo):
                        query = query.Where(x => x.DefectiveRequest.No.Contains(filter.Value));
                        break;
                    case nameof(EstateDocumentRequestGridItem.RequestTypeTitle):
                        query = query.Where(x => x.DocumentRequestType.Title.Contains(filter.Value.NormalizeTextChars(true)));
                        break;
                    case nameof(EstateDocumentRequestGridItem.StatusTitle):
                        query = query.Where(x => x.WorkflowStates.Title.Contains(filter.Value.NormalizeTextChars(true)));
                        break;
                    case nameof(EstateDocumentRequestGridItem.Hamesh):
                        query = query.Where(x => x.Hamesh.Contains(filter.Value.NormalizeTextChars(true)));
                        break;
                }

            }

            if (!string.IsNullOrWhiteSpace(globalSearch))
            {

                query = query.Where(x => (x.WorkflowStates.Title.Contains(globalSearch.NormalizeTextChars(true)) ||
                                      x.CreateDate.Contains(globalSearch) ||
                                      x.CreateTime.Contains(globalSearch) ||
                                      x.No.Contains(globalSearch) ||
                                      x.Basic.Contains(globalSearch) ||
                                      x.Secondary.Contains(globalSearch) ||
                                      x.DocumentRequestType.Title.Contains(globalSearch.NormalizeTextChars(true)) ||
                                      (x.DefectiveRequestId != null && x.DefectiveRequest.No.Contains(globalSearch)) ||
                                      x.DocumentCurrentType.Title.Contains(globalSearch.NormalizeTextChars(true)) ||
                                      x.EstateType.Title.Contains(globalSearch.NormalizeTextChars(true)) ||
                                      x.EstateSection.Title.Contains(globalSearch.NormalizeTextChars(true)) ||
                                      x.EstateSubsection.Title.Contains(globalSearch.NormalizeTextChars(true)) ||
                                      x.EstateDocumentRequestPeople.Any(p => p.Name.Contains(globalSearch.NormalizeTextChars(true))) ||
                                      x.EstateDocumentRequestPeople.Any(p => p.Family.Contains(globalSearch.NormalizeTextChars(true))) ||
                                       x.Hamesh.Contains(globalSearch.NormalizeTextChars(true))
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
                Select(y => new EstateDocumentRequestGridItem()
                {
                    RequestId = y.Id.ToString(),
                    RequestDate = y.CreateDate,
                    RequestNo = y.No,
                    EstateBasic = y.Basic,
                    EstateSectionTitle = y.EstateSection.Title,
                    EstateSubSectionTitle = y.EstateSubsection.Title,
                    Status = y.WorkflowStates.State,
                    EstateSecondary = y.Secondary,
                    EstateTypeTitle = y.EstateType.Title,
                    OwnershipDocumentTypeTitle = y.DocumentCurrentType.Title,
                    OwnerName = y.EstateDocumentRequestPeople.Where(p => p.IsOriginal == EstateConstant.BooleanConstant.True).Select(p => p.Name + " " + p.Family).First(),
                    EstateUnitTitle = y.UnitId,
                    StatusTitle = y.WorkflowStates.Title,
                    PreviousRequestNo = (y.DefectiveRequestId != null) ? y.DefectiveRequest.No : "",
                    RequestTypeTitle = y.DocumentRequestType.Title,
                    Hamesh = y.Hamesh,
                    RequestTime = y.CreateTime
                })

                .ToListAsync(cancellationToken);
            }
            if (result.TotalCount > 0)
            {
                if (isOrderBy)
                {
                    if (gridSortInput.Sort.Equals("requestdate", StringComparison.OrdinalIgnoreCase))
                    {
                        result.GridItems = await query
                            .Select(y => new EstateDocumentRequestGridItem()
                            {
                                RequestId = y.Id.ToString(),
                                RequestDate = y.CreateDate,
                                RequestNo = y.No,
                                EstateBasic = y.Basic,
                                EstateSectionTitle = y.EstateSection.Title,
                                EstateSubSectionTitle = y.EstateSubsection.Title,
                                Status = y.WorkflowStates.State,
                                EstateSecondary = y.Secondary,
                                EstateTypeTitle = y.EstateType.Title,
                                OwnershipDocumentTypeTitle = y.DocumentCurrentType.Title,
                                OwnerName = y.EstateDocumentRequestPeople.Where(p => p.IsOriginal == EstateConstant.BooleanConstant.True).Select(p => p.Name + " " + p.Family).First(),
                                EstateUnitTitle = y.UnitId,
                                StatusTitle = y.WorkflowStates.Title,
                                PreviousRequestNo = (y.DefectiveRequestId != null) ? y.DefectiveRequest.No : "",
                                RequestTypeTitle = y.DocumentRequestType.Title,
                                Hamesh = y.Hamesh,
                                RequestTime = y.CreateTime
                            })
                            .OrderBy($"{gridSortInput.Sort} {gridSortInput.SortType},RequestTime {gridSortInput.SortType}")
                            .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                                .ToListAsync(cancellationToken);
                    }
                    else
                    {
                        result.GridItems = await query
                            .Select(y => new EstateDocumentRequestGridItem()
                            {
                                RequestId = y.Id.ToString(),
                                RequestDate = y.CreateDate,
                                RequestNo = y.No,
                                EstateBasic = y.Basic,
                                EstateSectionTitle = y.EstateSection.Title,
                                EstateSubSectionTitle = y.EstateSubsection.Title,
                                Status = y.WorkflowStates.State,
                                EstateSecondary = y.Secondary,
                                EstateTypeTitle = y.EstateType.Title,
                                OwnershipDocumentTypeTitle = y.DocumentCurrentType.Title,
                                OwnerName = y.EstateDocumentRequestPeople.Where(p => p.IsOriginal == EstateConstant.BooleanConstant.True).Select(p => p.Name + " " + p.Family).First(),
                                EstateUnitTitle = y.UnitId,
                                StatusTitle = y.WorkflowStates.Title,
                                PreviousRequestNo = (y.DefectiveRequestId != null) ? y.DefectiveRequest.No : "",
                                RequestTypeTitle = y.DocumentRequestType.Title,
                                Hamesh = y.Hamesh,
                                RequestTime = y.CreateTime
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
                        .Select(y => new EstateDocumentRequestGridItem()
                        {
                            RequestId = y.Id.ToString(),
                            RequestDate = y.CreateDate,
                            RequestNo = y.No,
                            EstateBasic = y.Basic,
                            EstateSectionTitle = y.EstateSection.Title,
                            EstateSubSectionTitle = y.EstateSubsection.Title,
                            Status = y.WorkflowStates.State,
                            EstateSecondary = y.Secondary,
                            EstateTypeTitle = y.EstateType.Title,
                            OwnershipDocumentTypeTitle = y.DocumentCurrentType.Title,
                            OwnerName = y.EstateDocumentRequestPeople.Where(p => p.IsOriginal == EstateConstant.BooleanConstant.True).Select(p => p.Name + " " + p.Family).First(),
                            EstateUnitTitle = y.UnitId,
                            StatusTitle = y.WorkflowStates.Title,
                            PreviousRequestNo = (y.DefectiveRequestId != null) ? y.DefectiveRequest.No : "",
                            RequestTypeTitle = y.DocumentRequestType.Title,
                            Hamesh = y.Hamesh,
                            RequestTime = y.CreateTime
                        })
                        .ToListAsync(cancellationToken);
                }
            }

            foreach (var item in result.GridItems)
            {
                if (!string.IsNullOrWhiteSpace(item.RequestTime))
                    item.RequestDate = item.RequestDate + "-" + item.RequestTime;
            }
            Helper.NormalizeStringValuesDeeply(result, false);
            return result;
        }

        public async Task<EstateDocumentRequest> GetEstateDocumentRequestById(string estateDocumentRequestId,CancellationToken cancellationToken)
        {
            var query = TableNoTracking
                .Include(r=>r.EstateDocumentRequestPeople)
                .ThenInclude(r=>r.EstateDocReqPersonRelateAgentPeople)
                .ThenInclude(r => r.AgentType)
                .Include(r=>r.DocumentCurrentType)
                .Include(r => r.DocumentRequestType)
                .Include(r => r.EstateOwnershipType)
                .Include(r => r.EstateSection)
                .Include(r => r.EstateSubsection)
                .Include(r => r.EstateType)
                .Include(r => r.TransferDocumentType)
                .Include(r => r.WorkflowStates)
                .Where(r => r.Id == estateDocumentRequestId.ToGuid())
                ;
            return await query.FirstAsync(cancellationToken);
        }
    }
}
