namespace Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Validators.Shell
{
    using Notary.SSAA.BO.Configuration;
    using Notary.SSAA.BO.DataTransferObject.Coordinators.NotaryDocument;
    using Notary.SSAA.BO.Domain.Abstractions;
    using Notary.SSAA.BO.Domain.RichDomain.Document;
    using Notary.SSAA.BO.SharedKernel.Contracts.Coordinator.Document;
    using Notary.SSAA.BO.SharedKernel.Enumerations;
    using Notary.SSAA.BO.SharedKernel.Interfaces;
    using Notary.SSAA.BO.Utilities.Extensions;
    using Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Validators.Core;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Document = Notary.SSAA.BO.Domain.Entities.Document;

    /// <summary>
    /// Defines the <see cref="AgentDocumentValidator" />
    /// </summary>
    public class AgentDocumentValidator
    {
        /// <summary>
        /// Defines the agentDocValidatorConfiguration
        /// </summary>
        private readonly AgentDocValidatorConfiguration agentDocValidatorConfiguration;

        /// <summary>
        /// Defines the documentPersonRepository
        /// </summary>
        private readonly IDocumentPersonRepository documentPersonRepository;

        /// <summary>
        /// Defines the documentRepository
        /// </summary>
        private readonly IDocumentRepository documentRepository;

        /// <summary>
        /// Defines the documentTypeRepository
        /// </summary>
        private readonly IDocumentTypeRepository documentTypeRepository;

        /// <summary>
        /// Defines the documentPersonRelatedRepository
        /// </summary>
        private readonly IDocumentPersonRelatedRepository documentPersonRelatedRepository;

        /// <summary>
        /// Defines the userService
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// Defines the dateTimeService
        /// </summary>
        private readonly IDateTimeService dateTimeService;

        /// <summary>
        /// Defines the validatorCore
        /// </summary>
        private readonly AgentDocumentValidatorCore validatorCore;

        /// <summary>
        /// Initializes a new instance of the <see cref="AgentDocumentValidator"/> class.
        /// </summary>
        /// <param name="_documentRepository">The _documentRepository<see cref="IDocumentRepository"/></param>
        /// <param name="_documentTypeRepository">The _documentTypeRepository<see cref="IDocumentTypeRepository"/></param>
        /// <param name="_userService">The _userService<see cref="IUserService"/></param>
        /// <param name="_dateTimeService">The _dateTimeService<see cref="IDateTimeService"/></param>
        /// <param name="_documentPersonRepository">The _documentPersonRepository<see cref="IDocumentPersonRepository"/></param>
        /// <param name="_documentPersonRelatedRepository">The _documentPersonRelatedRepository<see cref="IDocumentPersonRelatedRepository"/></param>
        /// <param name="_agentDocValidatorConfiguration">The _agentDocValidatorConfiguration<see cref="AgentDocValidatorConfiguration"/></param>
        /// <param name="_validatorCore">The _validatorCore<see cref="AgentDocumentValidatorCore"/></param>
        public AgentDocumentValidator ( IDocumentRepository _documentRepository, IDocumentTypeRepository _documentTypeRepository,
            IUserService _userService, IDateTimeService _dateTimeService,
            IDocumentPersonRepository _documentPersonRepository, IDocumentPersonRelatedRepository _documentPersonRelatedRepository,
            AgentDocValidatorConfiguration _agentDocValidatorConfiguration, AgentDocumentValidatorCore _validatorCore )
        {
            documentRepository = _documentRepository;
            documentTypeRepository = _documentTypeRepository;
            userService = _userService;
            documentPersonRepository = _documentPersonRepository;
            documentPersonRelatedRepository = _documentPersonRelatedRepository;
            dateTimeService = _dateTimeService;
            agentDocValidatorConfiguration = _agentDocValidatorConfiguration;
            validatorCore = _validatorCore;
        }

