using Notary.SSAA.BO.SharedKernel.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Configuration;
using Notary.SSAA.BO.Domain.RichDomain.Document;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Commons;
using Notary.SSAA.BO.Utilities;
using System.Reflection.Metadata;
using Notary.SSAA.BO.SharedKernel.Contracts.Coordinator.Document;
using Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Estate.Shell;
using Document = Notary.SSAA.BO.Domain.Entities.Document;
using Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Estate.Core;
using Azure.Core;
using Notary.SSAA.BO.DataTransferObject.Coordinators.NotaryDocument;
using System.Collections;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;

namespace Notary.SSAA.BO.Coordinator.NotaryDocument.Core.FinalVerificationManager
{
    public class RegisterServiceRequestVerifier
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IPersonFingerprintRepository _personFingerprintRepository;
        private readonly ClientConfiguration _clientConfiguration;
        private readonly IDocumentPersonRepository _documentPersonRepository;
        private readonly IDocumentPersonRelatedRepository _documentPersonRelatedRepository;
        private readonly SmartQuotaGeneratorEngine _smartQuotaGeneratorEngine;
        private readonly QuotasValidator _quotasValidator;
        private readonly ValidatorGateway _validatorGateway;
        private EstateInquiryEngine _estateInquiryEngine;


        public RegisterServiceRequestVerifier ( IDocumentRepository documentRepository, IPersonFingerprintRepository personFingerprintRepository
        ,IDocumentPersonRepository documentPersonRepository, IDocumentPersonRelatedRepository documentPersonRelatedRepository
        , ClientConfiguration clientConfiguration, SmartQuotaGeneratorEngine smartQuotaGeneratorEngine
        ,QuotasValidator quotasValidator,ValidatorGateway validatorGateway, EstateInquiryEngine estateInquiryEngine )
        {
            _documentRepository = documentRepository;
            _clientConfiguration= clientConfiguration;
            _personFingerprintRepository = personFingerprintRepository;
            _documentPersonRepository = documentPersonRepository;
            _documentPersonRelatedRepository= documentPersonRelatedRepository;
            _smartQuotaGeneratorEngine = smartQuotaGeneratorEngine;
            _quotasValidator = quotasValidator;
            _validatorGateway = validatorGateway;
            _estateInquiryEngine = estateInquiryEngine;
        }

