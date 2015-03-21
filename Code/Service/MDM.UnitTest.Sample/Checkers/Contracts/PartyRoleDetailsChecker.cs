namespace EnergyTrading.MDM.Test.Checkers.Contracts
{
    public class PartyRoleDetailsChecker : NCheck.Checker<EnergyTrading.MDM.Contracts.Sample.PartyRoleDetails>
    {
        public PartyRoleDetailsChecker()
        {
            Compare(x => x.Name);
        }
    }
}