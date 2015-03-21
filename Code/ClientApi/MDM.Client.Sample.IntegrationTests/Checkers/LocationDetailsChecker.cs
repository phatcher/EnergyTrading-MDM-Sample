namespace MDM.Client.Sample.IntegrationTests.Checkers
{
    public class LocationDetailsChecker : NCheck.Checker<EnergyTrading.MDM.Contracts.Sample.LocationDetails>
    {
        public LocationDetailsChecker()
        {
			Compare(x => x.Name);
		}
    }
}