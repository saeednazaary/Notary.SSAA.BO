using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.DigitalSign;

namespace Notary.SSAA.BO.DataTransferObject.Validators.DigitalSign
{
    public class VerifySignValidator : AbstractValidator<VerifySignQuery>
    {
        public VerifySignValidator()
        {
            RuleFor(x => x.FormName).NotEmpty().WithMessage("عنوان فرم مرتبط  خالی می باشد");
            RuleFor(x => x.ConfigName).NotEmpty().WithMessage("ConfigName  خالی می باشد");
            RuleFor(x => x.EntityId).NotEmpty().When(x=>string.IsNullOrWhiteSpace(x.JsonData)).WithMessage("هم داده امضا شونده و هم شناسه موجودیت مرتبط خالی می باشد");
            RuleFor(x => x.JsonData).NotEmpty().When(x => string.IsNullOrWhiteSpace(x.EntityId)).WithMessage("هم داده امضا شونده و هم شناسه موجودیت مرتبط خالی می باشد");

        }
    }
    
}
