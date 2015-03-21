namespace EnergyTrading.MDM.Test.Checkers.Contracts
{
    public class LegalEntityChecker : NCheck.Checker<EnergyTrading.MDM.Contracts.Sample.LegalEntity>
    {
        public LegalEntityChecker()
        {
            Compare(x => x.Identifiers);
            Compare(x => x.Details);
            Compare(x => x.MdmSystemData); 
            Compare(x => x.Audit);
            Compare(x => x.Links);
        }
    }
}