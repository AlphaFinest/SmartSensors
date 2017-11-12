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
        public void AddSensorToDbContextSensorTable()
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
                PollingInterval = 0,
                ValueType = "DefaultValueType",
                IsPublic = true,
                MinRange = 0,
                MaxRange = 1
            };

            var users = new List<User>()
            {
                new User() { UserName = username, Id = userId }
            };
            var sensors = new List<Sensor>();

            var sensorSetMock = new Mock<DbSet<Sensor>>().SetupData(sensors);
            var usersSetMock = new Mock<DbSet<User>>().SetupData(users);

            dbContextMock.SetupGet(x => x.Sensors).Returns(sensorSetMock.Object);
            dbContextMock.SetupGet(x => x.Users).Returns(usersSetMock.Object);

            var sensorService = new SensorService(dbContextMock.Object);

            //Act
            sensorService.RegisterNewSensor(sensor, username);

            //Assert
            var sensorDb = dbContextMock.Object.Sensors.Single();
            
            Assert.AreEqual(sensorDb.Name, sensor.Name);
            Assert.AreEqual(sensorDb.Description, sensor.Description);
            Assert.AreEqual(sensorDb.Url, sensor.Url);
            Assert.AreEqual(sensorDb.PollingInterval, sensor.PollingInterval);
            Assert.AreEqual(sensorDb.ValueType, sensor.ValueType);
            Assert.AreEqual(sensorDb.MinRange, sensor.MinRange);
            Assert.AreEqual(sensorDb.MaxRange, sensor.MaxRange);
            Assert.AreEqual(sensorDb.IsPublic, sensor.IsPublic);

            dbContextMock.Verify(m => m.SaveChanges(), Times.Once());
        }

    }
}
