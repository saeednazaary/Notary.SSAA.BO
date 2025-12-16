using MediatR;
using Notary.SSAA.BO.Coordinator.Estate.Helpers;
using Notary.SSAA.BO.DataTransferObject.Queries.Grids.Estate;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Grids.Estate;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.QueryHandler.Grids.Estate
{
    public class DealSummarySelectableGridQueryHandler : BaseQueryHandler<DealSummarySelectableGridQuery, ApiResult<DealSummarySelectableGridViewModel>>
    {
        private readonly IDealSummaryRepository _dealSummaryRepository;
        private readonly BaseInfoServiceHelper _baseInfoServiceHelper;
        public DealSummarySelectableGridQueryHandler(IMediator mediator, IUserService userService,
            IDealSummaryRepository selectableGridRepository)
            : base(mediator, userService)
        {
            _dealSummaryRepository = selectableGridRepository ?? throw new ArgumentNullException(nameof(selectableGridRepository));
            _baseInfoServiceHelper = new BaseInfoServiceHelper(mediator);
        }
        protected override bool HasAccess(DealSummarySelectableGridQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
        protected override async Task<ApiResult<DealSummarySelectableGridViewModel>> RunAsync(DealSummarySelectableGridQuery request, CancellationToken cancellationToken)
        {
            DealSummarySelectableGridViewModel result = new();
            IDictionary<string, string> searchMap = new Dictionary<string, string>();
            bool isOrderBy = false;
            SortData gridSortInput = new();

            List<string> FieldsNotInFilterSearch = new()
            {
                "IsSelected".ToLower(),
                "DealSummaryId".ToLower(),
                "DealSummaryUnitName".ToLower(),               
                "Status".ToLower(),                
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
                gridSortInput.Sort = "DealSummaryDate";
                gridSortInput.SortType = "desc";
                isOrderBy = true;
            }
            if (request.ExtraParams.DS_Status == "0" || request.ExtraParams.DS_Status == EstateConstant.DealSummaryStates.NotSended)
                request.ExtraParams.DS_Status = "----";
            var dic = ToDictionary(request.ExtraParams);
            var resultTemp = await _dealSummaryRepository.GetDealSummaryGridItems(request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch, gridSortInput, request.SelectedItems, FieldsNotInFilterSearch,
                _userService.UserApplicationContext.BranchAccess.BranchCode, isOrderBy, null, dic, cancellationToken);
            result.SelectedItems = resultTemp.SelectedItems;
            result.GridItems = resultTemp.GridItems;
            result.TotalCount = resultTemp.TotalCount;
            foreach (var item in result.GridItems)
            {
                item.RelatedServer = "BO";
                if (request.SelectedItems.Contains(item.DealSummaryId))
                    item.IsSelected = true;
            }
            if (result.GridItems != null && result.GridItems.Count > 0)
            {
                var unitInfo = await _baseInfoServiceHelper.GetUnitById(result.GridItems.Select(x => x.DealSummaryUnitName).ToArray(), cancellationToken);
                foreach (DealSummaryGridItem obj in result.GridItems)
                {                   
                    var unit = unitInfo.UnitList.Where(u => u.Id == obj.DealSummaryUnitName).FirstOrDefault();
                    if (unit != null)
                        obj.DealSummaryUnitName = unit.Name;
                    else
                        obj.DealSummaryUnitName = "";
                }
            }
            if (result.SelectedItems != null && result.SelectedItems.Count > 0)
            {
                var unitInfo = await _baseInfoServiceHelper.GetUnitById(result.SelectedItems.Select(x => x.DealSummaryUnitName).ToArray(), cancellationToken);
                foreach (DealSummaryGridItem obj in result.SelectedItems)
                {
                    obj.RelatedServer = "BO";
                    var unit = unitInfo.UnitList.Where(u => u.Id == obj.DealSummaryUnitName).FirstOrDefault();
                    if (unit != null)
                        obj.DealSummaryUnitName = unit.Name;
                    else
                        obj.DealSummaryUnitName = "";
                }
            }
            return new ApiResult<DealSummarySelectableGridViewModel>(true, ApiResultStatusCode.Success, result, new List<string> { SystemMessagesConstant.Operation_Successful });
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