        #region VerifyCurrentRegisterServiceRequirments
        public async Task<bool> VerifyCurrentRegisterServiceRequirments ( string registerServiceReqObjectID,CancellationToken cancellationToken,  string verificationMessage,  string serverChallange,  bool mainSaveActionFlag,  string agnetsValidationMessages,  string relatedDocValidationMesages)
        {

             string serverChallangeRef=serverChallange;
             bool mainSaveActionFlagRef=mainSaveActionFlag;
             string agnetsValidationMessagesRef=agnetsValidationMessages;
             string relatedDocValidationMesagesRef = relatedDocValidationMesages;
             string verificationMessageRef = verificationMessage;
            bool needsSaveAction = false;
            mainSaveActionFlagRef = false;
            Document theCurrentNotaryRegisterServiceReqEntity = await this.GetRegisterServiceReqEntity(Guid.Parse(registerServiceReqObjectID),new List<string> { "DocumentCosts","DocumentType", "DocumentInfoConfirm", "DocumentPeople" },cancellationToken  );
            if ( theCurrentNotaryRegisterServiceReqEntity == null )
            {
                verificationMessage = "خطا در بازیابی سند مربوطه از سرور مرکزی.\nلطفاً مجدداً تلاش نمایید.";
                return false;
            }

            #region DocPersonsDataEntryValidation

            bool personsDataEntryAreValid;
             ( personsDataEntryAreValid,verificationMessageRef) =await  ValidateDocPersonsCollection( theCurrentNotaryRegisterServiceReqEntity.DocumentPeople?.ToList(),theCurrentNotaryRegisterServiceReqEntity,  verificationMessageRef);
            if ( !personsDataEntryAreValid )
                return false;
            #endregion

            _clientConfiguration.initializeScriptorium(theCurrentNotaryRegisterServiceReqEntity.ScriptoriumId);
            #region IsDSUGeneratingPermitted
            string dsuMessages = null;
            if ( !DsuUtility.IsDSUGeneratingPermitted ( ref dsuMessages, theCurrentNotaryRegisterServiceReqEntity,null,false ,_clientConfiguration ) )
            {
                if ( !string.IsNullOrWhiteSpace ( dsuMessages ) )
                {
                    verificationMessage = dsuMessages;
                    return false;
                }
            }
            #endregion

            #region SmartQuotaGenerator
            if ( IntelligantDocumentInheritor.IsAutoQuotaGeneratingPermitted ( theCurrentNotaryRegisterServiceReqEntity,_clientConfiguration, ref verificationMessage ) )
            {
                bool quotasGenerated;
                ( quotasGenerated,theCurrentNotaryRegisterServiceReqEntity,verificationMessageRef) =await _smartQuotaGeneratorEngine.GenerateQuotas( theCurrentNotaryRegisterServiceReqEntity,cancellationToken,  verificationMessageRef, true);
                if ( !quotasGenerated )
                {
                    if ( string.IsNullOrWhiteSpace ( verificationMessage ) )
                        verificationMessage = "خطا در انجام محاسبات حسب السهم. لطفاً مجدداً تلاش نمایید.";

                    return false;
                }
                else
                {
                    mainSaveActionFlagRef = true;//quotasGenerated | mainSaveActionFlag;
                }
            }
            #endregion

            #region QuotaValidation
            //theCurrentNotaryRegisterServiceReqEntity = this.GetRegisterServiceReqEntity(registerServiceReqObjectID);
            string inquiryAndQuotasMessages = string.Empty;
            bool result = _quotasValidator.VerifyRegCasesInquiriesAndQuotas(theCurrentNotaryRegisterServiceReqEntity, ref inquiryAndQuotasMessages);
            if ( !result )
            {
                if ( !string.IsNullOrEmpty ( verificationMessage ) )
                    verificationMessage += System.Environment.NewLine;

                verificationMessage += inquiryAndQuotasMessages;

                return false;
            }
            #endregion

            #region Centralized Validation Of Agents And Related-Doc
            bool isCentralizedValidatorEnabled = this.IsCentralizedValidatorEnabled();
            if ( isCentralizedValidatorEnabled )
            {
                #region RELATED DOC VALIDATION
                //==============================================================================================================
                //======================================RELATED DOC VALIDATION==================================================
                if ( theCurrentNotaryRegisterServiceReqEntity.DocumentType.HasRelatedDocument == YesNo.Yes.GetString() && theCurrentNotaryRegisterServiceReqEntity.IsRelatedDocAbroad != YesNo.Yes.GetString() && theCurrentNotaryRegisterServiceReqEntity.RelatedDocumentIsInSsar == YesNo.Yes.GetString() )
                {
                    if ( string.IsNullOrWhiteSpace ( theCurrentNotaryRegisterServiceReqEntity.RelatedDocumentNo ) )
                    {
                        verificationMessage = "شناسه سند وابسته در بخش سایر اطلاعات، مشخص نشده است.";
                        return false;
                    }

                    if ( string.IsNullOrWhiteSpace ( theCurrentNotaryRegisterServiceReqEntity.RelatedDocumentDate ) )
                    {
                        verificationMessage = "تاریخ سند وابسته در بخش سایر اطلاعات، مشخص نشده است.";
                        return false;
                    }

                    if ( string.Compare ( theCurrentNotaryRegisterServiceReqEntity.RelatedDocumentDate, "1392/06/26" ) < 0 )
                    {
                        verificationMessage = "تاریخ وارد شده برای سند وابسته در بخش سایر اطلاعات، قبل از راه اندازی سامانه ثبت الکترونیک اسناد می باشد.";
                        return false;
                    }

                    if ( theCurrentNotaryRegisterServiceReqEntity.RelatedScriptoriumId == null )
                    {
                        verificationMessage = "دفترخانه سند وابسته در بخش سایر اطلاعات، مشخص نشده است.";
                        return false;
                    }

                    RelatedDocumentValidationRequest relatedDocumentValidationRequest = new RelatedDocumentValidationRequest();

                    relatedDocumentValidationRequest.DocumentNationalNo = theCurrentNotaryRegisterServiceReqEntity.RelatedDocumentNo;
                    relatedDocumentValidationRequest.DocumentScriptoriumId = theCurrentNotaryRegisterServiceReqEntity.RelatedScriptoriumId;
                    relatedDocumentValidationRequest.DocumentDate = theCurrentNotaryRegisterServiceReqEntity.RelatedDocumentDate;
                    relatedDocumentValidationRequest.DocumentTypeID = theCurrentNotaryRegisterServiceReqEntity.DocumentTypeId;
                    relatedDocumentValidationRequest.IsRelatedDocumentInSSAR = theCurrentNotaryRegisterServiceReqEntity.RelatedDocumentIsInSsar == YesNo.Yes.GetString() ? YesNo.Yes : YesNo.No;


                    string relatedDocValidationOveralMessageText = string.Empty;
                    RelatedDocumentValidationResponse relatedDocValidationResult;
                    ( relatedDocValidationResult, relatedDocValidationOveralMessageText) =await _validatorGateway.ValidateRelatedDocument(relatedDocumentValidationRequest,  relatedDocValidationOveralMessageText);

                    relatedDocValidationMesages = relatedDocValidationOveralMessageText;
                    verificationMessage += relatedDocValidationMesages;

                    if ( !relatedDocValidationResult.ValidationResult )
                    {
                        needsSaveAction = true;
                        mainSaveActionFlag = true;
                        theCurrentNotaryRegisterServiceReqEntity.RelatedDocumentId = null;
                        verificationMessage += System.Environment.NewLine + relatedDocValidationResult.ValidationResponseMessage;
                        return false;
                    }
                    else
                    {
                        if ( theCurrentNotaryRegisterServiceReqEntity.RelatedDocumentId?.ToString() != relatedDocValidationResult.registerServiceReqObjectID )
                        {
                            theCurrentNotaryRegisterServiceReqEntity.RelatedDocumentId =Guid.Parse( relatedDocValidationResult.registerServiceReqObjectID ) ;

                            //SMS Not Implemented Yet

                            needsSaveAction = true;
                            mainSaveActionFlag = true;
                        }
                    }
                }
                #endregion

                #region DOC AGENT VALIDATION
                //==============================================================================================================
                //======================================DOC AGENT VALIDATION====================================================
                List<DocAgentValidationResponsPacket> docAgentValidationResponsPacketCollection = null;
                List<DocumentCase> currentRegCaseCollection = null;
                List<DocumentVehicle> currentVehicleCollection = null;
                List<DocumentEstate> currentEstateCollection = null;

                if ( theCurrentNotaryRegisterServiceReqEntity.HasCases())
                {

                    if ( theCurrentNotaryRegisterServiceReqEntity.DocumentCases.Any())
                    {
                        currentRegCaseCollection = new List<DocumentCase> ();
                        foreach ( DocumentCase theOneExistingCase in theCurrentNotaryRegisterServiceReqEntity.DocumentCases )
                            currentRegCaseCollection.Add ( theOneExistingCase );
                    }

                }
                if ( theCurrentNotaryRegisterServiceReqEntity.HasEstates() )
                {

                    if ( theCurrentNotaryRegisterServiceReqEntity.DocumentEstates.Any () )
                    {
                        currentEstateCollection = new List<DocumentEstate> ();
                        foreach ( DocumentEstate theOneExistingCase in theCurrentNotaryRegisterServiceReqEntity.DocumentEstates )
                            currentEstateCollection.Add ( theOneExistingCase );
                    }
                }
                if ( theCurrentNotaryRegisterServiceReqEntity.HasVehicles() )
                {

                    if ( theCurrentNotaryRegisterServiceReqEntity.DocumentVehicles.Any () )
                    {
                        currentVehicleCollection = new List<DocumentVehicle> ();
                        foreach ( DocumentVehicle theOneExistingCase in theCurrentNotaryRegisterServiceReqEntity.DocumentVehicles )
                            currentVehicleCollection.Add ( theOneExistingCase );
                    }
                }
              

                string overalAgentValidationMessage = string.Empty;
                ( docAgentValidationResponsPacketCollection,overalAgentValidationMessage, needsSaveAction) = await this.ValidateAgentsDocumentsOnBeforeNationalNo ( theCurrentNotaryRegisterServiceReqEntity.DocumentPeople,  overalAgentValidationMessage, 
                    theCurrentNotaryRegisterServiceReqEntity.DocumentTypeId, currentRegCaseCollection,currentVehicleCollection,currentEstateCollection,  needsSaveAction );
                mainSaveActionFlag = needsSaveAction | mainSaveActionFlag;
                if ( docAgentValidationResponsPacketCollection.Any () )
                {
                    bool agentValidationOveralResult = false;
                    agentValidationOveralResult = this.PerformAndGetOveralAgentValidationReponses ( docAgentValidationResponsPacketCollection, theCurrentNotaryRegisterServiceReqEntity.DocumentPeople, ref overalAgentValidationMessage );
                    verificationMessage += overalAgentValidationMessage;

                    if ( !agentValidationOveralResult )
                    {
                        return false;
                    }
                    else
                    {
                        needsSaveAction = true;
                        mainSaveActionFlag = true;
                    }
                }
                #endregion
            }
            #endregion

            #region ClassifyNo Verification
            //=====================================ClassifyNo Verification==================================================
            string classifyNoValidationMessage = string.Empty;
            bool verificationResult;
             (verificationResult,classifyNoValidationMessage) = await this.ValidateCurrentClassifyNo(theCurrentNotaryRegisterServiceReqEntity,cancellationToken,  classifyNoValidationMessage);
            verificationMessage += "\n" + classifyNoValidationMessage;

            if ( !verificationResult )
                return false;
            #endregion

            #region ClassifyNoSequenceCheck
            string classifyNoSequenceCheckMessage = null;
            bool sequenceIsOK = this.ValidateClassifyNoSequence(theCurrentNotaryRegisterServiceReqEntity, ref classifyNoSequenceCheckMessage);
            if ( !sequenceIsOK )
            {
                verificationMessage += "\n" + classifyNoSequenceCheckMessage;
            }
            #endregion

            //#region Server Challange
            ////======================================Server Challange========================================================
            //CertificatesAuthorization.AuthorizationCore authorizationCore = new CertificatesAuthorization.AuthorizationCore();
            //serverChallange = authorizationCore.GetServerChallenge ();
            //#endregion

            #region DSUSimulation
            // Naghavi-1400/02/21 : در هنگام اخذ اثر انگشت لازم نیست خلاصه معامله های سند چک شوند
            if ( theCurrentNotaryRegisterServiceReqEntity.State != NotaryRegServiceReqState.FinalPrinted.GetString() && DsuUtility.IsDSUGeneratingPermitted ( ref dsuMessages, theCurrentNotaryRegisterServiceReqEntity,null,false, _clientConfiguration ) )
            {
                string dsuSimulationMessages = string.Empty;
                bool isSuccessfull;
                 (isSuccessfull,dsuSimulationMessages) = await _estateInquiryEngine.SimulateDSUsSendingProcess(theCurrentNotaryRegisterServiceReqEntity,  dsuSimulationMessages,cancellationToken);
                verificationMessage += dsuSimulationMessages;
                if ( !isSuccessfull )
                {
                    if ( _clientConfiguration.RejectNaionalNoOnDSUFail && string.Compare ( theCurrentNotaryRegisterServiceReqEntity.RequestDate, _clientConfiguration.DSUInitializationDate ) >= 0 )
                        return false;
                    else
                        return true;
                }
            }
            else
            {
                if ( !string.IsNullOrWhiteSpace ( dsuMessages ) )
                {
                    verificationMessage += dsuMessages;
                    return false;
                }
            }
            #endregion

            return true;
        }
        #endregion
        private async Task<Document> GetRegisterServiceReqEntity ( Guid objectID, List<string> details, CancellationToken cancellationToken )
        {
            if ( objectID == null )
                return null;

            Document oNotaryRegisterServiceReq =
                await _documentRepository.GetDocumentById(objectID, details, cancellationToken);

            return oNotaryRegisterServiceReq;
        }

