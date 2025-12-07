using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Configuration;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RichDomain.Document;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.Utilities.Other;
using DocumentType = Notary.SSAA.BO.Domain.Entities.DocumentType;

namespace Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Estate.Core
{
    public class PersonOwnershipDocsInheritorCore
    {
        private ClientConfiguration _clientConfiguration;
        private IDocumentRepository _documentRepository;
        private IRepository<EstateInquiryPerson> _estateInquiryPersonRepository;
        private IEstateInquiryRepository _estateInquiryRepository;
        private IRepository<DocumentPersonType> _documentPersonTypePersonRepository;
        private readonly IDocumentEstateQuotaDetailRepository _documentEstateQuotaDetailRepository;
        private readonly IDocumentOwnerShipRepository _documentOwnerShipRepository;
        private readonly IDocumentEstateRepository _documentEstateRepository;
        private EstateInquiryEngine _estateInquiryEngine;
        private IUserService _userService;
        private readonly IApplicationContextService _applicationContextService;
        private bool _mainObjectIsDirty = false;
        private List<Document> _theDeterministicReqs = null;
        private Document _theRestRegisterServiceReq = null;
        List<string> _newGeneratedQuotas = null;

        public PersonOwnershipDocsInheritorCore(ClientConfiguration clientConfiguration
            , IDocumentRepository documentRepository, IRepository<EstateInquiryPerson> estateInquiryPersonRepository,
            IRepository<DocumentPersonType> documentPersonTypePersonRepository,
            IEstateInquiryRepository estateInquiryRepository,
            IUserService userService, EstateInquiryEngine estateInquiryEngine,
            IDocumentEstateQuotaDetailRepository documentEstateQuotaDetailRepository,
            IDocumentOwnerShipRepository documentOwnerShipRepository,
            IDocumentEstateRepository documentEstateRepository,
            IApplicationContextService applicationContextService)
        {
            _clientConfiguration = clientConfiguration;
            _documentRepository = documentRepository;
            _estateInquiryPersonRepository = estateInquiryPersonRepository;
            _userService = userService;
            _estateInquiryEngine = estateInquiryEngine;
            _documentPersonTypePersonRepository = documentPersonTypePersonRepository;
            _estateInquiryRepository = estateInquiryRepository;
            _documentEstateQuotaDetailRepository = documentEstateQuotaDetailRepository;
            _documentOwnerShipRepository = documentOwnerShipRepository;
            _applicationContextService = applicationContextService;
            _documentEstateRepository = documentEstateRepository;
        }

