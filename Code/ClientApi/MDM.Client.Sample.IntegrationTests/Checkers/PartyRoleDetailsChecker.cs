namespace MDM.Client.Sample.IntegrationTests.Checkers
{
    public class PartyRoleDetailsChecker : NCheck.Checker<EnergyTrading.MDM.Contracts.Sample.PartyRoleDetails>
    {
        public PartyRoleDetailsChecker()
        {
			Compare(x => x.Name);
		}
    }
}