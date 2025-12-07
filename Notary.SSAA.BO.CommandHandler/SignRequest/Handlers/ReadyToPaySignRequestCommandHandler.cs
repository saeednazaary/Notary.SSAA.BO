using Mapster;
using MediatR;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.SignRequest;
using Notary.SSAA.BO.DataTransferObject.Queries.SignRequest;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.Kateb;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using Serilog;


namespace Notary.SSAA.BO.CommandHandler.SignRequest.Handlers
{
    public class ReadyToPaySignRequestCommandHandler : BaseCommandHandler<ReadyToPaySignRequestCommand, ApiResult<SignRequestViewModel>>
    {
        private readonly ISignRequestRepository _signRequestRepository;
        private Domain.Entities.SignRequest masterEntity;
        private readonly IApplicationIdGeneratorService _applicationIdGeneratorService;
        private readonly ApiResult<SignRequestViewModel> apiResult;

        public ReadyToPaySignRequestCommandHandler(IMediator mediator, IUserService userService, ISignRequestRepository signRequestRepository, ILogger logger, IApplicationIdGeneratorService applicationIdGeneratorService) : base(mediator, userService, logger)
        {
            _signRequestRepository = signRequestRepository ?? throw new ArgumentNullException(nameof(signRequestRepository));
            apiResult = new ApiResult<SignRequestViewModel>
            {
                Data = new()
            };
            _applicationIdGeneratorService = applicationIdGeneratorService ?? throw new ArgumentNullException(nameof(signRequestRepository));
        }

        protected override async Task<ApiResult<SignRequestViewModel>> ExecuteAsync(ReadyToPaySignRequestCommand request, CancellationToken cancellationToken)
        {
            Guid signRequestId = _applicationIdGeneratorService.DecryptGuid(request.SignRequestId);
            if (signRequestId == Guid.Empty)
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("شناسه وارد شده معتبر نیست.");
                return apiResult;
            }
            masterEntity = await _signRequestRepository.GetByIdAsync(cancellationToken, signRequestId);
            if (masterEntity == null)
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("گواهی امضا مربوطه یافت نشد .");
                return apiResult;

            }
            ApiResult katebCallResponse = await _mediator.Send(new DocumentRequestPaymentInformationServiceInput(masterEntity.RemoteRequestId.ToString()), cancellationToken);

            masterEntity.IsReadyToPay = "1";
            await _signRequestRepository.UpdateAsync(masterEntity, cancellationToken);
            ApiResult<SignRequestViewModel> getResponse = await _mediator.Send(new GetSignRequestByIdQuery(request.SignRequestId), cancellationToken);
            if (getResponse.IsSuccess)
            {

                apiResult.Data = getResponse.Data.Adapt<SignRequestViewModel>();
                _ = apiResult.Data.SignRequestPersons.OrderBy(x => x.RowNo);
                apiResult.message.Add("اطلاعات پرداخت درخواست با موفقیت برای متقاضی ارسال شد");
            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.statusCode = getResponse.statusCode;
                apiResult.message.Add("مشکلی در دریافت اطلاعات از پایگاه داده رخ داده است ");


            }
            return apiResult;

        }

        protected override bool HasAccess(ReadyToPaySignRequestCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Admin) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
    }
}
