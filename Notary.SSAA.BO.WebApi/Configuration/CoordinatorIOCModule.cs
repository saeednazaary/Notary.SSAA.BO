namespace Notary.SSAA.BO.WebApi.Configuration
{
    using Autofac;
    using Notary.SSAA.BO.Coordinator.NotaryDocument;
    using Notary.SSAA.BO.Coordinator.NotaryDocument.Core;
    using Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Commons;
    using Notary.SSAA.BO.Coordinator.NotaryDocument.Core.ENoteBook;
    using Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Estate.Core;
    using Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Estate.Shell;
    using Notary.SSAA.BO.Coordinator.NotaryDocument.Core.FinalVerificationManager;
    using Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Firewall;
    using Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Messaging;
    using Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Validators.Core;
    using Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Validators.Shell;
    using Module = Autofac.Module;

    /// <summary>
    /// Defines the <see cref="CoordinatorIOCModule" />
    /// </summary>
    public class CoordinatorIOCModule : Module
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CoordinatorIOCModule"/> class.
        /// </summary>
        public CoordinatorIOCModule ( )
        {
        }

        /// <summary>
        /// The Load
        /// </summary>
        /// <param name="builder">The builder<see cref="ContainerBuilder"/></param>
        protected override void Load ( ContainerBuilder builder )
        {

            builder.RegisterType<SMSGeneratorEngine> ().InstancePerLifetimeScope ();
            builder.RegisterType<MessagingCoreOperations> ().InstancePerLifetimeScope ();
            builder.RegisterType<MessagingCore> ().InstancePerLifetimeScope ();
            builder.RegisterType<Firewall> ().InstancePerLifetimeScope ();
            builder.RegisterType<AnnotationsGenerator> ().InstancePerLifetimeScope ();
            builder.RegisterType<AnnotationsController> ().InstancePerLifetimeScope ();
            builder.RegisterType<RelatedDocumentValidatorCore> ().InstancePerLifetimeScope ();
            builder.RegisterType<AgentDocumentValidatorCore> ().InstancePerLifetimeScope ();
            builder.RegisterType<AgentDocumentValidator> ().InstancePerLifetimeScope ();
            builder.RegisterType<RelatedDocumentValidator> ().InstancePerLifetimeScope ();
            builder.RegisterType<DataCollector> ().InstancePerLifetimeScope ();
            builder.RegisterType<CentralizedValidator> ().InstancePerLifetimeScope ();
            builder.RegisterType<ValidatorGateway> ().InstancePerLifetimeScope ();
            
            builder.RegisterType<DSULogger>().InstancePerLifetimeScope ();
            builder.RegisterType<SignProvider> ().InstancePerLifetimeScope ();
            builder.RegisterType<ENoteBookServerController> ().InstancePerLifetimeScope ();
            builder.RegisterType<DSUPersonsManager> ().InstancePerLifetimeScope ();
            builder.RegisterType<EstateInquiryValidator> ().InstancePerLifetimeScope ();
            builder.RegisterType<NotaryOfficeDSUEngineCore> ().InstancePerLifetimeScope ();
            builder.RegisterType<EstateInquiryEngine> ().InstancePerLifetimeScope ();
            builder.RegisterType<PersonOwnershipDocsInheritorCore> ().InstancePerLifetimeScope ();
            builder.RegisterType<SeperationDealSummaryEngineCore> ().InstancePerLifetimeScope ();
            builder.RegisterType<SeparationDealSummaryValidator> ().InstancePerLifetimeScope ();
            builder.RegisterType<QuotasValidator> ().InstancePerLifetimeScope ();
            builder.RegisterType<QuotasGeneratorEngine> ().InstancePerLifetimeScope ();
            builder.RegisterType<SmartQuotaGeneratorEngine> ().InstancePerLifetimeScope ();
            builder.RegisterType<RegisterServiceRequestVerifier> ().InstancePerLifetimeScope ();








            builder.RegisterType<LoadDocumentCoordinator> ().InstancePerLifetimeScope ();
            builder.RegisterType<SaveDocumentCoordinator> ().InstancePerLifetimeScope ();
            builder.RegisterType<LoadDocumentStandardContractCoordinator>().InstancePerLifetimeScope();
            builder.RegisterType<SaveDocumentStandardContractCoordinator>().InstancePerLifetimeScope();
        }
    }
}
