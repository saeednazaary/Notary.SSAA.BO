

using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Queries.EDM;
using Notary.SSAA.BO.Utilities.Extensions;


namespace SSAA.Notary.DataTransferObject.Validators.EDM
{
    public class SignRequestReportEdmValidator : AbstractValidator<SignRequestReportEdmQuery>
    {
        public SignRequestReportEdmValidator() 
        {
           
        }
    }
}
