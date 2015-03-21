namespace EnergyTrading.MDM.Test.Checkers.Contracts
{
    public class PartyDetailsChecker : NCheck.Checker<EnergyTrading.MDM.Contracts.Sample.PartyDetails>
    {
        public PartyDetailsChecker()
        {
            Compare(x => x.Name);
            Compare(x => x.TelephoneNumber);
            Compare(x => x.FaxNumber);
            Compare(x => x.Role);
            Compare(x => x.IsInternal);
        }
    }
}