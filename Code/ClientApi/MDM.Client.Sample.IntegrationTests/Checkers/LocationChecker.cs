namespace MDM.Client.Sample.IntegrationTests.Checkers
{
    public class LocationChecker : NCheck.Checker<EnergyTrading.MDM.Contracts.Sample.Location>
    {
        public LocationChecker()
        {
            Compare(x => x.Identifiers);
            Compare(x => x.Details);
        }
    }
}