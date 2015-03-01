namespace MDM.Sync.Synchronizers.Entities
{
    using EnergyTrading.Mdm.Client.Services;

    using EnergyTrading.MDM.Contracts.Sample;

    public class LocationSynchronizer : MdmEntitySynchronizer<Location>
    {
        public LocationSynchronizer(IMdmService mdmService)
            : base(mdmService)
        {
        }
    }
}