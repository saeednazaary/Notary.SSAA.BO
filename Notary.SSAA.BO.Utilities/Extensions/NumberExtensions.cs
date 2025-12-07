using System.Text;

namespace Notary.SSAA.BO.Utilities.Extensions
{
    public static class NumberExtensions
    {
        public static string ToCommaString(this int value)
        {
            return value.ToString("n0");
        }
        public static string ToCommaString(this int? value)
        {
            if (value.HasValue) return value.Value.ToCommaString();
            return null;
        }
        public static string To_String(this int? value)
        {
            if (value.HasValue) return value.Value.ToString();
            return string.Empty;
        }
        public static string ToCommaString(this long value)
        {
            return value.ToString("n0");
        }
        public static string ToCommaString(this long? value)
        {
            if (value.HasValue) return value.Value.ToCommaString();
            return null;
        }
        public static string To_String(this long? value)
        {
            if (value.HasValue) return value.Value.ToString();
            return string.Empty;
        }
        public static string ToCommaString(this double value)
        {
            return value.ToString("n");
        }
        public static string ToCommaString(this double? value)
        {
            if (value.HasValue) return value.Value.ToCommaString();
            return null;
        }
        public static string ToCommaString(this double value, int decimalPlaces)
        {
            return value.ToString("n" + decimalPlaces.ToString());
        }
        public static string ToCommaString(this double? value, int decimalPlaces)
        {
            if (value.HasValue) return value.Value.ToCommaString(decimalPlaces);
            return null;
        }
        public static string To_String(this double? value)
        {
            if (value.HasValue) return value.Value.ToString();
            return string.Empty;
        }



        public static string ToCommaString(this decimal value)
        {
            return value.ToString("n");
        }
        public static string ToCommaString(this decimal? value)
        {
            if (value.HasValue) return value.Value.ToCommaString();
            return null;
        }
        public static string ToCommaString(this decimal value, int decimalPlaces)
        {
            return value.ToString("n" + decimalPlaces.ToString());
        }

        public static string ToPersianCommaString(this decimal value, int decimalPlaces)
        {
            return value.ToString("n" + decimalPlaces.ToString()).Replace('.', '/');
        }

        public static string ToCommaString(this decimal? value, int decimalPlaces)
        {
            if (value.HasValue) return value.Value.ToCommaString(decimalPlaces);
            return null;
        }

        public static string ToPersianCommaString(this decimal? value, int decimalPlaces)
        {
            if (value.HasValue) return value.Value.ToPersianCommaString(decimalPlaces);
            return null;
        }

        public static string To_String(this decimal? value)
        {
            if (value.HasValue) return value.Value.ToString();
            return string.Empty;
        }

        public static decimal GetOrDefault(this decimal? value)
        {
            return value.HasValue ? value.Value : 0;
        }

        public static T ToEnum<T>(this int value)
        {
            return (T)Enum.ToObject(typeof(T), value);
        }

        public static string ToPersianText(this int value)
        {
            return ((long)value).ToPersianText();
        }
        public static string ToPersianText(this int? value)
        {
            if (value.HasValue) return value.Value.ToPersianText();
            return null;
        }
        public static string ToPersianText(this long? value)
        {
            if (value.HasValue) return value.Value.ToPersianText();
            return null;
        }
        public static string ToPersianText(this long value)
        {
            string numText = value.ToCommaString();
            string[] numbersText = numText.Split(',');
            return GetPersianText(numbersText);

        }
        public static string ToPersianText(this double? value, bool showZeroBeforeDot = true, int decimalPlaces = 2)
        {
            if (value.HasValue) return value.Value.ToPersianText(showZeroBeforeDot, decimalPlaces);
            return null;
        }

        public static string ToPersianText(this double value, bool showZeroBeforeDot = true, int decimalPlaces = 2)
        {
            string sv = value.ToCommaString(decimalPlaces);
            int dotPos = sv.IndexOf('.');
            string v1, v2;
            if (dotPos > 0)
            {
                v1 = sv.Substring(0, dotPos);
                v2 = sv.Substring(dotPos + 1);
                while (v2.EndsWith("0")) v2 = v2.Substring(0, v2.Length - 1);
                if (string.IsNullOrWhiteSpace(v2)) v2 = null;
            }
            else
            {
                v1 = sv;
                v2 = null;
            }
            string[] numbersText = v1.Split(',');
            string pt1 = GetPersianText(numbersText);
            StringBuilder result = new StringBuilder();
            result.Append(pt1);
            if (v2 != null)
            {
                if (v2.Length > 7)
                {
                    v2 = v2.Substring(0, 7);
                }
                long lv = Convert.ToInt64(v2);
                if (lv > 0)
                {
                    string pt2 = lv.ToPersianText();
                    result.Append(" ممیز ");
                    if (value < 1 && !showZeroBeforeDot) result.Clear();
                    result.Append(pt2);
                    switch (v2.Length)
                    {
                        case 1:
                            result.Append(" دهم");
                            break;
                        case 2:
                            result.Append(" صدم");
                            break;
                        case 3:
                            result.Append(" هزارم");
                            break;
                        case 4:
                            result.Append(" ده هزارم");
                            break;
                        case 5:
                            result.Append(" صد هزارم");
                            break;
                        case 6:
                            result.Append(" یک میلیونیوم");
                            break;
                        case 7:
                            result.Append(" ده میلیونیوم");
                            break;
                    }
                }
            }



            return result.ToString();
        }

