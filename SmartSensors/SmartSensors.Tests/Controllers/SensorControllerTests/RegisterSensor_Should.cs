using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartSensors.Controllers;
using SmartSensors.Data;
using SmartSensors.Data.Models;
using SmartSensors.Data.Models.Sensors;
using SmartSensors.Service.Contracts;
using SmartSensors.Service.ViewModels;
using SmartSensors.Tests.Helpers;
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

            requestController.UserMocking(users[0].UserName);

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
                .ShouldRedirectTo<HomeController>(c => c.Index());
        }
    }
}
