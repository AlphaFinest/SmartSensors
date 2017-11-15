using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartSensors.Data;
using SmartSensors.Data.Models.Sensors;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.Testing;
using SmartSensors.Service.Contracts;
using SmartSensors.Service.ViewModels;
using SmartSensors.Data.Models;


namespace SmartSensors.Service.UnitTests.SensorServiceTests
{
    [TestClass]
    public class RegisterNewSensor_Should
    {
        [TestMethod]
        public async Task AddSensorToDbContextSensorTable()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();

            string userId = "userId";
            string username = "username";

            var sensor = new SensorViewModel()
            {
                Name = "DefaultName",
                Description = "DefaultDescription",
                Url = "DefaultUrl",
                PollingInterval = 12,
                IsPublic = true,
                MinRange = 3,
                MaxRange = 5,       
                SharedWith="DefaultUser"
            };

            var users = new List<User>()
            {
                new User() { UserName = username, Id = userId }
            };

            var urls = new List<Url>()
            {
                new Url() {ValueType="Default", SensorUrl="DefaultUrl"}
            };
            var sensors = new List<Sensor>();

            var sensorSetMock = new Mock<DbSet<Sensor>>().SetupData(sensors);
            var usersSetMock = new Mock<DbSet<User>>().SetupData(users);
            var urlsSetMock = new Mock<DbSet<Url>>().SetupData(urls);
            var sensorValueProviderMock = new Mock<ISensorValueProvider>();

            var userSharingProviderMock = new Mock<IUserSharingProvider>(); 

            dbContextMock.SetupGet(x => x.Sensors).Returns(sensorSetMock.Object);
            dbContextMock.SetupGet(x => x.Users).Returns(usersSetMock.Object);
            dbContextMock.SetupGet(x => x.Urls).Returns(urlsSetMock.Object);

            sensorValueProviderMock.Setup(x => x.GetValue(sensor.Url)).ReturnsAsync("");
            userSharingProviderMock.Setup(x => x.GetSubscribers(sensor.SharedWith)).Returns(users);



            var sensorService = new SensorService(dbContextMock.Object,sensorValueProviderMock.Object,userSharingProviderMock.Object);

            //Act
             await  sensorService.RegisterNewSensor(sensor, username);

            //Assert
            var sensorDb = dbContextMock.Object.Sensors.Single();
              
            Assert.AreEqual(sensorDb.Name, sensor.Name);
            Assert.AreEqual(sensorDb.Description, sensor.Description);
            Assert.AreEqual(sensorDb.Url, sensor.Url);
            Assert.AreEqual(sensorDb.PollingInterval, sensor.PollingInterval);
            Assert.AreEqual(sensorDb.MinRange, sensor.MinRange);
            Assert.AreEqual(sensorDb.MaxRange, sensor.MaxRange);
            Assert.AreEqual(sensorDb.IsPublic, sensor.IsPublic);

            dbContextMock.Verify(m => m.SaveChanges(), Times.Once());
        }

    }
}