        public async Task<(bool, string)> IsFingerprintGottenOfAllPersons ( ICollection<DocumentPerson> personsCollection, string message, CancellationToken cancellationToken, List<DocumentPerson> unsignedPersons = null )
        {
            string messageRef = message;
            string docuemntId = null;
            if ( !personsCollection.Any () )
            {
                messageRef = "اشخاص سند مشخص تعریف نشده اند.";
                return (false, messageRef);
            }

            //Rad.NotaryOffice.Service.IONotaryRegisterServiceReq theCurrentReq = ((IONotaryDocPerson)personsCollection[0]).TheONotaryRegisterServiceReq;

            List<DocumentPerson> fingerPrintNeededPersons = null;
            string docDate = null;

            foreach ( DocumentPerson theOneDocPerson in personsCollection )
            {
                if ( _clientConfiguration._currentOrg == null )
                    _clientConfiguration.initializeScriptorium ( theOneDocPerson.ScriptoriumId );//new ConfigurationManager.TypeDefinitions.ClientConfiguration ( theOneDocPerson.TheONotaryRegisterServiceReq.TheScriptorium.TheCMSOrganization );
                docuemntId = theOneDocPerson.DocumentId.ToString ();
                bool personHasSignedDocument = true;
                if ( unsignedPersons.Any () )
                    foreach ( DocumentPerson theOneUnsignedPerson in unsignedPersons )
                    {
                        if (
                            theOneDocPerson.Id == theOneUnsignedPerson.Id ||
                            ( theOneDocPerson.NationalNo == theOneUnsignedPerson.NationalNo && theOneDocPerson.FullName ().GetStandardFarsiString () == theOneUnsignedPerson.FullName ().GetStandardFarsiString () )
                            )
                        {
                            personHasSignedDocument = false;
                            break;
                        }
                    }

                if ( !personHasSignedDocument )
                    continue;

                #region CollectNeededDocPersonsFingerPrint
                if ( _clientConfiguration.IsFingerprintEnabled )
                {
                    bool isForced = this.IsFingerPrintForced(theOneDocPerson);
                    if ( !isForced )
                        continue;

                    if ( fingerPrintNeededPersons == null )
                        fingerPrintNeededPersons = new List<DocumentPerson> ();


                    if ( !fingerPrintNeededPersons.Any () || !fingerPrintNeededPersons.Contains ( theOneDocPerson ) )
                        fingerPrintNeededPersons.Add ( theOneDocPerson );
                }

                #endregion

                if ( string.IsNullOrWhiteSpace ( docDate ) )
                    docDate = theOneDocPerson.Document.DocumentDate; //تا قبل از تاریخ 23 دی 94 ، بجای این تاریخ (شناسه یکتا) از تاریخ تشکیل پرونده استفاده می شد.
            }

            if ( !_clientConfiguration.IsFingerprintEnabled )
                return (true, messageRef);
            if (
                _clientConfiguration.IsFingerprintEnabled &&
                string.Compare ( docDate, _clientConfiguration.FingerprintEnabledDate ) >= 0 &&
                fingerPrintNeededPersons.Any ()
                )
            {
                List<string> forcedFingerprintPersonIDs = new List<string>();
                foreach ( DocumentPerson theOneDocPerson in fingerPrintNeededPersons )
                    forcedFingerprintPersonIDs.Add ( theOneDocPerson.Id.ToString () );
                var theFingerprintsCollection= await _personFingerprintRepository.GetAllAsync(p => p.UseCaseMainObjectId == docuemntId  && personsCollection.Select(p=>p.Id.ToString()).ToList().Contains(p.UseCasePersonObjectId),
                    cancellationToken);

                if ( !theFingerprintsCollection.Any () )
                {
                    messageRef = "اثرانگشت اشخاص در سند یافت نشد.";
                    return (false, messageRef);
                }


                messageRef = "";
                foreach ( DocumentPerson theOneDocPersonToCheck in fingerPrintNeededPersons )
                {
                    bool fingerPrintEntityExists = false;
                    bool fingerprintFeaturesExists = false;
                    bool fingerprintDescriptionExists = false;

                    if ( _clientConfiguration.ISMOCEnabledForScriptorium && _clientConfiguration.IsForcedMOC )
                    {
                        if ( theOneDocPersonToCheck.MocState == MocState.None.GetString () ||
                        theOneDocPersonToCheck.MocState == MocState.NotDone.GetString () ||
                        theOneDocPersonToCheck.MocState == MocState.FingerprintNotMatched.GetString () ||
                        theOneDocPersonToCheck.MocState == MocState.PinNotMatched.GetString () )
                        {
                            if ( theOneDocPersonToCheck.HasSmartCard == YesNo.Yes.GetString () )
                            {
                                messageRef = "اثر انگشت " + theOneDocPersonToCheck.FullName () + " با اثر انگشت مندرج در کارت هوشمند ملی وی تطابق ندارد.";
                                return (false, messageRef);
                            }
                        }
                    }

                    foreach ( PersonFingerprint theOneFingerPrintEntity in theFingerprintsCollection )
                    {
                        DocumentPerson theEquivalantFingerPrintPerson =await  _documentPersonRepository.GetAsync(p=>p.Id== theOneFingerPrintEntity.Id ,cancellationToken) ;

                        if (
                            theOneDocPersonToCheck.Id == Guid.Parse ( theOneFingerPrintEntity.UseCasePersonObjectId ) ||
                            theEquivalantFingerPrintPerson.NationalNo == theOneDocPersonToCheck.NationalNo
                            )
                        {
                            fingerPrintEntityExists = true;

                            if ( !Equals ( theOneFingerPrintEntity.FingerprintFeatures, null ) )
                                fingerprintFeaturesExists = true;

                            if ( !string.IsNullOrEmpty ( theOneFingerPrintEntity.Description ) )
                                fingerprintDescriptionExists = true;

                            break;
                        }
                    }
                    string digitalBookValidatorMessage = null;

                    #region DigitalBookEnabled
                    if ( DigitalBookUtility.IsDigitalBookGeneratingPermitted ( theOneDocPersonToCheck.Document, _clientConfiguration.ENoteBookEnabledDate, _clientConfiguration.IsENoteBookAutoClassifyNoEnabled, ref digitalBookValidatorMessage ) == DigitalBookGeneratingPermissionStatus.Needed )
                    {
                        if ( fingerprintFeaturesExists )
                            continue;

                        bool isPersonDefinedAsReliable = this.IsReliablePerson(theOneDocPersonToCheck);

                        if ( isPersonDefinedAsReliable )
                        {
                            messageRef +=
                                System.Environment.NewLine +
                                " - برای " + theOneDocPersonToCheck.FullName () + " با شماره ملی " + theOneDocPersonToCheck.NationalNo + " و شماره ردیف " + theOneDocPersonToCheck.RowNo +
                                "اثرانگشت ثبت نشده است. درج اثرانگشت برای فرد معتمد اجباری است.";
                        }
                        else
                        {
                            var theAgentsCollection =await this.GetReliablePersonsCollection(theOneDocPersonToCheck,cancellationToken);
                            if ( theAgentsCollection.Any () )
                                continue;
                            else
                            {
                                messageRef +=
                                System.Environment.NewLine +
                                " - برای " + theOneDocPersonToCheck.FullName() + " با شماره ملی " + theOneDocPersonToCheck.NationalNo + " و شماره ردیف " + theOneDocPersonToCheck.RowNo +
                                " اثرانگشت ثبت نشده است. در صورت عدم امکان اخذ اثرانگشت بایستی مطابق تبصره 2 ماده 4 شیوه نامه بهره برداری از دفتر الکترونیک اقدام شود.";
                            }
                        }
                    }
                    #endregion

                    #region Non-DigitalBook
                    else
                    {
                        if ( fingerprintFeaturesExists )
                            continue;


                        if ( !fingerPrintEntityExists )
                            messageRef += " - برای " + theOneDocPersonToCheck.FullName () + " با شماره ملی " + theOneDocPersonToCheck.NationalNo + " و شماره ردیف " + theOneDocPersonToCheck.RowNo + " هیچ اثرانگشت و یا توضیحی در مورد علت عدم اخذ اثرانگشت ثبت نشده است. " + System.Environment.NewLine;
                        else
                        {
                            if ( fingerprintDescriptionExists )
                                continue;
                            else
                            {
                                messageRef +=
                                System.Environment.NewLine +
                                " - برای " + theOneDocPersonToCheck.FullName () + " با شماره ملی " + theOneDocPersonToCheck.NationalNo + " و شماره ردیف " + theOneDocPersonToCheck.RowNo +
                                " هیچ توضیحی در مورد علت عدم اخذ اثرانگشت ثبت نشده است.";
                            }
                        }
                    }
                    #endregion
                }

                if ( !string.IsNullOrWhiteSpace ( messageRef ) )
                {
                    messageRef =
                        "لطفاً به موارد زیر در خصوص اثرانگشت ها توجه فرمایید : " +
                        System.Environment.NewLine +
                        System.Environment.NewLine +
                        messageRef;

                    return (false, messageRef);
                }

            }

            return (true, messageRef);
        }

