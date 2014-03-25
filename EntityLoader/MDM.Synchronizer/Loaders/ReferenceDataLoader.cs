﻿namespace MDM.Sync.Loaders
{
    using System.Collections.Generic;
    using Microsoft.Practices.ServiceLocation;

    using EnergyTrading.Logging;
    using EnergyTrading.Mdm.Client.Services;
    using OpenNexus.MDM.Contracts; using EnergyTrading.Mdm.Contracts;

    public class ReferenceDataLoader : Loader
    {
        private readonly IDictionary<string, IList<ReferenceData>> referenceDataLists;
        private readonly ILogger logger = LoggerFactory.GetLogger(typeof(ReferenceDataLoader));

        public ReferenceDataLoader(IDictionary<string, IList<ReferenceData>> referenceDataLists)
        {
            this.referenceDataLists = referenceDataLists;
        }

        protected override void OnLoad()
        {
            logger.Info("ReferenceData: Begin load");
            foreach (var key in referenceDataLists.Keys)
            {
                logger.InfoFormat("Loading {0}", key);
                Load(key, referenceDataLists[key]);
            }
            logger.Info("ReferenceData: Load complete\r\n");
        }

        private void Load(string key, IList<ReferenceData> entries)
        {
            var referenceDataService = ServiceLocator.Current.GetInstance<IReferenceDataService>();
            Try(() => referenceDataService.Create(key, entries), 3);            
        }
    }
}