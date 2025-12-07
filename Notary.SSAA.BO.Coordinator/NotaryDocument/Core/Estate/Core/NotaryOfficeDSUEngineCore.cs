using Notary.SSAA.BO.DataTransferObject.Coordinators.Estate;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notary.SSAA.BO.Configuration;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.RichDomain.Document;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.BaseInfoService;
using System.Threading;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.BaseInfoService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Commons;
using Notary.SSAA.BO.Domain.RepositoryObjects.Estate;
using Notary.SSAA.BO.Coordinator.NotaryDocument.Core.ENoteBook;
using Notary.SSAA.BO.DataTransferObject.Queries.Lookups.Estate;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;
using Notary.SSAA.BO.Utilities;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.DealSummary;
using System.Security.Cryptography.X509Certificates;
namespace Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Estate.Core
{
    public class NotaryOfficeDSUEngineCore
    {
        public List<DocumentPerson> UnSignedPersonsList { get; set; }

        private readonly ClientConfiguration _clientConfiguration;
        private readonly DSULogger _logController;
        private readonly IDocumentInquiryRepository _documentInquiryRepository;
        private readonly IDocumentRepository _documentRepository;
        private readonly IEstateInquiryRepository _estateInquiryRepository;
        private readonly IPersonFingerprintRepository _personFingerprintRepository;
        private readonly IDealSummaryRepository _dealSummaryRepository;

        private readonly IRepository<DocumentEstateDealSummaryGenerated> _documentEstateDealSummaryGeneratedRepository;
        private readonly IRepository<DocumentEstateDealSummarySelected> _documentEstateDealSummarySelectedRepository;
        private readonly ENoteBookServerController _eNoteBookServerController;
        protected readonly IMediator _mediator;
        protected readonly DSULogger _dsuLogger;
        private readonly IUserService _userService;
        private readonly DSUPersonsManager _dSUPersonsManager;
        private readonly IDateTimeService _dateTimeService;

        public ProcessActionType _DsuProcessActionType = ProcessActionType.None;

        public NotaryOfficeDSUEngineCore(
            ClientConfiguration clientConfiguration,
            DSULogger logController, IDocumentInquiryRepository documentInquiryRepository,
            IDocumentRepository documentRepository,
            IRepository<DocumentEstateDealSummaryGenerated> documentEstateDealSummaryGeneratedRepository,
            IRepository<DocumentEstateDealSummarySelected> documentEstateDealSummarySelectedRepository,
            IEstateInquiryRepository estateInquiryRepository,
            IPersonFingerprintRepository personFingerprintRepository,
            IDealSummaryRepository dealSummaryRepository,
            IMediator mediator,
            DSULogger dsuLogger,
            DSUPersonsManager dSUPersonsManager,
            ENoteBookServerController eNoteBookServerController,
            IDateTimeService dateTimeService, IUserService userService
        )
        {
            _clientConfiguration = clientConfiguration;
            _logController = logController;
            _documentInquiryRepository = documentInquiryRepository;
            _documentEstateDealSummaryGeneratedRepository = documentEstateDealSummaryGeneratedRepository;
            _documentEstateDealSummarySelectedRepository = documentEstateDealSummarySelectedRepository;
            _documentRepository = documentRepository;
            _estateInquiryRepository = estateInquiryRepository;
            _personFingerprintRepository = personFingerprintRepository;
            _mediator = mediator;
            _dsuLogger = dsuLogger;
            _dSUPersonsManager = dSUPersonsManager;
            _eNoteBookServerController = eNoteBookServerController;
            _dateTimeService = dateTimeService;
            _dealSummaryRepository = dealSummaryRepository;
            _userService = userService;
        }

        public async Task<(List<DSUDealSummaryObject>, string)> CollectSignedDSUDealSummaries(
            List<DSUDealSummarySignPacket> signedPacketsCollection, Document theCurrentRegisterServiceReq,
            CancellationToken cancellationToken, string dealSummaryMessages, bool isFailedOldDSUInProcess = false)
        {
            List<DSUDealSummaryObject> dsuDealSummaryObjectsCollection = null;
            var dealSummaryMessagesRef = dealSummaryMessages;
            try
            {
                if (isFailedOldDSUInProcess)
                {
                    var theDeterministicRegisterServiceReq = await _documentRepository.GetDocumentById(
                        Guid.Parse(signedPacketsCollection[0].RegisterServiceReqObjectID),
                        new List<string> { "DocumentType", "DocumentPeople", "DocumentInquiries", "DocumentEstates" },
                        cancellationToken);
                    (dsuDealSummaryObjectsCollection, dealSummaryMessagesRef) =
                        await CollectDSUDealSummaries(theDeterministicRegisterServiceReq, cancellationToken,
                            dealSummaryMessagesRef, true);
                }
                else
                {
                    (dsuDealSummaryObjectsCollection, dealSummaryMessagesRef) =
                        await CollectDSUDealSummaries(theCurrentRegisterServiceReq, cancellationToken,
                            dealSummaryMessagesRef, true);
                }

                if (!dsuDealSummaryObjectsCollection.Any())
                {
                    dealSummaryMessagesRef =
                        "خلاصه معامله های مربوط به سند جاری از سرور دریافت نشد. لطفاً مجدداً تلاش نمایید.";
                    return (null, dealSummaryMessagesRef);
                }

                foreach (var theOneDealSummaryObject in dsuDealSummaryObjectsCollection)
                {
                    if (theOneDealSummaryObject.DealNo != theCurrentRegisterServiceReq.ClassifyNoReserved.ToString())
                    {
                        dealSummaryMessagesRef =
                            "ارسال خلاصه معامله با شکست مواجه گردید. خطا در تخصیص شماره خلاصه معامله و تطابق با شماره ترتیب سند. لطفاً مجدداً تلاش نمایید.";
                        return (null, dealSummaryMessagesRef);
                    }

                    DSUDealSummarySignPacket singleNotSignedPacket;

                    (singleNotSignedPacket, dealSummaryMessagesRef) =
                        await ProvideSingleSignPacket(theOneDealSummaryObject, cancellationToken,
                            dealSummaryMessagesRef);
                    var equivalantSignedDataPacket =
                        FindEquivalanteSignPacket(singleNotSignedPacket.RawDataB64, signedPacketsCollection);

                    if (equivalantSignedDataPacket == null)
                    {
                        dealSummaryMessagesRef = "خطا در دریافت امضای الکترونیک مربوط به خلاصه معامله های آماده ارسال!";
                        return (null, dealSummaryMessagesRef);
                    }

                    theOneDealSummaryObject.CertificateBase64 = equivalantSignedDataPacket.CertificateB64;
                    theOneDealSummaryObject.DataSignatureBase64String = equivalantSignedDataPacket.SignB64;
                    theOneDealSummaryObject.DataSignature = Convert.FromBase64String(equivalantSignedDataPacket.SignB64);
                    var cr = new X509Certificate2(Convert.FromBase64String(theOneDealSummaryObject.CertificateBase64));
                    theOneDealSummaryObject.SubjectDn = cr.Subject;
                }

                return (dsuDealSummaryObjectsCollection, dealSummaryMessagesRef);
            }
            catch (Exception ex)
            {
                dealSummaryMessagesRef = "خطا در دریافت بسته های آماده ارسال برای خلاصه معامله";
                return (null, dealSummaryMessagesRef);
            }
        }