        /// <summary>
        /// The ValidateAgentDocument
        /// </summary>
        /// <param name="inputAgentDocumentNationalNo">The inputAgentDocumentNationalNo<see cref="string"/></param>
        /// <param name="inputAgentDocumentScriptoriumId">The inputAgentDocumentScriptoriumId<see cref="string"/></param>
        /// <param name="inputAgentDocumentDate">The inputAgentDocumentDate<see cref="string"/></param>
        /// <param name="inputDocumentTypeID">The inputDocumentTypeID<see cref="string"/></param>
        /// <param name="inputMovakelNationalNo">The inputMovakelNationalNo<see cref="string"/></param>
        /// <param name="inputVakilNationalNo">The inputVakilNationalNo<see cref="string"/></param>
        /// <param name="inputMovakelFullName">The inputMovakelFullName<see cref="string"/></param>
        /// <param name="inputVakilFullName">The inputVakilFullName<see cref="string"/></param>
        /// <param name="inputRegCaseList">The inputRegCaseList<see cref="List{RegCasePacket}"/></param>
        /// <returns>The <see cref="Task{AgentDocValidationResult}"/></returns>
        public async Task<AgentDocValidationResult> ValidateAgentDocument (
                                                              string inputAgentDocumentNationalNo,
                                                              string inputAgentDocumentScriptoriumId,
                                                              string inputAgentDocumentDate,
                                                              string inputDocumentTypeID,
                                                              string inputMovakelNationalNo,
                                                              string inputVakilNationalNo,
                                                              string inputMovakelFullName,
                                                              string inputVakilFullName,
                                                              List<RegCasePacket> inputRegCaseList
                                                              )
        {

            List<AgentDocValidationResult> mainValidationResultCollection = new List<AgentDocValidationResult>();

            AgentDocValidationResult singleValidationResult = new AgentDocValidationResult();

            if ( !string.IsNullOrWhiteSpace ( inputAgentDocumentNationalNo ) )
            {
                if ( inputAgentDocumentNationalNo.Length < 18 )
                {
                    singleValidationResult.DocExists = false;
                    singleValidationResult.ResultRestrictionLevel = RestrictionLevel.Avoidance;
                    singleValidationResult.MessageText = "شناسه یکتا وارد شده صحیح نمی باشد. اطلاعات وکالتنامه مورد استناد در بخش اشخاص کنترل شود.";
                    return singleValidationResult;
                }
            }

            string scriptoriumCode = (!string.IsNullOrWhiteSpace(inputAgentDocumentNationalNo)) ? inputAgentDocumentNationalNo.Substring(7, 5) : string.Empty;

            try
            {
                singleValidationResult = await validatorCore.ValidateAgentDocument (
                                                        inputAgentDocumentNationalNo,
                                                        inputAgentDocumentScriptoriumId,
                                                        inputAgentDocumentDate,
                                                        inputDocumentTypeID,
                                                        inputMovakelNationalNo,
                                                        inputVakilNationalNo,
                                                        inputMovakelFullName,
                                                        inputVakilFullName,
                                                        inputRegCaseList
                                                        );
            }
            catch ( System.Exception ex )
            {

                singleValidationResult.DocExists = false;
                string message = ex.ToCompleteString();
                singleValidationResult.MessageText = message;

                return singleValidationResult;
            }

            mainValidationResultCollection.Add ( singleValidationResult );

            AgentDocValidationResult finalizedResult = this.finalizeValidationResultCollection(mainValidationResultCollection);
            return finalizedResult;
        }

