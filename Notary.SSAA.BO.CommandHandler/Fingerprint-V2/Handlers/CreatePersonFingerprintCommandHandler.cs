using MediatR;
using Serilog;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.Fingerprint;
using Notary.SSAA.BO.DataTransferObject.Mappers.Fingerprint;
using Notary.SSAA.BO.DataTransferObject.Queries.Fingerprint;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Fingerprint;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using System.Runtime.CompilerServices;

namespace Notary.SSAA.BO.CommandHandler.Fingerprint_V2.Handlers
{
    internal class CreatePersonFingerprintCommandHandler : BaseCommandHandler<CreatePersonFingerprintV2Command, ApiResult>
    {
        private Domain.Entities.PersonFingerprint masterEntity;
        private readonly IPersonFingerprintRepository _personFingerprintRepository;
        private ApiResult<GetInquiryPersonFingerprintRepositoryObject> apiResult;
        private readonly IDateTimeService _dateTimeService;
        private readonly IRepository<SignRequestPerson> _signRequestPersonRepository;
        private readonly IRepository<DocumentPerson> _documentPersonRepository;
        public CreatePersonFingerprintCommandHandler(IMediator mediator, IDateTimeService dateTimeService, IPersonFingerprintRepository personFingerprintRepository,
            IUserService userService, ILogger logger, IRepository<SignRequestPerson> signRequestPersonRepository, IRepository<DocumentPerson> documentPersonRepository) : base(mediator, userService, logger)
        {
            _personFingerprintRepository = personFingerprintRepository ?? throw new ArgumentNullException(nameof(personFingerprintRepository));
            apiResult = new ApiResult<GetInquiryPersonFingerprintRepositoryObject>();
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
            _signRequestPersonRepository = signRequestPersonRepository;
            _documentPersonRepository = documentPersonRepository;
        }

        protected override async Task<ApiResult> ExecuteAsync(CreatePersonFingerprintV2Command request, CancellationToken cancellationToken)
        {
            GetInquiryPersonFingerprintRepositoryObject viewModel = new();
            GetInquiryPersonFingerprintQuery inquiryFingerprintQuery = new(request.MainObjectId, request.PersonNationalNo);
            var inquiry = await _mediator.Send(inquiryFingerprintQuery, cancellationToken);


            if (inquiry.IsSuccess)
            {
                var fingerprint = await this._personFingerprintRepository.GetByIdAsync(cancellationToken, inquiry.Data.FingerprintId.ToGuid());
                if (request.ClientId == "7")
                {                    
                    fingerprint.PersonFingerprintUseCaseId = "7";
                    fingerprint.UseCasePersonObjectId = request.PersonNationalNo;
                    if (!string.IsNullOrWhiteSpace(request.PersonMobileNo))
                        fingerprint.TfaMobileNo = request.PersonMobileNo;
                    await _personFingerprintRepository.UpdateAsync(fingerprint, cancellationToken);
                }
                else
                {
                    if (request.ClientId == "2")
                    {
                        var documentPerson = await _documentPersonRepository.GetAsync(x => x.DocumentId == request.MainObjectId.ToGuid() && x.NationalNo == request.PersonNationalNo, cancellationToken);
                        fingerprint.UseCasePersonObjectId = documentPerson.Id.ToString();
                        fingerprint.PersonFingerprintUseCaseId = "2";
                        if (!string.IsNullOrWhiteSpace(request.PersonMobileNo))
                            fingerprint.TfaMobileNo = request.PersonMobileNo;
                        fingerprint.TfaIsRequired = request.IsTFARequired ? "1" : "2";
                        await _personFingerprintRepository.UpdateAsync(fingerprint, cancellationToken);
                    }
                    else if (request.ClientId == "1")
                    {
                        var documentPerson = await _signRequestPersonRepository.GetAsync(x => x.SignRequestId == request.MainObjectId.ToGuid() && x.NationalNo == request.PersonNationalNo, cancellationToken);
                        fingerprint.UseCasePersonObjectId = documentPerson.Id.ToString();
                        fingerprint.PersonFingerprintUseCaseId = "1";
                        if (!string.IsNullOrWhiteSpace(request.PersonMobileNo))
                            fingerprint.TfaMobileNo = request.PersonMobileNo;
                        fingerprint.TfaIsRequired = request.IsTFARequired ? "1" : "2";
                        await _personFingerprintRepository.UpdateAsync(fingerprint, cancellationToken);
                    }
                }
                viewModel = inquiry.Data;
            }
            else
            {
                masterEntity = new()
                {
                    Id = Guid.NewGuid(),
                    IsDeleted = "2",
                    MocIsRequired = "2",
                    TfaState = "2",
                    Ilm = "1",
                    RecordDate = _dateTimeService.CurrentDateTime,
                    OrganizationId = _userService.UserApplicationContext.BranchAccess.BranchCode,
                    UseCaseMainObjectId = request.MainObjectId,
                    State = "2"
                };
                PersonFingerprintMapper.ToEntity(ref masterEntity, request);
                await _personFingerprintRepository.AddAsync(masterEntity, cancellationToken);
                viewModel = PersonFingerprintMapper.ToViewModel(masterEntity);
            }
            apiResult.Data = viewModel;

            return apiResult;
        }

        protected override bool HasAccess(CreatePersonFingerprintV2Command request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
    }
}
