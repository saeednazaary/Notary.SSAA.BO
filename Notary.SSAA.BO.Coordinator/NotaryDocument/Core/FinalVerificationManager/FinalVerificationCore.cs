using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notary.SSAA.BO.Configuration;
using Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Commons;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.BaseInfoService;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices;
using Notary.SSAA.BO.Domain.RichDomain.Document;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using System.Threading;
using Notary.SSAA.BO.DataTransferObject.Coordinators.NotaryDocument;

namespace Notary.SSAA.BO.Coordinator.NotaryDocument.Core.FinalVerificationManager
{
    internal class FinalVerificationCore
    {
        ClientConfiguration _clientConfiguration = null;
        private PersonInquiryManager _personInquiryManager = null;
        private IDateTimeService _dateTimeService = null;

        internal FinalVerificationCore(ClientConfiguration clientConfiguration, PersonInquiryManager personInquiryManager, IDateTimeService dateTimeService)
        {
            _clientConfiguration = clientConfiguration;
            _personInquiryManager = personInquiryManager;
            _dateTimeService = dateTimeService;
        }

        #region PersonsInquiry
        #region AddFirstZeros
        private string AddFirstZeros(string CroppedNationalCode)
        {
            if (string.IsNullOrEmpty(CroppedNationalCode))
                return CroppedNationalCode;

            if (CroppedNationalCode.Length == 10)
                return CroppedNationalCode;

            while (CroppedNationalCode.Length < 10)
            {
                CroppedNationalCode = "0" + CroppedNationalCode;
            }

            return CroppedNationalCode;
        }
        #endregion

        #region SelectPersonFromList
        private List<DocumentPerson> SelectPersonsFromList(List<DocumentPerson> personsListInput, SabtAhvalServiceViewModel theOneEsPerson)
        {
            List<DocumentPerson> selectPersonsFromList = new List<DocumentPerson>();

            foreach (DocumentPerson theOnePerson in personsListInput)
            {
                if (theOnePerson.NationalNo == this.AddFirstZeros(theOneEsPerson.NationalNo))
                    selectPersonsFromList.Add(theOnePerson);
            }

            return selectPersonsFromList;
        }
        #endregion

