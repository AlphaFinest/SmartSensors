using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartSensors.Data;
using SmartSensors.Data.Models.Sensors;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using SmartSensors.Service;
using SmartSensors.Service.Contracts;
using System.Net.Http;
using RichardSzalay.MockHttp;
using SmartSensors.Service.ViewModels;
using EntityFramework.Testing;
using System.Data.Entity.Infrastructure;
using SmartSensors.Data.Models;
using SmartSensors.Service.UnitTests.MockHelpers;

namespace SmartSensors.Service.UnitTests.SensorServiceTests
{
    [TestClass]
    public class UpdateSensors_Should
    {
        [TestMethod]
        public async Task UpdateAllRegisteredSensors_WhenActionMethodIsCalledAsync()
        {
            //Assert
            var dbContextMock = new Mock<ApplicationDbContext>();
            var valueProviderMock = new Mock<ISensorValueProvider>();
            var userSharingProviderMock = new Mock<IUserSharingProvider>();
            var historiesMock = new Mock<DbSet<History>>();
            var dateTime = DateTime.Now;
            
            var sensorList = new List<Sensor>()
            {
                new Sensor(),
                new Sensor()
            };

            var history = new List<History>();

            DateTime date = DateTime.Now;
            List<JsonSensorViewModel> viewModels = new List<JsonSensorViewModel>()
            {
                new JsonSensorViewModel() { Value = "value1" },
                new JsonSensorViewModel() { Value = "value2" }
            };

            historiesMock.SetupData(history);
            dbContextMock.Setup(x => x.History).Returns(historiesMock.Object);
            //Act

            var sensorService = new SensorServiceMock(dbContextMock.Object, valueProviderMock.Object, userSharingProviderMock.Object,viewModels,sensorList,dateTime);
            await sensorService.UpdateSensors();

            //Assert
            dbContextMock.Verify(x => x.SaveChanges(), Times.Once);
            Assert.AreEqual(await historiesMock.Object.CountAsync(), 2);
        }
    }
}
