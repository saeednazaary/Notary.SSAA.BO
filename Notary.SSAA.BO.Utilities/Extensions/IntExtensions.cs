using System;


namespace Notary.SSAA.BO.Utilities.Extensions
{
    public static class IntExtensions
    {
        public static int IsValidId(this int value, string exMessage)
        {
            if (value > 0)
                return value;
            throw new ArgumentOutOfRangeException(exMessage);
        }

        public static int IsPercentage(this int value, string exMessage)
        {
            if (value >= 0 && value <= 100)
                return value;
            throw new ArgumentOutOfRangeException(exMessage);
        }
    }
}