        public async Task<(bool, string)> InheritDeterministicOwnershipDocs(string reqNo, string restRegisterServiceReqID, CancellationToken cancellationToken, string message)
        {
            string messageRef = message;
            if (!_clientConfiguration.IsAutoOwnershipGenerationEnabled)
            {
                messageRef = "تولید خودکار مستندات در حال حاضر غیر فعال می باشد.";
                return (false, messageRef);
            }

            _applicationContextService.BeginTransactionAsync(cancellationToken);
            // using ( NotaryOfficeCommons.TransactedOperation innerTransaction = new NotaryOfficeCommons.TransactedOperation ( true ) )

            List<Document> theDeterRegisterServiceReqsCollection = new();
            (theDeterRegisterServiceReqsCollection, messageRef) = await this.GetDeterministicRegisterServiceReq(reqNo, cancellationToken, messageRef);
            _theRestRegisterServiceReq = await _documentRepository.TableNoTracking
                .Include(x => x.DocumentEstates).ThenInclude(x => x.DocumentEstateOwnershipDocuments).ThenInclude(x => x.DocumentPerson)
                .Include(x => x.DocumentEstates).ThenInclude(x => x.DocumentEstateAttachments)
                .Include(x => x.DocumentEstates).ThenInclude(x => x.DocumentEstateType)
                .Include(x => x.DocumentPeople).ThenInclude(x => x.DocumentPersonType)
                .Include(x => x.DocumentEstates).ThenInclude(x => x.DocumentEstateSeparationPieces).ThenInclude(x => x.InverseAnbari)
                .Include(x => x.DocumentEstates).ThenInclude(x => x.DocumentEstateSeparationPieces).ThenInclude(x => x.InverseParking)
                .Include(x => x.DocumentEstates).ThenInclude(x => x.DocumentEstateSeparationPieces).ThenInclude(x => x.DocumentEstateSeparationPieceKind)
                .Include(x => x.DocumentEstates).ThenInclude(x => x.DocumentEstateSeparationPieces).ThenInclude(x => x.EstatePieceType)
                .Include(x => x.DocumentEstates).ThenInclude(x => x.DocumentEstateSeparationPieces).ThenInclude(x => x.DocumentEstateSeparationPiecesQuota)
                .Include(x => x.DocumentEstates).ThenInclude(y => y.DocumentEstateQuota).ThenInclude(y => y.DocumentPerson)
                .Include(x => x.DocumentEstates).ThenInclude(y => y.DocumentEstateQuotaDetails).ThenInclude(y => y.DocumentPersonBuyer)
                .Include(x => x.DocumentEstates).ThenInclude(y => y.DocumentEstateQuotaDetails).ThenInclude(y => y.DocumentPersonSeller)
                .Include(x => x.DocumentInquiries)
                .Include(x => x.DocumentType).Include(x => x.DocumentInfoConfirm)
                .Where(t => t.Id == Guid.Parse(restRegisterServiceReqID)).FirstAsync(cancellationToken);

            Boolean result = theDeterRegisterServiceReqsCollection.Count > 1;
            if (result == true)
            {
                if (string.IsNullOrWhiteSpace(messageRef))
                    messageRef = "اسناد قطعی مورد درخواست شما دریافت نشد.";
                return (false, messageRef);
            }



            if (_theRestRegisterServiceReq == null)
            {
                messageRef = "سند رهنی از سرور دریافت نشد. لطفاً مجدداً تلاش نمایید.";
                return (false, messageRef);
            }


            foreach (Document theOneDeterReq in theDeterRegisterServiceReqsCollection)
            {
                if (
                    theOneDeterReq.State != NotaryRegServiceReqState.Finalized.GetString() &&
                    theOneDeterReq.ClassifyNo != null
                    )
                {
                    bool isDeterDSUSimulationSucceeded;
                    (isDeterDSUSimulationSucceeded, messageRef) = await _estateInquiryEngine.SimulateDSUsSendingProcess(theOneDeterReq, messageRef, cancellationToken);
                    if (!isDeterDSUSimulationSucceeded)
                    {
                        messageRef = "سند قطعی مورد درخواست شما با شماره " + theOneDeterReq.RequestNo + " آماده ارسال خلاصه معامله نمی باشد." +
                                  System.Environment.NewLine +
                                  "لطفاً خطاهای زیر را در سند قطعی رفع نموده و مجدداً تلاش نمایید: " +
                                  System.Environment.NewLine +
                                  messageRef;

                        return (false, messageRef);
                    }
                }
            }

            List<DocumentEstateOwnershipDocument> newGeneratedOwnershipDocsCollection = null;
            _newGeneratedQuotas = new List<string>();

            foreach (Document theOneDeterRegisterServiceReq in theDeterRegisterServiceReqsCollection)
                foreach (DocumentEstate theOneDeterRegCase in theOneDeterRegisterServiceReq.DocumentEstates)
                {
                    if (string.IsNullOrWhiteSpace(theOneDeterRegCase.EstateInquiryId) && !theOneDeterRegCase.DocumentEstateInquiries.Any())
                        continue;

                    DocumentEstate theRestRegCase = this.GetEquivalantRegCase(theOneDeterRegCase, _theRestRegisterServiceReq.DocumentEstates);
                    if (theRestRegCase == null)
                    {
                        messageRef = _theRestRegisterServiceReq.DocumentType.CaseTitle + " معادل در سند رهنی یافت نشد. ";
                        continue;
                    }

                    //theRestRegCase.TheONotaryRegCasePersonQuotaList.DeleteCompletely();
                    foreach (DocumentEstateQuotaDetail theOnePersonQuota in theRestRegCase.DocumentEstateQuotaDetails)
                    {
                        _documentEstateQuotaDetailRepository.Delete(theOnePersonQuota, false);
                    }

                    string regCaseText = theRestRegCase.RegCaseText();

                    //this.CorrectExistingPrimaryOwnerships(ref theRestRegCase, theOneDeterRegisterServiceReq);
                    await this.ResetExistingPrimaryOwnershipsToDefaults(theRestRegCase, cancellationToken);

                    foreach (DocumentEstateOwnershipDocument theOneDeterOwnershipDoc in theOneDeterRegCase.DocumentEstateOwnershipDocuments)
                    {
                        if (string.IsNullOrWhiteSpace(theOneDeterOwnershipDoc.EstateInquiriesId))
                            continue;

                        if (!theOneDeterOwnershipDoc.DocumentEstateQuotaDetails.Any())
                            continue;

                        foreach (DocumentEstateQuotaDetail theOneDeterQuota in theOneDeterOwnershipDoc.DocumentEstateQuotaDetails)

                        {
                            DocumentEstateOwnershipDocument theNewPersonOwnershipDoc;
                            (theNewPersonOwnershipDoc, theRestRegCase, messageRef) = await this.GenerateNewRestOwnershipPackage(theOneDeterQuota, theRestRegCase, messageRef, cancellationToken);

                            if (theNewPersonOwnershipDoc != null)
                            {
                                if (newGeneratedOwnershipDocsCollection == null)
                                    newGeneratedOwnershipDocsCollection = new List<DocumentEstateOwnershipDocument>();

                                newGeneratedOwnershipDocsCollection.Add(theNewPersonOwnershipDoc);
                            }
                        }
                    }
                }

            Boolean isExistNewGeneratedOwnershipDocsCollectionsult = newGeneratedOwnershipDocsCollection?.Count > 0;

            if (isExistNewGeneratedOwnershipDocsCollectionsult == true)
            {
                _documentOwnerShipRepository.AddRange(newGeneratedOwnershipDocsCollection, false);
            }

            foreach (DocumentEstate theOneRestRegCase in _theRestRegisterServiceReq.DocumentEstates)
            {
                foreach (DocumentEstateOwnershipDocument theOneRestPersonOwnershipDoc in theOneRestRegCase.DocumentEstateOwnershipDocuments)
                {
                    if (string.IsNullOrWhiteSpace(theOneRestPersonOwnershipDoc.EstateInquiriesId) || theOneRestPersonOwnershipDoc.EstateInquiriesId.Contains("@Duplicated"))
                        continue;

                    if (!theOneRestPersonOwnershipDoc.DocumentEstateQuotaDetails.Any())
                        await this.GenerateIndividualPersonQuota4InquiryBasedPersons(theOneRestPersonOwnershipDoc, cancellationToken);

                    bool listIsDeleting = true;
                    foreach (DocumentEstateQuotaDetail theOneRestQuota in theOneRestPersonOwnershipDoc.DocumentEstateQuotaDetails)
                    {
                        //if ( !theOneRestQuota.IsMarkForDelete )
                        //{
                        //    listIsDeleting = false;
                        //    break;
                        //}
                    }

                    //if (listIsDeleting)
                        await this.GenerateIndividualPersonQuota4InquiryBasedPersons(theOneRestPersonOwnershipDoc, cancellationToken);
                }
            }


            if (newGeneratedOwnershipDocsCollection != null && newGeneratedOwnershipDocsCollection.Any() || _mainObjectIsDirty)
            {

                this.RemoveUnHealthyQuotas(_theRestRegisterServiceReq);
                await _applicationContextService.SaveChangesAsync(cancellationToken);
                await _applicationContextService.CommitTransactionAsync(cancellationToken);





                //Rad.CMS.OjbBridge.TransactionContext.Current.Commit ();
                return (true, messageRef);
            }
            else
            {
                await _applicationContextService.RollbackTransactionAsync(cancellationToken);

                // Rad.CMS.OjbBridge.TransactionContext.Current.RollBack ();
                return (false, messageRef);
            }

        }
        //========================================================================================================================================
        private void RemoveUnHealthyQuotas(Document theRestrictedReq)
        {
            if (!theRestrictedReq.DocumentEstates.Any())
                return;

            foreach (DocumentEstate theOneRestCase in theRestrictedReq.DocumentEstates)
            {
                if (!theOneRestCase.DocumentEstateQuotaDetails.Any())
                    continue;

                foreach (var theOneQuota in theOneRestCase.DocumentEstateQuotaDetails)
                {
                    if (theOneQuota.DocumentPersonSeller == null || theOneQuota.DocumentEstateOwnershipDocument == null)
                    {
                        _documentEstateQuotaDetailRepository.Delete(theOneQuota, false);

                    }
                    //theOneQuota.MarkForDelete ();
                }
            }
        }

