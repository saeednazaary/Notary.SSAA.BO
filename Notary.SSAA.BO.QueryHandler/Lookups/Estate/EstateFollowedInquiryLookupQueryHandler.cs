using MediatR;
using Notary.SSAA.BO.Coordinator.Estate.Helpers;
using Notary.SSAA.BO.DataTransferObject.Queries.Lookups.Estate;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Lookups.Estate;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.QueryHandler.Lookups.Estate
{
    public class EstateFollowedInquiryLookupQueryHandler : BaseQueryHandler<EstateFollowedInquiryLookupQuery, ApiResult<EstateFollowedInquiryLookupViewModel>>
    {
        private readonly IEstateInquiryRepository _estateInquiryRepository;
        private readonly BaseInfoServiceHelper _baseInfoServiceHelper;
        public EstateFollowedInquiryLookupQueryHandler(IMediator mediator, IUserService userService,
            IEstateInquiryRepository estateInquiryRepository)
            : base(mediator, userService)
        {
            _estateInquiryRepository = estateInquiryRepository ?? throw new ArgumentNullException(nameof(estateInquiryRepository));
            _baseInfoServiceHelper = new BaseInfoServiceHelper(mediator);
        }
        protected override bool HasAccess(EstateFollowedInquiryLookupQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
        protected override async Task<ApiResult<EstateFollowedInquiryLookupViewModel>> RunAsync(EstateFollowedInquiryLookupQuery request, CancellationToken cancellationToken)
        {
            EstateFollowedInquiryLookupViewModel result = new();

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

            string[] states = new string[]
            {
                EstateConstant.EstateInquiryStates.Sended,
                EstateConstant.EstateInquiryStates.EditAndReSend,
                EstateConstant.EstateInquiryStates.NeedCorrection,
                EstateConstant.EstateInquiryStates.NeedDocument,
                EstateConstant.EstateInquiryStates.ConfirmResponse,
                EstateConstant.EstateInquiryStates.Archived,
                EstateConstant.EstateInquiryStates.RejectResponse
            };
            var resultTemp = await _estateInquiryRepository.GetEstateInquiryGridItems(request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch, gridSortInput, request.SelectedItems, FieldsNotInFilterSearch,
                _userService.UserApplicationContext.BranchAccess.BranchCode, isOrderBy, states, null, cancellationToken);
            result.SelectedItems = resultTemp.SelectedItems;
            result.GridItems = resultTemp.GridItems;
            result.TotalCount = resultTemp.TotalCount;
            foreach (var item in result.GridItems)
            {
                if (request.SelectedItems.Contains(item.InquiryId))
                    item.IsSelected = true;
            }
            var lst = new List<string>();
            lst.AddRange(result.GridItems.Select(x => x.InquiryUnitName));
            if (result.SelectedItems != null && result.SelectedItems.Count > 0)
                lst.AddRange(result.SelectedItems.Select(x => x.InquiryUnitName));
            var unitInfo = await _baseInfoServiceHelper.GetUnitById(lst.ToArray(), cancellationToken);

            if (result.GridItems != null && result.GridItems.Count > 0)
            {                
                foreach (EstateInquiryGridItem obj in result.GridItems)
                {
                    obj.RelatedServer = "BO";
                    var unit = unitInfo.UnitList.Where(u => u.Id == obj.InquiryUnitName).FirstOrDefault();
                    if (unit != null)
                        obj.InquiryUnitName = unit.Name;
                    else
                        obj.InquiryUnitName = "";
                }
            }
            if (result.SelectedItems != null && result.SelectedItems.Count > 0)
            {                
                foreach (EstateInquiryGridItem obj in result.SelectedItems)
                {
                    obj.RelatedServer = "BO";
                    var unit = unitInfo.UnitList.Where(u => u.Id == obj.InquiryUnitName).FirstOrDefault();
                    if (unit != null)
                        obj.InquiryUnitName = unit.Name;
                    else
                        obj.InquiryUnitName = "";
                }
            }
            return new ApiResult<EstateFollowedInquiryLookupViewModel>(true, ApiResultStatusCode.Success, result, new List<string> { SystemMessagesConstant.Operation_Successful });
        }        
    }
}
