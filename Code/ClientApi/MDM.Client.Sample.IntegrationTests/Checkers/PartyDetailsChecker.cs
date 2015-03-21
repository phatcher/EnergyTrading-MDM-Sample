namespace MDM.Client.Sample.IntegrationTests.Checkers
{
    public class PartyDetailsChecker : NCheck.Checker<EnergyTrading.MDM.Contracts.Sample.PartyDetails>
    {
        public PartyDetailsChecker()
        {
			Compare(x => x.Name);
		}
    }
}