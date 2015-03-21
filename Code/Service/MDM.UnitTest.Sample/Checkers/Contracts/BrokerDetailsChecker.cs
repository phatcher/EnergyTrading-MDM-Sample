namespace EnergyTrading.MDM.Test.Checkers.Contracts
{
    public class BrokerDetailsChecker : NCheck.Checker<EnergyTrading.MDM.Contracts.Sample.BrokerDetails>
    {
        public BrokerDetailsChecker()
        {
            Compare(x => x.Name);
            Compare(x => x.Phone);
            Compare(x => x.Fax);
            Compare(x => x.Rate);
        }
    }
}