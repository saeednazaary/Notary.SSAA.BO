using Mapster;
using MediatR;
using Notary.SSAA.BO.DataTransferObject.Queries.Grids.Estate;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Grids;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.QueryHandler.Grids
{
    internal class ElzamArtSixQueryHandler : BaseQueryHandler<ElzamArtSixGridQuery, ApiResult<ElzamArtSixGridViewModel>>
    {
        private readonly IElzamArtSixRepository _ElzamArtSixRepository;

        public ElzamArtSixQueryHandler(IMediator mediator, IUserService userService, IElzamArtSixRepository ElzamArtSixRepository) : base(mediator, userService)
        {
            _ElzamArtSixRepository = ElzamArtSixRepository ?? throw new ArgumentNullException(nameof(ElzamArtSixRepository));
        }

        protected override bool HasAccess(ElzamArtSixGridQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Admin) || userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<ElzamArtSixGridViewModel>> RunAsync(ElzamArtSixGridQuery request, CancellationToken cancellationToken)
        {
            bool isOrderBy = false;
            SortData gridSortInput = new();

            List<string> FieldsNotInFilterSearch = new()
            {
                "IsSelected".ToLower(),
                "Attachments".ToLower(),
                "Type".ToLower(),
                "EstateArea".ToLower(),
                "Id".ToLower(),
                "WorkflowStatesId".ToLower(),
                "EstateUsingId".ToLower(),
                "EstateTypeId".ToLower(),
                "ScriptoriumId".ToLower(),
                "EstateUnitId".ToLower(),
                "EstateSectionId".ToLower(),
                "ProvinceId".ToLower(),
                "CountyId".ToLower(),
                "Ilm".ToLower(),
                "EstateMap".ToLower(),
                "EstateSubsectionId".ToLower(),
                "ResponseDate".ToLower(),
                "ResponseTime".ToLower(),
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
                gridSortInput.Sort = "No";
                gridSortInput.SortType = "desc";
                isOrderBy = true;
            }
            ElzamArtSixGrid databseResult = await _ElzamArtSixRepository.GetElzamArtSixGridItems(request.PageIndex, request.PageSize,
            request.GridFilterInput, request.GlobalSearch, gridSortInput, request.SelectedItems, FieldsNotInFilterSearch , _userService.UserApplicationContext.BranchAccess.BranchCode
           , request.ExtraParams, cancellationToken, isOrderBy);
            var result = databseResult.Adapt<ElzamArtSixGridViewModel>();

            foreach (var item in result.GridItems)
            {
                if (request.SelectedItems.Contains(item.Id))
                {
                    item.IsSelected = true;
                }
            }

            foreach (var item in result.SelectedItems)
            {
                if (request.SelectedItems.Contains(item.Id))
                {
                    item.IsSelected = true;
                }
            }

            return new ApiResult<ElzamArtSixGridViewModel>(true, ApiResultStatusCode.Success, result, new List<string> { SystemMessagesConstant.Operation_Successful });
        }
    }
}