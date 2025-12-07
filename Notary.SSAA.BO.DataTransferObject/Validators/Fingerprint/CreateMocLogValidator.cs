using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.Fingerprint;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Fingerprint
{
    public class CreateMocLogValidator : AbstractValidator<CreateMocLogCommand>
    {
        public CreateMocLogValidator()
        {
          
        }
    }
   
}
