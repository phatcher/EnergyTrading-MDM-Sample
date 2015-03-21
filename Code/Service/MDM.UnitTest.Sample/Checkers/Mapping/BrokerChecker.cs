namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using EnergyTrading.MDM;

    public class BrokerChecker : NCheck.Checker<Broker>
    {
        public BrokerChecker()
        {
            Compare(x => x.Id);
            Compare(x => x.Details).Count();
            Compare(x => x.Mappings).Count();
        }
    }
}
