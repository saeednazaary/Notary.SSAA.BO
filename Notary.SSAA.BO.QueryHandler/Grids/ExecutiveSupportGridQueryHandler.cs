using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Notary.SSAA.BO.DataTransferObject.Queries.ExecutiveSupport;
using Notary.SSAA.BO.DataTransferObject.Queries.Grids;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExecutiveSupport;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Grids;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.AppSettingModel;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Contracts.HttpEndPointCaller;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.QueryHandler.Grids
{
    public class ExecutiveSupportGridQueryHandler : BaseQueryHandler<ExecutiveSupportGridQuery, ApiResult<ExecutiveSupportGridViewModel>>
    {
        private readonly IExecutiveSupportRepository _executiveSupportRepository;
        private IConfiguration _configuration;
        private IHttpEndPointCaller _httpEndPointCaller;
        public ExecutiveSupportGridQueryHandler(IMediator mediator, IUserService userService, IExecutiveSupportRepository executiveSupportRepository
            , IConfiguration configuration, IHttpEndPointCaller httpEndPointCaller) : base(mediator, userService)
        {
            _executiveSupportRepository = executiveSupportRepository ?? throw new ArgumentNullException(nameof(executiveSupportRepository));
            _configuration = configuration;
            _httpEndPointCaller = httpEndPointCaller;

        }

        protected override bool HasAccess(ExecutiveSupportGridQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.SanadNevis) || userRoles.Contains(RoleConstants.Daftaryar);
        }

        protected override async Task<ApiResult<ExecutiveSupportGridViewModel>> RunAsync(ExecutiveSupportGridQuery request, CancellationToken cancellationToken)
        {
            ExecutiveSupportGridViewModel result = new();
            bool isOrderBy = false;
            SortData gridSortInput = new();

            List<string> FieldsNotInFilterSearch = new()
            {
                "IsSelected".ToLower(),
                "Id".ToLower()
            };
            if (request.GridSortInput.Count > 0)
            {
                isOrderBy = true;
            }
            foreach (SortData item in request.GridSortInput)
            {
                gridSortInput.Sort = item.Sort;
                gridSortInput.SortType = item.SortType;
            }
            if (request.GridSortInput.Count == 0)
            {
                gridSortInput.Sort = "no";
                gridSortInput.SortType = "desc";
                isOrderBy = true;
            }

            var resultTemp = await _executiveSupportRepository.GetExecutiveSupportGridItems(request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch
                 , gridSortInput, request.SelectedItems, FieldsNotInFilterSearch, _userService.UserApplicationContext.BranchAccess.BranchCode, cancellationToken, isOrderBy);

            result.TotalCount = resultTemp.TotalCount;
            result.SelectedItems = resultTemp.SelectedItems;
            result.GridItems = resultTemp.GridItems;
            string bo_Apis_prefix = _configuration.GetSection("BO_APIS_Prefix").Get<BOApisPrefix>().BASEINFO_APIS_Prefix;
            var baseInfoUrl = _configuration.GetValue<string>("InternalGatewayUrl") + bo_Apis_prefix + "api/v1/Specific/Ssar/";
            ExecutiveDetailsByIdQuery supportiveSelectedItemsUnitQuery = new();
            ExecutiveDetailsByIdQuery supportiveGridItemsUnitQuery = new();
            ExecutiveDetailsByIdViewModel supportiveUnitItemsResult = new();

            foreach (var item in result.GridItems)
            {
                if (request.SelectedItems.Contains(item.Id))
                {
                    item.IsSelected = true;

                    if (!item.ExecutionUnitId.IsNullOrEmpty())
                        supportiveSelectedItemsUnitQuery.ObjectId.Add(item.ExecutionUnitId);
                }
                if (!item.ExecutionUnitId.IsNullOrEmpty())
                    supportiveGridItemsUnitQuery.ObjectId.Add(item.ExecutionUnitId);
            }

            if (!supportiveGridItemsUnitQuery.ObjectId.IsNullOrEmpty())
            {
                supportiveUnitItemsResult = _httpEndPointCaller.CallPostApiAsync<ExecutiveDetailsByIdViewModel, ExecutiveDetailsByIdQuery>(
                  new HttpEndpointRequest<ExecutiveDetailsByIdQuery>(baseInfoUrl + ExecutiveSupportConstant.BaseInfoServiceApis.Unit,
                  _userService.UserApplicationContext.Token, supportiveGridItemsUnitQuery), cancellationToken).Result.Data;
            }

            if (!supportiveUnitItemsResult.Items.IsNullOrEmpty())
            {
                foreach (var item in result.GridItems)
                {
                    item.ExecutionUnit = supportiveUnitItemsResult.Items.Where(s => s.Id == item.ExecutionUnitId).First()?.Title;
                }
                foreach (var item in result.SelectedItems)
                {
                    item.ExecutionUnit = supportiveUnitItemsResult.Items.Where(s => s.Id == item.ExecutionUnitId).First()?.Title;
                }
            }


            return new ApiResult<ExecutiveSupportGridViewModel>(true, ApiResultStatusCode.Success, result, new List<string> { SystemMessagesConstant.Operation_Successful });
        }
    }
}