        /// <summary>
        /// The ValidateAgentDocumentPacket
        /// </summary>
        /// <param name="agentDocValidationRequestPacket">The agentDocValidationRequestPacket<see cref="AgentDocValidationRequestPacket"/></param>
        /// <param name="validationCommonDataPack">The validationCommonDataPack<see cref="DocValidationCommonData?"/></param>
        /// <returns>The <see cref="Task{AgentDocValidationResult}"/></returns>
        public async Task<AgentDocValidationResult> ValidateAgentDocumentPacket ( AgentDocValidationRequestPacket agentDocValidationRequestPacket, DocValidationCommonData? validationCommonDataPack = null )
        {
            AgentDocValidationResult agentDocValidationResult = new AgentDocValidationResult();

            if (!string.IsNullOrWhiteSpace(agentDocValidationRequestPacket.AgentDocumentNationalNo) &&
       agentDocValidationRequestPacket.AgentDocumentNationalNo.Length < 18)
            {
                agentDocValidationResult.MessageText = "شناسه یکتا وارد شده صحیح نمی باشد. اطلاعات وکالتنامه مورد استناد در بخش اشخاص کنترل شود.";
                agentDocValidationResult.ResultRestrictionLevel = RestrictionLevel.Avoidance;
                agentDocValidationResult.DocExists = false;

                return agentDocValidationResult;
            }

            try
            {
                if ( validationCommonDataPack == null )
                {
                    agentDocValidationResult.MessageText =
                   "بروز خطا در تصدیق وکالتنامه با شناسه یکتای  " + agentDocValidationRequestPacket.AgentDocumentNationalNo +
                   System.Environment.NewLine;
                }
                else
                {
                    validatorCore._theFoundRegisterServiceReqsCollection = validationCommonDataPack.TheFoundRegisterServiceReqsCollection;
                    validatorCore._theParentRegisterServiceReqDocumentType = validationCommonDataPack.TheParentRegisterServiceReqDocumentType;
                    agentDocValidationResult = await validatorCore.ValidateAgentDocument (
                                                                                   agentDocValidationRequestPacket.AgentDocumentNationalNo,
                                                                                   agentDocValidationRequestPacket.AgentDocumentScriptoriumId,
                                                                                   agentDocValidationRequestPacket.AgentDocumentDate,
                                                                                   agentDocValidationRequestPacket.DocumentTypeID,
                                                                                   agentDocValidationRequestPacket.MovakelNationalNo,
                                                                                   agentDocValidationRequestPacket.VakilNationalNo,
                                                                                   agentDocValidationRequestPacket.MovakelFullName,
                                                                                   agentDocValidationRequestPacket.VakilFullName,
                                                                                   agentDocValidationRequestPacket.RegCasesCollection );

                    agentDocValidationResult.CurrentDocAgentObjectID = agentDocValidationRequestPacket.CurrentDocAgentObjectID;

                }

            }
            catch ( System.Exception ex )
            {
                agentDocValidationResult = new AgentDocValidationResult ();
                agentDocValidationResult.DocExists = false;
                string message = ex.ToCompleteString();
                agentDocValidationResult.MessageText =
                    "بروز خطا در تصدیق وکالتنامه با شناسه یکتای  " + agentDocValidationRequestPacket.AgentDocumentNationalNo +
                    System.Environment.NewLine +
                    message;

                return agentDocValidationResult;
            }
            finally
            {
                agentDocValidationResult.ValidationRequestID = agentDocValidationRequestPacket.ValidationRequestID;
            }

            return agentDocValidationResult;
        }

        /// <summary>
        /// The ValidateAgentDocumentsCollection
        /// </summary>
        /// <param name="requestsCollection">The requestsCollection<see cref="List{AgentDocValidationRequestPacket}"/></param>
        /// <returns>The <see cref="Task{List{AgentDocValidationResult}}"/></returns>
        public async Task<List<AgentDocValidationResult>> ValidateAgentDocumentsCollection ( List<AgentDocValidationRequestPacket> requestsCollection )
        {
            List<AgentDocValidationResult> validationResultsCollection = new List<AgentDocValidationResult>{ };
            try
            {
                CancellationTokenSource source = new CancellationTokenSource();
                CancellationToken cancellationToken = source.Token;
                List<Document> currentServerFoundDocs = await this.FindOriginalDocs(requestsCollection);

                DocValidationCommonData validationCommonDataPack = new DocValidationCommonData();
                validationCommonDataPack.TheFoundRegisterServiceReqsCollection = currentServerFoundDocs;

                foreach ( AgentDocValidationRequestPacket theOneRequest in requestsCollection )
                {
                    if ( validationCommonDataPack.TheParentRegisterServiceReqDocumentType == null )
                        validationCommonDataPack.TheParentRegisterServiceReqDocumentType = await documentTypeRepository.GetDocumentType ( theOneRequest.DocumentTypeID, cancellationToken );

                    AgentDocValidationResult singleValidationResult =await this.ValidateAgentDocumentPacket(theOneRequest, validationCommonDataPack);

             

                    validationResultsCollection.Add ( singleValidationResult );
                }

            }
            catch ( Exception ex )
            {
                validationResultsCollection = this.GenerateErrorPackResult ( "بروز خطای سیستمی هنگام تصدیق اطلاعات وکالتنامه های مورد استناد. لطفاً مجدداً تلاش نمایید.", ex.ToCompleteString () );
            }

            return validationResultsCollection;
        }

