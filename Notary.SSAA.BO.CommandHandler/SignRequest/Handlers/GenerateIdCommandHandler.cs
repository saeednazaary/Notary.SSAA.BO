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
    public class GenerateIdCommandHandler : BaseCommandHandler<GenerateIdCommand, ApiResult<GenerateIdViewModel>>
    {
        private readonly IApplicationIdGeneratorService _applicationIdGeneratorService;
        private ApiResult<GenerateIdViewModel> apiResult;

        public GenerateIdCommandHandler(IMediator mediator, IUserService userService, ILogger logger,
            IApplicationIdGeneratorService applicationIdGeneratorService, ISignRequestSemaphoreRepository signRequestSemaphoreRepository) : base(mediator, userService, logger)
        {
            apiResult = new();
            apiResult.Data = new();
            _applicationIdGeneratorService = applicationIdGeneratorService ?? throw new ArgumentNullException(nameof(applicationIdGeneratorService));
        }

        protected override async Task<ApiResult<GenerateIdViewModel>> ExecuteAsync(GenerateIdCommand request, CancellationToken cancellationToken)
        {
            GenerateIdViewModel generateIdView = new();
            generateIdView.UniqeIdentifer = _applicationIdGeneratorService.ProvideEncryptedGuid();
            if (generateIdView.UniqeIdentifer is null )
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("خطا در ایجاد شناسه جدید لطفا با راهبر سامانه تماس بگیرید.");
            }
            apiResult.Data = generateIdView;
            return apiResult;
        }
        protected override bool HasAccess(GenerateIdCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
    }
}
