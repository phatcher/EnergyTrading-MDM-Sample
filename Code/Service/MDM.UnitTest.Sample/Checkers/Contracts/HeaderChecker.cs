namespace EnergyTrading.MDM.Test.Checkers.Contracts
{
    using EnergyTrading.Mdm.Contracts;

    public class HeaderChecker : NCheck.Checker<Header>
    {
        public HeaderChecker()
        {
            Compare(x => x.Notes);
            Compare(x => x.Version);
        }
    }
}