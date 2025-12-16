using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices.Circular;


namespace Notary.SSAA.BO.DataTransferObject.Validators.Services.Circular
{
    public class CircularValidator :  AbstractValidator<CircularQuery>
    {
        public CircularValidator() 
        {
            RuleFor(v => v.CircularInfoId)
      .     NotNull().WithMessage("شناسه اجباری است");

            RuleFor(v => v.CircularItemId)
             .NotNull().WithMessage("شناسه اجباری است");
        }
    }
}
