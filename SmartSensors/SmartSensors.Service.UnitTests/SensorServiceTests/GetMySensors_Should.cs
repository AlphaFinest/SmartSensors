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
            var sensorValueProviderMock = new Mock<ISensorValueProvider>();
            var userSharingProviderMock = new Mock<IUserSharingProvider>();

            string userId = "userId";
            string username = "username";
            string sensorName = "sensorName";
            string sensorDescription = "sensorDescription";
            
            

            var sensors = new List<Sensor>()
            {
                new Sensor() {Name = sensorName, Description = sensorDescription},
                new Sensor() {Name = sensorName, Description = sensorDescription}
            };

            var user = new User() { UserName = username, Id = userId, MySensors = new List<Sensor>() { sensors[0] } };

            var users = new List<User>()
            {
                user
            };

            sensors[0].Owner = users[0];
            var usersSetMock = new Mock<DbSet<User>>().SetupData(users);

            dbContextMock.SetupGet(x => x.Users).Returns(usersSetMock.Object);



            var sensorService = new SensorService(dbContextMock.Object, sensorValueProviderMock.Object, userSharingProviderMock.Object);

            //Act
            var mySensorList = sensorService.GetMySensors(username);

            //Assert
            foreach (var sensor in mySensorList)
            {
                Assert.AreEqual(sensor.Owner, username);
            }
        }
    }
}
