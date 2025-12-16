using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.SignRequest;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.BaseInfo;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.EPayment;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.Exordium;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.Media;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.BaseInfo;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.BusinessRules.SignRequestRules;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.SsrConfig;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using Serilog;


namespace Notary.SSAA.BO.CommandHandler.SignRequest.Handlers
{
    public class UpdateSignRequestPaymentStateCommandHandler : BaseCommandHandler<UpdateSignRequestPaymentStateCommand, ApiResult<UpdateSignRequestPaymentStateViewModel>>
    {
        private Domain.Entities.SignRequest masterEntity;
        private readonly ISignRequestRepository _signRequestRepository;
        private ApiResult<UpdateSignRequestPaymentStateViewModel> apiResult;
        private ApiResult<SignRequestBasicInfoViewModel> baseInfoapiResult;
        private readonly IConfiguration _configuration;
        private readonly IDateTimeService _dateTimeService;
        private readonly IRepository<VatInfo> _vatInfoRepository;
        private readonly IApplicationIdGeneratorService _applicationIdGeneratorService;
        private readonly ISsrConfigRepository _ssrConfigRepository;


        public UpdateSignRequestPaymentStateCommandHandler(IMediator mediator, IUserService userService, ILogger logger, ISignRequestRepository signRequestRepository,
            IConfiguration configuration, IDateTimeService dateTimeService, IRepository<VatInfo> vatInfoRepository, IApplicationIdGeneratorService applicationIdGeneratorService, ISsrConfigRepository ssrConfigRepository)
            : base(mediator, userService, logger)
        {
            masterEntity = new();
            apiResult = new();
            baseInfoapiResult = new();
            apiResult.Data = new();
            _signRequestRepository = signRequestRepository ?? throw new ArgumentNullException(nameof(signRequestRepository));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
            _vatInfoRepository = vatInfoRepository ?? throw new ArgumentNullException(nameof(vatInfoRepository));
            _applicationIdGeneratorService = applicationIdGeneratorService ?? throw new ArgumentNullException(nameof(applicationIdGeneratorService));
            _ssrConfigRepository = ssrConfigRepository ?? throw new ArgumentNullException(nameof(ssrConfigRepository));
        }

        protected override async Task<ApiResult<UpdateSignRequestPaymentStateViewModel>> ExecuteAsync(UpdateSignRequestPaymentStateCommand request, CancellationToken cancellationToken)
        {

            UpdateSignRequestPaymentStateViewModel viewModel = new();
            bool isValid = true;
            apiResult.IsSuccess = false;

            if (!request.SignRequestId.IsNullOrWhiteSpace())
            {
                Guid signRequestId = _applicationIdGeneratorService.DecryptGuid(request.SignRequestId);
                if (signRequestId == Guid.Empty)
                {
                    apiResult.IsSuccess = false;
                    apiResult.message.Add("شناسه وارد شده معتبر نیست.");
                    return apiResult;
                }
                masterEntity = await _signRequestRepository.SignRequestTracking(signRequestId, _userService.UserApplicationContext.BranchAccess.BranchCode, cancellationToken);

            }
            else
            {
                masterEntity = await _signRequestRepository.SignRequestTracking(request.SignRequestNo, cancellationToken);

            }
            if (masterEntity is not null)
            {
                viewModel.SignRequestId = masterEntity.Id.ToString();
                if (masterEntity.IsCostPaid != "1" && _configuration.GetValue<string>("PublishEnv") != "Red")
                {
                    if (masterEntity.IsRemoteRequest == "1" )
                    {
                        await InquiryPayment(cancellationToken);
                        if (masterEntity.IsCostPaid == "1")
                        {
                            return apiResult;
                        }
                        else
                        {
                            //if (request.InquiryMode)
                            //{
                            //    return apiResult;
                            //}
                        }
                    }
                    else
                    {
                        await InquiryPayment(cancellationToken);

                        if (masterEntity.IsCostPaid == "1")
                        {
                            return apiResult;
                        }
                        else
                        {
                            if (request.InquiryMode)
                            {
                                return apiResult;
                            }
                        }
                    }
                }
                else if (masterEntity.IsCostPaid == "1")
                {
                    apiResult.IsSuccess = true;
                    apiResult.message.Add("پرداخت قبلا انجام شده");
                    apiResult.Data.IsCostPaid = masterEntity.IsCostPaid;
                    apiResult.Data.PayCostDate = masterEntity.PayCostDate;
                    apiResult.Data.PayCostTime = masterEntity.PayCostTime;
                    apiResult.Data.BillNo = masterEntity.BillNo;
                    apiResult.Data.SumPrices = masterEntity.SumPrices.ToString();
                    apiResult.Data.PaymentType = masterEntity.PaymentType;
                    return apiResult;
                }

                ApiResult validateresult = new();
                var scriptoriumInfo = new SignRequestBasicInfoServiceInput();
                scriptoriumInfo.ScriptoriumId = masterEntity.ScriptoriumId;
                scriptoriumInfo.CurrentDateTime = _dateTimeService.CurrentPersianDateTime;
                baseInfoapiResult = await _mediator.Send(scriptoriumInfo, cancellationToken);
                if (!baseInfoapiResult.IsSuccess || baseInfoapiResult.Data is null)
                {
                    apiResult.IsSuccess = false;
                    apiResult.message.Add("ارتباط با اطلاعات پایه برقرار نشد.");
                    return apiResult;
                }
                if (masterEntity.IsRemoteRequest == "1")
                {
                    validateresult = await ValidateBusinessForRemoteRequest(cancellationToken);

                }
                else
                {
                    validateresult = await ValidateBusinessForSignRequest(cancellationToken);

                }

                if (validateresult.message.Count > 0)
                {
                    apiResult.message = validateresult.message;
                    apiResult.IsSuccess = false;
                    return apiResult;
                }
                /*
                Payment processing begins from this point onwards.
                CodeMeli and FollowCode are the current details at the beginning
                They are hardcoded for testing purposes.
                UnitId is also hardcoded for testing purposes.
                OrganizationId is hardcoded as well for testing purposes.
                In a real environment, OrganizationId can be obtained from the userService.
                Description can be utilized in certain scenarios. 
                */
                if (masterEntity.IsRemoteRequest == "1")
                {
                    if (_configuration.GetValue<string>("PublishEnv") == "Red")
                    {
                        FakePay();
                        apiResult.Data.PaymentLink = _configuration.GetValue<string>("BaseKatebUIURL");
                        apiResult.Data.IsCostPaid = "1";
                        apiResult.IsSuccess = false;
                        apiResult.message = new List<string>() { "این قبض به صورت فیک پرداخت شده است لطفا مجددا وارد فرم شوید .  " };
                    }
                    else
                    {
                        await CalculateCostForRemoteRequest(cancellationToken);

                    }


                }
                else
                {
                    if (_configuration.GetValue<string>("PublishEnv") == "Red")
                    {
                        FakePay();
                        apiResult.Data.PaymentLink = _configuration.GetValue<string>("BaseUIURL");
                        apiResult.Data.IsCostPaid = "1";
                        apiResult.IsSuccess = false;
                        apiResult.message = new List<string>() { "این قبض به صورت فیک پرداخت شده است لطفا مجددا وارد فرم شوید .  " };
                    }
                    else
                    {
                        await CalculateCostForSignRequest(cancellationToken);

                    }
                }


                await _signRequestRepository.UpdateAsync(masterEntity, cancellationToken);

            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("گواهی امضا مربوطه یافت نشد.");
                apiResult.statusCode = ApiResultStatusCode.NotFound;
            }


            return apiResult;

        }

