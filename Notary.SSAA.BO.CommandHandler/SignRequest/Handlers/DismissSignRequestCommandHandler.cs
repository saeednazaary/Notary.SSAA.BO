using Mapster;
using MediatR;
using Serilog;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.SignRequest;
using Notary.SSAA.BO.DataTransferObject.Queries.SignRequest;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.CommandHandler.SignRequest.Handlers
{
    public class DismissSignRequestCommandHandler : BaseCommandHandler<DismissSignRequestCommand, ApiResult<SignRequestViewModel>>
    {
        private Domain.Entities.SignRequest masterEntity;
        private readonly ISignRequestRepository _signRequestRepository;
        private readonly IDateTimeService _dateTimeService;
        private readonly IApplicationIdGeneratorService _applicationIdGeneratorService;
        private ApiResult<SignRequestViewModel> apiResult;

        public DismissSignRequestCommandHandler(IMediator mediator, IUserService userService, ILogger logger, ISignRequestRepository signRequestRepository, IDateTimeService dateTimeService, IApplicationIdGeneratorService applicationIdGeneratorService) : base(mediator, userService, logger)
        {
            masterEntity = new();
            apiResult = new();
            _signRequestRepository = signRequestRepository ?? throw new ArgumentNullException(nameof(signRequestRepository));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
            _applicationIdGeneratorService = applicationIdGeneratorService ?? throw new ArgumentNullException(nameof(applicationIdGeneratorService));
        }

        protected override async Task<ApiResult<SignRequestViewModel>> ExecuteAsync(DismissSignRequestCommand request, CancellationToken cancellationToken)
        {
            Guid signRequestId = _applicationIdGeneratorService.DecryptGuid(request.SignRequestId);
            if (signRequestId == Guid.Empty)
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("شناسه وارد شده معتبر نیست.");
                return apiResult;
            }
            masterEntity = await _signRequestRepository.GetByIdAsync(cancellationToken, signRequestId);
            if (masterEntity is not null)
            {
                DismissSignRequestViewModel dismissSignRequestViewModel = new();
                if (masterEntity.State != "1")
                {
                    apiResult.IsSuccess = false;
                    apiResult.message.Add("درخواست در وضعیت مناسب حذف نیست");
                    return apiResult;
                }
                if (masterEntity.IsRemoteRequest == "1" && string.IsNullOrWhiteSpace(request.Modifier))
                {
                    apiResult.IsSuccess = false;
                    apiResult.message.Add("فیلد نام انصراف دهنده اجباری میباشد");
                    return apiResult;
                }
                masterEntity.State = "3";
                masterEntity.ModifyDate = _dateTimeService.CurrentPersianDate;
                masterEntity.ModifyDate = _dateTimeService.CurrentTime;
                if (masterEntity.IsRemoteRequest == "1")
                {
                    masterEntity.Modifier = request.Modifier;
                }
                else
                {
                    masterEntity.Modifier = _userService.UserApplicationContext.User.Name + " " + _userService.UserApplicationContext.User.Family;

                }
                await _signRequestRepository.UpdateAsync(masterEntity, cancellationToken);
                ApiResult<SignRequestViewModel> getResponse = await _mediator.Send(new GetSignRequestByIdQuery(request.SignRequestId), cancellationToken);
                if (getResponse.IsSuccess)
                {

                    apiResult.Data = getResponse.Data.Adapt<SignRequestViewModel>();
                    _ = apiResult.Data.SignRequestPersons.OrderBy(x => x.RowNo);

                }
                else
                {
                    apiResult.IsSuccess = false;
                    apiResult.statusCode = getResponse.statusCode;
                    apiResult.message.Add("مشکلی در دریافت اطلاعات از پایگاه داده رخ داده است ");


                }

            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("گواهی امضا مربئطه یافت نشد.");
                apiResult.statusCode = ApiResultStatusCode.NotFound;
            }

            return apiResult;

        }

        protected override bool HasAccess(DismissSignRequestCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Admin) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
    }
}
