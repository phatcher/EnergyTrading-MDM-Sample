namespace EnergyTrading.MDM.Test 
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Net;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using OpenNexus.MDM.Contracts; using EnergyTrading.Mdm.Contracts;

    [TestClass]
    public class when_a_source_system_to_destination_system_mapping_request_is_made_for_partyrole : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static MDM.PartyRole entity;
        private static MappingResponse mappingResponse;
        private static HttpClient client;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Because_of();
        }

        protected static void Because_of()
        {
            entity = PartyRoleData.CreateEntityWithTwoDetailsAndTwoMappings();

            client = new HttpClient(ServiceUrl["PartyRole"] +
                "crossmap?source-system=trayport&destination-system=endur&mapping-string=" + entity.Mappings[0].MappingValue);

            response = client.Get();

            mappingResponse = response.Content.ReadAsDataContract<EnergyTrading.Mdm.Contracts.MappingResponse>();
        }

        [TestMethod]
        public void should_return_the_correct_vesrion_of_the_mapping()
        {
            Assert.AreEqual(entity.Mappings[1].Validity.Start, mappingResponse.Mappings[0].StartDate);
            Assert.AreEqual(entity.Mappings[1].Validity.Finish, mappingResponse.Mappings[0].EndDate);
            Assert.AreEqual("endur", mappingResponse.Mappings[0].SystemName.ToLower(), true);
            Assert.IsFalse(mappingResponse.Mappings[0].IsMdmId);
        }

        [TestMethod]
        public void should_return_correct_content_type()
        {
            Assert.AreEqual(ConfigurationManager.AppSettings["restReturnType"], response.Content.ContentType);
        }

        [TestMethod]
        public void should_return_status_ok()
        {
            response.StatusCode = HttpStatusCode.OK;
        }

        [TestMethod]
        public void should_return_only_one_mapping()
        {
            Assert.AreEqual(1, mappingResponse.Mappings.Count);
        }
    }

    [TestClass]
    public class when_a_source_system_to_destination_system_mapping_request_is_made_as_of_a_date_for_partyrole : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static MappingResponse mappingResponse;
        private static HttpClient client;
        private static MDM.PartyRole entity;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Because_of();
        }

        protected static void Because_of()
        {
            entity = PartyRoleData.CreateEntityWithTwoDetailsAndTwoMappings();

            client = new HttpClient(ServiceUrl["PartyRole"] +
                "crossmap?source-system=trayport&destination-system=endur&mapping-string=" + entity.Mappings[0].MappingValue
                + "&as-of=" + entity.Mappings[0].Validity.Start.ToString(DateFormatString));

            response = client.Get();
            mappingResponse = response.Content.ReadAsDataContract<EnergyTrading.Mdm.Contracts.MappingResponse>();
        }

        [TestMethod]
        public void should_return_the_correct_vesrion_of_the_partyrole()
        {
            Assert.AreEqual(entity.Mappings[1].Validity.Start, mappingResponse.Mappings[0].StartDate);
            Assert.AreEqual(entity.Mappings[1].Validity.Finish, mappingResponse.Mappings[0].EndDate);
            Assert.AreEqual("endur", mappingResponse.Mappings[0].SystemName.ToLower(), true);
            Assert.IsFalse(mappingResponse.Mappings[0].IsMdmId);
        }

        [TestMethod]
        public void should_return_correct_content_type()
        {
            Assert.AreEqual(ConfigurationManager.AppSettings["restReturnType"], response.Content.ContentType);
        }

        [TestMethod]
        public void should_return_status_ok()
        {
            response.StatusCode = HttpStatusCode.OK;
        }

        [TestMethod]
        public void should_return_only_one_mapping()
        {
            Assert.AreEqual(1, mappingResponse.Mappings.Count);
        }
    }
}