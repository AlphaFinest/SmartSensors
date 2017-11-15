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
using SmartSensors.Controllers;
using SmartSensors.Tests.Helpers;
using SmartSensors.Service.ViewModels;

namespace SmartSensors.Tests.Areas.Admin.Controllers.AdminControllerTests
{
    [TestClass]
    public class RegisterSensor_Should
    {
        [TestMethod]
        public void ReturnDefaultViewWithCorrectViewModel()
        {
            //Arrange
            var sensorServiceMock = new Mock<ISensorService>();
            var dbContextMock = new Mock<ApplicationDbContext>();
            var urlProviderMock = new Mock<IUrlProvider>();

            var requestController = new SensorController(dbContextMock.Object, urlProviderMock.Object, sensorServiceMock.Object);

            //Act & Assert
            requestController
                .WithCallTo(x => x.RegisterSensor())
                .ShouldRenderDefaultView();
        }

        [TestMethod]
        public void ReturnDefaultViewWithCorrectViewModel_WhenParametersAreCorrect()
        {
            //Arrange
            var sensorServiceMock = new Mock<ISensorService>();
            var dbContextMock = new Mock<ApplicationDbContext>();
            var urlProviderMock = new Mock<IUrlProvider>();


            var users = new List<User>()
            {
                new User() { UserName="FirstUser"}
            };

            var requestController = new SensorController(dbContextMock.Object, urlProviderMock.Object, sensorServiceMock.Object);

            requestController.UserMocking(users[0].UserName);

            var sensorViewModelMock = new SensorViewModel()
            {
                Owner = "DefaultOwner",
                Name = "DefaultName",
                Description = "DefaultDescription",
                Url = "DefaultUrl",
                PollingInterval = 50,
                IsPublic = true,
                MinRange = 8,
                MaxRange = 12
            };

            //Act & Assert
            requestController
                .WithCallTo(x => x.RegisterSensor(sensorViewModelMock))
                .ShouldRedirectToRoute("");
        }
    }
}
