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
    public class SignRequestGetterLookupQueryHandler : BaseQueryHandler<SignRequestGetterLookupQuery, ApiResult<BaseLookupRepositoryObject>>
    {
        private readonly ISignRequestGetterRepository _signRequestGetterRepository;


        public SignRequestGetterLookupQueryHandler(IMediator mediator, IUserService userService,
            ISignRequestGetterRepository signRequestGetterRepository)
            : base(mediator, userService)
        {
            _signRequestGetterRepository = signRequestGetterRepository ?? throw new ArgumentNullException(nameof(signRequestGetterRepository));


        }

        protected override bool HasAccess(SignRequestGetterLookupQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis) || userRoles.Contains(RoleConstants.SSAAAdmin);
        }

        protected override async Task<ApiResult<BaseLookupRepositoryObject>> RunAsync(SignRequestGetterLookupQuery request, CancellationToken cancellationToken)
        {
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
            BaseLookupRepositoryObject result = await _signRequestGetterRepository.GetSignRequestGetterLookupItems(request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch, gridSortInput, request.SelectedItems, FieldsNotInFilterSearch, cancellationToken, isOrderBy);

            return new ApiResult<BaseLookupRepositoryObject>(true, ApiResultStatusCode.Success, result, new List<string> { SystemMessagesConstant.Operation_Successful });
        }

    }

}
