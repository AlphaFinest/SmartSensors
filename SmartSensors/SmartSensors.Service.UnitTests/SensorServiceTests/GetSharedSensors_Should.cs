using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartSensors.Data;
using SmartSensors.Data.Models;
using SmartSensors.Data.Models.Sensors;
using SmartSensors.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSensors.Service.UnitTests.SensorServiceTests
{
    [TestClass]
    public class GetSharedSensors_Should
    {
        [TestMethod]
        public void ReturnAllSharedSensors_OfTheCurrentlyLoggedUser()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();

            string userId = "userId";
            string username = "username";
            string sensorName = "sensorName";
            string sensorDescription = "sensorDescription";

            var owner = new User() { UserName = "Pesho"};

            var sharedSensorsCollection = new List<Sensor>()
            {
                new Sensor() {Name = sensorName, Description = sensorDescription, Owner = owner},
                new Sensor() {Name = sensorName, Description = sensorDescription, Owner = owner}
            };

            var users = new List<User>()
            {
                new User() { UserName = username, Id = userId, SharedSensors = sharedSensorsCollection }
            };
            foreach (var s in sharedSensorsCollection)
            {
                s.Users = users;
            }

            var usersSetMock = new Mock<DbSet<User>>().SetupData(users);
            var sensorsSetMock = new Mock<DbSet<Sensor>>().SetupData(sharedSensorsCollection);

            dbContextMock.SetupGet(x => x.Users).Returns(usersSetMock.Object);
            dbContextMock.SetupGet(x => x.Sensors).Returns(sensorsSetMock.Object);

            var sensorService = new SensorService(dbContextMock.Object);

            //Act
            List<PublicViewModel> sharedSensorList = sensorService.GetSharedSensors(username);

            //Assert
            foreach (var sensor in sharedSensorList)
            {
                Assert.AreEqual(sensor.SensorName, sensorName);
            }
            
        }
    }
}
