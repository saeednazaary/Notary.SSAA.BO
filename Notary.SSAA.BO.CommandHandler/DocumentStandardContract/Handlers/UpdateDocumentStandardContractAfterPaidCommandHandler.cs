using MediatR;
using Microsoft.Extensions.Configuration;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.DocumentStandardContract;
using Notary.SSAA.BO.DataTransferObject.Mappers.Document;
using Notary.SSAA.BO.DataTransferObject.Mappers.DocumentStandardContract;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.EPayment;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using Serilog;

namespace Notary.SSAA.BO.CommandHandler.DocumentStandardContract.Handlers
{
    internal class UpdateDocumentStandardContractAfterPaidCommandHandler : BaseCommandHandler<UpdateDocumentStandardContractAfterPaidCommand, ApiResult<UpdateDocumentStandardContractAfterPaidViewModel>>
    {
        private Domain.Entities.Document masterEntity;
        private readonly IDocumentRepository _documentRepository;
        private ApiResult<UpdateDocumentStandardContractAfterPaidViewModel> apiResult;
        private readonly IConfiguration _configuration;
        private readonly IDateTimeService _dateTimeService;
        private readonly IRepository<VatInfo> _vatInfoRepository;
        private readonly IRepository<DocumentPayment> _documentPaymentRepository;

        public UpdateDocumentStandardContractAfterPaidCommandHandler(IMediator mediator, IUserService userService, IDocumentRepository documentRepository,
            IConfiguration configuration, IDateTimeService dateTimeService, ILogger logger, IRepository<VatInfo> vatInfoRepository, IRepository<DocumentPayment> documentPaymentRepository) : base(mediator, userService, logger)
        {
            _configuration = configuration;
            _dateTimeService = dateTimeService;
            _documentRepository = documentRepository;
            _vatInfoRepository = vatInfoRepository ?? throw new ArgumentNullException(nameof(vatInfoRepository));
            apiResult = new();
            apiResult.Data = new();
            _documentPaymentRepository = documentPaymentRepository;
        }
        protected override bool HasAccess(UpdateDocumentStandardContractAfterPaidCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Admin) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected override async Task<ApiResult<UpdateDocumentStandardContractAfterPaidViewModel>> ExecuteAsync(UpdateDocumentStandardContractAfterPaidCommand request, CancellationToken cancellationToken)
        {
            masterEntity = await _documentRepository.GetDocument(request.DocumentNo, cancellationToken);
            if (masterEntity != null)
            {
                InquiryPaymentServiceInput paymentInquiryDocument = new() { NationalNo = masterEntity.RequestNo, };
                var paymentInquiryResult = await _mediator.Send(paymentInquiryDocument, cancellationToken);
                if (paymentInquiryResult.IsSuccess)
                {
                    if (paymentInquiryResult.Data.State == PaymentInquiryResult.Paid.ToAssignedValue())
                    {
                        #region Document
                        masterEntity.IsCostPaymentConfirmed = CostPaymentConfirmed.Paid.ToAssignedValue();
                        masterEntity.State = DocumentState.CalculatePay.ToAssignedValue();
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
                        DocumentPayment documentPayment = masterEntity.DocumentPayments.Where(x => x.No == paymentInquiryResult.Data.No).FirstOrDefault();
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


                        #region DocumentPayment
                        documentPayment.Id = documentPayment == null ? Guid.NewGuid() : documentPayment.Id;
                        documentPayment.No = paymentInquiryResult.Data.No;
                        documentPayment.DocumentId = masterEntity.Id;
                        documentPayment.PaymentDate = paymentInquiryResult.Data.PaymentDate;
                        documentPayment.PaymentTime = paymentInquiryResult.Data.PaymentTime;
                        documentPayment.PaymentType = paymentInquiryResult.Data.PaymentType != null ? paymentInquiryResult.Data.PaymentType : "PCPos";
                        documentPayment.PaymentNo = paymentInquiryResult.Data.PaymentNo;
                        documentPayment.CardNo = paymentInquiryResult.Data.CardNo;
                        documentPayment.Price = (long)(Convert.ToDecimal(paymentInquiryResult.Data.Price));
                        documentPayment.HowToPay = HowToPay.Electronic.ToAssignedValue();
                        documentPayment.HowToQuotation = paymentInquiryResult.Data.HowToQuotation;
                        documentPayment.IsReused = YesNo.No.ToAssignedValue();
                        documentPayment.Ilm = "1";



                        if (masterEntity.ScriptoriumId == "52539" || masterEntity.ScriptoriumId == "57999")
                        {
                            documentPayment.ScriptoriumId = "52539";
                        }
                        else
                        {
                            documentPayment.ScriptoriumId = masterEntity.ScriptoriumId;
                        }
                        await _documentPaymentRepository.AddAsync(documentPayment, cancellationToken);
                        #endregion

                        #region apiResult
                        apiResult.Data.IsCostPaymentConfirmed = masterEntity.IsCostPaymentConfirmed;
                        apiResult.Data.State = masterEntity.State;
                        apiResult.Data.CostPaymentDate = masterEntity.CostPaymentDate;
                        apiResult.Data.CostPaymentTime = masterEntity.CostPaymentTime;
                        apiResult.Data.ReceiptNo = masterEntity.ReceiptNo;
                        apiResult.Data.BillNo = masterEntity.BillNo;
                        apiResult.Data.PaymentType = masterEntity.PaymentType;
                        #endregion

                        return apiResult;
                    }
                    else
                    {
                        apiResult.IsSuccess = true;
                        apiResult.message.Add("سند پرداخت نشد");
                        return apiResult;
                    }
                }
                else
                {
                    apiResult.IsSuccess = false;
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
    }
}
