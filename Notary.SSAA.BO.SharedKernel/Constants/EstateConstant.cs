using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.SharedKernel.Constants
{
    public class EstateConstant
    {
        public class BooleanConstant
        {
            public const string True = "1";
            public const string False = "2";
        }
        public class PersonTypeConstant
        {
            public const string RealPerson = "1";
            public const string LegalPerson = "2";
        }
        public class EstateInquiryStates
        {
            public const string Sended = "7";
            public const string EditAndReSend = "8";
            public const string ConfirmResponse = "9";
            public const string RejectResponse = "10";
            public const string NeedDocument = "11";
            public const string NeedCorrection = "12";
            public const string Archived = "13";
            public const string NotSended = "14";
            public const string None = "72";
            public const string Invisible = "74";
        }

        public class ForestOrganizationInquiryStates
        {
            public const string NotSended = "24";
            public const string Sended = "25";
            public const string Responsed = "26";
            public const string Defective = "47";
        }

        public class DealSummaryStates
        {
            public const string NotSended = "15";
            public const string Sended = "16";
            public const string Responsed = "17";
            public const string Archived = "19";
            public const string SendRemoveRestriction = "18";
            public const string None = "73";
        }

        public class EstateDocumentRequestStates
        {
            public const string PrimaryRegistry = "23";
            public const string Sent = "21";
            public const string Canceled = "20";
            public const string Rejected = "22";
        }
        public class EstateInquirySpecificStatus
        {
            //فقط مجاز برای تقسیم نامه
            public const string OnlyAllowedForDivision = "1";
            //دارای بازداشت مالکیت
            public const string Arrested = "2";
            //دارای بازداشت مالکیت و فقط مجاز برای تقسیم نامه
            public const string ArrestedAndOnlyAllowedForDivision = "3";
            // مجاز برای تقسیم نامه
            public const string AllowedForDivision = "9";

        }
        public static class RelatedAgentType
        {

            //وکیل
            public const string Vakil = "01";
            //نماینده
            public const string Nemayande = "02";
            //ولی
            public const string Vali = "03";
            //مدیر
            public const string Modir = "04";
            //قائم مقام
            public const string GhaemMagham = "05";
            //متولی
            public const string Motevalli = "06";
            //قیم
            public const string Ghayem = "07";
            //وارث****
            public const string Vares = "08";
            //مورث****
            public const string Movares = "09";
            //معتمد
            public const string Motamed = "10";
            //معرف
            public const string Moaref = "11";
            //مترجم
            public const string Motarjem = "12";
            //موصی****
            public const string Movasi = "13";
            //دادستان یا رئیس دادگاه بخش
            public const string DadsetanYaRaisDadgahBakhsh = "14";
            //شاهد****
            public const string Shahed = "15";
            //وصی****
            public const string Vasi = "16";
            //امین****
            public const string Amin = "17";
            //نماینده مقام قضایی
            public const string NemayandeMaghamGhazayi = "18";

        }
        public static class PersonSexType
        {
            //زن
            public const string Female = "1";
            //مرد
            public const string Male = "2";
        }
        public class EstateElzamSixArtInquiryStates
        {
            public const string NotSended = "75";
            public const string Sended = "76";
            public const string Responsed = "77";
           
        }

        public class EstateElzamSixRelationType
        {
            //فروشنده
            public const string Seller = "100";
            //خریدار
            public const string Buyer = "101";
        }
    }
}
