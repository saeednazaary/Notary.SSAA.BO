

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest
{
    public class SignRequestStatisticViewModel
    {
        public SignRequestStatisticViewModel()
        {
            signRequestStatisticItems = new List<SignRequestStatisticItem>();
        }
        public string FromDate { get; set;}
        public string ToDate { get; set; }
        public string CurrentDate { get; set; }
        public string SumSign { get; set; }
        public IList<SignRequestStatisticItem> signRequestStatisticItems { get; set; }
    }

    public class SignRequestStatisticItem
    {
        public string Getters { get; set; }
        public string Subjects { get; set; }
        public string FullName { get; set; }
        public string NationalNo { get; set; }
        public string SignDate { get; set; }
        public string ClassifyNo { get; set; }
    }
}
