using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartSensors.Areas.Admin.Controllers;
using Microsoft.AspNet.Identity;
using Moq;
using SmartSensors.Data.Models;
using SmartSensors.Data;
using SmartSensors.Service.Contracts;
using SmartSensors.Service.ViewModels;
using TestStack.FluentMVCTesting;
using System.Collections.Generic;
using SmartSensors.Tests.Helpers;

namespace SmartSensors.Tests.Areas.Admin.Controllers.AdminControllerTests
{
    [TestClass]
    public class EditSensor_Should
    {
        [TestMethod]
        public void ReturnDefaultViewWithTheRightViewModel_WhenTheIdIsValid()
        {
            //Arrange
            var userServiceMock = new Mock<IUserService>();
            var sensorServiceMock = new Mock<ISensorService>();
            var urlProviderMock = new Mock<IUrlProvider>();

            var controller = new AdminController(userServiceMock.Object, sensorServiceMock.Object, urlProviderMock.Object);

            var sensorViewModel = new SensorViewModel()
            {
                Id = 12,
                Owner = "user",
                Name = "sensor",
                Description = "description",
                Url = "url",
                PollingInterval = 50,
                IsPublic = true,
                MinRange = 8,
                MaxRange = 12,
                SharedWith = "user"
            };

            sensorServiceMock.Setup(s => s.GetSpecificSensor(sensorViewModel.Id)).Returns(sensorViewModel);

            //Act & Assert
            controller
                .WithCallTo(x => x.EditSensor(sensorViewModel.Id))
                .ShouldRenderDefaultView()
                .WithModel<SensorViewModel>( m => m.Description == sensorViewModel.Description);
        }

        [TestMethod]
        public void RedirectToMySensors_WhenTheViewModelIsCorrect()
        {
            //Arrange
            var userServiceMock = new Mock<IUserService>();
            var sensorServiceMock = new Mock<ISensorService>();
            var urlProviderMock = new Mock<IUrlProvider>();
            
            var users = new List<User>()
            {
                new User() { UserName="FirstUser"}
            };
            
            var controller = new AdminController(userServiceMock.Object, sensorServiceMock.Object, urlProviderMock.Object);

            controller.UserMocking(users[0].UserName);

            var sensorViewModel = new SensorViewModel()
            {
                Id = 12,
                Owner = "user",
                Name = "sensor",
                Description = "description",
                Url = "url",
                PollingInterval = 50,
                IsPublic = true,
                MinRange = 8,
                MaxRange = 12,
                SharedWith = "user"
            };

            //Act & Assert
            controller
                .WithCallTo(x => x.EditSensor(sensorViewModel))
                .ShouldRedirectTo<AdminController>(c => c.AllSensors());

            sensorServiceMock.Verify(s => s.EditSensor(sensorViewModel), Times.Once);
        }
    }
}

