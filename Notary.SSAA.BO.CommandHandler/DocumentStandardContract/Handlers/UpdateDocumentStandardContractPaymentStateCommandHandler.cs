using MediatR;
using Microsoft.Extensions.Configuration;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.DocumentStandardContract;
using Notary.SSAA.BO.DataTransferObject.Mappers.Document;
using Notary.SSAA.BO.DataTransferObject.Mappers.DocumentStandardContract;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.EPayment;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.Exordium;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExordiumAccount;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RichDomain.Document;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using Serilog;

namespace Notary.SSAA.BO.CommandHandler.DocumentStandardContract.Handlers
{
    internal class UpdateDocumentStandardContractPaymentStateCommandHandler : BaseCommandHandler<UpdateDocumentStandardContractPaymentStateCommand, ApiResult<UpdateDocumentStandardContractPaymentStateViewModel>>
    {
        private Domain.Entities.Document masterEntity;
        private readonly IDocumentRepository _documentRepository;
        private ApiResult<UpdateDocumentStandardContractPaymentStateViewModel> apiResult;
        private readonly IConfiguration _configuration;
        private readonly IDateTimeService _dateTimeService;
        private readonly IRepository<VatInfo> _vatInfoRepository;
        private readonly IRepository<DocumentPayment> _documentPaymentRepository;


        public UpdateDocumentStandardContractPaymentStateCommandHandler(IMediator mediator, IUserService userService, IDocumentRepository documentRepository, IRepository<DocumentPayment> documentPaymentRepository,
            IConfiguration configuration, IDateTimeService dateTimeService, ILogger logger, IRepository<VatInfo> vatInfoRepository) : base(mediator, userService, logger)
        {
            _configuration = configuration;
            _dateTimeService = dateTimeService;
            _documentRepository = documentRepository;
            _documentPaymentRepository = documentPaymentRepository;
            _vatInfoRepository = vatInfoRepository ?? throw new ArgumentNullException(nameof(vatInfoRepository));
            apiResult = new();
            apiResult.Data = new();

        }
        protected override bool HasAccess(UpdateDocumentStandardContractPaymentStateCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Admin) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<UpdateDocumentStandardContractPaymentStateViewModel>> ExecuteAsync(UpdateDocumentStandardContractPaymentStateCommand request, CancellationToken cancellationToken)
        {
            masterEntity = await _documentRepository.GetDocument(request.DocumentNo, cancellationToken);
            if (masterEntity != null)
            {
                if (_configuration.GetValue<string>("PublishEnv") != null && _configuration.GetValue<string>("PublishEnv") != PublishEnv.Blue)
                {
                    FakePay();
                    await _documentRepository.UpdateAsync(masterEntity, cancellationToken);
                    apiResult.Data.IsCostPaid = CostPaymentConfirmed.Paid.ToAssignedValue();
                    apiResult.IsSuccess = true;
                    apiResult.message.Add("پرداخت سند با موفقیت انجام شد");
                    return apiResult;
                }
                InquiryPaymentServiceInput paymentInquiryDocument = new() { NationalNo = masterEntity.RequestNo };
                var paymentInquiryResult = await _mediator.Send(paymentInquiryDocument, cancellationToken);
                if (paymentInquiryResult.IsSuccess)
                {
                    if (paymentInquiryResult.Data.State == PaymentInquiryResult.Paid.ToAssignedValue())
                    {
                        #region Document
                        masterEntity.CostPaymentDate = paymentInquiryResult.Data.PaymentDate;
                        masterEntity.CostPaymentTime = paymentInquiryResult.Data.PaymentTime;
                        masterEntity.ReceiptNo = paymentInquiryResult.Data.PaymentNo;
                        masterEntity.BillNo = paymentInquiryResult.Data.No;
                        masterEntity.PaymentType = paymentInquiryResult.Data.PaymentType != null ? paymentInquiryResult.Data.PaymentType : "PCPos";
                        await _documentRepository.UpdateAsync(masterEntity, cancellationToken);
                        apiResult.IsSuccess = true;
                        apiResult.message.Add("پرداخت سند با موفقیت انجام شد");
                        #endregion

                        #region DocumentPayment
                        var GetDocumentPayment = await _documentRepository.GetDocumentPayments(masterEntity.Id, cancellationToken);
                        DocumentPayment documentPayment = null;

                        if (GetDocumentPayment != null && GetDocumentPayment.DocumentPayments != null)
                            documentPayment = GetDocumentPayment.DocumentPayments.Where(x => x.No == paymentInquiryResult.Data.No).FirstOrDefault();


                        string ScriptoriumId = masterEntity.ScriptoriumId;
                        if (masterEntity.ScriptoriumId == "52539" || masterEntity.ScriptoriumId == "57999")
                            ScriptoriumId = "52539";

                        if (documentPayment == null)
                        {
                            documentPayment = new DocumentPayment();
                            DocumentStandardContractPaymentMapper.MapToInquiryPayment(ref documentPayment, paymentInquiryResult.Data, masterEntity.Id, Guid.NewGuid(), ScriptoriumId);
                            await _documentPaymentRepository.AddAsync(documentPayment, cancellationToken);
                        }
                        else
                        {
                            DocumentStandardContractPaymentMapper.MapToInquiryPayment(ref documentPayment, paymentInquiryResult.Data, masterEntity.Id, documentPayment.Id, ScriptoriumId);
                            await _documentPaymentRepository.UpdateAsync(documentPayment, cancellationToken);
                        }
                        #endregion
                    }
                    else if (request.InquiryMode)
                    {
                        apiResult.IsSuccess = true;
                        apiResult.message.Add("سند پرداخت نشد");
                        return apiResult;
                    }
                    else
                    {
                        apiResult = await CalculateCostForRemoteDocument(cancellationToken);
                    }


                    apiResult.Data.ReceiptNo = masterEntity.ReceiptNo;
                    apiResult.Data.PayCostDate = masterEntity.CostPaymentDate;
                    apiResult.Data.PayCostTime = masterEntity.CostPaymentTime;
                    apiResult.Data.BillNo = masterEntity.BillNo;
                    apiResult.Data.SumPrices = masterEntity.SumPrices().ToString();
                    apiResult.Data.PaymentType = masterEntity.PaymentType;
                    apiResult.IsSuccess = true;
                    return apiResult;
                }
                else
                {
                    apiResult.IsSuccess = false;
                    //apiResult.message.AddRange(paymentInquiryResult.message);
                    apiResult.message.Add("استعلام پرداخت با خطا مواجه شد");
                    return apiResult;

                }
            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("دریافت اطلاعات سند با خطا مواجه شد");
                return apiResult;
            }
        }

