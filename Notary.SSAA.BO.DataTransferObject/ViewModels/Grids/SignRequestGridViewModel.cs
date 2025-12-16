using System.ComponentModel.DataAnnotations.Schema;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Grids
{
    public sealed class SignRequestGridViewModel
    {
        public SignRequestGridViewModel()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<SignRequestGridItemViewModel> GridItems { get; set; }
        public List<SignRequestGridItemViewModel> SelectedItems { get; set; }
    }

    public record SignRequestGridItemViewModel
    {
        public string Id { get; set; }
        public string ReqNo { get; set; }
        public string NationalNo { get; set; }
        public string ReqDate { get; set; }
        public string SignDate { get; set; }
        public string SignRequestSubjectTitle { get; set; }
        public string SignRequestGetterTitle { get; set; }
        public string Persons { get; set; }
        public string StateId { get; set; }
        public string StateTitle => this.StateId switch
        {
            "1" => "پرونده ایجاد شده است",
            "2" => "تأیید نهایی شده است",
            "3" => "پرونده بسته شده است",
            _ => "وضعیت نا مشخص است",
        };
        public bool IsSelected { get; set; }
    }
}
