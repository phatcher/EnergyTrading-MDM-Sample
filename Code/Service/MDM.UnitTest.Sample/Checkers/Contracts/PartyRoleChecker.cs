namespace EnergyTrading.MDM.Test.Checkers.Contracts
{
    public class PartyRoleChecker : NCheck.Checker<EnergyTrading.MDM.Contracts.Sample.PartyRole>
    {
        public PartyRoleChecker()
        {
            Compare(x => x.Identifiers);
            Compare(x => x.Details);
            Compare(x => x.MdmSystemData); 
            Compare(x => x.Audit);
            Compare(x => x.Links);		
        }
    }
}