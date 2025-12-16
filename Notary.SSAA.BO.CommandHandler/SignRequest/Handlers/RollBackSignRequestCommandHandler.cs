using MediatR;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.SignRequest;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Serilog;

namespace Notary.SSAA.BO.CommandHandler.SignRequest.Handlers
{
    public class RollBackSignRequestCommandHandler : BaseCommandHandler<RollBackSignRequestCommand, ApiResult<RollBackSignRequestViewModel>>
    {
        private readonly ISignRequestSemaphoreRepository _signRequestSemaphoreRepository;
        private Domain.Entities.SignRequestSemaphore masterEntity;
        private readonly IApplicationIdGeneratorService _applicationIdGeneratorService;

        public RollBackSignRequestCommandHandler(IMediator mediator, IUserService userService, ISignRequestSemaphoreRepository signRequestSemaphoreRepository, ILogger logger, IApplicationIdGeneratorService applicationIdGeneratorService) : base(mediator, userService, logger)
        {
            _signRequestSemaphoreRepository = signRequestSemaphoreRepository ?? throw new ArgumentNullException(nameof(signRequestSemaphoreRepository));
            _applicationIdGeneratorService = applicationIdGeneratorService ?? throw new ArgumentNullException(nameof(applicationIdGeneratorService));
        }

        protected async override Task<ApiResult<RollBackSignRequestViewModel>> ExecuteAsync(RollBackSignRequestCommand request, CancellationToken cancellationToken)
        {
            Guid signRequestId = _applicationIdGeneratorService.DecryptGuid(request.SignRequestId);
            if (signRequestId == Guid.Empty)
            {
                ApiResult<RollBackSignRequestViewModel> apiResult = new();
                apiResult.IsSuccess = false;
                apiResult.message.Add("شناسه وارد شده معتبر نیست.");
                return apiResult;
            }
            masterEntity = await _signRequestSemaphoreRepository.GetAsync(x => x.SignRequestId== signRequestId, cancellationToken);

            if (masterEntity is not null)
            {
               await _signRequestSemaphoreRepository.DeleteAsync(masterEntity,cancellationToken);
            }

            return new ApiResult<RollBackSignRequestViewModel>();

        }

        protected override bool HasAccess(RollBackSignRequestCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
    }
}
