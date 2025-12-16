using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.Commands.DocumentStandardContract
{
    public class CreateDocumentStandardContractCommand : BaseCommandRequest<ApiResult>
    {
        public CreateDocumentStandardContractCommand()

        {
            DocumentPeople = new List<DocumentStandardContractPersonViewModel>();
            DocumentCases = new List<DocumentStandardContractCaseViewModel>();
            DocumentRelatedPeople = new List<DocumentStandardContractRelatedPersonViewModel>();
            DocumentRelations = new List<DocumentStandardContractRelationViewModel>();
            DocumentCosts = new List<DocumentStandardContractCostViewModel>();
            DocumentEstates = new List<DocumentStandardContractEstateViewModel>();
            DocumentVehicles = new List<DocumentStandardContractVehicleViewModel>();
            DocumentInfoOther = new DocumentStandardContractInfoOtherViewModel();
            DocumentInfoJudgment = new DocumentStandardContractInfoJudgmentViewModel();
            DocumentInfoText = new DocumentStandardContractInfoTextViewModel();
            DocumentCostQuestion = new DocumentStandardContractCostQuestionViewModel();
            DocumentVehicles = new List<DocumentStandardContractVehicleViewModel>();
            DocumentMainRelation = new DocumentStandardContractRelationViewModel ();
            DocumentPayments = new List<DocumentStandardContractPaymentViewModel>();

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
        public IList<DocumentStandardContractCaseViewModel> DocumentCases { get; set; }
        public IList<DocumentStandardContractPersonViewModel> DocumentPeople { get; set; }
        public IList<DocumentStandardContractRelatedPersonViewModel> DocumentRelatedPeople { get; set; }
        public IList<DocumentStandardContractRelationViewModel> DocumentRelations { get; set; }
        public IList<DocumentStandardContractCostViewModel> DocumentCosts { get; set; }
        public IList<DocumentStandardContractPaymentViewModel> DocumentPayments { get; set; }
        public IList<DocumentStandardContractEstateViewModel> DocumentEstates { get; set; }
        public IList<DocumentStandardContractVehicleViewModel> DocumentVehicles { get; set; }
        public DocumentStandardContractInfoTextViewModel DocumentInfoText { get; set; }
        public DocumentStandardContractInfoJudgmentViewModel DocumentInfoJudgment { get; set; }
        public DocumentStandardContractCostQuestionViewModel DocumentCostQuestion { get; set; }
        public DocumentStandardContractInfoOtherViewModel DocumentInfoOther { get; set; }
        public DocumentStandardContractRelationViewModel DocumentMainRelation { get; set; }



    }
}
