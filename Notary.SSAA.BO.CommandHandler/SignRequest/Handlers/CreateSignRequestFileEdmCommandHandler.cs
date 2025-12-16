using MediatR;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Serilog;
using SSAA.Notary.DataTransferObject.Commands.SignRequest;


namespace SSAA.Notary.CommandHandler.SignRequest.Handlers
{
    internal class CreateSignRequestFileEdmCommandHandler : BaseCommandHandler<CreateSignRequestFileEdmCommand, ApiResult>
    {
        private readonly ISignRequestFileRepository _signRequestFileRepository;
        private SignRequestFile signRequestFile;
        private readonly IDateTimeService _dateTimeService;
        private ApiResult _apiResult;
        public CreateSignRequestFileEdmCommandHandler(IMediator mediator, IUserService userService, ILogger logger, ISignRequestFileRepository signRequestFileRepository, IDateTimeService dateTimeService) : base(mediator, userService, logger)
        {
            _signRequestFileRepository = signRequestFileRepository;
            _dateTimeService = dateTimeService;
            _apiResult = new();
            signRequestFile = new();
        }

        protected override async Task<ApiResult> ExecuteAsync(CreateSignRequestFileEdmCommand request, CancellationToken cancellationToken)
        {
            var file = await _signRequestFileRepository.GetSignRequestEdmFile(request.SignRequestId, cancellationToken);
            if (file is null)
            {
                signRequestFile.Id = Guid.NewGuid();
                signRequestFile.LastFileCreateDate = _dateTimeService.CurrentPersianDate;
                signRequestFile.LastFileCreateTime = _dateTimeService.CurrentTime;
                signRequestFile.SignRequestId = request.SignRequestId;
                signRequestFile.ScriptoriumId = _userService.UserApplicationContext.BranchAccess.BranchCode;
                signRequestFile.EdmId = request.EDMId;
                signRequestFile.EdmVersion = request.EDMVersion;
                signRequestFile.Ilm = "1";
                await _signRequestFileRepository.AddAsync(signRequestFile, cancellationToken);
            }
            else
            {
                file.LastFileCreateDate = _dateTimeService.CurrentPersianDate;
                file.LastFileCreateTime = _dateTimeService.CurrentTime;
                file.EdmId = request.EDMId;
                file.EdmVersion = request.EDMVersion;
                await _signRequestFileRepository.UpdateAsync(file, cancellationToken);
            }
          
            _apiResult.IsSuccess = true;
            return _apiResult;
        }

        protected override bool HasAccess(CreateSignRequestFileEdmCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar);
        }
    }
}
