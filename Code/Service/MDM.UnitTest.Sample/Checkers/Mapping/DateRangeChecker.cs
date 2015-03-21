namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using EnergyTrading;

    public class DateRangeChecker : NCheck.Checker<DateRange>
    {
        public DateRangeChecker()
        {
            Compare(x => x.Start);
            Compare(x => x.Finish);
        }
    }
}