        //public Task<AgentDocValidationResponseCollection> ValidateAgentDocumentPacket ( AgentDocValidationRequestCollection requestsCollection )
        //{
        //    AgentDocValidationResponseCollection responseCollection = new AgentDocValidationResponseCollection();

        //    if ( !requestsCollection.CollectionHasElement () )
        //        return null;

        //    AgentDocumentValidatorCore validationCore = new AgentDocumentValidatorCore();
        //    foreach ( AgentDocValidationRequestPacket theOneRequest in requestsCollection )
        //    {
        //        AgentDocValidationResult individualAgentDocValidationResult = await validationCore.ValidateAgentDocument(
        //            theOneRequest.AgentDocumentNationalNo,
        //            theOneRequest.AgentDocumentScriptoriumId,
        //            theOneRequest.AgentDocumentDate,
        //            theOneRequest.DocumentTypeID,
        //            theOneRequest.MovakelNationalNo,
        //            theOneRequest.VakilNationalNo,
        //            theOneRequest.MovakelFullName,
        //            theOneRequest.VakilFullName,
        //            theOneRequest.RegCasesCollection);

        //        individualAgentDocValidationResult.ValidationRequestID = theOneRequest.ValidationRequestID;
        //    }

        //    return null;
        //}

        /// <summary>
        /// The finalizeValidationResultCollection
        /// </summary>
        /// <param name="inputCollection">The inputCollection<see cref="List{AgentDocValidationResult}"/></param>
        /// <returns>The <see cref="AgentDocValidationResult"/></returns>
        private AgentDocValidationResult finalizeValidationResultCollection ( List<AgentDocValidationResult> inputCollection )
        {
            if ( inputCollection != null && inputCollection.Count > 0 )
            {
                foreach ( AgentDocValidationResult theOneAgentDocValidationResult in inputCollection )
                {
                    if ( theOneAgentDocValidationResult.DocExists )
                    {
                        if ( !string.IsNullOrEmpty ( theOneAgentDocValidationResult.MessageText ) )
                            theOneAgentDocValidationResult.MessageText = System.Environment.NewLine + "هشدارهای مربوط به وکالتنامه مورد استناد در بخش اشخاص: " + System.Environment.NewLine + theOneAgentDocValidationResult.MessageText;

                        return theOneAgentDocValidationResult;
                    }
                }

                return inputCollection [ 0 ];
            }
            else
            {
                AgentDocValidationResult overalAgentDocValidationResult = new AgentDocValidationResult();
                overalAgentDocValidationResult.DocExists = false;
                overalAgentDocValidationResult.ResultRestrictionLevel = RestrictionLevel.None;
                overalAgentDocValidationResult.RegisterServiceReqID = null;
                return overalAgentDocValidationResult;
            }
        }

        //private void AppendDestinationServerToPackets ( ref List<AgentDocValidationRequestPacket> requestsCollection, ref List<ZCommonUtility.ConnectionManager.Servers> destinationServersCollection )
        //{
        //    destinationServersCollection = null;
        //    List<string> scriptoriumCodesCollection = requestsCollection.Where(q => q.AgentDocumentNationalNo.Length == 18).ToList().Select(q => q.AgentDocumentNationalNo.Substring(7, 5)).Distinct().ToList();
        //    List<string> scriptotiumObjectIDsCollection = requestsCollection.Where(q => q.AgentDocumentNationalNo.Length != 18).ToList().Select(q => q.AgentDocumentScriptoriumId).Distinct().ToList();

