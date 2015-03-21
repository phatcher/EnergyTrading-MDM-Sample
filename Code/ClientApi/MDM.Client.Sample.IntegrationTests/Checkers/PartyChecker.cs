namespace MDM.Client.Sample.IntegrationTests.Checkers
{
    public class PartyChecker : NCheck.Checker<EnergyTrading.MDM.Contracts.Sample.Party>
    {
        public PartyChecker()
        {
            Compare(x => x.Identifiers);
            Compare(x => x.Details);
        }
    }
}