        private async Task<(List<Document>, string)> GetDeterministicRegisterServiceReq(string reqNo, CancellationToken cancellationToken, string message)
        {
            string messageRef = message;
            if (string.IsNullOrWhiteSpace(reqNo))
            {
                messageRef = "شماره پرونده وارد شده صحیح نمی باشد. لطفاً از شماره پرونده معتبر استفاده نمایید.";
                return (null, messageRef);
            }

            string[] splitedReqNos = null;
            if (reqNo.Contains(','))
                splitedReqNos = reqNo.Split(',');
            else if (reqNo.Contains('و'))
                splitedReqNos = reqNo.Split('و');
            else if (reqNo.Contains('-'))
                splitedReqNos = reqNo.Split('-');
            else if (reqNo.Contains('.'))
                splitedReqNos = reqNo.Split('.');
            else if (reqNo.Contains('،'))
                splitedReqNos = reqNo.Split('،');
            else if (reqNo.Contains(' '))
                splitedReqNos = reqNo.Split(' ');
            else if (reqNo.Contains('/'))
                splitedReqNos = reqNo.Split('/');
            else if (reqNo.Contains('\\'))
                splitedReqNos = reqNo.Split('\\');
            else if (reqNo.Contains('_'))
                splitedReqNos = reqNo.Split('_');
            else
                splitedReqNos = new string[] { reqNo };

            var theDeterministicRegisterServiceReqs = await _documentRepository.TableNoTracking

                .Include(x => x.DocumentCosts).ThenInclude(x => x.CostType).Include(x => x.DocumentCostUnchangeds)
                .Include(x => x.DocumentEstates).ThenInclude(x => x.DocumentEstateOwnershipDocuments).ThenInclude(x => x.DocumentPerson)
                .Include(x => x.DocumentEstates).ThenInclude(x => x.DocumentEstateAttachments)
                .Include(x => x.DocumentEstates).ThenInclude(x => x.DocumentEstateSeparationPieces).ThenInclude(x => x.InverseAnbari)
                .Include(x => x.DocumentEstates).ThenInclude(x => x.DocumentEstateSeparationPieces).ThenInclude(x => x.InverseParking)
                .Include(x => x.DocumentEstates).ThenInclude(x => x.DocumentEstateSeparationPieces).ThenInclude(x => x.DocumentEstateSeparationPieceKind)
                .Include(x => x.DocumentEstates).ThenInclude(x => x.DocumentEstateSeparationPieces).ThenInclude(x => x.EstatePieceType)
                .Include(x => x.DocumentEstates).ThenInclude(x => x.DocumentEstateSeparationPieces).ThenInclude(x => x.DocumentEstateSeparationPiecesQuota)
                .Include(x => x.DocumentEstates).ThenInclude(y => y.DocumentEstateQuota).ThenInclude(y => y.DocumentPerson)
                .Include(x => x.DocumentEstates).ThenInclude(y => y.DocumentEstateQuotaDetails).ThenInclude(y => y.DocumentPersonBuyer)
                .Include(x => x.DocumentEstates).ThenInclude(y => y.DocumentEstateQuotaDetails).ThenInclude(y => y.DocumentPersonSeller)
                .Include(x => x.DocumentInquiries).Include(x => x.DocumentType).Include(x => x.DocumentInfoConfirm).Include(x => x.DocumentInfoOther)
                .Where(t => splitedReqNos.Contains(t.RequestNo) &&
            Mapper.DeterministicDocumentTypeCodes.Contains(t.DocumentTypeId) && t.ScriptoriumId == _userService.UserApplicationContext.ScriptoriumInformation.Id).ToListAsync(cancellationToken);



            if (!theDeterministicRegisterServiceReqs.Any())
            {
                messageRef = "سند درخواست شده یافت نشد. لطفاً از صحت شماره پرونده ورودی اطمینان حاصل نمایید.";
                return (null, messageRef);
            }

            List<Document> theDeterministicReqsCollection = null;

            foreach (Document theOneDeterministicReq in theDeterministicRegisterServiceReqs)
            {
                if (string.Compare(theOneDeterministicReq.RequestDate, _clientConfiguration.DSUInitializationDate) < 0)
                {
                    messageRef = "تاریخ تشکیل پرونده قطعی، قبل از راه اندازی نسخه جدید می باشد و قابل استفاده نمی باشد." +
                              System.Environment.NewLine +
                              "برای استفاده از این قابلیت از پرونده های تشکیل شده بعد از تاریخ " + _clientConfiguration.DSUInitializationDate + " استفاده نمایید.";

                    return (null, messageRef);
                }

                switch (theOneDeterministicReq.State)
                {
                    case "2"://NotaryRegServiceReqState.WaitForInquiry:
                    case "0"://NotaryRegServiceReqState.None:
                    case "1"://NotaryRegServiceReqState.Created:
                        messageRef =
                            "سند قطعی مورد درخواست شما در وضعیت قابل قبول نمی باشد." +
                            System.Environment.NewLine +
                            "ابتدا باید هزینه های سند قطعی محاسبه و تایید شده باشد.";

                        return (null, messageRef);
                    case "8"://NotaryRegServiceReqState.CanceledAfterGetCode:
                    case "9"://NotaryRegServiceReqState.CanceledBeforeGetCode:
                        messageRef =
                            "سند قطعی مورد درخواست شما در وضعیت قابل قبول نمی باشد." +
                            System.Environment.NewLine +
                            "اسناد باطل و یا بی اثر شده قابل استناد برای ایجاد رکورد های مستندات نمی باشند.";

                        return (null, messageRef);
                }

                if (theOneDeterministicReq.IsRegistered != YesNo.Yes.GetString())
                {
                    messageRef = "سند قطعی مورد درخواست دارای ملک ثبت شده نمی باشد.";
                    return (null, messageRef);
                }

                if (string.Compare(theOneDeterministicReq.RequestDate, _clientConfiguration.DSUInitializationDate) < 0)
                {
                    messageRef = "سند قطعی مورد درخواست باید بعد از راه اندازی نسخه خلاصه معامله تنظیم شده باشد.";
                    return (null, messageRef);
                }


                if (theDeterministicReqsCollection == null)
                    theDeterministicReqsCollection = new List<Document>();

                if (!theDeterministicReqsCollection.Contains(theOneDeterministicReq))
                    theDeterministicReqsCollection.Add(theOneDeterministicReq);

            }

            _theDeterministicReqs = theDeterministicReqsCollection;

            return (theDeterministicReqsCollection, messageRef);
        }

        private async Task<(DocumentEstateOwnershipDocument, DocumentEstate, string)> GenerateNewRestOwnershipPackage(DocumentEstateQuotaDetail theDeterQuota, DocumentEstate theRestRegCase, string message, CancellationToken cancellationToken)
        {
            DocumentEstate theRestRegCaseRef = theRestRegCase;
            string messageRef = message;

            bool deletePermitted = false;
            DocumentPerson theNewRestDocPerson;
            (theNewRestDocPerson, theRestRegCaseRef, deletePermitted) = await this.GenerateNewRestDocPerson(theDeterQuota.DocumentPersonBuyer, cancellationToken, theRestRegCaseRef, deletePermitted);
            if (theNewRestDocPerson == null)
                return (null, theRestRegCaseRef, messageRef);

            await this.CorrectExistingPrimaryOwnerships(theRestRegCaseRef, theNewRestDocPerson, theDeterQuota.DocumentEstateOwnershipDocument, cancellationToken);

            DocumentEstateOwnershipDocument theExistingRestOwnershipDoc = null;
            bool docExists = this.DoesOwnershipDocExistInRestRegCaseDocs(theNewRestDocPerson, theDeterQuota.DocumentEstateOwnershipDocument, theRestRegCaseRef.DocumentEstateOwnershipDocuments, ref theExistingRestOwnershipDoc);
            if (docExists)
            {
                if (
           deletePermitted &&
           (!string.IsNullOrWhiteSpace(theNewRestDocPerson.EstateInquiryId) || theNewRestDocPerson.DocumentInquiries.Any()) &&
           theNewRestDocPerson.EstateInquiryId?.Contains("@Duplicated") == true
       )
                {
                    theRestRegCaseRef.Document.DocumentPeople.Remove(theNewRestDocPerson);
                }
                // theNewRestDocPerson.MarkForDelete ();

                if (theRestRegCaseRef.IsProportionateQuota != /*Rad.CMS.Enumerations.*/YesNo.Yes.GetString() && theExistingRestOwnershipDoc != null)
                {
                    this.GenerateIndividualPersonQuota(theExistingRestOwnershipDoc, theDeterQuota);
                }

                return (null, theRestRegCaseRef, messageRef);
            }

            DocumentEstateOwnershipDocument theNewOwnershipDoc = this.GenerateIndividualNewRestOwnershipDoc(theDeterQuota.DocumentEstateOwnershipDocument, theNewRestDocPerson, theRestRegCaseRef);
            if (theNewOwnershipDoc == null)
                return (null, theRestRegCaseRef, messageRef);

            if (theRestRegCaseRef.IsProportionateQuota != YesNo.Yes.GetString() &&
       (theNewOwnershipDoc.DocumentEstateQuotaDetails == null ||
        theNewOwnershipDoc.DocumentEstateQuotaDetails.Count == 0))
            {
                this.GenerateIndividualPersonQuota(theNewOwnershipDoc, theDeterQuota);
            }

            return (theNewOwnershipDoc, theRestRegCaseRef, messageRef);
        }