        //    Dictionary<string, ZCommonUtility.ConnectionManager.Servers> scriptoriumServerMapTable = new Dictionary<string, ZCommonUtility.ConnectionManager.Servers>();
        //    Dictionary<string, ZCommonUtility.ConnectionManager.Servers> scriptoriumServerMapTableByID = new Dictionary<string, ZCommonUtility.ConnectionManager.Servers>();

        //    #region RelatedDocumentIsInSSAR
        //    foreach ( string theOneScriptoriumCode in scriptoriumCodesCollection )
        //    {
        //        if ( scriptoriumServerMapTable.ContainsKey ( theOneScriptoriumCode ) )
        //            continue;

        //        if ( _connectionSwitch == null )
        //            _connectionSwitch = new ZCommonUtility.ConnectionManager.ConnectionSwitch ();

        //        ZCommonUtility.ConnectionManager.Servers theDestinationServer = _connectionSwitch.GetSuperiorServerBasedOnScriptoriumCode(theOneScriptoriumCode);
        //        scriptoriumServerMapTable.Add ( theOneScriptoriumCode, theDestinationServer );

        //        if ( destinationServersCollection == null )
        //            destinationServersCollection = new List<ZCommonUtility.ConnectionManager.Servers> ();

        //        if ( !destinationServersCollection.Contains ( theDestinationServer ) )
        //            destinationServersCollection.Add ( theDestinationServer );
        //    }
        //    #endregion

        //    #region RelatedDocumentNotInSSAR
        //    foreach ( string theOneScriptoriumObjectId in scriptotiumObjectIDsCollection )
        //    {
        //        if ( scriptoriumServerMapTableByID.ContainsKey ( theOneScriptoriumObjectId ) )
        //            continue;

        //        if ( _connectionSwitch == null )
        //            _connectionSwitch = new ZCommonUtility.ConnectionManager.ConnectionSwitch ();

        //        Rad.BaseInfo.OrganizationChart.IScriptorium theOneScriptorium = Rad.CMS.InstanceBuilder.GetEntityById<Rad.BaseInfo.OrganizationChart.IScriptorium>(theOneScriptoriumObjectId);

        //        ZCommonUtility.ConnectionManager.Servers theDestinationServer = _connectionSwitch.GetSuperiorServerBasedOnScriptoriumCode(theOneScriptorium.Code);
        //        scriptoriumServerMapTable.Add ( theOneScriptoriumObjectId, theDestinationServer );
        //    }
        //    #endregion

        //    foreach ( AgentDocValidationRequestPacket theOnePacket in requestsCollection )
        //    {
        //        if ( theOnePacket.AgentDocumentNationalNo.Length == 18 )
        //        {
        //            string scriptoriumCode = theOnePacket.AgentDocumentNationalNo.Substring(7, 5);
        //            theOnePacket.PacketDestinationServer = scriptoriumServerMapTable [ scriptoriumCode ];
        //        }
        //        else
        //        {
        //            theOnePacket.PacketDestinationServer = scriptoriumServerMapTableByID [ theOnePacket.AgentDocumentScriptoriumId ];
        //        }
        //    }
        //}

