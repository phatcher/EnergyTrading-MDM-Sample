namespace MDM.Client.Sample.IntegrationTests
{
    public class CheckerFactory : EnergyTrading.Test.CheckerFactory
    {
        public CheckerFactory()
        {
            Initialize();
        }

        private void Initialize()
        {
            Register(typeof(CheckerFactory).Assembly);
            Register(typeof(EnergyTrading.Test.CheckerFactory).Assembly);
        }
    }
}