        protected override bool HasAccess(UpdateSignRequestPaymentStateCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Admin) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        private async Task<ApiResult> ValidateBusinessForSignRequest(CancellationToken cancellationToken)
        {
            var apiResult = new ApiResult();
            SsrConfigRepositoryInput configRepositoryInput = new SsrConfigRepositoryInput();
            configRepositoryInput.CurrentScriptoriumId = baseInfoapiResult.Data.ScriptoriumId;
            configRepositoryInput.CurrentGeoLocationId = baseInfoapiResult.Data.GeoLocationId;
            configRepositoryInput.UnitLevelCode = baseInfoapiResult.Data.UnitLevelCode;
            configRepositoryInput.CurrentDayOfWeek = baseInfoapiResult.Data.DayOfWeek;
            configRepositoryInput.CurrentDateTime = _dateTimeService.CurrentPersianDateTime;

            var configs = await _ssrConfigRepository.GetBusinessConfig(configRepositoryInput, cancellationToken);
            if (!SignRequestBusinessRule.CheckSignRequestConfig(configs))
            {
                apiResult.message.Add("خطا در پیکربندی سرویس های گواهی امضا. لفطا با راهبر سیستم تماس بگیرید");

            }
            if (!SignRequestBusinessRule.CheckWorkPermit(configs, baseInfoapiResult.Data.ScriptoriumId))
            {
                apiResult.message.Add("دفترخانه مجوز ثبت گواهی امضا ندارد. لفطا با راهبر سیستم تماس بگیرید");
            }
            if (masterEntity.State != "1")
            {
                apiResult.message.Add("گواهی امضا در وضعیت ایجاد شده نمیباشد .");
                return apiResult;
            }

            if (_userService.UserApplicationContext.UserRole.RoleId != RoleConstants.Sardaftar && _userService.UserApplicationContext.UserRole.RoleId != RoleConstants.Admin)
            {
                apiResult.message.Add("سمت کاربر ، سر دفتر نمیباشد . ");
                return apiResult;
            }
            if (masterEntity.ScriptoriumId=="57999"|| masterEntity.ScriptoriumId == "57998")
            {
                Console.WriteLine(JsonConvert.SerializeObject(_userService.UserApplicationContext));

                if (_userService.UserApplicationContext.UserRole.RoleId == RoleConstants.Sardaftar)
                {
                    if (_userService.UserApplicationContext.BranchAccess.IsBranchOwner == "false" &&
                        _userService.UserApplicationContext.BranchAccess.IssueDocAccess != "true")
                    {
                        apiResult.message.Add("با توجه به حکم ابلاغ صادر شده، شما مجاز به تنظیم اسناد نمی باشید.");
                        return apiResult;
                    }
                }
            }

            
            //Check if any Original person exists
            if (!masterEntity.SignRequestPeople.Any(x => x.IsOriginal == "1"))
            {
                apiResult.message.Add("شخص اصیل در گواهی امضا وجود ندارد . ");
                return apiResult;
            }

            string message = SignRequestPersonBusinessRule.CheckSabteAhval(masterEntity.SignRequestPeople, configs);
            if (!message.IsNullOrWhiteSpace())
            {
                apiResult.message.Add(message);
                return apiResult;
            }
            var notCheckPersonList = new List<Guid>();
            foreach (var item in masterEntity.SignRequestPersonRelateds)
            {
                switch (item.AgentTypeId)
                {
                    case "2":
                        notCheckPersonList.Add(item.MainPersonId);
                        break;
                    case "3":
                        notCheckPersonList.Add(item.MainPersonId);
                        break;
                    case "5":
                        notCheckPersonList.Add(item.MainPersonId);
                        break;
                    case "7":
                        notCheckPersonList.Add(item.MainPersonId);
                        break;
                    case "14":
                        notCheckPersonList.Add(item.MainPersonId);
                        break;
                    case "18":
                        notCheckPersonList.Add(item.MainPersonId);
                        break;
                    case "10":
                        if (item.ReliablePersonReasonId == "7")
                            notCheckPersonList.Add(item.MainPersonId);
                        break;
                    default:
                        break;
                }
            }
            notCheckPersonList = notCheckPersonList.Distinct().ToList();
            message = SignRequestPersonBusinessRule.CheckSana(masterEntity.SignRequestPeople.Where(x => !notCheckPersonList.Contains(x.Id)).ToList(), configs);
            if (!message.IsNullOrWhiteSpace())
            {
                apiResult.message.Add(message);
                return apiResult;
            }
            message = SignRequestPersonBusinessRule.CheckShahkar(masterEntity.SignRequestPeople.Where(x => !notCheckPersonList.Contains(x.Id)).ToList(), configs);
            if (!message.IsNullOrWhiteSpace())
            {
                apiResult.message.Add(message);
                return apiResult;
            }
            message = SignRequestPersonBusinessRule.CheckAmlakEskan(masterEntity.SignRequestPeople.Where(x => !notCheckPersonList.Contains(x.Id)).ToList(), configs);
            if (!message.IsNullOrWhiteSpace())
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add(message);
                return apiResult;
            }
            var attachmentInput = new LoadAttachmentServiceInput();
            attachmentInput.ClientId = SignRequestConstants.AttachmentClientId;
            attachmentInput.RelatedRecordIds = new List<string> { masterEntity.Id.ToString() };

