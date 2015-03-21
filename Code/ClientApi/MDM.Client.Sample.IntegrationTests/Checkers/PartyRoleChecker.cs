namespace MDM.Client.Sample.IntegrationTests.Checkers
{
    public class PartyRoleChecker : NCheck.Checker<EnergyTrading.MDM.Contracts.Sample.PartyRole>
    {
        public PartyRoleChecker()
        {
            Compare(x => x.Identifiers);
            Compare(x => x.Details);
        }
    }
}