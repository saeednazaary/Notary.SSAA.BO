using MediatR;
using Serilog;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.Fingerprint;
using Notary.SSAA.BO.DataTransferObject.Mappers.Fingerprint;
using Notary.SSAA.BO.DataTransferObject.Queries.Fingerprint;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Fingerprint;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.CommandHandler.Fingerprint.Handlers
{
    internal class CreatePersonFingerprintCommandHandler : BaseCommandHandler<CreatePersonFingerprintCommand, ApiResult>
    {
        private Domain.Entities.PersonFingerprint masterEntity;
        private readonly IPersonFingerprintRepository _personFingerprintRepository;
        private ApiResult<GetInquiryPersonFingerprintRepositoryObject> apiResult;
        private readonly IDateTimeService _dateTimeService;

        public CreatePersonFingerprintCommandHandler(IMediator mediator, IDateTimeService dateTimeService, IPersonFingerprintRepository personFingerprintRepository,
            IUserService userService, ILogger logger) : base(mediator, userService, logger)
        {
            _personFingerprintRepository = personFingerprintRepository ?? throw new ArgumentNullException(nameof(personFingerprintRepository));
            apiResult = new ApiResult<GetInquiryPersonFingerprintRepositoryObject>();
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
        }

        protected override async Task<ApiResult> ExecuteAsync(CreatePersonFingerprintCommand request, CancellationToken cancellationToken)
        {
            GetInquiryPersonFingerprintRepositoryObject viewModel = new();
            GetInquiryPersonFingerprintQuery inquiryFingerprintQuery = new(request.MainObjectId, request.PersonNationalNo);
            var inquiry = await _mediator.Send(inquiryFingerprintQuery, cancellationToken);


            if (inquiry.IsSuccess)
            {
                viewModel = inquiry.Data;
            }
            else
            {
                masterEntity = new()
                {
                    Id = Guid.NewGuid(),
                    IsDeleted = "2",
                    MocIsRequired = "2",
                    TfaState = "1",
                    Ilm = "1",
                    RecordDate = _dateTimeService.CurrentDateTime,
                    OrganizationId = _userService.UserApplicationContext.BranchAccess.BranchCode,
                    UseCaseMainObjectId = request.MainObjectId,
                    State = "1"
                };
                PersonFingerprintMapper.ToEntity(ref masterEntity, request);
                await _personFingerprintRepository.AddAsync(masterEntity, cancellationToken);
                viewModel = PersonFingerprintMapper.ToViewModel(masterEntity);
            }
            apiResult.Data = viewModel;

            return apiResult;
        }

        protected override bool HasAccess(CreatePersonFingerprintCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
    }
}