        private bool IsFingerPrintForced ( DocumentPerson theSelectedDocPerson )
        {
            //return false;

            if ( theSelectedDocPerson.Document.State != NotaryRegServiceReqState.FinalPrinted.GetString () )
                return false;

            if ( theSelectedDocPerson.PersonType == PersonType.Legal.GetString () )
                return false;

            //if (theSelectedDocPerson.TheONotaryRegServicePersonType == null)
            //    return true;

            FingerprintAquisitionPermission permission =FingerprintAquisitionManager.IsFingerprintAquisitionPermitted(theSelectedDocPerson.Document, theSelectedDocPerson);

            switch ( permission )
            {
                case FingerprintAquisitionPermission.Forbidden:
                    return false;
                case FingerprintAquisitionPermission.Mandatory:
                    return true;
                case FingerprintAquisitionPermission.Optional:

                    if ( theSelectedDocPerson.WouldSignedDocument == YesNo.No.GetString () )
                        return false;
                    else
                        return true;
            }


            return true;
        }
        private bool IsReliablePerson ( DocumentPerson theCurrentPerson )
        {
            if ( !theCurrentPerson.DocumentPersonRelatedAgentPeople.Any () || theCurrentPerson.IsRelated != YesNo.Yes.GetString () )
                return false;

            foreach ( DocumentPersonRelated theOneDocAgent in theCurrentPerson.DocumentPersonRelatedAgentPeople )
            {
                if ( theOneDocAgent.AgentTypeId == "10" )
                    return true;
            }

            return false;
        }
        private async Task<List<DocumentPersonRelated>> GetReliablePersonsCollection ( DocumentPerson theOneDocPersonToCheck, CancellationToken cancellationToken )
        {

            var theAgentsCollection=  await _documentPersonRelatedRepository.GetAllAsync(
                t => t.MainPersonId == theOneDocPersonToCheck.Id && t.AgentTypeId == "10" && t.ReliablePersonReasonId ==
                    NotaryNeedToReliableReason.WithoutFingerprint.GetString(), cancellationToken);



            return theAgentsCollection;
        }
        public async Task<(bool, string)> ValidateDocPersonsCollection ( List<DocumentPerson>? personsCollection,Document document, string message )
        {
            string messageRef = message;
            if (personsCollection ==null || personsCollection.Count == 0)
            {
                messageRef = "اشخاص سند مشخص تعریف نشده اند.";
                return (false, messageRef);
            }

            string nonCellNumberPersons = string.Empty;
            //List<IONotaryDocPerson> fingerPrintNeededPersons = null;
            await _clientConfiguration.initializeScriptorium ( document.ScriptoriumId );

            foreach ( DocumentPerson theOneDocPerson in personsCollection )
            {



                #region MobileNo
                if (
                    theOneDocPerson.PersonType == PersonType.NaturalPerson.GetString () &&
                    theOneDocPerson.IsIranian == YesNo.Yes.GetString () &&
                    theOneDocPerson.IsAlive == YesNo.Yes.GetString ()
                    )
                {
                    if (
                        string.IsNullOrWhiteSpace ( theOneDocPerson.MobileNo ) ||
                        !ValidatorsUtility.CheckCellPhoneFormat ( theOneDocPerson.MobileNo )
                        )
                    {
                        if ( !string.IsNullOrWhiteSpace ( nonCellNumberPersons ) && nonCellNumberPersons.GetStandardFarsiString ().Contains ( theOneDocPerson.FullName ().GetStandardFarsiString () ) )
                            continue;

                        nonCellNumberPersons += " - " + theOneDocPerson.FullName() + System.Environment.NewLine;
                        continue;
                    }
                }
                #endregion

                #region PostalCode
                if ( !string.IsNullOrEmpty ( theOneDocPerson.PostalCode ) && !ValidatorsUtility.checkPostalCode ( theOneDocPerson.PostalCode ) )
                {
                    messageRef = "کد پستی وارد شده برای  " + theOneDocPerson.FullName() + " معتبر نیست. ";
                    return (false, messageRef);
                }

                #endregion

                #region AgentsFields
                if ( theOneDocPerson.DocumentPersonRelatedAgentPeople.Any () )
                {
                    foreach ( DocumentPersonRelated theOneDocAgent in theOneDocPerson.DocumentPersonRelatedAgentPeople )
                    {
                        if ( theOneDocAgent.AgentTypeId == "3" )
                            continue;

                        if ( string.IsNullOrWhiteSpace ( theOneDocAgent.AgentDocumentNo ) || string.IsNullOrWhiteSpace ( theOneDocAgent.AgentDocumentDate ) )
                        {
                            messageRef =
                                "لطفاً اشخاص وابسته مربوط به " + theOneDocPerson.FullName () + " را بررسی نموده و از پر بودن فیلدهای اجباری اطمینان حاصل نمایید.";
                            return (false, messageRef);
                        }
                    }
                }
                #endregion

                #region LegalPersonAgentRequirmentValidation
                if ( theOneDocPerson.PersonType == PersonType.Legal.GetString () )
                {
                    bool isLegalPersonAgentsValid;
                    ( isLegalPersonAgentsValid ) = this.IsLegalPersonAgentValid ( theOneDocPerson, ref messageRef );
                    if ( !isLegalPersonAgentsValid )
                        return (false, messageRef);
                }
                #endregion

                #region IsRelationsGraphSingleSide
                List<string> personAgentsIDsCollection = null;
                string relationsGraph = null;
                bool isAgentsRelationsGraphSingleSide = PersonsLogicValidator.IsRelationsGraphSingleSide(theOneDocPerson, ref personAgentsIDsCollection, ref relationsGraph, ref messageRef);
                if ( !isAgentsRelationsGraphSingleSide )
                    return (false, messageRef);
                #endregion
            }

            if ( !string.IsNullOrWhiteSpace ( nonCellNumberPersons ) )
            {
                messageRef = "شماره تلفن همراه اشخاص زیر وارد نشده و یا فاقد اعتبار است:";

                messageRef =
                    messageRef +
                    System.Environment.NewLine +
                    nonCellNumberPersons;

                return (false, messageRef);
            }

            return (true, messageRef);
        }
        private bool IsLegalPersonAgentValid ( DocumentPerson theOneLegalDocPerson, ref string message )
        {
            //در اینجا برای استفاده از این تابع، پارامتر سوم را که تعیین کننده شروع گراف است، به گونه ای ست میکنیم که اشخاص حقوقی هم در داخال تابع مورد بررسی قرار گیرند.
            if ( FingerprintAquisitionManager.IsFingerprintAquisitionPermitted ( theOneLegalDocPerson.Document, theOneLegalDocPerson, false ) != FingerprintAquisitionPermission.Mandatory )
                return true;

            if (
                theOneLegalDocPerson.PersonType == PersonType.Legal.GetString () &&
                theOneLegalDocPerson.IsOriginal == YesNo.Yes.GetString ()
                )
            {

                bool hasValidAgent = false;

                foreach ( DocumentPerson theOnePersonToCheck4Agent in theOneLegalDocPerson.Document.DocumentPeople )
                {
                    if ( !theOnePersonToCheck4Agent.DocumentPersonRelatedAgentPeople.Any () )
                        continue;

                    foreach ( var theOneAgent in theOnePersonToCheck4Agent.DocumentPersonRelatedAgentPeople )
                    {
                        if (
                            theOneAgent.MainPerson.Id == theOneLegalDocPerson.Id &&
                            theOneAgent.AgentPerson.PersonType == PersonType.NaturalPerson.GetString ()
                            )
                            return true;

                        if (
                            theOneAgent.MainPerson.Id == theOneLegalDocPerson.Id &&
                            theOneAgent.AgentPerson.PersonType == PersonType.Legal.GetString ()
                            )
                            return this.IsLegalPersonAgentValid ( theOneAgent.AgentPerson, ref message );
                    }
                }

                //if ( !hasValidAgent ) همیشه true میشد
                //{
                    message =
                        "برای " +
                        theOneLegalDocPerson.FullName() +
                        "، هیچ شخص وابسته حقیقی معرفی نشده است. لطفاً حداقل یک شخص وابسته حقیقی برای این شخص مشخص نمایید.";
                //}

                return hasValidAgent;
            }

            return true;
        }

