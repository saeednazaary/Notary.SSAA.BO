using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Enumerations;
namespace Notary.SSAA.BO.Domain.RichDomain.Document
{
    /// <summary>
    /// Defines the <see cref="RichDocumentVehicle" />
    /// </summary>
    public static class RichDocumentVehicle
    {
        /// <summary>
        /// The RegCaseText
        /// </summary>
        /// <param name="documentVehicle">The documentVehicle<see cref="Domain.Entities.DocumentVehicle"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string RegCaseText ( this Domain.Entities.DocumentVehicle documentVehicle )
        {
            string caseText = "";
            if ( documentVehicle != null )
            {
                if ( documentVehicle.IsInTaxList == YesNo.Yes.ToString ()
                    && documentVehicle.DocumentVehicleSystem != null
                    && documentVehicle.DocumentVehicleTip != null
                    && documentVehicle.DocumentVehicleType != null )
                {
                    caseText += " دستگاه " + documentVehicle.DocumentVehicleType.Title + " - " + ( ( documentVehicle.MadeInIran == YesNo.Yes.ToString () ) ? "داخلی" : "خارجی" ) + " - " +
                                    documentVehicle.DocumentVehicleSystem.Title + " - " + documentVehicle.DocumentVehicleTip.Title + " - " + documentVehicle.Model;
                }
                else
                {
                    caseText += " دستگاه " + documentVehicle.Type;
                }

                if ( !string.IsNullOrWhiteSpace ( caseText ) )
                    caseText = "یک " + caseText;
            }

            
            return caseText;
        }




        /// <summary>
        /// The IsCalculateVehiclePrice
        /// </summary>
        /// <param name="documentVehicle">The documentVehicle<see cref="Domain.Entities.DocumentVehicle"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static bool IsCalculateVehiclePrice(this DocumentVehicle documentVehicle)
        {
            if (
            documentVehicle.DocumentVehicleTip != null &&
              documentVehicle.SellDetailQuota != null && documentVehicle.SellTotalQuota != null
            )
            {
                if (documentVehicle.SellDetailQuota != documentVehicle.SellTotalQuota &&
              documentVehicle.SellDetailQuota < documentVehicle.SellTotalQuota)
                {

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