            var attachmentResult = await _mediator.Send(attachmentInput, cancellationToken);

            bool flag = false;
            if (attachmentResult.IsSuccess && attachmentResult?.Data?.AttachmentViewModels != null)
            {

                foreach (var item in attachmentResult.Data.AttachmentViewModels)
                {
                    if (item?.Medias != null &&
                        item.Medias.Count > 0 &&
                        item.DocTypeId == SignRequestConstants.ScanFileDocumentType)
                    {
                        flag = true;
                    }
                }


            }
            if (!flag)
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("فایل اسکن گواهی امضا موجود نمیباشد");
                return apiResult;
            }
            message = SignRequestPersonBusinessRule.CheckMobileNo(masterEntity.SignRequestPeople);
            if (!message.IsNullOrWhiteSpace())
            {
                apiResult.message.Add(message);
                return apiResult;
            }

            message = SignRequestPersonBusinessRule.CheckRelatedPersonExists(masterEntity.SignRequestPeople, masterEntity.SignRequestPersonRelateds);
            if (!message.IsNullOrWhiteSpace())
            {
                apiResult.message.Add(message);
                return apiResult;
            }
            message = SignRequestPersonBusinessRule.CheckInheritorExists(masterEntity.SignRequestPeople, masterEntity.SignRequestPersonRelateds);
            if (!message.IsNullOrWhiteSpace())
            {
                apiResult.message.Add(message);
                return apiResult;
            }
            message = SignRequestPersonBusinessRule.CheckSaghir(masterEntity.SignRequestPeople, masterEntity.SignRequestPersonRelateds, _dateTimeService.CurrentPersianDate);
            if (!message.IsNullOrWhiteSpace())
            {
                apiResult.message.Add(message);
                return apiResult;
            }
            if (!SignRequestRelatedPersonBusinessRule.CheckLoopInPersonRelated(masterEntity.SignRequestPersonRelateds))
            {
                apiResult.message.Add("در گراف وابستگی اشخصاص حلقه به وجود آمده است .");
                return apiResult;
            }
            message = SignRequestPersonBusinessRule.CheckFingerprintPerson(masterEntity.SignRequestPeople, masterEntity.SignRequestPersonRelateds);
            if (!message.IsNullOrWhiteSpace())
            {
                apiResult.message.Add(message);
                return apiResult;
            }