        #region ConfirmPersonsUsingSabtAhval
        public async Task<(List<DocumentPerson>, string)> ConfirmPersonsCollection(List<DocumentPerson> personsListInput, CancellationToken cancellationToken, string sabtAhvalErrorMessage, bool immediateCommit = true, object extraParam = null)
        {
            string sabtAhvalErrorMessageRef = sabtAhvalErrorMessage;
            //using ( new TransactedOperation ( immediateCommit ) )
            {
                try
                {
                    List<InputPerson> inputPersons = null;
                    List<DocumentPerson> theValidatingPersonsCollection = null;
                    List<DocumentPerson> theNonValidatingPersonsCollection = null;


                    foreach (DocumentPerson theOneDocPerson in personsListInput)
                    {
                        if (inputPersons == null)
                            inputPersons = new List<InputPerson>();

                        if (
                            theOneDocPerson.PersonType == PersonType.NaturalPerson.GetString() &&
                            theOneDocPerson.IsIranian == YesNo.Yes.GetString() &&
                            (!string.IsNullOrWhiteSpace(theOneDocPerson.NationalNo)) && (!string.IsNullOrWhiteSpace(theOneDocPerson.BirthDate) || (theOneDocPerson.BirthYear != null))
                            )
                        {
                            InputPerson theCustomPerson = new InputPerson(theOneDocPerson);
                            inputPersons.Add(theCustomPerson);

                            if (theValidatingPersonsCollection == null)
                                theValidatingPersonsCollection = new List<DocumentPerson>();

                            theValidatingPersonsCollection.Add(theOneDocPerson);
                        }
                        else
                        {
                            if (theNonValidatingPersonsCollection == null)
                                theNonValidatingPersonsCollection = new List<DocumentPerson>();

                            theNonValidatingPersonsCollection.Add(theOneDocPerson);
                        }
                    }


                    object invokerObjectID = null;
                    if (personsListInput.Any())
                        invokerObjectID = personsListInput[0].Id;
                    var peopleInquiry = new List<ApiResult<SabtAhvalServiceViewModel>>();
                    var esPersonsList = new List<ApiResult<GetGeolocationByNationalityCodeViewModel>>();
                    var isvalid = false;
                    string? errorMessage = null;

                    if (inputPersons == null)
                    {
                        errorMessage = "لیست اشخاص ورودی نباید خالی باشد.";
                        isvalid = false;
                        sabtAhvalErrorMessageRef = "خطا در تصدیق اصالت اشخاص حقیقی با استفاده از پایگاه داده ثبت احوال\n لطفاً جهت تصدیق مشخصات، مجدداً تلاش نمایید.\n" + errorMessage;

                        return (personsListInput, sabtAhvalErrorMessageRef);
                    }

                    (peopleInquiry, esPersonsList, isvalid, errorMessage) =
                        await _personInquiryManager.GetPersonInfo(inputPersons, cancellationToken);

                    // PersonInquiryManager.PersonInquiryManagerShell personServiceManagerController = new PersonInquiryManager.PersonInquiryManagerShell();
                    //  sabtAhvalPersons = personServiceManagerController.ConfirmPersonsListInfo ( inputPersons, currentCMSOrganization, false, invokerObjectID );
                    if (peopleInquiry.Any() && inputPersons.Any())
                        foreach (var theOneEsPerson in peopleInquiry)
                        {
                            List<DocumentPerson> selectedDocPersons = SelectPersonsFromList(theValidatingPersonsCollection, theOneEsPerson.Data);
                            if (selectedDocPersons.Any())
                            {
                                for (int selectedDocPersonIndex = 0; selectedDocPersonIndex < selectedDocPersons.Count; selectedDocPersonIndex++)
                                {
                                    string name = selectedDocPersons[selectedDocPersonIndex].Name + "--------" + selectedDocPersons[selectedDocPersonIndex].NationalNo;

                                    selectedDocPersons[selectedDocPersonIndex].IsSabtahvalChecked = NotaryIsDone.IsDone.GetString();

                                    if (theOneEsPerson.IsSuccess == true)
                                    {
                                        selectedDocPersons[selectedDocPersonIndex].IsSabtahvalCorrect = (theOneEsPerson.IsSuccess == true) ? YesNo.Yes.GetString() : YesNo.No.GetString();
                                        selectedDocPersons[selectedDocPersonIndex].IsAlive = (theOneEsPerson.Data.IsDead == true) ? YesNo.No.GetString() : YesNo.Yes.GetString();
                                        selectedDocPersons[selectedDocPersonIndex].SabtahvalInquiryDate =
                                            _dateTimeService
                                                .CurrentPersianDate; //Rad.CMS.BaseInfoContext.Instance.CurrentDateTime;
                                        selectedDocPersons[selectedDocPersonIndex].SabtahvalInquiryTime =
                                            _dateTimeService
                                                .CurrentTime;
                                    }
                                    else
                                    {
                                        sabtAhvalErrorMessageRef += "\n خطا در تصدیق مشخصات " + selectedDocPersons[selectedDocPersonIndex].FullName() + this.TrimSabtAhvalErrorMessage(theOneEsPerson.message.Count > 0 ? theOneEsPerson.message.ElementAt(0) : "");
                                        selectedDocPersons[selectedDocPersonIndex].IsSabtahvalCorrect = YesNo.None.GetString();
                                        selectedDocPersons[selectedDocPersonIndex].IsAlive = YesNo.None.GetString();
                                    }
                                }
                            }
                        }

                    if (theNonValidatingPersonsCollection.Any())
                    {
                        foreach (DocumentPerson theOneNonValidatingPerson in theNonValidatingPersonsCollection)
                        {
                            theOneNonValidatingPerson.IsAlive = YesNo.None.GetString();
                            theOneNonValidatingPerson.IsSabtahvalCorrect = YesNo.None.GetString();
                            theOneNonValidatingPerson.IsSabtahvalChecked = NotaryIsDone.None.GetString();
                        }
                    }


                    //foreach (IONotaryDocPerson theOnePerson in personsListInput)
                    //{
                    //    if (theOnePerson.IsInformationCorrect == YesNo.None && theOnePerson.PersonType == PersonType.NaturalPerson && theOnePerson.IsIranian == YesNo.Yes && theOnePerson.TheONotaryRegisterServiceReq.IsDocBasedJudgeHokm != YesNo.Yes)
                    //        this.SetFakeResults(ref personsListInput);
                    //}
                    //===========================================================================================

                    //if ( immediateCommit )
                    //    Rad.CMS.OjbBridge.TransactionContext.Current.Commit ();
                }
                catch// (Exception timeoutException)
                {
                    sabtAhvalErrorMessageRef = "خطا در تصدیق اصالت اشخاص حقیقی با استفاده از پایگاه داده ثبت احوال\n لطفاً جهت تصدیق مشخصات، مجدداً تلاش نمایید.\n";
                      //  + timeoutException.Message;
                }
            }

            return (personsListInput, sabtAhvalErrorMessageRef);

        }

