namespace Notary.SSAA.BO.SharedKernel.Constants
{
    public static class ExecutiveRequestConstant
    {
        public static class State
        {
            public const string Valid = "1";
            public const string InValid = "2";
            //وضعیت های تقاضانامه
            //پرونده ایجاد شده است
            public const string Created = "1";
            //چاپ گرفته شده است
            public const string Printed = "2";
            //به اداره اجرا ارسال شده است
            public const string Sended = "3";
            //لغوشده است
            public const string Canceled = "4";
            //اعلام نقص شده است
            public const string Faulted = "5";
            //تقاضانامه رفع نقص ارسال شده است
            public const string EliminationFaults = "6";
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

        public static class ExecutiveRequestType
        {
            //چک بلا محل
            public const string ChekBelaMahal = "001";
            //قبوض اقساطی
            public const string GhobuzAghsati = "003";
            //قرارداد بانکی
            public const string GharardadBanki = "007";
            //قرارداد شرکت تعاونی
            public const string GharardadSherkatTavoni = "008";
            //شارژ آپارتمان
            public const string SharJhApartman = "009";
            //آراء کمیسیون قانون وصول بهای آب
            public const string AraComisionGhanoonVosulAb = "011";
            //داوری بورس اوراق بهادار
            public const string DavariBursOraghBahadar = "014";
            //برگ وثیقه انبارهای عمومی
            public const string BargVasigheAnbarhayeOmumi = "015";
            //شارژ واحدهای صنعتی
            public const string SharjhVahedhayeSanati = "021";
            //تلفن بها
            public const string TelBaha = "022";
            //آب بهای آب سازمانها و شرکت های تابع وزارت نیرو
            public const string AbBahayeAbSazmanha = "026";
            //وصول بهاء فاضلاب
            public const string VosulBahayeFazlab = "027";
            //عوارض شهرداری جدید
            public const string AvarezShahrdariJadid = "029";
            //قبض تخلیه فروشگاه های جدید
            public const string GhabzTakhliyeFrushgahhayeJadid = "030";
            //پرداخت خسارت بیمه گری
            public const string PardakhtKhesaratBimegari = "031";
            //آب بهاء جدید
            public const string AbBahaJadid = "032";
            //برق بهاء جدید
            public const string BarghBahaJadid = "033";
            //گاز بهاء جدید
            public const string GazBahaJadid = "034";
            //مطالبات فرودگاه های کشور جدید
            public const string MotalebatFroodgahhayeKeshvarJadid = "036";
            //مطالبات موضوع ماده 103 قانون شهرداري
            public const string MotalebatMozoMade103GhanoonShahrdari = "037";
            //مطالبات سازمان حفظ نباتات کشور
            public const string MotalebatSazmanHefzNabatatKeshvar = "038";
            //مطالبات صندوق تأمين خسارتهاي بدني
            public const string MotalebatSandoghTaminKhesarathayeBadani = "039";

            //***************** تایپ های زیر نباید در لوکاپ نوع اجرائیه باشند *********************

            //مهریه
            public const string Mahriye = "002";
            //سند اجاره
            public const string SanadEjare = "004";
            //سند معامله قطعی
            public const string SanadMoameleGhati = "005";
            //سند رهنی و شرطی
            public const string SanadRahniVaSahrti = "006";
            //سند تعهدی
            public const string SanadTahodi = "018";
            //تخلیه مورد صلح به استناد سند صلح
            public const string TakhliyeMoredSolh = "019";

        }

        public static class BaseInfoServiceApis
        {
            //نوع واحد پولی /نوع واحد اندازه گیری 
            public const string MeasurementUnitType = "Executive/MeasurementUnitType";
            //بانک
            public const string Bank = "Executive/Bank";
            //ملیت
            public const string Nationality = "Executive/Nationality";
            //واحد سازمانی
            public const string ExecutiveRequestUnit = "Executive/Unit";
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

