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
    public class GetInquiryPersonFingerprintListQueryHandler : BaseQueryHandler<GetInquiryPersonFingerprintListQuery, ApiResult<List<GetInquiryPersonFingerprintRepositoryObject>>>
    {
        private readonly IPersonFingerprintRepository _personFingerprintRepository;
        private ApiResult<List<GetInquiryPersonFingerprintRepositoryObject>> apiResult;
        public GetInquiryPersonFingerprintListQueryHandler(IMediator mediator, IUserService userService,
           IPersonFingerprintRepository personFingerprintRepository) : base(mediator, userService)
        {
            _personFingerprintRepository = personFingerprintRepository;
            apiResult = new();
        }
        protected override async Task<ApiResult<List<GetInquiryPersonFingerprintRepositoryObject>>> RunAsync(GetInquiryPersonFingerprintListQuery request, CancellationToken cancellationToken)
        {
            List<GetInquiryPersonFingerprintRepositoryObject> inquiryFingerprintResult = new();
            GetInquiryPersonFingerprintListValidator validationRules = new GetInquiryPersonFingerprintListValidator();
            var validateResult = await validationRules.ValidateAsync(request, cancellationToken);
            if (validateResult.IsValid)
            {
                inquiryFingerprintResult = await _personFingerprintRepository.GetInquiryPersonFingerprint(request.PersonNationalNos, request.MainObjectId, cancellationToken);
                apiResult.Data = inquiryFingerprintResult;

            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.message.AddRange(validateResult.Errors.Select(x => x.ErrorMessage).ToList());
            }


            return apiResult;
        }

        protected override bool HasAccess(GetInquiryPersonFingerprintListQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
    }
}
