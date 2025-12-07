using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.InquiryFromUnit;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Commands.InquiryFromUnit
{
    public sealed class CreateInquiryFromUnitCommand : BaseCommandRequest<ApiResult<InquiryFromUnitViewModel>>
    {
        public CreateInquiryFromUnitCommand()
        {
            InquiryFromUnitPersons = new List<InquiryFromUnitPersonViewModel>();
        }
        public bool IsValid { get; set; }
        public bool IsNew { get; set; }
        public bool IsDelete { get; set; }
        public bool IsDirty { get; set; }
        public bool IsLoaded { get; set; }
        public string InquiryFromUnitId { get; set; }
        public IList<string> InquiryFromUnitTypeId { get; set; }
        public IList<string> InquiryUnitId { get; set; }
        public string InquiryStatementNo { get; set; }
        public string InquiryItemDescription { get; set; }
        public string InquiryText { get; set; }
        public IList<InquiryFromUnitPersonViewModel> InquiryFromUnitPersons { get; set; }
    }
}
