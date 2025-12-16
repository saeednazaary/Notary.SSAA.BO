using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.Fingerprint;
using Notary.SSAA.BO.DataTransferObject.Mappers.Fingerprint;
using Notary.SSAA.BO.DataTransferObject.Queries.Fingerprint;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Fingerprint;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Fingerprint;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.Utilities.Fingerprint;
using Serilog;
using SourceAFIS;


namespace Notary.SSAA.BO.CommandHandler.Fingerprint_V2.Handlers
{
    internal class TakePersonFingerprintCommandHandler : BaseCommandHandler<TakePersonFingerprintV2Command, ApiResult<GetInquiryPersonFingerprintRepositoryObject>>
    {
        private Domain.Entities.PersonFingerprint masterEntity;
        private readonly IRepository<PersonFingerprint> _personFingerprintRepository;
        private ApiResult<GetInquiryPersonFingerprintRepositoryObject> apiResult;
        private readonly IRepository<Notary.SSAA.BO.Domain.Entities.Document> _documentRepository;
        private readonly IRepository<Notary.SSAA.BO.Domain.Entities.SignRequest> _signRequestRepository;
        private readonly IDateTimeService _dateTimeService;
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;
        public TakePersonFingerprintCommandHandler(IMediator mediator, IRepository<PersonFingerprint> personFingerprintRepository, IUserService userService,
            ILogger logger, IDateTimeService dateTimeService, IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration, 
            IRepository<Notary.SSAA.BO.Domain.Entities.Document> documentRepository,IRepository<Notary.SSAA.BO.Domain.Entities.SignRequest> signRequestRepository
            ) : base(mediator, userService, logger)
        {
            _personFingerprintRepository = personFingerprintRepository ?? throw new ArgumentNullException(nameof(personFingerprintRepository));
            _documentRepository = documentRepository ?? throw new ArgumentNullException(nameof(documentRepository));
            _signRequestRepository = signRequestRepository ?? throw new ArgumentNullException(nameof(signRequestRepository));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
            apiResult = new ApiResult<GetInquiryPersonFingerprintRepositoryObject>();
        }