        private bool IsCentralizedValidatorEnabled ( )
        {
            bool isEnabled = true;

            isEnabled = Settings.CentralizedValidatorsMainGateway;//System.Configuration.ConfigurationSettings.AppSettings["CentralizedValidatorsMainGateway"] as string;

            return isEnabled;
        }

        #region ValidateAgentsDocuments
        private async Task <(List<DocAgentValidationResponsPacket>,string,bool)> ValidateAgentsDocumentsOnBeforeNationalNo ( ICollection<DocumentPerson> theDocPersonsList,
             string overalValidationMessageText, string documentTypeID,
             System.Collections.Generic.List<DocumentCase> CurrentRegCasesCollection,
             System.Collections.Generic.List<DocumentVehicle> CurrentRegVehiclesCollection,
             System.Collections.Generic.List<DocumentEstate> CurrentRegEstateCollection,  bool needsSaveAction )
        {
            string overalValidationMessageTextRef = overalValidationMessageText;
            bool needsSaveActionRef = needsSaveAction;
            needsSaveAction = false;

            if ( !theDocPersonsList.Any () )
                return (null, overalValidationMessageTextRef, needsSaveActionRef);

            List<DocAgentValidationRequestPacket> docAgentValidationRequestsCollection = this.CollectDocAgentValidationRequests(theDocPersonsList);
            if (docAgentValidationRequestsCollection == null || !docAgentValidationRequestsCollection.Any () )
                return (null, overalValidationMessageTextRef, needsSaveActionRef);

            string documentTypeTitle = (theDocPersonsList.ElementAt(0) ).Document.DocumentType.Title;
            string mainEntityID = (theDocPersonsList.ElementAt(0) ).DocumentId.ToString();

            List<DocAgentValidationResponsPacket> docAgentValidationResponsPacketCollection = new List<DocAgentValidationResponsPacket>();

            (docAgentValidationResponsPacketCollection,overalValidationMessageTextRef) =await _validatorGateway.ValidateAgentDocumentsCollection ( docAgentValidationRequestsCollection,  overalValidationMessageTextRef, mainEntityID, documentTypeTitle );

            foreach ( DocAgentValidationResponsPacket theOneResponse in docAgentValidationResponsPacketCollection )
            {
                if ( string.IsNullOrWhiteSpace ( theOneResponse.CorrespondingRegisterServiceReqObjectID ) )
                {
                    needsSaveActionRef = true;
                    break;
                }
            }

            return (docAgentValidationResponsPacketCollection,overalValidationMessageTextRef,needsSaveActionRef);
        }
        #endregion
        #region CollectDocAgentValidationRequests
        private List<DocAgentValidationRequestPacket> CollectDocAgentValidationRequests ( ICollection<DocumentPerson> theDocPersonsList )
        {
            List<DocAgentValidationRequestPacket> docAgentValidationInputPacketCollection = new List<DocAgentValidationRequestPacket>();


            if (theDocPersonsList==null || !theDocPersonsList.Any ()  )
                return null;

            foreach ( DocumentPerson theOnePerson in theDocPersonsList )
            {
                foreach ( DocumentPersonRelated theOneDocAgent in theOnePerson.DocumentPersonRelatedAgentPeople )
                {
                    //Only If the AgentType Is "وکالتنامه"
                    if ( theOneDocAgent.AgentTypeId== "1" )
                        continue;

                    if ( theOneDocAgent.IsAgentDocumentAbroad == YesNo.Yes.GetString() || theOneDocAgent.IsRelatedDocumentInSsar == YesNo.No.GetString() 
                        || theOneDocAgent.IsRelatedDocumentInSsar == YesNo.None.GetString() )
                        continue;

                    //If The Validation Is Done Before, Don't Validate Again.
                    //This "theOneDocAgent.ONotaryRegisterServiceReqId" is filled only if the validation is Succeeded Or The Current Agent Object change
                    //============***Do The Check***Because the object is on server and it might be changed**************
                    //if (!string.IsNullOrWhiteSpace(theOneDocAgent.ONotaryRegisterServiceReqId))
                    //    continue;

                    DocAgentValidationRequestPacket docAgentValidationInputPacket = this.ImplementValidationPacket(theOneDocAgent);
                    if ( docAgentValidationInputPacket != null )
                        docAgentValidationInputPacketCollection.Add ( docAgentValidationInputPacket );
                }
            }

            return docAgentValidationInputPacketCollection;
        }
        #endregion
        #region PerformAndGetOveralAgentValidationReponses
        private bool PerformAndGetOveralAgentValidationReponses ( List<DocAgentValidationResponsPacket> docAgentValidationResponsPacketCollection, ICollection<DocumentPerson> oNotaryDocPersonList, ref string responseMessage )
        {
            bool validationOveralResult = true;

            foreach ( DocAgentValidationResponsPacket theOneValidationResponse in docAgentValidationResponsPacketCollection )
            {
                validationOveralResult = validationOveralResult & theOneValidationResponse.Response;

                DocumentPersonRelated theOneDocAgent = this.FindDocAgentByObjectID(oNotaryDocPersonList, theOneValidationResponse.CurrentDocAgentObjectID);
                if ( theOneDocAgent != null )
                {
                    if ( !theOneValidationResponse.Response )
                    {
                        theOneDocAgent.Id = Guid.Empty;
                    }
                    else
                    {
                        theOneDocAgent.Id =Guid.Parse( theOneValidationResponse.CorrespondingRegisterServiceReqObjectID ); 
                    }
                }

                responseMessage += System.Environment.NewLine + theOneValidationResponse.ResponseMessage;
            }

            return validationOveralResult;
        }
        #endregion

