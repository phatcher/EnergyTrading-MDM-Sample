namespace EnergyTrading.MDM.Test
{
    using System.Net;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using NUnit.Framework;

    [TestFixture]
    public class when_a_request_is_made_to_update_a_location_entity : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static HttpContent content;
        private static EnergyTrading.MDM.Contracts.Sample.Location locationDataContract;
        private static HttpClient client;
        private static MDM.Location entity;

        private static EnergyTrading.MDM.Contracts.Sample.Location updatedContract;

        [TestFixtureSetUp]
        public static void ClassInit()
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            client = new HttpClient();
            entity = Script.LocationData.CreateBasicEntity();
            var getResponse = client.Get(ServiceUrl["Location"] + entity.Id);

            updatedContract = getResponse.Content.ReadAsDataContract<EnergyTrading.MDM.Contracts.Sample.Location>();
            content = HttpContentExtensions.CreateDataContract(Script.LocationData.MakeChangeToContract(updatedContract));
        }

        protected static void Because_of()
        {
            client.DefaultHeaders.Add("If-Match", entity.Version.ToString());
            response = client.Post(ServiceUrl["Location"] + entity.Id, content);
        }

        [Test]
        public void should_update_the_location_in_the_database_with_the_correct_details()
        {
            Script.LocationDataChecker.ConfirmEntitySaved(entity.Id, updatedContract);
        }

        [Test]
        public void should_return_a_no_content_status_code()
        {
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
