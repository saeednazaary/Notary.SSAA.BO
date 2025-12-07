using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.EstateArticle6Inquiry;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Estate.EstateArticle6Inquiry
{
    public class EstateArticle6InquiryRelatedOrgansValidator : AbstractValidator<EstateArticle6InquiryRelatedOrgansCommand>
    {
        public EstateArticle6InquiryRelatedOrgansValidator()
        {
            this.RuleFor(x => x.UserName).NotEmpty().WithMessage("1");
            this.RuleFor(x => x.Password).NotEmpty().WithMessage("1");
            this.RuleFor(x => x.RequestNo).NotEmpty().WithMessage("2");
            this.RuleFor(x => x.RelatedOrganList).NotEmpty().WithMessage("3");           
        }
    }
}
