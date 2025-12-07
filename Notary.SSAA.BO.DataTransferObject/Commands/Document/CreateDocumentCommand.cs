using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Commands.Document
{
    public class CreateDocumentCommand : BaseCommandRequest<ApiResult>
    {
        public CreateDocumentCommand()
        {
            DocumentPeople = new List<DocumentPersonViewModel>();
            DocumentCases = new List<DocumentCaseViewModel>();
            DocumentRelatedPeople = new List<DocumentRelatedPersonViewModel>();
            DocumentRelations = new List<DocumentRelationViewModel>();
            DocumentCosts = new List<DocumentCostViewModel>();
            DocumentEstates = new List<DocumentEstateViewModel>();
            DocumentVehicles = new List<DocumentVehicleViewModel>();
            DocumentInfoOther = new DocumentInfoOtherViewModel();
            DocumentInfoJudgment = new DocumentInfoJudgmentViewModel();
            DocumentInfoText = new DocumentInfoTextViewModel();
            DocumentCostQuestion = new DocumentCostQuestionViewModel();
            DocumentVehicles = new List<DocumentVehicleViewModel>();
            DocumentMainRelation = new DocumentRelationViewModel ();
            DocumentPayments = new List<DocumentPaymentViewModel>();

        }
        public List<string> validateCurrencyTypeId(string documentTypeCode, string documentTypeTitle)
        {
            List<string> DocumentTypeCodesValid = new List<string>()
            {
                "421",
                "422",
                "423",
                "424",
                "425",
            };
            List<string> messages = new List<string>();
            bool isRequired = false;
            if (!(RequestCurrencyTypeId.Count > 0) && !string.IsNullOrEmpty(documentTypeCode) && (IsNew || IsDirty))
            {


                if (!DocumentTypeCodesValid.Contains(documentTypeCode))
                {
                    messages.Add($"در سند {documentTypeTitle} لوکاپ واحد پولی اجباری است.  ");
                    return messages;
                }
            }

            return null;
        }
        public bool IsValid { get; set; }
        public bool IsNew { get; set; }
        public bool IsDelete { get; set; }
        public bool IsDirty { get; set; }
        public string RequestId { get; set; }
        public string RequestScriptoriumId { get; set; }
        public string RequestNo { get; set; }
        public string RequestDate { get; set; }
        public string RequestTime { get; set; }
        public string DocumentDate { get; set; }
        public string GetDocumentNoDate { get; set; }
        public string RequestSignDate { get; set; }
        public bool? CheckRepeatedRequest { get; set; }
        public string RequestSignTime { get; set; }
        public string RequestWriteInBookDate { get; set; }
        public string DocumentSecretCode { get; set; }
        public string RequestNationalNo { get; set; }
        public string RequestClassifyNo { get; set; }
        public string RequestBookVolumeNo { get; set; }
        public string RequestBookPapersNo { get; set; }
        public bool? IsRequestBasedJudgment { get; set; }
        public bool? IsRequestCostPaymentConfirmed { get; set; }
        public bool? IsRequestCostCalculateConfirmed { get; set; }
        public bool? IsRequestFinalVerificationVisited { get; set; }
        public bool? IsRequestRahProcessed { get; set; }
        public bool? IsRequestSentToTaxOrganization { get; set; }
        public string RequestState { get; set; }
        public string RequestRecordDate { get; set; }
        public string RequestLegacyId { get; set; }
        public string DocumentInfoTextId { get; set; }
        public string DocumentInfoConfirmId { get; set; }
        public string DocumentInfoJudgmentId { get; set; }
        public string DocumentInfoOtherId { get; set; }
        public string DocumentInfoPaymentId { get; set; }
        public string DocumentCopyId { get; set; }
        public string DocumentTemplateId { get; set; }

        //مبلغ سند
        public string RequestPrice { get; set; }
        //ماخذ حق الثبت
        public string RequestSabtPrice { get; set; }
        //نحوه پرداخت 
        public string RequestHowToPay { get; set; }
        //واحد پولی
        public string RequestRegionPrice { get; set; }


        public string RelatedtId { get; set; }
        public string RelatedRequestNo { get; set; }
        public string RelatedRequestDate { get; set; }
        public string RelatedRequestSecretCode { get; set; }
        public string RelatedDocumentScriptorium { get; set; }
        public IList<string> RelatedScriptoriumId { get; set; }
        public bool? IsRequestAbroad { get; set; }
        public bool? IsRequestInSsar { get; set; }
        public IList<string> RelatedDocumentTypeId { get; set; }
        public IList<string> RelatedDocAbroadCountryId { get; set; }

        public bool? IsCopyDocumentInfoText { get; set; }
        public bool? IsCopyDocumentCases { get; set; }
        public bool? IsCopyDocumentPeople { get; set; }
        public bool? IsCreateRegisterReqPrices { get; set; }
        public IList<string> RequestCurrencyTypeId { get; set; }
        public IList<string> DocumentAssetTypeId { get; set; }
        public IList<string> DocumentTypeId { get; set; }
        public IList<string> DocumentVehicleId { get; set; }
        public IList<string> DocumentTypeSubjectId { get; set; }
        public IList<DocumentCaseViewModel> DocumentCases { get; set; }
        public IList<DocumentPersonViewModel> DocumentPeople { get; set; }
        public IList<DocumentRelatedPersonViewModel> DocumentRelatedPeople { get; set; }
        public IList<DocumentRelationViewModel> DocumentRelations { get; set; }
        public IList<DocumentCostViewModel> DocumentCosts { get; set; }
        public IList<DocumentPaymentViewModel> DocumentPayments { get; set; }
        public IList<DocumentEstateViewModel> DocumentEstates { get; set; }
        public IList<DocumentVehicleViewModel> DocumentVehicles { get; set; }
        public DocumentInfoTextViewModel DocumentInfoText { get; set; }
        public DocumentInfoJudgmentViewModel DocumentInfoJudgment { get; set; }
        public DocumentCostQuestionViewModel DocumentCostQuestion { get; set; }
        public DocumentInfoOtherViewModel DocumentInfoOther { get; set; }
        public DocumentRelationViewModel DocumentMainRelation { get; set; }



    }
}
