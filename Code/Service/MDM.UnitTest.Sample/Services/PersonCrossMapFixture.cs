﻿namespace EnergyTrading.MDM.Test.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NUnit.Framework;

    using Moq;

    using EnergyTrading.MDM.Contracts.Sample; using EnergyTrading.Mdm.Contracts;
    using EnergyTrading;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Search;
    using EnergyTrading.Validation;
    using EnergyTrading.MDM;
    using EnergyTrading.MDM.Messages;
    using EnergyTrading.MDM.Services;

    [TestFixture]
    public class PersonCrossMapFixture
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullRequestErrors()
        {
            // Arrange
            var validatorFactory = new Mock<IValidatorEngine>(); 
            var mappingEngine = new Mock<IMappingEngine>();
            var repository = new Mock<IRepository>();
            var searchCache = new Mock<ISearchCache>();
            var service = new PersonService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            // Act
            service.CrossMap(null);
        }

        [Test]
        public void UnsuccessfulMatchReturnsNotFound()
        {
            // Arrange
            var validatorFactory = new Mock<IValidatorEngine>(); 
            var mappingEngine = new Mock<IMappingEngine>();
            var repository = new Mock<IRepository>();
            var searchCache = new Mock<ISearchCache>();
            var service = new PersonService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            var list = new List<PersonMapping>();
            repository.Setup(x => x.Queryable<PersonMapping>()).Returns(list.AsQueryable());

            var request = new CrossMappingRequest { SystemName = "Endur", Identifier = "A", ValidAt = SystemTime.UtcNow(), TargetSystemName = "Trayport" };

            // Act
            var contract = service.CrossMap(request);

            // Assert
            Assert.IsNotNull(contract, "Contract null");
            Assert.IsFalse(contract.IsValid, "Contract valid");
            Assert.AreEqual(ErrorType.NotFound, contract.Error.Type, "ErrorType difers");
        }

        [Test]
        public void SuccessMatch()
        {
            // Arrange
            var validatorFactory = new Mock<IValidatorEngine>(); 
            var mappingEngine = new Mock<IMappingEngine>();
            var repository = new Mock<IRepository>();
            var searchCache = new Mock<ISearchCache>();
            var service = new PersonService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            // Domain details
            var system = new MDM.SourceSystem { Name = "Endur" };
            var mapping = new PersonMapping
                {
                    System = system,
                    MappingValue = "A"
                };
            var targetSystem = new MDM.SourceSystem { Name = "Trayport" };
            var targetMapping = new PersonMapping
                {
                    System = targetSystem,
                    MappingValue = "B",
                    IsDefault = true
                };
            var details = new MDM.PersonDetails
                {
                    FirstName = "John",
                    LastName = "Smith",
                    Email = "test@test.com"
                };
            var person = new MDM.Person
                {
                    Id = 1
                };
            person.AddDetails(details);
            person.ProcessMapping(mapping);
            person.ProcessMapping(targetMapping);

            // Contract details
            var targetIdentifier = new MdmId
                {
                    SystemName = "Trayport",
                    Identifier = "B"
                };

            mappingEngine.Setup(x => x.Map<PersonMapping, MdmId>(targetMapping)).Returns(targetIdentifier);

            var list = new List<PersonMapping> { mapping };
            repository.Setup(x => x.Queryable<PersonMapping>()).Returns(list.AsQueryable());

            var request = new CrossMappingRequest
            {
                SystemName = "Endur",
                Identifier = "A",
                TargetSystemName = "trayport",
                ValidAt = SystemTime.UtcNow(),
                Version = 1
            };

            // Act
            var response = service.CrossMap(request);
            var candidate = response.Contract;

            // Assert
            Assert.IsNotNull(response, "Contract null");
            Assert.IsTrue(response.IsValid, "Contract invalid");
            Assert.IsNotNull(candidate, "Mapping null");
            Assert.AreEqual(1, candidate.Mappings.Count, "Identifier count incorrect");
            Assert.AreSame(targetIdentifier, candidate.Mappings[0], "Different identifier assigned");
        }

        [Test]
        public void SuccessMatchCurrentVersion()
        {
            // Arrange
            var validatorFactory = new Mock<IValidatorEngine>();
            var mappingEngine = new Mock<IMappingEngine>();
            var repository = new Mock<IRepository>();
            var searchCache = new Mock<ISearchCache>();
            var service = new PersonService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            // Domain details
            var system = new MDM.SourceSystem { Name = "Endur" };
            var mapping = new PersonMapping
            {
                System = system,
                MappingValue = "A"
            };
            var targetSystem = new MDM.SourceSystem { Name = "Trayport" };
            var targetMapping = new PersonMapping
            {
                System = targetSystem,
                MappingValue = "B",
                IsDefault = true
            };
            var details = new MDM.PersonDetails
            {
                FirstName = "John",
                LastName = "Smith",
                Email = "test@test.com"
            };
            var person = new MDM.Person
            {
                Id = 1
            };
            person.AddDetails(details);
            person.ProcessMapping(mapping);
            person.ProcessMapping(targetMapping);

            // Contract details
            var targetIdentifier = new MdmId
            {
                SystemName = "Trayport",
                Identifier = "B"
            };

            mappingEngine.Setup(x => x.Map<PersonMapping, MdmId>(targetMapping)).Returns(targetIdentifier);

            var list = new List<PersonMapping> { mapping };
            repository.Setup(x => x.Queryable<PersonMapping>()).Returns(list.AsQueryable());

            var request = new CrossMappingRequest
            {
                SystemName = "Endur",
                Identifier = "A",
                TargetSystemName = "trayport",
                ValidAt = SystemTime.UtcNow(),
                Version = 0
            };

            // Act
            var response = service.CrossMap(request);
            var candidate = response.Contract;

            // Assert
            Assert.IsNotNull(response, "Contract null");
            Assert.IsTrue(response.IsValid, "Contract invalid");
            Assert.IsNull(candidate, "Mapping not null");
        }
    }
}