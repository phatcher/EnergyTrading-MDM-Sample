﻿namespace EnergyTrading.MDM.Test
{
    using System.Net;

    using Microsoft.Http;
    using NUnit.Framework;

    [TestFixture]
    public class when_a_request_is_made_to_update_a_legalentity_entity_and_the_entity_is_not_found_ : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static HttpContent content;
        private static HttpClient client;

        [TestFixtureSetUp]
        public static void ClassInit()
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            client = new HttpClient();
            var legalentity = new EnergyTrading.MDM.Contracts.Sample.LegalEntity();
            content = HttpContentExtensions.CreateDataContract(legalentity);
        }

        protected static void Because_of()
        {
            client.DefaultHeaders.Add("If-Match", 0.ToString());
            response = client.Post(ServiceUrl["LegalEntity"] + int.MaxValue, content);
        }

        [Test]
        public void should_return_a_not_found_status_code()
        {
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
