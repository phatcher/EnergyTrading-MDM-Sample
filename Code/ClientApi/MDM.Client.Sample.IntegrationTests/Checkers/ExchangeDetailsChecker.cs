namespace MDM.Client.Sample.IntegrationTests.Checkers
{
    using EnergyTrading.MDM.Contracts.Sample;

    public class ExchangeDetailsChecker : NCheck.Checker<ExchangeDetails>
    {
        public ExchangeDetailsChecker()
        {
			Compare(x => x.Name);
		}
    }
}
