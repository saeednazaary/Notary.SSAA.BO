using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.SignRequest;
using Notary.SSAA.BO.DataTransferObject.Queries.SsrSignEbookBaseInfo;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.SsrSignEbookBaseInfo
{
    public class GetSsrSignEbookBaseInfoByIdValidator : AbstractValidator<GetSsrSignEbookBaseInfoByIdQuery>
    {
        public GetSsrSignEbookBaseInfoByIdValidator()
        {
        }
    }
}
