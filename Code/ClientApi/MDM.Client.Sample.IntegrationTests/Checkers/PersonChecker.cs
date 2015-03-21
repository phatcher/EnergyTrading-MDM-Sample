namespace MDM.Client.Sample.IntegrationTests.Checkers
{
    using EnergyTrading.MDM.Contracts.Sample;
    using EnergyTrading.Test;

    public class PersonChecker : NCheck.Checker<EnergyTrading.MDM.Contracts.Sample.Person>
    {
        public PersonChecker()
        {
            Compare(x => x.Identifiers);
            Compare(x => x.Details);
        }
    }
}