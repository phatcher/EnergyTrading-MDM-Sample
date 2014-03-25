namespace EnergyTrading.MDM.Test
{
    using System.Net;

    using Microsoft.Http;
    using NUnit.Framework;

    using EnergyTrading.Data.EntityFramework;
    using EnergyTrading.MDM.Data.EF.Configuration;

    [TestFixture]
    public class when_a_request_is_made_to_update_a_exchange_entity_and_the_etag_is_not_current : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static HttpContent content;
        private static HttpClient client;
        private static ulong startVersion;
        private static MDM.Exchange entity;

        [TestFixtureSetUp]
        public static void ClassInit()
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            client = new HttpClient();
            entity = ExchangeData.CreateBasicEntity();

            content = HttpContentExtensions.CreateDataContract(new EnergyTrading.MDM.Contracts.Sample.Exchange());
            startVersion = CurrentEntityVersion();
        }

        protected static void Because_of()
        {
            client.DefaultHeaders.Add("If-Match", long.MaxValue.ToString());
            response = client.Post(ServiceUrl["Exchange"] + entity.Id, content);
        }

        [Test]
        public void should_not_update_the_exchange_in_the_database()
        {
            Assert.AreEqual(startVersion, CurrentEntityVersion());
        }

        [Test]
        public void should_return_a_no_content_status_code()
        {
            Assert.AreEqual(HttpStatusCode.PreconditionFailed, response.StatusCode);
        }

        private static ulong CurrentEntityVersion()
        {
            return new DbSetRepository(new DbContextProvider(() => new SampleMappingContext())).FindOne<MDM.Exchange>(entity.Id).Version;
        }
    }
}
