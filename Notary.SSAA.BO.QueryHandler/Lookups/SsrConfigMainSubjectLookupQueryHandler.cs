using MediatR;
using Notary.SSAA.BO.DataTransferObject.Queries.Lookups;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.QueryHandler.Lookups
{
    internal class SsrConfigMainSubjectLookupQueryHandler : BaseQueryHandler<SsrConfigMainSubjectLookupQuery, ApiResult<BaseLookupRepositoryObject>>
    {
        private readonly ISsrConfigMainSubjectRepository _ssrConfigMainSubjectRepository;
        public SsrConfigMainSubjectLookupQueryHandler(IMediator mediator, IUserService userService,ISsrConfigMainSubjectRepository ssrConfigMainSubjectRepository) : base(mediator, userService)
        {
            _ssrConfigMainSubjectRepository = ssrConfigMainSubjectRepository ?? throw new ArgumentNullException(nameof(ssrConfigMainSubjectRepository));

        }

        protected override bool HasAccess(SsrConfigMainSubjectLookupQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.SSAAAdmin);
        }

        protected async override Task<ApiResult<BaseLookupRepositoryObject>> RunAsync(SsrConfigMainSubjectLookupQuery request, CancellationToken cancellationToken)
        {
            bool isOrderBy = false;
            SortData gridSortInput = new();
            List<string> FieldsNotInFilterSearch = new()
            {
                "IsSelected".ToLower(),
                "Id".ToLower()
            };
            List<string> ResultFields = new()
            {
                "Id".ToLower(),
                "Title".ToLower(),
                "Code".ToLower()
            }
                ;
            isOrderBy = true;
            if (request.GridSortInput.Count > 0)
            {
                foreach (SortData item in request.GridSortInput)
                {
                    gridSortInput.Sort = item.Sort;
                    gridSortInput.SortType = item.SortType;
                }
            }
            else
            {
                gridSortInput.Sort = "code";
                gridSortInput.SortType = "asc";
            }

            BaseLookupRepositoryObject result = await _ssrConfigMainSubjectRepository.GetSsrConfigMainSubjectLookupItems(request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch, gridSortInput, request.SelectedItems, FieldsNotInFilterSearch, cancellationToken, isOrderBy);

            return new ApiResult<BaseLookupRepositoryObject>(true, ApiResultStatusCode.Success, result, new List<string> { SystemMessagesConstant.Operation_Successful });
        }
    }
}
