using Mapster;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.EPayment;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.EPayment;


namespace Notary.SSAA.BO.DataTransferObject.Mappers.EPayment
{
    public class EPaymentMapper
    {
        public static CreateInvoiceEndpointInput ToInput(CreateInvoiceServiceInput createInvoiceServiceInput)
        {
            var config = new TypeAdapterConfig();
            var createInvoiceEndpointInput = createInvoiceServiceInput.Adapt<CreateInvoiceEndpointInput>(config);
            return createInvoiceEndpointInput;
        }
        public static InquiryPaymentEndpointInput ToInput(InquiryPaymentServiceInput inquiryPaymentServiceInput)
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<InquiryPaymentServiceInput, InquiryPaymentEndpointInput>()
                .Map(dest => dest.NationalNo, src => src.NationalNo)
                .IgnoreNullValues(true);

            var inquiryPaymentEndpointInput = inquiryPaymentServiceInput.Adapt<InquiryPaymentEndpointInput>(config);
            return inquiryPaymentEndpointInput;
        }
        public static EpaymentCostCalculatorServiceInput ToCalculatorServiceInput(KatebEpaymentCostCalculatorQuery katebCalculatorViewModel)
        {
            var config = new TypeAdapterConfig();
            var KatebViewModel = katebCalculatorViewModel.Adapt<EpaymentCostCalculatorServiceInput>(config);
            return KatebViewModel;
        }
        public static KatebEpaymentCostCalculatorViewModel ToCalculatorKatebViewModel(EpaymentCostCalculatorViewModel calculatorViewModel)
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<EpaymentCostCalculatorViewModel, KatebEpaymentCostCalculatorViewModel>()
                .Map(dest => dest.KatebEpaymentCostCalculatorDetailList, src => src.EpaymentCostCalculatorDetailList.Adapt<List<KatebEpaymentCostCalculatorDetail>>(config))
                .Map(dest => dest.TotalPrice, src => src.TotalPrice);
            return calculatorViewModel.Adapt<KatebEpaymentCostCalculatorViewModel>(config);
        }
        public static EpaymentCostCalculatorServiceInput ToCalculatorServiceInput(EpaymentCostCalculatorExternalQuery katebCalculatorViewModel)
        {
            var config = new TypeAdapterConfig();
            var KatebViewModel = katebCalculatorViewModel.Adapt<EpaymentCostCalculatorServiceInput>(config);
            return KatebViewModel;
        }
    }
}
