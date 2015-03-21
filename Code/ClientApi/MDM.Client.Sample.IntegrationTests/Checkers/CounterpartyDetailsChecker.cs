namespace MDM.Client.Sample.IntegrationTests.Checkers
{
    using EnergyTrading.MDM.Contracts.Sample;

    public class CounterpartyDetailsChecker : NCheck.Checker<CounterpartyDetails>
    {
        public CounterpartyDetailsChecker()
        {
			Compare(x => x.Name);
		}
    }
}