        public async Task<DocumentEstateDealSummaryGenerated?>
            CollectFailedDeterministicDealSummaries(Document theCurrentRegisterServiceReq,
                CancellationToken cancellationToken)
        {
            if (!Mapper.IsONotaryDocumentRestrictedType(theCurrentRegisterServiceReq.DocumentTypeId))
                return null;

            if (!theCurrentRegisterServiceReq.DocumentInquiries.Any())
                return null;

            var estestateInquiryIDsCollection = new List<string>();
            foreach (var theOneInquiry in theCurrentRegisterServiceReq.DocumentInquiries)
            {
                if (string.IsNullOrWhiteSpace(theOneInquiry.EstateInquiriesId))
                    continue;

                if (!estestateInquiryIDsCollection.Contains(theOneInquiry.EstateInquiriesId))
                    estestateInquiryIDsCollection.Add(theOneInquiry.EstateInquiriesId);
            }

            if (!estestateInquiryIDsCollection.Any())
                return null;

            var deterministicInquiries = await _documentInquiryRepository.CollectSignedDSUDealSummaries(
                theCurrentRegisterServiceReq.Id,
                _userService.UserApplicationContext.ScriptoriumInformation.Id, estestateInquiryIDsCollection,
                _clientConfiguration.IsUnRegisteredEstateInquiryForced, _clientConfiguration.DSUInitializationDate);

            if (!deterministicInquiries.Any())
                return null;

            List<Document> deterministicRegisterServiceReqsCollection = null;

            foreach (var theOneDeterministicInquiry in deterministicInquiries)
            {
                if (deterministicRegisterServiceReqsCollection == null)
                    deterministicRegisterServiceReqsCollection = new List<Document>();

                if (!deterministicRegisterServiceReqsCollection.Contains(theOneDeterministicInquiry.Document))
                    deterministicRegisterServiceReqsCollection.Add(theOneDeterministicInquiry.Document);
            }

            if (!deterministicRegisterServiceReqsCollection.Any())
                return null;

            var theDeterRegisterServiceReqsObjectIDs = new List<string>();

            foreach (var theOneDeterRegisterServiceReq in deterministicRegisterServiceReqsCollection)
                if (!theDeterRegisterServiceReqsObjectIDs.Contains(theOneDeterRegisterServiceReq.Id.ToString()))
                    theDeterRegisterServiceReqsObjectIDs.Add(theOneDeterRegisterServiceReq.Id.ToString());

            if (!theDeterRegisterServiceReqsObjectIDs.Any())
                return null;

            var deterDSUsXMLs = await _documentEstateDealSummaryGeneratedRepository.GetAllAsync(
                t => theDeterRegisterServiceReqsObjectIDs.Contains(t.DocumentId.ToString()) &&
                     t.IsSent == NotaryGeneratedDealSummary.NotSent.GetString(), cancellationToken
            );

            if (deterDSUsXMLs == null || !deterDSUsXMLs.Any()) return null;

            var isDSURegisteredOnCPMS = await IsDSUSent4ThisRequestUsingCPMS(deterDSUsXMLs[0].Document, cancellationToken);
            if (isDSURegisteredOnCPMS)
            {
                _ = _logController.UpdateDSULogStatus(deterDSUsXMLs[0].DocumentId.ToString(), NotaryGeneratedDealSummary.Sent,
                    "", cancellationToken);
                return null;
            }

            return deterDSUsXMLs[0];
        }

        /// <summary>
        /// </summary>
        /// <param name="theCurrentRegisterServiceReq"></param>
        /// <param name="dealSummaryMessages"></param>
        /// <param name="clientSignProcessDone">
        ///     If True: Returns Logged DSUObject with No-Change,If False: Appends Many Changes To
        ///     Log Then Returns DSUObjects
        /// </param>
        /// <returns></returns>
        public async Task<(List<DSUDealSummaryObject>, string)> CollectDSUDealSummaries(
            Document theCurrentRegisterServiceReq, CancellationToken cancellationToken, string dealSummaryMessages,
            bool clientSignProcessDone = false)
        {
            var dealSummaryMessagesRef = dealSummaryMessages;

            List<DSUDealSummaryObject> theDSUDealSummaryCollection = null;
            var isCollectionValid = false;
            var isRestricted = Mapper.IsONotaryDocumentRestrictedType(theCurrentRegisterServiceReq.DocumentTypeId);
            var isRemovingDSU = theCurrentRegisterServiceReq.DocumentTypeId == "004" ? true : false;

            //if (
            //    theCurrentRegisterServiceReq.State == Rad.CMS.Enumerations.NotaryRegServiceReqState.SetNationalDocumentNo ||
            //    theCurrentRegisterServiceReq.State == Rad.CMS.Enumerations.NotaryRegServiceReqState.FinalPrinted ||
            //    theCurrentRegisterServiceReq.State == Rad.CMS.Enumerations.NotaryRegServiceReqState.Finalized
            //    )
            //{
            //    DSULogger logController = new DSULogger();

            //    theDSUDealSummaryCollection = logController.ExportGeneratedDSULog(theCurrentRegisterServiceReq.ObjectId);

            //    bool isCollectionValid = this.IsGeneratedDSUsCollectionValid(theCurrentRegisterServiceReq, theDSUDealSummaryCollection, ref dealSummaryMessages);
            //    if (theDSUDealSummaryCollection.CollectionHasElement() && isCollectionValid)
            //    {
            //        if (clientSignProcessDone)
            //        {
            //            return theDSUDealSummaryCollection;
            //        }
            //        else
            //        {
            //            //In Restricted DSUs, only 2 persons can be included in collection, 
            //            //So if there exists any Logged-DSU which its PersonsCollection count == 2
            //            //theres no need to regenerate or add any extra person, and the current DSU should be returned as the generated DSU.
            //            //Keep in mind that ONLY "RESTRICTED" DSUs behave like this.
            //            bool isCurrentDSUCollectionValid = true;
            //            if (isRestricted)
            //            {
            //                foreach (DSUDealSummaryObject theOneDSUObject in theDSUDealSummaryCollection)
            //                {
            //                    if (theOneDSUObject.TheDSURealLegalPersonList.Count != 2)
            //                    {
            //                        isCurrentDSUCollectionValid = false;
            //                        break;
            //                    }
            //                }
            //            }

            //            if (_DsuProcessActionType == InquiryManagerTypeDefinitions.ProcessActionType.DSUSending)
            //            {
            //                this.CorrectInvalidDataOnDSUObjects(ref theDSUDealSummaryCollection);
            //                theDSUDealSummaryCollection = this.AppendSignDateToDSUObjects(theCurrentRegisterServiceReq, theDSUDealSummaryCollection);
            //            }

            //            if (isCurrentDSUCollectionValid)
            //                return theDSUDealSummaryCollection;
            //        }
            //    }
            //}

            try
            {
                if (clientSignProcessDone)
                {
                    //DSULogger logController = new DSULogger();
                    theDSUDealSummaryCollection =
                        await _logController.ExportGeneratedDSULog(theCurrentRegisterServiceReq.Id.ToString(),
                            cancellationToken);

                    isCollectionValid = ValidateGeneratedDSUsCount(theCurrentRegisterServiceReq,
                        theDSUDealSummaryCollection, ref dealSummaryMessagesRef);
                    if (theDSUDealSummaryCollection != null && theDSUDealSummaryCollection.Any() && isCollectionValid)
                        return (theDSUDealSummaryCollection, dealSummaryMessagesRef);
                }

                if (isRestricted)
                {
                    if (isRemovingDSU)
                        (theDSUDealSummaryCollection, dealSummaryMessagesRef) =
                            await CreateRemovingRestrictedDSUsCollection(theCurrentRegisterServiceReq, cancellationToken,
                                dealSummaryMessagesRef);
                    else
                        (theDSUDealSummaryCollection, dealSummaryMessagesRef) =
                            await CreateRestrictedDSUsCollection(theCurrentRegisterServiceReq, cancellationToken,
                                dealSummaryMessagesRef);
                }
                else
                {
                    (theDSUDealSummaryCollection, dealSummaryMessagesRef) =
                        await CreateDeterministicDSUsCollection(theCurrentRegisterServiceReq, cancellationToken,
                            dealSummaryMessagesRef);

                    if (theCurrentRegisterServiceReq.DocumentTypeId == "115") // سند قطعي مشتمل بر رهن - غيرمنقول
                    {
                        List<DSUDealSummaryObject> theDSUDealSummaryCollectionRestricted = null;

                        (theDSUDealSummaryCollectionRestricted, dealSummaryMessagesRef) =
                            await CreateRestrictedDSUsCollection(theCurrentRegisterServiceReq, cancellationToken,
                                dealSummaryMessagesRef);
                        theDSUDealSummaryCollectionRestricted[0].DSUTransferTypeId = "1"; // رهنی

                        for (var i = 0; i < theDSUDealSummaryCollectionRestricted.Count; ++i)
                        {
                            theDSUDealSummaryCollectionRestricted[i].DSUTransferTypeId = "1"; // رهنی

                            theDSUDealSummaryCollectionRestricted[i].TheDSURealLegalPersonList[0].DSURelationKindId =
                                "102"; // ذينفع
                            theDSUDealSummaryCollectionRestricted[i].TheDSURealLegalPersonList[1].DSURelationKindId = "100";
                        }

                        theDSUDealSummaryCollection.AddRange(theDSUDealSummaryCollectionRestricted);
                    }
                }

                isCollectionValid = ValidateGeneratedDSUsCount(theCurrentRegisterServiceReq, theDSUDealSummaryCollection,
                    ref dealSummaryMessagesRef);
                if (!isCollectionValid)
                    return (null, dealSummaryMessagesRef);

                if (!theDSUDealSummaryCollection.Any())
                    return (theDSUDealSummaryCollection, dealSummaryMessagesRef);

                if (_DsuProcessActionType == ProcessActionType.DSUSending)
                {
                    theDSUDealSummaryCollection =
                        await CorrectInvalidDataOnDSUObjects(theDSUDealSummaryCollection, cancellationToken);
                    theDSUDealSummaryCollection = await AppendSignDateToDSUObjects(theCurrentRegisterServiceReq,
                        theDSUDealSummaryCollection, cancellationToken);
                    AppendUnsigedPersonsDescription(UnSignedPersonsList, ref theDSUDealSummaryCollection);
                }

                var exportManager = new ExportManager();
                var xml = exportManager.GenerateXML(theDSUDealSummaryCollection);
                theDSUDealSummaryCollection = exportManager.ExportDSUObjectFromXML(xml);

                return (theDSUDealSummaryCollection, dealSummaryMessagesRef);
            }
            catch (Exception ex)
            {
                dealSummaryMessagesRef += "\n[Exceptions]\n" + ex;
                return (null, dealSummaryMessagesRef);
            }
        }

