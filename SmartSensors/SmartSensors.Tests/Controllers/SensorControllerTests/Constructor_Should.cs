using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartSensors.Controllers;
using SmartSensors.Data;
using SmartSensors.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSensors.Tests.Controllers.SensorControllerTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void CreateInstance_WhenParametersAreCorrect()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var urlProviderMock = new Mock<IUrlProvider>();
            var valueTypeProviderMock = new Mock<IValueTypeProvider>();
            var sensorServiceMock = new Mock<ISensorService>();

            //Act
            var sensorController = new SensorController(dbContextMock.Object, urlProviderMock.Object, valueTypeProviderMock.Object, sensorServiceMock.Object);

            //Assert
            Assert.IsNotNull(sensorController);
        }
        [TestMethod]
        public void ThrowException_WhenContextIsNull()
        {
            //Arrange
            var urlProviderMock = new Mock<IUrlProvider>();
            var valueTypeProviderMock = new Mock<IValueTypeProvider>();
            var sensorServiceMock = new Mock<ISensorService>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new SensorController(null, urlProviderMock.Object, valueTypeProviderMock.Object, sensorServiceMock.Object));
        }
        [TestMethod]
        public void ThrowException_WhenUrlProviderIsNull()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var valueTypeProviderMock = new Mock<IValueTypeProvider>();
            var sensorServiceMock = new Mock<ISensorService>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new SensorController(dbContextMock.Object, null, valueTypeProviderMock.Object, sensorServiceMock.Object));
        }
        [TestMethod]
        public void ThrowException_WhenValueTypeProviderIsNull()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var urlProviderMock = new Mock<IUrlProvider>();
            var sensorServiceMock = new Mock<ISensorService>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new SensorController(dbContextMock.Object, urlProviderMock.Object, null, sensorServiceMock.Object));
        }
        [TestMethod]
        public void ThrowException_WhenSensorServiceIsNull()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var urlProviderMock = new Mock<IUrlProvider>();
            var valueTypeProviderMock = new Mock<IValueTypeProvider>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new SensorController(dbContextMock.Object, urlProviderMock.Object, valueTypeProviderMock.Object, null));
        }
    }
}
