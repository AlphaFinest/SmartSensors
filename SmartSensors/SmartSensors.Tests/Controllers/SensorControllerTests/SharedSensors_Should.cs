using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartSensors.Service.Contracts;
using Moq;
using SmartSensors.Data;
using SmartSensors.Service.ViewModels;
using System.Collections.Generic;
using SmartSensors.Data.Models;
using SmartSensors.Controllers;
using SmartSensors.Tests.Helpers;
using TestStack.FluentMVCTesting;

namespace SmartSensors.Tests.Controllers.SensorControllerTests
{
    [TestClass]
    public class SharedSensors_Should
    {
        [TestMethod]
        public void ReturnTheCorrectViewModel()
        {
            //Arrange
            var sensorServiceMock = new Mock<ISensorService>();
            var dbContextMock = new Mock<ApplicationDbContext>();
            var urlProviderMock = new Mock<IUrlProvider>();
            var valueTypeProviderMock = new Mock<IValueTypeProvider>();

            var sensors = new List<PublicViewModel>()
            {
                new PublicViewModel(){ SensorName = "Sensor1" },
                new PublicViewModel(){ SensorName = "Sensor2" }
            };

            var user = new User() { UserName = "FirstUser" };

            sensorServiceMock.Setup(s => s.GetSharedSensors(user.UserName)).Returns(sensors);

            var controller = new SensorController(dbContextMock.Object, urlProviderMock.Object, valueTypeProviderMock.Object, sensorServiceMock.Object);

            controller.UserMocking(user.UserName);

            //Act & Assert
            controller
                .WithCallTo(c => c.SharedSensors())
                .ShouldRenderDefaultView()
                .WithModel<List<PublicViewModel>>(m =>
                {
                    for (int i = 0; i < m.Count; i++)
                    {
                        Assert.AreEqual(m[i].SensorName, sensors[i].SensorName);
                    }
                });
        }
    }
}