        internal async Task<List<DSUDealSummarySignPacket>> ProvideSignPacketCollection(
            List<DSUDealSummaryObject> dsuDealSummaryCollection, CancellationToken cancellationToken,
            bool failedOldDSUs = false)
        {
            if (!dsuDealSummaryCollection.Any())
                return null;

            var signPacketCollection = new List<DSUDealSummarySignPacket>();

            string messages = null;

            foreach (var theOneDSUDealSummary in dsuDealSummaryCollection)
            {
                DSUDealSummarySignPacket singleSignPacket = null;
                (singleSignPacket, messages) =
                    await ProvideSingleSignPacket(theOneDSUDealSummary, cancellationToken, messages, failedOldDSUs);

                if (singleSignPacket != null)
                {
                    signPacketCollection.Add(singleSignPacket);
                }
            }

            return signPacketCollection;
        }

        internal async Task<bool> IsDSUSent4ThisRequest(Document theCurrentRegisterServiceReq,
            CancellationToken cancellationToken)
        {
            var excludedReqs = new List<string> { "139311151458000035" };

            if (excludedReqs.Any() && excludedReqs.Contains(theCurrentRegisterServiceReq.NationalNo))
                return false;

            var generatedDsuDealSummary = await _documentEstateDealSummaryGeneratedRepository
                .TableNoTracking.CountAsync(t => t.DocumentId == theCurrentRegisterServiceReq.Id &&
                                                 t.IsSent == NotaryGeneratedDealSummary.Sent.GetString(),
                    cancellationToken) > 0;

            if (!generatedDsuDealSummary)
            {
                if (await IsDSUSent4ThisRequestUsingCPMS(theCurrentRegisterServiceReq, cancellationToken))
                {
                    await _dsuLogger.UpdateDSULogStatus(theCurrentRegisterServiceReq.Id.ToString(),
                        NotaryGeneratedDealSummary.Sent, "Sent Recently...", cancellationToken);
                    return true;
                }

                return false;
            }

            return true;
        }

        internal async Task<bool> IsDSUSent4ThisRequestUsingCPMS(Document theCurrentRegisterServiceReq,
            CancellationToken cancellationToken)
        {
            var theInquiriesCollection = new List<Guid>();
            foreach (var theOneInquiry in theCurrentRegisterServiceReq.DocumentInquiries)
            {
                if (string.IsNullOrWhiteSpace(theOneInquiry.EstateInquiriesId))
                    continue;

                theInquiriesCollection.Add(Guid.Parse(theOneInquiry.EstateInquiriesId));
            }

            bool? isRestricted = null;
            var dsuTransferTypeID =
                Mapper.GetEquivalantDSUTransferTypeID(theCurrentRegisterServiceReq.DocumentTypeId, ref isRestricted);
            var summaryStatusCodes = new List<string> { "1", "3" };

            var classifyNo = theCurrentRegisterServiceReq.ClassifyNo == null
                ? null
                : theCurrentRegisterServiceReq.ClassifyNo.ToString();

            var theSentDSUsCollection = await _dealSummaryRepository.TableNoTracking.Include(t => t.WorkflowStates).Where(
                    t =>
                        t.NewNotaryDocumentId == theCurrentRegisterServiceReq.Id
                        && t.DealNo == classifyNo
                        && t.ScriptoriumId == theCurrentRegisterServiceReq.ScriptoriumId &&
                        t.DealSummaryTransferTypeId == dsuTransferTypeID
                        && theInquiriesCollection.Contains(t.Id) &&
                        (summaryStatusCodes.Contains(t.WorkflowStates.State)
                         || (t.WorkflowStates.State == "4" && t.Response == "\"خلاصه معامله مورد تاييد مي باشد\"")))
                .ToListAsync(cancellationToken);

            //Criteria dealNoCriteria = new Criteria();
            //dealNoCriteria.AddEqualTo ( Rad.CMS.EstateQuery.DSUDealSummary.NotaryDocumentNo, theCurrentRegisterServiceReq.ObjectId );
            //if ( theCurrentRegisterServiceReq.ClassifyNo != null )
            //    dealNoCriteria.AddOrEqualTo ( Rad.CMS.EstateQuery.DSUDealSummary.DealNo, theCurrentRegisterServiceReq.ClassifyNo );

            //Criteria criterira = new Criteria();

            //criterira.AddAndCriteria ( dealNoCriteria );
            //criterira.AddEqualTo ( Rad.CMS.EstateQuery.DSUDealSummary.DealSummeryIssuerId, theCurrentRegisterServiceReq.TheScriptorium.TheCMSOrganization.ObjectId );
            //criterira.AddEqualTo ( Rad.CMS.EstateQuery.DSUDealSummary.DSUTransferTypeId, dsuTransferTypeID );
            //criterira.AddIn ( Rad.CMS.EstateQuery.DSUDealSummary.ESTEstateInquiryId, theInquiriesCollection );

            //Criteria stateCriteria = new Criteria();
            //stateCriteria.AddIn ( Rad.CMS.EstateQuery.DSUDealSummary.SENDINGSTATUS, new string [ ] { "1", "3" } );
            //Criteria sentCriteria = new Criteria();
            //sentCriteria.AddEqualTo ( Rad.CMS.EstateQuery.DSUDealSummary.SENDINGSTATUS, "4" );
            //sentCriteria.AddEqualTo ( Rad.CMS.EstateQuery.DSUDealSummary.Response, "خلاصه معامله مورد تاييد مي باشد" );
            //stateCriteria.AddOrCriteria ( sentCriteria );

            //criterira.AddAndCriteria ( stateCriteria );

            //Rad.ssaa.DealSummeryClass.IDSUDealSummaryCollection theSentDSUsCollection = Rad.CMS.InstanceBuilder.GetEntityListByCriteria<Rad.ssaa.DealSummeryClass.IDSUDealSummary>(criterira) as Rad.ssaa.DealSummeryClass.IDSUDealSummaryCollection;

            if (theSentDSUsCollection.Any())
                return true;
            return false;
        }

