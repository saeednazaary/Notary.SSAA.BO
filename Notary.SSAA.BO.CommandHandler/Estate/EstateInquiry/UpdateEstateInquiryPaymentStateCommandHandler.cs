using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.EstateInquiry;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateInquiry;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.EPayment;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.SharedKernel.AppSettingModel;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.Coordinator.Estate.Helpers;


namespace Notary.SSAA.BO.CommandHandler.Estate.EstateInquiry
{
    public class UpdateEstateInquiryPaymentStateCommandHandler : BaseCommandHandler<UpdateEstateInquiryPaymentStateCommand, ApiResult<UpdateEstateInquiryPaymentStateViewModel>>
    {
        private readonly IEstateInquiryRepository _estateInquiryRepository;
        private protected Domain.Entities.EstateInquiry masterEntity;
        private protected ApiResult<UpdateEstateInquiryPaymentStateViewModel> apiResult;
        private ExternalServiceHelper externalServiceHelper = null;
        private readonly IRepository<Notary.SSAA.BO.Domain.Entities.ConfigurationParameter> _configurationParameterRepository;
        private ConfigurationParameterHelper _configurationParameterHelper = null;
        private readonly IConfiguration _configuration;
        public UpdateEstateInquiryPaymentStateCommandHandler(IMediator mediator, IEstateInquiryRepository estateInquiryRepository, IUserService userService,
            ILogger logger, IDateTimeService dateTimeService, IRepository<Notary.SSAA.BO.Domain.Entities.ConfigurationParameter> repository, IEstateSectionRepository estateSectionRepository, IEstateSubSectionRepository estateSubSectionRepository, IEstateSeriDaftarRepository estateSeriDaftarRepository, IWorkfolwStateRepository workfolwStateRepository, IConfiguration configuration, IHttpEndPointCaller httpEndPointCaller)
            : base(mediator, userService, logger)
        {
            apiResult = new();
            apiResult.Data = new();
            _estateInquiryRepository = estateInquiryRepository;
            _configurationParameterRepository = repository;
            _configuration = configuration;
            _configurationParameterHelper = new ConfigurationParameterHelper(_configurationParameterRepository, mediator);
            externalServiceHelper = new ExternalServiceHelper(mediator,
               dateTimeService,
               userService,
               _configurationParameterHelper,
               estateSeriDaftarRepository,
               estateSectionRepository,
               estateSubSectionRepository,
               configuration,
               httpEndPointCaller,
               workfolwStateRepository,
               null);
        }
        protected override async Task<ApiResult<UpdateEstateInquiryPaymentStateViewModel>> ExecuteAsync(UpdateEstateInquiryPaymentStateCommand request, CancellationToken cancellationToken)
        {
            masterEntity = await _estateInquiryRepository.GetByIdAsync(cancellationToken, request.EstateInquiryId.ToGuid());
            BusinessValidation(request);
            if (apiResult.IsSuccess)
            {
                await _estateInquiryRepository.LoadCollectionAsync(masterEntity, x => x.EstateInquiryPeople, cancellationToken);                
                var credentials = _configuration.GetSection("SignRequestEPaymentCredentials").Get<EPaymentCredentialsModel>();
                InquiryPaymentServiceInput paymentEstateInquiry = new()
                {
                    NationalNo = masterEntity.No2,
                };
                paymentEstateInquiry.Credentials = credentials;
                if (masterEntity.IsCostPaid == "3")
                {
                    var paymentInquiryResult = await _mediator.Send(paymentEstateInquiry, cancellationToken);
                    if (paymentInquiryResult.IsSuccess)
                    {
                        if (paymentInquiryResult.Data.State == "2")
                        {
                            masterEntity.IsCostPaid = "1";
                            masterEntity.PayCostDate = paymentInquiryResult.Data.PaymentDate;
                            masterEntity.PayCostTime = paymentInquiryResult.Data.PaymentTime;
                            masterEntity.ReceiptNo = paymentInquiryResult.Data.PaymentNo;
                            masterEntity.BillNo = paymentInquiryResult.Data.No;
                            masterEntity.SumPrices = paymentInquiryResult.Data.Price.ToInt();
                            masterEntity.PaymentType = "PCPos";
                            await _estateInquiryRepository.UpdateAsync(masterEntity, cancellationToken);
                            apiResult.message.Add("پرداخت قبلا انجام شده است . ");
                            apiResult.IsSuccess = true;
                            apiResult.Data.IsCostPaid = masterEntity.IsCostPaid;
                            apiResult.Data.PayCostDate = masterEntity.PayCostDate;
                            apiResult.Data.PayCostTime = masterEntity.PayCostTime;
                            apiResult.Data.BillNo = masterEntity.BillNo;
                            apiResult.Data.ReceiptNo = masterEntity.ReceiptNo;
                            apiResult.Data.SumPrices = masterEntity.SumPrices.ToString();
                            apiResult.Data.PaymentType = masterEntity.PaymentType;
                            return apiResult;
                        }
                        else
                        {
                            masterEntity.IsCostPaid = "2";
                            apiResult.Data.IsCostPaid = masterEntity.IsCostPaid;
                            apiResult.Data.PayCostDate = masterEntity.PayCostDate;
                            apiResult.Data.PayCostTime = masterEntity.ReceiptNo;
                            apiResult.Data.BillNo = masterEntity.BillNo;
                            apiResult.Data.SumPrices = masterEntity.SumPrices.ToString();
                            apiResult.Data.PaymentType = masterEntity.PaymentType;
                            await _estateInquiryRepository.UpdateAsync(masterEntity, cancellationToken);

                            if (request.InquiryMode)
                            {
                                return apiResult;
                            }
                        }
                    }
                }
                externalServiceHelper.EstateInquiry = masterEntity;
                await externalServiceHelper.GetEstateInquiryBaseInfoData(cancellationToken);
                Vat vat = new();
                vat.CodeMelli = "0067071554";
                vat.FollowCode = "1694357944";
                CreateInvoiceServiceInput createInvoiceServiceInput = new()
                {
                    BusinessObjectId = masterEntity.Id.ToString(),
                    UnitId = "01000",
                    UserNationalNo = _userService.UserApplicationContext.User.UserName,
                    OrganizationId = "52539",
                    ProvinceId = "01000",
                    NationalNo = masterEntity.No2,
                    Title = string.Format("استعلام ملک به شماره {0}", masterEntity.No2),
                    Description = "",
                    ExtraParam = "",
                    ReturnURL = _configuration.GetValue<string>("BaseUIURL")
                };
                createInvoiceServiceInput.Vat = vat;
                var Response_To_Estate_Inquiry_Price = await _configurationParameterHelper.GetConfigurationParameter( "Response_To_Estate_Inquiry_Price",cancellationToken);
                var Electronic_Response_To_City_Estate_Inquiry_Price = await _configurationParameterHelper.GetConfigurationParameter("Electronic_Response_To_City_Estate_Inquiry_Price", cancellationToken);
                var Estate_Inquiry_HagolTahrir_Price = await _configurationParameterHelper.GetConfigurationParameter("Estate_Inquiry_HagolTahrir_Price", cancellationToken);
                var Electronic_Response_To_Village_Estate_Inquiry_Price = await _configurationParameterHelper.GetConfigurationParameter("Electronic_Response_To_Village_Estate_Inquiry_Price", cancellationToken);
                if (string.IsNullOrWhiteSpace(Response_To_Estate_Inquiry_Price) || Response_To_Estate_Inquiry_Price == "0")
                {
                    apiResult.message.Add("مبلغ هزینه پاسخ گویی به استعلامات املاک در سامانه تعریف نشده است");
                    apiResult.IsSuccess = false;
                }
                if (string.IsNullOrWhiteSpace(Estate_Inquiry_HagolTahrir_Price) || Estate_Inquiry_HagolTahrir_Price == "0")
                {
                    apiResult.message.Add("مبلغ هزینه حق التحریر استعلامات املاک در سامانه تعریف نشده است");
                    apiResult.IsSuccess = false;
                }
                if (string.IsNullOrWhiteSpace(Electronic_Response_To_City_Estate_Inquiry_Price) || Electronic_Response_To_City_Estate_Inquiry_Price == "0")
                {
                    apiResult.message.Add("مبلغ هزینه پاسخ گویی الکترونیکی به استعلامات شهری املاک در سامانه تعریف نشده است");
                    apiResult.IsSuccess = false;
                }                
                if (string.IsNullOrWhiteSpace(Electronic_Response_To_Village_Estate_Inquiry_Price) || Electronic_Response_To_Village_Estate_Inquiry_Price == "0")
                {
                    apiResult.message.Add("مبلغ هزینه پاسخ گویی الکترونیکی به استعلامات روستایی املاک در سامانه تعریف نشده است");
                    apiResult.IsSuccess = false;
                }
                if(!apiResult.IsSuccess)
                {
                    apiResult.statusCode = ApiResultStatusCode.Success;
                    return apiResult;
                }
                decimal totalPrice = 0;
                Quotation responseToEstateInquiry = new();
                responseToEstateInquiry.CostTypeID = "21";
                responseToEstateInquiry.DetailPrice = Response_To_Estate_Inquiry_Price;
                createInvoiceServiceInput.Quotation.Add(responseToEstateInquiry);
                totalPrice += Convert.ToDecimal(Response_To_Estate_Inquiry_Price);
                var estateInquiryGeolocation = externalServiceHelper.estateInquiryGeolocations.Where(x => Convert.ToInt32(x.Id) == masterEntity.GeoLocationId.Value).First();
                if (estateInquiryGeolocation.LocationType == 7)
                {
                    Quotation electronicResponseToVillageEstateInquiry = new();
                    electronicResponseToVillageEstateInquiry.CostTypeID = "23";
                    electronicResponseToVillageEstateInquiry.DetailPrice = Electronic_Response_To_Village_Estate_Inquiry_Price;
                    createInvoiceServiceInput.Quotation.Add(electronicResponseToVillageEstateInquiry);
                    totalPrice += Convert.ToDecimal(Electronic_Response_To_Village_Estate_Inquiry_Price);
                }
                else
                {
                    Quotation electronicResponseToCityEstateInquiry = new();
                    electronicResponseToCityEstateInquiry.CostTypeID = "22";
                    electronicResponseToCityEstateInquiry.DetailPrice = Electronic_Response_To_City_Estate_Inquiry_Price;
                    createInvoiceServiceInput.Quotation.Add(electronicResponseToCityEstateInquiry);
                    totalPrice += Convert.ToDecimal(Electronic_Response_To_City_Estate_Inquiry_Price);
                }

                Quotation estateInquiryHagolTahrir = new();
                estateInquiryHagolTahrir.CostTypeID = "02";
                estateInquiryHagolTahrir.DetailPrice = Convert.ToInt64(Estate_Inquiry_HagolTahrir_Price).ToString();
                createInvoiceServiceInput.Quotation.Add(estateInquiryHagolTahrir);
                totalPrice += Convert.ToDecimal(estateInquiryHagolTahrir.DetailPrice);

                Quotation maliat = new();
                maliat.CostTypeID = "09";
                var maliatValue = (long)(Convert.ToInt64(Estate_Inquiry_HagolTahrir_Price) * 0.1);
                maliat.DetailPrice = maliatValue.ToString();
                createInvoiceServiceInput.Quotation.Add(maliat);
                totalPrice += Convert.ToDecimal(maliatValue);
                
                var callResult = await _mediator.Send(createInvoiceServiceInput, cancellationToken);
                if (callResult.IsSuccess)
                {
                    masterEntity.IsCostPaid = "3";
                    masterEntity.SumPrices = (int)totalPrice;
                    masterEntity.BillNo = callResult.Data.PaymentNo;
                    await _estateInquiryRepository.UpdateAsync(masterEntity, cancellationToken);
                    apiResult.Data.PaymentLink = callResult.Data.RedirectLink;
                    apiResult.Data.IsCostPaid = "3";
                    apiResult.IsSuccess = true;
                }
                else
                {
                    apiResult.IsSuccess = false;
                    apiResult.message.Add("مشکلی در پرداخت به وجود آمده است .");
                    apiResult.statusCode = ApiResultStatusCode.ServerError;
                }
            }
            return apiResult;
        }

        protected override bool HasAccess(UpdateEstateInquiryPaymentStateCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
        private void BusinessValidation(UpdateEstateInquiryPaymentStateCommand request)
        {
            if (masterEntity != null)
            {
                if (masterEntity.WorkflowStatesId != EstateConstant.EstateInquiryStates.NotSended)
                {
                    apiResult.message.Add("استعلام در وضعیت غیر مجاز برای پرداخت می باشد");
                }
                else if (masterEntity.IsCostPaid == EstateConstant.BooleanConstant.True)
                {
                    apiResult.message.Add("هزینه استعلام قبلا پرداخت شده است");
                }
            }
            else
            {
                apiResult.message.Add("استعلام یافت نشد");
            }
            if (apiResult.message.Count > 0)
            {
                apiResult.statusCode = ApiResultStatusCode.Success;
                apiResult.IsSuccess = false;
            }
        }       
    }
}