        public static class ReliablePersonReason
        {
            //اشخاص بی سواد
            public const string AshkhasBisavad = "01";
            //اشخاص کر،کور،گنگ بی سواد
            public const string AshkhasKarBimar = "02";
            //اشخاص بیمار
            public const string AshkhasBimar = "03";
            //اشخاص معلول بی دست و پا
            public const string AshkhasMalulBidastoPa = "04";
            //زندانیان
            public const string Zendaniyan = "05";
            //سردفتران و دفترياران و اقارب و خدمه آنها
            public const string SarDaftaranODaftaryaran = "06";
            //اشخاص بدون اثر انگشت
            public const string AshkhasBedunAsarAngosht = "07";
        }

        public static class BindingSubgectType
        {
            //وصول وجه چک
            public const string VosulVajhChek = "001";
            //وصول وجه قبوض
            public const string VosulVajhGhobuz = "003";
            //تخلیه مورد اجاره
            public const string TakhliyeMoredEjare = "004";
            //اصل طلب
            public const string AslTalab = "008";
            //سود
            public const string Sood = "009";
            //خسارت تاخیر تادیه
            public const string KhesaratTakhirTadiye = "010";
            //خسارت تاخیر روزانه
            public const string KhesaratTakhirRoozane = "011";
            //حق الوکاله
            public const string HagholVekale = "012";
            //کارمزد
            public const string Karmozd = "013";
            //وصول حق بیمه پرداخت شده
            public const string VosulHaghBimePardakhtShode = "020";
        }

        public static class PersonPostType
        {
            //متعهد
            public const string Motaahed = "001";
            //متعهدله
            public const string MotaahedoLah = "002";
            //ورثه متعهد
            public const string VaraseMotaahed = "004";
            //ورثه متعهد له
            public const string VaraseMotaahedoLah = "005";
            //دارنده چك
            public const string DarandeChek = "008";
            //صادركننده چك
            public const string SaderKonandeChek = "009";
            //ضامن
            public const string Zamen = "011";
            //موجر
            public const string Mojer = "022";
            //مستاجر
            public const string Mostajer = "023";
            //مالك يا متصرف
            public const string MalekYaMotesaref = "026";
            //مدير يا هيات مديره
            public const string ModirYaHeyatModire = "027";
            //ورثه ضامن
            public const string VaraseZamen = "028";
            //بستانکار
            public const string Bestankar = "029";
            //بدهکار
            public const string Bedehkar = "030";
            //ورثه بدهكار
            public const string VaraseBedehkar = "031";
            //ورثه بستانكا
            public const string VaraseBestankar = "032";
            //ورثه موجر
            public const string VaraseMojer = "033";
            //ورثه مستاجر
            public const string VaraseMostajer = "034";
            //ورثه دارنده چك
            public const string VaraseDarandeChek = "042";
            //ورثه صادر كننده چك
            public const string VaraseSaderKonnadeChek = "043";
            //بيمه گذار
            public const string BimeGozar = "049";
            //بيمه گر
            public const string Bimegar = "050";
            //صندوق تامين خسارتهاي بدني
            public const string SandoghTaminKhesarathayeBadani = "052";
            //شخص ثالث-ضامن دين
            public const string ShakhsSalesZamenDin = "111";
            //مدير تصفي
            public const string ModirTasfiye = "117";
            //شخص مسبب حادثه
            public const string ShakhsMosababHadese = "123";

        }

        public static class PersonType
        {
            //حقیقی
            public const string Haghighi = "1";
            //حقوقی
            public const string Hoghughi = "2";
        }

        public static class PersonSexType
        {
            //زن
            public const string Female = "1";
            //مرد
            public const string Male = "2";
        }
        public static class ReportName
        {
            public const string FinalReport = "\\Content\\Reports\\Executive\\ExecutiveRequest\\ExecutiveRequestFinalReport.mrt";
            public const string DraftReport = "\\Content\\Reports\\Executive\\ExecutiveRequest\\ExecutiveRequestDraftReport.mrt";
        }
    }
}
