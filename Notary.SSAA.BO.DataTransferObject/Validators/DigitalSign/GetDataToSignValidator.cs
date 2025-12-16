using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.DigitalSign;

namespace Notary.SSAA.BO.DataTransferObject.Validators.DigitalSign
{
    public class GetDataToSignValidator : AbstractValidator<GetDataToSignQuery>
    {
        public GetDataToSignValidator()
        {
            RuleFor(x => x.FormName).NotEmpty().WithMessage("عنوان فرم مرتبط  خالی می باشد");
            RuleFor(x => x.ConfigName).NotEmpty().WithMessage("ConfigName  خالی می باشد");
            RuleFor(x => x.EntityId).NotEmpty().WithMessage("شناسه موجودیت مرتبط  خالی می باشد");
        }
    }
    
}
