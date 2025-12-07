namespace Notary.SSAA.BO.WebApi.Configuration
{
    using Autofac;
    using Notary.SSAA.BO.Configuration;
    using Module = Autofac.Module;

    /// <summary>
    /// Defines the <see cref="ConfigurationIOCModule" />
    /// </summary>
    public class ConfigurationIOCModule : Module
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationIOCModule"/> class.
        /// </summary>
        public ConfigurationIOCModule ( )
        {
        }

        /// <summary>
        /// The Load
        /// </summary>
        /// <param name="builder">The builder<see cref="ContainerBuilder"/></param>
        protected override void Load ( ContainerBuilder builder )
        {

            var relatedDocValidatorConfigurationInstance = new RelatedDocValidatorConfiguration();
            builder.RegisterInstance ( relatedDocValidatorConfigurationInstance ).As<RelatedDocValidatorConfiguration> ().SingleInstance ();

            var agentDocValidatorConfigurationInstance = new AgentDocValidatorConfiguration();
            builder.RegisterInstance ( agentDocValidatorConfigurationInstance ).As<AgentDocValidatorConfiguration> ().SingleInstance ();

            var validatorConfigurationInstance = new ValidatorConfiguration();
            builder.RegisterInstance ( validatorConfigurationInstance ).As<ValidatorConfiguration> ().SingleInstance ();

            var willDocValidatorConfigurationInstance = new WillDocValidatorConfiguration();
            builder.RegisterInstance ( willDocValidatorConfigurationInstance ).As<WillDocValidatorConfiguration> ().SingleInstance ();

            var  messagingConfigurationInstance = new MessagingConfiguration();
            builder.RegisterInstance ( messagingConfigurationInstance ).As<MessagingConfiguration> ().SingleInstance ();
            builder.RegisterType<ClientConfiguration> ().InstancePerLifetimeScope ();
        }
    }
}
