using MediatR;
using Notary.SSAA.BO.DataTransferObject.Queries.Lookups;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.SignRequest;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.QueryHandler.Lookups
{
    public class SignRequestSubjectLookupQueryHandler : BaseQueryHandler<SignRequestSubjectLookupQuery, ApiResult<SignRequestSubjectLookupRepositoryObject>>
    {
        private readonly ISignRequestSubjectRepository _signRequestSubjectRepository;
        public SignRequestSubjectLookupQueryHandler(IMediator mediator, IUserService userService,
            ISignRequestSubjectRepository signRequestSubjectRepository)
            : base(mediator, userService)
        {
            _signRequestSubjectRepository = signRequestSubjectRepository ?? throw new ArgumentNullException(nameof(signRequestSubjectRepository));


        }

        protected override bool HasAccess(SignRequestSubjectLookupQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis) || userRoles.Contains(RoleConstants.SSAAAdmin);
        }

        protected override async Task<ApiResult<SignRequestSubjectLookupRepositoryObject>> RunAsync(SignRequestSubjectLookupQuery request, CancellationToken cancellationToken)
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
            SignRequestSubjectLookupRepositoryObject result = await _signRequestSubjectRepository.GetSignRequestSubjectLookupItems(request.PageIndex, request.PageSize, request.GridFilterInput, request.GlobalSearch, gridSortInput, request.SelectedItems, FieldsNotInFilterSearch, cancellationToken, isOrderBy);

            return new ApiResult<SignRequestSubjectLookupRepositoryObject>(true, ApiResultStatusCode.Success, result, new List<string> { SystemMessagesConstant.Operation_Successful });
        }


    }

}
