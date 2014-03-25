namespace EnergyTrading.MDM.Test.Contracts.Mappers
{
    using NUnit.Framework;

    using Moq;

    using EnergyTrading.Data;

    [TestFixture]
    public class ExchangeDetailsMapperFixture : Fixture
    {
        [Test]
        public void Map()
        {
            var mockRepository = new Mock<IRepository>();
            var fakeParty = new MDM.Party();

            mockRepository.Setup(repository => repository.FindOne<MDM.Party>(1)).Returns(fakeParty);
            // Arrange
            var source = new EnergyTrading.MDM.Contracts.Sample.ExchangeDetails
                {
					Name = "test"
                };

            var mapper = new EnergyTrading.MDM.Contracts.Mappers.ExchangeDetailsMapper(mockRepository.Object);

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.IsNotNull(result);
			Assert.AreEqual(source.Name, result.Name);
        }
    }
}
		
