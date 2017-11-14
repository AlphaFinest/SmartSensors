using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartSensors.Service.Contracts;
using Moq;
using SmartSensors.Controllers;

namespace SmartSensors.Tests.Controllers.RequestControllerTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void CreateInstance_WhenParametersAreCorrect()
        {
            //Arrange
            var serviceMock = new Mock<ISensorService>();

            //Act
            var requestController = new RequestController(serviceMock.Object);

            //Assert
            Assert.IsNotNull(requestController);
        }
    }
}