        private DocumentEstate GetEquivalantRegCase(DocumentEstate originalRegCase, ICollection<DocumentEstate> theRegCasesCollection)
        {
            foreach (DocumentEstate theOneRegCase in theRegCasesCollection)
            {
                if (
                    theOneRegCase.BasicPlaque.Trim() == originalRegCase.BasicPlaque.Trim() &&
                    theOneRegCase.SecondaryPlaque.Trim() == originalRegCase.SecondaryPlaque.Trim()
                    )
                    return theOneRegCase;
            }

            return null;
        }

        private bool DoesOwnershipDocExistInRestRegCaseDocs(DocumentPerson theDeterBuyerPerson, DocumentEstateOwnershipDocument theDeterOwnershipDoc, ICollection<DocumentEstateOwnershipDocument> theRestPersonOwnershipDocsCollection, ref DocumentEstateOwnershipDocument theExistingRestOwnershipDoc)
        {
            if (!theRestPersonOwnershipDocsCollection.Any())
                return false;

            foreach (DocumentEstateOwnershipDocument theOneRestOwnershipDoc in theRestPersonOwnershipDocsCollection)
            {
                //if (theOneRestOwnershipDoc.OwnershipDocTitle.GetStandardFarsiString() == theDeterOwnershipDoc.OwnershipDocTitle.GetStandardFarsiString())
                if (theOneRestOwnershipDoc.EstateInquiriesId == theDeterOwnershipDoc.EstateInquiriesId)
                {
                    if (
                        theOneRestOwnershipDoc.DocumentPerson.NationalNo == theDeterBuyerPerson.NationalNo ||
                        theOneRestOwnershipDoc.DocumentPerson.FullName().GetStandardFarsiString() == theDeterBuyerPerson.FullName().GetStandardFarsiString()
                        )
                    {
                        theExistingRestOwnershipDoc = theOneRestOwnershipDoc;
                        return true;
                    }
                }
            }

            return false;
        }

        private DocumentPerson GetEquivalantPerson(DocumentPerson theDocPersonToFind, ICollection<DocumentPerson> theDocPersonCollection)
        {
            foreach (DocumentPerson theOneDocPerson in theDocPersonCollection)
            {
                if (theOneDocPerson.NationalNo == theDocPersonToFind.NationalNo)
                    return theOneDocPerson;

                if (theOneDocPerson.FullName() == theDocPersonToFind.FullName())
                    return theOneDocPerson;
            }

            return null;
        }

        private DocumentPerson GetEquivalantPerson(EstateInquiryPerson theDocPersonToFind, ICollection<DocumentPerson> theDocPersonCollection)
        {
            DocumentPerson theEquivalantPerson = null;

            if (string.IsNullOrWhiteSpace(theDocPersonToFind.NationalityCode))
                return null;
             foreach (DocumentPerson theOneDocPerson in theDocPersonCollection)
                {
                    if (string.IsNullOrWhiteSpace(theOneDocPerson.NationalNo))
                        continue;

                    if (theOneDocPerson.NationalNo == theDocPersonToFind.NationalityCode)
                        return theOneDocPerson;
                }

            return theEquivalantPerson;
        }

        /// <summary>
        /// Clones New DocPerson From InputPerson
        /// </summary>
        /// <param name="theDeterBuyer">
        /// thePerson for being cloned
        /// </param>
        /// <param name="theRestPersonsCollection">
        /// if This Collection Has Value, Existing Person In This Collection Will Be Returned, Otherwise New DocPerson Will Be Cloned
        /// </param>
        /// <returns></returns>
        private async Task<(DocumentPerson, DocumentEstate, bool)> GenerateNewRestDocPerson(DocumentPerson theDeterBuyer, CancellationToken cancellationToken, DocumentEstate theRestRegCase, bool deletePermitted)
        {
            DocumentEstate theRestRegCaseRef = theRestRegCase;
            bool deletePermittedRef = deletePermitted;

            deletePermittedRef = false;
            DocumentPerson theRestDocPerson = null;
            DocumentPersonType thePersonType = await this.GetCorrespondingPersonType(theRestRegCaseRef.Document.DocumentTypeId, cancellationToken);

            if (theRestRegCaseRef.Document.DocumentPeople.Any())
            {
                foreach (DocumentPerson theOneRestDocPerson in theRestRegCaseRef.Document.DocumentPeople)
                {
                    if (
                        theOneRestDocPerson.NationalNo == theDeterBuyer.NationalNo ||
                        theOneRestDocPerson.FullName().GetStandardFarsiString() == theDeterBuyer.FullName().GetStandardFarsiString()
                        )
                    {
                        if (theOneRestDocPerson.DocumentPersonType == null &&
                            theOneRestDocPerson.IsOriginal == YesNo.Yes.GetString())
                        {
                            theOneRestDocPerson.DocumentPersonType = thePersonType;
                            theOneRestDocPerson.DocumentPersonTypeId = thePersonType.Id;

                        }


                        return (theOneRestDocPerson, theRestRegCaseRef, deletePermittedRef);
                    }
                }
            }

            theRestDocPerson = new DocumentPerson();
            _mainObjectIsDirty = true;
            deletePermittedRef = true;

            theRestDocPerson.IsAlive = YesNo.None.GetString();
            theRestDocPerson.IsSabtahvalCorrect = YesNo.None.GetString();
            theRestDocPerson.IsSabtahvalChecked = NotaryIsDone.IsNotDone.GetString();
            theRestDocPerson.NationalNo = theDeterBuyer.NationalNo;
            theRestDocPerson.Name = theDeterBuyer.Name;
            theRestDocPerson.Family = theDeterBuyer.Family;
            theRestDocPerson.FatherName = theDeterBuyer.FatherName;
            theRestDocPerson.IdentityIssueGeoLocationId = theDeterBuyer.IdentityIssueGeoLocationId;
            theRestDocPerson.Address = theDeterBuyer.Address;
            theRestDocPerson.BirthDate = theDeterBuyer.BirthDate;
            theRestDocPerson.IdentityNo = theDeterBuyer.IdentityNo;
            theRestDocPerson.Seri = theDeterBuyer.Seri;
            theRestDocPerson.SeriAlpha = theDeterBuyer.SeriAlpha;
            theRestDocPerson.Serial = theDeterBuyer.Serial;
            theRestDocPerson.IsRelated = YesNo.No.GetString();
            theRestDocPerson.IsIranian = theDeterBuyer.IsIranian;
            theRestDocPerson.IsOriginal = theDeterBuyer.IsOriginal;
            if (theDeterBuyer.IsOriginal == YesNo.Yes.GetString())
                theRestDocPerson.DocumentPersonTypeId = thePersonType.Id;
            theRestDocPerson.PersonType = theDeterBuyer.PersonType;
            theRestDocPerson.PostalCode = theDeterBuyer.PostalCode;
            theRestDocPerson.SexType = theDeterBuyer.SexType;
            theRestDocPerson.Tel = theDeterBuyer.Tel;
            theRestDocPerson.RowNo = (short)this.GetMaxRowNo(theRestRegCaseRef.Document.DocumentPeople);

            theRestRegCaseRef.Document.DocumentPeople.Add(theRestDocPerson);

            return (theRestDocPerson, theRestRegCaseRef, deletePermittedRef);
        }

