using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartSensors.Service.Contracts;
using Moq;
using SmartSensors.Data;
using System.Collections.Generic;
using SmartSensors.Data.Models;
using SmartSensors.Controllers;
using SmartSensors.Tests.Helpers;
using SmartSensors.Service.ViewModels;
using TestStack.FluentMVCTesting;
using System.Threading.Tasks;

namespace SmartSensors.Tests.Controllers.SensorControllerTests
{
    [TestClass]
    public class EditSensor_Should
    {
        [TestMethod]
        public void ReturnDefaultViewWithTheRightViewModel_WhenTheIdIsValid()
        {
            //Arrange
            var sensorServiceMock = new Mock<ISensorService>();
            var dbContextMock = new Mock<ApplicationDbContext>();
            var urlProviderMock = new Mock<IUrlProvider>();
            
            var controller = new SensorController(dbContextMock.Object, urlProviderMock.Object, sensorServiceMock.Object);
            
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
                .WithModel<SensorViewModel>(m => m.Name == sensorViewModel.Name);
        }

        [TestMethod]
        public void RedirectToMySensors_WhenTheViewModelIsCorrect()
        {
            //Arrange
            var sensorServiceMock = new Mock<ISensorService>();
            var dbContextMock = new Mock<ApplicationDbContext>();
            var urlProviderMock = new Mock<IUrlProvider>();
            
            var users = new List<User>()
            {
                new User() { UserName="FirstUser"}
            };

            sensorServiceMock.Setup(s => s.EditSensorOwner(new SensorViewModel())).Throws<ArgumentNullException>();

            var controller = new SensorController(dbContextMock.Object, urlProviderMock.Object, sensorServiceMock.Object);

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
                .ShouldRedirectTo<SensorController>(c => c.MySensors());

            sensorServiceMock.Verify(s => s.EditSensorOwner(sensorViewModel), Times.Once);
        }
    }
}
