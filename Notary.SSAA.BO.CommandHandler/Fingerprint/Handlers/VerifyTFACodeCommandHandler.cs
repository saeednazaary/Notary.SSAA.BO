using MediatR;
using Serilog;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.Fingerprint;
using Notary.SSAA.BO.DataTransferObject.Queries.Fingerprint;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Fingerprint;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.CommandHandler.Fingerprint.Handlers
{
    public class VerifyTFACodeCommandHandler : BaseCommandHandler<VerifyTFACodeCommand, ApiResult>

    {
        private Domain.Entities.PersonFingerprint masterEntity;
        private readonly IRepository<PersonFingerprint> _personFingerprintRepository;
        private readonly IDateTimeService _dateTimeService;
        private ApiResult<GetInquiryPersonFingerprintRepositoryObject> apiResult;

        public VerifyTFACodeCommandHandler(IMediator mediator, IUserService userService, ILogger logger,
            IRepository<PersonFingerprint> personFingerprintRepository, IDateTimeService dateTimeService) : base(mediator, userService, logger)
        {
            _personFingerprintRepository = personFingerprintRepository ?? throw new ArgumentNullException(nameof(personFingerprintRepository));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
            apiResult = new ApiResult<GetInquiryPersonFingerprintRepositoryObject>();
        }

        protected override async Task<ApiResult> ExecuteAsync(VerifyTFACodeCommand request, CancellationToken cancellationToken)

        {
            masterEntity = await _personFingerprintRepository.GetByIdAsync(cancellationToken, request.FingerprintId.ToGuid());
            if (masterEntity is not null)
            {
                if (masterEntity.TfaValue == request.TFACode)
                {
                    string sendDateTime = masterEntity.TfaSendDate + "-" + masterEntity.TfaSendTime;
                    string nowDateTime = _dateTimeService.CurrentPersianDate + "-" + _dateTimeService.CurrentTime;
                    TimeSpan timeDistance = nowDateTime.GetDateTimeDistance(sendDateTime);
                    if (timeDistance.TotalSeconds < 120)
                    {
                        masterEntity.TfaValidateDate = _dateTimeService.CurrentPersianDate;
                        masterEntity.TfaValidateTime = _dateTimeService.CurrentTime;
                        masterEntity.TfaState = "2";
                        apiResult.message.Add("احراز کد با موفقیت انجام شد .");
                    }
                    else
                    {
                        masterEntity.TfaValidateDate = _dateTimeService.CurrentPersianDate;
                        masterEntity.TfaValidateTime = _dateTimeService.CurrentTime;
                        masterEntity.TfaState = "3";
                        apiResult.IsSuccess = false;
                        apiResult.message.Add("کد دو مرحله ای باطل شده است . ");
                    }
                }
                else
                {
                    apiResult.IsSuccess = false;
                    apiResult.message.Add("کد وارد شده معتبر نیست .");
                }
                await _personFingerprintRepository.UpdateAsync(masterEntity, cancellationToken);

            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("اثرانگشت مربوطه یافت نشد . ");

            }

            GetInquiryPersonFingerprintQuery inquiryFingerprintQuery = new(request.FingerprintId);
            var inquiryFingerprintResult = await _mediator.Send(inquiryFingerprintQuery, cancellationToken);
            if (inquiryFingerprintResult.IsSuccess)
                apiResult.Data = inquiryFingerprintResult.Data;

            return apiResult;
        }

        protected override bool HasAccess(VerifyTFACodeCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
    }
}
