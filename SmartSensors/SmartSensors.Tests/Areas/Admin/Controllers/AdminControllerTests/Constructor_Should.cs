using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartSensors.Data.Models;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using System.Linq;
using SmartSensors.Areas.Admin.Controllers;
using TestStack.FluentMVCTesting;
using SmartSensors.Service.Contracts;
using SmartSensors.Data;
using SmartSensors.Service.ViewModels;
using System;

namespace SmartSensors.Tests.Areas.Admin.Controllers.AdminControllerTests
{
    [TestClass]
    public class Constructor
    {
        [TestMethod]
        public void CreateInstance_WhenParametersAreCorrect()
        {
            //Arrange
            var userServiceMock = new Mock<IUserService>();
            var sensorServiceMock = new Mock<ISensorService>();
            var urlProviderMock = new Mock<IUrlProvider>();

            //Act
            var adminController = new AdminController(userServiceMock.Object, sensorServiceMock.Object, urlProviderMock.Object);

            //Assert
            Assert.IsNotNull(adminController);
        }

        [TestMethod]
        public void ThrowException_WhenUserServiceIsNull()
        {
            //Arrange
            var userServiceMock = new Mock<IUserService>();
            var sensorServiceMock = new Mock<ISensorService>();
            var urlProviderMock = new Mock<IUrlProvider>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new AdminController(null, sensorServiceMock.Object, urlProviderMock.Object));
        }
        [TestMethod]
        public void ThrowException_WhenSensorServiceIsNull()
        {
            //Arrange
            var userServiceMock = new Mock<IUserService>();
            var sensorServiceMock = new Mock<ISensorService>();
            var urlProviderMock = new Mock<IUrlProvider>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new AdminController(userServiceMock.Object, null, urlProviderMock.Object));
        }
        [TestMethod]
        public void ThrowException_WhenUrlProviderIsNull()
        {
            //Arrange
            var userServiceMock = new Mock<IUserService>();
            var sensorServiceMock = new Mock<ISensorService>();
            var urlProviderMock = new Mock<IUrlProvider>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new AdminController(userServiceMock.Object, sensorServiceMock.Object, null));
        }

    }
}
