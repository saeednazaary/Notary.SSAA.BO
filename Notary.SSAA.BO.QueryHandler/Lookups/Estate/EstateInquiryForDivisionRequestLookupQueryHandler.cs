using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Coordinator.Estate.Helpers;
using Notary.SSAA.BO.DataTransferObject.Queries.Lookups.Estate;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.BaseInfoService;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Estate;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.QueryHandler.Lookups.Estate
{
    public class EstateInquiryForDivisionRequestLookupQueryHandler : BaseQueryHandler<EstateInquiryForDivisionRequestLookupQuery, ApiResult<EstateInquiryGrid2>>
    {
        private readonly IEstateInquiryRepository _estateInquiryRepository;
        private readonly BaseInfoServiceHelper _baseInfoServiceHelper;
        public EstateInquiryForDivisionRequestLookupQueryHandler(IMediator mediator, IUserService userService,
            IEstateInquiryRepository estateInquiryRepository)
            : base(mediator, userService)
        {
            _estateInquiryRepository = estateInquiryRepository ?? throw new ArgumentNullException(nameof(estateInquiryRepository));
            _baseInfoServiceHelper = new BaseInfoServiceHelper(mediator);
        }

        protected override bool HasAccess(EstateInquiryForDivisionRequestLookupQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<EstateInquiryGrid2>> RunAsync(EstateInquiryForDivisionRequestLookupQuery request, CancellationToken cancellationToken)
        {
            EstateInquiryGrid result = new();

            if (request.GridSortInput.Count == 0)
            {
                var sortData = new SortData()
                {
                    Sort = "InquiryDate",
                    SortType = "desc"
                };
                request.GridSortInput.Add(sortData);
            }

            string[] states = new string[]
            {               
                EstateConstant.EstateInquiryStates.ConfirmResponse,
                EstateConstant.EstateInquiryStates.Archived,
                EstateConstant.EstateInquiryStates.RejectResponse
            };
            string[] specificStatusList = new string[] {
                    EstateConstant.EstateInquirySpecificStatus.OnlyAllowedForDivision,                    
                    EstateConstant.EstateInquirySpecificStatus.AllowedForDivision};
            var toDate = DateTime.Now;
            var persianFromDate1 = toDate.AddDays(-37).ToPersianDate();
            var persianFromDate2 = toDate.AddDays(-50).ToPersianDate();
            var persianToDate = toDate.ToPersianDate();

            var query =  _estateInquiryRepository
                                    .TableNoTracking
                                    .Include(x => x.WorkflowStates)
                                    .Include(x => x.EstateInquiryPeople)
                                    .Include(x=>x.EstateSeridaftar)
                                    .Include(x=>x.EstateSection)
                                    .Include(x=>x.EstateSubsection)
                                    .Where(x => x.ScriptoriumId == request.ExtraParams.ScriptoriumId &&
                                           states.Contains(x.WorkflowStatesId) &&
                                           x.ResponseDate.CompareTo(persianFromDate2) >= 0 &&
                                           x.ResponseDate.CompareTo(persianToDate) <= 0
                                    );
            if (request.ExtraParams.OnlyCurrentEstateInquiry)
            {
                query = query.Where(x => x.EstateInquiryTypeId == "2");
            }
            var scriptorium = await _baseInfoServiceHelper.GetScriptoriumById(new string[] { request.ExtraParams.ScriptoriumId }, cancellationToken);
            foreach (SearchData filter in request.GridFilterInput)
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
                        query = query.Where(x => x.EstateInquiryPeople.Any(p => p.Name.Contains(filter.Value.NormalizeTextChars(true)) || p.Family.Contains(filter.Value.NormalizeTextChars(true))));
                        break;                   
                    case nameof(EstateInquiryGridItem.EstateDocPrintNo):
                        query = query.Where(x => x.DocPrintNo.Contains(filter.Value));
                        break;
                    case nameof(EstateInquiryGridItem.StatusTitle):
                        query = query.Where(x => x.WorkflowStates.Title.Contains(filter.Value.NormalizeTextChars(true)));
                        break;
                    case nameof(EstateInquiryGridItem.EstateDocElectronicNoteNo):
                        query = query.Where(x => x.ElectronicEstateNoteNo.Contains(filter.Value));
                        break;
                    case nameof(EstateInquiryGridItem.EstateDocNoteNo):
                        query = query.Where(x => x.EstateNoteNo.Contains(filter.Value));
                        break;
                    case nameof(EstateInquiryGridItem.EstateDocPageNo):
                        query = query.Where(x => x.PageNo.Contains(filter.Value));
                        break;
                    case nameof(EstateInquiryGridItem.EstateDocSeriDaftarSSAACode):
                        query = query.Where(x => x.EstateSeridaftar.SsaaCode.Contains(filter.Value));
                        break;
                    case nameof(EstateInquiryGridItem.EstateDocSeriDaftarTitle):
                        query = query.Where(x => x.EstateSeridaftar.Title.Contains(filter.Value.NormalizeTextChars(true)));
                        break;
                    case nameof(EstateInquiryGridItem.EstateSectionSSAACode):
                        query = query.Where(x => x.EstateSection.SsaaCode.Contains(filter.Value));
                        break;
                    case nameof(EstateInquiryGridItem.EstateSectionTitle):
                        query = query.Where(x => x.EstateSection.Title.Contains(filter.Value.NormalizeTextChars(true)));
                        break;
                    case nameof(EstateInquiryGridItem.EstateSubSectionSSAACode):
                        query = query.Where(x => x.EstateSubsection.SsaaCode.Contains(filter.Value));
                        break;
                    case nameof(EstateInquiryGridItem.EstateSubSectionTitle):
                        query = query.Where(x => x.EstateSubsection.Title.Contains(filter.Value.NormalizeTextChars(true)));
                        break;
                    case nameof(EstateInquiryGridItem.InsertTime):
                        query = query.Where(x => x.FirstReceiveDate.Contains(filter.Value) || x.FirstReceiveTime.Contains(filter.Value));
                        break;
                    case nameof(EstateInquiryGridItem.LastEditReceiveTime):
                        query = query.Where(x => x.LasteditReceiveDate.Contains(filter.Value) || x.LasteditReceiveTime.Contains(filter.Value));
                        break;
                    case nameof(EstateInquiryGridItem.LastSendTime):
                        query = query.Where(x => x.LastSendDate.Contains(filter.Value) || x.LastSendTime.Contains(filter.Value));
                        break;
                }

            }

            if (!string.IsNullOrWhiteSpace(request.GlobalSearch))
            {

                query = query.Where(x => (x.WorkflowStates.Title.Contains(request.GlobalSearch.NormalizeTextChars(true)) ||
                                      x.InquiryDate.Contains(request.GlobalSearch) ||
                                      x.InquiryNo.Contains(request.GlobalSearch) ||
                                      x.Basic.Contains(request.GlobalSearch) ||
                                      x.Secondary.Contains(request.GlobalSearch) ||
                                      x.DocPrintNo.Contains(request.GlobalSearch) ||
                                      x.EstateInquiryPeople.Any(p => p.Name.Contains(request.GlobalSearch.NormalizeTextChars(true))) ||
                                      x.EstateInquiryPeople.Any(p => p.Family.Contains(request.GlobalSearch.NormalizeTextChars(true))) ||
                                      x.ElectronicEstateNoteNo.Contains(request.GlobalSearch) ||
                                      x.EstateNoteNo.Contains(request.GlobalSearch) ||
                                      x.PageNo.Contains(request.GlobalSearch) ||
                                      x.EstateSection.Title.Contains(request.GlobalSearch.NormalizeTextChars(true)) ||
                                      x.EstateSection.SsaaCode.Contains(request.GlobalSearch) ||
                                      x.EstateSubsection.Title.Contains(request.GlobalSearch.NormalizeTextChars(true)) ||
                                      x.EstateSubsection.SsaaCode.Contains(request.GlobalSearch) ||
                                      x.ResponseDate.Contains(request.GlobalSearch) ||
                                      x.ResponseTime.Contains(request.GlobalSearch) ||
                                      x.FirstReceiveDate.Contains(request.GlobalSearch) ||
                                      x.FirstReceiveTime.Contains(request.GlobalSearch) ||
                                      x.LasteditReceiveDate.Contains(request.GlobalSearch) ||
                                      x.LasteditReceiveTime.Contains(request.GlobalSearch)
                                      ));

            }
            var unitIdList = new List<string>();
            if ( request.SelectedItems.Count > 0)
            {
                var guidlist = new List<Guid>();
                foreach (var item in request.SelectedItems)
                {
                    guidlist.Add(item.ToGuid());
                }
                var queryResult = await _estateInquiryRepository.TableNoTracking
                    .Include(x => x.WorkflowStates)
                    .Include(x => x.EstateInquiryPeople)
                    .Include(x => x.EstateSeridaftar)
                    .Include(x => x.EstateSection)
                    .Include(x => x.EstateSubsection)
                    .Where(p => guidlist.Contains(p.Id))
                .ToListAsync(cancellationToken);
                result.SelectedItems =queryResult.
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
                    ResponseResult = y.ResponseResult,
                    SpecificStatus = y.SpecificStatus,
                    InquiryResponseDate = y.ResponseDate,
                    IsExecutiveTransfer = y.EstateInquiryPeople.First().ExecutiveTransfer.ToBoolean(),
                    AttachedToDealSummary = y.AttachedToDealsummary,
                    EstateDocElectronicNoteNo = y.ElectronicEstateNoteNo,
                    EstateDocNoteNo = y.EstateNoteNo,
                    EstateDocPageNo = y.PageNo,
                    EstateDocSeriDaftarSSAACode = y.EstateSeridaftar != null ? y.EstateSeridaftar.SsaaCode : "",
                    EstateDocSeriDaftarTitle = y.EstateSeridaftar != null ? y.EstateSeridaftar.Title : "",
                    EstateSectionSSAACode = y.EstateSection.SsaaCode,
                    EstateSectionTitle = y.EstateSection.Title,
                    EstateSubSectionSSAACode = y.EstateSubsection.SsaaCode,
                    EstateSubSectionTitle = y.EstateSubsection.Title,
                    InsertTime = !string.IsNullOrWhiteSpace(y.FirstReceiveDate) ? y.FirstReceiveDate + "-" + y.FirstReceiveTime : "",
                    LastSendTime = !string.IsNullOrWhiteSpace(y.LastSendDate) ? y.LastSendDate + "-" + y.LastSendTime : "",
                    LastEditReceiveTime = !string.IsNullOrWhiteSpace(y.LasteditReceiveDate) ? y.LasteditReceiveDate + "-" + y.LasteditReceiveTime : "",
                    RelatedServer = "BO",
                    UnitMessage = y.WorkflowStates.State == "8" || y.WorkflowStates.State == "5" ? y.Response : ""
                }).ToList();
                unitIdList.AddRange(result.SelectedItems.Select(x => x.InquiryUnitName));
            }
            result.TotalCount = await query.CountAsync(cancellationToken);
            GetUnitByIdViewModel unitList = null;
            if (result.TotalCount > 0)
            {
                var queryResult = await query.ToListAsync(cancellationToken);
                unitIdList.AddRange(queryResult.Select(x => x.UnitId));
                
                unitList = await _baseInfoServiceHelper.GetUnitById(unitIdList.ToArray(), cancellationToken);
                var estateInquiryGridItemList = queryResult.Select(x => new EstateInquiryGridItem() { 
                    EstateBasic = x.Basic,
                    EstateDocPrintNo = x.DocPrintNo, 
                    EstateSecondary = x.Secondary, 
                    InquiryDate = x.InquiryDate, 
                    InquiryId = x.Id.ToString(), 
                    InquiryNnumber = x.InquiryNo, 
                    InquiryUnitName = x.UnitId,  
                    OwnerName = x.EstateInquiryPeople.Select(p => p.Name + " " + p.Family).First(), 
                    Status = x.WorkflowStates.State, 
                    StatusTitle = x.WorkflowStates.Title,
                    ResponseResult = x.ResponseResult,
                    SpecificStatus = x.SpecificStatus, 
                    InquiryResponseDate = x.ResponseDate,
                    IsExecutiveTransfer = x.EstateInquiryPeople.First().ExecutiveTransfer.ToBoolean(), 
                    AttachedToDealSummary = x.AttachedToDealsummary,
                    EstateDocElectronicNoteNo = x.ElectronicEstateNoteNo,
                    EstateDocNoteNo = x.EstateNoteNo,
                    EstateDocPageNo = x.PageNo,
                    EstateDocSeriDaftarSSAACode = x.EstateSeridaftar != null ? x.EstateSeridaftar.SsaaCode : "",
                    EstateDocSeriDaftarTitle = x.EstateSeridaftar != null ? x.EstateSeridaftar.Title : "",
                    EstateSectionSSAACode = x.EstateSection.SsaaCode,
                    EstateSectionTitle = x.EstateSection.Title,
                    EstateSubSectionSSAACode = x.EstateSubsection.SsaaCode,
                    EstateSubSectionTitle = x.EstateSubsection.Title,
                    InsertTime = !string.IsNullOrWhiteSpace(x.FirstReceiveDate) ? x.FirstReceiveDate + "-" + x.FirstReceiveTime : "",
                    LastSendTime = !string.IsNullOrWhiteSpace(x.LastSendDate) ? x.LastSendDate + "-" + x.LastSendTime : "",
                    LastEditReceiveTime = !string.IsNullOrWhiteSpace(x.LasteditReceiveDate) ? x.LasteditReceiveDate + "-" + x.LasteditReceiveTime : "",
                    RelatedServer = "BO",
                    UnitMessage = x.WorkflowStates.State == "8" || x.WorkflowStates.State == "5" ? x.Response : ""
                }).ToList();
                estateInquiryGridItemList.ForEach(x =>
                {
                    x.ScriptoriumGeolocationId = scriptorium.ScriptoriumList[0].GeoLocationId;
                    x.UnitGeolocationId = unitList.UnitList.Where(u => u.Id == x.InquiryUnitName).First().GeoLocationId;
                    x.InquiryUnitName = unitList.UnitList.Where(u => u.Id == x.InquiryUnitName).First().Name;
                });

                var resultItemList = estateInquiryGridItemList.Where(x => (x.ResponseResult == "True" && string.IsNullOrWhiteSpace(x.AttachedToDealSummary) && x.InquiryResponseDate.CompareTo(persianFromDate1) >= 0 && x.InquiryResponseDate.CompareTo(persianToDate) <= 0 && x.ScriptoriumGeolocationId == x.UnitGeolocationId && specificStatusList.Contains(x.SpecificStatus))
                                                    || (string.IsNullOrWhiteSpace(x.AttachedToDealSummary)  && x.InquiryResponseDate.CompareTo(persianFromDate1) >= 0 && x.InquiryResponseDate.CompareTo(persianToDate) <= 0 && x.ScriptoriumGeolocationId == x.UnitGeolocationId && ((string.IsNullOrWhiteSpace(x.SpecificStatus) && x.ResponseResult == "True") || x.SpecificStatus == EstateConstant.EstateInquirySpecificStatus.Arrested || x.SpecificStatus == EstateConstant.EstateInquirySpecificStatus.ArrestedAndOnlyAllowedForDivision) && x.IsExecutiveTransfer)
                                                    || (x.ResponseResult == "True" && string.IsNullOrWhiteSpace(x.AttachedToDealSummary) &&  x.InquiryResponseDate.CompareTo(persianFromDate2) >= 0 && x.InquiryResponseDate.CompareTo(persianToDate) <= 0 && x.ScriptoriumGeolocationId != x.UnitGeolocationId && specificStatusList.Contains(x.SpecificStatus))
                                                    || (string.IsNullOrWhiteSpace(x.AttachedToDealSummary)  && x.InquiryResponseDate.CompareTo(persianFromDate2) >= 0 && x.InquiryResponseDate.CompareTo(persianToDate) <= 0 && x.ScriptoriumGeolocationId != x.UnitGeolocationId && ((string.IsNullOrWhiteSpace(x.SpecificStatus) && x.ResponseResult == "True") || x.SpecificStatus == EstateConstant.EstateInquirySpecificStatus.Arrested || x.SpecificStatus == EstateConstant.EstateInquirySpecificStatus.ArrestedAndOnlyAllowedForDivision) && x.IsExecutiveTransfer)
                                                    );
                if (request.GridSortInput != null && request.GridSortInput.Count > 0)
                {
                    var sortData = request.GridSortInput.First();
                    sortData.Sort = $"{sortData.Sort[..1].ToUpper()}{sortData.Sort[1..]}";
                    if (sortData.SortType.Equals("asc", StringComparison.OrdinalIgnoreCase))
                    {
                        switch (sortData.Sort)
                        {
                            case nameof(EstateInquiryGridItem.InquiryNnumber):
                                resultItemList = resultItemList.OrderBy(x => x.InquiryNnumber);
                                break;
                            case nameof(EstateInquiryGridItem.InquiryDate):
                                resultItemList = resultItemList.OrderBy(x => x.InquiryDate);
                                break;
                            case nameof(EstateInquiryGridItem.EstateBasic):
                                resultItemList = resultItemList.OrderBy(x => x.EstateBasic);
                                break;
                            case nameof(EstateInquiryGridItem.EstateSecondary):
                                resultItemList = resultItemList.OrderBy(x => x.EstateSecondary);
                                break;
                            case nameof(EstateInquiryGridItem.OwnerName):
                                resultItemList = resultItemList.OrderBy(x => x.OwnerName);
                                break;                            
                            case nameof(EstateInquiryGridItem.EstateDocPrintNo):
                                resultItemList = resultItemList.OrderBy(x => x.EstateDocPrintNo);
                                break;
                            case nameof(EstateInquiryGridItem.EstateDocElectronicNoteNo):
                                resultItemList = resultItemList.OrderBy(x => x.EstateDocElectronicNoteNo);
                                break;
                            case nameof(EstateInquiryGridItem.EstateDocNoteNo):
                                resultItemList = resultItemList.OrderBy(x => x.EstateDocNoteNo);
                                break;
                            case nameof(EstateInquiryGridItem.EstateDocPageNo):
                                resultItemList = resultItemList.OrderBy(x => x.EstateDocPageNo);
                                break;
                            case nameof(EstateInquiryGridItem.EstateDocSeriDaftarSSAACode):
                                resultItemList = resultItemList.OrderBy(x => x.EstateDocSeriDaftarSSAACode);
                                break;
                            case nameof(EstateInquiryGridItem.EstateDocSeriDaftarTitle):
                                resultItemList = resultItemList.OrderBy(x => x.EstateDocSeriDaftarTitle);
                                break;
                            case nameof(EstateInquiryGridItem.EstateSectionSSAACode):
                                resultItemList = resultItemList.OrderBy(x => x.EstateSectionSSAACode);
                                break;
                            case nameof(EstateInquiryGridItem.EstateSectionTitle):
                                resultItemList = resultItemList.OrderBy(x => x.EstateSectionTitle);
                                break;
                            case nameof(EstateInquiryGridItem.EstateSubSectionSSAACode):
                                resultItemList = resultItemList.OrderBy(x => x.EstateSubSectionSSAACode);
                                break;
                            case nameof(EstateInquiryGridItem.EstateSubSectionTitle):
                                resultItemList = resultItemList.OrderBy(x => x.EstateSubSectionTitle);
                                break;
                            case nameof(EstateInquiryGridItem.InquiryResponseDate):
                                resultItemList = resultItemList.OrderBy(x => x.InquiryResponseDate);
                                break;
                            case nameof(EstateInquiryGridItem.InsertTime):
                                resultItemList = resultItemList.OrderBy(x => x.InsertTime);
                                break;
                            case nameof(EstateInquiryGridItem.LastEditReceiveTime):
                                resultItemList = resultItemList.OrderBy(x => x.LastEditReceiveTime);
                                break;
                            case nameof(EstateInquiryGridItem.LastSendTime):
                                resultItemList = resultItemList.OrderBy(x => x.LastSendTime);
                                break;


                        }
                    }
                    else if(sortData.SortType.Equals("desc", StringComparison.OrdinalIgnoreCase))
                    {
                        switch (sortData.Sort)
                        {
                            case nameof(EstateInquiryGridItem.InquiryNnumber):
                                resultItemList = resultItemList.OrderByDescending(x => x.InquiryNnumber);
                                break;
                            case nameof(EstateInquiryGridItem.InquiryDate):
                                resultItemList = resultItemList.OrderByDescending(x => x.InquiryDate);
                                break;
                            case nameof(EstateInquiryGridItem.EstateBasic):
                                resultItemList = resultItemList.OrderByDescending(x => x.EstateBasic);
                                break;
                            case nameof(EstateInquiryGridItem.EstateSecondary):
                                resultItemList = resultItemList.OrderByDescending(x => x.EstateSecondary);
                                break;
                            case nameof(EstateInquiryGridItem.OwnerName):
                                resultItemList = resultItemList.OrderByDescending(x => x.OwnerName);
                                break;                            
                            case nameof(EstateInquiryGridItem.EstateDocPrintNo):
                                resultItemList = resultItemList.OrderByDescending(x => x.EstateDocPrintNo);
                                break;
                            case nameof(EstateInquiryGridItem.EstateDocElectronicNoteNo):
                                resultItemList = resultItemList.OrderByDescending(x => x.EstateDocElectronicNoteNo);
                                break;
                            case nameof(EstateInquiryGridItem.EstateDocNoteNo):
                                resultItemList = resultItemList.OrderByDescending(x => x.EstateDocNoteNo);
                                break;
                            case nameof(EstateInquiryGridItem.EstateDocPageNo):
                                resultItemList = resultItemList.OrderByDescending(x => x.EstateDocPageNo);
                                break;
                            case nameof(EstateInquiryGridItem.EstateDocSeriDaftarSSAACode):
                                resultItemList = resultItemList.OrderByDescending(x => x.EstateDocSeriDaftarSSAACode);
                                break;
                            case nameof(EstateInquiryGridItem.EstateDocSeriDaftarTitle):
                                resultItemList = resultItemList.OrderByDescending(x => x.EstateDocSeriDaftarTitle);
                                break;
                            case nameof(EstateInquiryGridItem.EstateSectionSSAACode):
                                resultItemList = resultItemList.OrderByDescending(x => x.EstateSectionSSAACode);
                                break;
                            case nameof(EstateInquiryGridItem.EstateSectionTitle):
                                resultItemList = resultItemList.OrderByDescending(x => x.EstateSectionTitle);
                                break;
                            case nameof(EstateInquiryGridItem.EstateSubSectionSSAACode):
                                resultItemList = resultItemList.OrderByDescending(x => x.EstateSubSectionSSAACode);
                                break;
                            case nameof(EstateInquiryGridItem.EstateSubSectionTitle):
                                resultItemList = resultItemList.OrderByDescending(x => x.EstateSubSectionTitle);
                                break;
                            case nameof(EstateInquiryGridItem.InquiryResponseDate):
                                resultItemList = resultItemList.OrderByDescending(x => x.InquiryResponseDate);
                                break;
                            case nameof(EstateInquiryGridItem.InsertTime):
                                resultItemList = resultItemList.OrderByDescending(x => x.InsertTime);
                                break;
                            case nameof(EstateInquiryGridItem.LastEditReceiveTime):
                                resultItemList = resultItemList.OrderByDescending(x => x.LastEditReceiveTime);
                                break;
                            case nameof(EstateInquiryGridItem.LastSendTime):
                                resultItemList = resultItemList.OrderByDescending(x => x.LastSendTime);
                                break;

                        }
                    }
                }
                result.TotalCount = resultItemList.Count();
                result.GridItems = resultItemList
                .Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                            .ToList();
            }
            if (result.SelectedItems != null && result.SelectedItems.Count > 0)
            {               
                foreach (EstateInquiryGridItem obj in result.SelectedItems)
                {
                    var unit = unitList.UnitList.Where(u => u.Id == obj.InquiryUnitName).FirstOrDefault();
                    if (unit != null)
                        obj.InquiryUnitName = unit.Name;
                    else
                        obj.InquiryUnitName = "";
                    if (result.GridItems.Count > 0)
                    {
                        var estateInquiryGridItem = result.GridItems.Where(x => x.InquiryId == obj.InquiryId).FirstOrDefault();
                        if (estateInquiryGridItem != null)
                        {
                            estateInquiryGridItem.IsSelected = true;
                        }
                    }
                }
            }
            Helper.NormalizeStringValuesDeeply(result, false);
            EstateInquiryGrid2 r = new EstateInquiryGrid2() { TotalCount = result.TotalCount };
            foreach (var item in result.GridItems)
            {
                var item1 = item.Adapt<EstateInquiryGridItem2>();
                item1.InquiryType = "ملکی";
                item1.ScriptoriumTitle = scriptorium.ScriptoriumList[0].Name;
                item1.ScriptoriumId = scriptorium.ScriptoriumList[0].Id;
                r.GridItems.Add(item1);
            }
            foreach (var item in result.SelectedItems)
            {
                var item1 = item.Adapt<EstateInquiryGridItem2>();
                item1.InquiryType = "ملکی";
                item1.ScriptoriumTitle = scriptorium.ScriptoriumList[0].Name;
                item1.ScriptoriumId = scriptorium.ScriptoriumList[0].Id;
                r.SelectedItems.Add(item1);
            }
            return new ApiResult<EstateInquiryGrid2>(true, ApiResultStatusCode.Success, r, new List<string> { SystemMessagesConstant.Operation_Successful });
        }
    }
}
