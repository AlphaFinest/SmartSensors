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
            var urlProviderMock = new Mock<IUrlProvider>();
            var sensorServiceMock = new Mock<ISensorService>();

            //Act
            var sensorController = new SensorController(urlProviderMock.Object, sensorServiceMock.Object);

            //Assert
            Assert.IsNotNull(sensorController);
        }
        [TestMethod]
        public void ThrowException_WhenUrlProviderIsNull()
        {
            //Arrange
            var sensorServiceMock = new Mock<ISensorService>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new SensorController(null, sensorServiceMock.Object));
        }
        [TestMethod]
        public void ThrowException_WhenSensorServiceIsNull()
        {
            //Arrange
            var urlProviderMock = new Mock<IUrlProvider>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new SensorController(urlProviderMock.Object, null));
        }
    }
}
