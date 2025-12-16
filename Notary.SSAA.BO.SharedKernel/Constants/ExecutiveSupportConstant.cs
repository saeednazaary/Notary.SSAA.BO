namespace Notary.SSAA.BO.SharedKernel.Constants
{
    public static class ExecutiveSupportConstant
    {
        public static class State
        {
            public const string Valid = "1";
            public const string InValid = "2";
            //تنظیم شده
            public const string Created = "44";
            //پاسخ دريافت شده
            public const string AnswerReceived = "43";
            //رد شده توسط اداره اجرا
            public const string Rejected = "45";
            //ارسال شده به اداره اجرا
            public const string Sended = "42";
            //لغو شـده
            public const string Canceled = "46";
        }
        public static class YesNo
        {
            public const string Yes = "1";

            public const string No = "2";
        }
        public static class General
        {
            public const string Ilm = "1";
        }
        public static class BaseInfoServiceApis
        {
            //واحد سازمانی
            public const string Unit = "Executive/Unit";
            //نوع واحد پولی /نوع واحد اندازه گیری 
            public const string MeasurementUnitType = "Executive/MeasurementUnitType";
            //بانک
            public const string Bank = "Executive/Bank";
            //ملیت
            public const string Nationality = "Executive/Nationality";
            //واحد سازمانی
            public const string ExecutiveRequestUnit = "Executive/ExecutiveRequestUnit";
            //دفترخانه
            public const string Scriptorium = "Executive/Scriptorium";
            //نوع شرکت
            public const string CompanyType = "Executive/CompanyType";
            //ماهیت
            public const string LegalPersonNature = "Executive/LegalPersonNature";
            //نوع شخص حقوقی
            public const string LegalPersonType = "Executive/LegalPersonType";
            //نوع سند
            public const string ExecutiveREquestAttachmentType = "LoadAttachmentTypes";
        }
        public static class ExecutiveSupportType
        {
            public static class Codes
            {
                /// <summary>
                /// معرفي اموال
                /// </summary>
                public const string Moarrefi_Amval = "00006";

                /// <summary>
                /// اعلام وصول طلب
                /// </summary>
                public const string Elame_Vosoule_Talab = "00010";

                /// <summary>
                /// معرفي نماينده
                /// </summary>
                public const string Moarefi_Namayandeh = "00014";

                /// <summary>
                /// تقاضاي كسر حقوق
                /// </summary>
                public const string Taghazaye_Kasr_Hoghough = "00023";

                /// <summary>
                /// اعتراض به عمليات اجرايي
                /// </summary>
                public const string Eteraz_Be_Amaliat_Ejraee = "00025";

                /// <summary>
                /// درخواست عدم ابلاغ تا اطلاع ثانوي
                /// </summary>
                public const string Darkhaste_Adam_Eblagh_Ta_Etelae_Sanavi = "00028";

                /// <summary>
                /// درخواست توقف عمليات اجرايي تا اطلاع ثانوي
                /// </summary>
                public const string Darkhaste_Tavaghof_Amaliat_Ejraee_Ta_Etelae_Sanavi = "00029";

                /// <summary>
                /// درخواست تغيير حافظ
                /// </summary>
                public const string Darkhaste_Tagheere_Hafez = "00031";

                /// <summary>
                /// درخواست كپي
                /// </summary>
                public const string Darkhaste_Copy = "00035";

                /// <summary>
                /// درخواست فك رهن
                /// </summary>
                public const string Darkhaste_fake_rahn = "00037";

                /// <summary>
                /// درخواست واريز مبلغ
                /// </summary>
                public const string Darkhaste_Varize_Mablagh = "00041";

                /// <summary>
                /// درخواست فك قفل
                /// </summary>
                public const string Darkhaste_Fake_Ghofl = "00042";

                /// <summary>
                /// گزارش عمليات اجرايي
                /// </summary>
                public const string Gozareshe_Amaliat_Ejraee = "00045";

                /// <summary>
                /// اعلام وضعيت بيمه
                /// </summary>
                public const string Elame_Vaziate_Bime = "00046";

                /// <summary>
                /// درخواست تخليه
                /// </summary>
                public const string Darkhate_Takhlie = "00051";

                /// <summary>
                /// درخواست احتساب مانده بدهي
                /// </summary>
                public const string Darkhaste_Ehtesab_Mande_Bedehi = "00057";

                /// <summary>
                /// گواهي عمليلات اجرايي جهت ارائه به مراجع قضايي
                /// </summary>
                public const string Govahi_Amaliate_Ejraee_Jahate_Eraye_Be_Maraje_Ghazaee = "00060";

                /// <summary>
                /// درخواست ممنوع الخروجي
                /// </summary>
                public const string Darkhate_MamnooOl_Khorooji = "00066";

                /// <summary>
                /// درخواست تسريع در ارسال جواب
                /// </summary>
                public const string Darkhaste_Tasree_Dar_Ersale_Javab = "00068";

                /// <summary>
                /// درخواست ارسال تصوير اجراييه ابلاغ شده
                /// </summary>
                public const string Darkhaste_Ersale_tasvie_Ejraee_Eblagh_Shode = "00070";

                /// <summary>
                /// درخواست اعلام نتيجه اقدامات
                /// </summary>
                public const string Darkhaste_Elam_Natije_Eghdamat = "00083";

                /// <summary>
                /// درخواست ارزيابي پلاك ثبتي
                /// </summary>
                public const string Darkhaste_Arzyabi_Pelake_Sabti = "00090";

                /// <summary>
                /// اعلام شماره حساب
                /// </summary>
                public const string Elame_Shomare_Hesab = "00093";

                /// <summary>
                /// اعلام وكالت
                /// </summary>
                public const string Elame_Vekalat = "00101";

                /// <summary>
                /// آگهي مزايده منقول
                /// </summary>
                public const string Agahi_Mozayede_Manghool = "00103";

                /// <summary>
                /// آگهي مزايده غير منقول
                /// </summary>
                public const string Agahi_Mozayede_Gheire_Manghool = "00104";

                /// <summary>
                /// اعتراض به نظريه معاونت
                /// </summary>
                public const string Eteraz_Be_Nazarie_Moavenat = "00105";

                /// <summary>
                /// ابلاغ اجرائيه از طريق جرايد
                /// </summary>
                public const string Eblagh_Ejraee_Az_Tarighe_Jarayed = "00108";

                /// <summary>
                /// مغايرت مشخصات سجلي
                /// </summary>
                public const string Moghayerate_Moshakhasate_Sejeli = "00110";

                /// <summary>
                /// بازداشت ديه
                /// </summary>
                public const string Bazdashte_Diye = "00111";

                /// <summary>
                /// اعتراض به نظر كارشناس،ارزيابي و حسابدار
                /// </summary>
                public const string Eteraz_Be_Nazare_Karshenas_Azryabi_Va_Hesabdar = "00115";

                /// <summary>
                /// درخواست ادامه عمليات اجرايي
                /// </summary>
                public const string Darkhaste_Edame_Amaliat_Ejraee = "00119";

                /// <summary>
                /// نشاني جديد اعلامي متعهد له
                /// </summary>
                public const string Neshani_Jadide_Elami_Moteahed_Leh = "00127";

                /// <summary>
                /// نظريه كارشناس و كروكي
                /// </summary>
                public const string Nazarie_Karshnas_Va_Korooki = "00128";

                /// <summary>
                /// حق الزحمه كارشناس
                /// </summary>
                public const string HaghOl_Zahme_Karshenas = "00129";

                /// <summary>
                /// مغايرت پلاك ثبتي
                /// </summary>
                public const string Moghayerat_Pelake_Sabti = "00132";

                /// <summary>
                /// فيش مابه التفاوت
                /// </summary>
                public const string Fish_Ma_BeOl_Tafavot = "00134";

                /// <summary>
                /// تقاضاي توقيف خودرو
                /// </summary>
                public const string Taghazaye_toghife_Khodro = "00135";

                /// <summary>
                /// درخواست ارزيابي
                /// </summary>
                public const string Darkhate_Arzyabi = "00138";

                /// <summary>
                /// اعلام تاريخ خسارت
                /// </summary>
                public const string Elame_Tarikh_Khesarat = "00143";

                /// <summary>
                /// اعلام نرخ سود
                /// </summary>
                public const string Elame_Nerkhe_Soud = "00144";

                /// <summary>
                /// اعلام تاريخ تسويه
                /// </summary>
                public const string Elame_Tarikhe_Tasvie = "00152";

                /// <summary>
                /// ارسال اقرارنامه
                /// </summary>
                public const string Ersale_EghrarName = "00154";

                /// <summary>
                /// واريز مبلغ كسر از حقوق
                /// </summary>
                public const string Variz_Mablagh_kasr_Az_Hoghough = "00157";

                /// <summary>
                /// درخواست خودداري از ابلاغ اجرائيه
                /// </summary>
                public const string Darkhaste_Khoddari_Az_Eblagh_Ejraee = "00158";

                /// <summary>
                /// تقاضاي بازداشت تلفن همراه
                /// </summary>
                public const string Taghazaye_Bazdashte_Telephone_Hamrah = "00159";

                /// <summary>
                /// تقاضاي بازداشت تلفن ثابت
                /// </summary>
                public const string Taghazaye_Bazdasht_Telephone_Sabet = "00160";

                /// <summary>
                /// تقاضاي تحويل اموال
                /// </summary>
                public const string Taghazaye_Tahvil_Amval = "00162";

                /// <summary>
                /// درخواست تعيين حق الحفاظه
                /// </summary>
                public const string Darkhaste_Taeen_Haghol_Hefaze = "00163";

                /// <summary>
                /// تقاضاي بازداشت پلاك ثبتي
                /// </summary>
                public const string Taghazaye_Bazdasht_Pelake_Sabti = "00164";

                /// <summary>
                /// تقاضاي بازداشت سرقفلي
                /// </summary>
                public const string Taghazaye_Bazdasht_Sarghofli = "00165";

                /// <summary>
                /// تقاضاي استرداد چك
                /// </summary>
                public const string Taghazaye_Esterdade_Check = "00166";

                /// <summary>
                /// تقاضاي پرداخت بدهي توسط بدهكار
                /// </summary>
                public const string Taghazaye_Pardakht_Bedehi_Tavasot_Bedehkar = "00167";

                /// <summary>
                /// اعلام كد ملي بدهكار
                /// </summary>
                public const string Elame_Kode_Melli_Bedehkar = "00168";

                /// <summary>
                /// پرداخت وجه به بستانكار
                /// </summary>
                public const string Pardaskht_Vajh_be_Bestankar = "00169";

                /// <summary>
                /// اعمال ماده 201 آيين نامه اجرايي
                /// </summary>
                public const string Emale_Made_201_Aeen_Name_Ejraee = "00170";

                /// <summary>
                /// ارائه رضايت نامه
                /// </summary>
                public const string Eraye_RezayatName = "00172";

                /// <summary>
                /// ارائه صلح نامه
                /// </summary>
                public const string Eraye_SolhName = "00173";

                /// <summary>
                /// درخواست جابجايي اموال بازداشتي
                /// </summary>
                public const string Darkhast_Jabejaee_Amval_Bazdashti = "00174";

                /// <summary>
                /// عزل وكيل
                /// </summary>
                public const string Azle_Vakil = "00175";

                /// <summary>
                /// انصراف كارشناس از ارزيابي
                /// </summary>
                public const string Enserafe_Karshenas_Az_Arzyabi = "00180";

                /// <summary>
                /// تقاضاي بازداشت سهم الارث
                /// </summary>
                public const string Taghazaye_Bazdasht_Sahmolers = "00183";

                /// <summary>
                /// تقاضاي بازداشت اموال منقول
                /// </summary>
                public const string Taghazaye_Bazdasht_Amvale_Manghool = "00187";

                /// <summary>
                /// تقاضاي بازداشت سهام
                /// </summary>
                public const string Taghazaye_Bazdasht_Saham = "00190";
                /// <summary>
                /// تقاضاي بازداشت اموال منقول نزد شخص ثالث
                /// </summary>
                public const string Taghazaye_Bazdasht_Amvale_Manghool_Nazd_Shakhse_Sales = "00191";

                /// <summary>
                /// تقاضاي بازداشت ديه
                /// </summary>
                public const string Taghazaye_Bazdasht_Diye = "00194";

                /// <summary>
                /// گزارش ارزيابي توسط كارشناس
                /// </summary>
                public const string Gozaresh_Arzyabi_Tavasote_Karshenas = "00195";

                /// <summary>
                /// گزارش ارزيابي مجدد
                /// </summary>
                public const string Gozaresh_Arzyabi_Mojadad = "00196";

                /// <summary>
                /// اعتراض به نظريه رئيس ثبت محل
                /// </summary>
                public const string Eteraz_Be_Nazarie_raees_Sabt_Mahal = "00197";

                /// <summary>
                /// اعتراض به راي هيئت نظارت
                /// </summary>
                public const string Eteraz_Be_Ray_Heiat_Nezarat = "00198";

                /// <summary>
                /// صورتجلسه تخليه- تحويل مال به موجر
                /// </summary>
                public const string SouratJalaseh_Takhlie_TahvileMal_Be_Mojer = "00199";

                /// <summary>
                /// صورتجلسه تخليه- تحويل مال به خريدار
                /// </summary>
                public const string SouratJalaseh_Takhlie_TahvileMal_Be_Kharidar = "00200";

                /// <summary>
                /// اعلام سازش
                /// </summary>
                public const string Elam_Sazesh = "00201";

                /// <summary>
                /// نشاني جديد اعلامي متعهد
                /// </summary>
                public const string Neshani_Jadid_Elami_Moteahed = "00203";

                /// <summary>
                /// نامه اعراض از رهن
                /// </summary>
                public const string Name_Eraz_Az_Rahn = "00206";
            }
        }
        public static class PersonType
        {
            //حقیقی
            public const string Haghighi = "1";
            //حقوقی
            public const string Hoghughi = "2";
        }
        public static class TrueFalse
        {
            public const string True = "1";

            public const string False = "2";
        }
        public static class ExecutiveGeneralPersonPostType
        {
            //متعهد
            public const string Deptor = "1";
            //متعهدله
            public const string Creditor = "2";
            //نماینده
            public const string Agent = "2";
            //سایر
            public const string Other = "2";
        }
        public static class NationalityCode
        {
            //ایرانی
            public const string Iran = "0024000010000001";
        }

    }
}


