namespace EnergyTrading.MDM.Test.Checkers.Contracts
{
    public class ExchangeChecker : NCheck.Checker<EnergyTrading.MDM.Contracts.Sample.Exchange>
    {
        public ExchangeChecker()
        {
            Compare(x => x.Identifiers);
            Compare(x => x.Details);
            Compare(x => x.MdmSystemData); 
            Compare(x => x.Audit);
            Compare(x => x.Links);		
        }
    }
}