        private DocumentEstateQuotaDetail GenerateIndividualPersonQuota(DocumentEstateOwnershipDocument theNewOwnershipDoc, DocumentEstateQuotaDetail theDeterQuota)
        {
            DocumentEstateQuotaDetail newRestQuota = new DocumentEstateQuotaDetail();
            _mainObjectIsDirty = true;
            _newGeneratedQuotas.Add(newRestQuota.Id.ToString());

            newRestQuota.DocumentPersonSeller = theNewOwnershipDoc.DocumentPerson;
            newRestQuota.DocumentEstate = theNewOwnershipDoc.DocumentEstate;
            newRestQuota.DocumentEstateOwnershipDocumentId = theNewOwnershipDoc.Id;

            DocumentPerson theBuyerPerson = this.FindBuyerPerson(_theRestRegisterServiceReq.DocumentPeople);
            if (theBuyerPerson != null)
                newRestQuota.DocumentPersonBuyerId = theBuyerPerson.Id;

            newRestQuota.OwnershipDetailQuota = theDeterQuota.SellDetailQuota;
            newRestQuota.OwnershipTotalQuota = theDeterQuota.SellTotalQuota;
            newRestQuota.SellDetailQuota = theDeterQuota.SellDetailQuota;
            newRestQuota.SellTotalQuota = theDeterQuota.SellTotalQuota;

            newRestQuota.QuotaText = theDeterQuota.QuotaText;
            _documentEstateQuotaDetailRepository.Add(newRestQuota);

            return newRestQuota;
        }

        private async Task<DocumentEstateQuotaDetail> GenerateIndividualPersonQuota4InquiryBasedPersons(DocumentEstateOwnershipDocument theNewOwnershipDoc, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(theNewOwnershipDoc.EstateInquiriesId) || theNewOwnershipDoc.EstateInquiriesId.Contains("@Duplicated"))
                return null;

            string estInquiryID = theNewOwnershipDoc.EstateInquiriesId;
            EstateInquiryPerson theMasterBSTPerson =
                await _estateInquiryPersonRepository.GetAsync(t => t.EstateInquiryId == Guid.Parse(estInquiryID),
                    cancellationToken); //   Rad.CMS.InstanceBuilder.GetEntityByCriteria<Rad.ssaa.ssaaClass.IBSTPerson>(criteria);
            if (theMasterBSTPerson == null)
                return null;

            DocumentEstateQuotaDetail newRestQuota = new DocumentEstateQuotaDetail();
            newRestQuota.Ilm = "1";
            newRestQuota.Id = Guid.NewGuid();
            newRestQuota.ScriptoriumId = _userService.UserApplicationContext.ScriptoriumInformation.Id;
            newRestQuota.DocumentEstateId = theNewOwnershipDoc.DocumentEstateId;
            newRestQuota.DocumentEstateOwnershipDocumentId = theNewOwnershipDoc.Id;
            _mainObjectIsDirty = true;
            _newGeneratedQuotas.Add(newRestQuota.Id.ToString());

            newRestQuota.DocumentPersonSellerId = theNewOwnershipDoc.DocumentPersonId;


            DocumentPerson theBuyerPerson = this.FindBuyerPerson(_theRestRegisterServiceReq.DocumentPeople);
            if (theBuyerPerson != null)
                newRestQuota.DocumentPersonBuyerId = theBuyerPerson.Id;

            newRestQuota.OwnershipDetailQuota = (decimal?)theMasterBSTPerson.SharePart;
            newRestQuota.OwnershipTotalQuota = (theMasterBSTPerson.ShareTotal != null) ? (long)theMasterBSTPerson.ShareTotal : 0;
            newRestQuota.SellDetailQuota = null;
            newRestQuota.SellTotalQuota = null;

            newRestQuota.QuotaText = theMasterBSTPerson.ShareText;
            _documentEstateQuotaDetailRepository.Add(newRestQuota, false);

            return newRestQuota;
        }

