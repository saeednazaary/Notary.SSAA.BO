using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices.Estate;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices.Estate
{
    public class ConfirmDocumentByElectronicEstateNoteNoQuery : BaseQueryRequest<ApiResult<ConfirmDocumentByElectronicEstateNoteNoViewModel>>
    {
        public ConfirmDocumentByElectronicEstateNoteNoQuery()
        {
            ElectronicEstateNoteNo = string.Empty;
            NationalityCode = string.Empty;
        }
        public string ElectronicEstateNoteNo { get; set; }
        public string NationalityCode { get; set; }
        public ConfirmDocumentByElectronicEstateNoteNoUIForm? UIForm { get; set; }
    }

    public enum ConfirmDocumentByElectronicEstateNoteNoUIForm
    {
        None = 0,
        EstateTaxInquiry = 1,
        EstateArticleSixInquiry = 2
    }
}
