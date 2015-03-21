namespace MDM.Client.Sample.IntegrationTests.Checkers
{
    using EnergyTrading.MDM.Contracts.Sample;

    public class ExchangeChecker : NCheck.Checker<Exchange>
    {
        public ExchangeChecker()
        {
            Compare(x => x.Identifiers);
            Compare(x => x.Details);
        }
    }
}
