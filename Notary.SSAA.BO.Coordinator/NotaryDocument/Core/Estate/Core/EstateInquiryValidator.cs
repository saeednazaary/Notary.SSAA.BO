using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using Notary.SSAA.BO.Domain.Abstractions;
using MediatR;
using System.Threading;
using Notary.SSAA.BO.Configuration;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.EstateInquiry;
using Notary.SSAA.BO.Domain.RichDomain.Document;
using Notary.SSAA.BO.Utilities.Other;
using StackExchange.Profiling.Internal;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Estate.Core
{
    public class EstateInquiryValidator
    {
        public List<EstateInquiry> _theESTEstateInquiryCollection = null;
        public Document _theONotaryRegisterServiceReq = null;
        private readonly IEstateInquiryRepository _estateInquiryRepository;
        private readonly ClientConfiguration _clientConfiguration;
        private readonly IMediator _mediator;
        private readonly DSULogger _dSULogger;
        private readonly IDocumentRepository _documentRepository;

        public EstateInquiryValidator(IEstateInquiryRepository estateInquiryRepository, IDocumentRepository documentRepository, ClientConfiguration clientConfiguration, IMediator mediator, DSULogger dSULogger)
        {
            _estateInquiryRepository = estateInquiryRepository;
            _clientConfiguration = clientConfiguration;
            _mediator = mediator;
            _dSULogger = dSULogger;
            _documentRepository = documentRepository;
        }
        public async Task initialEstateInquiryValidator(Document theONotaryRegisterServiceReqInput, CancellationToken cancellationToken)
        {

            List<Guid> estEstateInquiriesIDs = new();

            _theONotaryRegisterServiceReq = theONotaryRegisterServiceReqInput;

            if (_theONotaryRegisterServiceReq.DocumentType.WealthType != WealthType.Immovable.GetString())
                return;

            if (_theONotaryRegisterServiceReq.DocumentInquiries.Any())
            {
                foreach (DocumentInquiry theOneRegServiceInquiry in _theONotaryRegisterServiceReq.DocumentInquiries)
                {
                    if (theOneRegServiceInquiry.DocumentInquiryOrganizationId == "1")
                    {


                        estEstateInquiriesIDs.Add(Guid.Parse(theOneRegServiceInquiry.EstateInquiriesId));
                    }
                }

                if (!estEstateInquiriesIDs.Any())
                    return;


                _theESTEstateInquiryCollection = await _estateInquiryRepository.TableNoTracking.Include(t => t.EstateInquiryPeople).Where(t => estEstateInquiriesIDs.Contains(t.Id)).ToListAsync(cancellationToken);//Rad.CMS.InstanceBuilder.GetEntityListByCriteria<IESTEstateInquiry> ( criteria ) as IESTEstateInquiryCollection;
            }

        }

        public async Task<bool> Verify(string verificationMessage, List<string> sentInquiriesCollection, CancellationToken cancellationToken)
        {
            string verificationMessageRef = verificationMessage;
            List<string> sentInquiriesCollectionRef = sentInquiriesCollection;
            if (_theONotaryRegisterServiceReq.DocumentType.WealthType != WealthType.Immovable.GetString())
                return true;

            if (!_theONotaryRegisterServiceReq.DocumentInquiries.Any())
            {
                verificationMessageRef = "هیچ استعلامی در سند وجود ندارد.";
                return false;
            }

            if (!_theESTEstateInquiryCollection.Any())
            {
                verificationMessageRef = "هیچ استعلامی در سند یافت نشد.";
                return false;
            }

            //This is being used to validate inquiries based on current time and state, these inquiries should not be used in other dsuDealSummaries and have to meet time expiration limits
            if (_theONotaryRegisterServiceReq.DocumentType.DocumentTypeGroup1Id != "6")    // تقسیم نامه
            {
                bool inquiriesValidated = await this.ValidateInquiries(_theESTEstateInquiryCollection, cancellationToken, verificationMessageRef, sentInquiriesCollectionRef);
                if (!inquiriesValidated)
                    return false;
            }

            bool verifyPersonsBasedOnInquiries =
                await VerifyPersonsBasedOnInquiries(verificationMessageRef, cancellationToken);
            if (!verifyPersonsBasedOnInquiries)
                return false;

            if (!this.VerifyRegCasesBasedOnInquiries(ref verificationMessageRef))
                return false;

            if (!this.VerifyOwnershipDocBasedOnInquiries(ref verificationMessageRef))
                return false;


            return true;
        }

        public async Task<(RequirmentsValidationStatus, List<string>, string)> ValidateDSURequirments(Document theCurrentRegisterServiceReq, CancellationToken cancellationToken, List<string> sentInquiries, string message)
        {
            List<string> sentInquiriesRef = sentInquiries;
            string messageRef = message;

            bool isRestricted = Mapper.IsONotaryDocumentRestrictedType(theCurrentRegisterServiceReq.DocumentTypeId);

            #region QuotasValidation
            QuotasValidator quotasValidator = new QuotasValidator(_clientConfiguration);
            bool quotasAreValid = quotasValidator.VerifyRegCasesInquiriesAndQuotas(theCurrentRegisterServiceReq, ref messageRef);
            if (!quotasAreValid)
            {
                if (string.IsNullOrWhiteSpace(messageRef))
                    messageRef = "خطا در بررسی سهم بندی های انجام شده. ";

                await _dSULogger.LogVerificationMessages(theCurrentRegisterServiceReq.Id.ToString(), messageRef, cancellationToken);
                return (RequirmentsValidationStatus.Failed, sentInquiriesRef, messageRef);
            }
            #endregion

            #region Restricted
            if (isRestricted)
            {
                ///در صورتی که کاربر در حال گرفتن اثرانگشت می باشد لازم نمی باشد که ترتیب اسناد رهنی و قطعی را بررسی نماییم.
                ///بنابراین زنجیره فراخوانی توابع را بررسی می نماییم. در صورتیکه تابع اثرانگشت تابع جاری را فراخوانی کرده باشد، ترتیب اسناد را بررسی نمی کنیم.
                #region Sequence Check
                List<Document> theRelatedDeterministicRegisterServicesCollection = null;
                System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace();
                List<System.Diagnostics.StackFrame> stackFramesCollection = stackTrace.GetFrames().Where(q => q.GetMethod().Name == "GetPagedFingerPrintsAndPersons").ToList();
                if (!stackFramesCollection.Any())
                {
                    if (
                        theCurrentRegisterServiceReq.State != NotaryRegServiceReqState.FinalPrinted.GetString() &&
                        theCurrentRegisterServiceReq.State != NotaryRegServiceReqState.SetNationalDocumentNo.GetString()
                        )
                    {
                        bool sequenceIsConfirmed = await this.VerifySequenceOnBeforeNationalNo(theCurrentRegisterServiceReq, cancellationToken, theRelatedDeterministicRegisterServicesCollection, messageRef);
                        if (!sequenceIsConfirmed)
                        {
                            await _dSULogger.LogVerificationMessages(theCurrentRegisterServiceReq.Id.ToString(), messageRef, cancellationToken);
                            return (RequirmentsValidationStatus.SequenceCheckError, sentInquiriesRef, messageRef); ;
                        }
                    }
                    else
                    {
                        bool sequenceIsConfirmed = await this.VerifySequenceOnBeforeSend(theCurrentRegisterServiceReq, cancellationToken, theRelatedDeterministicRegisterServicesCollection, messageRef);
                        if (!sequenceIsConfirmed)
                        {
                            _dSULogger.LogVerificationMessages(theCurrentRegisterServiceReq.Id.ToString(), messageRef, cancellationToken);
                            return (RequirmentsValidationStatus.SequenceCheckError, sentInquiriesRef, messageRef); ;
                        }
                    }
                }
                #endregion

                #region Compatiblity Check
                bool isCompatibleToDeterministicReqs = await this.IsCurrentReqCompatibleToRelatedDeterministicReq(theCurrentRegisterServiceReq, cancellationToken, messageRef, theRelatedDeterministicRegisterServicesCollection);
                if (!isCompatibleToDeterministicReqs)
                {
                    _dSULogger.LogVerificationMessages(theCurrentRegisterServiceReq.Id.ToString(), messageRef, cancellationToken);
                    return (RequirmentsValidationStatus.CompatibleCheckError, sentInquiriesRef, messageRef); ;
                }
                #endregion
            }
            #endregion

            #region Deterministic
            else
            {
                (bool areInquiriesPermittedToUse, messageRef) = await this.AreCurrentInquiriesPermittedToUse(theCurrentRegisterServiceReq, cancellationToken, messageRef);
                if (!areInquiriesPermittedToUse)
                {
                    await _dSULogger.LogVerificationMessages(theCurrentRegisterServiceReq.Id.ToString(), messageRef, cancellationToken);
                    return (RequirmentsValidationStatus.InquiriesNotPermitted, sentInquiriesRef, messageRef); ;
                }
            }
            #endregion

            #region Inquiries Validation And Verification
            sentInquiriesRef = new List<string>();
            bool inquiriesAreValid = await this.Verify(messageRef, sentInquiriesRef, cancellationToken);
            if (!inquiriesAreValid)
            {
                await _dSULogger.LogVerificationMessages(theCurrentRegisterServiceReq.Id.ToString(), messageRef, cancellationToken);
                return (RequirmentsValidationStatus.InquiriesNotPermitted, sentInquiriesRef, messageRef);
            }
            #endregion

            return (RequirmentsValidationStatus.Succeeded, sentInquiriesRef, messageRef);
        }

        public RequirmentsValidationStatus ValidateRemovingDSURequirments(Document theCurrentRegisterServiceReq, ref string message)
        {
            if (theCurrentRegisterServiceReq.DocumentTypeId != "004")
            {
                message = "نوع سند فک رهن نمی باشد. ارسال خلاصه رفع محدودیت وجود نخواهد داشت.";
                return RequirmentsValidationStatus.Failed;
            }


            return RequirmentsValidationStatus.Succeeded;
        }

        private async Task<bool> VerifySequenceOnBeforeSend(Document theCurrentRegisterServiceReq, CancellationToken cancellationToken, ICollection<Document> theRelatedDeterministicReqs, string sequenceMessage)
        {
            ICollection<Document> theRelatedDeterministicReqsRef = theRelatedDeterministicReqs;
            string sequenceMessageRef = sequenceMessage;
            //ConfigurationManager.TypeDefinitions.ClientConfiguration clientConfiguration = new ConfigurationManager.TypeDefinitions.ClientConfiguration(theCurrentRegisterServiceReq.TheScriptorium.TheCMSOrganization);

            if (string.Compare(theCurrentRegisterServiceReq.RequestDate, _clientConfiguration.DSUInitializationDate) < 0)  // بعد از راه اندازی ارسال خلاصه معامله از طریق ثبت الکترونیک اسناد
                return true;

            if (!Mapper.IsONotaryDocumentRestrictedType(theCurrentRegisterServiceReq.DocumentTypeId)) //اسناد رهنی
                return true;

            if (!theCurrentRegisterServiceReq.DocumentInquiries.Any())
                return true;

            if (theRelatedDeterministicReqsRef == null ||  !theRelatedDeterministicReqsRef.Any())
                theRelatedDeterministicReqsRef = await this.FindRelatedDeterministicRegServices(theCurrentRegisterServiceReq, cancellationToken);

            if (theRelatedDeterministicReqsRef == null || !theRelatedDeterministicReqsRef.Any())
                return true;

            List<string> notFinalizedNationalNos = new List<string>();
            foreach (Document theOneDeterministicRegisterServiceReq in theRelatedDeterministicReqsRef)
            {
                if (
                    theOneDeterministicRegisterServiceReq.State != NotaryRegServiceReqState.Finalized.GetString() &&
                    (theOneDeterministicRegisterServiceReq.State == NotaryRegServiceReqState.SetNationalDocumentNo.GetString() || theOneDeterministicRegisterServiceReq.State == NotaryRegServiceReqState.FinalPrinted.GetString())
                    )
                {
                    if (!notFinalizedNationalNos.Contains(theOneDeterministicRegisterServiceReq.NationalNo))
                        notFinalizedNationalNos.Add(theOneDeterministicRegisterServiceReq.NationalNo);
                }
            }

            if (!notFinalizedNationalNos.Any())
            {
                return true;
            }

            foreach (string theOneNationalNo in notFinalizedNationalNos)
                sequenceMessageRef += " - " + theOneNationalNo + System.Environment.NewLine;

            sequenceMessageRef = "اسناد غیر منقول با شناسه یکتاهای اعلام شده در زیر هنوز تایید نهایی نشده اند:"
                              + System.Environment.NewLine
                              + sequenceMessageRef
                              + System.Environment.NewLine
                              + "برای تایید نهایی سند جاری و ارسال خلاصه معامله های مربوط به سند جاری، لطفاً نسبت به تایید نهایی اسناد ذکر شده اقدام نمایید.";

            return false;
        }

        private async Task<bool> VerifySequenceOnBeforeNationalNo(Document theCurrentRegisterServiceReq, CancellationToken cancellationToken, List<Document> theRelatedDeterministicReqs, string sequenceMessage)
        {
            List<Document> theRelatedDeterministicReqsRef = theRelatedDeterministicReqs;
            string sequenceMessageRef = sequenceMessage;
            //ConfigurationManager.TypeDefinitions.ClientConfiguration clientConfiguration = new ConfigurationManager.TypeDefinitions.ClientConfiguration(theCurrentRegisterServiceReq.TheScriptorium.TheCMSOrganization);

            if (string.Compare(theCurrentRegisterServiceReq.RequestDate, _clientConfiguration.DSUInitializationDate) < 0)  // بعد از راه اندازی ارسال خلاصه معامله از طریق ثبت الکترونیک اسناد
                return true;

            if (!Mapper.IsONotaryDocumentRestrictedType(theCurrentRegisterServiceReq.DocumentTypeId)) //اگر سند از نوع محدودیت نیست
                return true;

            if (!theCurrentRegisterServiceReq.DocumentInquiries.Any())
                return true;

            theRelatedDeterministicReqsRef = await this.FindRelatedDeterministicRegServices(theCurrentRegisterServiceReq, cancellationToken);
            if (theRelatedDeterministicReqsRef==null || !theRelatedDeterministicReqsRef.Any())
            {
                //sequenceMessage = 
                //    "نکات مربوط به خلاصه معامله:\n" +
                //    "سند قطعی مربوط به این پرونده رهنی یافت نشد. لطفاً ترتیب تنظیم سند قطعی و رهنی را رعایت فرمایید.\n"+
                //    "در نظر داشته باشید در صورت تایید نهایی پرونده رهنی، تنظیم و تایید پرونده قطعی مربوطه امکان پذیر نخواهد بود.";

                return true;
            }
            else
            {
                List<string> reqNosCollection = new List<string>();

                foreach (Document theOneDeterRegServiceReq in theRelatedDeterministicReqsRef)
                {
                    //اگر سندی با شناسه یکتا وجود داشته باشد ، بنابراین سند قطعی ترتیبش رعایت شده و سند رهنی مجاز به اخذ شناسه می باشد.
                    if (
                        theOneDeterRegServiceReq.State == NotaryRegServiceReqState.SetNationalDocumentNo.GetString() ||
                        theOneDeterRegServiceReq.State == NotaryRegServiceReqState.FinalPrinted.GetString() ||
                        theOneDeterRegServiceReq.State == NotaryRegServiceReqState.Finalized.GetString()
                        )
                        continue;

                    string individualMessage = System.Environment.NewLine + theOneDeterRegServiceReq.ReceiptNo;
                    if (!reqNosCollection.Contains(individualMessage))
                        reqNosCollection.Add(individualMessage);
                }

                if (!reqNosCollection.Any())
                    return true;

                foreach (string theOneReqNo in reqNosCollection)
                    sequenceMessageRef += theOneReqNo;

                sequenceMessageRef = "پرونده های غیر منقول با شماره پرونده های اعلام شده در زیر شناسه یکتا نگرفته اند:"
                                  + System.Environment.NewLine
                                  + sequenceMessageRef
                                  + System.Environment.NewLine
                                  + "لطفاً ابتدا برای اخذ شناسه پرونده قطعی مورد نظر اقدام نموده و پرونده های بلااستفاده را باطل نمایید.";

                return false;
            }
        }

        
        private async Task<(bool, string)> AreCurrentInquiriesPermittedToUse(Document theCurrentRegisterServiceReq, CancellationToken cancellationToken, string message)
        {
            return (true, null);
            string messageRef = message;
            var foundUsedInquiryRegServices = await this.FindRelatedDeterministicRegServices(theCurrentRegisterServiceReq, cancellationToken);

            if (foundUsedInquiryRegServices.Any())
            {
                foreach (Document theOneRegisterServiceReq in foundUsedInquiryRegServices)
                {
                    if (
                        theOneRegisterServiceReq.State == NotaryRegServiceReqState.SetNationalDocumentNo.GetString() ||
                        theOneRegisterServiceReq.State == NotaryRegServiceReqState.FinalPrinted.GetString() ||
                        theOneRegisterServiceReq.State == NotaryRegServiceReqState.Finalized.GetString()
                        )
                        messageRef += " - " + theOneRegisterServiceReq.RequestNo + " : " + theOneRegisterServiceReq.DocumentType.Title + System.Environment.NewLine;
                }

                if (string.IsNullOrWhiteSpace(messageRef))
                    return (true, messageRef);

                messageRef =
                    "استعلام های انتخاب شده شما، در پرونده های زیر استفاده شده اند:\n\n" +
                    messageRef +
                    System.Environment.NewLine +
                    "امکان استفاده استعلام در پرونده های از نوع انتقال قطعی، بیش از یکبار مجاز نمی باشد.";

                return (false, messageRef);
            }
            else
            {
                return (true, messageRef);
            }
        }

        private async Task<bool> IsCurrentReqCompatibleToRelatedDeterministicReq(Document theCurrentRegisterServiceReq, CancellationToken cancellationToken, string message, ICollection<Document> theRelatedDeterministicReqs = null)
        {
            string messageRef = message;
            bool isCompatible = false;

            if (theRelatedDeterministicReqs == null || !theRelatedDeterministicReqs.Any())
                theRelatedDeterministicReqs = await this.FindRelatedDeterministicRegServices(theCurrentRegisterServiceReq, cancellationToken);

            //If No Deterministic Found, it's neccessary not to change InquiryPersons as Owner
            if (theRelatedDeterministicReqs == null || !theRelatedDeterministicReqs.Any())
            {
                bool areOwnersEqualToInquiryPersons = await this.AreOwnersEqualToInquiryPersons(theCurrentRegisterServiceReq, cancellationToken, messageRef);
                if (!areOwnersEqualToInquiryPersons)
                    return false;

                isCompatible = true;
            }
            else
            {
                isCompatible = await this.IsRestrictedReqCompatibleToDeterministicReqs(theRelatedDeterministicReqs, theCurrentRegisterServiceReq, cancellationToken, messageRef);
            }

            return isCompatible;
        }

        private async Task<bool> IsRestrictedReqCompatibleToDeterministicReqs(ICollection<Document> theDeterministicRegisterServiceReqs, Document theRestrictedRegisterServiceReq, CancellationToken cancellationToken, string message)
        {
            string messageRef = message;

            if (!theDeterministicRegisterServiceReqs.Any())
            {
                messageRef = "هیچ سند قطعی معادلی یافت نشد.";
                return false;
            }


            foreach (DocumentEstate theOneRestrictedRegCase in theRestrictedRegisterServiceReq.DocumentEstates)
            {
                if (string.IsNullOrWhiteSpace(theOneRestrictedRegCase.EstateInquiryId))
                    continue;

                if (!theOneRestrictedRegCase.DocumentEstateOwnershipDocuments.Any())
                {
                    messageRef = "برای " + theOneRestrictedRegCase.RegCaseText + " هیچ مستند مالکیتی تعریف نشده است.";
                    return false;
                }

                foreach (DocumentEstateOwnershipDocument theOneRestrictedOwnershipDoc in theOneRestrictedRegCase.DocumentEstateOwnershipDocuments)
                {
                    if (string.IsNullOrWhiteSpace(theOneRestrictedOwnershipDoc.EstateInquiriesId))
                        continue;

                    #region DoCompatiblity-Check Only If OwnershipDocPerson Is Not Same As InquiryOwnerPerson
                    //Rad.ssaa.ssaaClass.IBSTPerson theMainBSTPerson = (Rad.ssaa.ssaaClass.IBSTPerson)theMainInquiryObject.TheBSTPersonList[0];
                    //string inquiryPersonNationalNo = theMainBSTPerson.C_NationalityCode;
                    //string inquiryPersonFullName = theMainBSTPerson.C_Name;
                    //bool isInquiryOwnerChanged = true;

                    //if (
                    //    inquiryPersonNationalNo != null && theOneRestrictedOwnershipDoc.TheONotaryDocPerson.NationalNo != null &&
                    //    inquiryPersonNationalNo == theOneRestrictedOwnershipDoc.TheONotaryDocPerson.NationalNo
                    //    )
                    //{
                    //    isInquiryOwnerChanged = false;
                    //}
                    //else if (inquiryPersonFullName.GetStandardFarsiString() == theOneRestrictedOwnershipDoc.TheONotaryDocPerson.FullName.GetStandardFarsiString())
                    //{
                    //    isInquiryOwnerChanged = false;
                    //}

                    //if (!isInquiryOwnerChanged)
                    //{
                    //    //InquiryOwner must have some share in the current Document (Transfer should not be done entirely!)
                    //    continue;
                    //}

                    #endregion

                    EstateInquiryPerson theMainESTOwner = null;
                    bool ownerHasChanged = await this.IsOwnerChanged(theOneRestrictedOwnershipDoc, cancellationToken, theMainESTOwner, messageRef);
                    if (!ownerHasChanged && !string.IsNullOrWhiteSpace(messageRef))
                        return false;

                    DocumentEstateOwnershipDocument theDeterministicOwnershipDoc = this.FindEquivalantOwnershipDocInDeterministicReq(theDeterministicRegisterServiceReqs, theOneRestrictedOwnershipDoc);
                    if (theDeterministicOwnershipDoc == null)
                    {
                        if (ownerHasChanged)
                        {
                            messageRef =
                                "مالک مستند تغییر کرده است و همچنین این مالکیت در هیچ سند قطعی معادلی یافت نشد." +
                                System.Environment.NewLine +
                                System.Environment.NewLine +
                                "جزئیات خطا: " +
                                System.Environment.NewLine +
                                " - " + theOneRestrictedOwnershipDoc.DocumentEstate.Document.DocumentType.CaseTitle + " : " + theOneRestrictedOwnershipDoc.DocumentEstate.RegCaseText() +
                                System.Environment.NewLine +
                                " - مستند مالکیت با شماره ردیف  " + theOneRestrictedOwnershipDoc.RowNo + " : " + theOneRestrictedOwnershipDoc.OwnershipDocTitle() +
                                System.Environment.NewLine +
                                System.Environment.NewLine +
                                System.Environment.NewLine +
                                "** لطفاً مالک مستند ذکر شده را، " + theMainESTOwner.Name + " تعیین نموده و سپس سهم مربوط به این مستند را مجدداً در بخش سهم بندی وارد نمایید. **";

                            return false;
                        }
                        else
                        {
                            //اگر مالک مستند تغییر نکرده است و مطابق استعلام می باشد، لازم نیست با سند قطعی تطبیق داده شود. 
                            continue;
                        }
                    }


                    //if (theDeterministicOwnershipDoc.TheONotaryRegCase.IsAttachment == Rad.CMS.Enumerations.YesNo.Yes)
                    //{
                    //    message = 
                    //        "لطفاً برای تنظیم این سند از استعلام جدید استفاده نمایید." + 
                    //        System.Environment.NewLine + 
                    //        System.Environment.NewLine + 
                    //        "سند قطعی مرتبط با این سند از نوع انتقال منضم بوده و لازم است استعلام جدیدی برای پرونده جاری مورد استفاده قرار گیرد.";


                    //    return false;
                    //}

                    if (!theDeterministicOwnershipDoc.DocumentEstateQuotaDetails.Any())
                    {
                        messageRef =
                            "در سند قطعی مربوطه سهم های هر خریدار از فروشنده تعریف نشده است." +
                            System.Environment.NewLine +
                            System.Environment.NewLine +
                            "جزئیات خطا برای سند قطعی :" +
                            System.Environment.NewLine +
                            " - شماره پرونده سند قطعی در حال بررسی : " + theDeterministicOwnershipDoc.DocumentEstate.Document.RequestNo +
                            System.Environment.NewLine +
                            " - " + theDeterministicOwnershipDoc.DocumentEstate.Document.DocumentType.CaseTitle + " : " + theDeterministicOwnershipDoc.DocumentEstate.RegCaseText() +
                            System.Environment.NewLine +
                            " - مستند مالکیت با شماره ردیف  " + theDeterministicOwnershipDoc.RowNo + " : " + theDeterministicOwnershipDoc.OwnershipDocTitle() +
                            System.Environment.NewLine +
                            System.Environment.NewLine +
                            " لطفاً سهم بندی های سند قطعی را انجام داده سپس سند رهنی را تنظیم نمایید.";

                        return false;
                    }

                    bool isCurrentPersonDefinedAsBuyerInDeterministicReq = false;
                    #region IsPerson Defined As Buyer In Detterministic ----- Is Selling Quotas Compatible To Deterministic Qutoas
                    foreach (DocumentEstateQuotaDetail theOneDeterQuota in theDeterministicOwnershipDoc.DocumentEstateQuotaDetails)
                    {
                        if (!string.IsNullOrWhiteSpace(theOneDeterQuota.DocumentPersonBuyer.NationalNo) && !string.IsNullOrWhiteSpace(theOneRestrictedOwnershipDoc.DocumentPerson.NationalNo))
                        {
                            if (theOneDeterQuota.DocumentPersonBuyer.NationalNo == theOneRestrictedOwnershipDoc.DocumentPerson.NationalNo)
                            {
                                isCurrentPersonDefinedAsBuyerInDeterministicReq = true;

                                string maxDeterQuotasMessage = null;
                                bool quotasAreValid = this.AreRestrictedQuotasCompatibleToDeterQuotas(theOneDeterQuota, theOneRestrictedOwnershipDoc.DocumentEstateQuotaDetails, ref maxDeterQuotasMessage);
                                if (!quotasAreValid)
                                {
                                    messageRef =
                                        "سهم های مشخص شده در پرونده جاری از سهم های تعیین شده در سند قطعی مربوطه بیشتر می باشد." +
                                        System.Environment.NewLine +
                                        System.Environment.NewLine +
                                        "جزئیات خطا: " +
                                        System.Environment.NewLine +
                                        " - " + theOneRestrictedOwnershipDoc.DocumentEstate.Document.DocumentType.CaseTitle + " : " + theOneRestrictedOwnershipDoc.DocumentEstate.RegCaseText() +
                                        System.Environment.NewLine +
                                        " - مستند مالکیت با شماره ردیف  " + theOneRestrictedOwnershipDoc.RowNo + " : " + theOneRestrictedOwnershipDoc.OwnershipDocTitle() +
                                        System.Environment.NewLine +
                                        System.Environment.NewLine +
                                        " - با توجه به سند قطعی تنظیم شده با شماره پرونده " + theDeterministicOwnershipDoc.DocumentEstate.Document.RequestNo + " و شناسه یکتای " + theDeterministicOwnershipDoc.DocumentEstate.Document.NationalNo +
                                        System.Environment.NewLine +
                                        "سهم های تعریف شده در پرونده جاری باید برابر یا کمتر از سهم های معامله شده در سند قطعی باشند." +
                                        System.Environment.NewLine +
                                        System.Environment.NewLine +
                                        " * " + maxDeterQuotasMessage +
                                        System.Environment.NewLine +
                                        " * " + "لطفاً به منظور سهولت در رفع این خطا از دکمه (ایجاد رکورد مستندات مالکیت بر اساس سند قطعی ) در بخش مستندات، استفاده نمایید.";

                                    return false;
                                }

                                break;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            if (
                                theOneDeterQuota.DocumentPersonBuyer.FullName() == theOneRestrictedOwnershipDoc.DocumentPerson.FullName()
                                //&& theOneDeterQuota.TheBuyer.FatherName == theOneRestrictedOwnershipDoc.TheONotaryDocPerson.FatherName
                                )
                            {
                                isCurrentPersonDefinedAsBuyerInDeterministicReq = true;
                                break;
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    #endregion

                    if (!isCurrentPersonDefinedAsBuyerInDeterministicReq)
                    {
                        bool isDocumentBeingSoldEntirlyByInquiryPerson = this.IsDocumentBeingSoldEntirlyByInquiryPerson(theDeterministicOwnershipDoc, theMainESTOwner);
                        if (!isDocumentBeingSoldEntirlyByInquiryPerson && !ownerHasChanged)
                            return true;
                        else if (isDocumentBeingSoldEntirlyByInquiryPerson && !ownerHasChanged)
                        {
                            messageRef =
                                "سند مالکیت بطور کامل منتقل شده است و شخص " + theOneRestrictedOwnershipDoc.DocumentPerson.FullName() + "، دارای مالکیت نمی باشد. " +
                                System.Environment.NewLine +
                                System.Environment.NewLine +
                                "جزئیات خطا: " +
                                System.Environment.NewLine +
                                " - شماره پرونده سند قطعی مرتبط : " + theDeterministicOwnershipDoc.DocumentEstate.Document.RequestNo +
                                System.Environment.NewLine +
                                " - شناسه یکتا سند قطعی مرتبط : " + theDeterministicOwnershipDoc.DocumentEstate.Document.NationalNo +
                                System.Environment.NewLine +
                                " - مستند مالکیت : " + theOneRestrictedOwnershipDoc.OwnershipDocTitle() +
                                System.Environment.NewLine +
                                System.Environment.NewLine +
                                "لطفاً اطلاعات سهم های وارد شده برای خریداران در سند قطعی و مالکین مستند در سند رهنی را کنترل نمایید.";

                            return false;
                        }
                        else
                        {
                            messageRef =
                            "شخص " + theOneRestrictedOwnershipDoc.DocumentPerson.FullName() +
                            " در سند قطعی مرتبط، با شماره پرونده " + theDeterministicOwnershipDoc.DocumentEstate.Document.RequestNo +
                            " و شناسه یکتای " + theDeterministicOwnershipDoc.DocumentEstate.Document.NationalNo +
                            System.Environment.NewLine +
                            "به عنوان خریدار  " + theOneRestrictedOwnershipDoc.OwnershipDocTitle() + "،" +
                            "تعریف نشده است." +
                            System.Environment.NewLine +
                            "لطفاً اطلاعات سهم های وارد شده برای خریداران در سند قطعی و مالکین مستند در سند رهنی را کنترل نمایید.";

                            return false;
                        }
                    }
                }
            }


            return true;
        }

        private async Task<List<Document>> FindRelatedDeterministicRegServices(Document theCurrentRegisterServiceReq, CancellationToken cancellationToken)
        {
            List<string> eSTEStateIdCollection = null;

            foreach (DocumentInquiry theOneInquiry in theCurrentRegisterServiceReq.DocumentInquiries)
            {
                if (string.IsNullOrEmpty(theOneInquiry.EstateInquiriesId) || theOneInquiry.DocumentInquiryOrganizationId != "1")
                    continue;

                if (eSTEStateIdCollection == null)
                    eSTEStateIdCollection = new List<string>();

                if (!eSTEStateIdCollection.Contains(theOneInquiry.EstateInquiriesId))
                    eSTEStateIdCollection.Add(theOneInquiry.EstateInquiriesId);
            }

            if (!eSTEStateIdCollection.Any())
                return null;

            var foundUsedInquiryRegServicesCollecttion = await _documentRepository.FindRelatedDeterministicRegServices(
                theCurrentRegisterServiceReq.ScriptoriumId, eSTEStateIdCollection,
                Mapper.DeterministicDocumentTypeCodes, theCurrentRegisterServiceReq.Id, cancellationToken);


            //Criteria masterCriteria = new Criteria();
            //masterCriteria.AddEqualTo ( Rad.CMS.NotaryOfficeQuery.ONotaryRegisterServiceReq.ScriptoriumId, theCurrentRegisterServiceReq.ScriptoriumId );
            //masterCriteria.AddIn ( Rad.CMS.NotaryOfficeQuery.ONotaryRegisterServiceReq.TheONotaryRegServiceInquiryList.ESTEstateInquiryId, eSTEStateIdCollection );
            //masterCriteria.AddIn ( Rad.CMS.NotaryOfficeQuery.ONotaryRegisterServiceReq.TheONotaryDocumentType.Code, NotaryOfficeCommons.EstateInquiryManager.Mapper.DeterministicDocumentTypeCodes );
            //masterCriteria.AddNotEqualTo ( Rad.CMS.NotaryOfficeQuery.ONotaryRegisterServiceReq.ObjectId, theCurrentRegisterServiceReq.ObjectId );
            //masterCriteria.AddNotEqualTo ( Rad.CMS.NotaryOfficeQuery.ONotaryRegisterServiceReq.State, Rad.CMS.Enumerations.NotaryRegServiceReqState.CanceledAfterGetCode );
            //masterCriteria.AddNotEqualTo ( Rad.CMS.NotaryOfficeQuery.ONotaryRegisterServiceReq.State, Rad.CMS.Enumerations.NotaryRegServiceReqState.CanceledBeforeGetCode );

            //IONotaryRegisterServiceReqCollection foundUsedInquiryRegServicesCollecttion = Rad.CMS.InstanceBuilder.GetEntityListByCriteria<IONotaryRegisterServiceReq>(masterCriteria) as IONotaryRegisterServiceReqCollection;

            return foundUsedInquiryRegServicesCollecttion;
        }

        private DocumentEstateOwnershipDocument FindEquivalantOwnershipDocInDeterministicReq(ICollection<Document> theDeterministicReqs, DocumentEstateOwnershipDocument theRestrictedOwnershipDoc)
        {
            string ownershipDocInquiryID = theRestrictedOwnershipDoc.EstateInquiriesId.Replace("@Duplicated", "");

            foreach (Document theOneDeterRegServiceReq in theDeterministicReqs)
            {
                foreach (DocumentEstate theOneRegCase in theOneDeterRegServiceReq.DocumentEstates)
                {
                    if (string.IsNullOrWhiteSpace(theOneRegCase.EstateInquiryId))
                        continue;

                    if (!theOneRegCase.EstateInquiryId.Contains(ownershipDocInquiryID))
                        continue;

                    foreach (DocumentEstateOwnershipDocument theOneDeterministicOwnershipDoc in theOneRegCase.DocumentEstateOwnershipDocuments)
                    {
                        if (string.IsNullOrWhiteSpace(theOneDeterministicOwnershipDoc.EstateInquiriesId))
                            continue;

                        if (theOneDeterministicOwnershipDoc.EstateInquiriesId.Contains(ownershipDocInquiryID))
                            return theOneDeterministicOwnershipDoc;
                    }
                }
            }

            return null;
        }



        private async Task<bool> IsOwnerChanged(DocumentEstateOwnershipDocument theOneRestrictedOwnershipDoc, CancellationToken cancellationToken, EstateInquiryPerson theMainPerson, string message)
        {
            EstateInquiryPerson theMainPersonRef = theMainPerson;
            string messageRef = message;
            string parentInquiryID = theOneRestrictedOwnershipDoc.EstateInquiriesId.Replace("@Duplicated", "");

            //string ownershipDocInquiryID = theOneRestrictedOwnershipDoc.ESTEstateInquiryId.Replace("@Duplicated", "");
            EstateInquiry theMainInquiryObject = null;
            bool estInquiryExistsInCollection = false;
            foreach (EstateInquiry theESTInquiry in _theESTEstateInquiryCollection)
            {
                if (theESTInquiry.Id.ToString() == parentInquiryID)
                {
                    estInquiryExistsInCollection = true;
                    break;
                }
            }

            if (!estInquiryExistsInCollection)
            {
                theMainInquiryObject = await _estateInquiryRepository.GetAsync(t => t.Id == Guid.Parse(parentInquiryID), cancellationToken);  //Rad.CMS.InstanceBuilder.GetEntityById<Rad.ssaa.ssaaClass.IESTEstateInquiry> ( parentInquiryID );

                if (theMainInquiryObject == null)
                {
                    messageRef = "خطا در دریافت استعلام مرتبط با " + theOneRestrictedOwnershipDoc.OwnershipDocTitle() + "، هنگام بررسی تطابق سند رهنی با سند قطعی.";
                    return false;
                }
                else
                    _theESTEstateInquiryCollection.Add(theMainInquiryObject);
            }

            foreach (EstateInquiry theOneMainInquiryObject in _theESTEstateInquiryCollection)
            {
                if (theOneMainInquiryObject.Id != Guid.Parse(parentInquiryID))
                    continue;

                theMainPersonRef = (EstateInquiryPerson)theOneMainInquiryObject.EstateInquiryPeople.ElementAt(0);

                string nationalCode = theMainPersonRef.NationalityCode;
                string fullName = theMainPersonRef.Name;

                if (!string.IsNullOrWhiteSpace(theOneRestrictedOwnershipDoc.DocumentPerson.NationalNo) && !string.IsNullOrWhiteSpace(nationalCode))
                {
                    if (nationalCode == theOneRestrictedOwnershipDoc.DocumentPerson.NationalNo)
                        return false;
                }

                if (!string.IsNullOrWhiteSpace(theOneRestrictedOwnershipDoc.DocumentPerson.FullName()) && !string.IsNullOrWhiteSpace(fullName))
                {
                    if (fullName.GetStandardFarsiString() == theOneRestrictedOwnershipDoc.DocumentPerson.FullName().GetStandardFarsiString())
                        return false;

                    if (theOneRestrictedOwnershipDoc.DocumentPerson.PersonType == PersonType.Legal.GetString())
                    {
                        if (fullName.GetStandardFarsiString().Contains(theOneRestrictedOwnershipDoc.DocumentPerson.FullName().GetStandardFarsiString()))
                            return false;

                        if (theOneRestrictedOwnershipDoc.DocumentPerson.FullName().GetStandardFarsiString().Contains(fullName.GetStandardFarsiString()))
                            return false;
                    }
                }
            }

            return true;
        }

        private async Task<bool> AreOwnersEqualToInquiryPersons(Document theCurrentRegisterServiceReq, CancellationToken cancellationToken, string message)
        {
            string messageRef = message;
            foreach (DocumentEstate theOneDeterRegCase in theCurrentRegisterServiceReq.DocumentEstates)
            {
                foreach (DocumentEstateOwnershipDocument theOneOwnershipDoc in theOneDeterRegCase.DocumentEstateOwnershipDocuments)
                {
                    if (string.IsNullOrWhiteSpace(theOneOwnershipDoc.EstateInquiriesId))
                        continue;

                    EstateInquiryPerson theMainESTOwner = null;
                    bool isESTEstateOwnerChanged = await this.IsOwnerChanged(theOneOwnershipDoc, cancellationToken, theMainESTOwner, messageRef);

                    if (!isESTEstateOwnerChanged && !string.IsNullOrWhiteSpace(messageRef))
                        return false;

                    if (!isESTEstateOwnerChanged)
                    {
                        continue;
                    }
                    else
                    {
                        messageRef =
                        "مالک مستند تغییر کرده است و همچنین این مالکیت در هیچ سند قطعی معادلی یافت نشد." +
                        System.Environment.NewLine +
                        System.Environment.NewLine +
                        "جزئیات خطا: " +
                        System.Environment.NewLine +
                        " - " + theCurrentRegisterServiceReq.DocumentType.CaseTitle + " : " + theOneDeterRegCase.RegCaseText() +
                        System.Environment.NewLine +
                        " - مستند مالکیت با شماره ردیف  " + theOneOwnershipDoc.RowNo + " : " + theOneOwnershipDoc.OwnershipDocTitle() +
                        System.Environment.NewLine +
                        System.Environment.NewLine +
                        System.Environment.NewLine +
                        "** لطفاً مالک مستند ذکر شده را، " + theMainESTOwner.Name + " تعیین نموده و سپس سهم مربوط به این مستند را مجدداً در بخش سهم بندی وارد نمایید. **";


                        return false;
                    }
                }
            }

            return true;
        }

        private bool AreRestrictedQuotasCompatibleToDeterQuotas(DocumentEstateQuotaDetail theDeterQuota, ICollection<DocumentEstateQuotaDetail> theRestrictedQutoasCollection, ref string message)
        {
            if (theDeterQuota.SellDetailQuota == null || theDeterQuota.SellDetailQuota == 0 || theDeterQuota.SellTotalQuota == null || theDeterQuota.SellTotalQuota == 0)
                return true;

            decimal[] summarizedBuyingQuotasFraction = null;
            decimal[] summarizedSellingQuotasFraction = null;

            #region TotalBuyingQuotas
          
                summarizedBuyingQuotasFraction = new decimal[2];
                double? value = (double?)theDeterQuota.SellDetailQuota.TrimDoubleValue();
                summarizedBuyingQuotasFraction[0] = (decimal)(value ?? 0.0);
                summarizedBuyingQuotasFraction[1] = (decimal)theDeterQuota.SellTotalQuota;
            
            #endregion

            #region TotalSellingQuotas
            foreach (DocumentEstateQuotaDetail theOneSellingQuota in theRestrictedQutoasCollection)
            {
                //if ( theOneSellingQuota.IsMarkForDelete )
                //    continue;

                if (theOneSellingQuota.SellDetailQuota == null || theOneSellingQuota.SellTotalQuota == null || theOneSellingQuota.SellDetailQuota == 0 || theOneSellingQuota.SellTotalQuota == 0)
                    continue;

                //if (!string.IsNullOrWhiteSpace(theOneSellingQuota.QuotaText))
                //    continue;

                if (summarizedSellingQuotasFraction == null)
                {
                    summarizedSellingQuotasFraction = new decimal[2];
                    double? value1 = (double?)theOneSellingQuota.SellDetailQuota.TrimDoubleValue();

                    summarizedSellingQuotasFraction[0] = (decimal)(value1 ?? 0.0);
                    summarizedSellingQuotasFraction[1] = (decimal)theOneSellingQuota.SellTotalQuota;
                }
                else
                {
                    double? value2 = (double?)theOneSellingQuota.SellDetailQuota.TrimDoubleValue();

                    summarizedSellingQuotasFraction = Mathematics.MakhrajMoshtarak(summarizedSellingQuotasFraction[0], (decimal)(value2 ?? 0.0), summarizedSellingQuotasFraction[1], (decimal)theOneSellingQuota.SellTotalQuota);
                }
            }
            #endregion

            if (
                summarizedSellingQuotasFraction == null ||
                summarizedSellingQuotasFraction[0] * summarizedBuyingQuotasFraction[1] <= summarizedSellingQuotasFraction[1] * summarizedBuyingQuotasFraction[0]
                )
            {
                return true;
            }

            message =
                "- بیشترین میزان سهم مجاز برای استفاده در مستند مذکور به مالکیت  " + theDeterQuota.DocumentPersonBuyer.FullName() + " : " +
                summarizedBuyingQuotasFraction[0] +
                " سهم از " +
                summarizedBuyingQuotasFraction[1] +
            " سهم ";
            return false;
        }

        private bool IsDocumentBeingSoldEntirlyByInquiryPerson(DocumentEstateOwnershipDocument theOneDeterministicOwnershipDoc, EstateInquiryPerson theMainPerson)
        {
            decimal[] totalSellingFraction = null;
            decimal[] totalOwnershipFraction = null;
            string ownershipDocPersonFullName = theOneDeterministicOwnershipDoc.DocumentPerson.FullName();
            string ownershipDocPersonNationalNo = theOneDeterministicOwnershipDoc.DocumentPerson.NationalNo;
            string mainBSTPersonFullName = theMainPerson.Name;
            string mainBSTPersonNationalno = theMainPerson.NationalityCode;

            foreach (DocumentEstateQuotaDetail theOneDeterQuota in theOneDeterministicOwnershipDoc.DocumentEstateQuotaDetails)
            {
                //If Quota Has Text So It's not possible to understand whether the document is sold entirly or not.
                if (!string.IsNullOrWhiteSpace(theOneDeterQuota.QuotaText))
                    return false;

                if (totalSellingFraction == null)
                {

                    totalSellingFraction = new decimal[2];
                    double? value = (double?)theOneDeterQuota.SellDetailQuota.TrimDoubleValue();
                    totalSellingFraction[0] = (decimal)(value ?? 0.0);
                    totalSellingFraction[1] = (decimal)theOneDeterQuota.SellTotalQuota;
                }
                else
                {
                    totalSellingFraction = new decimal[2];
                    double? value = (double?)theOneDeterQuota.SellDetailQuota.TrimDoubleValue();
                    totalSellingFraction = Mathematics.MakhrajMoshtarak(totalSellingFraction[0], (decimal)(value ?? 0.0), totalSellingFraction[1], (decimal)theOneDeterQuota.SellTotalQuota);
                }

                if (
                    totalOwnershipFraction == null &&
                    theOneDeterQuota.OwnershipDetailQuota != null && theOneDeterQuota.OwnershipDetailQuota != 0 &&
                    theOneDeterQuota.OwnershipTotalQuota != null && theOneDeterQuota.OwnershipTotalQuota != 0
                    )
                {
                    totalOwnershipFraction = new decimal[2];
                    double? value = (double?)theOneDeterQuota.OwnershipDetailQuota.TrimDoubleValue();
                    totalOwnershipFraction[0] = (decimal)(value ?? 0.0);
                    totalOwnershipFraction[1] = (decimal)theOneDeterQuota.OwnershipTotalQuota;
                }
            }


            //در حالتی که پاسخ استعلام دارای متن سهم می باشد، مقایسه توسط کل سهم مالکیت درج شده توسط کاربر مورد بررسی قرار می گیرد.
            if (
                !string.IsNullOrWhiteSpace(theMainPerson.ShareText) &&
                (
                theMainPerson.ShareTotal == null || theMainPerson.SharePart == null ||
                theMainPerson.ShareTotal == 0 || theMainPerson.SharePart == 0
                ) &&
                totalOwnershipFraction != null
                )
            {
                if (totalSellingFraction[0] * totalOwnershipFraction[1] == totalSellingFraction[1] * totalOwnershipFraction[0])
                    return true;
                else
                    return false;
            }
            else // در حالتی که پاسخ استعلام دارای جزء و کل سهم می باشد می توان بطور کامل سهم های را با میزان مالکیت مقایسه نمود.
            {
                double? sharePartValue = (double?)theMainPerson.SharePart.TrimDoubleValue();
                decimal safeSharePart = (decimal)(sharePartValue ?? 0.0);

                if (totalSellingFraction[0] * theMainPerson.ShareTotal
                    == totalSellingFraction[1] * safeSharePart)
                {
                    return true;
                }
                else
                    return false;
            }
        }

        #region ValidateInquiries
        private async Task<bool> ValidateInquiries(List<EstateInquiry> inputESTEstateInquiryCollection, CancellationToken cancellationToken, string validationMessage, List<string> sentInquiriesCollection)
        {
            string validationMessageRef = validationMessage;
            List<string> sentInquiriesCollectionRef = sentInquiriesCollection;

            bool overalResult = true;

            if (!inputESTEstateInquiryCollection.Any())
                return true;

            try
            {
                foreach (EstateInquiry theOneInquiry in inputESTEstateInquiryCollection)
                {
                    bool singleResult = true;

                    bool isRestricted = Mapper.IsONotaryDocumentRestrictedType(_theONotaryRegisterServiceReq.DocumentTypeId);

                    ValidateEstateInquiryForDealSummaryQuery validationRequest =
                        new ValidateEstateInquiryForDealSummaryQuery(inputESTEstateInquiryCollection.Select(t => t.Id.ToString()).ToArray(), isRestricted);
                    var response = await _mediator.Send(validationRequest, cancellationToken);

                    if (response.IsSuccess)
                    {
                        singleResult = true;
                        overalResult = singleResult & overalResult;
                        continue;
                    }
                    else
                    {

                        validationMessage = response.Data.ElementAt(0).ErrorMessage;
                        singleResult = false; //singleResult & overalResult;
                        overalResult = false;
                    }





                    ////string dsuTransferTypeID = NotaryOfficeCommons.EstateInquiryManager.Mapper.GetEquivalantDSUTransferTypeID(_theONotaryRegisterServiceReq.TheONotaryDocumentType.Code, ref isRestricted);

                    //// validationRequest.NotaryOfficeId = theOneInquiry.ScriptoriumId;


                    //int? result = response;

                    //if ( result == 1 )
                    //{
                    //    singleResult = true;
                    //    overalResult = singleResult & overalResult;
                    //    continue;
                    //}
                    //else
                    //{
                    //    if ( result == null )
                    //    {
                    //        validationMessage = "استعلام با شماره " + theOneInquiry.InquiryNo + " یافت نشد.";
                    //    }

                    //    //***********بســــــــــــــــیار مهـــــــــــم***********
                    //    //*tip: هنگام اخذ شناسه یکتا حتماً باید عدم اعتبار استعلام بررسی شود.
                    //    //*tip: ولی در وضعیتی که سند در حال تایید نهایی شدن می باشد اعتبار استعلام فقط از طریق تابع (ارسال خلاصه معامله) مورد بررسی قرار می گیرد.
                    //    //*tip: در صورتی که تاریخ امضا پر باشد اعتبار استعلام در این تاریخ مورد بررسی قرار می گیرد.
                    //    if ( result == 0 )
                    //    {
                    //        if ( _theONotaryRegisterServiceReq.State == NotaryRegServiceReqState.FinalPrinted.GetString() )
                    //        {
                    //            singleResult = true;
                    //            overalResult = singleResult & overalResult;
                    //            continue;
                    //        }

                    //        validationMessage = "استعلام با شماره " + theOneInquiry.InquiryNo + " معتبر نمی باشد. " + System.Environment.NewLine + response.Message;

                    //        //if (!string.IsNullOrWhiteSpace(response.Message) && response.Message.GetStandardFarsiString().Contains("خلاصهمعاملهمالکیتبرایایناستعلامثبتشدهاست"))
                    //        //{
                    //        //    if (sentInquiriesCollection == null)
                    //        //        sentInquiriesCollection = new List<string>();

                    //        //    if (!sentInquiriesCollection.Contains(theOneInquiry.ObjectId))
                    //        //        sentInquiriesCollection.Add(theOneInquiry.ObjectId);
                    //        //}
                    //    }

                    //    if ( result == -1 )
                    //    {
                    //        validationMessage = "خطا در اعتبارسنجی استعلام با شماره " + theOneInquiry.InquiryNo + ". لطفاً مجدداً تلاش نمایید" + System.Environment.NewLine + response.Message;
                    //    }

                    //    singleResult = false;
                    //    overalResult = singleResult & overalResult;
                    //    //break;
                    //}
                }

            }
            catch (System.Exception ex)
            {
                overalResult = false;
                //validationMessage = "خطا در ارتباط با سرور اعتبار سنجی استعلام های ملک. لطفاً مجدداً تلاش نمایید.";
            }

            return overalResult;
        }
        #endregion

        #region OwnershipDocVerification
        private bool VerifyOwnershipDocBasedOnInquiries(ref string ownershipDocVerificationMessage)
        {
            bool isVerificationSuccessfully = true;

            foreach (DocumentEstate theOneRegCase in _theONotaryRegisterServiceReq.DocumentEstates)
            {
                bool isVerified = this.VerifyOwnershipDocBasedOnInquiries(theOneRegCase.DocumentEstateOwnershipDocuments, _theESTEstateInquiryCollection, ref ownershipDocVerificationMessage);
                isVerificationSuccessfully = isVerificationSuccessfully & isVerified;

                if (!isVerificationSuccessfully)
                    break;
            }

            return isVerificationSuccessfully;
        }

        private bool VerifyOwnershipDocBasedOnInquiries(ICollection<DocumentEstateOwnershipDocument> theONotaryPersonOwnershipDocCollection, List<EstateInquiry> theESTEstateInquiryCollection, ref string ownershipDocVerificationMessage)
        {
            if (!theONotaryPersonOwnershipDocCollection.Any() || !theESTEstateInquiryCollection.Any())
                return false;

            EstateInquiry theDesiredESTEstateInquiry = null;
            foreach (EstateInquiry theOneESTEstateInquiry in theESTEstateInquiryCollection)
            {
                bool isProccessDone = false;

                foreach (DocumentEstateOwnershipDocument theOneOwnershipDoc in theONotaryPersonOwnershipDocCollection)
                {
                    if (!string.IsNullOrWhiteSpace(theOneOwnershipDoc.EstateInquiriesId) && theOneESTEstateInquiry?.Id != null && theOneOwnershipDoc.EstateInquiriesId.Contains(theOneESTEstateInquiry.Id.ToString()))

                        if (theOneOwnershipDoc.EstateInquiriesId.Contains(theOneESTEstateInquiry.Id.ToString()))
                        {
                            theDesiredESTEstateInquiry = theOneESTEstateInquiry;
                            isProccessDone = true;
                            break;
                        }
                }

                if (isProccessDone)
                    break;
            }

            bool isVerified = this.VerifyOwnershipDocBasedOnInquiries(theONotaryPersonOwnershipDocCollection, theDesiredESTEstateInquiry, ref ownershipDocVerificationMessage);
            return isVerified;
        }

        private bool VerifyOwnershipDocBasedOnInquiries(ICollection<DocumentEstateOwnershipDocument> theONotaryPersonOwnershipDocCollection, EstateInquiry theESTEstateInquiry, ref string ownershipDocVerificationMessage)
        {
            bool isVerificationSuccefully = false;

            DocumentEstateOwnershipDocument theOwnershipDoc = this.FindEquivalantPersonOwnershipDoc(theONotaryPersonOwnershipDocCollection, theESTEstateInquiry, ref ownershipDocVerificationMessage);
            if (theOwnershipDoc == null)
                return false;

            isVerificationSuccefully = this.VerifyOwnershipDocBasedOnInquiries(theOwnershipDoc, theESTEstateInquiry, ref ownershipDocVerificationMessage);
            return isVerificationSuccefully;
        }

        private bool VerifyOwnershipDocBasedOnInquiries(DocumentEstateOwnershipDocument theONotaryPersonOwnershipDoc, EstateInquiry theESTEstateInquiry, ref string ownershipDocVerificationMessage)
        {
            bool isVerificationSuccessfully = true;

            if (theONotaryPersonOwnershipDoc.EstateSabtNo != theESTEstateInquiry.RegisterNo)
            {
                ownershipDocVerificationMessage = "شماره ثبت ذکر شده در پرونده جاری با شماره ثبت مندرج در استعلام انتخاب شده مغایرت دارد.";
                return false;
            }

            if (theONotaryPersonOwnershipDoc.EstateDocumentNo != theESTEstateInquiry.DocPrintNo)
            {
                ownershipDocVerificationMessage = "شماره چاپی سند ذکر شده در پرونده جاری با شماره چاپی سند مندرج در استعلام انتخاب شده مغایرت دارد.";
                return false;
            }

            if (theONotaryPersonOwnershipDoc.EstateSeridaftarId != theESTEstateInquiry.EstateSeridaftarId)
            {
                ownershipDocVerificationMessage = "سری دفتر ذکر شده در پرونده جاری با سری دفتر مندرج در استعلام انتخاب شده مغایرت دارد.";
                return false;
            }

            if (theONotaryPersonOwnershipDoc.EstateBookNo != theESTEstateInquiry.EstateNoteNo)
            {
                ownershipDocVerificationMessage = "شماره دفتر ذکر شده در پرونده جاری با شماره دفتر مندرج در استعلام انتخاب شده مغایرت دارد.";
                return false;
            }

            if (theONotaryPersonOwnershipDoc.EstateBookPageNo != theESTEstateInquiry.PageNo)
            {
                ownershipDocVerificationMessage = "شماره صفحه ذکر شده در پرونده جاری با شماره صفحه مندرج در استعلام انتخاب شده مغایرت دارد.";
                return false;
            }

            //if (theONotaryPersonOwnershipDoc.MortgageText != theESTEstateInquiry.HasMortgage)
            //{
            //    if (
            //        theONotaryPersonOwnershipDoc.MortgageText == null && theESTEstateInquiry.HasMortgage == "ندارد" ||
            //        theONotaryPersonOwnershipDoc.MortgageText == "ندارد" && theESTEstateInquiry.HasMortgage == null
            //        )
            //        return true;

            //    ownershipDocVerificationMessage = "متن رهن ذکر شده در پرونده جاری با متن رهن مندرج در استعلام انتخاب شده مغایرت دارد.";
            //    return false;
            //}

            return isVerificationSuccessfully;
        }

        private DocumentEstateOwnershipDocument FindEquivalantPersonOwnershipDoc(ICollection<DocumentEstateOwnershipDocument> theONotaryPersonOwnershipDocCollection, EstateInquiry theESTEstateInquiry, ref string ownershipDocVerificationMessage)
        {
            if (!theONotaryPersonOwnershipDocCollection.Any())
            {
                ownershipDocVerificationMessage = "مستند مالکیت معادل استعلام انتخاب شده در پرونده جاری یافت نشد.";
                return null;
            }

            foreach (DocumentEstateOwnershipDocument theOneONotaryPersonOwnershipDoc in theONotaryPersonOwnershipDocCollection)
            {
                if (!string.IsNullOrWhiteSpace(theOneONotaryPersonOwnershipDoc.EstateInquiriesId))
                    if (theOneONotaryPersonOwnershipDoc.EstateInquiriesId.Contains(theESTEstateInquiry.Id.ToString()))
                        return theOneONotaryPersonOwnershipDoc;
            }

            ownershipDocVerificationMessage = "مستند مالکیت معادل استعلام انتخاب شده در پرونده جاری یافت نشد.";
            return null;
        }
        #endregion

        #region RegCaseVerification
        private bool VerifyRegCasesBasedOnInquiries(ref string regCaseValidationMessage)
        {
            bool isVerificationSuccessfully = true;

            isVerificationSuccessfully = this.VerifyRegCasesBasedOnInquiries(_theONotaryRegisterServiceReq.DocumentEstates, _theESTEstateInquiryCollection, ref regCaseValidationMessage);

            return isVerificationSuccessfully;
        }

        private bool VerifyRegCasesBasedOnInquiries(ICollection<DocumentEstate> theRegCaseCollection, List<EstateInquiry> theESTEstateInquiryCollection, ref string regCaseValidationMessage)
        {
            bool isVerificationSuccessfully = true;

            if (!theRegCaseCollection.Any() || !theESTEstateInquiryCollection.Any())
                return true;

            foreach (EstateInquiry theOneESTEstateInquiry in theESTEstateInquiryCollection)
            {
                bool isVerified = this.VerifyRegCasesBasedOnInquiries(theRegCaseCollection, theOneESTEstateInquiry, ref regCaseValidationMessage);

                isVerificationSuccessfully = isVerified & isVerificationSuccessfully;
                if (!isVerificationSuccessfully)
                    break;
            }
            return isVerificationSuccessfully;
        }

        private bool VerifyRegCasesBasedOnInquiries(ICollection<DocumentEstate> theRegCaseCollection, EstateInquiry theESTEstateInquiry, ref string regCaseValidationMessage)
        {
            bool isVerificationSuccessfully = true;

            if (!theRegCaseCollection.Any())
                return true;

            DocumentEstate theOneRegCase = this.FindEquivalantRegCase(theRegCaseCollection, theESTEstateInquiry, ref regCaseValidationMessage);
            if (theOneRegCase == null)
                return false;

            isVerificationSuccessfully = this.VerifyRegCasesBasedOnInquiries(theOneRegCase, theESTEstateInquiry, ref regCaseValidationMessage);
            return isVerificationSuccessfully;
        }

        private bool VerifyRegCasesBasedOnInquiries(DocumentEstate theOneRegCase, EstateInquiry theESTEstateInquiry, ref string regCaseValidationMessage)
        {
            bool isVerificationSuccessfully = true;

            if (theOneRegCase.Area != (decimal?)theESTEstateInquiry.Area)
            {
                regCaseValidationMessage =
                    "مساحت ذکر شده در سند با مساحت مندرج در استعلام مغایرت دارد." +
                    System.Environment.NewLine +
                    System.Environment.NewLine +
                    " - مساحت مندرج در استعلام : " + theESTEstateInquiry.Area +
                    System.Environment.NewLine +
                    " - مساحت مندرج در پرونده : " + theOneRegCase.Area;

                return false;
            }

            if (theOneRegCase.BasicPlaque != theESTEstateInquiry.Basic)
            {
                regCaseValidationMessage = "پلاک اصلی ذکر شده در سند با پلاک اصلی مندرج در استعلام مغایرت دارد.";
                return false;
            }

            if (theOneRegCase.SecondaryPlaque != theESTEstateInquiry.Secondary)
            {
                regCaseValidationMessage = "پلاک فرعی ذکر شده در سند با پلاک فرعی مندرج در استعلام مغایرت دارد.";
                return false;
            }

            //if (theOneRegCase.GeoLocationId != theESTEstateInquiry.GeoLocationId)
            //{
            //    regCaseValidationMessage = "شهر ذکر شده در سند با شهر مندرج در استعلام مغایرت دارد.";
            //    return false;
            //}

            if (theOneRegCase.EstateSectionId != theESTEstateInquiry.EstateSectionId)
            {
                regCaseValidationMessage = "بخش ذکر شده در سند با بخش مندرج در استعلام مغایرت دارد.";
                return false;
            }

            if (theOneRegCase.EstateSubsectionId != theESTEstateInquiry.EstateSubsectionId)
            {
                regCaseValidationMessage = "ناحیه ذکر شده در سند با ناحیه مندرج در استعلام مغایرت دارد.";
                return false;
            }
            return isVerificationSuccessfully;
        }

        private DocumentEstate FindEquivalantRegCase(ICollection<DocumentEstate> theRegCaseCollection, EstateInquiry theESTEstateInquiry, ref string regCaseValidationMessage)
        {
            if (!theRegCaseCollection.Any())
            {
                regCaseValidationMessage = "مورد معامله متناظر با استعلام های انتخاب شده در پرونده جاری یافت نشد.";
                return null;
            }

            foreach (DocumentEstate theOneRegCase in theRegCaseCollection)
            {
                if (!string.IsNullOrWhiteSpace(theOneRegCase.EstateInquiryId))
                    if (theOneRegCase.EstateInquiryId.Contains(theESTEstateInquiry.Id.ToString()))
                        return theOneRegCase;

                //In Order to return registerCase if regcase couldn't be found by IDs
                if ((theOneRegCase.BasicPlaque == theESTEstateInquiry.Basic) && (theOneRegCase.SecondaryPlaque == theESTEstateInquiry.Secondary))
                    return theOneRegCase;
            }

            regCaseValidationMessage = "مورد معامله متناظر با استعلام های انتخاب شده در پرونده جاری یافت نشد.";
            return null;
        }
        #endregion

        #region PersonsVerification
        private async Task<bool> VerifyPersonsBasedOnInquiries(string verificationMessage, CancellationToken cancellationToken)
        {
            string verificationMessageRef = verificationMessage;
            bool isPersonVerifiedSuccessfully = await this.VerifyOwnerPersonsBasedOnInquiries(_theONotaryRegisterServiceReq, _theESTEstateInquiryCollection, cancellationToken, verificationMessageRef);

            return isPersonVerifiedSuccessfully;
        }

        //private bool VerifyPersonsBasedOnInquiries(IONotaryDocPersonCollection theDocPersonCollection, IESTEstateInquiryCollection theESTEstateInquiryCollection, ref string personValidationMessage)
        //{
        //    bool personVerificationResult = true;

        //    if (!theESTEstateInquiryCollection.CollectionHasElement() || !theDocPersonCollection.CollectionHasElement())
        //        return true;

        //    foreach (IESTEstateInquiry theOneIESTEstateInquiry in theESTEstateInquiryCollection)
        //    {
        //        if (!theOneIESTEstateInquiry.TheBSTPersonList.CollectionHasElement())
        //            continue;

        //        bool isPersonInfoVerified = this.VerifyPersonsBasedOnInquiries(theDocPersonCollection, theOneIESTEstateInquiry, ref personValidationMessage);

        //        personVerificationResult = isPersonInfoVerified & personVerificationResult;

        //        if (!personVerificationResult)
        //            break;
        //    }

        //    return personVerificationResult;
        //}

        //private bool VerifyPersonsBasedOnInquiries(IONotaryDocPersonCollection theDocPersonCollection, IESTEstateInquiry theESTEstateInquiry, ref string personValidationMessage)
        //{
        //    IBSTPerson theBSTPerson = theESTEstateInquiry.TheBSTPersonList[0] as IBSTPerson;
        //    bool isVerifiedSuccessfully = false;

        //    if (theBSTPerson == null)
        //    {
        //        personValidationMessage = "خطای سیستمی در تبدیل اشخاص استعلام به اشخاص پرونده!";
        //        return false;
        //    }

        //    isVerifiedSuccessfully = this.VerifyPersonsBasedOnInquiries(theDocPersonCollection, theBSTPerson, ref personValidationMessage);

        //    return isVerifiedSuccessfully;
        //}

        //private bool VerifyPersonsBasedOnInquiries(IONotaryDocPersonCollection theDocPersonCollection, IBSTPerson theBSTPerson, ref string personValidationMessage)
        //{
        //    bool isVerifiedSuccessfully = false;

        //    IONotaryDocPerson theEquivalantDocPerson = this.FindEquivalanteDocPerson(theDocPersonCollection, theBSTPerson);

        //    if (theEquivalantDocPerson == null)
        //    {
        //        personValidationMessage = "شخص درج شده در استعلام شماره " + theBSTPerson.TheESTEstateInquiry.EnquiryNo + " در پرونده جاری وجود ندارد. ";
        //        return false;
        //    }

        //    isVerifiedSuccessfully = this.VerifyPersonsBasedOnInquiries(theEquivalantDocPerson, theBSTPerson, ref personValidationMessage);

        //    return isVerifiedSuccessfully;
        //}

        private bool VerifyPersonsBasedOnInquiries(DocumentPerson theOneDocPerson, EstateInquiryPerson theBSTPerson, ref string personValidationMessage)
        {
            //Restricted Docs Should Be Verified By Both Deterministic And Restricted Docs
            if (
                (string.IsNullOrWhiteSpace(theOneDocPerson.NationalNo) || string.IsNullOrWhiteSpace(theBSTPerson.NationalityCode)) &&
                theOneDocPerson.Document.IsBasedJudgment == YesNo.Yes.GetString()
                )
                return true;

            if (!string.IsNullOrWhiteSpace(theOneDocPerson.NationalNo) && !string.IsNullOrWhiteSpace(theBSTPerson.NationalityCode))
            {
                if (theOneDocPerson.NationalNo == theBSTPerson.NationalityCode)
                    return true;

                if (theOneDocPerson.NationalNo != theBSTPerson.NationalityCode && theOneDocPerson.PersonType == PersonType.NaturalPerson.GetString())
                {

                    personValidationMessage =
                                              "شماره شناسه ملی شخص ذکر شده در استعلام با شماره شناسه ملی شخص درج شده در پرونده جاری مغایرت دارد. " +
                                              System.Environment.NewLine +
                                              "لطفاً از شماره ملی ذکر شده در پاسخ استعلام استفاده نمایید." +
                                              System.Environment.NewLine +
                                              System.Environment.NewLine +
                                              "* شماره ملی شخص در پرونده جاری : " + theOneDocPerson.NationalNo +
                                              System.Environment.NewLine +
                                              "* شماره ملی شخص در پاسخ استعلام : " + theBSTPerson.NationalityCode;
                    return false;
                }
            }

            if (!string.IsNullOrWhiteSpace(theOneDocPerson.FullName()) && !string.IsNullOrWhiteSpace(theBSTPerson.Name))
            {
                if (theOneDocPerson.FullName().GetStandardFarsiString() == theBSTPerson.Name.GetStandardFarsiString())
                {
                    if (theOneDocPerson.PersonType == PersonType.Legal.GetString() || theOneDocPerson.IsIranian == YesNo.No.GetString())
                        return true;
                }
                else if (theBSTPerson.Name.GetStandardFarsiString().Contains(theOneDocPerson.FullName().GetStandardFarsiString()))
                {
                    if (theOneDocPerson.PersonType == PersonType.Legal.GetString())
                        return true;
                }
                else if (theOneDocPerson.FullName().GetStandardFarsiString().Contains(theBSTPerson.Name.GetStandardFarsiString()))
                {
                    if (theOneDocPerson.PersonType == PersonType.Legal.GetString())
                        return true;
                }
                else
                {
                    personValidationMessage =
                                              "نام شخص ذکر شده در پاسخ استعلام با نام ذکر شده در پرونده جاری تطابق ندارد." +
                                              System.Environment.NewLine +
                                              "لطفاً از نام ذکر شده در پاسخ استعلام استفاده نمایید." +
                                              System.Environment.NewLine +
                                              System.Environment.NewLine +
                                              "* نام شخص در پرونده جاری : " + theOneDocPerson.FullName +
                                              System.Environment.NewLine +
                                              "* نام شخص در پاسخ استعلام : " + theBSTPerson.Name;
                    return false;
                }
            }
            return false;
        }

        private DocumentPerson FindEquivalanteDocPerson(ICollection<DocumentPerson> docPersonCollection, EstateInquiryPerson theOneBSTPerson)
        {
            foreach (DocumentPerson theOneDocPerson in docPersonCollection)
            {
                //Whether docPerson could be found using NationalNo
                if (theOneDocPerson.NationalNo == theOneBSTPerson.NationalityCode)
                    return theOneDocPerson;

                //Whether docPerson could be found using FullName (For Inquiries Based On Executions)
                if (theOneDocPerson.FullName().GetStandardFarsiString() == theOneBSTPerson.Name.GetStandardFarsiString())
                    return theOneDocPerson;

                //Whether docPerson could be found using ESTEstateInquiryID
                if (!string.IsNullOrWhiteSpace(theOneDocPerson.EstateInquiryId))
                    if (theOneDocPerson.EstateInquiryId.Contains(theOneBSTPerson.EstateInquiryId.ToString()))
                        return theOneDocPerson;
            }

            return null;
        }

        private async Task<bool> VerifyOwnerPersonsBasedOnInquiries(Document theRegisterServiceReq, ICollection<EstateInquiry> theESTEstateInquiryCollection, CancellationToken cancellationToken, string personValidationMessage)
        {
            string personValidationMessageRef = personValidationMessage;

            if (!theESTEstateInquiryCollection.Any())
            {
                personValidationMessageRef = "هیچ استعلامی در سند یافت نشد.";
                return false;
            }

            if (!theRegisterServiceReq.DocumentEstates.Any())
            {
                personValidationMessageRef = "هیچ مورد معامله ای در سند یافت نشد.";
                return false;
            }

            foreach (DocumentEstate theOneRegCase in theRegisterServiceReq.DocumentEstates)
            {
                if (string.IsNullOrWhiteSpace(theOneRegCase.EstateInquiryId))
                    continue;

                if (!theOneRegCase.DocumentEstateOwnershipDocuments.Any())
                    continue;

                foreach (DocumentEstateOwnershipDocument theOneOwnershipDoc in theOneRegCase.DocumentEstateOwnershipDocuments)
                {
                    if (string.IsNullOrWhiteSpace(theOneOwnershipDoc.EstateInquiriesId))
                        continue;

                    EstateInquiry theEquivalantInquiry = await this.FindEquivalantESTEstateInquiry(theESTEstateInquiryCollection, theOneOwnershipDoc.EstateInquiriesId);
                    if (theEquivalantInquiry == null)
                    {
                        personValidationMessageRef = "استعلام معادل مستند موجود در سند یافت نشد. تطبیبق مالکین مستندات امکان پذیر نمی باشد.";
                        return false;
                    }

                    EstateInquiryPerson theBSTPerson = theEquivalantInquiry.EstateInquiryPeople.ElementAt(0);
                    if (theBSTPerson == null)
                    {
                        personValidationMessageRef = "خطای سیستمی در تبدیل اشخاص استعلام به اشخاص پرونده!";
                        return false;
                    }

                    bool isRestricted = Mapper.IsONotaryDocumentRestrictedType(theRegisterServiceReq.DocumentTypeId); //اسناد رهنی

                    bool isVerificationSucceeded = this.VerifyPersonsBasedOnInquiries(theOneOwnershipDoc.DocumentPerson, theBSTPerson, ref personValidationMessageRef);
                    if (isVerificationSucceeded)
                        return true;
                    else
                    {
                        if (!isRestricted) //در اسناد قطعی مالکین مستند باید با استعلام تطابق داشته باشند.
                        {
                            personValidationMessageRef = "مشخصات شخص " +
                                                      theOneOwnershipDoc.DocumentPerson.FullName() + "،" +
                                                      " با مشخصات شخص مالک استعلام تطابق ندارد." +
                                                      System.Environment.NewLine +
                                                      System.Environment.NewLine +
                                                      "سند مورد بررسی : " +
                                                      System.Environment.NewLine +
                                                      " - " + theOneOwnershipDoc.OwnershipDocTitle +
                                                      System.Environment.NewLine +
                                                      System.Environment.NewLine +
                                                      personValidationMessageRef;

                            return false;
                        }

                        //Do Deeper Verification Based On Deterministic-RegisterServiceReq:
                        #region OldVersion
                        //string estestateInquiry = theOneOwnershipDoc.ESTEstateInquiryId;
                        //if (string.IsNullOrWhiteSpace(estestateInquiry))
                        //    continue;

                        //if (estestateInquiry.Contains("@Duplicated"))
                        //    estestateInquiry = estestateInquiry.Replace("@Duplicated", "");

                        //string[] estestateInquiryIDCollection = null;
                        //if (estestateInquiry.Contains(','))
                        //    estestateInquiryIDCollection = estestateInquiry.Split(',');
                        //else
                        //    estestateInquiryIDCollection = new string[] { estestateInquiry };


                        //Criteria mainDeterministicCriteria = new Criteria();
                        //mainDeterministicCriteria.AddIn(Rad.CMS.NotaryOfficeQuery.ONotaryRegServiceInquiry.ESTEstateInquiryId, estestateInquiryIDCollection);

                        //Criteria stateCriteria = new Criteria();
                        //stateCriteria.AddNotEqualTo(Rad.CMS.NotaryOfficeQuery.ONotaryRegServiceInquiry.TheONotaryRegisterServiceReq.State, Rad.CMS.Enumerations.NotaryRegServiceReqState.CanceledAfterGetCode);
                        //stateCriteria.AddNotEqualTo(Rad.CMS.NotaryOfficeQuery.ONotaryRegServiceInquiry.TheONotaryRegisterServiceReq.State, Rad.CMS.Enumerations.NotaryRegServiceReqState.CanceledBeforeGetCode);
                        //stateCriteria.AddNotEqualTo(Rad.CMS.NotaryOfficeQuery.ONotaryRegServiceInquiry.TheONotaryRegisterServiceReq.State, Rad.CMS.Enumerations.NotaryRegServiceReqState.None);

                        //Criteria typeCriteria = new Criteria();
                        //typeCriteria.AddEqualTo(Rad.CMS.NotaryOfficeQuery.ONotaryRegServiceInquiry.TheONotaryRegisterServiceReq.TheONotaryDocumentType.Code, "211"); //صلح اموال غیر منقول
                        //typeCriteria.AddOrEqualTo(Rad.CMS.NotaryOfficeQuery.ONotaryRegServiceInquiry.TheONotaryRegisterServiceReq.TheONotaryDocumentType.Code, "212"); //صلح اموال غیر منقول با حق استرداد
                        //typeCriteria.AddOrEqualTo(Rad.CMS.NotaryOfficeQuery.ONotaryRegServiceInquiry.TheONotaryRegisterServiceReq.TheONotaryDocumentType.Code, "111"); //قطعی اموال غیر منقول 
                        //typeCriteria.AddOrEqualTo(Rad.CMS.NotaryOfficeQuery.ONotaryRegServiceInquiry.TheONotaryRegisterServiceReq.TheONotaryDocumentType.Code, "112"); // قطعی اموال غیر منقول با حق استرداد

                        //mainDeterministicCriteria.AddAndCriteria(stateCriteria);
                        //mainDeterministicCriteria.AddAndCriteria(typeCriteria);

                        //IONotaryRegServiceInquiryCollection fetchedInquiriesCollection = Rad.CMS.InstanceBuilder.GetEntityListByCriteria<IONotaryRegServiceInquiry>(mainDeterministicCriteria) as IONotaryRegServiceInquiryCollection;

                        //if (!fetchedInquiriesCollection.CollectionHasElement())
                        //{
                        //    //سند قطعی برای سند رهنی یافت نشده است.
                        //    //بنابراین مجاز به ادامه می باشد.

                        //    return true;
                        //}


                        //List<IONotaryRegisterServiceReq> deterministicRegisterServicesCollection = new List<IONotaryRegisterServiceReq>();
                        //foreach (IONotaryRegServiceInquiry theOneInquiry in fetchedInquiriesCollection)
                        //{
                        //    if (!deterministicRegisterServicesCollection.Contains(theOneInquiry.TheONotaryRegisterServiceReq))
                        //        deterministicRegisterServicesCollection.Add(theOneInquiry.TheONotaryRegisterServiceReq);
                        //}
                        #endregion

                        ICollection<Document> deterministicRegisterServicesCollection = await this.FindRelatedDeterministicReqs(theOneOwnershipDoc, cancellationToken);
                        if (deterministicRegisterServicesCollection==null || !deterministicRegisterServicesCollection.Any())
                        {
                            return false;
                        }

                        string nonNationalNoMessages = null;
                        foreach (Document theOneDeterministicRegisterServiceReq in deterministicRegisterServicesCollection)
                        {
                            if (
                               theOneDeterministicRegisterServiceReq.State != NotaryRegServiceReqState.SetNationalDocumentNo.GetString() &&
                               theOneDeterministicRegisterServiceReq.State != NotaryRegServiceReqState.FinalPrinted.GetString() &&
                               theOneDeterministicRegisterServiceReq.State != NotaryRegServiceReqState.Finalized.GetString()
                               )
                            {
                                nonNationalNoMessages += " - " + theOneDeterministicRegisterServiceReq.RequestNo + System.Environment.NewLine;
                                continue;
                            }

                            foreach (DocumentPerson theOneDocPerson in theOneDeterministicRegisterServiceReq.DocumentPeople)
                            {
                                //حتماً باید در سند قطعی به عنوان خریدار تعریف شده باشد

                                string sTheDocPerson = theOneDocPerson.FullName + "NationalNo: " + theOneDocPerson.NationalNo + "-ObjectID: " + theOneDocPerson.Id;
                                string sTheOwnershipDocPerson = theOneOwnershipDoc.DocumentPerson.FullName() + "NationalNo: " + theOneOwnershipDoc.DocumentPerson.NationalNo + "-ObjectID: " + theOneOwnershipDoc.DocumentPerson.Id;

                                if (theOneDocPerson.DocumentPersonType == null)
                                    continue;

                                if (theOneDocPerson.DocumentPersonType.IsOwner != YesNo.No.GetString())
                                    continue;

                                if (!string.IsNullOrWhiteSpace(theOneDocPerson.NationalNo) && !string.IsNullOrWhiteSpace(theOneOwnershipDoc.DocumentPerson.NationalNo))
                                    if (theOneDocPerson.NationalNo == theOneOwnershipDoc.DocumentPerson.NationalNo)
                                    {
                                        personValidationMessageRef = null;
                                        return true;
                                    }

                                if (!string.IsNullOrWhiteSpace(theOneDocPerson.FullName()) && !string.IsNullOrWhiteSpace(theOneOwnershipDoc.DocumentPerson.FullName()))
                                    if (theOneDocPerson.FullName().GetStandardFarsiString() == theOneOwnershipDoc.DocumentPerson.FullName().GetStandardFarsiString())
                                    {
                                        personValidationMessageRef = null;
                                        return true;
                                    }
                            }
                        }

                        string buyerFullName = theOneOwnershipDoc.DocumentPerson.FullName();

                        personValidationMessageRef = buyerFullName + "، در سند قطعی تنظیم شده بر اساس استعلام،به عنوان خریدار سند مالکیت تعیین نشده است.";

                        if (!string.IsNullOrWhiteSpace(nonNationalNoMessages))
                        {
                            personValidationMessageRef +=
                                System.Environment.NewLine +
                                "پرونده های ذکر شده شناسه یکتا نگرفته و برای تطبیق با سند رهنی معتبر نمی باشند : " +
                                System.Environment.NewLine +
                                nonNationalNoMessages;
                        }

                        return false;
                    }
                }
            }
            return false;
        }

        private async Task<EstateInquiry> FindEquivalantESTEstateInquiry(ICollection<EstateInquiry> theESTEstateInquiryCollection, string combinedID)
        {
            foreach (EstateInquiry theOneInquiry in theESTEstateInquiryCollection)
            {
                if (combinedID.Contains(theOneInquiry.Id.ToString()))
                    return theOneInquiry;
            }

            return null;
        }

        private async Task<ICollection<Document>> FindRelatedDeterministicReqs(DocumentEstateOwnershipDocument theOneOwnershipDoc, CancellationToken cancellationToken)
        {
            string estestateInquiry = theOneOwnershipDoc.EstateInquiriesId;
            if (string.IsNullOrWhiteSpace(estestateInquiry))
                return null;

            if (estestateInquiry.Contains("@Duplicated"))
                estestateInquiry = estestateInquiry.Replace("@Duplicated", "");

            string[] estestateInquiryIDCollection = null;
            if (estestateInquiry.Contains(','))
                estestateInquiryIDCollection = estestateInquiry.Split(',');
            else
                estestateInquiryIDCollection = new string[] { estestateInquiry };

            var theDeterministicReqsCollection = await _documentRepository
                .FindRelatedDeterministicRegServices(theOneOwnershipDoc.DocumentEstate.Document.ScriptoriumId,
                    estestateInquiryIDCollection.ToList()
                    , Mapper.DeterministicDocumentTypeCodes
                    , theOneOwnershipDoc.DocumentEstate.Document.Id,
                    cancellationToken);




            //Criteria mainDeterministicCriteria = new Criteria();
            //mainDeterministicCriteria.AddEqualTo ( Rad.CMS.NotaryOfficeQuery.ONotaryRegisterServiceReq.ScriptoriumId, theOneOwnershipDoc.TheONotaryRegCase.TheONotaryRegisterServiceReq.ScriptoriumId );
            //mainDeterministicCriteria.AddIn ( Rad.CMS.NotaryOfficeQuery.ONotaryRegisterServiceReq.TheONotaryRegServiceInquiryList.ESTEstateInquiryId, estestateInquiryIDCollection );
            //mainDeterministicCriteria.AddIn ( Rad.CMS.NotaryOfficeQuery.ONotaryRegisterServiceReq.TheONotaryDocumentType.Code, NotaryOfficeCommons.EstateInquiryManager.Mapper.DeterministicDocumentTypeCodes );

            //mainDeterministicCriteria.AddNotEqualTo ( Rad.CMS.NotaryOfficeQuery.ONotaryRegisterServiceReq.State, Rad.CMS.Enumerations.NotaryRegServiceReqState.CanceledAfterGetCode );
            //mainDeterministicCriteria.AddNotEqualTo ( Rad.CMS.NotaryOfficeQuery.ONotaryRegisterServiceReq.State, Rad.CMS.Enumerations.NotaryRegServiceReqState.CanceledBeforeGetCode );
            //mainDeterministicCriteria.AddNotNull ( Rad.CMS.NotaryOfficeQuery.ONotaryRegisterServiceReq.State );

            //IONotaryRegisterServiceReqCollection theDeterministicReqsCollection = Rad.CMS.InstanceBuilder.GetEntityListByCriteria<IONotaryRegisterServiceReq>(mainDeterministicCriteria) as IONotaryRegisterServiceReqCollection;

            return theDeterministicReqsCollection;
        }
        #endregion

        //private void Test()
        //{
        //    Ojb.Net.Linq.Query<IONotaryRegCase> query = new Ojb.Net.Linq.Query<IONotaryRegCase>();
        //    Ojb.Net.Linq.Query<IONotaryRegCasePersonQuota> query1 = new Ojb.Net.Linq.Query<IONotaryRegCasePersonQuota>();
        //    var q = from a in query
        //            from b in query1
        //            where a.Address == "" && a.Id == b.ONotaryRegCaseId
        //            select new { a.Address, aaa = b.TheBuyer.Address };

        //    var result = q.la();
        //}
    }

}
