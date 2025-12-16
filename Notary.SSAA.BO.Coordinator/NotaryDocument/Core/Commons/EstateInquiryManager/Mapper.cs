using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Commons.EstateInquiryManager
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
        public static string GetEquivalantDSUTransferTypeID(string documentTypeCode, ref bool? isRestricted)
        {
            isRestricted = null;

            var mapping = new Dictionary<string, (string TransferId, bool Restricted)>
    {
        // انتقال قطعی
        { "111", ("26B149EBEEAB4FD8A00A52815E69095B", false) },
        { "979", ("26B149EBEEAB4FD8A00A52815E69095B", false) },
        { "115", ("26B149EBEEAB4FD8A00A52815E69095B", false) },

        // بیع شرطی
        { "112", ("89830F2E9C6B4E959FEAC12672951A1E", false) },

        // صلح
        { "211", ("07D8B7F98D624C73872869C45B521FCB", false) },
        { "212", ("6BD32003B9B44B6F8187DCF20D9E59ED", false) }, // صلح مشروط
        { "221", ("A48F344E2DF648E09A784C6EFA5D3721", true) }, // سرقفلی

        // رهني
        { "441", ("1", true) }, { "442", ("1", true) },
        { "411", ("1", true) }, { "412", ("1", true) },
        { "413", ("1", true) }, { "414", ("1", true) },
        { "426", ("1", true) }, { "415", ("1", true) },
        { "416", ("1", true) }, { "417", ("1", true) },
        { "431", ("1", true) },

        // اجاره
        { "511", ("58376D73759844609A201D8165E1AF0A", true) },
        { "225", ("58376D73759844609A201D8165E1AF0A", true) },

        // سایر
        { "611", ("3E12783909DA4F9681480FE9B8AD50E0", false) },
        { "612", ("3E12783909DA4F9681480FE9B8AD50E0", false) },
        { "971", ("3E12783909DA4F9681480FE9B8AD50E0", false) },

        // اقاله
        { "711", ("A30DC26C019C4F3482FF972D44665486", false) },

        // وصیت
        { "941", ("A2250C5DB4554BD28C75DB20B153387A", true) },

        // وقفنامه
        { "981", ("6106563D59CD478A8D5EC4A9B7FD32C6", false) },

        // فک رهن
        { "004", ("B8B9ABF3-26E1-41A1-B01F-A01D996A", true) },

        // پیش فروش
        { "901", ("214FD154DF05468BAE26B25057F73E86", true) }
    };

            if (mapping.TryGetValue(documentTypeCode, out var result))
            {
                isRestricted = result.Restricted;
                return result.TransferId;
            }

            return string.Empty; // default
        }
    }
}
