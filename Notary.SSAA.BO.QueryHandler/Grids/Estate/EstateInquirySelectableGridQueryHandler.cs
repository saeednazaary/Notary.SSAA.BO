using MediatR;
using Notary.SSAA.BO.Coordinator.Estate.Helpers;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.BaseInfoService;
using Notary.SSAA.BO.DataTransferObject.Queries.Grids.Estate;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.BaseInfoService;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Grids.Estate;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.QueryHandler.Grids.Estate
{
    public class EstateInquirySelectableGridQueryHandler : BaseQueryHandler<EstateInquirySelectableGridQuery, ApiResult<EstateInquirySelectableGridViewModel>>
    {
        private readonly IEstateInquiryRepository _estateInquiryRepository;
        private readonly BaseInfoServiceHelper _baseInfoServiceHelper;

        public EstateInquirySelectableGridQueryHandler(IMediator mediator, IUserService userService,
            IEstateInquiryRepository selectableGridRepository)
            : base(mediator, userService)
        {
            _estateInquiryRepository = selectableGridRepository ?? throw new ArgumentNullException(nameof(selectableGridRepository));
            _baseInfoServiceHelper = new BaseInfoServiceHelper(mediator);
        }
        protected override bool HasAccess(EstateInquirySelectableGridQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
        protected override async Task<ApiResult<EstateInquirySelectableGridViewModel>> RunAsync(EstateInquirySelectableGridQuery request, CancellationToken cancellationToken)
        {
            try
            {
                EstateInquirySelectableGridViewModel result = new();
                IDictionary<string, string> searchMap = new Dictionary<string, string>();
                bool isOrderBy = false;
                SortData gridSortInput = new();

                List<string> FieldsNotInFilterSearch = new()
            {
                "IsSelected".ToLower(),
                "InquiryId".ToLower(),
                "InquiryUnitName".ToLower(),
                "OwnerName".ToLower(),
                "OwnerFamily".ToLower(),
                "Status".ToLower(),
                "StatusTitle".ToLower()
            };
                if (request.GridSortInput.Count > 0)
                {
                    isOrderBy = true;
                }
                foreach (var item in request.GridSortInput)
                {
                    gridSortInput.Sort = item.Sort;
                    gridSortInput.SortType = item.SortType;
                }
                if (request.GridSortInput.Count == 0)
                {
                    gridSortInput.Sort = "InquiryDate";
                    gridSortInput.SortType = "desc";
                    isOrderBy = true;
                }
                if (request.ExtraParams.InqStatus == "0" || request.ExtraParams.InqStatus == EstateConstant.EstateInquiryStates.NotSended)
                    request.ExtraParams.InqStatus = "----";
                var dic = ToDictionary(request.ExtraParams);
                var resultTemp = await _estateInquiryRepository.GetEstateInquiryGridItems(request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch, gridSortInput, request.SelectedItems, FieldsNotInFilterSearch,
                    _userService.UserApplicationContext.BranchAccess.BranchCode, isOrderBy, null, dic, cancellationToken);
                result.SelectedItems = resultTemp.SelectedItems;
                result.GridItems = resultTemp.GridItems;
                result.TotalCount = resultTemp.TotalCount;
                foreach (var item in result.GridItems)
                {
                    item.RelatedServer = "BO";
                    if (request.SelectedItems.Contains(item.InquiryId))
                        item.IsSelected = true;
                }
                if (result.GridItems != null && result.GridItems.Count > 0)
                {
                    var unitInfo = await _baseInfoServiceHelper.GetUnitById(result.GridItems.Select(x => x.InquiryUnitName).ToArray(), cancellationToken);
                    var scriptoriumInfo = await _baseInfoServiceHelper.GetScriptoriumById(result.GridItems.Select(x => x.ScriptoriumId).ToArray(), cancellationToken);
                    foreach (EstateInquiryGridItem obj in result.GridItems)
                    {
                        obj.IsFollowable = false;
                        var unit = unitInfo.UnitList.Where(u => u.Id == obj.InquiryUnitName).FirstOrDefault();
                        if (unit != null)
                        {
                            obj.InquiryUnitName = unit.Name;
                            obj.UnitGeolocationId = unit.GeoLocationId;
                        }
                        else
                        {
                            obj.InquiryUnitName = "";
                            obj.UnitGeolocationId = "";
                        }
                        var scriptorium = scriptoriumInfo.ScriptoriumList.Where(u => u.Id == obj.ScriptoriumId).FirstOrDefault();
                        if (scriptorium != null)
                        {
                            obj.ScriptoriumGeolocationId = scriptorium.GeoLocationId;
                        }
                        else
                        {
                            obj.ScriptoriumGeolocationId = "";
                        }
                        if (!string.IsNullOrWhiteSpace(obj.ResponseResult) && obj.ResponseResult.Equals("true", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrWhiteSpace(obj.InquiryResponseDate) && (obj.Status == "3" || obj.Status == "4"))
                        {
                            var responseDateTime = obj.InquiryResponseDate.ToDateTime();
                            if (obj.ScriptoriumGeolocationId == obj.UnitGeolocationId)
                            {
                                if (responseDateTime.HasValue && DateTime.Now.Subtract(responseDateTime.Value).TotalDays < 37)
                                {
                                    obj.IsFollowable = true;
                                }
                            }
                            else
                            {
                                if (responseDateTime.HasValue && DateTime.Now.Subtract(responseDateTime.Value).TotalDays < 50)
                                {
                                    obj.IsFollowable = true;
                                }
                            }
                        }
                    }
                }
                if (result.SelectedItems != null && result.SelectedItems.Count > 0)
                {
                    var unitInfo = await _baseInfoServiceHelper.GetUnitById(result.SelectedItems.Select(x => x.InquiryUnitName).ToArray(), cancellationToken);
                    var scriptoriumInfo = await _baseInfoServiceHelper.GetScriptoriumById(result.SelectedItems.Select(x => x.ScriptoriumId).ToArray(), cancellationToken);
                    foreach (EstateInquiryGridItem obj in result.SelectedItems)
                    {
                        obj.IsFollowable = false;
                        obj.RelatedServer = "BO";
                        var unit = unitInfo.UnitList.Where(u => u.Id == obj.InquiryUnitName).FirstOrDefault();
                        if (unit != null)
                        {
                            obj.InquiryUnitName = unit.Name;
                            obj.UnitGeolocationId = unit.GeoLocationId;
                        }
                        else
                        {
                            obj.InquiryUnitName = "";
                            obj.UnitGeolocationId = "";
                        }
                        var scriptorium = scriptoriumInfo.ScriptoriumList.Where(u => u.Id == obj.ScriptoriumId).FirstOrDefault();
                        if (scriptorium != null)
                        {
                            obj.ScriptoriumGeolocationId = scriptorium.GeoLocationId;
                        }
                        else
                        {
                            obj.ScriptoriumGeolocationId = "";
                        }
                        if (!string.IsNullOrWhiteSpace(obj.ResponseResult) && obj.ResponseResult.Equals("true", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrWhiteSpace(obj.InquiryResponseDate) && (obj.Status == "3" || obj.Status == "4"))
                        {
                            var responseDateTime = obj.InquiryResponseDate.ToDateTime();
                            if (obj.ScriptoriumGeolocationId == obj.UnitGeolocationId)
                            {
                                if (responseDateTime.HasValue && DateTime.Now.Subtract(responseDateTime.Value).TotalDays < 37)
                                {
                                    obj.IsFollowable = true;
                                }
                            }
                            else
                            {
                                if (responseDateTime.HasValue && DateTime.Now.Subtract(responseDateTime.Value).TotalDays < 50)
                                {
                                    obj.IsFollowable = true;
                                }
                            }
                        }
                    }
                }

                return new ApiResult<EstateInquirySelectableGridViewModel>(true, ApiResultStatusCode.Success, result, new List<string> { SystemMessagesConstant.Operation_Successful });
            }
            catch (Exception ex)
            {
                var exp = ex;
                while (exp.InnerException != null)
                    exp = exp.InnerException;
                return new ApiResult<EstateInquirySelectableGridViewModel>(true, ApiResultStatusCode.Success, null, new List<string> { exp.Message });
            }
        }

        private static Dictionary<string, object> ToDictionary(object obj)
        {
            Dictionary<string, object> r = new();
            var type = obj.GetType();
            var piLst = type.GetProperties();
            foreach (var pi in piLst)
            {
                var val = pi.GetValue(obj, null);
                if (val != null)
                {
                    r.Add(pi.Name, val);
                }
            }
            return r;
        }        
    }
}
