namespace EnergyTrading.MDM.Test
{
    using System.Configuration;
    using System.Linq;
    using System.Net;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using OpenNexus.MDM.Contracts; using EnergyTrading.Mdm.Contracts;

    [TestClass]
    public class when_a_source_system_to_destination_system_mapping_request_is_made_and_the_source_system_mapping_string_is_unkown_for_partyrole : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static HttpClient client;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Because_of();
        }

        protected static void Because_of()
        {
            client = new HttpClient(ServiceUrl["PartyRole"] +
                "crossmap?source-system=trayport&destination-system=endur&mapping-string=abc");

            response = client.Get();
        }

        [TestMethod]
        public void should_return_nexus_failure_with_correct_information()
        {
            Fault fault = null;
            try { fault = response.Content.ReadAsDataContract<Fault>(); } catch { }

            Assert.IsNotNull(fault);
            Assert.AreEqual("Unknown Mapping", fault.Reason);
            Assert.AreEqual("abc", fault.Mapping);
            Assert.AreEqual("Trayport", fault.SourceSystem, true);
            Assert.IsNull(fault.AsOfDate);
            Assert.AreEqual(
                "Mapping String 'abc' not found for Source System 'Trayport'",
                fault.Message, true);
        }

        [TestMethod]
        public void should_return_correct_content_type()
        {
            Assert.AreEqual(ConfigurationManager.AppSettings["restReturnType"], response.Content.ContentType);
        }

        [TestMethod]
        public void should_return_status_not_found()
        {
            response.StatusCode = HttpStatusCode.NotFound;
        }
    }
}