        /// <summary>
        /// The FindOriginalDocs
        /// </summary>
        /// <param name="requestsCollection">The requestsCollection<see cref="List{AgentDocValidationRequestPacket}"/></param>
        /// <returns>The <see cref="Task{List{Document}}"/></returns>
        private async Task<List<Document>> FindOriginalDocs ( List<AgentDocValidationRequestPacket> requestsCollection )
        {
            List<string> requestedDocsNationalNos = requestsCollection.Where(q => q.AgentDocumentNationalNo.Length == 18).ToList().Select(q => q.AgentDocumentNationalNo).Distinct().ToList();
            List<Document> theFoundOriginalDocs = new List<Document>{ };

            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken cancellationToken = source.Token;
            IList? theFoundOriginalDocsList =await documentRepository.GetDocuments (new List<string>{ },cancellationToken,nationalNoes:requestedDocsNationalNos );
            if ( theFoundOriginalDocsList != null && theFoundOriginalDocsList.Count > 0 )
            {
                foreach ( Document theOneDoc in theFoundOriginalDocsList )
                    theFoundOriginalDocs.Add ( theOneDoc );
            }

            theFoundOriginalDocsList = null;
            List<AgentParam> agentParams=new List<AgentParam>();
            foreach ( AgentDocValidationRequestPacket theOneValidationReq in requestsCollection.Where ( q => q.AgentDocumentNationalNo.Length != 18 ).ToList () )
            {

                agentParams.Add ( new AgentParam ( theOneValidationReq.AgentDocumentScriptoriumId, theOneValidationReq.AgentDocumentNationalNo ) );

            }

            if ( agentParams.Count > 0 )
                theFoundOriginalDocsList = await documentRepository.GetDocuments ( new List<string> { }, cancellationToken, agentParams: agentParams );
            if ( theFoundOriginalDocsList != null && theFoundOriginalDocsList.Count > 0 )
            {


                foreach ( Document theOneDoc in theFoundOriginalDocsList)
                    theFoundOriginalDocs.Add ( theOneDoc );
            }

            return theFoundOriginalDocs;
        }

        /// <summary>
        /// The GenerateErrorPackResult
        /// </summary>
        /// <param name="message">The message<see cref="string?"/></param>
        /// <param name="exceptionMessages">The exceptionMessages<see cref="string?"/></param>
        /// <returns>The <see cref="List{AgentDocValidationResult}"/></returns>
        public List<AgentDocValidationResult> GenerateErrorPackResult ( string? message = null, string? exceptionMessages = null )
        {
            AgentDocValidationResult singleValidationResult = new AgentDocValidationResult();
            singleValidationResult.DocExists = false;
            singleValidationResult.ResultRestrictionLevel = RestrictionLevel.Avoidance;
            if ( string.IsNullOrWhiteSpace ( message ) )
                message = "بروز خطا در اعتبارسنجی و تصدیق وکالتنامه های استفاده شده. ";

            singleValidationResult.MessageText = message;
            singleValidationResult.ExceptionMessageText = exceptionMessages;

            List<AgentDocValidationResult> validationResultsCollection = new List<AgentDocValidationResult>();
            validationResultsCollection.Add ( singleValidationResult );

            return validationResultsCollection;
        }

        /// <summary>
        /// The ValidateDocumnetNumbers
        /// </summary>
        /// <param name="requestsCollection">The requestsCollection<see cref="List{AgentDocValidationRequestPacket}"/></param>
        /// <param name="message">The message<see cref="string"/></param>
        /// <returns>The <see cref="bool"/></returns>
        private bool ValidateDocumentNumbers(List<AgentDocValidationRequestPacket> requestsCollection, ref string message)
        {
            if (requestsCollection == null || requestsCollection.Count == 0)
            {
                // اگر مجموعه ورودی خالی باشد، هیچ شماره‌ای برای بررسی نیست و تابع true برمی‌گرداند
                return true;
            }

            // گرفتن شماره‌های سند یکتا
            var invalidNumbersCollection = requestsCollection
                .Select(q => q.AgentDocumentNationalNo)
                .Distinct()
                .ToList();

            if (invalidNumbersCollection.Count == 0)
                return true;

            // ساخت پیام خطا
            message = "شماره سندهای زیر اشتباه می باشد. لطفاً در ورود اطلاعات دقت فرمایید.";
            foreach (var number in invalidNumbersCollection)
            {
                message += Environment.NewLine + " - " + number;
            }

            return false;
        }
    }

}
