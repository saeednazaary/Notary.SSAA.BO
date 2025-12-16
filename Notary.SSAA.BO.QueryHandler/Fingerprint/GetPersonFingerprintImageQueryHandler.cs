using MediatR;
using Notary.SSAA.BO.DataTransferObject.Queries.Fingerprint;
using Notary.SSAA.BO.DataTransferObject.Validators.Fingerprint;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Fingerprint;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.QueryHandler.Fingerprint
{
    public class GetPersonFingerprintImageQueryHandler : BaseQueryHandler<GetPersonFingerprintImageQuery, ApiResult<List<GetPersonFingerprintImageRepositoryObject>>>
    {
        private readonly IPersonFingerprintRepository _personFingerprintRepository;
        private ApiResult<List<GetPersonFingerprintImageRepositoryObject>> apiResult;
        public GetPersonFingerprintImageQueryHandler(IMediator mediator, IUserService userService,
           IPersonFingerprintRepository personFingerprintRepository) : base(mediator, userService)
        {
            _personFingerprintRepository = personFingerprintRepository;
            apiResult = new();
        }

        protected override async Task<ApiResult<List<GetPersonFingerprintImageRepositoryObject>>> RunAsync(GetPersonFingerprintImageQuery request, CancellationToken cancellationToken)
        {
            List<GetPersonFingerprintImageRepositoryObject> inquiryFingerprintResult = new();
            GetPersonFingerprintImageValidator validationRules = new();
            var validateResult = await validationRules.ValidateAsync(request, cancellationToken);
            if (validateResult.IsValid)
            {


                if (request.ValidateAllPeople)
                {
                    inquiryFingerprintResult = await _personFingerprintRepository.GetPersonFingerprintImage(request.MainObjectId, cancellationToken);

                }
                else
                {
                    inquiryFingerprintResult = await _personFingerprintRepository.GetPersonFingerprintImage(request.PersonObjectIds, request.MainObjectId, cancellationToken);

                }
            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.message.AddRange(validateResult.Errors.Select(x => x.ErrorMessage).ToList());
            }
            apiResult.Data = inquiryFingerprintResult;


            return apiResult;
        }

        protected override bool HasAccess(GetPersonFingerprintImageQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis) || userRoles.Contains(RoleConstants.Admin);
        }
    }
}
