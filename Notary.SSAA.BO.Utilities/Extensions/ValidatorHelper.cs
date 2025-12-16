using System.Globalization;
using System.Text.RegularExpressions;

namespace Notary.SSAA.BO.Utilities.Extensions
{
    public static class ValidatorHelper
    {
        public static bool BeValidGuid(string value)
        {

            if (value.ToGuid() == Guid.Empty)
                return false;
            return true;


        }
        public static bool BeValidPersianDate(string input)
        {
            string format = "yyyy/MM/dd";
            DateTime dateTime;
            CultureInfo persianCulture = new CultureInfo("fa-IR");

            return DateTime.TryParseExact(input, format, persianCulture, DateTimeStyles.None, out dateTime);
        }
        public static bool BeValidPersianYear(string input)
        {
            string format = "yyyy";
            DateTime dateTime;
            CultureInfo persianCulture = new CultureInfo("fa-IR");

            return DateTime.TryParseExact(input, format, persianCulture, DateTimeStyles.None, out dateTime);
        }
        public static bool ValidatePersianDateTime(string input)
        {
            string format = "yyyy/MM/dd-HH:mm";
            DateTime dateTime;
            CultureInfo persianCulture = new CultureInfo("fa-IR");

            return DateTime.TryParseExact(input, format, persianCulture, DateTimeStyles.None, out dateTime);
        }
        public static bool BeValidNumber(string value)
        {
            return decimal.TryParse(value, out _);
        }
        public static bool BeValidNumberNullable(string value)
        {
            if (string.IsNullOrEmpty(value)) return true;
            return decimal.TryParse(value, out _);
        }
        public static bool ValidateExactValue(string value, decimal exactValue)
        {
            if (!decimal.TryParse(value, out decimal number))
                return false;

            return number == exactValue;
        }
        public static bool ValidateMinValue(string value, decimal minValue)
        {
            if (!decimal.TryParse(value, out decimal number))
                return false;

            return number >= minValue;
        }
        public static bool ValidateRangeValue(string value, decimal minValue, decimal maxValue)
        {
            if (!decimal.TryParse(value, out decimal number))
                return false;

            return number >= minValue && number <= maxValue;
        }
        public static bool ValidateMaxValue(string value, decimal maxValue)
        {
            if (!decimal.TryParse(value, out decimal number))
                return false;

            return number <= maxValue;
        }
        public static bool BeValidPersianNationalNumber(string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;

            var regex = new Regex(@"^\d{10}$");
            if (!regex.IsMatch(value))
                return false;

            var check = int.Parse(value.Substring(9, 1));
            var sum = 0;
            for (int i = 0; i < 9; i++)
            {
                sum += int.Parse(value.Substring(i, 1)) * (10 - i);
            }

            var remainder = sum % 11;
            return remainder < 2 && check == remainder || remainder >= 2 && check == 11 - remainder;
        }
        public static bool BeValidMobileNumber(string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;

            var regex = new Regex(@"^09\d{9}$");
            return regex.IsMatch(value);
        }
        public static bool BeValidFaxNo(string value)
        {
            if (string.IsNullOrEmpty(value))
                return true;

            var regex = new Regex(@"^\+?[0-9]{6,}$");
            return regex.IsMatch(value);
        }
    }
}
