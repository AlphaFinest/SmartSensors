using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartSensors.Data;
using SmartSensors.Service.Seeding;
using SmartSensors.Service.Contracts;

namespace SmartSensors.Service.UnitTests.Seeding.UrlsSeederTests
{
    [TestClass]
    public class Seed_Should
    {
        [TestMethod]
        public void FillDatabaseWithUrls()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var urlDatabaseProviderMock = new Mock<IUrlDataBaseProvider>();

            var seeder = new UrlsSeeder(dbContextMock.Object, urlDatabaseProviderMock.Object);

            //Act
            seeder.Seed();

            //Assert
            urlDatabaseProviderMock.Verify(p => p.ProvideUrls(), Times.Once);
        }
    }
}
