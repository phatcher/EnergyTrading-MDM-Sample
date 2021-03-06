namespace RWEST.Nexus.MDM.Test
{
    using System.Net;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.Data.EntityFramework;
    using RWEST.Nexus.MDM.Data.EF.Configuration;

    [TestClass]
    public class when_a_request_is_made_to_update_a_curve_entity_and_the_etag_is_not_current : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static HttpContent content;
        private static HttpClient client;
        private static long startVersion;
        private static MDM.Curve entity;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            client = new HttpClient();
            entity = CurveData.CreateBasicEntity();

            content = HttpContentExtensions.CreateDataContract(new MDM.Contracts.Curve());
            startVersion = CurrentEntityVersion();
        }

        protected static void Because_of()
        {
            client.DefaultHeaders.Add("If-Match", long.MaxValue.ToString());
            response = client.Post(ServiceUrl["Curve"] + entity.Id, content);
        }

        [TestMethod]
        public void should_not_update_the_curve_in_the_database()
        {
            Assert.AreEqual(startVersion, CurrentEntityVersion());
        }

        [TestMethod]
        public void should_return_a_no_content_status_code()
        {
            Assert.AreEqual(HttpStatusCode.PreconditionFailed, response.StatusCode);
        }

        private static long CurrentEntityVersion()
        {
            return new DbSetRepository<MDM.Curve>(new MappingContext()).FindOne<MDM.Curve>(entity.Id).Version;
        }
    }
}