        internal async Task<List<DSUDealSummaryObject>> RemoveSentDSUObjectsFromSignedCollection(
            Document theCurrentRegisterServiceReq, CancellationToken cancellationToken,
            List<DSUDealSummaryObject> signedDSUDealSummaryCollection, List<string> sentDSUsESTEStateInquiryIDs)
        {
            var signedDSUDealSummaryCollectionRef = signedDSUDealSummaryCollection;
            if (!sentDSUsESTEStateInquiryIDs.Any() || !signedDSUDealSummaryCollectionRef.Any())
                return signedDSUDealSummaryCollectionRef;

            Stack<DSUDealSummaryObject> removingObjects = null;

            foreach (var theOneDSUObject in signedDSUDealSummaryCollectionRef)
                if (theCurrentRegisterServiceReq.DocumentTypeId == "004") //فک رهن 
                {
                    if (sentDSUsESTEStateInquiryIDs.Contains(theOneDSUObject.Id))
                    {
                        if (removingObjects == null)
                            removingObjects = new Stack<DSUDealSummaryObject>();

                        removingObjects.Push(theOneDSUObject);
                    }
                }
                else
                {
                    if (sentDSUsESTEStateInquiryIDs.Contains(theOneDSUObject.ESTEstateInquiryId))
                    {
                        if (!Mapper.IsONotaryDocumentRestrictedType(theCurrentRegisterServiceReq.DocumentTypeId))
                        {
                            var sentDSUs = await _estateInquiryRepository.GetAllAsync(t =>
                                    t.EstateInquiryId == Guid.Parse(theOneDSUObject.ESTEstateInquiryId) &&
                                    (theOneDSUObject.NotaryDocumentNo == theCurrentRegisterServiceReq.Id.ToString()
                                     || theOneDSUObject.DealNo == theCurrentRegisterServiceReq.ClassifyNo.ToString()),
                                cancellationToken);

                            //If The Equivalant DSU be found on DB, so the DSU Is Sent and should be removed from Generated DSUs Collection.
                            if (sentDSUs.Any())
                            {
                                if (removingObjects == null)
                                    removingObjects = new Stack<DSUDealSummaryObject>();

                                removingObjects.Push(theOneDSUObject);
                            }
                        }
                        else
                        {
                            if (removingObjects == null)
                                removingObjects = new Stack<DSUDealSummaryObject>();

                            removingObjects.Push(theOneDSUObject);
                        }
                    }
                }

            DSUDealSummaryObject theOneRemovingObject = null;
            while (removingObjects != null && removingObjects.Any())
            {
                theOneRemovingObject = removingObjects.Pop();
                if (theOneRemovingObject != null)
                    signedDSUDealSummaryCollectionRef.Remove(theOneRemovingObject);
            }

            return signedDSUDealSummaryCollectionRef;
        }

        internal bool ValidateGeneratedDSUsCount(Document theCurrentRegisterServiceReq,
            List<DSUDealSummaryObject> generatedDSUsCollection, ref string validationMessage)
        {
            var isRemovingDSU = theCurrentRegisterServiceReq.DocumentTypeId == "004" ? true : false;
            if (isRemovingDSU)
                return true;

            var neededDSUsCount = CalculateNeededDSUsCount(theCurrentRegisterServiceReq);

            if (neededDSUsCount == 0 ||
                theCurrentRegisterServiceReq.DocumentTypeId == "115") // قطعی مشتمل بر رهن
                return true;

            if (generatedDSUsCollection==null || !generatedDSUsCollection.Any() || generatedDSUsCollection.Count != neededDSUsCount)
            {
                if (!string.IsNullOrWhiteSpace(validationMessage))
                    validationMessage += Environment.NewLine;

                validationMessage +=
                    "عدم تطابق تعداد خلاصه معامله های تولید شده با تعداد خلاصه معامله های مورد نیاز برای ارسال.\nارسال خلاصه معامله برای این سند توسط سیستم لغو گردید.";
                return false;
            }

            return true;
        }

        internal int CalculateNeededDSUsCount(Document theCurrentRegisterServiceReq)
        {
            var neededDSUsCount = 0;

            var isRestricted = Mapper.IsONotaryDocumentRestrictedType(theCurrentRegisterServiceReq.DocumentTypeId);

            foreach (var theOneRegCase in theCurrentRegisterServiceReq.DocumentEstates)
                foreach (var theOnePersonOwnershipDoc in theOneRegCase.DocumentEstateOwnershipDocuments)
                {
                    if (string.IsNullOrWhiteSpace(theOnePersonOwnershipDoc.EstateInquiriesId))
                        continue;

                    if (!isRestricted &&
                        theCurrentRegisterServiceReq.DocumentTypeId != "115") // سند قطعي مشتمل بر رهن - غيرمنقول)
                        neededDSUsCount++;
                    else
                        switch (theCurrentRegisterServiceReq.DocumentTypeId)
                        {
                            case "901": //قراداد پیش فروش 

                                foreach (var theOneDocPerson in theCurrentRegisterServiceReq.DocumentPeople)
                                {
                                    if (theOneDocPerson.IsOriginal != YesNo.Yes.GetString() ||
                                        theOneDocPerson.DocumentPersonType == null ||
                                        theOneDocPerson.DocumentPersonType.IsOwner != YesNo.No.GetString())
                                        continue;

                                    neededDSUsCount++;
                                }

                                break;

                            default:
                                foreach (var theOnePersonQuota in theOnePersonOwnershipDoc.DocumentEstateQuotaDetails)
                                    neededDSUsCount++;
                                break;
                        }
                }

            if (theCurrentRegisterServiceReq.DocumentTypeId == "115") // سند قطعي مشتمل بر رهن - غيرمنقول
                neededDSUsCount = neededDSUsCount * 2;

            return neededDSUsCount;
        }

        private DocumentEstateOwnershipDocument FindEquivalantOwnershipDocInDeterministicReq(
            List<Document> theDeterministicReqs, DocumentEstateOwnershipDocument theRestrictedOwnershipDoc)
        {
            var ownershipDocInquiryID = theRestrictedOwnershipDoc.EstateInquiriesId.Replace("@Duplicated", "");

            foreach (var theOneDeterRegServiceReq in theDeterministicReqs)
                foreach (var theOneRegCase in theOneDeterRegServiceReq.DocumentEstates)
                {
                    if (string.IsNullOrWhiteSpace(theOneRegCase.EstateInquiryId))
                        continue;

                    if (!theOneRegCase.EstateInquiryId.Contains(ownershipDocInquiryID))
                        continue;

                    foreach (var theOneDeterministicOwnershipDoc in theOneRegCase.DocumentEstateOwnershipDocuments)
                    {
                        if (string.IsNullOrWhiteSpace(theOneDeterministicOwnershipDoc.EstateInquiriesId))
                            continue;

                        if (theOneDeterministicOwnershipDoc.EstateInquiriesId.Contains(ownershipDocInquiryID))
                            return theOneDeterministicOwnershipDoc;
                    }
                }

            return null;
        }

        private async Task<List<Document>> FindRelatedDeterministicRegServices(Document theCurrentRegisterServiceReq,
            CancellationToken cancellationToken)
        {
            List<string> eSTEStateIdCollection = null;

            foreach (var theOneInquiry in theCurrentRegisterServiceReq.DocumentInquiries)
            {
                if (string.IsNullOrEmpty(theOneInquiry.EstateInquiriesId) ||
                    theOneInquiry.DocumentInquiryOrganizationId != "1")
                    continue;

                if (eSTEStateIdCollection == null)
                    eSTEStateIdCollection = new List<string>();

                if (!eSTEStateIdCollection.Contains(theOneInquiry.EstateInquiriesId))
                    eSTEStateIdCollection.Add(theOneInquiry.EstateInquiriesId);
            }

            if (!eSTEStateIdCollection.Any())
                return null;

            var foundUsedInquiryRegServicesCollecttion = await _documentRepository.FindRelatedDeterministicRegServices(
                _userService.UserApplicationContext.ScriptoriumInformation.Id
                , eSTEStateIdCollection, Mapper.DeterministicDocumentTypeCodes, theCurrentRegisterServiceReq.Id,
                cancellationToken);

            return foundUsedInquiryRegServicesCollecttion;
        }

