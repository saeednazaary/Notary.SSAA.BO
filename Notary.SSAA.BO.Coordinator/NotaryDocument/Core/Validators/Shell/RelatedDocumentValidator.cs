namespace Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Validators.Shell
{
    using Notary.SSAA.BO.Configuration;
    using Notary.SSAA.BO.DataTransferObject.Coordinators.NotaryDocument;
    using Notary.SSAA.BO.Domain.RichDomain.Document;
    using Notary.SSAA.BO.SharedKernel.Enumerations;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="RelatedDocumentValidator" />
    /// </summary>
    public class RelatedDocumentValidator
    {
        /// <summary>
        /// Defines the _relatedDocValidatorConfiguration
        /// </summary>
        internal RelatedDocValidatorConfiguration? _relatedDocValidatorConfiguration = null;

        /// <summary>
        /// Defines the validatorCore
        /// </summary>
        internal Core.RelatedDocumentValidatorCore validatorCore;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelatedDocumentValidator"/> class.
        /// </summary>
        /// <param name="_validatorCore">The _validatorCore<see cref="Core.RelatedDocumentValidatorCore"/></param>
        /// <param name="relatedDocValidatorConfiguration">The relatedDocValidatorConfiguration<see cref="RelatedDocValidatorConfiguration"/></param>
        public RelatedDocumentValidator ( Core.RelatedDocumentValidatorCore _validatorCore, RelatedDocValidatorConfiguration relatedDocValidatorConfiguration )
        {
            _relatedDocValidatorConfiguration = relatedDocValidatorConfiguration;
            validatorCore = _validatorCore;
        }

        /// <summary>
        /// The ValidateRelatedDocument
        /// </summary>
        /// <param name="theRequest">The theRequest<see cref="Notary.SSAA.BO.SharedKernel.Contracts.Coordinator.Document.RelatedDocValidationRequest"/></param>
        /// <returns>The <see cref="Task{RelatedDocValidationResult}"/></returns>
        public async Task<RelatedDocValidationResult> ValidateRelatedDocument ( Notary.SSAA.BO.SharedKernel.Contracts.Coordinator.Document.RelatedDocValidationRequest theRequest )
        {
            RelatedDocValidationResult relatedDocValidationResult = new RelatedDocValidationResult();
            string pMessageText = string.Empty;

            try
            {
                if ( _relatedDocValidatorConfiguration?.ValidatorEnabled == RestrictionLevel.Disabled )
                {
                    relatedDocValidationResult.IsValid = true;
                    return relatedDocValidationResult;
                }
                //System.Environment.NewLine + "هشدارهای مربوط به اسناد وابسته ذکر شده در بخش سایر اطلاعات: " + System.Environment.NewLine;

                if ( !string.IsNullOrWhiteSpace ( theRequest.RelatedDocumentNationalNo ) )
                {
                    if ( theRequest.RelatedDocumentNationalNo.Length < 18 )
                    {
                        pMessageText += "\t - شناسه وارد شده برای سند وابسته، صحیح نمی باشد.";
                        relatedDocValidationResult.IsValid = false;
                        relatedDocValidationResult.ValidationMessage = pMessageText;
                        return relatedDocValidationResult;
                    }
                }

                string scriptoriumCode = (!string.IsNullOrWhiteSpace(theRequest.RelatedDocumentNationalNo)) ? theRequest.RelatedDocumentNationalNo.Substring(7, 5) : string.Empty;

                relatedDocValidationResult = await validatorCore.ValidateRelatedDocument ( theRequest );

                return relatedDocValidationResult;

            }
            catch ( Exception ex )
            {

                pMessageText += "خطای غیرمنتظره:\n" + ex.ToString ();

                if ( _relatedDocValidatorConfiguration?.V7 == RestrictionLevel.Avoidance )
                {
                    relatedDocValidationResult.IsValid = false;
                    relatedDocValidationResult.ValidationMessage = pMessageText;

                    return relatedDocValidationResult;
                }
                else
                {
                    relatedDocValidationResult.IsValid = true;
                    relatedDocValidationResult.ValidationMessage = pMessageText;

                    return relatedDocValidationResult;
                }
            }
        }
    }

}
