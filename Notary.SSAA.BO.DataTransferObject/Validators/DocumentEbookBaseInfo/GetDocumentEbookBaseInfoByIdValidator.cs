using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.SignRequest;
using Notary.SSAA.BO.DataTransferObject.Queries.DocumentEbookBaseInfo;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.DocumentEbookBaseInfo
{
    public class GetDocumentEbookBaseInfoByIdValidator : AbstractValidator<GetDocumentEbookBaseInfoByIdQuery>
    {
        public GetDocumentEbookBaseInfoByIdValidator()
        {
        }
    }
}
