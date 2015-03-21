namespace EnergyTrading.MDM.Test.Checkers.Contracts
{
    public class CounterpartyDetailsChecker : NCheck.Checker<EnergyTrading.MDM.Contracts.Sample.CounterpartyDetails>
    {
        public CounterpartyDetailsChecker()
        {
            Compare(x => x.Name);
            Compare(x => x.Phone);
            Compare(x => x.Fax);
            Compare(x => x.ShortName);
        }
    }
}