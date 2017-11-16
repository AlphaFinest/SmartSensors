using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartSensors.Data;
using SmartSensors.Data.Models;
using SmartSensors.Data.Models.Sensors;
using SmartSensors.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSensors.Service.UnitTests.SensorServiceTests
{
    [TestClass]
    public class GetSharedWithLikeString_Should
    {
        [TestMethod]
        public void ReturnAllSubscribersToTheSensorAsString_WhenParametersAreCorrect()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var valueTypeProviderMock = new Mock<ISensorValueProvider>();
            var userSharingProviderMock = new Mock<IUserSharingProvider>();

            var usersList = new List<User>()
            {
                new User() {UserName="FirstUser"},
                new User() {UserName="SecondUser"},
                new User() {UserName="ThirdUser"}

            };

            var sensor = new Sensor()
            {
                Users = usersList
            };

            var sensorService = new SensorService(dbContextMock.Object, valueTypeProviderMock.Object, userSharingProviderMock.Object);

            var expectedResult = String.Join(", ", usersList.Select(x => x.UserName).ToList());

            //Act

            var result = sensorService.GetSharedWithLikeString(sensor);

            //Assert
            Assert.AreEqual(result, expectedResult);



        }

        [TestMethod]
        public void ThrowException_WhenParameterIsNull()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var valueTypeProviderMock = new Mock<ISensorValueProvider>();
            var userSharingProviderMock = new Mock<IUserSharingProvider>();

            var usersList = new List<User>()
            {
                new User() {UserName="FirstUser"},
                new User() {UserName="SecondUser"},
                new User() {UserName="ThirdUser"}

            };

            Sensor sensor = null;

            var sensorService = new SensorService(dbContextMock.Object, valueTypeProviderMock.Object, userSharingProviderMock.Object);

            var expectedResult = String.Join(", ", usersList.Select(x => x.UserName).ToList());

            //Act && Assert
            Assert.ThrowsException<NullReferenceException>(() => sensorService.GetSharedWithLikeString(sensor));
        }
    }
}
