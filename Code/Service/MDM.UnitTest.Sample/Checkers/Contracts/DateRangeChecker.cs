namespace EnergyTrading.MDM.Test.Checkers.Contracts
{
    public class DateRangeChecker : NCheck.Checker<EnergyTrading.Mdm.Contracts.DateRange>
    {
        public DateRangeChecker()
        {
            Compare(x => x.StartDate);
            Compare(x => x.EndDate);
        }
    }
}