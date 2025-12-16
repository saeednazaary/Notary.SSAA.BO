using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.DocumentModify;

namespace Notary.SSAA.BO.DataTransferObject.Validators.DocumentModify
{
    public class GetDocumentByNationalNoValidator : AbstractValidator<GetDocumentByNationalNoQuery>
    {
        public GetDocumentByNationalNoValidator()
        {
            RuleFor(x => x.NationalNo).MaximumLength(18).WithMessage("مقدار شناسه یکتا غیرمجاز میباشد")
                        .NotEmpty().WithMessage("مقدار شناسه یکتا اجباری میباشد");

        }
    }
}
