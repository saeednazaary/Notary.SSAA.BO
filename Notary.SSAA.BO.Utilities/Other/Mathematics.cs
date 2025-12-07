namespace Notary.SSAA.BO.Utilities.Other
{
    public static class Mathematics
    {
        public static bool IsDevidableByCurrentDivisor(decimal num1, decimal num2, decimal targetDenumerator, ref decimal? divisor)
        {
            if (num2 % targetDenumerator != 0)
            {
                return false;
            }

            divisor = num2 / targetDenumerator;

            if (num1 % divisor != 0)
            {
                decimal simplifiedEnumerator = num1 / (decimal)divisor;
                string[] simplifiedEnumeratorSeparatedParts = simplifiedEnumerator.ToString().Split('.');
                if (simplifiedEnumeratorSeparatedParts.Length > 1) //اگر اعشاری نیست پس بخشپذیر می باشد.
                {
                    if (simplifiedEnumeratorSeparatedParts[1].Length > 3)
                    {
                        divisor = null;
                        return false;
                    }
                }
            }

            return true;
        }


        public static decimal calcGCD(decimal num1, decimal num2, decimal? desiringDenumerator = null)
        {
            decimal? divisor = null;
            bool isDevidableByDesiringDenumerator;
            if (desiringDenumerator != null && desiringDenumerator > 0)
            {
                isDevidableByDesiringDenumerator = IsDevidableByCurrentDivisor(num1, num2, (decimal)desiringDenumerator, ref divisor);
                if (isDevidableByDesiringDenumerator && divisor != null)
                {
                    return (decimal)divisor;
                }
            }

            isDevidableByDesiringDenumerator = IsDevidableByCurrentDivisor(num1, num2, 6, ref divisor);
            if (isDevidableByDesiringDenumerator && divisor != null)
            {
                return (decimal)divisor;
            }

            while (num2 != 0)
            {
                decimal reminder = num1 % num2;
                num1 = num2;
                num2 = reminder;
            }

            return num1;
        }

        /// <summary>
        /// Calculates Summary Of Two Fractions
        /// By Mr. Khateri
        /// </summary>
        /// <param name="s1">Soorat Kasr Avval</param>
        /// <param name="s2">Soorat Kasr Dovvom</param>
        /// <param name="m1">Makhraj Kasr Avval</param>
        /// <param name="m2">Makhraj Kasr Dovvom</param>
        /// <returns>Majmoo' Ba Makhraje Moshtarak</returns>
        public static decimal[] MakhrajMoshtarak(decimal s1, decimal s2, decimal m1, decimal m2)
        {
            decimal[] arr = new decimal[2];

            if (m1 == 0 && s1 == 0)
            {
                arr[0] = s2;
                arr[1] = m2;
                return arr;
            }

            if (m2 == 0 && s2 == 0)
            {
                arr[0] = s1;
                arr[1] = m1;
                return arr;
            }

            decimal mm = 0,
                   leastM, greatestM, i;

            greatestM = (m1 > m2) ? m1 : m2;
            leastM = (m1 > m2) ? m2 : m1;

            if (greatestM % leastM == 0)
            {
                mm = greatestM;
                s1 *= mm / m1;
                s2 *= mm / m2;
            }
            else
            {
                for (i = leastM / 2; i > 0; i--)
                {
                    if ((greatestM % i == 0) && (leastM % i == 0))
                    {
                        decimal bmm = i;
                        mm = m1 * m2 / bmm;
                        s1 *= mm / m1;
                        s2 *= mm / m2;
                        break;
                    }
                }
            }


            arr[0] = s1 + s2;
            arr[1] = mm;
            return arr;

        }

        public static decimal[] InverseFraction(decimal[] mainFraction)
        {
            decimal[] inversedFraction = new decimal[2];


            inversedFraction[0] = mainFraction[1];
            inversedFraction[1] = mainFraction[0];

            return inversedFraction;
        }

        public static decimal[] RemoveDecimalType(decimal[] theInputFraction)
        {
            if (theInputFraction == null || theInputFraction.Length < 2)
                throw new ArgumentException("Input array must contain at least 2 elements", nameof(theInputFraction));

            int decimalPlaces = BitConverter.GetBytes(decimal.GetBits(theInputFraction[1])[3])[2];

            if (decimalPlaces > 0)
            {
                decimal multiplier = (decimal)Math.Pow(10, decimalPlaces);
                theInputFraction[0] *= multiplier;
                theInputFraction[1] *= multiplier;
            }

            return theInputFraction;
        }

        public static decimal[] SimplifyFraction(decimal[] theInputFraction, decimal[] targetBaseFraction = null)
        {
            decimal gcd = targetBaseFraction != null
                ? calcGCD(theInputFraction[0], theInputFraction[1], targetBaseFraction[1])
                : calcGCD(theInputFraction[0], theInputFraction[1]);
            decimal tempSellDetailQuota = theInputFraction[0] / gcd;
            decimal tempSellTotalQuota = theInputFraction[1] / gcd;

            if (tempSellDetailQuota < theInputFraction[0] && tempSellTotalQuota < theInputFraction[1])
            {
                theInputFraction[0] = tempSellDetailQuota;
                theInputFraction[1] = tempSellTotalQuota;
            }

            return theInputFraction;
        }

        public static decimal FormatDoubleDecimalPlaces(decimal inputDouble, int decimalPlacesCount = 3)
        {
            decimal formattedDouble = inputDouble;

            string temp = inputDouble.ToString();
            if (temp.Contains('.'))
            {
                string[] tempArray = temp.Split('.');
                if (tempArray.Length > 1)
                {
                    string intSection = tempArray[0];
                    string decimalSection = tempArray[1];

                    if (decimalSection.Length > decimalPlacesCount)
                    {
                        decimalSection = decimalSection[..decimalPlacesCount];
                    }

                    temp = intSection + "." + decimalSection;
                    _ = decimal.TryParse(temp, out formattedDouble);

                }
            }

            return formattedDouble;
        }

        public static int GetDecimalPlacesCount(this decimal value)
        {
            string sValue = value.ToString();

            string[] tempArray = sValue.Contains('.') ? sValue.Split('.') : (new string[] { sValue });

            int decimalPlacesCount = tempArray.Length > 1 ? tempArray[1].Length : 0;
            return decimalPlacesCount;
        }

   

        public static bool IsQuotaDetailValueAllowed(this decimal? value)
        {
            return value.HasValue && value.Value.GetDecimalPlacesCount() <= 4 && !(value.Value > (decimal?)999999999999.9999);
        }

        public static bool IsQuotaTotalValueAllowed(this decimal? value)
        {
            return value.HasValue && value.Value.GetDecimalPlacesCount() <= 0 && value.Value <= 999999999999;
        }



    }

}