        #region FindDocAgentByObjectID
        private DocumentPersonRelated FindDocAgentByObjectID ( ICollection<DocumentPerson> theOnotaryDocPersonsList, string objectID )
        {
            if ( theOnotaryDocPersonsList == null || theOnotaryDocPersonsList.Count == 0 )
                return null;
            foreach ( DocumentPerson theOneDocPerson in theOnotaryDocPersonsList )
            {
                foreach ( DocumentPersonRelated theOneAgent in theOneDocPerson.DocumentPersonRelatedAgentPeople )
                {
                    if ( theOneAgent.Id .ToString()== objectID )
                        return theOneAgent;
                }
            }

            return null;
        }
        #endregion
        #region ImplementValidationPacket
        private DocAgentValidationRequestPacket ImplementValidationPacket ( DocumentPersonRelated theOneDocAgent )
        {
            if ( theOneDocAgent == null )
                return null;

            DocAgentValidationRequestPacket docAgentValidationInputPacket = new DocAgentValidationRequestPacket();

            docAgentValidationInputPacket.CurrentDocAgentObjectID = theOneDocAgent?.Id.ToString();
            docAgentValidationInputPacket.DocumentDate = theOneDocAgent.AgentDocumentDate;
            docAgentValidationInputPacket.DocumentNationalNo = theOneDocAgent.AgentDocumentNo;
            docAgentValidationInputPacket.DocumentScriptoriumId = theOneDocAgent.DocumentScriptoriumId;
            docAgentValidationInputPacket.DocumentSecretCode = theOneDocAgent.AgentDocumentSecretCode;
            docAgentValidationInputPacket.MovakelFullName = theOneDocAgent.MainPerson.FullName();
            docAgentValidationInputPacket.MovakelNationalNo = theOneDocAgent.MainPerson.NationalNo;
            docAgentValidationInputPacket.VakilFullName = theOneDocAgent.AgentPerson.FullName();
            docAgentValidationInputPacket.VakilNationalNo = theOneDocAgent.AgentPerson.NationalNo;

            if ( theOneDocAgent.DocumentSmsId ==null)
                docAgentValidationInputPacket.SMSIsRequired = true;
            else
                docAgentValidationInputPacket.SMSIsRequired = false;

            return docAgentValidationInputPacket;
        }
        #endregion

