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

namespace SmartSensors.Tests.Areas.Admin.Controllers.AdminControllerTests
{
    [TestClass]
    public class AllSensors_Should
    {
        [TestMethod]
        public void ReturnTheCorrectViewModel()
        {
            //Arrange
            var sensorServiceMock = new Mock<ISensorService>();
            var userServiceMock = new Mock<IUserService>();
            var dbContextMock = new Mock<ApplicationDbContext>();
            var urlProviderMock = new Mock<IUrlProvider>();
            var userManagerMock = new Mock<ApplicationUserManager>();

            var sensors = new List<FullSensorViewModel>()
            {
                new FullSensorViewModel(){ Name = "Sensor1" },
                new FullSensorViewModel(){ Name = "Sensor2" }
            };

            //var user = new User() { UserName = "FirstUser" };

            sensorServiceMock.Setup(s => s.GetAllSensors()).Returns(sensors);

            var controller = new AdminController( userServiceMock.Object, sensorServiceMock.Object, urlProviderMock.Object);

            //Act & Assert
            controller
                .WithCallTo(c => c.AllSensors())
                .ShouldRenderDefaultView()
                .WithModel<List<FullSensorViewModel>>
            (viewModel =>
            {
                for (int i = 0; i < viewModel.Count; i++)
                {
                    Assert.AreEqual(viewModel[i].Name, sensors[i].Name);
                }
            });



        }
    }
}
