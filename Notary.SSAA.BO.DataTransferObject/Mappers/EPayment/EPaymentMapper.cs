using Mapster;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.EPayment;
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
    }
}
