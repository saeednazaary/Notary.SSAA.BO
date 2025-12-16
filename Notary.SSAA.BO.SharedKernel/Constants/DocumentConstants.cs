namespace Notary.SSAA.BO.SharedKernel.Constants
{
    public static class DocumentConstants
    {
        public const string CreateIlm = "1";
        public const string UpdateIlm = "1";
    }
    public static class DocumentTemporaryConstants
    {
        public const string IsAlive = "1";
        public const string IsFingerprintGotten = "2";
        public const string State = "1";
        public const string IsCostPaid = "2";
    }
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
    public static class CostTypeId
    {
        public const string carTaxId = "10";
        public const string haghSabtId = "1";
        public const string haghSabtElectronicId = "14";
        public const string haghTahrirId = "2";
        public const string haghTahrirOverPersonsId = "8";
        public const string paperOverCostId = "4";
        public const string taxId = "9";
        public const string haghSabHalfPercent = "20";
        public const string inquiryCostId = "7";
        public const string paperCostId = "3";
        public const string haghSabtSocialSecurityId = "15";

    }
    public static class CostTypeCode
    {
        public const string carTax = "10";
        public const string haghSabt = "01";
        public const string haghSabtElectronic = "14";
        public const string haghTahrir = "02";
        public const string haghTahrirOverPersons = "08";
        public const string paperOverCost = "04";
        public const string tax = "09";
        public const string haghSabHalfPercent = "20";
        public const string inquiryCost = "07";
        public const string paperCost = "03";
        public const string haghSabtSocialSecurity = "15";

    }
    public static class DocumentAgentType
    {

        //وکیل
        public const string Vakil = "1";
        //نماینده
        public const string Nemayande = "2";
        //ولی
        public const string Vali = "3";
        //مدیر
        public const string Modir = "4";
        //قائم مقام
        public const string GhaemMagham = "5";
        //متولی
        public const string Motevalli = "6";
        //قیم
        public const string Ghayem = "7";
        //وارث****
        public const string Vares = "8";
        //مورث****
        public const string Movares = "9";
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

}
