using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.Grids;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Grids
{
    public sealed class DocumentSelselehAyadiGridValidator : AbstractValidator<DocumentSelselehAyadiGridQuery>
    {
        public DocumentSelselehAyadiGridValidator()
        {
            RuleFor(x => x.BasicPlaque)
        .NotEmpty().WithMessage("پلاک اصلی نباید خالی باشد.");

            RuleFor(x => x.SecondaryPlaque)
                .NotEmpty().WithMessage("پلاک فرعی نباید خالی باشد.");

            RuleFor(x => x.DocumentUnitId)
                .NotEmpty().WithMessage("شناسه واحد ثبتی الزامی است.");

            RuleFor(x => x.DocumentEstateSectionId)
                .NotEmpty().WithMessage("شناسه بخش ثبتی الزامی است.");

            RuleFor(x => x.DocumentEstateSubSectionId)
          .NotEmpty().WithMessage("شناسه ناحیه ثبتی  الزامی است.");
        }
    }
}
