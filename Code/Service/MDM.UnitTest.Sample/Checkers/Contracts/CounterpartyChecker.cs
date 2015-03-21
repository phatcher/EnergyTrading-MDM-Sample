namespace EnergyTrading.MDM.Test.Checkers.Contracts
{
    public class CounterpartyChecker : NCheck.Checker<EnergyTrading.MDM.Contracts.Sample.Counterparty>
    {
        public CounterpartyChecker()
        {
            Compare(x => x.Identifiers);
            Compare(x => x.Details);
            Compare(x => x.MdmSystemData); 
            Compare(x => x.Audit);
            Compare(x => x.Links);		
        }
    }
}