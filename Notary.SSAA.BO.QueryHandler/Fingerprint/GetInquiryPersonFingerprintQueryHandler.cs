using MediatR;
using Notary.SSAA.BO.DataTransferObject.Queries.Fingerprint;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Fingerprint;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.QueryHandler.Fingerprint
{
    public class GetInquiryPersonFingerprintQueryHandler : BaseQueryHandler<GetInquiryPersonFingerprintQuery, ApiResult<GetInquiryPersonFingerprintRepositoryObject>>
    {
        private PersonFingerprint masterEntity;
        private readonly IPersonFingerprintRepository _personFingerprintRepository;
        private readonly IDateTimeService _dateTimeService;
        private ApiResult<GetInquiryPersonFingerprintRepositoryObject> apiResult;
        public GetInquiryPersonFingerprintQueryHandler(IMediator mediator, IUserService userService,
            IPersonFingerprintRepository personFingerprintRepository, IDateTimeService dateTimeService) : base(mediator, userService)
        {
            _personFingerprintRepository = personFingerprintRepository;
            _dateTimeService = dateTimeService;
            apiResult = new();
        }

        protected override async Task<ApiResult<GetInquiryPersonFingerprintRepositoryObject>> RunAsync(GetInquiryPersonFingerprintQuery request, CancellationToken cancellationToken)
        {
            GetInquiryPersonFingerprintRepositoryObject inquiryFingerprintResult = new();

            if (request.ValidateById)
            {
                inquiryFingerprintResult = await _personFingerprintRepository.GetInquiryPersonFingerprint(request.FingerprintId, cancellationToken);

            }else
            {
                inquiryFingerprintResult = await _personFingerprintRepository.GetInquiryPersonFingerprint(request.NationalNo,request.MainObjectId, cancellationToken);

            }
            if (inquiryFingerprintResult is not null)
            {
                apiResult.Data = inquiryFingerprintResult;

            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.statusCode = ApiResultStatusCode.NotFound;
                apiResult.message.Add("اثرانگشت مربوطه یافت نشد . ");

            }
            return apiResult;
        }

        protected override bool HasAccess(GetInquiryPersonFingerprintQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
    }
}