        private void SetFakeResults(ref List<DocumentPerson> inputPersonsList)
        {
            if (inputPersonsList.Any())
                foreach (DocumentPerson theOneDocPerson in inputPersonsList)
                {
                    theOneDocPerson.IsSabtahvalCorrect = YesNo.None.GetString();
                }
        }

        internal async Task<(SabtAhvalServiceViewModel, string)> ConfirmPerson(InputPerson inputPerson, string sabtAhvalErrorMessage, CancellationToken cancellationToken, bool immediateCommit = true, object invokerObjectID = null, bool compareUsingSabtAhvalData = false)
        {
            string sabtAhvalErrorMessagRef = sabtAhvalErrorMessage;
            List<SabtAhvalServiceViewModel> sabtAhvalPersons = null;
            List<InputPerson> inputPersons = new List<InputPerson>();
            inputPersons.Add(inputPerson);
            var peopleInquiry = new List<ApiResult<SabtAhvalServiceViewModel>>();
            var esPersonsList = new List<ApiResult<GetGeolocationByNationalityCodeViewModel>>();
            var isvalid = false;
            string errorMessage = null;
            (peopleInquiry, esPersonsList, isvalid, errorMessage) = await _personInquiryManager.GetPersonInfo(inputPersons, cancellationToken);

            //try
            //{
                //ICMSOrganization currentCMSOrganization = Rad.CMS.InstanceBuilder.GetEntityById<ICMSOrganization>(Rad.CMS.BaseInfoContext.Instance.CurrentCMSOrganization.ObjectId);

                //PersonInquiryManager.PersonInquiryManagerShell personServiceManagerController = new PersonInquiryManager.PersonInquiryManagerShell();
                //sabtAhvalPersons = personServiceManagerController.ConfirmPersonsListInfo ( inputPersons, currentCMSOrganization, !immediateCommit, invokerObjectID, compareUsingSabtAhvalData );

                //if ( immediateCommit )
                //    Rad.CMS.OjbBridge.TransactionContext.Current.Commit ();
            //}
            //catch (System.TimeoutException timeoutException)
            //{
            //    sabtAhvalErrorMessagRef = "خطا در تصدیق اصالت اشخاص حقیقی با استفاده از پایگاه داده ثبت احوال\n لطفاً جهت تصدیق مشخصات، مجدداً تلاش نمایید.\n" + timeoutException.Message;
            //}


            if (sabtAhvalPersons!=null && sabtAhvalPersons.Any())
                return (sabtAhvalPersons[0], sabtAhvalErrorMessagRef);
            else
                return (null, sabtAhvalErrorMessagRef);
        }
        #endregion

        #region GetCircularImage
        private byte[] GetCircularImage(string circularImageObjectID)
        {
            return null;
            //Rad.Circular.CircularImage circularImage = Rad.CMS.InstanceBuilder.GetEntityById<Rad.Circular.CircularImage>(circularImageObjectID);
            //if ( circularImage != null )
            //    return circularImage.CircularImageFile;
            //else
            //    return null;
        }
        #endregion

        #region TrimSabtAhvalErrorMessage
        private string TrimSabtAhvalErrorMessage(string inputMessage)
        {
            if (!string.IsNullOrEmpty(inputMessage) && inputMessage.Contains("ثبت احوال:\r\n"))
                return " - " + inputMessage.Replace("ثبت احوال:\r\n", string.Empty);
            else
                return string.Empty;
        }
        #endregion

