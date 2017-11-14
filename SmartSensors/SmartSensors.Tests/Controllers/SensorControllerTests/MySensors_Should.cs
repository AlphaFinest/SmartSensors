using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartSensors.Service.Contracts;
using Moq;
using SmartSensors.Data;
using SmartSensors.Controllers;
using TestStack.FluentMVCTesting;
using SmartSensors.Service.ViewModels;
using SmartSensors.Data.Models;
using System.Linq;
using System.Data.Entity;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using SmartSensors.Data.Models.Sensors;

namespace SmartSensors.Tests.Controllers.SensorControllerTests
{
    [TestClass]
    public class MySensors_Should
    {
        [TestMethod]
        public void ReturnTheCorrectViewModel()
        {
            //TODO
            //Arrange
            var sensorServiceMock = new Mock<ISensorService>();
            var dbContextMock = new Mock<ApplicationDbContext>();
            var urlProviderMock = new Mock<IUrlProvider>();

            var sensors = new List<PublicViewModel>()
            {
                new PublicViewModel(){ SensorName = "Sensor1" },
                new PublicViewModel(){ SensorName = "Sensor2" }
            };

            var userMock = new Mock<IPrincipal>();
            userMock.SetupGet(x => x.Identity.Name).Returns("FirstUser");

            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(x => x.User).Returns(userMock.Object);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(x => x.HttpContext)
                                 .Returns(contextMock.Object);



            var controller = new SensorController(dbContextMock.Object, urlProviderMock.Object, sensorServiceMock.Object);

            controller.ControllerContext = controllerContextMock.Object;


            //Act & Assert
            controller
                .WithCallTo(c => c.MySensors())
                .ShouldRenderDefaultView()
                .WithModel<List<PublicViewModel>>
            (viewModel =>
            {
                for (int i = 0; i < viewModel.Count; i++)
                {
                    Assert.AreEqual(viewModel[i].SensorName, sensors[i].SensorName);
                }
            });
        }
    }
}
