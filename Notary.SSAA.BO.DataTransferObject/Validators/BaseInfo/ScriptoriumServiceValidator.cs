using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.BaseInfo;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.EPayment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.DataTransferObject.Validators.BaseInfo
{
    public class ScriptoriumServiceValidator : AbstractValidator<ScriptoriumInput>
    {
        public ScriptoriumServiceValidator()
        {
            RuleFor(x => x.IdList).NotEmpty().WithMessage("لیست شناسه های دفترخانه اجباری می باشد");
        }
    }
}
