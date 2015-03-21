namespace MDM.Client.Sample.IntegrationTests.Checkers
{
    using EnergyTrading.MDM.Contracts.Sample;

    public class BrokerDetailsChecker : NCheck.Checker<BrokerDetails>
    {
        public BrokerDetailsChecker()
        {
			Compare(x => x.Name);
		}
    }
}