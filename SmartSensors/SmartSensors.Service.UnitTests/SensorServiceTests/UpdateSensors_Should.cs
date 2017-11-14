using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartSensors.Data;
using SmartSensors.Data.Models.Sensors;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using SmartSensors.Service;

namespace SmartSensors.Service.UnitTests.SensorServiceTests
{
    [TestClass]
    public class UpdateSensors_Should
    {
        [TestMethod]
        public async Task UpdateAllRegisteredSensors_WhenActionMethodIsCalledAsync()
        {
            ////Assert
            //var dbContextMock = new Mock<ApplicationDbContext>();

            //var sensors = new List<Sensor>();

            //var sensorsSetMock = new Mock<DbSet<Sensor>>().SetupData(sensors);

            //dbContextMock.SetupGet(d => d.Sensors).Returns(sensorsSetMock.Object);

            //var service = new SensorService(dbContextMock.Object);

            ////Act
            //await service.UpdateSensors();
            //dbContextMock.Verify(d => d.SaveChangesAsync());
        }
    }
}
