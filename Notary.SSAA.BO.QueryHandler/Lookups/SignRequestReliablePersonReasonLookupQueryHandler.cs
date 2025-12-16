using MediatR;
using Notary.SSAA.BO.DataTransferObject.Queries.Lookups;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using static Stimulsoft.Report.StiRecentConnections;

namespace Notary.SSAA.BO.QueryHandler.Lookups
{
    public class SignRequestReliablePersonReasonLookupQueryHandler : BaseQueryHandler<SignRequestReliablePersonReasonLookupQuery, ApiResult<BaseLookupRepositoryObject>>
    {
        private readonly IReliablePersonReasonRepository _signRequestReliablePersonReasonRepository;

        public SignRequestReliablePersonReasonLookupQueryHandler(IMediator mediator, IUserService userService,
            IReliablePersonReasonRepository signRequestReliablePersonReasonRepository)
            : base(mediator, userService)
        {
            _signRequestReliablePersonReasonRepository = signRequestReliablePersonReasonRepository ?? throw new ArgumentNullException(nameof(signRequestReliablePersonReasonRepository));


        }

        protected override bool HasAccess(SignRequestReliablePersonReasonLookupQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis) || userRoles.Contains(RoleConstants.SSAAAdmin);
        }

        protected override async Task<ApiResult<BaseLookupRepositoryObject>> RunAsync(SignRequestReliablePersonReasonLookupQuery request, CancellationToken cancellationToken)
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

            BaseLookupRepositoryObject result = await _signRequestReliablePersonReasonRepository.GetSignRequestReliablePersonReasonLookupItems(request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch, gridSortInput, request.SelectedItems, ResultFields, FieldsNotInFilterSearch, request.ExtraParams, cancellationToken, isOrderBy);

            return new ApiResult<BaseLookupRepositoryObject>(true, ApiResultStatusCode.Success, result, new List<string> { SystemMessagesConstant.Operation_Successful });
        }

    }

}
