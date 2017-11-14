using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartSensors.Data;
using SmartSensors.Data.Models;
using SmartSensors.Data.Models.Sensors;
using SmartSensors.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSensors.Service.UnitTests.SensorServiceTests
{
    [TestClass]
    public class GetMySensors_Should
    {
        [TestMethod]
        public void ReturnAllSensors_OfTheCurrentlyLoggedUser()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();

            string userId = "userId";
            string username = "username";
            string sensorName = "sensorName";
            string sensorDescription = "sensorDescription";

            var users = new List<User>()
            {
                new User() { UserName = username, Id = userId }
            };

            var sensors = new List<Sensor>()
            {
                new Sensor() {Name = sensorName, Description = sensorDescription, Owner = users.Single()},
                new Sensor() {Name = sensorName, Description = sensorDescription}
            };

            var usersSetMock = new Mock<DbSet<User>>().SetupData(users);
            var sensorsSetMock = new Mock<DbSet<Sensor>>().SetupData(sensors);
            var sensorValueProviderMock = new Mock<ISensorValueProvider>();
            var userSharingProviderMock = new Mock<IUserSharingProvider>();

            dbContextMock.SetupGet(x => x.Users).Returns(usersSetMock.Object);
            dbContextMock.SetupGet(x => x.Sensors).Returns(sensorsSetMock.Object);

            var sensorService = new SensorService(dbContextMock.Object, sensorValueProviderMock.Object, userSharingProviderMock.Object);

            //Act
            var mySensorList = sensorService.GetMySensors(username);

            //Assert
            foreach (var sensor in mySensorList)
            {
                Assert.AreEqual(sensor.OwnerName, username);
            }
        }
    }
}
