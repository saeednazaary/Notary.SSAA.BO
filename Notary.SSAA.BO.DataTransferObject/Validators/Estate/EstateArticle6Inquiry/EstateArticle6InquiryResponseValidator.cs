using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.EstateArticle6Inquiry;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Estate.EstateArticle6Inquiry
{
    public class EstateArticle6InquiryResponseValidator : AbstractValidator<EstateArticle6InquiryResponseCommand>
    {
        public EstateArticle6InquiryResponseValidator()
        {
            this.RuleFor(x => x.UserName).NotEmpty().WithMessage("1");
            this.RuleFor(x => x.Password).NotEmpty().WithMessage("1");
            this.RuleFor(x => x.RequestNo).NotEmpty().WithMessage("3");
            this.RuleFor(x => x.ResponseType).NotEmpty().WithMessage("4");
            this.RuleFor(x => x.ResponseOrganization).NotEmpty().WithMessage("5");
            this.RuleFor(x => x.OppositionReasonCode).NotEmpty().When(x => x.ResponseType == 2).WithMessage("6");
            this.RuleFor(x => x.OppositionReasonTitle).NotEmpty().When(x => x.ResponseType == 2).WithMessage("7");
        }
    }
}
