using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartSensors.Controllers;
using SmartSensors.Data;
using SmartSensors.Data.Models;
using SmartSensors.Data.Models.Sensors;
using SmartSensors.Service.Contracts;
using SmartSensors.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TestStack.FluentMVCTesting;

namespace SmartSensors.Tests.Controllers.SensorControllerTests
{
    [TestClass]
    public class RegisterSensor_Should
    {
        [TestMethod]
        public void ReturnDefultViewWithCorrectViewModel()
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
        public void ReturnDefultViewWithCorrectViewModel_WhenParametersAreCorrect()
        {
            //Arrange
            var sensorServiceMock = new Mock<ISensorService>();
            var dbContextMock = new Mock<ApplicationDbContext>();
            var urlProviderMock = new Mock<IUrlProvider>();


            var users = new List<User>()
            {
                new User() { UserName="FirstUser"}
            };

            var requestController = new SensorController(dbContextMock.Object, urlProviderMock.Object,  sensorServiceMock.Object);

            var userMock = new Mock<IPrincipal>();
            userMock.SetupGet(x => x.Identity.Name).Returns("FirstUser");

            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(x => x.User).Returns(userMock.Object);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(x => x.HttpContext)
                                 .Returns(contextMock.Object);

            requestController.ControllerContext = controllerContextMock.Object;

            var sensorViewModelMock = new SensorViewModel()
            {
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
                .ShouldRenderDefaultView()
                .WithModel<SensorViewModel>();
        }
    }
}
