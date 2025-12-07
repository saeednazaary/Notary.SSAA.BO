using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.Document;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Document
{
    public class SaveFromKatebDocumentValidator : AbstractValidator<SaveDocumentCommand>
    {
        public SaveFromKatebDocumentValidator()
        {
        }
    }
}

