using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.Domain.BusinessRules.GeneralRules
{
    public static class GeneralBusinessRule
    {
        public static bool IsValidMobileNo(string input)
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(input)) {  isValid = false; }

            /*check Regex in future */

            return isValid;
        }

        public static bool IsValidPostalCode (string input)
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(input)) { isValid = false; }
            if (input.IsDigit(10)) { isValid = false; }
            return isValid;
        }


    }
}