        private async Task<List<DSUDealSummaryObject>> AppendSignDateToDSUObjects(Document theCurrentRegisterServiceReq,
            List<DSUDealSummaryObject> dsuDealSummaryCollection, CancellationToken cancellationToken)
        {
            if (!dsuDealSummaryCollection.Any())
                return dsuDealSummaryCollection;

            string message = null;
            foreach (var theOneDSUObject in dsuDealSummaryCollection)
                if (DigitalBookUtility.IsDigitalBookGeneratingPermitted(theCurrentRegisterServiceReq,
                        _clientConfiguration.ENoteBookEnabledDate, _clientConfiguration.IsENoteBookAutoClassifyNoEnabled,
                        ref message) == DigitalBookGeneratingPermissionStatus.Needed)
                {
                    string signDate;
                    (signDate, message) =
                        await GetLastFingerprintDateTime(theCurrentRegisterServiceReq, cancellationToken, message);
                    if (string.IsNullOrWhiteSpace(signDate))
                        signDate = theCurrentRegisterServiceReq.DocumentDate;
                    else
                        signDate = signDate.Substring(0, 10);

                    theOneDSUObject.SignDate = signDate;
                    theOneDSUObject.DealMainDate = theOneDSUObject.SignDate;
                    theOneDSUObject.DealDate = theOneDSUObject.SignDate;
                }
                else
                {
                    theOneDSUObject.SignDate = theCurrentRegisterServiceReq.SignDate;
                    theOneDSUObject.DealMainDate = theCurrentRegisterServiceReq.SignDate;
                    theOneDSUObject.DealDate = theCurrentRegisterServiceReq.SignDate;
                }

            return dsuDealSummaryCollection;
        }

        private async Task<List<DSUDealSummaryObject>> CorrectInvalidDataOnDSUObjects(
            List<DSUDealSummaryObject> dsuDealSummaryCollection, CancellationToken cancellationToken)
        {
            var dsuDealSummaryCollectionRef = dsuDealSummaryCollection;
            if (!dsuDealSummaryCollectionRef.Any())
                return dsuDealSummaryCollectionRef;

            foreach (var theOneDSUObject in dsuDealSummaryCollectionRef)
            {

                foreach (var theOneDSUPerson in theOneDSUObject.TheDSURealLegalPersonList)
                    if (!string.IsNullOrWhiteSpace(theOneDSUPerson.PostalCode) &&
                        !ValidatorsUtility.checkPostalCode(theOneDSUPerson.PostalCode))
                        theOneDSUPerson.PostalCode = "0000000000";

                if (theOneDSUObject.NotebookSeri != null && theOneDSUObject.NotebookSeri.Length > 20)
                {
                    var mainESTEStateInquiry =
                        await _estateInquiryRepository.GetAsync(t =>
                                t.EstateInquiryId == Guid.Parse(theOneDSUObject.ESTEstateInquiryId),
                            cancellationToken); // Rad.CMS.InstanceBuilder.GetEntityById<Rad.ssaa.ssaaClass.IESTEstateInquiry>(theOneDSUObject.ESTEstateInquiryId);
                    theOneDSUObject.NotebookSeri = mainESTEStateInquiry.EstateSeridaftar != null
                        ? mainESTEStateInquiry.EstateSeridaftar.SsaaCode
                        : null;
                }

            }

            return dsuDealSummaryCollectionRef;
        }

        private async Task<(List<DSUDealSummaryObject>, string)> CreateDeterministicDSUsCollection(
            Document theCurrentRegisterServiceReq, CancellationToken cancellationToken, string dealSummaryMessages)
        {
            var dealSummaryMessagesRef = dealSummaryMessages;
            List<DSUDealSummaryObject> theDSUDealSummaryCollection = null;

            foreach (var theOneRegCase in theCurrentRegisterServiceReq.DocumentEstates)
            {
                if (string.IsNullOrWhiteSpace(theOneRegCase.EstateInquiryId))
                    continue;

                foreach (var theOneOwnershipDoc in theOneRegCase.DocumentEstateOwnershipDocuments)
                {
                    if (string.IsNullOrWhiteSpace(theOneOwnershipDoc.EstateInquiriesId))
                        continue;

                    //In Order To Find @Duplicated OwnershipDocs
                    var estateObjectID = theOneOwnershipDoc.EstateInquiriesId;
                    if (estateObjectID.Contains("@Duplicated"))
                        estateObjectID = estateObjectID.Replace("@Duplicated", "");

                    var theMainESTEstateInquiry =
                        await _estateInquiryRepository.TableNoTracking.Include(t => t.EstateInquiryPeople)
                            .FirstOrDefaultAsync(t => t.Id == Guid.Parse(estateObjectID),
                                cancellationToken);

                    if (theMainESTEstateInquiry == null)
                    {
                        dealSummaryMessagesRef =
                            "خطا در دریافت استعلام ملک مطابق با " + theOneOwnershipDoc.OwnershipDocTitle();
                        return (null, dealSummaryMessagesRef);
                    }

                    var thePersonQuotaCollection = new List<DocumentEstateQuotaDetail>();
                    foreach (var theOnePersonQuota in theOneOwnershipDoc.DocumentEstateQuotaDetails)
                        thePersonQuotaCollection.Add(theOnePersonQuota);
                    DSUDealSummaryObject theOneDSUDealSummary;
                    (theOneDSUDealSummary, dealSummaryMessagesRef) = await CreateIndividualDSUDealSummary(
                        thePersonQuotaCollection, theMainESTEstateInquiry, YesNo.No, cancellationToken,
                        dealSummaryMessagesRef);

                    if (theOneDSUDealSummary != null)
                    {
                        if (theDSUDealSummaryCollection == null)
                            theDSUDealSummaryCollection = new List<DSUDealSummaryObject>();

                        theDSUDealSummaryCollection.Add(theOneDSUDealSummary);
                    }
                }
            }

            return (theDSUDealSummaryCollection, dealSummaryMessagesRef);
        }

        private async Task<(List<DSUDealSummaryObject>, string)> CreateRestrictedDSUsCollection(
            Document theCurrentRegisterServiceReq, CancellationToken cancellationToken, string dealSummaryMessages)
        {
            var dealSummaryMessagesRef = dealSummaryMessages;
            List<DSUDealSummaryObject> theDSUDealSummaryCollection = null;

            foreach (var theOneRegCase in theCurrentRegisterServiceReq.DocumentEstates)
            {
                if (string.IsNullOrWhiteSpace(theOneRegCase.EstateInquiryId))
                    continue;

                var estateInquiriesIds =
                    theOneRegCase.DocumentEstateOwnershipDocuments.Select(t => new { t.EstateInquiriesId, t.Id });

                var estateInquiries = new List<EstateInquiry>();
                if (estateInquiriesIds.Count() > 0)
                    estateInquiries = await _estateInquiryRepository
                        .GetAllAsync(
                            t => estateInquiriesIds.Select(t => t.EstateInquiriesId.ToString())
                                .Contains(t.EstateInquiryId.ToString()), cancellationToken);

                foreach (var theOneOwnershipDoc in theOneRegCase.DocumentEstateOwnershipDocuments)
                {
                    if (string.IsNullOrWhiteSpace(theOneOwnershipDoc.EstateInquiriesId))
                        continue;

                    //In Order To Find @Duplicated OwnershipDocs
                    var estateObjectID = theOneOwnershipDoc.EstateInquiriesId;
                    if (estateObjectID.Contains("@Duplicated")) estateObjectID = estateObjectID.Replace("@Duplicated", "");

                    var theMainESTEstateInquiry = estateInquiries.FirstOrDefault(t => t.Id == Guid.Parse(estateObjectID));
                    if (theMainESTEstateInquiry == null)
                    {
                        dealSummaryMessagesRef =
                            "خطا در دریافت استعلام ملک مطابق با " + theOneOwnershipDoc.OwnershipDocTitle();
                        return (null, dealSummaryMessagesRef);
                    }

                    ///در تولید خلاصه معامله برای اسناد از نوع محدودیت، برخی انواع سند تفاوت رفتاری دارند.
                    ///برای اسناد از نوع (قرارداد پیش فروش ساختمان) باید خلاصه معامله با روش دیگری تولید گردد.
                    ///در خلاصه معامله قرارداد پیش فروش ساختمان به ازای هر سند مالکیت و خریدار، خلاصه معامله تولید می گردد. 
                    ///در حالی که برای سایر اسناد رهنی، خلاصه معامله به ازای هر سهم تعریف شده تولید می گردد
                    ///در اسناد قرارداد پیش فروش چون سهمی وجود ندارد لذا این معماری انتخاب و طراحی گردیده است.
                    switch (theCurrentRegisterServiceReq.DocumentTypeId)
                    {

                        case "901": // قرارداد پیش فروش 
                            List<DSUDealSummaryObject> presellDSUsCollection;
                            (presellDSUsCollection, dealSummaryMessagesRef) =
                                await CreatePresellDSUDealSummariesCollection(theOneOwnershipDoc, theMainESTEstateInquiry,
                                    cancellationToken, dealSummaryMessagesRef);
                            if (!presellDSUsCollection.Any() || !string.IsNullOrWhiteSpace(dealSummaryMessagesRef))
                                return (null, dealSummaryMessagesRef);

                            if (theDSUDealSummaryCollection == null)
                                theDSUDealSummaryCollection = new List<DSUDealSummaryObject>();

                            theDSUDealSummaryCollection.AddRange(presellDSUsCollection);

                            break;

                        default:

                            //For Restricted Documents Each Quota Creates A Single DSUObject
                            //1 Quota = 1 DSUObject
                            foreach (var theOnePersonQuota in theOneOwnershipDoc.DocumentEstateQuotaDetails)
                            {
                                var thePersonQuotaCollection = new List<DocumentEstateQuotaDetail>();
                                thePersonQuotaCollection.Add(theOnePersonQuota);
                                DSUDealSummaryObject theOneDSUDealSummary;
                                (theOneDSUDealSummary, dealSummaryMessagesRef) =
                                    await CreateIndividualDSUDealSummary(thePersonQuotaCollection, theMainESTEstateInquiry,
                                        YesNo.Yes, cancellationToken, dealSummaryMessagesRef);

                                if (theOneDSUDealSummary != null)
                                {
                                    if (theCurrentRegisterServiceReq.State ==
                                        NotaryRegServiceReqState.FinalPrinted.GetString() &&
                                        !string.IsNullOrEmpty(theCurrentRegisterServiceReq.SignDate))
                                        theOneDSUDealSummary.SignDate = theCurrentRegisterServiceReq.SignDate;

                                    if (theDSUDealSummaryCollection == null)
                                        theDSUDealSummaryCollection = new List<DSUDealSummaryObject>();

                                    theDSUDealSummaryCollection.Add(theOneDSUDealSummary);
                                }
                            }

                            break;

                    }
                }
            }

            return (theDSUDealSummaryCollection, dealSummaryMessagesRef);
        }

