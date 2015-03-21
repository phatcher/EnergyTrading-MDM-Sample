namespace MDM.Client.Sample.IntegrationTests.Checkers
{
    using EnergyTrading.Mdm.Contracts;

    public class MdmIdChecker : NCheck.Checker<MdmId>
    {
        public MdmIdChecker()
        {
            Compare(x => x.SystemId);
            Compare(x => x.SystemName);
            Compare(x => x.Identifier);
        }
    }
}