        /// <summary>
        /// Clones The DeterministicOwnershipDoc And Mark It As @Duplicated
        /// </summary>
        /// <param name="theDeterOwnershipDoc">The Deterministic OwnershipDoc</param>        
        /// <param name="theRestDocPerson">The RestrictedDocPerson, This is being used to append to newRestrictedOwnershipDoc</param>        
        /// <param name="theRestRegCase">TheRestrictedRegCase Which The Ownershipdoc Will Be Added to Its Collection</param>
        /// <returns>Returns Resticted OwnershipDocument</returns>
        private DocumentEstateOwnershipDocument GenerateIndividualNewRestOwnershipDoc(DocumentEstateOwnershipDocument theDeterOwnershipDoc, DocumentPerson theRestDocPerson, DocumentEstate theRestRegCase)
        {
            DocumentEstateOwnershipDocument theExistingRestOwnershipDoc = null;
            bool docExists = this.DoesOwnershipDocExistInRestRegCaseDocs(theRestDocPerson, theDeterOwnershipDoc, theRestRegCase.DocumentEstateOwnershipDocuments, ref theExistingRestOwnershipDoc);
            if (docExists)
                return null;


            DocumentEstateOwnershipDocument theNewRestOwnershipDoc = new DocumentEstateOwnershipDocument();
            _mainObjectIsDirty = true;
            theNewRestOwnershipDoc.Ilm = "1";
            theNewRestOwnershipDoc.Id = Guid.NewGuid();
            theNewRestOwnershipDoc.DocumentEstateId = theDeterOwnershipDoc.DocumentEstateId;

            theNewRestOwnershipDoc.EstateSeridaftarId = theDeterOwnershipDoc.EstateSeridaftarId;
            theNewRestOwnershipDoc.DealSummaryText = theDeterOwnershipDoc.DealSummaryText;
            theNewRestOwnershipDoc.EstateBookNo = theDeterOwnershipDoc.EstateBookNo;
            theNewRestOwnershipDoc.EstateBookPageNo = theDeterOwnershipDoc.EstateBookPageNo;
            theNewRestOwnershipDoc.EstateBookType = theDeterOwnershipDoc.EstateBookType;
            theNewRestOwnershipDoc.Description = theDeterOwnershipDoc.Description;
            theNewRestOwnershipDoc.NotaryDocumentNo = theDeterOwnershipDoc.NotaryDocumentNo;
            theNewRestOwnershipDoc.EstateDocumentType = theDeterOwnershipDoc.EstateDocumentType;
            theNewRestOwnershipDoc.EstateIsReplacementDocument = theDeterOwnershipDoc.EstateIsReplacementDocument;
            theNewRestOwnershipDoc.NotaryDocumentDate = theDeterOwnershipDoc.NotaryDocumentDate;
            theNewRestOwnershipDoc.NotaryDocumentNo = theDeterOwnershipDoc.NotaryDocumentNo;
            theNewRestOwnershipDoc.NotaryLocation = theDeterOwnershipDoc.NotaryLocation;
            theNewRestOwnershipDoc.NotaryNo = theDeterOwnershipDoc.NotaryNo;
            theNewRestOwnershipDoc.EstateSabtNo = theDeterOwnershipDoc.EstateSabtNo;
            theNewRestOwnershipDoc.SabtStateReportDate = theDeterOwnershipDoc.SabtStateReportDate;
            theNewRestOwnershipDoc.SabtStateReportNo = theDeterOwnershipDoc.SabtStateReportNo;
            theNewRestOwnershipDoc.MortgageText = theDeterOwnershipDoc.MortgageText;
            theNewRestOwnershipDoc.OwnershipDocumentType = theDeterOwnershipDoc.OwnershipDocumentType;
            theNewRestOwnershipDoc.EstateElectronicPageNo = theDeterOwnershipDoc.EstateElectronicPageNo;

            if (!string.IsNullOrWhiteSpace(theNewRestOwnershipDoc.EstateInquiriesId))
            {
                if (!theNewRestOwnershipDoc.EstateInquiriesId.Contains("@Duplicated"))
                    theNewRestOwnershipDoc.EstateInquiriesId += "@Duplicated";
            }

            string newDuplicationESTEStateInquiryID = this.GetCorrespondingDuplicatedIdentifier(theDeterOwnershipDoc, theRestRegCase);
            theNewRestOwnershipDoc.EstateInquiriesId = newDuplicationESTEStateInquiryID;

            theNewRestOwnershipDoc.DocumentPersonId = theRestDocPerson.Id;

            theRestRegCase.DocumentEstateOwnershipDocuments.Add(theNewRestOwnershipDoc);

            return theNewRestOwnershipDoc;
        }

        private string GetCorrespondingDuplicatedIdentifier(DocumentEstateOwnershipDocument theDeterOwnershipDoc, DocumentEstate theRestRegCase)
        {
            string restDuplicatedIdentifier = null;

            if (
                string.IsNullOrWhiteSpace(theRestRegCase.EstateInquiryId) ||
                string.IsNullOrWhiteSpace(theDeterOwnershipDoc.EstateInquiriesId)
                )
                return null;

            string deterESTEStateInquiryID = theDeterOwnershipDoc.EstateInquiriesId.Replace("@Duplicated", "");
            //int startIndex = theRestRegCase.ESTEstateInquiryId.IndexOf(deterESTEStateInquiryID);
            //int finishIndex = theRestRegCase.ESTEstateInquiryId.Length - startIndex;

            restDuplicatedIdentifier = deterESTEStateInquiryID;// theRestRegCase.ESTEstateInquiryId.Substring(startIndex, finishIndex);

            restDuplicatedIdentifier += "@Duplicated";

            return restDuplicatedIdentifier;
        }

        private bool IsPersonSepecifiedAsBuyerInDetermnisticReq(DocumentPerson theCurrentDocPerson, DocumentEstateOwnershipDocument theDeterOwnershipDoc, ref bool? isSoldEntirely)
        {
            bool isPersonDefinedAsBuyer = false;
            string personName = theCurrentDocPerson.FullName();
            decimal[] owningFraction = null;
            decimal[] sellingFraction = null;

            foreach (DocumentEstateQuotaDetail theOneDeterQuota in theDeterOwnershipDoc.DocumentEstateQuotaDetails)
            {
                if (
                    theCurrentDocPerson.NationalNo == theOneDeterQuota.DocumentPersonBuyer.NationalNo &&
                    theCurrentDocPerson.FullName().GetStandardFarsiString() == theOneDeterQuota.DocumentPersonBuyer.FullName().GetStandardFarsiString()
                    )
                {
                    isPersonDefinedAsBuyer = true;
                }


                if (
                    theOneDeterQuota.OwnershipDetailQuota == null || theOneDeterQuota.OwnershipDetailQuota == 0 ||
                    theOneDeterQuota.OwnershipTotalQuota == null || theOneDeterQuota.OwnershipTotalQuota == 0 ||
                    theOneDeterQuota.SellDetailQuota == null || theOneDeterQuota.SellDetailQuota == 0 ||
                    theOneDeterQuota.SellTotalQuota == null || theOneDeterQuota.SellTotalQuota == 0
                    )
                {
                    isSoldEntirely = false;
                    return isPersonDefinedAsBuyer;
                }

                if (owningFraction == null)
                {
                    owningFraction = new decimal[2];
                    owningFraction[0] = (decimal)theOneDeterQuota.OwnershipDetailQuota;
                    owningFraction[1] = ((decimal)theOneDeterQuota.OwnershipTotalQuota);
                }

                if (sellingFraction == null)
                {
                    sellingFraction = new decimal[2];
                    sellingFraction[0] = (decimal)theOneDeterQuota.SellDetailQuota;
                    sellingFraction[1] = ((decimal)theOneDeterQuota.SellTotalQuota);
                }
                else
                {
                    sellingFraction =
                        Mathematics.MakhrajMoshtarak(sellingFraction[0], ((decimal)theOneDeterQuota.SellDetailQuota), sellingFraction[1], ((decimal)theOneDeterQuota.SellTotalQuota));
                }
            }

            if (sellingFraction[0] * owningFraction[1] == sellingFraction[1] * owningFraction[0])
                isSoldEntirely = true;
            else
                isSoldEntirely = false;


            return isPersonDefinedAsBuyer;
        }