        private async Task<(List<DSUDealSummaryObject>, string)> CreateRemovingRestrictedDSUsCollection(
            Document theCurrentRegisterServiceReq, CancellationToken cancellationToken, string dealSummaryMessages)
        {
            var dealSummaryMessagesRef = dealSummaryMessages;
            List<DSUDealSummaryObject> theRemovingDSUDealSummaryCollection = null;

            var userSelectedDSUsCollection =
                await ProvideUserSelectedDSUsXML(theCurrentRegisterServiceReq, cancellationToken);

            if (userSelectedDSUsCollection!=null &&  userSelectedDSUsCollection.Any())
            {
                foreach (var theOneSelectedDSU in userSelectedDSUsCollection)
                {
                    var theNewRemovingDSUObject = CreateIndividualRemovingDSUDealSummary(theOneSelectedDSU,
                        theCurrentRegisterServiceReq, ref dealSummaryMessagesRef);
                    if (theNewRemovingDSUObject == null)
                        continue;

                    if (theRemovingDSUDealSummaryCollection == null)
                        theRemovingDSUDealSummaryCollection = new List<DSUDealSummaryObject>();

                    theRemovingDSUDealSummaryCollection.Add(theNewRemovingDSUObject);
                }
            }
            else
            {
                return (null, dealSummaryMessagesRef);

                //Create Removing DSUs using basic data which is available in Request.
                foreach (var theOneRegCase in theCurrentRegisterServiceReq.DocumentEstates)
                    foreach (var theOneOwnershipDoc in theOneRegCase.DocumentEstateOwnershipDocuments)
                    {
                        if (theRemovingDSUDealSummaryCollection == null)
                            theRemovingDSUDealSummaryCollection = new List<DSUDealSummaryObject>();

                        var theNewDSU = CreateIndividualRemovingDSUDealSummary(theOneOwnershipDoc, ref dealSummaryMessagesRef);
                        theRemovingDSUDealSummaryCollection.Add(theNewDSU);
                    }
            }

            return (theRemovingDSUDealSummaryCollection, dealSummaryMessagesRef);
        }

        private async Task<(DSUDealSummarySignPacket, string)> ProvideSingleSignPacket(
            DSUDealSummaryObject theOneDSUDealSummaryObject, CancellationToken cancellationToken,
            string dealSummaryMessages, bool failedOldDSU = false)
        {
            var dealSummaryMessagesRef = dealSummaryMessages;
            DSUDealSummarySignPacket singleSignPacket = null;

            var dealSummaryToByteArrayInput = new DealSummaryToByteArrayInput();
            dealSummaryToByteArrayInput.DSUDealSummaryObject = theOneDSUDealSummaryObject;
            byte[] rawDataByteArray = null;
            var response = await _mediator.Send(dealSummaryToByteArrayInput, cancellationToken);
            if (response.IsSuccess) rawDataByteArray = response.Data;

            if (rawDataByteArray == null)
                return (null, dealSummaryMessagesRef);

            singleSignPacket = new DSUDealSummarySignPacket();
            singleSignPacket.RawDataByteArray = rawDataByteArray;
            singleSignPacket.RawDataB64 = Convert.ToBase64String(singleSignPacket.RawDataByteArray);
            var standardizedByte = Encoding.Unicode.GetBytes(singleSignPacket.RawDataB64);
            singleSignPacket.RawDataB64 = Convert.ToBase64String(standardizedByte);
            singleSignPacket.FailedOldDeterministicDSU = failedOldDSU;
            singleSignPacket.RegisterServiceReqObjectID = theOneDSUDealSummaryObject.NotaryDocumentNo;

            return (singleSignPacket, dealSummaryMessagesRef);
        }

        private async Task<(DSUDealSummaryObject, string)> CreateIndividualDSUDealSummary(
            List<DocumentEstateQuotaDetail> quotasCollection, EstateInquiry? theMainESTEstateInquiry, YesNo isRahni,
            CancellationToken cancellationToken, string dsuGenerationMessages)
        {
            var dsuGenerationMessagesRef = dsuGenerationMessages;
            DSUDealSummaryObject theOneDSUDealSummary;
            (theOneDSUDealSummary, dsuGenerationMessagesRef) = await CreateIndividualDSUDealSummaryMainBody(
                quotasCollection[0].DocumentEstate.Document, theMainESTEstateInquiry, cancellationToken,
                dsuGenerationMessagesRef);

            bool? isRestricted = false;
            Mapper.GetEquivalantDSUTransferTypeID(quotasCollection[0].DocumentEstate.Document.DocumentTypeId,
                ref isRestricted);
            //if (quotasCollection[0].TheONotaryRegCase.TheONotaryRegisterServiceReq.TheONotaryDocumentType.Code == "115")    // قطعی مشتمل بر رهن غیرمنقول
            //{
            //    if (isRahni == YesNo.Yes)
            //        isRestricted = true;
            //    else
            //        isRestricted = false;
            //}

            _dSUPersonsManager._IsRestricted = isRestricted;
            _dSUPersonsManager._DsuCurrentActionType = _DsuProcessActionType;
            List<DSURealLegalPersonObject> dealPersonsCollection;
            (dealPersonsCollection, theOneDSUDealSummary, dsuGenerationMessagesRef) =
                await _dSUPersonsManager.CreateAllDSUPersonsCollection(quotasCollection, theMainESTEstateInquiry,
                    cancellationToken, theOneDSUDealSummary, dsuGenerationMessagesRef);

            if (dealPersonsCollection.Any())
            {
                if (theOneDSUDealSummary.TheDSURealLegalPersonList == null)
                    theOneDSUDealSummary.TheDSURealLegalPersonList = new List<DSURealLegalPersonObject>();

                theOneDSUDealSummary.TheDSURealLegalPersonList.AddRange(dealPersonsCollection);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(dsuGenerationMessagesRef))
                    dsuGenerationMessagesRef += "خطا در ایجاد اشخاص خلاصه معامله!";

                return (null, dsuGenerationMessagesRef);
            }

