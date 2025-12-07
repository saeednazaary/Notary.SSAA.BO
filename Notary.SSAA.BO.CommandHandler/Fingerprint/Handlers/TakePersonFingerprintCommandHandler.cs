using MediatR;
using Serilog;
using SourceAFIS;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.Fingerprint;
using Notary.SSAA.BO.DataTransferObject.Mappers.Fingerprint;
using Notary.SSAA.BO.DataTransferObject.Queries.Fingerprint;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ReportTools;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Fingerprint;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Fingerprint;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.Utilities.Fingerprint;


namespace Notary.SSAA.BO.CommandHandler.Fingerprint.Handlers
{
    internal class TakePersonFingerprintCommandHandler : BaseCommandHandler<TakePersonFingerprintCommand, ApiResult<GetInquiryPersonFingerprintRepositoryObject>>
    {
        private Domain.Entities.PersonFingerprint masterEntity;
        private readonly IRepository<PersonFingerprint> _personFingerprintRepository;
        private ApiResult<GetInquiryPersonFingerprintRepositoryObject> apiResult;
        private readonly IDateTimeService _dateTimeService;

        public TakePersonFingerprintCommandHandler(IMediator mediator, IRepository<PersonFingerprint> personFingerprintRepository, IUserService userService,
            ILogger logger, IDateTimeService dateTimeService) : base(mediator, userService, logger)
        {
            _personFingerprintRepository = personFingerprintRepository ?? throw new ArgumentNullException(nameof(personFingerprintRepository));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
            apiResult = new ApiResult<GetInquiryPersonFingerprintRepositoryObject>();
        }

        protected override async Task<ApiResult<GetInquiryPersonFingerprintRepositoryObject>> ExecuteAsync(TakePersonFingerprintCommand request, CancellationToken cancellationToken)
        {
            masterEntity = await _personFingerprintRepository.GetByIdAsync(cancellationToken, request.FingerprintId.ToGuid());
            TakeFingerprintPersonViewModel takeFingerprintPersonViewModel = new();
            GetInquiryPersonFingerprintQuery inquiryFingerprintQuery = new(request.FingerprintId);
            ApiResult<GetInquiryPersonFingerprintRepositoryObject> inquiryFingerprintResult = null;
            if (masterEntity is not null)
            {
                if (!string.IsNullOrWhiteSpace(request.State))
                {
                    masterEntity.State = request.State;
                    masterEntity.Description = "";
                    await _personFingerprintRepository.UpdateAsync(masterEntity, cancellationToken);

                    inquiryFingerprintQuery = new(request.FingerprintId);
                    inquiryFingerprintResult = await _mediator.Send(inquiryFingerprintQuery, cancellationToken);
                    if (inquiryFingerprintResult.IsSuccess)
                        apiResult.Data = inquiryFingerprintResult.Data;
                    return apiResult;
                }
                takeFingerprintPersonViewModel.FingerprintId = masterEntity.Id.ToString();
                PersonFingerprintMapper.ToEntity(ref masterEntity, request);
                masterEntity.FingerprintGetDate = _dateTimeService.CurrentPersianDate;
                masterEntity.FingerprintGetTime = _dateTimeService.CurrentTime;
                masterEntity.FingerprintRawImage = Convert.FromBase64String(request.FingerprintImageFile);
                masterEntity.FingerprintImageType = "JPEG";
                masterEntity.IsDeleted = "2";
                masterEntity.State = "1";
                var convertToJpegInput = new FingerprintRAWToJPEGInput();
                convertToJpegInput.Width = request.FingerprintImageWidth.ToInt();
                convertToJpegInput.Height = request.FingerprintImageHeight.ToInt();
                convertToJpegInput.Data = request.FingerprintImageFile;
                convertToJpegInput.DeviceType = FingerDevice.Fotronic;

                masterEntity.FingerprintImageFile = Convert.FromBase64String(FingerprintUtilities.ConvertToJPEGBase64(convertToJpegInput));

                var fpiOptions = new FingerprintImageOptions() { Dpi = 500 };
                var fpi = new FingerprintImage(masterEntity.FingerprintImageFile, fpiOptions);
                var fpiTemplate = new FingerprintTemplate(fpi);
                masterEntity.FingerprintFeatures = fpiTemplate.ToByteArray();

                await _personFingerprintRepository.UpdateAsync(masterEntity, cancellationToken);

                var cmd = new MatchPersonFingerprintCommand() { FingerprintId = masterEntity.Id.ToString() };
                var matchApiResult = await _mediator.Send(cmd, cancellationToken);
                if (!matchApiResult.IsSuccess)
                {
                    masterEntity.State = "2";
                    masterEntity.Description = "اثر انگشت جاری ، به علت عدم تطابق با آخرین اثر انگشت اخذ شده در سامانه برای متقاضی ،غیر فعال شده است";
                    await _personFingerprintRepository.UpdateAsync(masterEntity, cancellationToken);

                    apiResult.IsSuccess = false;
                    apiResult.message.Add("هشدار! اثرانگشت دریافت شده با آخرین اثرانگشت ثبت شده برای این شخص مطابقت ندارد.سردفتر محترم، آیا با شرایط اعلام شده، ثبت این اثرانگشت را ادامه می دهید ؟");
                    apiResult.HasAllarmMessage = true;
                    return apiResult;
                }
                takeFingerprintPersonViewModel.StateId = masterEntity.TfaState;
                apiResult.message.Add("اخذ اثرانگشت موفقیت آمیز بود .");
            }
            else
            {
                apiResult.message.Add("اثرانگشت یافت نشد .");
                apiResult.IsSuccess = false;
                apiResult.statusCode = ApiResultStatusCode.NotFound;
            }


            inquiryFingerprintResult = await _mediator.Send(inquiryFingerprintQuery, cancellationToken);
            if (inquiryFingerprintResult.IsSuccess)
                apiResult.Data = inquiryFingerprintResult.Data;

            return apiResult;
        }

        protected override bool HasAccess(TakePersonFingerprintCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
    }
}
