using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Services
{
    public class RealForeignerServiceValidator : AbstractValidator<RealForeignerServiceQuery>
    {
        public RealForeignerServiceValidator()
        {
            RuleFor(v => v.ForeignerCode)
            .NotEmpty().WithMessage("کد اتباع خارجی اجباری است");
        }
    }
}