            return (theOneDSUDealSummary, dsuGenerationMessagesRef);
        }

        private async Task<(List<DSUDealSummaryObject>, string)> CreatePresellDSUDealSummariesCollection(
            DocumentEstateOwnershipDocument theOwnershipDoc, EstateInquiry theMainESTEstateInquiry,
            CancellationToken cancellationToken, string dsuGenerationMessages)
        {
            var dsuGenerationMessagesRef = dsuGenerationMessages;
            var dsusCollection = new List<DSUDealSummaryObject>();

            //Presell DSU Generation Flow : 
            // * Each DSU packet contains 2 persons. 1st : Owner (Generated From Inquiry), 2nd : Buyer
            // * Since the DSU packet contains only 2 persons, it is needed to loop through buyers in order to generate DSUs.
            // * 1 Buyer = 1 DSU
            foreach (var theOneBuyerPerson in theOwnershipDoc.DocumentEstate.Document.DocumentPeople)
            {
                if (theOneBuyerPerson.IsOriginal != YesNo.Yes.GetString() ||
                    theOneBuyerPerson.DocumentPersonType == null ||
                    theOneBuyerPerson.DocumentPersonType.IsOwner != YesNo.No.GetString())
                    continue;

                // Generating DSU packet parent object
                DSUDealSummaryObject theOneDSUDealSummary;
                (theOneDSUDealSummary, dsuGenerationMessagesRef) = await CreateIndividualDSUDealSummaryMainBody(
                    theOwnershipDoc.DocumentEstate.Document, theMainESTEstateInquiry, cancellationToken,
                    dsuGenerationMessagesRef);

                // Generating DSU PersonsCollection
                _dSUPersonsManager._IsRestricted = true;
                _dSUPersonsManager._DsuCurrentActionType = _DsuProcessActionType;
                List<DSURealLegalPersonObject> dealPersonsCollection;
                (dealPersonsCollection, theOneDSUDealSummary, dsuGenerationMessagesRef) =
                    await _dSUPersonsManager.CreateAllDSUPersons4Presell(theOneBuyerPerson, theMainESTEstateInquiry,
                        theOwnershipDoc.DocumentEstate.Document, cancellationToken, theOneDSUDealSummary,
                        dsuGenerationMessagesRef);

                if (dealPersonsCollection.Any())
                {
                    if (theOneDSUDealSummary.TheDSURealLegalPersonList == null)
                        theOneDSUDealSummary.TheDSURealLegalPersonList = new List<DSURealLegalPersonObject>();

                    theOneDSUDealSummary.TheDSURealLegalPersonList.AddRange(dealPersonsCollection);
                    dsusCollection.Add(theOneDSUDealSummary);
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(dsuGenerationMessagesRef))
                        dsuGenerationMessagesRef += "خطا در ایجاد اشخاص خلاصه معامله!";

                    return (null, dsuGenerationMessagesRef);
                }
            }

