using MediatR;
using Serilog;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.Fingerprint;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Fingerprint;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.CommandHandler.Fingerprint_V2.Handlers
{
    public class CreateMocLogCommandHandler : BaseCommandHandler<CreateMocLogCommand, ApiResult>
    {
        private Domain.Entities.PersonFingerprint masterEntity;
        private readonly IRepository<PersonFingerprint> _personFingerprintRepository;
        private readonly  IDateTimeService _dateTimeService ;
        private ApiResult apiResult;
        public CreateMocLogCommandHandler(IMediator mediator, IUserService userService, ILogger logger, 
            IRepository<PersonFingerprint> personFingerprintRepository, IDateTimeService dateTimeService) : base(mediator, userService, logger)
        {
            _personFingerprintRepository = personFingerprintRepository ?? throw new ArgumentNullException(nameof(personFingerprintRepository));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
            apiResult = new ApiResult<GetInquiryPersonFingerprintRepositoryObject>();
        }
        protected override async Task<ApiResult> ExecuteAsync(CreateMocLogCommand request, CancellationToken cancellationToken)
        {
            await Task.Run(() => { }, cancellationToken);
            return apiResult;
        }
        protected override bool HasAccess(CreateMocLogCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
    }
}
