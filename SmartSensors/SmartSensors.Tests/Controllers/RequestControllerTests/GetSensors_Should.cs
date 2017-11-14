using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartSensors.Service.Contracts;
using SmartSensors.Controllers;
using Moq;
using System.Threading.Tasks;

namespace SmartSensors.Tests.Controllers.SensorControllerTests
{
    [TestClass]
    public class UpdateSensors_Should
    {
        [TestMethod]
        public async Task CallUpdateSensors_WhenActionIsCalledAsync()
        {
            //Arrange
            var serviceMock = new Mock<ISensorService>();
            var requestController = new RequestController(serviceMock.Object);

            //Act & Assert
            await requestController.GetSensors();
            serviceMock.Verify(s => s.UpdateSensors(), Times.Once);
        }
    }
}