            return apiResult;
        }
        private async Task<ApiResult> ValidateBusinessForRemoteRequest(CancellationToken cancellationToken)
        {
            var apiResult = new ApiResult();
            SsrConfigRepositoryInput configRepositoryInput = new SsrConfigRepositoryInput();
            configRepositoryInput.CurrentScriptoriumId = baseInfoapiResult.Data.ScriptoriumId;
            configRepositoryInput.CurrentGeoLocationId = baseInfoapiResult.Data.GeoLocationId;
            configRepositoryInput.UnitLevelCode = baseInfoapiResult.Data.UnitLevelCode;
            configRepositoryInput.CurrentDayOfWeek = baseInfoapiResult.Data.DayOfWeek;
            configRepositoryInput.CurrentDateTime = _dateTimeService.CurrentPersianDateTime;

            var configs = await _ssrConfigRepository.GetBusinessConfig(configRepositoryInput, cancellationToken);
            if (!SignRequestBusinessRule.CheckSignRequestConfig(configs))
            {
                apiResult.message.Add("خطا در پیکربندی سرویس های گواهی امضا. لفطا با راهبر سیستم تماس بگیرید");

            }
            if (masterEntity.State != "1")
            {
                apiResult.message.Add("گواهی امضا در وضعیت ایجاد شده نمیباشد .");
                return apiResult;
            }

            if (_userService.UserApplicationContext.UserRole.RoleId != RoleConstants.Admin && _userService.UserApplicationContext.UserRole.RoleId != RoleConstants.Sardaftar)
            {
                apiResult.message.Add("سمت کاربر ، ادمین نمیباشد . ");
                return apiResult;
            }
            //Check if any Original person exists
            if (!masterEntity.SignRequestPeople.Any(x => x.IsOriginal == "1"))
            {
                apiResult.message.Add("شخص اصیل در گواهی امضا وجود ندارد . ");
                return apiResult;
            }

            string message = SignRequestPersonBusinessRule.CheckSabteAhval(masterEntity.SignRequestPeople, configs);
            if (!message.IsNullOrWhiteSpace())
            {
                apiResult.message.Add(message);
                return apiResult;
            }


            var attachmentInput = new LoadAttachmentServiceInput();
            attachmentInput.ClientId = SignRequestConstants.AttachmentClientId;
            attachmentInput.RelatedRecordIds = new List<string> { masterEntity.RemoteRequestId.ToString() };

            var attachmentResult = await _mediator.Send(attachmentInput, cancellationToken);
            bool flag = false;

            if (attachmentResult.IsSuccess && attachmentResult?.Data?.AttachmentViewModels != null)
            {

                foreach (var item in attachmentResult.Data.AttachmentViewModels)
                {
                    if (item?.Medias != null &&
                        item.Medias.Count > 0 &&
                        item.DocTypeId == SignRequestConstants.ScanFileDocumentType)
                    {
                        flag = true;
                    }
                }


            }
            if (!flag)
            {
                apiResult.message.Add("فایل اسکن گواهی امضا موجود نمیباشد");
                return apiResult;
            }
            message = SignRequestPersonBusinessRule.CheckMobileNo(masterEntity.SignRequestPeople);
            if (!message.IsNullOrWhiteSpace())
            {
                apiResult.message.Add(message);
                return apiResult;
            }

            message = SignRequestPersonBusinessRule.CheckRelatedPersonExists(masterEntity.SignRequestPeople, masterEntity.SignRequestPersonRelateds);
            if (!message.IsNullOrWhiteSpace())
            {
                apiResult.message.Add(message);
                return apiResult;
            }
            message = SignRequestPersonBusinessRule.CheckInheritorExists(masterEntity.SignRequestPeople, masterEntity.SignRequestPersonRelateds);
            if (!message.IsNullOrWhiteSpace())
            {
                apiResult.message.Add(message);
                return apiResult;
            }
            message = SignRequestPersonBusinessRule.CheckSaghir(masterEntity.SignRequestPeople, masterEntity.SignRequestPersonRelateds, _dateTimeService.CurrentPersianDate);
            if (!message.IsNullOrWhiteSpace())
            {
                apiResult.message.Add(message);
                return apiResult;
            }
            if (!SignRequestRelatedPersonBusinessRule.CheckLoopInPersonRelated(masterEntity.SignRequestPersonRelateds))
            {
                apiResult.message.Add("در گراف وابستگی اشخصاص حلقه به وجود آمده است .");
                return apiResult;
            }
            return apiResult;
        }
        private async Task CalculateCostForSignRequest(CancellationToken cancellationToken)
        {
            string provinceId = string.Empty;
            var accountExordiumInquiry = new GetAccountInfoByScriptoriumCodeInput();
            accountExordiumInquiry.ScriptoriumCode = masterEntity.ScriptoriumId;
            var accountExordiumRes = await _mediator.Send(accountExordiumInquiry, cancellationToken);
            if (masterEntity.ScriptoriumId == "52539" || masterEntity.ScriptoriumId == "57999")
            {
                accountExordiumRes = await _mediator.Send(new GetAccountInfoByScriptoriumCodeInput() { ScriptoriumCode = "52539" }, cancellationToken);
            }
            else
            {
                accountExordiumRes = await _mediator.Send(new GetAccountInfoByScriptoriumCodeInput() { ScriptoriumCode = masterEntity.ScriptoriumId }, cancellationToken);
            }
            // Safely extract the NationalNo first
            var nationalNo = accountExordiumRes.Data?.NationalNo;
            if (string.IsNullOrWhiteSpace(nationalNo))
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("شماره ملی معتبر یافت نشد.");
                return;
            }
            VatInfo vatInfo = null;
            if (masterEntity.ScriptoriumId == "52539" || masterEntity.ScriptoriumId == "57999")
            {
                vatInfo = await _vatInfoRepository.GetAsync(x => x.NationalNo == "0067071554", cancellationToken);
            }
            else
            {
                vatInfo = await _vatInfoRepository.GetAsync(x => x.NationalNo == _userService.UserApplicationContext.User.UserName, cancellationToken);
            }
            if (vatInfo is null)
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("اطلاعات مالیاتی سردفتر یافت نشد");
                return;
            }
            Vat vat = new();

            vat.CodeMelli = vatInfo.NationalNo;
            vat.FollowCode = vatInfo.TaxCode;
            if (accountExordiumRes.Data != null && accountExordiumRes.IsSuccess && !string.IsNullOrWhiteSpace(accountExordiumRes?.Data?.ProvinceId))
            {
                provinceId = accountExordiumRes.Data.ProvinceId;
            }
            else
            {
                provinceId = baseInfoapiResult.Data.ProvinceId;
            }
            CreateInvoiceServiceInput createInvoiceServiceInput = new()
            {
                BusinessObjectId = masterEntity.Id.ToString(),
                UnitId = accountExordiumRes.Data.UnitId,
                UserNationalNo = accountExordiumRes.Data.NationalNo,
                OrganizationId = masterEntity.ScriptoriumId,
                ProvinceId = provinceId,
                NationalNo = masterEntity.ReqNo,
                Title = string.Format("گواهی امضا به شماره {0}", masterEntity.ReqNo),
                Description = "",
                ExtraParam = "",
                ReturnURL = _configuration.GetValue<string>("BaseUIURL"),
                ServiceName = "Notary.SSAR.SignRequest"
            };
            createInvoiceServiceInput.ExordiumShebaSarDaftar = accountExordiumRes.Data.ShebaNo;
            createInvoiceServiceInput.Vat = vat;
            //هزینه ها با استفاده از فایل دریافتی از اقای نقوی در تاریخ 1404/05/30 بروز شد.

            //حق صدور
            Quotation sodor = new();
            sodor.CostTypeID = "14";

            //سایر درامد ها
            Quotation otherIncome = new();
            otherIncome.CostTypeID = "17";
            otherIncome.DetailPrice = SignRequestConstants.otherIncomePrice.ToString();
            if (masterEntity.ScriptoriumId == "52539" || masterEntity.ScriptoriumId == "57999")
            {
                sodor.DetailPrice = SignRequestConstants.sodorTEST.ToString();
                otherIncome.DetailPrice = SignRequestConstants.otherIncomePriceTEST.ToString();

            }


            decimal tahrirPrice = 0;
            decimal sodorPrice = 0;
            foreach (var item in masterEntity.SignRequestPeople.Where(x => x.IsOriginal == "1").ToList())
            {
                if (masterEntity.ScriptoriumId == "52539" || masterEntity.ScriptoriumId == "57999")
                {
                    tahrirPrice += SignRequestConstants.tahrirPricePerPersonTEST;
                    sodorPrice += SignRequestConstants.sodorTEST;
                }
                else
                {
                    tahrirPrice += SignRequestConstants.tahrirPricePerPerson;
                    sodorPrice += SignRequestConstants.sodor;
                }

            }
            sodor.DetailPrice = sodorPrice.ToString();

            //10 درصد کانون
            Quotation tenPercent = new();
            tenPercent.CostTypeID = "25";
            tenPercent.DetailPrice = (tahrirPrice / 10).ToString();

            //ارزش افزوده
            Quotation addedTax = new();
            addedTax.CostTypeID = "9";
            addedTax.DetailPrice = (tahrirPrice / 10).ToString();

            //حق التحریر
            Quotation tahrir = new();
            tahrir.CostTypeID = "2";
            tahrir.DetailPrice = (tahrirPrice - (decimal.Parse(tenPercent.DetailPrice))).ToString();

            createInvoiceServiceInput.Quotation.Add(tahrir);
            createInvoiceServiceInput.Quotation.Add(otherIncome);
            createInvoiceServiceInput.Quotation.Add(sodor);
            createInvoiceServiceInput.Quotation.Add(addedTax);
            createInvoiceServiceInput.Quotation.Add(tenPercent);


            decimal totalPrice = tahrirPrice + otherIncome.DetailPrice.ToDecimal() + sodor.DetailPrice.ToDecimal() + addedTax.DetailPrice.ToDecimal();

            masterEntity.SumPrices = (int)totalPrice;
            masterEntity.PaymentType = "PCPOS";

            var callResult = await _mediator.Send(createInvoiceServiceInput, cancellationToken);
            if (callResult?.IsSuccess == true)
            {
                masterEntity.IsCostPaid = "3";
                masterEntity.BillNo = callResult.Data.PaymentNo;
                apiResult.Data.PaymentLink = callResult.Data.RedirectLink;
                apiResult.Data.BillNo = masterEntity.BillNo;
                apiResult.Data.PaymentType = masterEntity.PaymentType;
                apiResult.Data.SumPrices = masterEntity.SumPrices.ToString();
                apiResult.Data.IsCostPaid = "3";
                apiResult.IsSuccess = true;
            }
            else
            {
                if (callResult.message.Count != 0)
                {
                    apiResult.message.Add(callResult.message[0]);

                }
                else
                {
                    apiResult.message.Add("مشکلی در سامانه پرداخت به وجود امده است لطفا پس از مدتی دوباره تلاش کنید.");
                }
                apiResult.Data.BillNo = masterEntity.BillNo;
                apiResult.Data.PaymentType = masterEntity.PaymentType;
                apiResult.Data.SumPrices = masterEntity.SumPrices.ToString();
                apiResult.Data.IsCostPaid = "3";
                apiResult.IsSuccess = false;
            }

        }
        private async Task CalculateCostForRemoteRequest(CancellationToken cancellationToken)
        {
            var provinceId = string.Empty;
            var accountExordiumInquiry = new GetAccountInfoByScriptoriumCodeInput();
            accountExordiumInquiry.ScriptoriumCode = masterEntity.ScriptoriumId;
            var accountExordiumRes = await _mediator.Send(accountExordiumInquiry, cancellationToken);
            if (masterEntity.ScriptoriumId == "52539" || masterEntity.ScriptoriumId == "57999")
            {
                accountExordiumRes = await _mediator.Send(new GetAccountInfoByScriptoriumCodeInput() { ScriptoriumCode = "52539" }, cancellationToken);
            }
            else
            {
                accountExordiumRes = await _mediator.Send(new GetAccountInfoByScriptoriumCodeInput() { ScriptoriumCode = masterEntity.ScriptoriumId }, cancellationToken);
            }
            // Safely extract the NationalNo first
            var nationalNo = accountExordiumRes.Data?.NationalNo;
            if (string.IsNullOrWhiteSpace(nationalNo))
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("شماره ملی معتبر یافت نشد.");
                apiResult.message.AddRange(accountExordiumRes.message);
                return;
            }
            VatInfo vatInfo = null;
            if (masterEntity.ScriptoriumId == "52539" || masterEntity.ScriptoriumId == "57999")
            {
                vatInfo = await _vatInfoRepository.GetAsync(x => x.NationalNo == "0067071554", cancellationToken);
            }
            else
            {
                vatInfo = await _vatInfoRepository.GetAsync(x => x.NationalNo == _userService.UserApplicationContext.User.UserName, cancellationToken);
            }
            if (vatInfo is null)
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("اطلاعات مالیاتی سردفتر یافت نشد");
                return;
            }
            Vat vat = new();

            vat.CodeMelli = vatInfo.NationalNo;
            vat.FollowCode = vatInfo.TaxCode;
            if (baseInfoapiResult.IsSuccess && baseInfoapiResult.Data != null && baseInfoapiResult.Data.ProvinceId != null)
            {
                provinceId = baseInfoapiResult.Data.ProvinceId;
            }
            else
            {
                provinceId = accountExordiumRes.Data.ScriptoriumId;
            }
            CreateInvoiceServiceInput createInvoiceServiceInput = new()
            {
                BusinessObjectId = masterEntity.Id.ToString(),
                UnitId = accountExordiumRes.Data.UnitId,
                UserNationalNo = accountExordiumRes.Data.NationalNo,
                OrganizationId = accountExordiumRes.Data.ScriptoriumId,
                ProvinceId = accountExordiumRes.Data.ProvinceId,
                NationalNo = masterEntity.ReqNo,
                Title = string.Format("گواهی امضا به شماره {0}", masterEntity.ReqNo),
                Description = "",
                ExtraParam = "",
                ReturnURL = _configuration.GetValue<string>("BaseKatebUIURL") + masterEntity.ReqNo,
                ServiceName = "Notary.SSAR.RemoteSignRequest"
            };
            createInvoiceServiceInput.ExordiumShebaSarDaftar = accountExordiumRes.Data.ShebaNo;
            createInvoiceServiceInput.Vat = vat;

            //هزینه ها با استفاده از فایل دریافتی از اقای نقوی در تاریخ 1404/05/30 بروز شد.
            //حق صدور
            Quotation sodor = new();
            sodor.CostTypeID = "14";
            sodor.DetailPrice = SignRequestConstants.sodor.ToString();

            //سایر درامد ها
            Quotation otherIncome = new();
            otherIncome.CostTypeID = "17";
            otherIncome.DetailPrice = SignRequestConstants.otherIncomePrice.ToString();
            if (masterEntity.ScriptoriumId == "52539" || masterEntity.ScriptoriumId == "57999")
            {
                sodor.DetailPrice = SignRequestConstants.sodorTEST.ToString();
                otherIncome.DetailPrice = SignRequestConstants.otherIncomePriceTEST.ToString();

            }


            decimal tahrirPrice = 0;
            foreach (var item in masterEntity.SignRequestPeople.Where(x => x.IsOriginal == "1").ToList())
            {
                if (masterEntity.ScriptoriumId == "52539" || masterEntity.ScriptoriumId == "57999")
                {
                    tahrirPrice += SignRequestConstants.tahrirPricePerPersonTEST;
                }
                else
                {
                    tahrirPrice += SignRequestConstants.tahrirPricePerPerson;
                }

            }
            //10 درصد کانون
            Quotation tenPercent = new();
            tenPercent.CostTypeID = "25";
            tenPercent.DetailPrice = (tahrirPrice / 10).ToString();

            //ارزش افزوده
            Quotation addedTax = new();
            addedTax.CostTypeID = "9";
            addedTax.DetailPrice = (tahrirPrice / 10).ToString();

            //حق التحریر
            Quotation tahrir = new();
            tahrir.CostTypeID = "2";
            tahrir.DetailPrice = (tahrirPrice - (decimal.Parse(tenPercent.DetailPrice))).ToString();

            createInvoiceServiceInput.Quotation.Add(tahrir);
            createInvoiceServiceInput.Quotation.Add(otherIncome);
            createInvoiceServiceInput.Quotation.Add(sodor);
            createInvoiceServiceInput.Quotation.Add(addedTax);
            createInvoiceServiceInput.Quotation.Add(tenPercent);


            decimal totalPrice = tahrirPrice + otherIncome.DetailPrice.ToDecimal() + sodor.DetailPrice.ToDecimal() + addedTax.DetailPrice.ToDecimal();

            masterEntity.SumPrices = (int)totalPrice;
            masterEntity.PaymentType = "IPG";

            var callResult = await _mediator.Send(createInvoiceServiceInput, cancellationToken);
            if (callResult.IsSuccess)
            {
                masterEntity.IsCostPaid = "3";
                masterEntity.BillNo = callResult.Data.PaymentNo;
                apiResult.Data.PaymentLink = callResult.Data.RedirectLink;
                apiResult.Data.BillNo = masterEntity.BillNo;
                apiResult.Data.PaymentType = masterEntity.PaymentType;
                apiResult.Data.SumPrices = masterEntity.SumPrices.ToString();
                apiResult.Data.IsCostPaid = "3";
                apiResult.IsSuccess = true;
            }
            else
            {
                apiResult.Data.BillNo = masterEntity.BillNo;
                apiResult.Data.PaymentType = masterEntity.PaymentType;
                apiResult.Data.SumPrices = masterEntity.SumPrices.ToString();
                apiResult.Data.IsCostPaid = "3";
                apiResult.IsSuccess = false;
                if (callResult.message.Count != 0)
                {
                    apiResult.message.Add(callResult.message[0]);

                }
                else
                {
                    apiResult.message.Add("مشکلی در سامانه پرداخت به وجود امده است لطفا پس از مدتی دوباره تلاش کنید.");
                }
            }
        }
        private async Task CalculateCostForBehPardazRemoteRequest(CancellationToken cancellationToken)
        {
            var accountExordiumInquiry = new GetAccountInfoByScriptoriumCodeInput();
            accountExordiumInquiry.ScriptoriumCode = masterEntity.ScriptoriumId;
            var accountExordiumRes = await _mediator.Send(accountExordiumInquiry, cancellationToken);
            if (!accountExordiumRes.IsSuccess)
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("شماره شبا سردفتر یافت نشد.");
                return;
            }

            //var vatInfo = await _vatInfoRepository.GetAsync(x => x.NationalNo == _userService.UserApplicationContext.User.UserName, cancellationToken);
            //if (vatInfo is null)
            //{
            //    apiResult.IsSuccess = false;
            //    apiResult.message.Add("اطلاعات مالیاتی سردفتر یافت نشد.");
            //    return;
            //}
            //Vat vat = new();
            //vat.CodeMelli = vatInfo.NationalNo;
            //vat.FollowCode = vatInfo.TaxAccountNo;

            PaymentQuotation test = new();
            test.CostTypeID = "1008";
            test.DetailPrice = "11000";
            test.ExtensionData = "";
            test.IsMaliyati = false;
            test.IsSetadi = false;
            test.AccountNo = "IR020100004062013201686144";


            PaymentQuotation sodor = new();
            sodor.CostTypeID = "14";
            sodor.DetailPrice = SignRequestConstants.sodorTEST.ToString();
            sodor.IsMaliyati = false;
            sodor.IsSetadi = false;
            sodor.AccountNo = accountExordiumRes.Data.ShebaNo;
            decimal tahrirPrice = 0;
            decimal otherIncomePrice = 0;
            foreach (var item in masterEntity.SignRequestPeople)
            {

                otherIncomePrice += SignRequestConstants.otherIncomePriceTEST;
                tahrirPrice += SignRequestConstants.tahrirPricePerPersonTEST;
            }

            PaymentQuotation tenPercent = new();
            tenPercent.CostTypeID = "25";
            tenPercent.DetailPrice = (tahrirPrice / 10).ToString();
            tenPercent.IsMaliyati = false;
            tenPercent.IsSetadi = false;
            tenPercent.AccountNo = accountExordiumRes.Data.ShebaNo;

            PaymentQuotation addedTax = new();
            addedTax.CostTypeID = "9";
            addedTax.DetailPrice = (tahrirPrice / 10).ToString();
            addedTax.IsMaliyati = false;
            addedTax.IsSetadi = false;
            addedTax.AccountNo = accountExordiumRes.Data.ShebaNo;

            //سایر درامد ها
            PaymentQuotation otherIncome = new();
            otherIncome.CostTypeID = "17";
            otherIncome.DetailPrice = otherIncomePrice.ToString();
            otherIncome.IsMaliyati = false;
            otherIncome.IsSetadi = false;
            otherIncome.AccountNo = accountExordiumRes.Data.ShebaNo;

            PaymentQuotation tahrir = new();
            tahrir.CostTypeID = "2";
            tahrir.DetailPrice = (tahrirPrice - (decimal.Parse(tenPercent.DetailPrice))).ToString();
            tahrir.IsMaliyati = false;
            tahrir.IsSetadi = false;
            tahrir.AccountNo = accountExordiumRes.Data.ShebaNo;



            decimal totalPrice = tahrirPrice + otherIncome.DetailPrice.ToDecimal() + sodor.DetailPrice.ToDecimal() + addedTax.DetailPrice.ToDecimal();


            IPGPaymentRequestServiceInput paymentRequest = new IPGPaymentRequestServiceInput
            {
                SystemRequestID = masterEntity.ReqNo,
                RecordKey = masterEntity.ReqNo,
                ServiceCode = "digital sign",
                Amount = 11000, // Original sum logic
                BriefDesc = string.Format("گواهی امضا به شماره {0}", masterEntity.ReqNo), // Same as Title
                DocTypeID = "910",
                HowtoPay = 0,
                Title = string.Format("گواهی امضا به شماره {0}", masterEntity.ReqNo),
                RedirectAddress = _configuration.GetValue<string>("BaseKatebUIURL"),
                Email = "",
                Name = accountExordiumRes.Data.ExordiumFullName,
                Mobile = "", // Not available
                NationalCode = accountExordiumRes.Data.NationalNo,
                PspCode = 0, // Default
                HowToQuotationWithVerhofe = "",
                ClientId = "SSAR"// Default
            };
            //paymentRequest.LstQuotation.Add(tahrir);
            //paymentRequest.LstQuotation.Add(otherIncome);
            //paymentRequest.LstQuotation.Add(sodor);
            //paymentRequest.LstQuotation.Add(addedTax);
            paymentRequest.LstQuotation.Add(test);

            var callResult = await _mediator.Send(paymentRequest, cancellationToken);

            masterEntity.SumPrices = (int)totalPrice;

            if (callResult.IsSuccess)
            {
                if (callResult.Data.ResponseCode == 0)
                {
                    masterEntity.IsCostPaid = "3";
                    masterEntity.BillNo = callResult.Data.TrackingCode;
                    apiResult.Data.PaymentLink = callResult.Data.PaymentLink;
                    apiResult.Data.BillNo = masterEntity.BillNo;
                    apiResult.Data.PaymentType = "IPG";
                    apiResult.Data.SumPrices = masterEntity.SumPrices.ToString();
                    apiResult.Data.IsCostPaid = "3";
                    apiResult.IsSuccess = true;
                }
                else
                {
                    apiResult.Data.BillNo = masterEntity.BillNo;
                    apiResult.Data.PaymentType = "IPG";
                    apiResult.Data.SumPrices = masterEntity.SumPrices.ToString();
                    apiResult.Data.IsCostPaid = "3";
                    apiResult.IsSuccess = false;
                    apiResult.message.Add(callResult.Data.ResponseDescription);
                }

            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("مشکلی در پرداخت به وجود آمده است .");
                apiResult.statusCode = ApiResultStatusCode.ServerError;
            }
        }
        private async Task InquiryFromBehPardaz(CancellationToken cancellationToken)
        {
            IPGPaymentDetailServiceInput iPGPaymentDetailServiceInput = new();
            iPGPaymentDetailServiceInput.TrackingCode = masterEntity.BillNo;
            iPGPaymentDetailServiceInput.SystemRequestId = masterEntity.ReqNo;
            iPGPaymentDetailServiceInput.ClientId = "SSAR";
            var paymentInquiryResult = await _mediator.Send(iPGPaymentDetailServiceInput, cancellationToken);

            if (paymentInquiryResult.Data.TranSuccessful == "Y")
            {
                masterEntity.IsCostPaid = "1";
                masterEntity.PayCostDate = paymentInquiryResult.Data.TransactionDateTime.ToPersianDate();
                masterEntity.PayCostTime = paymentInquiryResult.Data.TransactionDateTime.TimeOfDay.ToString();
                masterEntity.ReceiptNo = paymentInquiryResult.Data.TraceNO;
                masterEntity.BillNo = paymentInquiryResult.Data.TrackingCode;
                masterEntity.SumPrices = paymentInquiryResult.Data.Amount;
                masterEntity.PaymentType = "IPG";
                await _signRequestRepository.UpdateAsync(masterEntity, cancellationToken);

                apiResult.Data.IsCostPaid = masterEntity.IsCostPaid;
                apiResult.Data.PayCostDate = masterEntity.PayCostDate;
                apiResult.Data.PayCostTime = masterEntity.PayCostTime;
                apiResult.Data.BillNo = masterEntity.BillNo;
                apiResult.Data.ReceiptNo = masterEntity.ReceiptNo;
                apiResult.Data.SumPrices = masterEntity.SumPrices.ToString();
                apiResult.Data.PaymentType = masterEntity.PaymentType;
                apiResult.message.Add("پرداخت قبلا انجام شده است . ");
                apiResult.IsSuccess = true;

            }
            else
            {
                masterEntity.IsCostPaid = "2";
                apiResult.Data.IsCostPaid = masterEntity.IsCostPaid;
                apiResult.Data.PayCostDate = masterEntity.PayCostDate;
                apiResult.Data.PayCostTime = masterEntity.PayCostTime;
                apiResult.Data.BillNo = masterEntity.BillNo;
                apiResult.Data.SumPrices = masterEntity.SumPrices.ToString();
                apiResult.Data.PaymentType = masterEntity.PaymentType;
                await _signRequestRepository.UpdateAsync(masterEntity, cancellationToken);
                apiResult.IsSuccess = true;

            }
        }
        private async Task InquiryPayment(CancellationToken cancellationToken)
        {
            InquiryPaymentServiceInput paymentInquirySignRequest = new()
            {
                NationalNo = masterEntity.ReqNo,
            };
            var paymentInquiryResult = await _mediator.Send(paymentInquirySignRequest, cancellationToken);
            if (paymentInquiryResult?.IsSuccess == true)
            {
                if (paymentInquiryResult?.Data?.State == "2")
                {
                    masterEntity.IsCostPaid = "1";
                    masterEntity.PayCostDate = paymentInquiryResult.Data.PaymentDate;
                    masterEntity.PayCostTime = paymentInquiryResult.Data.PaymentTime;
                    masterEntity.ReceiptNo = paymentInquiryResult.Data.PaymentNo;
                    masterEntity.BillNo = paymentInquiryResult.Data.No;
                    masterEntity.SumPrices = paymentInquiryResult.Data.Price.ToInt();
                    await _signRequestRepository.UpdateAsync(masterEntity, cancellationToken);

                    apiResult.Data.IsCostPaid = masterEntity.IsCostPaid;
                    apiResult.Data.PayCostDate = masterEntity.PayCostDate;
                    apiResult.Data.PayCostTime = masterEntity.PayCostTime;
                    apiResult.Data.BillNo = masterEntity.BillNo;
                    apiResult.Data.ReceiptNo = masterEntity.ReceiptNo;
                    apiResult.Data.SumPrices = masterEntity.SumPrices.ToString();
                    apiResult.Data.PaymentType = masterEntity.PaymentType;
                    apiResult.message.Add("پرداخت قبلا انجام شده است . ");
                    apiResult.IsSuccess = true;
                }
                else
                {
                    masterEntity.IsCostPaid = "2";

                    apiResult.Data.IsCostPaid = masterEntity.IsCostPaid;
                    apiResult.Data.PayCostDate = masterEntity.PayCostDate;
                    apiResult.Data.PayCostTime = masterEntity.PayCostTime;
                    apiResult.Data.BillNo = masterEntity.BillNo;
                    apiResult.Data.SumPrices = masterEntity.SumPrices.ToString();
                    apiResult.Data.PaymentType = masterEntity.PaymentType;
                    await _signRequestRepository.UpdateAsync(masterEntity, cancellationToken);
                    apiResult.IsSuccess = true;

                }
            }
            else
            {
                apiResult.Data.IsCostPaid = masterEntity.IsCostPaid;
                apiResult.Data.PayCostDate = masterEntity.PayCostDate;
                apiResult.Data.PayCostTime = masterEntity.PayCostTime;
                apiResult.Data.BillNo = masterEntity.BillNo;
                apiResult.Data.SumPrices = masterEntity.SumPrices.ToString();
                apiResult.Data.PaymentType = masterEntity.PaymentType;
                await _signRequestRepository.UpdateAsync(masterEntity, cancellationToken);
                apiResult.IsSuccess = false;
            }
        }
        private void FakePay()
        {
            masterEntity.IsCostPaid = "1";
            masterEntity.PayCostDate = _dateTimeService.CurrentPersianDate;
            masterEntity.PayCostTime = _dateTimeService.CurrentTime;
            masterEntity.ReceiptNo = RandomNumberGenerator();
            masterEntity.BillNo = masterEntity.ReceiptNo;
            masterEntity.PaymentType = "PCPos";
        }
        private static string RandomNumberGenerator()
        {

            Random generator = new();
            return generator.Next(0, 1000000).ToString("D6");
        }
    }
}

