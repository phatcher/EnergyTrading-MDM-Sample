namespace EnergyTrading.MDM.Test
{
    using System.Net;

    using Microsoft.Http;
    using NUnit.Framework;

    [TestFixture]
    public class when_a_request_is_made_to_create_a_party_mapping_and_the_xml_does_not_satisfy_the_schema : IntegrationTestBase
    {
        private static HttpResponseMessage response;

        private static HttpContent content;

        private static HttpClient client;

        private static MDM.Party entity;

        [TestFixtureSetUp]
        public static void ClassInit()
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            entity = Script.PartyData.CreateBasicEntity();
            var notAMapping = new EnergyTrading.MDM.Contracts.Sample.Party();
            content = HttpContentExtensions.CreateDataContract(notAMapping);
            client = new HttpClient();
        }

        protected static void Because_of()
        {
            response = client.Post(ServiceUrl["Party"] + string.Format("{0}/Mapping", entity.Id), content);
        }

        [Test]
        public void should_return_bad_request_status_code()
        {
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
        }
    }
}