        private bool IsPersonSepecifiedAsSellerInDetermnisticReq(DocumentEstateOwnershipDocument theRestOwnershipDoc, DocumentEstateOwnershipDocument theDeterOwnershipDoc, ref bool? isSoldEntirely)
        {
            bool isPersonDefinedAsSeller = false;
            string personName = theRestOwnershipDoc.DocumentPerson.FullName();
            decimal[] owningFraction = null;
            decimal[] sellingFraction = null;

            foreach (DocumentEstateOwnershipDocument theOneDeterOwneshipDoc in theDeterOwnershipDoc.DocumentEstate.DocumentEstateOwnershipDocuments)
            {
                if (string.IsNullOrWhiteSpace(theOneDeterOwneshipDoc.EstateInquiriesId))
                    continue;

                if (theOneDeterOwneshipDoc.EstateInquiriesId != theRestOwnershipDoc.EstateInquiriesId)
                    continue;

                foreach (DocumentEstateQuotaDetail theOneDeterQuota in theDeterOwnershipDoc.DocumentEstateQuotaDetails)
                {
                    if (
                        theRestOwnershipDoc.DocumentPerson.NationalNo == theOneDeterQuota.DocumentPersonSeller.NationalNo &&
                        theRestOwnershipDoc.DocumentPerson.FullName().GetStandardFarsiString() == theOneDeterQuota.DocumentPersonBuyer.FullName().GetStandardFarsiString()
                        )
                    {
                        isPersonDefinedAsSeller = true;
                    }
                    else
                    {
                        continue;
                    }


                    if (
                        theOneDeterQuota.OwnershipDetailQuota == null || theOneDeterQuota.OwnershipDetailQuota == 0 ||
                        theOneDeterQuota.OwnershipTotalQuota == null || theOneDeterQuota.OwnershipTotalQuota == 0 ||
                        theOneDeterQuota.SellDetailQuota == null || theOneDeterQuota.SellDetailQuota == 0 ||
                        theOneDeterQuota.SellTotalQuota == null || theOneDeterQuota.SellTotalQuota == 0
                        )
                    {
                        isSoldEntirely = false;
                        return isPersonDefinedAsSeller;
                    }

                    if (owningFraction == null)
                    {
                        owningFraction = new decimal[2];
                        owningFraction[0] = (decimal)theOneDeterQuota.OwnershipDetailQuota;
                        owningFraction[1] = ((decimal)theOneDeterQuota.OwnershipTotalQuota);
                    }

                    if (sellingFraction == null)
                    {
                        sellingFraction = new decimal[2];
                        sellingFraction[0] = (decimal)theOneDeterQuota.SellDetailQuota;
                        sellingFraction[1] = ((decimal)theOneDeterQuota.SellTotalQuota);
                    }
                    else
                    {
                        sellingFraction = Mathematics.MakhrajMoshtarak(sellingFraction[0], ((decimal)theOneDeterQuota.SellDetailQuota), sellingFraction[1], ((decimal)theOneDeterQuota.SellTotalQuota));
                    }
                }

            }

            if (sellingFraction != null && owningFraction != null)
                if (sellingFraction[0] * owningFraction[1] == sellingFraction[1] * owningFraction[0])
                    isSoldEntirely = true;
                else
                    isSoldEntirely = false;


            return isPersonDefinedAsSeller;
        }

        private async Task<DocumentEstate> CorrectExistingPrimaryOwnerships(DocumentEstate theRestRegCase, DocumentPerson theNewRestDocPerson, DocumentEstateOwnershipDocument theDeterOwnershipDoc, CancellationToken cancellationToken)
        {
            DocumentEstate theRestRegCaseRef = theRestRegCase;

            string ownerName = theNewRestDocPerson.FullName();

            foreach (DocumentEstateOwnershipDocument theOneRestOwnership in theRestRegCaseRef.DocumentEstateOwnershipDocuments)
            {
                if (string.IsNullOrWhiteSpace(theOneRestOwnership.EstateInquiriesId))
                    continue;

                if (theOneRestOwnership.EstateInquiriesId.Contains("@Duplicated"))
                    continue;

                string estestateInquiryID = theOneRestOwnership.EstateInquiriesId;
                EstateInquiry theMasterInquiry =
                   await _estateInquiryRepository.GetAsync(t => t.Id == Guid.Parse(estestateInquiryID), cancellationToken);//Rad.CMS.InstanceBuilder.GetEntityById<Rad.ssaa.ssaaClass.IESTEstateInquiry>(estestateInquiryID);

                if (theMasterInquiry == null)
                    continue;

                EstateInquiryPerson theBSTPerson = theMasterInquiry.EstateInquiryPeople.ElementAt(0);
                if (theBSTPerson == null)
                    continue;

                string bstPersonName = theBSTPerson.Name;
                bool? isSoldEntirely = null;
                bool isDefinedAsBuyer = this.IsPersonSepecifiedAsBuyerInDetermnisticReq(theNewRestDocPerson, theDeterOwnershipDoc, ref isSoldEntirely);
                if (!isDefinedAsBuyer)
                {
                    DocumentPerson theEquivalantDocPerson = this.GetEquivalantPerson(theBSTPerson, theOneRestOwnership.DocumentEstate.Document.DocumentPeople);
                    if (theEquivalantDocPerson != null)
                    {
                        DocumentEstateOwnershipDocument theExistingRestOwnershipDoc = null;
                        bool docExists = this.DoesOwnershipDocExistInRestRegCaseDocs(theEquivalantDocPerson, theOneRestOwnership, theRestRegCaseRef.DocumentEstateOwnershipDocuments, ref theExistingRestOwnershipDoc);
                        if (docExists)
                            continue;

                        theOneRestOwnership.DocumentPersonId = theEquivalantDocPerson.Id;
                        _mainObjectIsDirty = true;
                    }
                }
                else if (
                         isSoldEntirely == true &&
                        theBSTPerson.NationalityCode == theOneRestOwnership.DocumentPerson.NationalNo &&
                        theBSTPerson.Name.GetStandardFarsiString() == theOneRestOwnership.DocumentPerson.FullName().GetStandardFarsiString()
                        )
                {
                    DocumentEstateOwnershipDocument theExistingRestOwnershipDoc = null;
                    bool docExists = this.DoesOwnershipDocExistInRestRegCaseDocs(theNewRestDocPerson, theOneRestOwnership, theRestRegCaseRef.DocumentEstateOwnershipDocuments, ref theExistingRestOwnershipDoc);
                    if (docExists)
                        continue;

                    bool? doesSellerSoldEntirly = null;
                    if (this.IsPersonSepecifiedAsSellerInDetermnisticReq(theOneRestOwnership, theDeterOwnershipDoc, ref doesSellerSoldEntirly) && doesSellerSoldEntirly.HasValue && doesSellerSoldEntirly == true)
                        theOneRestOwnership.DocumentPersonId = theNewRestDocPerson.Id;

                    _mainObjectIsDirty = true;
                }
            }

            return theRestRegCaseRef;
        }