        private async Task<ApiResult<UpdateDocumentStandardContractPaymentStateViewModel>> CalculateCostForRemoteDocument(CancellationToken cancellationToken)
        {
            ApiResult<GetAccountInfoByScriptoriumCodeViewModel> accountExordiumRes = null;
            if (masterEntity.ScriptoriumId == "52539" || masterEntity.ScriptoriumId == "57999")
            {
                accountExordiumRes = await _mediator.Send(new GetAccountInfoByScriptoriumCodeInput() { ScriptoriumCode = "52539" }, cancellationToken);
            }
            else
            {
                accountExordiumRes = await _mediator.Send(new GetAccountInfoByScriptoriumCodeInput() { ScriptoriumCode = masterEntity.ScriptoriumId }, cancellationToken);
            }
            #region Validation
            if (!accountExordiumRes.IsSuccess || accountExordiumRes.Data == null)
            {
                apiResult.IsSuccess = false;
                apiResult.message = accountExordiumRes.message;
                return apiResult;
            }

            var nationalNo = accountExordiumRes.Data?.NationalNo;
            if (string.IsNullOrWhiteSpace(nationalNo))
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("شماره ملی معتبر یافت نشد.");
                apiResult.message.AddRange(accountExordiumRes.message);
                return apiResult;
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
                return apiResult;
            }
            #endregion

            CreateInvoiceServiceInput createInvoiceServiceInput = new()
            {
                BusinessObjectId = masterEntity.Id.ToString(),
                NationalNo = masterEntity.RequestNo,
                UnitId = accountExordiumRes.Data.UnitId,
                UserNationalNo = accountExordiumRes.Data.NationalNo,
                OrganizationId = accountExordiumRes.Data.ScriptoriumId,
                ProvinceId = accountExordiumRes.Data.ProvinceId,
                ExordiumShebaSarDaftar = accountExordiumRes.Data.ShebaNo,
                ReturnURL = masterEntity.IsRemoteRequest == YesNo.Yes.ToAssignedValue() ? _configuration.GetValue<string>("BaseKatebUIURL") + masterEntity.RequestNo : _configuration.GetValue<string>("BaseUIURL"),
                ServiceName = "Notary.SSAR.Document",
                Title = string.Format("سند به شماره {0}", masterEntity.RequestNo),
                Description = string.Empty,
                ExtraParam = string.Empty,
                Vat = new() { CodeMelli = vatInfo.NationalNo, FollowCode = vatInfo.TaxAccountNo },
            };

            string[] CostTypeSardaftar = new string[] { "3", "4", "5", "6", "7", "8" };
            var SumPriceSardaftar = masterEntity.DocumentCosts.Where(x => CostTypeSardaftar.Contains(x.CostTypeId)).Select(x => (decimal)x.Price).Sum();

            //createInvoiceServiceInput.ProvinceId = createInvoiceServiceInput.ProvinceId != "443" ? createInvoiceServiceInput.ProvinceId : "250";
            if (createInvoiceServiceInput.OrganizationId == "52539" || createInvoiceServiceInput.OrganizationId == "57999")
            {
                createInvoiceServiceInput.Quotation.AddRange(masterEntity.DocumentCosts.Select(x => new Quotation { CostTypeID = x.CostTypeId, DetailPrice = "4000" }));
            }
            else
            {
                createInvoiceServiceInput.Quotation.AddRange(masterEntity.DocumentCosts.Select(x => new Quotation { CostTypeID = x.CostTypeId, DetailPrice = x.Price.ToString() }));
            }
            var InvoiceResult = await _mediator.Send(createInvoiceServiceInput, cancellationToken);
            apiResult.Data = new();
            apiResult.Data.BillNo = masterEntity.BillNo;
            apiResult.Data.PaymentType = masterEntity.IsRemoteRequest == YesNo.Yes.ToAssignedValue() ? "IPG" : "PCPOS";
            apiResult.Data.SumPrices = createInvoiceServiceInput.Quotation.Sum(x => x.DetailPrice.ToDecimal()).ToString();
            apiResult.Data.IsCostPaid = CostPaymentConfirmed.Uncertain.ToAssignedValue();

            if (InvoiceResult.IsSuccess)
            {
                masterEntity.BillNo = InvoiceResult.Data.PaymentNo;
                apiResult.Data.PaymentLink = InvoiceResult.Data.RedirectLink;
                apiResult.IsSuccess = true;
            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.message = InvoiceResult.message;
            }
            return apiResult;
        }

        private void FakePay()
        {
            //masterEntity.State = DocumentState.CalculatePay.ToAssignedValue();
            masterEntity.IsCostPaymentConfirmed = YesNo.Yes.ToAssignedValue();
            masterEntity.CostPaymentDate = _dateTimeService.CurrentPersianDate;
            masterEntity.CostPaymentTime = _dateTimeService.CurrentTime;
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
