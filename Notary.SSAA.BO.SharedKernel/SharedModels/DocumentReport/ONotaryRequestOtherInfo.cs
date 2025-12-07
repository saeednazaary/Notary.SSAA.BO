

namespace Notary.SSAA.BO.SharedKernel.SharedModels.DocumentReport
{
    public class ONotaryRequestOtherInfo
    {
        public string LegalText { get; set; }
        public int? IsDocBasedJudgeHokm { get; set; }
        public string IsDocBasedJudgeHokmTitle { get; set; }
        public string HokmTypeTitle { get; set; }
        public int? HokmType { get; set; }
        public string HokmNoLebel { get; set; }
        public string HokmDateLabel { get; set; }
        public string CaseClassifyNo { get; set; }
        public string DadnamehNo { get; set; }
        public string DadnamehDate { get; set; }
        public string DadnamehIssuerName { get; set; }
        public decimal? DividedSectionsCount { get; set; }
        public decimal? RegCount { get; set; }
        public int? HasDismissalPermit { get; set; }
        public string HasDismissalPermitTitle { get; set; }
        public int? HasAdvocacy2OthersPermit { get; set; }
        public string HasAdvocacy2OthersPermitTitle { get; set; }
        public int? HasTime { get; set; }
        public string HasTimeTitle { get; set; }
        public string AdvocacyEndDate { get; set; }

        public string RentStartDate { get; set; }
        public string RentDuration { get; set; }
        public int? IsPeace4Lifetime { get; set; }
        public string IsPeace4LifetimeTitle { get; set; }
        public string PeaceDuration { get; set; }
        public int? HagheEntefae { get; set; }
        public string HagheEntefaeTitle { get; set; }
        public string VaghfTypeTitle { get; set; }
        public int? IsRentWithSarGhofli { get; set; }
        public string IsRentWithSarGhofliTitle { get; set; }
        public string MortageDuration { get; set; }
        public string MortageUnit { get; set; }
        public string DocumentGroup1Code { get; set; }
        public string ConditionsText { get; set; }
    }
}
