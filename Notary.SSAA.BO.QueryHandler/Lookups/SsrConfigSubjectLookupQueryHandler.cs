using MediatR;
using Notary.SSAA.BO.DataTransferObject.Queries.Lookups;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Lookups;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.QueryHandler.Lookups
{
    internal class SsrConfigSubjectLookupQueryHandler : BaseQueryHandler<SsrConfigSubjectLookupQuery, ApiResult<SsrConfigSubjectLookupRepositoryObject>>
    {
        private readonly ISsrConfigSubjectRepository _ssrConfigSubjectRepository;

        public SsrConfigSubjectLookupQueryHandler(IMediator mediator, IUserService userService, ISsrConfigSubjectRepository ssrConfigSubjectRepository) : base(mediator, userService)
        {
            _ssrConfigSubjectRepository = ssrConfigSubjectRepository ?? throw new ArgumentNullException(nameof(ssrConfigSubjectRepository));

        }

        protected override bool HasAccess(SsrConfigSubjectLookupQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.SSAAAdmin);
        }

        protected async override Task<ApiResult<SsrConfigSubjectLookupRepositoryObject>> RunAsync(SsrConfigSubjectLookupQuery request, CancellationToken cancellationToken)
        {
            bool isOrderBy = false;
            SortData gridSortInput = new();
            List<string> FieldsNotInFilterSearch = new()
            {
                "IsSelected".ToLower(),
                "Id".ToLower()
            };

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
                isOrderBy = false;
            }

            SsrConfigSubjectLookupRepositoryObject result = await _ssrConfigSubjectRepository.GetSsrConfigSubjectLookupItems(request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch, gridSortInput, request.SelectedItems, FieldsNotInFilterSearch, request.ExtraParams, cancellationToken, isOrderBy);

            return new ApiResult<SsrConfigSubjectLookupRepositoryObject>(true, ApiResultStatusCode.Success, result, new List<string> { SystemMessagesConstant.Operation_Successful });
        }
    }
}
