namespace EnergyTrading.MDM.Test.Checkers.Contracts
{
    public class ExchangeDetailsChecker : NCheck.Checker<EnergyTrading.MDM.Contracts.Sample.ExchangeDetails>
    {
        public ExchangeDetailsChecker()
        {
            Compare(x => x.Name);
            Compare(x => x.Phone);
            Compare(x => x.Fax);
        }
    }
}