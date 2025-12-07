using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;


namespace Notary.SSAA.BO.Utilities.Extensions
{
    public static class DecimalExtensions
    {
        public static decimal RoundToMoney(this decimal value, int @decimal = 2)
        {
            return Math.Round(value, @decimal, MidpointRounding.ToEven);
        }

        public static decimal CheckMoney(this decimal value, string exMessage)
        {
            if (value > 0)
                return value;
            throw new ValidationException(exMessage);

        }

        public static string ToCommaSeparated(this decimal value, string format = "N")
            => value.ToString("N", CultureInfo.InvariantCulture);

        public static decimal ToDecimalInvariantCulture(this string value)
        {
            if (value == "?")
                return 0;

            var matchString = Regex.Match(value, "[\\d.]+");
            return Convert.ToDecimal(matchString.Value, CultureInfo.InvariantCulture);
        }
        public static decimal ChangeDecimal(this decimal digit, int decimalno)
        {
            string s = digit.ToString();
            var res = s.Split('.');
            var section1 = res[0];
            if (res.Length > 1)
            {

                if (decimalno > res[1].Length)
                {
                    decimalno = res[1].Length;
                }
                var t = res[1].Length - decimalno;
                var section2 = res[1].Remove(decimalno, t);
                return Convert.ToDecimal(section1 + "." + section2);
            }
            else
            {
                return Convert.ToDecimal(res[0]);
            }
        }
        public static decimal DecimalToUp(this decimal digit, int decimalno)
        {

            decimal multiplier = (decimal)Math.Pow(10, Convert.ToDouble(decimalno));
            return Math.Ceiling(digit * multiplier) / multiplier;
        }

        public static decimal DecimalToDown(this decimal digit, int decimalno)
        {

            var power = Convert.ToDecimal(Math.Pow(10, decimalno));
            return Math.Floor(digit * power) / power;

        }
        public static decimal Round(this decimal value, int fractional = 6)
            => Math.Round(value, fractional, MidpointRounding.AwayFromZero);
    }
}