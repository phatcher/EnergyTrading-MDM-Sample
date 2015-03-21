namespace MDM.Client.Sample.IntegrationTests.Checkers
{
    using EnergyTrading.MDM.Contracts.Sample;

    public class CounterpartyChecker : NCheck.Checker<Counterparty>
    {
        public CounterpartyChecker()
        {
            Compare(x => x.Identifiers);
            Compare(x => x.Details);
        }
    }
}