using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentPayments;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.DealSummary;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using EntityState = Notary.SSAA.BO.DataTransferObject.Bases.EntityState;
namespace Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract
{
    public class DocumentStandardContractViewModel : EntityState
    {
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

        public string RequestId { get; set; }
        public string RequestScriptoriumId { get; set; }
        public string RequestNo { get; set; }
        public string RequestDate { get; set; }
        public string RequestTime { get; set; }
        public string DocumentDate { get; set; }
        public string GetDocumentNoDate { get; set; }
        public string RequestSignDate { get; set; }
        public string RequestSignTime { get; set; }
        public string RequestWriteInBookDate { get; set; }
        public string DocumentTypeTitle { get; set; }
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
        public string DocumentState { get; set; }
        public NotaryRegServiceReqState StateId { get; set; }
        public string RequestRecordDate { get; set; }
        public string RequestLegacyId { get; set; }
        public string DocumentInfoTextId { get; set; }
        public string DocumentAssetTypeId { get; set; }
        public string DocumentInfoConfirmId { get; set; }
        public string DocumentInfoJudgmentId { get; set; }
        public string DocumentInfoOtherId { get; set; }
        public string DocumentInfoPaymentId { get; set; }
        public string DocumentCopyId { get; set; }
        //مبلغ سند
        public string RequestPrice { get; set; }
        //ماخذ حق الثبت
        public string RequestSabtPrice { get; set; }
        //نحوه پرداخت 
        public string RequestHowToPay { get; set; }
        //واحد پولی
        public string RequestRegionPrice { get; set; }

        public string DocumentTemplateId { get; set; }
        public int? DocumentCostPaperCount { get; set; }
        public bool? IsCopyDocumentInfoText { get; set; }
        public bool? IsCopyDocumentCases { get; set; }
        public bool? IsCopyDocumentPeople { get; set; }
        public bool? IsCreateRegisterReqPrices { get; set; }
        public string RemoteRequestId { get; set; }
        public bool? IsRemoteRequest { get; set; }

        public IList<string> RequestCurrencyTypeId { get; set; }
        public IList<string> DocumentTypeGroupOneId { get; set; }
        public IList<string> DocumentTypeGroupTwoId { get; set; }
        public IList<string> DocumentTypeId { get; set; }

        //public IList<string> DocumentVehicleId { get; set; }
        public IList<string> DocumentSubjectTypeId { get; set; }
        public IList<DocumentStandardContractPersonViewModel> DocumentPeople { get; set; }
        public IList<DocumentStandardContractCaseViewModel> DocumentCases { get; set; }
        public IList<DocumentStandardContractRelatedPersonViewModel> DocumentRelatedPeople { get; set; }
        public IList<DocumentStandardContractRelationViewModel> DocumentRelations { get; set; }
        public DocumentStandardContractRelationViewModel DocumentMainRelation { get; set; }
        public IList<DocumentStandardContractCostViewModel> DocumentCosts { get; set; }
        public IList<DocumentStandardContractPaymentViewModel> DocumentPayments { get; set; }
        public IList<DocumentStandardContractVehicleViewModel> DocumentVehicles { get; set; }
        public IList<DocumentStandardContractEstateViewModel> DocumentEstates { get; set; }
        public IList<DocumentStandardContractSmsViewModel> DocumentSms { get; set; }
        public DocumentStandardContractInfoTextViewModel DocumentInfoText { get; set; }
        public DocumentStandardContractInfoConfirmViewModel DocumentInfoConfirm { get; set; }
        public DocumentStandardContractInfoOtherViewModel DocumentInfoOther { get; set; }
        public DocumentStandardContractInfoJudgmentViewModel DocumentInfoJudgment { get; set; }
        public DocumentStandardContractCostQuestionViewModel DocumentCostQuestion { get; set; }
        public IList<DocumentStandardContractInquiryViewModel> DocumentInquiries { get; set; }
        public DocumentStandardContractTypeViewModel RequestType { get; set; }
        public bool IsRegistered { get; set; }
        public bool IsDSUPermitted { get; set; }
        public bool IsDSUForced { get; set; }
        public bool IsDocRestrictedType { get; set; }
        public bool IsENoteBookBaseInfoInitialized { get; set; }
        public bool IsClassifyNoEditable { get; set; }
        public string RelatedDocDigitalBookClassifyNo { get; set; }
        public bool GetPersonPhotoEnabled { get; set; }
        public bool SMSServiceEnabled { get; set; }
        public bool UserDefinedMessages { get; set; }
        public bool IsENoteBookEnabled { get; set; }
        public bool ViewRelatedDocumentImageEnabled { get; set; }
        public bool IsMechanizedTaxEnabled { get; set; }
        public string IsDSUDealSummaryCreationEnabled { get; set; }
        public bool IsMechanizedMunicipalitySettlementEnabled { get; set; }
        public bool showFinalVerificationWindow { get; set; }
        public bool IsFingerprintEnabled { get; set; }
        public bool SeparationVerify { get; set; }
        public string ENoteBookEnabledDate { get; set; }
        public bool AutomatedRemoveRestrictionEnabled { get; set; }
        public long RelatedDocClassifyNo { get; set; }
        public List<string> ForbiddenCostTypeCodes { get; set; }
        public List<string> ForbiddenSabtCostDocumentTypeCodes { get; set; }
        public List<string> ForbiddenTahrirCostDocumentTypeCodes { get; set; }







    }
}