        #region ResultsValidation
        internal bool IsResponseListPermitted(List<DocumentPerson> requestPersonsList, List<DocumentPerson> responsePersonsList, ref string warningMessage)
        {
            if (requestPersonsList.Count != responsePersonsList.Count)
            {
                warningMessage = "تعداد اشخاص دریافت شده از تعداد اشخاص مورد استعلام کمتر است. جواب استعلام معتبر نمی باشد.";
                return false;
            }

            try
            {
                bool deadIndividualsStatus = this.VerifyDeadIndividulas(responsePersonsList, ref warningMessage);
                bool wrongIndividualsStatus = this.VerifyUnknownAndWrongIndividulas(responsePersonsList, ref warningMessage);

                bool isPermitted = deadIndividualsStatus && wrongIndividualsStatus;
                return isPermitted;
            }
            catch (Exception ex)
            {
                warningMessage = "خطا در بررسی نتیجه استعلام ثبت احوال. لطفاً مجدداً تلاش نمایید.";
                return false;
            }
        }

        private bool IsAgentRelationPermitted(string AgentTypeCode)
        {
            switch (AgentTypeCode)
            {
                case "9":
                    return true;
                default:
                    return false;
            }
        }

        private bool VerifyDeadIndividulas(List<DocumentPerson> responsePersonsList, ref string warningMessage)
        {
            bool isPermitted = true;
            foreach (DocumentPerson theOnePerson in responsePersonsList)
            {
                string s = theOnePerson.NationalNo;
                bool isAgentRelationDefined = this.IsAgentRelationDefined(theOnePerson);

                //Newly Added
                if (theOnePerson.IsAlive == YesNo.None.GetString() && (theOnePerson.PersonType == PersonType.Legal.GetString() || theOnePerson.IsIranian == YesNo.No.GetString()))
                    continue;

                if (theOnePerson.IsAlive == YesNo.None.GetString())
                {
                    if (
                        theOnePerson.Document.IsBasedJudgment == YesNo.Yes.GetString() &&
                        (
                        (theOnePerson.DocumentPersonType != null && theOnePerson.DocumentPersonType.IsOwner == YesNo.Yes.GetString()) ||
                        (theOnePerson.DocumentPersonType == null && isAgentRelationDefined)
                        )
                        )
                    {
                        continue;
                    }
                    if (
                        theOnePerson.DocumentPersonType != null &&
                        (
                            theOnePerson.DocumentPersonType.Id == "007" ||
                            theOnePerson.DocumentPersonType.Id == "0034" ||
                            theOnePerson.DocumentPersonType.Id == "008" ||
                            theOnePerson.DocumentPersonType.Id == "12" ||
                            theOnePerson.DocumentPersonType.Id == "11" ||
                            theOnePerson.DocumentPersonType.Id == "8" ||
                            theOnePerson.DocumentPersonType.Id == "7" ||
                            theOnePerson.DocumentPersonType.Id == "28" ||
                            theOnePerson.DocumentPersonType.Id == "5" ||
                            theOnePerson.DocumentPersonType.Id == "3" ||
                            theOnePerson.DocumentPersonType.Id == "85" ||
                            theOnePerson.DocumentPersonType.Id == "20" ||
                            theOnePerson.DocumentPersonType.Id == "25" ||
                            theOnePerson.DocumentPersonType.Id == "13" ||
                            theOnePerson.DocumentPersonType.Id == "73" ||
                            theOnePerson.DocumentPersonType.Id == "1" ||
                            theOnePerson.DocumentPersonType.Id == "60" ||
                            theOnePerson.DocumentPersonType.Id == "70" ||
                            theOnePerson.DocumentPersonType.Id == "71" ||
                            theOnePerson.DocumentPersonType.Id == "75" ||
                            theOnePerson.DocumentPersonType.Id == "78" ||
                            theOnePerson.DocumentPersonType.Id == "83" ||
                            theOnePerson.DocumentPersonType.Id == "15" ||
                            theOnePerson.DocumentPersonType.Id == "57"
                        )
                    )
                    {
                        continue;
                    }

                    isPermitted = false;

                    string temp = "وضعیت حیات " + theOnePerson.FullName() + " استعلام نشده است. ";
                    if (string.IsNullOrWhiteSpace(warningMessage) || !warningMessage.Contains(temp))
                        warningMessage += temp + System.Environment.NewLine;

                    continue;
                }

                //if (theOnePerson.IsDead != Rad.CMS.Enumerations.YesNo.Yes)
                if (theOnePerson.IsAlive == YesNo.Yes.GetString())
                    continue;

                if (theOnePerson.IsAlive == YesNo.No.GetString())
                {
                    #region Judgement
                    if (
                        theOnePerson.Document.IsBasedJudgment == YesNo.Yes.GetString() &&
                        (
                        (theOnePerson.DocumentPersonType != null && theOnePerson.DocumentPersonType.IsOwner == YesNo.Yes.GetString()) ||
                        (theOnePerson.DocumentPersonType == null && isAgentRelationDefined)
                        )
                        )
                    {
                        continue;
                    }
                    else if (theOnePerson.Document.IsBasedJudgment == YesNo.No.GetString() && isAgentRelationDefined)
                    {
                        continue;
                    }
                    else if // با تایید نهایی مهندس فراهانی این بخش از کد فعال می گردد.
                        (
                        theOnePerson.Document.DocumentTypeId == "004" && //سند فک رهن 
                        theOnePerson.IsOriginal == YesNo.Yes.GetString() && //شخص اصیل
                        theOnePerson.DocumentPersonTypeId != null && theOnePerson.DocumentPersonTypeId == "3" //راهن
                        )
                    {
                        continue;
                    }
                    else
                    {
                        isPermitted = false;

                        string temp = theOnePerson.FullName() + " در قید حیات نمی باشد، ";
                        if (string.IsNullOrWhiteSpace(warningMessage) || !warningMessage.Contains(temp))
                            warningMessage += temp + System.Environment.NewLine;

                        continue;
                    }
                    #endregion

                    #region PersonRole And DocType
                    if (
                        (!this.IsDeadPersonPermittedBasedOnDocType(theOnePerson.Document.DocumentTypeId)) &&
                        (!this.IsDeadPersonPermittedBasedOnPersonRole(theOnePerson))
                        )
                    {
                        isPermitted = false;

                        string temp = theOnePerson.FullName() + " در قید حیات نمی باشد، ";
                        if (string.IsNullOrWhiteSpace(warningMessage) || !warningMessage.Contains(temp))
                            warningMessage += temp + System.Environment.NewLine;

                        continue;
                    }
                    #endregion
                }

            }
            return isPermitted;
        }

