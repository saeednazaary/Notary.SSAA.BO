using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.EPayment;


namespace Notary.SSAA.BO.DataTransferObject.Validators.EPayment
{
    public class CreateInvoiceServiceValidator : AbstractValidator<CreateInvoiceServiceInput>
    {
        public CreateInvoiceServiceValidator()
        {
            RuleFor(x => x.OrganizationId).NotEmpty().WithMessage("شناسه ارگان مربوطه اجباری است .");
            //RuleFor(x => x.ClientDeviceType).NotEmpty().WithMessage("نوع دستگاهی که از آن پرداخت انجام میشود ، اجباری است .");

        }
    }
}
