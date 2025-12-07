using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.Coordinator.NotaryDocument.Core
{
    public static class Mapper
    {
        public static List<string> DeterministicDocumentTypeCodes
        {
            get
            {
                return new List<string> ()
                {
                    "111", //سند قطعی غیر منقول 
                    "115", //سند قطعي مشتمل بر رهن - غيرمنقول
                    "112", //سند قطعي غيرمنقول با حق استرداد
                    "979", //صداق
                    "211", //سند صلح اموال غيرمنقول
                    "225", //سند صلح حق انتفاع - غيرمنقول
                    "212", //سند صلح اموال غيرمنقول با حق استرداد
                    "711", //سند اقاله اموال غيرمنقول
                    "971", //سند هبه غيرمنقول
                    //"941", //سند وصيت تمليكي
                    "981"  //اسناد وقف اموال غيرمنقول
                };
            }
        }

        /// <summary>
        /// این نوع سند ها دارای خلاصه های از نوع محدودیت می باشند ولی برای ارسال خلاصه معامله سهم مالک از اطلاعات استعلام کپی می گردد و از سرویس املاک استفاده نمی گردد.
        /// </summary>
        public static List<string> SemiRestrictedDocTypes
        {
            get
            {
                return new List<string> ()
                {
                    "941" //وصیت نامه تکمیلی
                };
            }
        }

        public static bool IsONotaryDocumentRestrictedType ( string documentTypeCode )
        {
            if ( string.IsNullOrWhiteSpace ( documentTypeCode ) )
                return false;

            string dsuTransferTypeID = string.Empty;
            bool? isRestricted = false;
            dsuTransferTypeID = GetEquivalantDSUTransferTypeID ( documentTypeCode, ref isRestricted );
            if ( isRestricted.HasValue )
                return isRestricted.Value;
            else
                return false;
        }

        public static string GetEquivalantDSUTransferTypeID ( string documentTypeCode, ref bool? isRestricted )
        {
            string dSUTransferTypeID = string.Empty;
            isRestricted = null;

            switch (documentTypeCode)
            {
                // 🔸 انتقال قطعی
                case "111": //سند قطعي غيرمنقول
                case "979": //سند صداق
                case "115": //سند قطعي مشتمل بر رهن - غيرمنقول
                    dSUTransferTypeID = "26B149EBEEAB4FD8A00A52815E69095B";
                    isRestricted = false;
                    break;

                // 🔸 بیع شرطی
                case "112":
                    dSUTransferTypeID = "89830F2E9C6B4E959FEAC12672951A1E";
                    isRestricted = false;
                    break;

                // 🔸 صلح
                case "211":
                    dSUTransferTypeID = "07D8B7F98D624C73872869C45B521FCB";
                    isRestricted = false;
                    break;

                // 🔸 صلح مشروط
                case "212":
                    dSUTransferTypeID = "6BD32003B9B44B6F8187DCF20D9E59ED";
                    isRestricted = false;
                    break;

                // 🔸 واگذاری سرقفلی
                case "221":
                    dSUTransferTypeID = "A48F344E2DF648E09A784C6EFA5D3721";
                    isRestricted = true;
                    break;

                // 🔸 رهني
                case "441": //سند متمم رهني مالي
                case "442": //سند متمم رهني غيرمالي
                case "411": //سند جعاله
                case "412": //سند فروش اقساطي
                case "413": //سند مساقات
                case "414": //سند رهني مسكن
                case "426": //سند رهني غیر بانکی غیر ملکی
                case "415": //سند مشاركت مدني
                case "416": //سند مضاربه
                case "417": //ساير تسهيلات بانكي
                case "431": //سند تعويض يا ضم وثيقه
                    dSUTransferTypeID = "1";
                    isRestricted = true;
                    break;

                // 🔸 اجاره
                case "511": //سند اجاره اموال غيرمنقول
                case "225": //سند صلح حق انتفاع - غيرمنقول
                    dSUTransferTypeID = "58376D73759844609A201D8165E1AF0A";
                    isRestricted = true;
                    break;

                // 🔸 سایر
                case "611": //سند تقسيم نامه با صورتمجلس تفكيكي
                case "612": //سند تقسيم نامه بدون صورتمجلس تفكيكي
                case "971": //سند هبه غيرمنقول
                    dSUTransferTypeID = "3E12783909DA4F9681480FE9B8AD50E0";
                    isRestricted = false;
                    break;

                // 🔸 اقاله
                case "711":
                    dSUTransferTypeID = "A30DC26C019C4F3482FF972D44665486";
                    isRestricted = false;
                    break;

                // 🔸 وصیت
                case "941":
                    dSUTransferTypeID = "A2250C5DB4554BD28C75DB20B153387A";
                    isRestricted = true;
                    break;

                // 🔸 وقف
                case "981":
                    dSUTransferTypeID = "6106563D59CD478A8D5EC4A9B7FD32C6";
                    isRestricted = false;
                    break;

                // 🔸 فک رهن
                case "004":
                    dSUTransferTypeID = "B8B9ABF3-26E1-41A1-B01F-A01D996A";
                    isRestricted = true;
                    break;

                // 🔸 پیش فروش
                case "901":
                    dSUTransferTypeID = "214FD154DF05468BAE26B25057F73E86";
                    isRestricted = true;
                    break;

                default:
                    break;
            }

            return dSUTransferTypeID;
        }
    }



}
