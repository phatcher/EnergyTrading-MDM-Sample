namespace EnergyTrading.MDM.Test.Checkers.Contracts
{
    public class BrokerChecker : NCheck.Checker<EnergyTrading.MDM.Contracts.Sample.Broker>
    {
        public BrokerChecker()
        {
            Compare(x => x.Identifiers);
            Compare(x => x.Details);
            Compare(x => x.MdmSystemData); 
            Compare(x => x.Audit);
            Compare(x => x.Links);		
        }
    }
}
