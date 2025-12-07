using MediatR;
using Notary.SSAA.BO.DataTransferObject.Queries.Fingerprint;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Fingerprint;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.QueryHandler.Fingerprint
{
    internal class GetPersonFingerprintTypeQueryHandler : BaseQueryHandler<GetPersonFingerTypeQuery, ApiResult<List<GetPersonFingerType>>>
    {
        private readonly IPersonFingerTypeRepository _personFingerTypeRepository;

        public GetPersonFingerprintTypeQueryHandler(IMediator mediator, IUserService userService, IPersonFingerTypeRepository personFingerTypeRepository) : base(mediator, userService)
        {
            _personFingerTypeRepository = personFingerTypeRepository;
        }

        protected override bool HasAccess(GetPersonFingerTypeQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);

        }

        protected override async Task<ApiResult<List<GetPersonFingerType>>> RunAsync(GetPersonFingerTypeQuery request, CancellationToken cancellationToken)
        {
            ApiResult<List<GetPersonFingerType>> apiResult = new();
            List<GetPersonFingerType> viewModel = await _personFingerTypeRepository.GetAllPersonFingerTypes(cancellationToken);
            apiResult.Data = viewModel;
            return apiResult;
        }
    }
}