        private async Task ResetExistingPrimaryOwnershipsToDefaults(DocumentEstate theRestRegCase, CancellationToken cancellationToken)
        {
            if (
                string.IsNullOrWhiteSpace(theRestRegCase.EstateInquiryId) ||
                !theRestRegCase.DocumentEstateOwnershipDocuments.Any()
                )
                return;

            Stack<DocumentEstateOwnershipDocument> theRemovingDocsStack = new Stack<DocumentEstateOwnershipDocument>();

            foreach (DocumentEstateOwnershipDocument theOneRestOwnershipDoc in theRestRegCase.DocumentEstateOwnershipDocuments)
            {
                if (string.IsNullOrWhiteSpace(theOneRestOwnershipDoc.EstateInquiriesId))
                    continue;

                if (theOneRestOwnershipDoc.EstateInquiriesId.Contains("@Duplicated"))
                {
                    //  theOneRestOwnershipDoc.MarkForDelete ();
                    _mainObjectIsDirty = true;
                    theRemovingDocsStack.Push(theOneRestOwnershipDoc);
                    continue;
                }

                string estestateInquiryID = theOneRestOwnershipDoc.EstateInquiriesId;
                EstateInquiryPerson theBSTPerson =
                    await _estateInquiryPersonRepository.GetAsync(t => t.EstateInquiryId == Guid.Parse(estestateInquiryID),
                        cancellationToken);


                DocumentPerson theEquivalantDocPerson = this.GetEquivalantPerson(theBSTPerson, theOneRestOwnershipDoc.DocumentEstate.Document.DocumentPeople);
                if (theEquivalantDocPerson == null)
                    continue;

                theOneRestOwnershipDoc.DocumentPersonId = theEquivalantDocPerson.Id;
                _mainObjectIsDirty = true;
            }

            while (theRemovingDocsStack.Count > 0)
                theRestRegCase.DocumentEstateOwnershipDocuments.Remove(theRemovingDocsStack.Pop());
            await _documentEstateRepository.UpdateAsync(theRestRegCase, cancellationToken, false);

        }

        //private void CorrectExistingPrimaryOwnerships(ref IONotaryRegCase theRestRegCase, IONotaryRegisterServiceReq theDeterReq)
        //{
        //    foreach (IONotaryPersonOwnershipDoc theOneRestOwnership in theRestRegCase.TheONotaryPersonOwnershipDocList)
        //    {
        //        if (string.IsNullOrWhiteSpace(theOneRestOwnership.ESTEstateInquiryId))
        //            continue;

        //        if (theOneRestOwnership.ESTEstateInquiryId.Contains("@Duplicated"))
        //            continue;

        //        string estestateInquiryID = theOneRestOwnership.ESTEstateInquiryId;
        //        Rad.ssaa.ssaaClass.IESTEstateInquiry theMasterInquiry = Rad.CMS.InstanceBuilder.GetEntityById<Rad.ssaa.ssaaClass.IESTEstateInquiry>(estestateInquiryID);

        //        if (theMasterInquiry == null)
        //            continue;

        //        Rad.ssaa.ssaaClass.IBSTPerson theBSTPerson = theMasterInquiry.TheBSTPersonList[0] as Rad.ssaa.ssaaClass.IBSTPerson;
        //        if (theBSTPerson == null)
        //            continue;

        //        IONotaryDocPerson theEquivalantDocPerson = this.GetEquivalantPerson(theBSTPerson, theOneRestOwnership.TheONotaryRegCase.TheONotaryRegisterServiceReq.TheONotaryDocPersonList);
        //        if (theEquivalantDocPerson == null)
        //            continue;                

        //        bool isDefinedAsBuyer = this.IsPersonSepecifiedAsBuyerInDetermnisticReq(theEquivalantDocPerson, theDeterReq);
        //        if (!isDefinedAsBuyer)
        //        {
        //            if (theEquivalantDocPerson != null)
        //            {
        //                IONotaryPersonOwnershipDoc theExistingRestOwnershipDoc = null;
        //                bool docExists = this.DoesOwnershipDocExistInRestRegCaseDocs(theEquivalantDocPerson, theOneRestOwnership, theRestRegCase.TheONotaryPersonOwnershipDocList, ref theExistingRestOwnershipDoc);
        //                if (docExists)
        //                    continue;

        //                theOneRestOwnership.TheONotaryDocPerson = theEquivalantDocPerson;
        //                _mainObjectIsDirty = true;
        //            }
        //        }
        //    }
        //}
        public async Task<(bool, string)> AutoGenerateRestrictedOwnershipDocs(string RequestNo, string ONotaryRegisterServiceReqId, CancellationToken cancellationToken)
        {



            string messages = null;
            bool isGenerated;
            (isGenerated, messages) = await InheritDeterministicOwnershipDocs(RequestNo, ONotaryRegisterServiceReqId, cancellationToken, messages);


            return (isGenerated, messages);
        }
        private List<Document> GetIncludingDeterministicReqsEquivalantToBuyer(DocumentPerson theRestPerson, List<Document> theDeterReqsCollection = null)
        {
            List<Document> theIncludingDeterministicReqs = null;

            string personName = theRestPerson.FullName();

            if (theDeterReqsCollection == null)
                theDeterReqsCollection = _theDeterministicReqs;

            foreach (Document theOneDeterReq in theDeterReqsCollection)
            {
                foreach (DocumentPerson theOneDeterDocPerson in theOneDeterReq.DocumentPeople)
                {
                    if (theRestPerson.NationalNo == theOneDeterDocPerson.NationalNo && theRestPerson.FullName().GetStandardFarsiString() == theOneDeterDocPerson.FullName().GetStandardFarsiString())
                        if (theOneDeterDocPerson.DocumentEstateQuotaDetailDocumentPersonBuyers.Any())
                        {
                            if (theIncludingDeterministicReqs == null)
                                theIncludingDeterministicReqs = new List<Document>();

                            if (!theIncludingDeterministicReqs.Contains(theOneDeterReq))
                                theIncludingDeterministicReqs.Add(theOneDeterReq);
                        }
                }
            }

            return theIncludingDeterministicReqs;
        }

        private decimal? GetMaxRowNo(ICollection<DocumentPerson> docPersonsCollection)
        {
            decimal? maxValue = 0;
            foreach (DocumentPerson theOneDocPerson in docPersonsCollection)
            {
                if (theOneDocPerson.RowNo > maxValue)
                    maxValue = theOneDocPerson.RowNo;
            }

            return (maxValue + 1);
        }

        private async Task<DocumentPersonType> GetCorrespondingPersonType(string theRestDocumentTypeId, CancellationToken cancellationToken)
        {
            var personType =
                await _documentPersonTypePersonRepository.GetAsync(
                    t => t.LegacyId == "320F2F4E17C144AD8ED9CD53314992AC" && t.DocumentTypeId == theRestDocumentTypeId, cancellationToken);




            return personType;
        }

        private DocumentPerson FindBuyerPerson(ICollection<DocumentPerson> thePersonsCollection)
        {
            if (!thePersonsCollection.Any())
                return null;

            int totalPersonsCount = (thePersonsCollection.Any()) ? thePersonsCollection.Count : 0;

            int buyersCount = 0;
            DocumentPerson theBuyer = null;

            foreach (DocumentPerson theOneDocPerson in thePersonsCollection)
            {
                if (
                    theOneDocPerson.DocumentPersonType != null &&
                    theOneDocPerson.DocumentPersonType.IsOwner != YesNo.Yes.GetString()
                    )
                {
                    if (
                        theBuyer == null ||
                        ( theBuyer.NationalNo == theOneDocPerson.NationalNo && theBuyer.FullName().GetStandardFarsiString() == theOneDocPerson.FullName().GetStandardFarsiString())
                        )
                    {
                        theBuyer = theOneDocPerson;
                        buyersCount++;
                    }
                }
            }

            if (buyersCount == 1)
                return theBuyer;

            return null;
        }


    }

}
