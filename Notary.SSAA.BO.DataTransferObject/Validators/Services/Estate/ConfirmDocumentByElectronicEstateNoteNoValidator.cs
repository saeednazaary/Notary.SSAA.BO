using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices.Estate;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Services.Estate
{
    public class ConfirmDocumentByElectronicEstateNoteNoValidator : AbstractValidator<ConfirmDocumentByElectronicEstateNoteNoQuery>
    {
        public ConfirmDocumentByElectronicEstateNoteNoValidator()
        {
            RuleFor(x => x.ElectronicEstateNoteNo).NotEmpty().WithMessage("شماره دفتر املاک الکترونیک سند اجباری می باشد");
            RuleFor(x => x.NationalityCode).NotEmpty().WithMessage("شماره/شناسه ملی مالک اجباری می باشد");
        }
    }
}
