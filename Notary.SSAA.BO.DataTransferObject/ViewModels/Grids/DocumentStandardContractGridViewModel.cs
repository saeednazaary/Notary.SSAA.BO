using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Grids
{
    public sealed class DocumentStandardContractGridViewModel
    {
        public DocumentStandardContractGridViewModel()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<DocumentStandardContractGridItemViewModel> GridItems { get; set; }
        public List<DocumentStandardContractGridItemViewModel> SelectedItems { get; set; }
    }
    public class DocumentStandardContractGridItemViewModel
    {
        public string Id { get; set; }
        public string RequestNo { get; set; }
        public string RelatedDocumentTypeId { get; set; }
        public string DocumentTypeId { get; set; }
        public string ClassifyNo { get; set; }
        public string RequestDate { get; set; }
        public string DocumentPersons { get; set; }
        public string DocumentTypeTitle { get; set; }
        public string DocumentCases { get; set; }
        public string StateId { get; set; }
        public string StateTitle => this.StateId switch
        {
            "1" => "پرونده ایجاد شده است",
            "3" => "هزينه ها محاسبه شده است",
            "4" => "هزينه ها پرداخت شده است",
            "5" => "سند شناسه يکتا گرفته است",
            "6" => "سند توسط سردفتر تاييد نهايي شده است",
            "7" => "چاپ نسخه پشتيبان سند گرفته شده است",
            "8" => "بعد از اخذ شناسه يکتا، پرونده بسته شده است",
            "9" => "قبل از اخذ شناسه يکتا، پرونده بسته شده است",
            _ => "وضعیت نا مشخص است",
        };
        public bool IsSelected { get; set; }
    }
}
