using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.Utilities.Extensions
{
    public static class BooleanExtensions
    {
        public static string ToYesNo(this bool? value)
        {
            if (value.HasValue)
            {
                if (value.Value)
                    return "1";
                else
                    return "2";
            }
            else
                return null;
        }

        public static string ToYesNo(this bool value)
        {
            if (value)
                return "1";
            else
                return "2";
        }

        public static bool ToYesNo(this string value)
        {
            if (!value.IsNullOrWhiteSpace())
            {
                if (value == "1")
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        public static bool? ToNullableYesNo ( this string value )
        {
            if ( !value.IsNullOrWhiteSpace () )
            {
                if ( value == "1" )
                    return true;
                else
                    return false;
            }
            else
                return null;
        }
        public static string ToYesNoTitle(this string value)
        {
            if (!value.IsNullOrWhiteSpace())
            {
                if (value == "1")
                    return "بله";
                else
                    return "خیر";
            }
            else
                return null;
        }
        public static string ToYesNoTitle(this bool value)
        {
            if (value)
                return "بله";
            else
                return "خیر";
        }

    }
}