        protected override async Task<ApiResult<GetInquiryPersonFingerprintRepositoryObject>> ExecuteAsync(TakePersonFingerprintV2Command request, CancellationToken cancellationToken)
        {
            masterEntity = await _personFingerprintRepository.GetByIdAsync(cancellationToken, request.FingerprintId.ToGuid());
            TakeFingerprintPersonViewModel takeFingerprintPersonViewModel = new();
            if (masterEntity is not null)
            {
                if (!string.IsNullOrWhiteSpace(request.State))
                {
                    masterEntity.State = request.State;
                    masterEntity.Description = "";
                    await _personFingerprintRepository.UpdateAsync(masterEntity, cancellationToken);

                    GetInquiryPersonFingerprintQuery inquiryFingerprintQuery1 = new(request.FingerprintId);
                    var inquiryFingerprintResult1 = await _mediator.Send(inquiryFingerprintQuery1, cancellationToken);
                    if (inquiryFingerprintResult1.IsSuccess)
                        apiResult.Data = inquiryFingerprintResult1.Data;
                    return apiResult;
                }
                if (masterEntity.PersonFingerprintUseCaseId=="1")
                {
                    var person = await _signRequestRepository.TableNoTracking
                        .Where(x =>
                            x.ScriptoriumId == masterEntity.OrganizationId &&
                            x.Id == masterEntity.UseCaseMainObjectId.ToGuid()
                        )
                        .SelectMany(x => x.SignRequestPeople)
                        .SingleOrDefaultAsync(p =>
                            p.ScriptoriumId == masterEntity.OrganizationId &&
                            p.Id == masterEntity.UseCasePersonObjectId.ToGuid(),cancellationToken
                        );
                    SabteAhvalWithoutImageServiceInput checkAliveInput = new();
                    checkAliveInput.NationalNo=person.NationalNo;
                    checkAliveInput.BirthDate=person.BirthDate;
                    checkAliveInput.ClientId = "SSAR-Fingerprint-CheckAlive";
                    checkAliveInput.MainObjectId = masterEntity.Id.ToString();
                    var checkAliveRes=await _mediator.Send(checkAliveInput,cancellationToken);
                    if (checkAliveRes.IsSuccess && checkAliveRes.Data is not null)
                    {
                        if (checkAliveRes.Data.IsDead)
                        {
                            apiResult.IsSuccess = false;
                            apiResult.Data = null;
                            apiResult.message.Add(
                                $"{person.Name} {person.Family} در قید حیات نمی‌باشد. لطفاً وضعیت حیات را از طریق فرم مجدداً بروزرسانی کنید."
                            );

                            return apiResult;
                        }
                    }
                    else
                    {
                        apiResult.IsSuccess = false;
                        apiResult.Data = null;
                        apiResult.message.Add("خطا در ارتباط با ثبت احوال لطفا پس از مدتی دوباره تلاش کنید .");
                        return apiResult;
                    }

                }
                else if (masterEntity.PersonFingerprintUseCaseId=="2")
                {
                    var person = await _documentRepository.TableNoTracking
                        .Where(x =>
                            x.ScriptoriumId == masterEntity.OrganizationId &&
                            x.Id == masterEntity.UseCaseMainObjectId.ToGuid()
                        )
                        .SelectMany(x => x.DocumentPeople)
                        .SingleOrDefaultAsync(p =>
                            p.ScriptoriumId == masterEntity.OrganizationId &&
                            p.Id == masterEntity.UseCasePersonObjectId.ToGuid(), cancellationToken
                        );

                    SabteAhvalWithoutImageServiceInput checkAliveInput = new();
                    checkAliveInput.NationalNo = person.NationalNo;
                    checkAliveInput.BirthDate = person.BirthDate;
                    checkAliveInput.ClientId = "SSAR-Fingerprint-CheckAlive";
                    checkAliveInput.MainObjectId = masterEntity.Id.ToString();
                    var checkAliveRes = await _mediator.Send(checkAliveInput, cancellationToken);
                    if (checkAliveRes.IsSuccess && checkAliveRes.Data is not null)
                    {
                        if (checkAliveRes.Data.IsDead)
                        {
                            apiResult.IsSuccess = false;
                            apiResult.Data = null;
                            apiResult.message.Add(
                                $"{person.Name} {person.Family} در قید حیات نمی‌باشد. لطفاً وضعیت حیات را از طریق فرم مجدداً بروزرسانی کنید."
                            );
                            return apiResult;
                        }
                    }
                    else
                    {
                        apiResult.IsSuccess = false;
                        apiResult.Data = null;
                        apiResult.message.Add("خطا در ارتباط با ثبت احوال لطفا پس از مدتی دوباره تلاش کنید .");
                        return apiResult;
                    }
                }
                takeFingerprintPersonViewModel.FingerprintId = masterEntity.Id.ToString();
                PersonFingerprintMapper.ToEntity(ref masterEntity, request);
                masterEntity.FingerprintGetDate = _dateTimeService.CurrentPersianDate;
                masterEntity.FingerprintGetTime = _dateTimeService.CurrentTime;
                masterEntity.FingerprintImageType = "JPEG";
                masterEntity.FingerprintRawImage = Convert.FromBase64String(request.FingerprintImageFile);
                masterEntity.IsDeleted = "2";
                var convertToJpegInput = new FingerprintRAWToJPEGInput();
                convertToJpegInput.Width = request.FingerprintImageWidth.ToInt();
                convertToJpegInput.Height = request.FingerprintImageHeight.ToInt();
                convertToJpegInput.Data = request.FingerprintImageFile;
                var str = request.FingerprintScannerDeviceType.ToLower();
                if (str.StartsWith("futronic"))
                {
                    convertToJpegInput.DeviceType = FingerDevice.Fotronic;
                }
                else if (str == "suprema")
                {
                    convertToJpegInput.DeviceType = FingerDevice.Suprima;
                }
                else if (str == "hongdas580")
                {
                    convertToJpegInput.DeviceType = FingerDevice.Hongda;

                }

                masterEntity.FingerprintImageFile = Convert.FromBase64String(FingerprintUtilities.ConvertToJPEGBase64(convertToJpegInput));

                var fpiOptions = new FingerprintImageOptions() { Dpi = 500 };
                var fpi = new FingerprintImage(masterEntity.FingerprintImageFile, fpiOptions);
                var fpiTemplate = new FingerprintTemplate(fpi);
                masterEntity.FingerprintFeatures = fpiTemplate.ToByteArray();
                masterEntity.State = "2";
                await _personFingerprintRepository.UpdateAsync(masterEntity, cancellationToken);
                var cmd = new MatchPersonFingerprintV2Command() { FingerprintId = masterEntity.Id.ToString() };
                var matchApiResult = await _mediator.Send(cmd, cancellationToken);

                if (!matchApiResult.IsSuccess)
                {
                    if (matchApiResult.HasAllarmMessage)
                    {
                        masterEntity.State = "2";
                        masterEntity.Description = "اثر انگشت جاری ، به علت عدم تطابق با آخرین اثر انگشت اخذ شده در سامانه برای متقاضی ،غیر فعال شده است";
                        await _personFingerprintRepository.UpdateAsync(masterEntity, cancellationToken);

                        apiResult.IsSuccess = false;
                        if (matchApiResult.message.Count>0)
                        {
                            apiResult.message = matchApiResult.message;
                        }
                        else
                        {
                            apiResult.message.Add($"هشدار! اثرانگشت دریافت شده با آخرین اثرانگشت ثبت شده برای این شخص در دفترخانه اسناد رسمي مطابقت ندارد.سردفتر محترم، آیا با شرایط اعلام شده، ثبت این اثرانگشت را ادامه می دهید ؟");

                        }
                        apiResult.HasAllarmMessage = true;
                        return apiResult;
                    }
                    else
                    {
                        apiResult.IsSuccess = false;
                        if (apiResult.message.Count == 0)
                            apiResult.message.Add("خطا در مقایسه آخرین اثر انگشت ثبت شده متقاضی در سامانه با اثر انگشت جاری وی رخ داده است");
                        masterEntity.State = "2";
                        masterEntity.Description = apiResult.message[0];
                        await _personFingerprintRepository.UpdateAsync(masterEntity, cancellationToken);
                    }
                }
                else
                {
                    masterEntity.State = "1";
                    masterEntity.Description = string.Empty;
                    await _personFingerprintRepository.UpdateAsync(masterEntity, cancellationToken);
                }

                takeFingerprintPersonViewModel.StateId = masterEntity.State;
                apiResult.message.Add("اخذ اثرانگشت موفقیت آمیز بود .");

                GetInquiryPersonFingerprintQuery inquiryFingerprintQuery = new(request.FingerprintId);
                var inquiryFingerprintResult = await _mediator.Send(inquiryFingerprintQuery, cancellationToken);
                if (inquiryFingerprintResult.IsSuccess)
                    apiResult.Data = inquiryFingerprintResult.Data;
            }
            else
            {
                apiResult.message.Add("اثرانگشت یافت نشد .");
                apiResult.IsSuccess = false;
            }

            return apiResult;
        }

        protected override bool HasAccess(TakePersonFingerprintV2Command request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
    }
}