        #region ValidateCurrentClassifyNo
        public async Task<ONotaryRegisterServiceReqOutputMessage> ValidateCurrentClassifyNo ( ONotaryRegisterServiceReqInputMessage input,CancellationToken cancellationToken )
        {
            ONotaryRegisterServiceReqOutputMessage output = new ONotaryRegisterServiceReqOutputMessage();
            output.CurrentActionResult = true;
            output.Message = true;
            output.MessageText = string.Empty;
            string messageText = "";

            if ( input == null || input.EntityID == null )
            {
                messageText = "ارتباط با سامانه مرکزی برقرار نشد. لطفاً مجدداً تلاش نمایید.";
                ONotaryRegisterServiceReqOutputMessage outputError = new ONotaryRegisterServiceReqOutputMessage() { Message = false, MessageText = messageText };
                return outputError;
            }

            Document theCurrentNotaryRegisterServiceReqEntity =await this.GetRegisterServiceReqEntity(Guid.Parse(input.EntityID),new List<string>(){},cancellationToken);
            if ( theCurrentNotaryRegisterServiceReqEntity == null )
            {
                messageText = "خطا در اخذ اطلاعات پرونده از سرور مرکزی. لطفاً مجدداً تلاش نمایید.";
                ONotaryRegisterServiceReqOutputMessage outputError = new ONotaryRegisterServiceReqOutputMessage() { Message = false, MessageText = messageText };

                return outputError;
            }

            string verificationMessage = string.Empty;
            bool verificationResult;
            ( verificationResult,verificationMessage) = await this.ValidateCurrentClassifyNo(theCurrentNotaryRegisterServiceReqEntity,cancellationToken,  verificationMessage);
            output.Message = verificationResult;
            output.CurrentActionResult = verificationResult;
            output.MessageText = verificationMessage;


            #region OldVersion
            bool rejectIterativeClassifyNo = true;
            string rejectIterativeClassifyNoString = System.Configuration.ConfigurationSettings.AppSettings["RejectIterativeClassifyNo"];
            Boolean.TryParse ( rejectIterativeClassifyNoString, out rejectIterativeClassifyNo );

            if ( theCurrentNotaryRegisterServiceReqEntity.DocumentType.IsSupportive == YesNo.No.GetString() && rejectIterativeClassifyNo )
            {
                try
                {
                    if ( theCurrentNotaryRegisterServiceReqEntity.ClassifyNo != null )
                    {
                        int clasifyNo = int.Parse(theCurrentNotaryRegisterServiceReqEntity.ClassifyNo.ToString());
                       bool validateCurrentClassifyNo= await  _documentRepository. ValidateCurrentClassifyNo( clasifyNo , theCurrentNotaryRegisterServiceReqEntity.ScriptoriumId,cancellationToken );


                            if ( validateCurrentClassifyNo )
                            {
                                output.CurrentActionResult = true;
                                output.Message = output.CurrentActionResult;
                            }
                            else
                            {
                                
                                    output.CurrentActionResult = false;
                                    output.Message = output.CurrentActionResult;
                                    output.MessageText = "شماره ترتیب سند وارد شده تکراری می باشد. ";
                                
                              
                            }
                        
                    }
                    else
                    {
                        output.CurrentActionResult = false;
                        output.Message = output.CurrentActionResult;
                        output.MessageText = "شمار ه ترتیب سند وارد نشده است.";
                    }
                }
                catch ( Exception ex )
                {
                    messageText = "خطا در اجرای سرویس.\n" + ex.ToString ();
                    var outputError = new ONotaryRegisterServiceReqOutputMessage() { Message = false, MessageText = messageText };
                    return outputError;
                }
            }
            #endregion

            return output;
        }

