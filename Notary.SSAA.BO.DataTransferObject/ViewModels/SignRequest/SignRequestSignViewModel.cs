

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest
{
    public class SignRequestSignViewModel
    {
        public SignRequestSignViewModel()
        {
            SignRequestPeople = new();
            SignRequestPersonRelateds = new();
        }
        public string Id { get; set; }
        public string ReqNo { get; set; }
        public string ReqDate { get; set; }
        public string ReqTime { get; set; }
        public string ScriptoriumId { get; set; }
        public string SignRequestGetterId { get; set; }
        public string SignRequestSubjectId { get; set; }
        public string SignText { get; set; }
        public string IsCostPaid { get; set; }
        public string SumPrices { get; set; }
        public string ReceiptNo { get; set; }
        public string PayCostDate { get; set; }
        public string PayCostTime { get; set; }
        public string PaymentType { get; set; }
        public string BillNo { get; set; }
        public string NationalNo { get; set; }
        public string SecretCode { get; set; }
        public string SignDate { get; set; }
        public string SignTime { get; set; }
        public string Modifier { get; set; }
        public string ModifyDate { get; set; }
        public string ModifyTime { get; set; }
        public string Confirmer { get; set; }
        public string ConfirmDate { get; set; }
        public string ConfirmTime { get; set; }
        public string Description { get; set; }
        public string State { get; set; }
        public string HowToPay { get; set; }
        public List<SignRequestPersonSignViewModel> SignRequestPeople { get; set; }
        public List<SignRequestPersonRelatedSignViewModel> SignRequestPersonRelateds { get; set; }
    }
}
