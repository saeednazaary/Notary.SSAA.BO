using MediatR;
using Notary.SSAA.BO.DataTransferObject.Mappers.OtherPayment;
using Notary.SSAA.BO.DataTransferObject.Queries.Grids;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Grids.Estate;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.QueryHandler.Grids
{
    public class OtherPaymentGridQueryHandler : BaseQueryHandler<OtherPaymentGridQuery, ApiResult<OtherPaymentGridViewModel>>
    {
        private readonly IOtherPaymentRepository _otherPaymentRepository;
        private ApiResult<OtherPaymentGridViewModel> _apiResult;

        public OtherPaymentGridQueryHandler(IMediator mediator, IUserService userService, IOtherPaymentRepository otherPaymentRepository) : base(mediator, userService)
        {
            _otherPaymentRepository = otherPaymentRepository ?? throw new ArgumentNullException(nameof(otherPaymentRepository));
            _apiResult = new ApiResult<OtherPaymentGridViewModel>();
        }

        protected override bool HasAccess(OtherPaymentGridQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<OtherPaymentGridViewModel>> RunAsync(OtherPaymentGridQuery request, CancellationToken cancellationToken)
        {
            List<SortData> gridSortInputDefaults = null;
            SortData gridSortInput = null;
            List<string> FieldsNotInFilterSearch = new()
            {
                "IsSelected".ToLower(),
                "Id".ToLower(),
                "OtherPaymentsTypeId".ToLower(),
                "CreateTime".ToLower()
            };
            if (request.GridSortInput.Any())
            {
                gridSortInput = new SortData();
                gridSortInput.Sort = request.GridSortInput.Last().Sort;
                gridSortInput.SortType = request.GridSortInput.Last().SortType;
            }
            else
            {
                gridSortInputDefaults = new List<SortData>();
                SortData gridSortInputDate = new SortData();
                SortData gridSortInputTime = new SortData();
                gridSortInputDate.Sort = "createdate";
                gridSortInputDate.SortType = "desc";
                gridSortInputTime.Sort = "createtime";
                gridSortInputTime.SortType = "desc";
                gridSortInputDefaults.Add(gridSortInputDate);
                gridSortInputDefaults.Add(gridSortInputTime);
            }
            var databseresult = await _otherPaymentRepository.GetOtherPaymentGridItems(request.PageIndex, request.PageSize, request.GridFilterInput, gridSortInputDefaults, gridSortInput, request.GlobalSearch, request.SelectedItems.Select(Guid.Parse).ToList(), FieldsNotInFilterSearch, _userService.UserApplicationContext.BranchAccess.BranchCode, cancellationToken, true);
            foreach (var item in databseresult.GridItems)
            {
                if (request.SelectedItems.Contains(item.Id))
                    item.IsSelected = true;
            }
            _apiResult.Data = OtherPaymentMapper.ToViewModel(databseresult);
            return _apiResult;
        }
    }
}
