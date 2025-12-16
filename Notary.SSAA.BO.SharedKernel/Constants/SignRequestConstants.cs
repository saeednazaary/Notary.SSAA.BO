namespace Notary.SSAA.BO.SharedKernel.Constants
{
    public static class SignRequestConstants
    {
        public const string CreateIlm = "1";
        public const string SabtahvalChecked = "1";
        public const string UpdateIlm = "1";
        public const string PersonType = "1";
        public const string IsAlive = "1";
        public const string IsFingerprintGotten = "2";
        public const string State = "1";
        public const string IsCostPaid = "2";
        public const string AttachmentClientId = "9001";
        public const string ScanFileDocumentType = "0909";

        public const decimal sodor = 250000;
        public const decimal otherIncomePrice = 250000;
        public const decimal tahrirPricePerPerson = 1200000;
        public const int valueAddedPercent = 10;

        public const decimal sodorTEST = 2500;
        public const decimal otherIncomePriceTEST = 2500;
        public const decimal tahrirPricePerPersonTEST = 12000;
    }

    public static class SignRequestConfigConstants
    {
        public const string SanaConfig = "SignRequest-Sana-Service";
        public const string ShahkarConfig = "SignRequest-Shahkar-Service";
        //public const string MocConfig = "";
        public const string TFAConfig = "SignRequest-TFA-Service";
        public const string SabteAhvalConfig = "SignRequest-SabteAhval-Service";
        public const string AmlakEskanConfig = "SignRequest-AmlakEskan-Service";
        public const string PostConfig = "PostIsrRequired";
        public const string WorkPermit = "NewSystem-SignRequest-Scriptorium-Grants";
    }
    public static class SignRequestWorkTimeConfigConstants
    {
        public const string GetNationalNo = "SignRequest-NationalNo-WorkTime";
        public const string GetFingerprint = "SignRequest-GetFingerprint-Worktime";

    }
}
