using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Services
{
    public class IsMatchedByILENCServiceValidator : AbstractValidator<IsMatchedByILENCServiceQuery>
    {
        public IsMatchedByILENCServiceValidator()
        {
            RuleFor(v => v.NationalNo)
            .Length(11).WithMessage("مقدار شناسه ملی مجاز نیست")
            .NotNull().WithMessage("شناسه ملی اجباری است");
        }
    }
}