        private async Task<(bool,string)> ValidateCurrentClassifyNo ( Document theCurrentNotaryRegisterServiceReqEntity,CancellationToken cancellationToken,  string verificationMessage )
        {
            string verificationMessageRef = verificationMessage;
            bool isPermitted = false;

            bool rejectIterativeClassifyNo = true;
            bool rejectIterativeClassifyNoConf = Settings.RejectIterativeClassifyNo; //System.Configuration.ConfigurationSettings.AppSettings["RejectIterativeClassifyNo"] as string;

            if ( theCurrentNotaryRegisterServiceReqEntity.DocumentType.IsSupportive == YesNo.No.GetString() && rejectIterativeClassifyNo )
            {
                if ( theCurrentNotaryRegisterServiceReqEntity.ClassifyNo != null )
                {
                    if ( theCurrentNotaryRegisterServiceReqEntity.ScriptoriumId != null )
                    {
                        int classifyNo = int.Parse(theCurrentNotaryRegisterServiceReqEntity.ClassifyNo.ToString());
                       
                        if ( await _documentRepository.ValidateCurrentClassifyNo( classifyNo,
                                theCurrentNotaryRegisterServiceReqEntity.ScriptoriumId,cancellationToken )   )
                        {
                            isPermitted = true;
                        }
                        else
                        {
                            
                                isPermitted = false;
                                verificationMessageRef = "شماره ترتیب سند وارد شده تکراری می باشد. ";
                          
                        }
                    }
                }
                else
                {
                    isPermitted = false;
                    verificationMessageRef = "شمار ه ترتیب سند وارد نشده است.";
                }
            }
            else
            {
                isPermitted = true;
            }

            return (isPermitted, verificationMessageRef);
        }
        #endregion
        #region ValidateClassifyNoSequence
        private bool ValidateClassifyNoSequence ( Document theCurrentReq, ref string message )
        {
            return true;

            if ( theCurrentReq.ClassifyNo == null )
                return true;

            //string currentClassifyNo = theCurrentReq.ClassifyNo.ToString();

            //string maxClassifyNoQuery =
            //        @"select onotaryregisterservicereq.id, onotaryregisterservicereq.nationalno, onotaryregisterservicereq.confirmdate, onotaryregisterservicereq.classifyno, onotarydocumenttype.title 
            //        from onotaryregisterservicereq, onotarydocumenttype, scriptorium
            //        where onotaryregisterservicereq.scriptoriumid = scriptorium.id
            //        and onotaryregisterservicereq.onotarydocumenttypeid = onotarydocumenttype.id
            //        and onotarydocumenttype.is4registerservice = '{0}'
            //        and scriptorium.code = '{1}'
            //        and onotaryregisterservicereq.confirmdate is not null
            //        and onotaryregisterservicereq.confirmdate||onotaryregisterservicereq.id =
            //        (
            //        select max(req.confirmdate||req.id)
            //        from scriptorium scr, onotaryregisterservicereq req, onotarydocumenttype t
            //        where 
            //        scr.code = '{1}'
            //        and req.scriptoriumid = scr.id
            //        and t.id = req.onotarydocumenttypeid
            //        and t.is4registerservice = '{0}'
            //        and req.confirmdate is not null
            //        )";

            //maxClassifyNoQuery = string.Format ( maxClassifyNoQuery, theCurrentReq.TheONotaryDocumentType.Is4RegisterService.ToInt32 (), theCurrentReq.TheScriptorium.Code );

            //System.Data.DataTable theLastClassifyNoDT = Rad.CMS.InstanceBuilder.ExecuteQuery(maxClassifyNoQuery);

            //if ( theLastClassifyNoDT == null || theLastClassifyNoDT.Rows == null || !theLastClassifyNoDT.Rows.CollectionHasElement () )
            //    return true;

            //decimal theLastClassifyNo = (decimal)theLastClassifyNoDT.Rows[0]["classifyno"];
            //decimal? theCurrentClassifyNo = theCurrentReq.ClassifyNo;

            //if ( theLastClassifyNo + 1 != theCurrentReq.ClassifyNo )

            //        "هشدار در خصوص عدم رعایت تسلسل شماره ترتیب سند: " +
            //        System.Environment.NewLine +
            //        System.Environment.NewLine +
            //        "نظم موجود در شماره ترتیب اسناد رعایت نشده است. لطفاً در صورت لزوم شماره ترتیب وارد شده را اصلاح نموده و مجدداً جهت اخذ شناسه یکتا اقدام نمایید." +
            //        System.Environment.NewLine;

            //    return false;
            //}

            return true;
        }
        #endregion
    }



}

