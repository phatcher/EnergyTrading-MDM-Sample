namespace MDM.Client.Sample.IntegrationTests.Checkers
{
    using EnergyTrading.MDM.Contracts.Sample;

    public class BrokerChecker : NCheck.Checker<Broker>
    {
        public BrokerChecker()
        {
            Compare(x => x.Identifiers);
            Compare(x => x.Details);
        }
    }
}