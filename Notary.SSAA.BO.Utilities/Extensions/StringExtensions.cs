using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Notary.SSAA.BO.Utilities.Extensions
{
    public static class StringExtensions
    {
        public static bool CheckFarsiAndDigits(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return true;
            }
            //  string pattern = @"([A-Za-z])";
            string pattern = @"(^[^a-zA-Z]*$)";
            Regex reg = new(pattern);
            return reg.IsMatch(text);
        }
        public static string EncodeBase64Url(this byte[] bytes)
        {
            return Convert.ToBase64String(bytes)
                          .TrimEnd('=')       // remove padding
                          .Replace('+', '-')  // URL‑safe
                          .Replace('/', '_'); // URL‑safe
        }
        public static bool CheckOnlyFarsi(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return true;
            }

            string pattern = @"(^[^a-zA-Z0-9]*$)";
            // string pattern = @"([\u0600-\u06FF])";
            Regex reg = new(pattern);

            return reg.IsMatch(text);
        }
        public static double Similarity(this string s, string t)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.IsNullOrEmpty(t) ? 0 : t.Length;
            }

            if (string.IsNullOrEmpty(t))
            {
                return s.Length;
            }

            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // initialize the top and right of the table to 0, 1, 2, ...
            for (int i = 0; i <= n; d[i, 0] = i++)
            {
                ;
            }

            for (int j = 1; j <= m; d[0, j] = j++)
            {
                ;
            }

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = t[j - 1] == s[i - 1] ? 0 : 1;
                    int min1 = d[i - 1, j] + 1;
                    int min2 = d[i, j - 1] + 1;
                    int min3 = d[i - 1, j - 1] + cost;
                    d[i, j] = Math.Min(Math.Min(min1, min2), min3);
                }
            }
            int xxx = d[n, m];
            double x1 = xxx / (double)s.Length;
            return (1 - x1) * 100;
        }
        public static bool HasValue(this string value, bool ignoreWhiteSpace = true)
        {
            return ignoreWhiteSpace ? !string.IsNullOrWhiteSpace(value) : !string.IsNullOrEmpty(value);
        }

        public static int ToInt(this string value)
        {
            return Convert.ToInt32(value);
        }
        public static short ToShort(this string value)
        {
            return Convert.ToInt16(value);
        }
        public static byte ToByte(this string value)
        {
            return Convert.ToByte(value);
        }
        public static byte? ToNullableByte(this string value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : Convert.ToByte(value);
        }
        public static int? ToNullableInt(this string value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : int.Parse(value);
        }
        public static int? ToRequiredInt(this string value)
        {
            return string.IsNullOrWhiteSpace(value) ? 0 : int.Parse(value);
        }
        public static long? ToNullableLong(this string value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : long.Parse(value);
        }
        public static decimal? ToNullableDecimal(this string value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : Convert.ToDecimal(value);
        }

        public static bool? ToNullabbleBoolean(this string value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : value == "1";
        }
        public static bool ToBoolean(this string value)
        {
            return value == "1";
        }

        public static decimal ToDecimal(this string value)
        {
            return Convert.ToDecimal(value);
        }

        public static string ToNumeric(this int value)
        {
            return value.ToString("N0"); //"123,456"
        }

        public static string ToNumeric(this decimal value)
        {
            return value.ToString("N0");
        }


        public static string ToCurrency(this decimal value)
        {
            return value.ToString("C0");
        }



        public static string NullIfEmpty(this string str)
        {
            return str?.Length == 0 ? null : str;
        }
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
        /// <summary>
        /// ساخت شرط مربوط به فیلدی که در گرید به صورت لیست می باشد
        /// </summary>
        /// <param name="filterQueryString">شرطی که برای گرید ساخته شده (میتواند خالی باشد یا نباشد)</param>
        /// <param name="globalSearch"></param>
        /// <param name="field">نام فیلدی که بر اساس آن شرط فیلتر ساخته شود</param>
        /// <param name="value">مقدار فیلتر</param>
        /// <returns></returns>
        public static string AddLambdaQueryForEntityFieldList(this string filterQueryString, string globalSearch, string field, string value = "")
        {
            if (string.IsNullOrEmpty(globalSearch))
            {

                if (string.IsNullOrEmpty(filterQueryString))
                {

                    filterQueryString += $"({field}.Any(s=>s.Contains(\"{value}\")))";
                }
                else
                {
                    filterQueryString += $" && ({field}.Any(s=>s.Contains(\"{value}\")))";

                }
            }
            else
            {
                if (string.IsNullOrEmpty(filterQueryString))
                {

                    filterQueryString += $"({field}.Any(s=>s.Contains(\"{globalSearch}\")))";
                }
                else
                {
                    int pos = filterQueryString.IndexOf("||");
                    if (pos > 0)
                    {
                        string replace = $" || ({field}.Any(s=>s.Contains(\"{globalSearch}\"))) ";
                        _ = filterQueryString[..pos];
                        _ = filterQueryString[(pos + replace.Length)..];
                        filterQueryString = string.Concat(filterQueryString.AsSpan(0, pos), replace, filterQueryString.AsSpan(pos));
                    }
                    else
                    {
                        filterQueryString += $" || ({field}.Any(s=>s.Contains(\"{globalSearch}\")))";

                    }


                }
            }
            return filterQueryString;
        }

        public static string GetPacketString(this string packet)
        {
            try
            {
                return string.IsNullOrEmpty(packet) ? "" : JObject.Parse(packet)["packet"].ToString();
            }
            catch (Exception exception)
            {
                if (exception is FormatException || exception is JsonReaderException || exception is JsonSerializationException)
                {
                    Console.WriteLine(exception.ToCompleteString());
                }
                return "";
            }
            finally
            {
                Console.WriteLine(packet);
            }
        }

        public static string PersianToArabic(this string arabicString)
        {
            if (string.IsNullOrWhiteSpace(arabicString))
            {
                return string.Empty;
            }

            string persianString = arabicString.Replace("ک", "ك").Replace("ی", "ي").Replace("ی", "ﯼ").Replace("ی", "ى");
            return persianString;
        }
        public static string ArabicTopersian(this string persianString)
        {
            if (string.IsNullOrWhiteSpace(persianString))
            {
                return string.Empty;
            }

            string arabicString = persianString.Replace("ك", "ک").Replace("ي", "ی").Replace("ﯼ", "ی").Replace("ى", "ی").Replace("ة", "ه");
            return arabicString;
        }
        public static bool IsValidNationalCode(this string nationalCode)
        {
            if (string.IsNullOrWhiteSpace(nationalCode))
            {
                return false;
            }

            if (nationalCode.Length != 10)
            {
                return false;
            }

            switch (nationalCode)
            {
                case "0000000000":
                case "1111111111":
                case "2222222222":
                case "3333333333":
                case "4444444444":
                case "5555555555":
                case "6666666666":
                case "7777777777":
                case "8888888888":
                case "9999999999":
                    return false;
                default:
                    int code = 0;
                    char ch;
                    for (int i = 0; i < 9; i++)
                    {
                        ch = nationalCode[i];
                        if (ch < '0')
                        {
                            return false;
                        }

                        if (ch > '9')
                        {
                            return false;
                        }

                        int v = ch - 48;
                        code += v * (10 - i);
                    }
                    int r = code % 11;
                    if (r > 1)
                    {
                        r = 11 - r;
                    }

                    ch = nationalCode[9];
                    if (r == ch - 48)
                    {
                        return true;
                    }

                    break;
            }

            return false;
        }
        public static string ToHash(this string value, int number = 1)
        {
            if (number < 1)
            {
                return value;
            }

            Encoding encode = Encoding.UTF8;
            byte[] byteHash = MD5.HashData(encode.GetBytes(value));
            if (number > 1)
            {
                int n = 1;
                while (n < number)
                {
                    byteHash = MD5.HashData(byteHash);
                    n++;
                }
            }
            return Convert.ToBase64String(byteHash);

        }
        private static string GetDefaultEncryptDecryptKey()
        {
            return "!@#$%^&*()|':;=+-_";
        }
        public static string Encrypt(this string value, string key = null)
        {
            try
            {
                Encoding encode = Encoding.UTF8;
                using TripleDESCryptoServiceProvider objDESCrypto = new();
                byte[] byteHash, byteBuff;
                if (string.IsNullOrEmpty(key))
                {
                    key = GetDefaultEncryptDecryptKey();
                }

                string strTempKey = key;
                byteHash = MD5.HashData(encode.GetBytes(strTempKey));
                objDESCrypto.Key = byteHash;
                objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB
                byteBuff = encode.GetBytes(value);
                return Convert.ToBase64String(objDESCrypto.CreateEncryptor()
                    .TransformFinalBlock(byteBuff, 0, byteBuff.Length));
            }
            catch (Exception ex)
            {
                return "Wrong Input. " + ex.Message;
            }
        }

        public static string Decrypt(this string value, string key = null)
        {
            try
            {
                Encoding encode = Encoding.UTF8;
                using TripleDESCryptoServiceProvider objDESCrypto = new();
                byte[] byteHash, byteBuff;
                if (string.IsNullOrEmpty(key))
                {
                    key = GetDefaultEncryptDecryptKey();
                }

                string strTempKey = key;
                byteHash = MD5.HashData(encode.GetBytes(strTempKey));
                objDESCrypto.Key = byteHash;
                objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB
                byteBuff = Convert.FromBase64String(value);
                string strDecrypted = encode.GetString(objDESCrypto.CreateDecryptor()
                    .TransformFinalBlock(byteBuff, 0, byteBuff.Length));
                return strDecrypted;
            }
            catch (Exception ex)
            {
                return "Wrong Input. " + ex.Message;
            }
        }
        public static string NormalizeTextChars(this string strText, bool persian2Arabic = true)
        {
            if (string.IsNullOrEmpty(strText))
            {
                return string.Empty;
            }

            char arabicKaf = (char)1603;
            char arabicYa = (char)1610;

            char persianKaf = (char)1705;
            char persianYa = (char)1740;

            if (persian2Arabic)
            {
                string result = strText.Replace(persianKaf, arabicKaf);
                result = result.Replace(persianYa, arabicYa);
                return result;
            }
            else
            {
                string result = strText.Replace(arabicKaf, persianKaf);
                result = result.Replace(arabicYa, persianYa);
                return result;
            }
        }

        public static string ToPersianDateString(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            string[] dateValues = value.Split('/');
            StringBuilder result = new();
            int day = Convert.ToInt32(dateValues[2]);
            switch (day)
            {
                case 3:
                    _ = result.Append("سوم");
                    break;
                case 23:
                    _ = result.Append("بیست و سوم");
                    break;
                case 30:
                    _ = result.Append("سی ام");
                    break;
                default:
                    _ = result.Append(day.ToPersianText()).Append('م');
                    break;
            }
            _ = result.Append(' ');
            int moon = Convert.ToInt32(dateValues[1]);
            switch (moon)
            {
                case 1:
                    _ = result.Append("فروردین");
                    break;
                case 2:
                    _ = result.Append("اردیبهشت");
                    break;
                case 3:
                    _ = result.Append("خرداد");
                    break;
                case 4:
                    _ = result.Append("تیر");
                    break;
                case 5:
                    _ = result.Append("مرداد");
                    break;
                case 6:
                    _ = result.Append("شهریور");
                    break;
                case 7:
                    _ = result.Append("مهر");
                    break;
                case 8:
                    _ = result.Append("آبان");
                    break;
                case 9:
                    _ = result.Append("آذر");
                    break;
                case 10:
                    _ = result.Append("دی");
                    break;
                case 11:
                    _ = result.Append("بهمن");
                    break;
                case 12:
                    _ = result.Append("اسفند");
                    break;
            }
            _ = result.Append(" ماه ");
            int year = Convert.ToInt32(dateValues[0]);
            _ = result.Append(year.ToPersianText());



            return result.ToString();
        }

        public static bool IsDigit(this string value, int length = 0)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            if (length != 0 && value.Length != length)
            {
                return false;
            }

            for (int i = 0; i < value.Length; i++)
            {
                if (!char.IsDigit(value[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsLetter(this string value, int length = 0)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            if (length != 0 && value.Length != length)
            {
                return false;
            }

            for (int i = 0; i < value.Length; i++)
            {
                if (!char.IsLetter(value[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsText(this string value, int length = 0)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            if (length != 0 && value.Length != length)
            {
                return false;
            }

            for (int i = 0; i < value.Length; i++)
            {
                if (char.IsWhiteSpace(value[i]))
                {
                    continue;
                }

                if (!char.IsLetter(value[i]))
                {
                    return false;
                }
            }
            return true;
        }


        public static bool LessThan(this string self, string value)
        {
            return (self != null || value != null) && value != null && (self == null || self.CompareTo(value) < 0);
        }

        public static bool LessThanEqual(this string self, string value)
        {
            return (self == null && value == null) || (value != null && (self == null || self.CompareTo(value) <= 0));
        }

        public static bool GreaterThan(this string self, string value)
        {
            return (self != null || value != null) && (value == null || (self != null && self.CompareTo(value) > 0));
        }

        public static bool GreaterThanEqual(this string self, string value)
        {
            return (self == null && value == null) || value == null || (self != null && self.CompareTo(value) >= 0);
        }

        public static string ToRtf(this string value)
        {
            string text = "{\\rtf1\\fbidis\\ansi\\deff0{\\fonttbl{\\f0\\fnil\\fcharset178 B Nazanin;}}\\viewkind4\\uc1\\lang1065\\f0\\fs28 " + value + "}0";
            text = text.Replace("\r\n", @"\par");
            text = text.Replace("\n", @"\par");
            StringBuilder sb = new();
            foreach (char c in text)
            {
                _ = c <= 0x7f
                    ? sb.Append(c)
                    : c <= 0xFF ? sb.Append("\\'" + Convert.ToUInt32(c).ToString("X")) : sb.Append("\\u" + Convert.ToUInt32(c) + "?");
            }
            return sb.ToString();

        }
        public static string ToUtf8(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            byte[] buffer = Encoding.UTF8.GetBytes(value);
            return Encoding.UTF8.GetString(buffer);

        }

        public static string[] ToSmsParameters(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            string sms = value.Trim(' ', '*');
            string[] result = sms.Split('*');
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = result[i].Trim();
                if (!result[i].IsDigit())
                {
                    return null;
                }
            }
            return result;
        }

        public static T As<T>(this object value)
        {
            return (T)value;
        }

        public static IDictionary<string, object> GetPropertyValues(this object value)
        {
            Dictionary<string, object> result = [];
            if (value != null)
            {
                Type type = value.GetType();
                if (!(type.IsValueType || type == typeof(string)))
                {
                    System.Reflection.PropertyInfo[] properties = type.GetProperties();
                    foreach (System.Reflection.PropertyInfo p in properties)
                    {
                        object v = p.GetValue(value, null);
                        result.Add(p.Name, v);
                    }
                }
            }

            return result;
        }


        public static string Compress(this string text)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            MemoryStream ms = new();
            using (System.IO.Compression.DeflateStream zip = new(ms, System.IO.Compression.CompressionMode.Compress, true))
            {
                zip.Write(buffer, 0, buffer.Length);
            }

            ms.Position = 0;
            _ = new MemoryStream();

            byte[] compressed = new byte[ms.Length];
            _ = ms.Read(compressed, 0, compressed.Length);

            byte[] gzBuffer = new byte[compressed.Length + 4];
            Buffer.BlockCopy(compressed, 0, gzBuffer, 4, compressed.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gzBuffer, 0, 4);
            return Convert.ToBase64String(gzBuffer);
        }

        public static string Decompress(this string compressedText)
        {
            byte[] gzBuffer = Convert.FromBase64String(compressedText);
            using MemoryStream ms = new();
            int msgLength = BitConverter.ToInt32(gzBuffer, 0);
            ms.Write(gzBuffer, 4, gzBuffer.Length - 4);

            byte[] buffer = new byte[msgLength];

            ms.Position = 0;
            using (System.IO.Compression.DeflateStream zip = new(ms, System.IO.Compression.CompressionMode.Decompress))
            {
                _ = zip.Read(buffer, 0, buffer.Length);
            }

            return Encoding.UTF8.GetString(buffer);
        }

        public static bool IsValidLegalNationalCode(this string nationalCode)
        {
            if (nationalCode.Length != 11)
            {
                return false;
            }

            int checkDigit = nationalCode[10] - 48;
            int dgt10th = nationalCode[9] - 48;

            int calc1th = (nationalCode[0] - 48 + dgt10th + 2) * 29;
            int calc2th = (nationalCode[1] - 48 + dgt10th + 2) * 27;
            int calc3th = (nationalCode[2] - 48 + dgt10th + 2) * 23;
            int calc4th = (nationalCode[3] - 48 + dgt10th + 2) * 19;
            int calc5th = (nationalCode[4] - 48 + dgt10th + 2) * 17;
            int calc6th = (nationalCode[5] - 48 + dgt10th + 2) * 29;
            int calc7th = (nationalCode[6] - 48 + dgt10th + 2) * 27;
            int calc8th = (nationalCode[7] - 48 + dgt10th + 2) * 23;
            int calc9th = (nationalCode[8] - 48 + dgt10th + 2) * 19;
            int calc10th = (nationalCode[9] - 48 + dgt10th + 2) * 17;

            int getdigit = (calc1th + calc2th + calc3th + calc4th + calc5th + calc6th + calc7th + calc8th + calc9th + calc10th) % 11;
            if (getdigit > 9)
            {
                getdigit = getdigit % 10;
            }

            return checkDigit == getdigit;
        }

        public static Guid ToGuid(this string input)
        {
            return Guid.TryParse(input, out Guid result) ? result : Guid.Empty;
        }
        public static Guid? ToNullableGuid(this string input)
        {
            return !string.IsNullOrEmpty(input) ? Guid.TryParse(input, out Guid result) ? result : Guid.Empty : null;
        }
        public static List<Guid> ToListGuid(this IList<string> input)
        {
            List<Guid> guids = [];
            foreach (string item in input)
            {
                if (Guid.TryParse(item, out Guid result))
                {
                    guids.Add(result);
                }
            }
            return guids;
        }
        public static string ToNullableString(this string value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : value;
        }

        #region Encode & Decode To Hex
        public static string EncodeToHex(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            byte[] buffer = Encoding.UTF8.GetBytes(value);
            return EncodeToHexInternal(buffer);
        }
        private static string EncodeToHexInternal(byte[] buffer)
        {
            StringBuilder hexValue = new();
            for (int index = 0; index < buffer.Length; index++)
            {
                byte b1 = buffer[index];
                byte b2 = (byte)(b1 >> 4);
                _ = hexValue.Append(EncodeToHexInternal(b2));
                b2 = (byte)(b1 << 4);
                b2 = (byte)(b2 >> 4);
                _ = hexValue.Append(EncodeToHexInternal(b2));
            }
            return hexValue.ToString();
        }

        private static char EncodeToHexInternal(byte value)
        {
            switch (value)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                    return (char)(value + 48);
                case 10:
                    return 'A';
                case 11:
                    return 'B';
                case 12:
                    return 'C';
                case 13:
                    return 'D';
                case 14:
                    return 'E';
                case 15:
                    return 'F';
            }
            return 'Z';
        }

        public static string DecodeFromHex(this string encodedValue)
        {
            if (string.IsNullOrEmpty(encodedValue))
            {
                return encodedValue;
            }
            byte[] buffer = DecodeFromHexInternal(encodedValue);
            return Encoding.UTF8.GetString(buffer);
        }

        private static byte[] DecodeFromHexInternal(string encodedValue)
        {
            if (encodedValue.Length % 2 != 0)
            {
                throw new ArgumentException("Hex string must have an even length", nameof(encodedValue));
            }

            byte[] buffer = new byte[encodedValue.Length >> 1];
            for (int i = 0, j = 0; i < encodedValue.Length; i += 2, j++)
            {
                char c1 = encodedValue[i];
                char c2 = encodedValue[i + 1];
                byte b1 = DecodeFromHexInternal(c1);
                byte b2 = DecodeFromHexInternal(c2);
                byte b = (byte)((b1 << 4) | b2);
                buffer[j] = b;
            }
            return buffer;
        }

        private static byte DecodeFromHexInternal(char hexChar)
        {
            switch (hexChar)
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    return (byte)(hexChar - 48);
                case 'A':
                    return 10;
                case 'B':
                    return 11;
                case 'C':
                    return 12;
                case 'D':
                    return 13;
                case 'E':
                    return 14;
                case 'F':
                    return 15;
            }
            return (byte)'Z';
        }

        #endregion Encode & Decode To Hex

        public static byte[] toByteArray(this string value)
        {
            if (value.IsNullOrEmpty())
            {
                return null;
            }

            byte[] byteArray = Encoding.UTF8.GetBytes(value);
            return byteArray;


        }
        public static string GetStandardFarsiString(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return input;
            }

            input = input.Replace("الله", "اله");
            input = input.Replace("آ", "ا");
            input = input.Replace("أ", "ا");
            input = input.Replace("إ", "ا");
            input = input.Replace("ك", "ک");
            input = input.Replace("ي", "ی");
            input = input.Replace("ئ", "ی");
            input = input.Replace("ؤ", "و");
            input = input.Replace("ة", "ه");
            input = input.Replace("ۀ", "ه");
            input = input.Replace("ء", "ی");
            input = input.Replace("ئي", "يي");
            input = input.Replace("وو", "و");
            input = input.Replace("ئو", "و");

            input = input.Replace("\u064B", string.Empty); //tanvin-ann
            input = input.Replace("\u064C", string.Empty); //tanvin-onn
            input = input.Replace("\u064D", string.Empty); //tanvin-enn
            input = input.Replace("\u064E", string.Empty); //fathe
            input = input.Replace("\u064F", string.Empty); //zamme
            input = input.Replace("\u0650", string.Empty); //kasre            
            input = input.Replace("\u0651", string.Empty); //tashdid
            input = input.Replace("\u0654", string.Empty); //hamza-high
            input = input.Replace("\u0655", string.Empty); //hamza-low
            input = input.Replace("\u0674", string.Empty); //hamza
            input = input.Replace("\u0640", string.Empty); //Ctrl+J
            input = input.Replace(" ", string.Empty);
            input = input.Replace("\t", string.Empty);

            return input;
        }

        public static string GetStandardFarsiString(this string input, bool correctAllConflicts = true)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return input;
            }

            input = input.Replace("ك", "ک");
            input = input.Replace("ي", "ی");
            input = input.Replace("\u0649", "ی");

            if (correctAllConflicts)
            {
                input = input.Replace(" ", string.Empty);
                input = input.Replace("\t", string.Empty);
                input = input.Replace("الله", "اله");
                input = input.Replace("ئ", "ی");
                input = input.Replace("ؤ", "و");
                input = input.Replace("ة", "ه");
                input = input.Replace("ۀ", "ه");
                input = input.Replace("ء", "ی");
                input = input.Replace("ئي", "يي");
                input = input.Replace("وو", "و");
                input = input.Replace("ئو", "و");

                input = input.Replace("\u064B", string.Empty); //tanvin-ann
                input = input.Replace("\u064C", string.Empty); //tanvin-onn
                input = input.Replace("\u064D", string.Empty); //tanvin-enn
                input = input.Replace("\u064E", string.Empty); //fathe
                input = input.Replace("\u064F", string.Empty); //zamme
                input = input.Replace("\u0650", string.Empty); //kasre            
                input = input.Replace("\u0651", string.Empty); //tashdid
                input = input.Replace("\u0654", string.Empty); //hamza-high
                input = input.Replace("\u0655", string.Empty); //hamza-low
                input = input.Replace("\u0674", string.Empty); //hamza
                input = input.Replace("\u200A", string.Empty); //Special Space
                input = input.Replace("\u200B", string.Empty); //Special Space
                input = input.Replace("\u200C", string.Empty); //Special Space
                input = input.Replace("\u200D", string.Empty); //Special Space
                input = input.Replace("\u200E", string.Empty); //Special Space
                input = input.Replace("\u200F", string.Empty); //Special Space
            }


            return input;
        }

    }
}
