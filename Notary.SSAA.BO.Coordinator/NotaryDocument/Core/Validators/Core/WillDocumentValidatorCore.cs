namespace Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Validators.Core
{
    using MediatR;
    using Notary.SSAA.BO.Configuration;
    using Notary.SSAA.BO.DataTransferObject.Coordinators.NotaryDocument;
    using Notary.SSAA.BO.Domain.Abstractions;
    using Notary.SSAA.BO.Domain.RichDomain.Document;
    using Notary.SSAA.BO.SharedKernel.Enumerations;
    using Notary.SSAA.BO.SharedKernel.Interfaces;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="WillDocumentValidatorCore" />
    /// </summary>
    public class WillDocumentValidatorCore : AgentDocumentValidatorCore
    {
        //IList<Document> _theFoundRegisterServiceReqsCollection = null;
        //Notary.SSAA.BO.Domain.Entities.DocumentType _theParentRegisterServiceReqDocumentType = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="WillDocumentValidatorCore"/> class.
        /// </summary>
        /// <param name="_documentRepository">The _documentRepository<see cref="IDocumentRepository"/></param>
        /// <param name="_userService">The _userService<see cref="IUserService"/></param>
        /// <param name="_dateTimeService">The _dateTimeService<see cref="IDateTimeService"/></param>
        /// <param name="_documentPersonRepository">The _documentPersonRepository<see cref="IDocumentPersonRepository"/></param>
        /// <param name="_documentPersonRelatedRepository">The _documentPersonRelatedRepository<see cref="IDocumentPersonRelatedRepository"/></param>
        /// <param name="_agentDocValidatorConfiguration">The _agentDocValidatorConfiguration<see cref="AgentDocValidatorConfiguration"/></param>
        /// <param name="_willDocValidatorConfiguration">The _willDocValidatorConfiguration<see cref="WillDocValidatorConfiguration"/></param>
        /// <param name="_mediator">The _mediator<see cref="IMediator"/></param>
        /// <param name="firewall">The firewall<see cref="Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Firewall.Firewall"/></param>
        public WillDocumentValidatorCore ( IDocumentRepository _documentRepository, IUserService _userService, IDateTimeService _dateTimeService, IDocumentPersonRepository _documentPersonRepository, IDocumentPersonRelatedRepository _documentPersonRelatedRepository, AgentDocValidatorConfiguration _agentDocValidatorConfiguration, WillDocValidatorConfiguration _willDocValidatorConfiguration, IMediator _mediator, Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Firewall.Firewall firewall ) : base ( _documentRepository, _userService, _dateTimeService, _documentPersonRepository, _documentPersonRelatedRepository, _agentDocValidatorConfiguration, _willDocValidatorConfiguration, firewall, _mediator )
        {
        }

        //private WillDocumentValidatorCore ( IList<Document> theFoundRegisterServiceReqsCollection, Notary.SSAA.BO.Domain.Entities.DocumentType theParentRegisterServiceReqDocumentType )
        //{
        //    _theFoundRegisterServiceReqsCollection = theFoundRegisterServiceReqsCollection;
        //    _theParentRegisterServiceReqDocumentType = theParentRegisterServiceReqDocumentType;
        //}

        //public WillDocumentValidatorCore ( DocValidationCommonData validationCommonDataPack )
        //{
        //    this.willDocValidatorConfiguration = new WillDocValidatorConfiguration ();
        //    _theFoundRegisterServiceReqsCollection = validationCommonDataPack.TheFoundRegisterServiceReqsCollection;
        //    _theParentRegisterServiceReqDocumentType = validationCommonDataPack.TheParentRegisterServiceReqDocumentType;
        //}

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
        internal override async Task<AgentDocValidationResult> ValidateAgentDocument(
            string inputAgentDocumentNationalNo,
            string inputAgentDocumentScriptoriumId,
            string inputAgentDocumentDate,
            string inputDocumentTypeID,
            string inputMovakelNationalNo,
            string inputVakilNationalNo,
            string inputMovakelFullName,
            string inputVakilFullName,
            List<RegCasePacket> inputRegCaseList)
        {
            string? pMessageText = null;

            if (willDocValidatorConfiguration.ValidatorEnabled == RestrictionLevel.Disabled)
            {
                var disabledResult = new AgentDocValidationResult
                {
                    DocExists = true,
                    ResultRestrictionLevel = RestrictionLevel.Pass,
                    RegisterServiceReqID = string.Empty,
                    MessageText = willDocValidatorConfiguration.AppendErrorCode
                        ? "#VALIDATOR-CODE:00\n"
                        : pMessageText
                };

                return disabledResult;
            }

            var agentDocValidationResult = await base.ValidateAgentDocument(
                inputAgentDocumentNationalNo,
                inputAgentDocumentScriptoriumId,
                inputAgentDocumentDate,
                inputDocumentTypeID,
                inputMovakelNationalNo,
                inputVakilNationalNo,
                inputMovakelFullName,
                inputVakilFullName,
                inputRegCaseList);

            if (agentDocValidationResult.ResultRestrictionLevel == RestrictionLevel.Avoidance)
                return agentDocValidationResult;

            return agentDocValidationResult;
        }
    }

}
