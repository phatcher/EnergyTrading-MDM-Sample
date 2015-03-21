namespace EnergyTrading.MDM.Test
{
    public class CheckerFactory : EnergyTrading.Test.CheckerFactory
    {
        public CheckerFactory()
        {
            this.Initialize();
        }

        private void Initialize()
        {
            Register(typeof(CheckerFactory).Assembly);
        }
    }
}