            return (dsusCollection, dsuGenerationMessagesRef);
        }

        private DSUDealSummaryObject CreateIndividualRemovingDSUDealSummary(
            DocumentEstateDealSummarySelected theOneDSUXmlObject, Document theCurrentRegisterServiceReq,
            ref string dsuGenerationMessages)
        {
            var exportManager = new ExportManager();
            var theOneDSUObject = exportManager.ExportIndividualDSUObjectFromXML(theOneDSUXmlObject.Xml);

            theOneDSUObject.RemoveRestrictionDate = theCurrentRegisterServiceReq.SignDate;
            theOneDSUObject.RemoveRestrictionNo = theCurrentRegisterServiceReq.NationalNo;
            theOneDSUObject.unrestrictedOrganizationId = theCurrentRegisterServiceReq.ScriptoriumId;
            theOneDSUObject.DSURemoveRestirctionTypeId = "B8B9ABF3-26E1-41A1-B01F-A01D996A";

            return theOneDSUObject;
        }

        private DSUDealSummaryObject CreateIndividualRemovingDSUDealSummary(
            DocumentEstateOwnershipDocument thePersonOwnershipDoc, ref string dsuGenerationMessages)
        {
            var theDSUObject = new DSUDealSummaryObject();

            theDSUObject.NotaryDocumentNo = thePersonOwnershipDoc.DocumentEstate.DocumentId.ToString();
            theDSUObject.PostalCode = thePersonOwnershipDoc.DocumentEstate.PostalCode;
            theDSUObject.RegisterNo = thePersonOwnershipDoc.EstateSabtNo;
            theDSUObject.PrintNumberDoc = thePersonOwnershipDoc.NotaryDocumentNo;
            theDSUObject.OfficeNo = thePersonOwnershipDoc.EstateBookNo;
            theDSUObject.PageNo = thePersonOwnershipDoc.EstateBookPageNo;
            theDSUObject.Basic = thePersonOwnershipDoc.DocumentEstate.BasicPlaque;
            theDSUObject.Secondary = thePersonOwnershipDoc.DocumentEstate.SecondaryPlaque;
            theDSUObject.GeoLocationId = thePersonOwnershipDoc.DocumentEstate.GeoLocationId.ToString();
            theDSUObject.DealSummeryIssuerId = thePersonOwnershipDoc.DocumentEstate.Document.ScriptoriumId;

            return theDSUObject;
        }

        private async Task<(DSUDealSummaryObject, string)> CreateIndividualDSUDealSummaryMainBody(
            Document theCurrentRegisterServiceReq, EstateInquiry theMainESTEstateInquiry,
            CancellationToken cancellationToken, string dsuGenerationMessages)
        {
            var dsuGenerationMessagesRef = dsuGenerationMessages;
            var theOneDSUDealSummary = new DSUDealSummaryObject();

            bool? isRestricted = false;

            theOneDSUDealSummary.DSUTransferTypeId =
                Mapper.GetEquivalantDSUTransferTypeID(theCurrentRegisterServiceReq.DocumentTypeId, ref isRestricted);

            if (theCurrentRegisterServiceReq.CurrencyTypeId == null)
            {
                theOneDSUDealSummary.AmountUnitId = "10"; //ریال
            }
            else
            {
                MeasurementUnitTypeByIdViewModel? measurementUnitTypes = null;

                var lst = new List<string>();
                lst.Add(theCurrentRegisterServiceReq.CurrencyTypeId);
                measurementUnitTypes = await GetMeasurementUnitTypes(lst.ToArray(), cancellationToken);

                theOneDSUDealSummary.AmountUnitId = measurementUnitTypes?.MesurementUnitTypeList[0]?.Id;
            }

            //قرارداد پیش فروش هزینه اش از مبلغ سند اخذ می گردد
            //در حالی که برای بقیه اسناد مبلغ، از مورد معامله برداشته می شود و در هنگام ساخت اشخاص پر می گردد.
            //همچنین توضیحات مربوط به خلاصه معامله، از متن خلاصه موجود در سایر اطلاعات سند، اخذ می گردد.
            if (theCurrentRegisterServiceReq.DocumentTypeId == "901") //قرارداد پیش فروش
            {
                theOneDSUDealSummary.Amount = (long?)theCurrentRegisterServiceReq.Price;

                if (!string.IsNullOrWhiteSpace(theCurrentRegisterServiceReq.DocumentInfoText.ConditionsText) &&
                    theCurrentRegisterServiceReq.DocumentInfoText.ConditionsText.Length > 2000)
                {
                    dsuGenerationMessagesRef = "متن توضیحات مربوط به خلاصه معامله نباید بیشتر از 2000 کاراکتر باشد.";
                    return (null, dsuGenerationMessagesRef);
                }

                theOneDSUDealSummary.Description = theCurrentRegisterServiceReq.DocumentInfoText.ConditionsText;
            }

            theOneDSUDealSummary.DealNo = theCurrentRegisterServiceReq.ClassifyNo.ToString();

            string digitalBookMessage = null;
            if (string.IsNullOrWhiteSpace(theOneDSUDealSummary.DealNo) &&
                _clientConfiguration.IsENoteBookAutoClassifyNoEnabled)
            {
                decimal? nextClassifyNo;
                (nextClassifyNo, digitalBookMessage) =
                    await _eNoteBookServerController.ProvideNextClassifyNo(theCurrentRegisterServiceReq, cancellationToken,
                        digitalBookMessage);
                theOneDSUDealSummary.DealNo = nextClassifyNo.HasValue ? nextClassifyNo.ToString() : "0";
            }

            //theOneDSUDealSummary.DealMainDate = _dateTimeService.CurrentTime;
            theOneDSUDealSummary.DealDate = _dateTimeService.CurrentPersianDate;
            theOneDSUDealSummary.ESTEstateInquiryId = theMainESTEstateInquiry.Id.ToString();
            theOneDSUDealSummary.Basic = theMainESTEstateInquiry.Basic;
            theOneDSUDealSummary.BasicAppendant = theMainESTEstateInquiry.BasicRemaining;
            theOneDSUDealSummary.Secondary = theMainESTEstateInquiry.Secondary;
            theOneDSUDealSummary.SecondaryAppendant = theMainESTEstateInquiry.SecondaryRemaining;
            //theOneDSUDealSummary.Address = theMainESTEstateInquiry.Address; //Is Being Fetched From TheRegCase
            theOneDSUDealSummary.Area = theMainESTEstateInquiry.Area;
            theOneDSUDealSummary.GeoLocationId = theMainESTEstateInquiry.GeoLocationId.ToString();
            theOneDSUDealSummary.SectionId = theMainESTEstateInquiry.EstateSectionId;
            theOneDSUDealSummary.SubsectionId = theMainESTEstateInquiry.EstateSubsectionId;
            theOneDSUDealSummary.DealSummeryIssueeId = theMainESTEstateInquiry.UnitId;
            theOneDSUDealSummary.DealSummeryIssuerId = theMainESTEstateInquiry.ScriptoriumId;
            theOneDSUDealSummary.OfficeNo = theMainESTEstateInquiry.EstateNoteNo;
            theOneDSUDealSummary.RegisterNo = theMainESTEstateInquiry.RegisterNo;
            theOneDSUDealSummary.PrintNumberDoc = theMainESTEstateInquiry.DocPrintNo;
            theOneDSUDealSummary.BSTSeriDaftarId = theMainESTEstateInquiry.EstateSeridaftarId;

            theOneDSUDealSummary.PageNoteSystemNo =
                theMainESTEstateInquiry.ElectronicEstateNoteNo; //Due to changes on 1395/04/02

            //"theMainESTEstateInquiry.BSTSeriDaftarId" Has Been Changed To "theMainESTEstateInquiry.TheBSTSeriDaftar.SSAACode" On 13930611 By Mr. Khateri Recommendation
            theOneDSUDealSummary.NotebookSeri = theMainESTEstateInquiry.EstateSeridaftar != null
                ? theMainESTEstateInquiry.EstateSeridaftar.SsaaCode
                : null;
            theOneDSUDealSummary.PageNo = theMainESTEstateInquiry.PageNo;
            theOneDSUDealSummary.PostalCode = theMainESTEstateInquiry.EstatePostalCode;
            theOneDSUDealSummary.NotaryDocumentNo = theCurrentRegisterServiceReq.Id.ToString();

            if (isRestricted == true) // محدودیت. رهنی
            {
                theOneDSUDealSummary.Duration = theCurrentRegisterServiceReq.DocumentInfoOther.MortageDuration;
                theOneDSUDealSummary.TimeUnitId = theCurrentRegisterServiceReq.DocumentInfoOther.MortageTimeUnitId;
            }

            return (theOneDSUDealSummary, dsuGenerationMessagesRef);
        }

        private async Task<List<DocumentEstateDealSummarySelected>> ProvideUserSelectedDSUsXML(
            Document theCurrentRegisterServiceReq, CancellationToken cancellationToken)
        {
            List<DocumentEstateDealSummarySelected> userSelectedDSUsCollection = null;

            userSelectedDSUsCollection =
                await _documentEstateDealSummarySelectedRepository.GetAllAsync(t =>
                    t.DocumentId == theCurrentRegisterServiceReq.Id, cancellationToken);

            if (!userSelectedDSUsCollection.Any())
                return null;

            var exportManager = new ExportManager();
            var theGeneratedDSUsFromProxy = new List<RestrictionDealSummaryListItem>();
            List<string> theDSUIDsCollection = null;

            foreach (var theOneSelectedDSULog in userSelectedDSUsCollection)
                if (!string.IsNullOrWhiteSpace(theOneSelectedDSULog.Xml) &&
                    !theOneSelectedDSULog.Xml.StartsWith("<?xml version="))
                {
                    if (theDSUIDsCollection == null)
                        theDSUIDsCollection = new List<string>();

                    theDSUIDsCollection.Add(theOneSelectedDSULog.Xml);
                }
            //else
            //{
            //    if (theDSUIDsCollection == null)
            //        theDSUIDsCollection = new List<string>();
            //    DSUDealSummaryObject theOneDSUObject = exportManager.ExportIndividualDSUObjectFromXML(theOneSelectedDSULog.XmlDSU);
            //    theGeneratedDSUsFromProxy.Add(theOneDSUObject);
            //}
            //Get DSU Objects From Proxy Then Update the "UserSelectedDSUsCollection"

            if (theDSUIDsCollection.Any())
            {
                var dsuRequestMessage = new RestrictionDealSummaryLookupQuery();
                dsuRequestMessage.PageSize = 10000;
                dsuRequestMessage.PageIndex = 1;
                dsuRequestMessage.ExtraParams = new RestrictionDealSummaryLookupQueryExtraParam();
                dsuRequestMessage.ExtraParams.DealSummaryId = theDSUIDsCollection;
                dsuRequestMessage.ExtraParams.ScriptoriumId = theCurrentRegisterServiceReq.ScriptoriumId;

                var response = await _mediator.Send(dsuRequestMessage, cancellationToken);
                if (response.IsSuccess) theGeneratedDSUsFromProxy = response.Data.GridItems;

                foreach (var theOneGeneratedDSU in theGeneratedDSUsFromProxy)
                    foreach (var theOneSelectedDSULog in userSelectedDSUsCollection)
                        if (theOneSelectedDSULog.Xml == theOneGeneratedDSU.DealSummaryId)
                        {
                            theOneSelectedDSULog.Xml = exportManager.GenerateXML(theOneGeneratedDSU);
                            break;
                        }
            }

            return userSelectedDSUsCollection;
        }

        private DSUDealSummarySignPacket FindEquivalanteSignPacket(string rawDataB64,
            List<DSUDealSummarySignPacket> signedPacketsCollection)
        {
            if (!signedPacketsCollection.Any())
                return null;

            foreach (var theOneSignedPacket in signedPacketsCollection)
                if (rawDataB64 == theOneSignedPacket.RawDataB64)
                    return theOneSignedPacket;

            return null;
        }

        private async Task<(string, string)> GetLastFingerprintDateTime(Document theCurrentReq,
            CancellationToken cancellationToken, string messages)
        {
            var messagesRef = messages;

            var lastsigndate =
                await _personFingerprintRepository.GetLastFingerprintDateTime(theCurrentReq.Id.ToString(),
                    cancellationToken);

            return (lastsigndate, messagesRef);
        }

        private void AppendUnsigedPersonsDescription(List<DocumentPerson> theUnsignedPersonsColletion,
            ref List<DSUDealSummaryObject> dsuDealSummaryCollection)
        {
            if (theUnsignedPersonsColletion == null || !theUnsignedPersonsColletion.Any())
                return;

            var unsignedText = "اشخاصی که سند را امضاء نکرده اند: " + Environment.NewLine;

            foreach (var theOneDocPerson in theUnsignedPersonsColletion)
                unsignedText += "، " + theOneDocPerson.FullName();

            foreach (var theOneDSUObject in dsuDealSummaryCollection)
                if (string.IsNullOrWhiteSpace(theOneDSUObject.Description))
                    theOneDSUObject.Description += unsignedText;
                else
                    theOneDSUObject.Description += Environment.NewLine + unsignedText;
        }

        public async Task<MeasurementUnitTypeByIdViewModel> GetMeasurementUnitTypes(string[] legacyId,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new MeasurementUnitTypeByLegacyIdQuery(legacyId), cancellationToken);
            if (response.IsSuccess)
                return response.Data;
            return null;
        }

    }


}
