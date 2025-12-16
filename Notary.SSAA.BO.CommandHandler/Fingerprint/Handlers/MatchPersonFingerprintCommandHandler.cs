using Azure.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SourceAFIS;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.Fingerprint;
using Notary.SSAA.BO.DataTransferObject.Mappers.Fingerprint;
using Notary.SSAA.BO.DataTransferObject.Queries.Fingerprint;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ReportTools;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Fingerprint;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.ExternalServices;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.RepositoryObjects.Fingerprint;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.Utilities.Fingerprint;


namespace Notary.SSAA.BO.CommandHandler.Fingerprint.Handlers
{
    internal class MatchPersonFingerprintCommandHandler : BaseCommandHandler<MatchPersonFingerprintCommand, ApiResult<GetInquiryPersonFingerprintRepositoryObject>>
    {
        private Domain.Entities.PersonFingerprint masterEntity;
        private readonly IPersonFingerprintRepository _personFingerprintRepository;
        private readonly IRepository<Notary.SSAA.BO.Domain.Entities.Document> _documentRepository;
        private ApiResult<GetInquiryPersonFingerprintRepositoryObject> apiResult;
        private readonly IDateTimeService _dateTimeService;

        public MatchPersonFingerprintCommandHandler(IMediator mediator, IPersonFingerprintRepository personFingerprintRepository, IUserService userService,
            ILogger logger, IDateTimeService dateTimeService, IRepository<Notary.SSAA.BO.Domain.Entities.Document> documentRepository) : base(mediator, userService, logger)
        {
            _personFingerprintRepository = personFingerprintRepository ?? throw new ArgumentNullException(nameof(personFingerprintRepository));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
            _documentRepository = documentRepository;
            apiResult = new ApiResult<GetInquiryPersonFingerprintRepositoryObject>();
        }

        protected override async Task<ApiResult<GetInquiryPersonFingerprintRepositoryObject>> ExecuteAsync(MatchPersonFingerprintCommand request, CancellationToken cancellationToken)
        {
            return apiResult;
            masterEntity = await _personFingerprintRepository.GetByIdAsync(cancellationToken, request.FingerprintId.ToGuid());
            TakeFingerprintPersonViewModel takeFingerprintPersonViewModel = new();
            if (masterEntity is not null)
            {
                await _personFingerprintRepository.LoadReferenceAsync(masterEntity, x => x.PersonFingerType, cancellationToken);
                string LegacyId = "";
                if (masterEntity.PersonFingerprintUseCaseId == "2")
                {
                    var document = await _documentRepository.TableNoTracking.Where(x => x.Id == masterEntity.UseCaseMainObjectId.ToGuid()).FirstOrDefaultAsync(cancellationToken);
                    if (document is not null)
                    {
                        if (!string.IsNullOrWhiteSpace(document.LegacyId))
                        {
                            LegacyId = document.LegacyId;
                        }
                    }
                }
                var lastFingerprintFromServiceInput = new FindLastFingerprintServiceInput();
                lastFingerprintFromServiceInput.PersonNationalNo = masterEntity.PersonNationalNo;
                lastFingerprintFromServiceInput.DocumentId = masterEntity.UseCaseMainObjectId.Replace("-", "").Replace("_", "").ToUpper();
                if (!string.IsNullOrWhiteSpace(LegacyId))
                {
                    lastFingerprintFromServiceInput.DocumentId += "|LegacyId=" + LegacyId;
                }
                lastFingerprintFromServiceInput.SelectedFinger = Convert.ToInt32(masterEntity.PersonFingerType.SabtahvalCode);
                var lastFingerprintFromService = await _mediator.Send(lastFingerprintFromServiceInput, cancellationToken);

                if (lastFingerprintFromService.IsSuccess)
                {
                    if (lastFingerprintFromService.Data != null && (!string.IsNullOrWhiteSpace(lastFingerprintFromService.Data.PersonLastFingerprintImage) || !string.IsNullOrWhiteSpace(lastFingerprintFromService.Data.PersonLastFingerprintRawImage)))
                    {
                        try
                        {
                            byte[] alternativeSecondImage = null;
                            if (!string.IsNullOrWhiteSpace(lastFingerprintFromService.Data.PersonLastFingerprintRawImage))
                                alternativeSecondImage = GetImage(lastFingerprintFromService.Data);
                            MatchFingerprints(masterEntity.FingerprintImageFile, !string.IsNullOrWhiteSpace(lastFingerprintFromService.Data.PersonLastFingerprintImage) ? Convert.FromBase64String(lastFingerprintFromService.Data.PersonLastFingerprintImage) : null, alternativeSecondImage);
                        }
                        catch
                        {
                            apiResult.message.Add(" اثر انگشت جاری متقاضی  با آخرین اثرانگشت ثبت شده ی وی در سامانه مطابقت ندارد");
                            apiResult.IsSuccess = false;
                        }
                    }

                }
                else
                {
                    apiResult.message.Add("خطا در دریافت اخرین اثر انگشت ثبت شده ی متقاضی رخ داد ");
                    apiResult.IsSuccess = false;
                }

            }
            else
            {
                apiResult.message.Add("اثرانگشت یافت نشد .");
                apiResult.IsSuccess = false;
                apiResult.statusCode = ApiResultStatusCode.NotFound;
            }



            return apiResult;
        }

        protected override bool HasAccess(MatchPersonFingerprintCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        private void MatchFingerprints(byte[] firstFingerprint, byte[] secondFingerprint, byte[] alternativeSecondFingerprint)
        {
            var options = new FingerprintImageOptions { Dpi = 500 };
            var probe = new FingerprintTemplate(
                new FingerprintImage(firstFingerprint, options));

            FingerprintTemplate candidate = null;
            double score = 0;
            FingerprintTemplate candidate1 = null;
            double score1 = 0;

            if (secondFingerprint != null)
            {
                candidate = new FingerprintTemplate(
                new FingerprintImage(secondFingerprint, options));
            }

            if (alternativeSecondFingerprint != null)
            {
                candidate1 = new FingerprintTemplate(
                new FingerprintImage(alternativeSecondFingerprint, options));
            }
            if (candidate != null)
            {
                score = new FingerprintMatcher(probe)
                   .Match(candidate);
            }
            if (candidate1 != null)
            {
                score1 = new FingerprintMatcher(probe)
                   .Match(candidate1);
            }
            int threshhold = 40;
            try
            {
                threshhold = Convert.ToInt32(Configuration.Settings.MinimumFingerprintQualityThreshold);
            }
            catch
            {

            }
            if (score >= threshhold)
            {
                apiResult.IsSuccess = true;
            }
            else if (score1 >= threshhold)
            {
                apiResult.IsSuccess = true;
            }
            else
            {
                apiResult.message.Add("اثر انگشت اخذ شده با اثر انگشت موجود در پایگاه داده مطابقت ندارد ، آیا مایل به ادامه هستید ؟");
                apiResult.IsSuccess = false;
            }
        }

        private static byte[] GetImage(FindLastFingerprintServiceViewModel request)
        {
            try
            {
                var convertToJpegInput = new FingerprintRAWToJPEGInput();
                convertToJpegInput.Width = request.PersonLastFingerprintImageWidth;
                convertToJpegInput.Height = request.PersonLastFingerprintImageHeight;
                convertToJpegInput.Data = request.PersonLastFingerprintRawImage;
                convertToJpegInput.DeviceType = FingerDevice.Fotronic;

                return Convert.FromBase64String(FingerprintUtilities.ConvertToJPEGBase64(convertToJpegInput));
            }
            catch
            {
                return null;
            }
        }
    }
}
