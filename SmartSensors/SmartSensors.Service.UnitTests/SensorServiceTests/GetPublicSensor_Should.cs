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
    public class GetPublicSensor_Should
    {
        [TestMethod]
        public void ReturnCorrectViewModel_WhenParametersAreCorrect()
        {
            //Arrange
            var sensorValueProviderMock = new Mock<ISensorValueProvider>();
            var userSharingProviderMock = new Mock<IUserSharingProvider>();
            var dbContextMock = new Mock<ApplicationDbContext>();
            var sensorDbSetMock = new Mock<DbSet<Sensor>>();

            var user = new User() { UserName = "DefaultUsername" };

            var sensorList = new List<Sensor>()
            {
                new Sensor() {Owner =user,Name="DefaultName",Value="DefaultValue",ValueType="DefaulValuetype",Url="DefaultUrl",IsPublic=true },
                new Sensor() {Owner =user,Name="DefaultNameTwo",Value="DefaultValueTwo",ValueType="DefaulValuetypeTwo",Url="DefaultUrlTwo",IsPublic=true }
            };

            sensorDbSetMock.SetupData(sensorList);

            dbContextMock.Setup(x => x.Sensors).Returns(sensorDbSetMock.Object);

            var sensorService = new SensorService(dbContextMock.Object, sensorValueProviderMock.Object,userSharingProviderMock.Object);

            var expectedResult = 2;

            //Act
            var result = sensorService.GetPublicSensor();

            //Assert
            Assert.AreEqual(result.Count, expectedResult);

        }
    }
}

