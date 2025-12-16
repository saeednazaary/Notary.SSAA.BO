using MediatR;
using Serilog;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.Fingerprint;
using Notary.SSAA.BO.DataTransferObject.Queries.Fingerprint;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Fingerprint;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.CommandHandler.Fingerprint_V2.Handlers
{
    internal class DeletePersonFingerprintCommandHandler : BaseCommandHandler<DeletePersonFingerprintV2Command, ApiResult>
    {
        private readonly IPersonFingerprintRepository _personFingerprintRepository;
        private ApiResult<GetInquiryPersonFingerprintRepositoryObject> apiResult;
        public DeletePersonFingerprintCommandHandler(IMediator mediator, IUserService userService, ILogger logger,
           IPersonFingerprintRepository personFingerprintRepository ) : base(mediator, userService, logger)
        {
            _personFingerprintRepository = personFingerprintRepository ?? throw new ArgumentNullException(nameof(personFingerprintRepository));
            apiResult = new();
        }

        protected async override Task<ApiResult> ExecuteAsync(DeletePersonFingerprintV2Command request, CancellationToken cancellationToken)
        {
            var personFingerprint = await _personFingerprintRepository.GetByIdAsync(cancellationToken, request.FingerprintId.ToGuid());
            if (personFingerprint !=null && personFingerprint.FingerprintImageFile!=null)
            {
                personFingerprint.IsDeleted = "1";
                personFingerprint.TfaState = "1";
                if(!string.IsNullOrWhiteSpace( personFingerprint.MocState))
                {
                    personFingerprint.MocState = (Convert.ToInt16(personFingerprint.MocState) + 2).ToString();
                }
                await _personFingerprintRepository.UpdateAsync(personFingerprint, cancellationToken);
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

        protected override bool HasAccess(DeletePersonFingerprintV2Command request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
    }
}
