using   Notary.SSAA.BO.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.Utilities
{
    public class ValidatorsUtility
    {
        #region CheckOnlyFarsi
        public static bool CheckOnlyFarsi ( string text )
        {
            if ( string.IsNullOrEmpty ( text ) )
                return true;

            //string pattern = @"(^[\u0600-\u06FF]+$)";
            string pattern = @"([\u0600-\u06FF])";
            Regex reg = new Regex(pattern);

            return reg.IsMatch ( text );
        }
        #endregion

        #region CheckFarsiAndDigits
        public static bool CheckFarsiAndDigits ( string text )
        {
            if ( string.IsNullOrEmpty ( text ) )
                return true;

            string pattern = @"([A-Za-z])";
            Regex reg = new Regex(pattern);

            return !reg.IsMatch ( text );
        }
        #endregion;

        #region CheckDigits
        public static bool CheckDigits ( string text )
        {
            if ( string.IsNullOrEmpty ( text ) )
                return true;
            //string pattern = @"([0-9])";
            string pattern = @"^(([0-9]*))$";
            Regex reg = new Regex(pattern);
            bool result = reg.IsMatch(text);

            return result;
        }
        #endregion

        #region CheckDigitsAndSigns
        public static bool CheckDigitsAndSigns ( string text )
        {
            if ( string.IsNullOrEmpty ( text ) )
                return true;

            string pattern = @"([A-Za-z])";
            Regex reg = new Regex(pattern);

            if ( reg.IsMatch ( text ) )
                return false;

            pattern = @"([\u0600-\u06FF])";
            reg = new Regex ( pattern );

            if ( reg.IsMatch ( text ) )
                return false;

            return true;
        }
        #endregion

        #region CheckDateNotInFuture
        public static bool CheckDateNotInFuture ( string selectedDate, string CurrentDate )
        {
            if ( selectedDate != null && string.Compare ( selectedDate, CurrentDate ) > 0 )
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region checkNaturalNationalCode
        public static bool checkNaturalNationalCode ( string nationalCode )
        {
            if ( !nationalCode.IsDigit ( 10 ) )
                return false;

            if ( nationalCode.Length == 10 )
            {
                if ( nationalCode == "0000000000" ||
                    nationalCode == "1111111111" ||
                    nationalCode == "2222222222" ||
                    nationalCode == "3333333333" ||
                    nationalCode == "4444444444" ||
                    nationalCode == "5555555555" ||
                    nationalCode == "6666666666" ||
                    nationalCode == "7777777777" ||
                    nationalCode == "8888888888" ||
                    nationalCode == "9999999999" )
                    return false;

                int c = Int32.Parse(nationalCode.Substring(9, 1));
                decimal n =
                    Int32.Parse(nationalCode.Substring(0, 1)) * 10 +
                    Int32.Parse(nationalCode.Substring(1, 1)) * 9 +
                    Int32.Parse(nationalCode.Substring(2, 1)) * 8 +
                    Int32.Parse(nationalCode.Substring(3, 1)) * 7 +
                    Int32.Parse(nationalCode.Substring(4, 1)) * 6 +
                    Int32.Parse(nationalCode.Substring(5, 1)) * 5 +
                    Int32.Parse(nationalCode.Substring(6, 1)) * 4 +
                    Int32.Parse(nationalCode.Substring(7, 1)) * 3 +
                    Int32.Parse(nationalCode.Substring(8, 1)) * 2;

                decimal r = n - System.Math.Floor(n / 11) * 11;

                if ( ( r == 0 && r == c ) ||
                    ( r == 1 && c == 1 ) ||
                    ( r > 1 && c == 11 - r ) )
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        #endregion

        #region checkPostalCode
        public static bool checkPostalCode ( string postalCode )
        {
            //postalCode.IndexOf('0') >= 0 || postalCode.IndexOf('2') >= 0 ||
            if ( postalCode.Length != 10 || !CheckDigits ( postalCode ) )
                return false;
            else
                return true;
        }
        #endregion

        #region CheckEmailFormat
        public static bool CheckEmailFormat ( string emailAddress )
        {
            if ( string.IsNullOrEmpty ( emailAddress ) )
                return true;

            string pattern =
                @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@" +
                @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]? [0-9]{1,2}|25[0-5]|2[0-4][0-9])\." +
                @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]? [0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|" +
                @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

            Regex reg = new Regex(pattern);

            return reg.IsMatch ( emailAddress );
        }
        #endregion

        #region CheckCellPhoneFormat
        public static bool CheckCellPhoneFormat ( string cellPhoneNumber )
        {
            if ( string.IsNullOrEmpty ( cellPhoneNumber ) )
                return true;

            string pattern = @"^09[0-9][0-9]{8}$";
            Regex reg = new Regex(pattern);

            return reg.IsMatch ( cellPhoneNumber );
        }
        #endregion

       

        #region ValidatePaparNumber
        public static bool ValidatePaparNumber ( string paperNumber, out string validationMessage )
        {
            if ( CheckDigits ( paperNumber ) )
            {
                if ( paperNumber.Length == 18 )
                {
                    validationMessage = string.Empty;
                    return true;
                }
                else
                {
                    validationMessage = "شماره برگ باید 18 رقم باشد";
                    return false;
                }

            }
            else
            {
                validationMessage = "شماره برگه وارد شده معتبر نیست";
                return false;
            }
        }
        #endregion

        #region CheckDigits2
        public static bool CheckDigits2 ( string text )
        {
            if ( string.IsNullOrEmpty ( text ) )
                return true;

            string pattern = @"([^0-9.])";
            Regex reg = new Regex(pattern);
            bool d = reg.IsMatch(text);
            return reg.IsMatch ( text );
        }
        #endregion
    }

}
