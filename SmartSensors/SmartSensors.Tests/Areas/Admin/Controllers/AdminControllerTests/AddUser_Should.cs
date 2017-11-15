using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartSensors.Service.Contracts;
using Moq;
using SmartSensors.Data;
using SmartSensors.Areas.Admin.Controllers;
using TestStack.FluentMVCTesting;
using System.Collections.Generic;
using SmartSensors.Data.Models;
using SmartSensors.Service.ViewModels;

namespace SmartSensors.Tests.Areas.Admin.Controllers.AdminControllerTests
{
    [TestClass]
    public class AddUser_Should
    {
        [TestMethod]
        public void ReturnDefaultViewWithCorrectViewModel()
        {
            //Arrange
            var userServiceMock = new Mock<IUserService>();
            var sensorServiceMock = new Mock<ISensorService>();
            var urlProviderMock = new Mock<IUrlProvider>();

            var requestController = new AdminController(userServiceMock.Object, sensorServiceMock.Object, urlProviderMock.Object);

            //Act & Assert
            requestController
                .WithCallTo(x => x.AddUser())
                .ShouldRenderDefaultView();
        }

        [TestMethod]
        public void ReturnDefaultViewWithCorrectViewModel_WhenParametersAreCorrect()
        {
            //Arrange
            var userServiceMock = new Mock<IUserService>();
            var sensorServiceMock = new Mock<ISensorService>();
            var urlProviderMock = new Mock<IUrlProvider>();

            var requestController = new AdminController(userServiceMock.Object, sensorServiceMock.Object, urlProviderMock.Object);

            var addUserViewModel = new AddUserViewModel()
            {
                Username = "DefaultName",
                Email = "Def@def",
                Password = "defDef"
            };

            //Act & Assert
            requestController
                .WithCallTo(x => x.AddUser())
                .ShouldRenderDefaultView();
        }

    }
}
