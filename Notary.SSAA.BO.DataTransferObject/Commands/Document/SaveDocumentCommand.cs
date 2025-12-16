namespace Notary.SSAA.BO.DataTransferObject.Commands.Document
{
    using Notary.SSAA.BO.DataTransferObject.Bases;
    using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
    using Notary.SSAA.BO.SharedKernel.Enumerations;
    using Notary.SSAA.BO.SharedKernel.Result;


    /// <summary>
    /// Defines the <see cref="SaveDocumentCommand" />
    /// </summary>
    public class SaveDocumentCommand : BaseCommandRequest<ApiResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SaveDocumentCommand"/> class.
        /// </summary>
        public SaveDocumentCommand()
        {
            DocumentPeople = new List<DocumentPersonViewModel>();
            DocumentCases = new List<DocumentCaseViewModel>();
            DocumentRelatedPeople = new List<DocumentRelatedPersonViewModel>();
            DocumentRelations = new List<DocumentRelationViewModel>();
            DocumentMainRelation = new DocumentRelationViewModel();
            DocumentCosts = new List<DocumentCostViewModel>();
            DocumentEstates = new List<DocumentEstateViewModel>();
            DocumentVehicles = new List<DocumentVehicleViewModel>();
            DocumentInfoOther = new DocumentInfoOtherViewModel();
            DocumentInfoJudgment = new DocumentInfoJudgmentViewModel();
            DocumentInfoText = new DocumentInfoTextViewModel();
            DocumentCostQuestion = new DocumentCostQuestionViewModel();
            DocumentVehicles = new List<DocumentVehicleViewModel>();
            DocumentPayments = new List<DocumentPaymentViewModel>();
            DocumentSms = new List<DocumentSmsViewModel>();
        }

        /// <summary>
        /// The validateCurrencyTypeId
        /// </summary>
        /// <param name="documentTypeCode">The documentTypeCode<see cref="string"/></param>
        /// <param name="documentTypeTitle">The documentTypeTitle<see cref="string"/></param>
        /// <returns>The <see cref="List{string}"/></returns>
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

        /// <summary>
        /// Gets or sets a value indicating whether IsValid
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsNew
        /// </summary>
        public bool IsNew { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsDelete
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsDirty
        /// </summary>
        public bool IsDirty { get; set; }

        /// <summary>
        /// Gets or sets the RequestId
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// Gets or sets the RequestScriptoriumId
        /// </summary>
        public string RequestScriptoriumId { get; set; }

        /// <summary>
        /// Gets or sets the RequestNo
        /// </summary>
        public string RequestNo { get; set; }

        /// <summary>
        /// Gets or sets the RequestDate
        /// </summary>
        public string RequestDate { get; set; }

        /// <summary>
        /// Gets or sets the RequestTime
        /// </summary>
        public string RequestTime { get; set; }

        /// <summary>
        /// Gets or sets the DocumentDate
        /// </summary>
        public string DocumentDate { get; set; }

        /// <summary>
        /// Gets or sets the GetDocumentNoDate
        /// </summary>
        public string GetDocumentNoDate { get; set; }

        /// <summary>
        /// Gets or sets the RequestSignDate
        /// </summary>
        public string RequestSignDate { get; set; }

        /// <summary>
        /// Gets or sets the RequestSignTime
        /// </summary>
        public string RequestSignTime { get; set; }

        /// <summary>
        /// Gets or sets the RequestWriteInBookDate
        /// </summary>
        public string RequestWriteInBookDate { get; set; }

        /// <summary>
        /// Gets or sets the DocumentSecretCode
        /// </summary>
        public string DocumentSecretCode { get; set; }

        /// <summary>
        /// Gets or sets the RequestNationalNo
        /// </summary>
        public string RequestNationalNo { get; set; }

        /// <summary>
        /// Gets or sets the RequestClassifyNo
        /// </summary>
        public string RequestClassifyNo { get; set; }

        /// <summary>
        /// Gets or sets the RequestBookVolumeNo
        /// </summary>
        public string RequestBookVolumeNo { get; set; }

        /// <summary>
        /// Gets or sets the RequestBookPapersNo
        /// </summary>
        public string RequestBookPapersNo { get; set; }

        /// <summary>
        /// Gets or sets the IsRequestBasedJudgment
        /// </summary>
        public bool? IsRequestBasedJudgment { get; set; }

        /// <summary>
        /// Gets or sets the IsRequestCostPaymentConfirmed
        /// </summary>
        public bool? IsRequestCostPaymentConfirmed { get; set; }

        /// <summary>
        /// Gets or sets the IsRequestCostCalculateConfirmed
        /// </summary>
        public bool? IsRequestCostCalculateConfirmed { get; set; }

        /// <summary>
        /// Gets or sets the IsRequestFinalVerificationVisited
        /// </summary>
        public bool? IsRequestFinalVerificationVisited { get; set; }

        /// <summary>
        /// Gets or sets the IsRequestRahProcessed
        /// </summary>
        public bool? IsRequestRahProcessed { get; set; }

        /// <summary>
        /// Gets or sets the IsRequestSentToTaxOrganization
        /// </summary>
        public bool? IsRequestSentToTaxOrganization { get; set; }

        /// <summary>
        /// Gets or sets the DocumentState
        /// </summary>
        public string DocumentState { get; set; }

        /// <summary>
        /// Gets or sets the StateId
        /// </summary>
        public NotaryRegServiceReqState StateId { get; set; }

        /// <summary>
        /// Gets or sets the RequestRecordDate
        /// </summary>
        public string RequestRecordDate { get; set; }

        /// <summary>
        /// Gets or sets the RequestLegacyId
        /// </summary>
        public string RequestLegacyId { get; set; }

        /// <summary>
        /// Gets or sets the DocumentInfoTextId
        /// </summary>
        public string DocumentInfoTextId { get; set; }

        /// <summary>
        /// Gets or sets the DocumentAssetTypeId
        /// </summary>
        public List<string> DocumentAssetTypeId { get; set; }

        /// <summary>
        /// Gets or sets the DocumentInfoConfirmId
        /// </summary>
        public string DocumentInfoConfirmId { get; set; }

        /// <summary>
        /// Gets or sets the DocumentInfoJudgmentId
        /// </summary>
        public string DocumentInfoJudgmentId { get; set; }

        /// <summary>
        /// Gets or sets the DocumentInfoOtherId
        /// </summary>
        public string DocumentInfoOtherId { get; set; }

        /// <summary>
        /// Gets or sets the DocumentInfoPaymentId
        /// </summary>
        public string DocumentInfoPaymentId { get; set; }

        /// <summary>
        /// Gets or sets the DocumentCopyId
        /// </summary>
        public string DocumentCopyId { get; set; }

        /// <summary>
        /// Gets or sets the DocumentTemplateId
        /// </summary>
        public string DocumentTemplateId { get; set; }

        //مبلغ سند

        /// <summary>
        /// Gets or sets the RequestPrice
        /// </summary>
        public string RequestPrice { get; set; }

        //ماخذ حق الثبت

        /// <summary>
        /// Gets or sets the RequestSabtPrice
        /// </summary>
        public string RequestSabtPrice { get; set; }

        //نحوه پرداخت 

        /// <summary>
        /// Gets or sets the RequestHowToPay
        /// </summary>
        public string RequestHowToPay { get; set; }

        //ارزش معاملاتی

        /// <summary>
        /// Gets or sets the RequestRegionPrice
        /// </summary>
        public string RequestRegionPrice { get; set; }

        /// <summary>
        /// Gets or sets the IsCopyDocumentInfoText
        /// </summary>
        public bool? IsCopyDocumentInfoText { get; set; }

        /// <summary>
        /// Gets or sets the CheckRepeatedRequest
        /// </summary>
        public bool? CheckRepeatedRequest { get; set; }

        /// <summary>
        /// Gets or sets the IsCopyDocumentCases
        /// </summary>
        public bool? IsCopyDocumentCases { get; set; }

        /// <summary>
        /// Gets or sets the IsCopyDocumentPeople
        /// </summary>
        public bool? IsCopyDocumentPeople { get; set; }

        /// <summary>
        /// Gets or sets the DocumentTypeSubjectId
        /// </summary>
        public IList<string> DocumentTypeSubjectId { get; set; }

        /// <summary>
        /// Gets or sets the DetailName
        /// </summary>
        public SharedKernel.Enumerations.DocumentDatailName DetailName { get; set; }

        /// <summary>
        /// Gets or sets the RequestCurrencyTypeId
        /// </summary>
        public IList<string> RequestCurrencyTypeId { get; set; }

        /// <summary>
        /// Gets or sets the DocumentTypeId
        /// </summary>
        public IList<string> DocumentTypeId { get; set; }

        /// <summary>
        /// Gets or sets the DocumentVehicleId
        /// </summary>
        public IList<string> DocumentVehicleId { get; set; }

        /// <summary>
        /// Gets or sets the InquiriesId
        /// </summary>
        public IList<string> InquiriesId { get; set; }

        /// <summary>
        /// Gets or sets the DocumentCases
        /// </summary>
        public IList<DocumentCaseViewModel> DocumentCases { get; set; }

        /// <summary>
        /// Gets or sets the DocumentPeople
        /// </summary>
        public IList<DocumentPersonViewModel> DocumentPeople { get; set; }

        /// <summary>
        /// Gets or sets the DocumentRelatedPeople
        /// </summary>
        public IList<DocumentRelatedPersonViewModel> DocumentRelatedPeople { get; set; }

        /// <summary>
        /// Gets or sets the DocumentRelations
        /// </summary>
        public IList<DocumentRelationViewModel> DocumentRelations { get; set; }

        /// <summary>
        /// Gets or sets the DocumentEstates
        /// </summary>
        public IList<DocumentEstateViewModel> DocumentEstates { get; set; }

        /// <summary>
        /// Gets or sets the DocumentVehicles
        /// </summary>
        public IList<DocumentVehicleViewModel> DocumentVehicles { get; set; }

        /// <summary>
        /// Gets or sets the DocumentCosts
        /// </summary>
        public IList<DocumentCostViewModel> DocumentCosts { get; set; }

        /// <summary>
        /// Gets or sets the DocumentPayments
        /// </summary>
        public IList<DocumentPaymentViewModel> DocumentPayments { get; set; }
        /// <summary>
        /// Gets or sets the Documentsms
        /// </summary>
        public IList<DocumentSmsViewModel> DocumentSms { get; set; }

        /// <summary>
        /// Gets or sets the DocumentInfoText
        /// </summary>
        public DocumentInfoTextViewModel DocumentInfoText { get; set; }

        /// <summary>
        /// Gets or sets the DocumentInfoOther
        /// </summary>
        public DocumentInfoOtherViewModel DocumentInfoOther { get; set; }

        /// <summary>
        /// Gets or sets the DocumentInfoJudgment
        /// </summary>
        public DocumentInfoJudgmentViewModel DocumentInfoJudgment { get; set; }

        /// <summary>
        /// Gets or sets the DocumentCostQuestion
        /// </summary>
        public DocumentCostQuestionViewModel DocumentCostQuestion { get; set; }

        /// <summary>
        /// Gets or sets the DocumentInquiries
        /// </summary>
        public IList<DocumentInquiryViewModel> DocumentInquiries { get; set; }

        /// <summary>
        /// Gets or sets the DocumentMainRelation
        /// </summary>
        public DocumentRelationViewModel DocumentMainRelation { get; set; }

        /// <summary>
        /// Gets or sets the IsRegistered
        /// </summary>
        public bool? IsRegistered { get; set; }

        /// <summary>
        /// Gets or sets the IsRemoteRequest
        /// </summary>
        public bool? IsRemoteRequest { get; set; }
        /// <summary>
        /// Gets or sets the RemoteRequestId
        /// </summary>
        public string RemoteRequestId { get; set; }

        /// <summary>
        /// Gets or sets the IsAttachment
        /// </summary>
        public bool? IsAttachment { get; set; }

        /// <summary>
        /// Gets or sets the UnSignedPersonsListIds
        /// </summary>
        public List<string> UnSignedPersonsListIds { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether PrepareDocumentConfirmation
        /// </summary>
        public bool PrepareDocumentConfirmation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether SardaftarConfirm
        /// </summary>
        public bool SardaftarConfirm { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether PrepareDaftaryarConfirm
        /// </summary>
        public bool PrepareDaftaryarConfirm { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether DaftaryarConfirm
        /// </summary>
        public bool DaftaryarConfirm { get; set; }

        /// <summary>
        /// Gets or sets the DigitalBookSignatureCollection
        /// </summary>
        public List<string> DigitalBookSignatureCollection { get; set; }

        /// <summary>
        /// Gets or sets the SignedDSUDealSummaryCollection
        /// </summary>
        public List<string> SignedDSUDealSummaryCollection { get; set; }

        /// <summary>
        /// Gets or sets the SignedDocumentId
        /// </summary>
        public string SignedDocumentId { get; set; }

        /// <summary>
        /// The IsRequestInFinalState
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        public bool IsRequestInFinalState()
        {

            if (StateId == NotaryRegServiceReqState.Finalized ||
                 StateId == NotaryRegServiceReqState.CanceledAfterGetCode ||
                 StateId == NotaryRegServiceReqState.CanceledBeforeGetCode

                )
            {
                return true;

            }
            return false;
        }

        /// <summary>
        /// The IsRequestInSetNationalDocumentNo
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        public bool IsRequestInSetNationalDocumentNo()
        {
            if (StateId == NotaryRegServiceReqState.SetNationalDocumentNo)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// The IsRequestInFinalized
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        public bool IsRequestInFinalized()
        {
            if (StateId == NotaryRegServiceReqState.Finalized)
            {
                return true;
            }
            return false;
        }
    }
}