        private static string GetPersianText(string[] numbersText)
        {
            int level = numbersText.Length;
            if (level == 1)
            {
                return GetThreeDigitNumberPersianText(numbersText[0]);
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < numbersText.Length; i++)
                {
                    string txt = numbersText[i];
                    int v = Convert.ToInt32(txt);
                    if (v == 0)
                    {
                        level--;
                        continue;
                    }
                    string pt = v.ToPersianText();
                    if (!string.IsNullOrWhiteSpace(pt))
                    {

                        if (sb.Length > 0) sb.Append(" و ");
                        sb.Append(pt);
                        switch (level)
                        {
                            case 2:
                                sb.Append(' ').Append("هزار");
                                break;
                            case 3:
                                sb.Append(' ').Append("میلیون");
                                break;
                            case 4:
                                sb.Append(' ').Append("میلیارد");
                                break;
                            case 5:
                                sb.Append(' ').Append("بیلیون");
                                break;
                            case 6:
                                sb.Append(' ').Append("تریلیون");
                                break;
                            case 7:
                                sb.Append(' ').Append("کوادریلیون");
                                break;
                            case 8:
                                sb.Append(' ').Append("کوینتیلیون");
                                break;
                            case 9:
                                sb.Append(' ').Append("سیکستیلیون");
                                break;
                            case 10:
                                sb.Append(' ').Append("سپتیلیون");
                                break;
                            case 11:
                                sb.Append(' ').Append("اکتیلیون");
                                break;
                            case 12:
                                sb.Append(' ').Append("نونیلیون");
                                break;
                            case 13:
                                sb.Append(' ').Append("دسیلیون");
                                break;
                            case 14:
                                sb.Append(' ').Append("آندسیلیون");
                                break;
                            case 15:
                                sb.Append(' ').Append("دودسیلیون");
                                break;
                        }

                    }
                    level--;
                }
                return sb.ToString();
            }

        }
        private static string GetThreeDigitNumberPersianText(string value)
        {
            string txt = value.PadLeft(3, '0');
            StringBuilder sb = new StringBuilder();
            switch (txt[0])
            {
                case '1':
                    sb.Append("صد");
                    break;
                case '2':
                    sb.Append("دویست");
                    break;
                case '3':
                    sb.Append("سیصد");
                    break;
                case '4':
                    sb.Append("چهارصد");
                    break;
                case '5':
                    sb.Append("پانصد");
                    break;
                case '6':
                    sb.Append("ششصد");
                    break;
                case '7':
                    sb.Append("هفتصد");
                    break;
                case '8':
                    sb.Append("هشتصد");
                    break;
                case '9':
                    sb.Append("نهصد");
                    break;
            }

            if (txt[1] == '0' && txt[2] == '0')
            {
                if (sb.Length == 0) return "صفر";
                return sb.ToString();
            }

            if (sb.Length > 0) sb.Append(" و ");
            if (txt[1] == '1')
            {
                int v = Convert.ToInt32(txt.Substring(1));
                switch (v)
                {
                    case 10:
                        sb.Append("ده");
                        break;
                    case 11:
                        sb.Append("یازده");
                        break;
                    case 12:
                        sb.Append("دوازده");
                        break;
                    case 13:
                        sb.Append("سیزده");
                        break;
                    case 14:
                        sb.Append("چهارده");
                        break;
                    case 15:
                        sb.Append("پانزده");
                        break;
                    case 16:
                        sb.Append("شانزده");
                        break;
                    case 17:
                        sb.Append("هفده");
                        break;
                    case 18:
                        sb.Append("هجده");
                        break;
                    case 19:
                        sb.Append("نوزده");
                        break;
                }
                return sb.ToString();
            }

            switch (txt[1])
            {
                case '2':
                    sb.Append("بیست");
                    break;
                case '3':
                    sb.Append("سی");
                    break;
                case '4':
                    sb.Append("چهل");
                    break;
                case '5':
                    sb.Append("پنجاه");
                    break;
                case '6':
                    sb.Append("شصت");
                    break;
                case '7':
                    sb.Append("هفتاد");
                    break;
                case '8':
                    sb.Append("هشتاد");
                    break;
                case '9':
                    sb.Append("نود");
                    break;
            }

            if (txt[1] != '0' && txt[2] != '0') sb.Append(" و ");

            switch (txt[2])
            {
                case '1':
                    sb.Append("یک");
                    break;
                case '2':
                    sb.Append("دو");
                    break;
                case '3':
                    sb.Append("سه");
                    break;
                case '4':
                    sb.Append("چهار");
                    break;
                case '5':
                    sb.Append("پنج");
                    break;
                case '6':
                    sb.Append("شش");
                    break;
                case '7':
                    sb.Append("هفت");
                    break;
                case '8':
                    sb.Append("هشت");
                    break;
                case '9':
                    sb.Append("نه");
                    break;

            }

            return sb.ToString();

        }

        public static decimal? TrimDoubleValue ( this decimal? inputDouble )
        {
            decimal? output = null;

            if ( !inputDouble.HasValue )
                return null;

            var decimalValue = System.Convert.ToDecimal(inputDouble);

            output = System.Convert.ToDecimal ( decimalValue );

            return output;
        }
    }

}
