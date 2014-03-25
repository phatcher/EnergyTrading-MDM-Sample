﻿// <autogenerated>
//   This file was generated by T4 code generator CreateIntegrationTestsScript.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

namespace MDM.Client.Sample.IntegrationTests.Person.MdmEntityLocatorFactory
{
    using System.Configuration;
    using System.Linq;

    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.MDM.Contracts.Sample;

    using NUnit.Framework;

    [TestFixture]
    public class NonCachedMdmEntityLocatorFactoryTests : MdmEntityLocatorFactoryIntegrationTestBase
    {
        private Person expected;

        protected override void OnSetup()
        {
			ConfigurationManager.AppSettings["MdmCaching"] = false.ToString();

            base.OnSetup();

            this.expected = PersonData.PostBasicEntity();
        }

        [Test]
        public void ShouldSuccessfullyLocateEntity()
        {
            // given
            var nexusId = this.expected.Identifiers.First(id => id.SystemName == "Nexus");

            // when
            var candidate = this.MdmEntityLocatorService.Get<Person>(nexusId);

            // then
            this.Check(this.expected, candidate);
        }

        [Test]
        public void ShouldReturnDefaultWhenUnableToLocateEntity()
        {
            // given
            var nexusId = new MdmId { SystemName = "Nexus", Identifier = "0" };

            // when
            var candidate = this.MdmEntityLocatorService.Get<Person>(nexusId);

            // then
            Assert.AreEqual(default(Person), candidate);
        }
    }
}
