using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateTaxInquiry;



namespace Notary.SSAA.BO.DataTransferObject.Validators.Estate.EstateTaxInquiry
{
    public class GetTehranTaxBlockRadifValidator : AbstractValidator<GetTehranTaxBlockRadifQuery>
    {
        public GetTehranTaxBlockRadifValidator()
        {
            RuleFor(x => x.NosaziRadifNumber).NotEmpty().WithMessage("شماره ردیف باید مقدار داشته باشد");
            RuleFor(x => x.NosaziMelkNumber).NotEmpty().WithMessage("شماره ملک باید مقدار داشته باشد");
            RuleFor(x => x.NosaziBlockNumber).NotEmpty().WithMessage("شماره بلوک باید مقدار داشته باشد");
        }


    }
}

