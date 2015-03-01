using Mdm.Client.Sample.Registrars;

namespace MDM.Loader.Configuration
{
    using System;
    using System.Collections.Generic;

    using EnergyTrading.Configuration;

    using Microsoft.Practices.Unity;

    public class MdmServiceConfiguration : IGlobalConfigurationTask
    {
        private readonly IUnityContainer container;

        public MdmServiceConfiguration(IUnityContainer container)
        {
            this.container = container;
        }

        public IList<Type> DependsOn
        {
            get
            {
                return new List<Type>();
            }
        }

        public void Configure()
        {
            var config = new NexusMdmClientRegistrar();

            config.Register(container);
        }
    }
}