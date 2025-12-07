using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Enumerations;
namespace Notary.SSAA.BO.Domain.RichDomain.Document
{
    /// <summary>
    /// Defines the <see cref="RichDocumentVehicleTip" />
    /// </summary>
    public static class RichDocumentVehicleTip
    {

        public static bool IsNotMadeInIran(this DocumentVehicleTip vehicleTip)
        {

            if (vehicleTip.Code.Substring(0, 3) == "508" ||   // ماشین آلات خارجی
          vehicleTip.Code.Substring(0, 3) == "506")
            // موتورسيكلت خارجي
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsMadeInIran(this DocumentVehicleTip vehicleTip)
        {

            if (vehicleTip.Code.Substring(0, 1) == "6")   // وسایل نقلیه داخلی
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
