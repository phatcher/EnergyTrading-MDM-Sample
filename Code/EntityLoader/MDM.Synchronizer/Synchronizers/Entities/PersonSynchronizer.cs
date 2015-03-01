namespace MDM.Sync.Synchronizers.Entities
{
    using EnergyTrading.Mdm.Client.Services;

    using EnergyTrading.MDM.Contracts.Sample;

    public class PersonSynchronizer : MdmEntitySynchronizer<Person>
    {
        public PersonSynchronizer(IMdmService mdmService)
            : base(mdmService)
        {
        }
    }
}