        private bool IsDeadPersonPermittedBasedOnPersonRole(DocumentPerson theDeadPerson)
        {
            if (theDeadPerson.IsAlive == YesNo.Yes.GetString())
                return true;

            if (theDeadPerson.IsAlive == YesNo.No.GetString())
            {
                foreach (DocumentPerson theOneMainDocPerson in theDeadPerson.Document.DocumentPeople)
                {
                    foreach (DocumentPersonRelated theOneAgentPrs in theOneMainDocPerson.DocumentPersonRelatedAgentPeople)
                    {
                        if (theOneAgentPrs.AgentPerson.NationalNo == theDeadPerson.NationalNo)
                        {
                            if (this.IsAgentRelationPermitted(theOneAgentPrs.AgentTypeId))
                                return true;
                            else
                                return false;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }

            return false;
        }

        private bool IsDeadPersonPermittedBasedOnDocType(string theDocTypeCode)
        {
            switch (theDocTypeCode)
            {
                case "004":
                    return true;

                default:
                    return false;
            }
        }

        private bool VerifyUnknownAndWrongIndividulas(List<DocumentPerson> responsePersonsList, ref string warningMessage)
        {
            bool isAllPermitted = true;

            if (_clientConfiguration.RejectWrongIndividuals)
            {
                foreach (DocumentPerson theOneResponsPerson in responsePersonsList)
                {
                    if (theOneResponsPerson.PersonType == PersonType.Legal.GetString() || theOneResponsPerson.IsIranian == YesNo.No.GetString())
                        continue;

                    bool isSinglePermitted = false;

                    switch (theOneResponsPerson.IsSabtahvalCorrect)
                    {
                        case "2":
                            warningMessage += "\n بر اساس پاسخ استعلام از ثبت احوال، اطلاعات شخصی " + theOneResponsPerson.FullName() + " معتبر نمی باشد.";
                            isSinglePermitted = false;
                            break;

                        case "0":

                            bool isAgentRelationDefined = this.IsAgentRelationDefined(theOneResponsPerson);

                            if (
                                theOneResponsPerson.Document.IsBasedJudgment == YesNo.Yes.GetString() &&
                                (
                                (theOneResponsPerson.DocumentPersonType != null && theOneResponsPerson.DocumentPersonType.IsOwner == YesNo.Yes.GetString()) ||
                                (theOneResponsPerson.DocumentPersonType == null && isAgentRelationDefined)
                                )
                                )
                            {
                                isSinglePermitted = true;
                            }
                            else if (
                                     (theOneResponsPerson.DocumentPersonTypeId == "007" ||
                                     theOneResponsPerson.DocumentPersonTypeId == "0034" ||
                                     theOneResponsPerson.DocumentPersonTypeId == "008") &&    // اخطاریه، اجرائیه یا رفع نقص اجرائیه
                                     theOneResponsPerson.DocumentPersonTypeId != null &&
                                     (theOneResponsPerson.DocumentPersonTypeId == "12" ||
                                      theOneResponsPerson.DocumentPersonTypeId == "11" ||
                                      theOneResponsPerson.DocumentPersonTypeId == "8" ||
                                      theOneResponsPerson.DocumentPersonTypeId == "7" ||
                                      theOneResponsPerson.DocumentPersonTypeId == "28" ||
                                      theOneResponsPerson.DocumentPersonTypeId == "5" ||
                                      theOneResponsPerson.DocumentPersonTypeId == "3" ||
                                      theOneResponsPerson.DocumentPersonTypeId == "85" ||
                                      theOneResponsPerson.DocumentPersonTypeId == "20" ||
                                      theOneResponsPerson.DocumentPersonTypeId == "25" ||
                                      theOneResponsPerson.DocumentPersonTypeId == "13" ||
                                      theOneResponsPerson.DocumentPersonTypeId == "73" ||
                                      theOneResponsPerson.DocumentPersonTypeId == "1" ||
                                      theOneResponsPerson.DocumentPersonTypeId == "60" ||
                                      theOneResponsPerson.DocumentPersonTypeId == "70" ||
                                      theOneResponsPerson.DocumentPersonTypeId == "71" ||
                                      theOneResponsPerson.DocumentPersonTypeId == "75" ||
                                      theOneResponsPerson.DocumentPersonTypeId == "78" ||
                                      theOneResponsPerson.DocumentPersonTypeId == "83" ||
                                      theOneResponsPerson.DocumentPersonTypeId == "15" ||
                                      theOneResponsPerson.DocumentPersonTypeId == "57")
                                     )
                            {
                                isSinglePermitted = true;
                            }
                            else
                            {
                                warningMessage += "\nاستعلام ثبت احوال برای " + theOneResponsPerson.FullName() + " نتیجه ای دربر نداشت. ";
                                isSinglePermitted = false;
                            }

                            break;

                        case "1":
                            isSinglePermitted = true;
                            break;
                    }

                    isAllPermitted = isSinglePermitted & isAllPermitted;

                }
            }

            return isAllPermitted;
        }
        #endregion

        #region IsAgentRelationDefined
        private bool IsAgentRelationDefined(DocumentPerson theOneDocPerson)
        {
            if (theOneDocPerson.DocumentPersonType == null && theOneDocPerson.IsOriginal == YesNo.No.GetString())
            {
                #region If Movarres Is Being Searched - (For MovarresTypes The Relation Is Defined In TheCurrentPerson's AgentsColletion)
                if (theOneDocPerson.DocumentPersonRelatedAgentPeople.Any())
                {
                    foreach (DocumentPersonRelated theOneDocAgent in theOneDocPerson.DocumentPersonRelatedAgentPeople)
                    {
                        if (
                            theOneDocAgent.AgentTypeId == "9" ||    // مورث
                            theOneDocAgent.AgentTypeId == "13"      // موصی
                             //theOneDocAgent.TheONotaryAgentType.Code == "5"      //قائم مقام
                            )
                        {
                            return true;
                        }
                    }
                }
                #endregion

                #region If NoAgent Is Defiend For Current Person - (جتسجوی ارتباطاتی همانند قائم مقامی)
                else
                {
                    foreach (DocumentPerson theSeekingPerson in theOneDocPerson.Document.DocumentPeople)
                    {
                        if (theSeekingPerson.Id == theOneDocPerson.Id)
                            continue;

                        if (!theSeekingPerson.DocumentPersonRelatedAgentPeople.Any())
                            continue;

                        foreach (DocumentPersonRelated theOneDocAgent in theSeekingPerson.DocumentPersonRelatedAgentPeople)
                        {
                            if (theOneDocAgent.AgentTypeId == "5")
                            {
                                if (theOneDocAgent.MainPersonId == theOneDocPerson.Id)
                                    return true;
                            }
                        }
                    }
                }
                #endregion
            }


            return false;
        }
        #endregion

        #endregion


        //public FinalVerificationOutputMessage FinalVerifyPersonsCollection ( FinalVerificationInputMessage verificationRequest )
        //{
        //    NotaryOfficeImplementation.MessageDefinitions.FinalVerificationOutputMessage verificationResponse = new NotaryOfficeImplementation.MessageDefinitions.FinalVerificationOutputMessage();

        //    using ( new TransactedOperation ( true ) )
        //    {
        //        #region PersonsInquiryVerification
        //        string finalVerificationEstelam = System.Configuration.ConfigurationSettings.AppSettings["FinalVerificationEstelam"];
        //        bool finalVerificationEstelamFlag = true;
        //        Boolean.TryParse ( finalVerificationEstelam, out finalVerificationEstelamFlag );

        //        if ( finalVerificationEstelamFlag && verificationRequest.SabtAhvalValidation )
        //        {
        //            FinalVerificationManager.TypeDefinition.PersonsVerificationResponse personsVerificationResponse = this.ConfirmPersonsCollection(verificationRequest.personsListInput, false);

        //            verificationResponse.personsList = personsVerificationResponse.VerifiedPersonsCollection;
        //            verificationResponse.IsPersonsListPermitted = personsVerificationResponse.IsResponseValid;
        //            verificationResponse.SabtAhvalErrors = personsVerificationResponse.SabtAhvalErrorMessage;
        //            verificationResponse.PersonsValidationMessage = personsVerificationResponse.ValidationMessage;
        //        }
        //        else
        //        {
        //            verificationResponse.personsList = new List<DocumentPerson> ();
        //            verificationResponse.personsList = verificationRequest.personsListInput;
        //        }
        //        #endregion

        //        #region PersonsCircularsVerification


        //        int totalNaturalCircularItemsCount = 0;
        //        CircularsManager.CircularItemsHelper circularItemsHelper = new CircularsManager.CircularItemsHelper();
        //        NotaryOfficeImplementation.CustomClassesDefinitions.InputPerson inputPerson = null;
        //        inputPerson = new InputPerson ( verificationRequest.thOnePersonInput );

        //        verificationResponse.circularItems = circularItemsHelper.GetProhibitedNaturalPersonsList ( inputPerson, verificationRequest.thOnePersonInput, verificationRequest.PageNumber, ref totalNaturalCircularItemsCount, true, verificationRequest.CircularLogging, false );
        //        verificationResponse.totalItemsCount = totalNaturalCircularItemsCount;

        //        for ( int i = 1; i < verificationRequest.personsListInput.Count; i++ )
        //        {
        //            NotaryOfficeImplementation.CustomClassesDefinitions.InputPerson inputPersonList = null;
        //            inputPersonList = new InputPerson ( verificationRequest.personsListInput [ i ] );
        //            circularItemsHelper.GetProhibitedNaturalPersonsList ( inputPersonList, verificationRequest.thOnePersonInput, verificationRequest.PageNumber, ref totalNaturalCircularItemsCount, false, verificationRequest.CircularLogging, false );
        //        }


        //        #endregion

        //        Rad.CMS.OjbBridge.TransactionContext.Current.Commit ();
        //    }

        //    return verificationResponse;
        //}
    }

}
