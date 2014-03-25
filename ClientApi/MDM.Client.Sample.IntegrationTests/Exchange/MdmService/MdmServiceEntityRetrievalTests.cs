﻿// <autogenerated>
//   This file was generated by T4 code generator CreateIntegrationTestsScript.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

namespace MDM.Client.Sample.IntegrationTests.Exchange.MdmService
{
    using System.Configuration;
    using System.Linq;

    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.MDM.Contracts.Sample;

    using NUnit.Framework;

    [TestFixture]
    public class MdmServiceBasicIntegrationTests : MdmServiceIntegrationTestBase
    {
        private Exchange exchange;

        protected override void OnSetup()
        {
			ConfigurationManager.AppSettings["MdmCaching"] = false.ToString();

            base.OnSetup();

            this.exchange = ExchangeData.PostBasicEntity();
        }

        [Test]
        public void ShouldGetByIntId()
        {
            // given
            var id = int.Parse(this.exchange.ToMdmId().Identifier);

            // when
            var response = this.MdmService.Get<Exchange>(id);

            // then
            Assert.IsTrue(response.IsValid, "###Error : " + response.Code + " : " + (response.Fault == null ? string.Empty : response.Fault.Message + " : " + response.Fault.Reason));
            this.Check(this.exchange, response.Message);
        }

        [Test]
        public void ShouldGetByMdmId()
        {
            // given
            var nexusId = this.exchange.ToMdmId();

            // when
            var response = this.MdmService.Get<Exchange>(nexusId);

            // then
            Assert.IsTrue(response.IsValid, "###Error : " + response.Code + " : " + (response.Fault == null ? string.Empty : response.Fault.Message + " : " + response.Fault.Reason));
            this.Check(this.exchange, response.Message);
        }

        [Test]
        public void ShouldGetMapping()
        {
            // given
            var id = int.Parse(this.exchange.ToMdmId().Identifier);

            // when
            var response = this.MdmService.GetMapping<Exchange>(id, nexusId => nexusId.SystemName == "Endur");

            // then
            Assert.IsTrue(response.IsValid, "###Error : " + response.Code + " : " + (response.Fault == null ? string.Empty : response.Fault.Message + " : " + response.Fault.Reason));
            Assert.AreEqual(this.exchange.Identifiers.First(i => i.SystemName == "Endur").Identifier, response.Message.Identifier);
        }

        [Test]
        public void ShouldMap()
        {
            // given
            var id = int.Parse(this.exchange.ToMdmId().Identifier);

            // when
            var response = this.MdmService.Map<Exchange>(id, "Endur");

            // then
            Assert.IsTrue(response.IsValid, "###Error : " + response.Code + " : " + (response.Fault == null ? string.Empty : response.Fault.Message + " : " + response.Fault.Reason));
            Assert.AreEqual(this.exchange.Identifiers.First(i => i.SystemName == "Endur").Identifier, response.Message.Identifier);
        }

        [Test]
        public void ShouldCrossMap()
        {
            // given
            var sourceIdentifier = this.exchange.Identifiers.First(i => i.SystemName == "Endur");

            // when
            var response = this.MdmService.CrossMap<Exchange>(sourceIdentifier, "Trayport");

            // then
            Assert.IsTrue(response.IsValid, "###Error : " + response.Code + " : " + (response.Fault == null ? string.Empty : response.Fault.Message + " : " + response.Fault.Reason));
            Assert.AreEqual(this.exchange.Identifiers.First(i => i.SystemName == "Trayport").Identifier, response.Message.Mappings[0].Identifier);
        }
    }
}
