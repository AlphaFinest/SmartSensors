using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartSensors.Controllers;
using SmartSensors.Data;
using SmartSensors.Data.Models.Sensors;
using SmartSensors.Service.Contracts;
using SmartSensors.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TestStack.FluentMVCTesting;

namespace SmartSensors.Tests.Controllers.SensorControllerTests
{
    [TestClass]
    public class SensorDropdown_Should
    {
        [TestMethod]
        public void ReturnDefaultPartialViewWithCorrectViewModel()
        {
            //Arrange
            var sensorViewModel = new SensorViewModel();
            sensorViewModel.UrlCollection = new List<SelectListItem>()
            {
                new SelectListItem() {Value="FirstDefaultValue",Text="FirstDefaultText"},
                new SelectListItem() {Value="SecondDefaultValue",Text="SecondDefaultText"}
            };
            var sensorServiceMock = new Mock<ISensorService>();
            var dbContextMock = new Mock<ApplicationDbContext>();
            var urlProviderMock = new Mock<IUrlProvider>();
            var valueTypeProviderMock = new Mock<IValueTypeProvider>();

            var requestController = new SensorController(dbContextMock.Object, urlProviderMock.Object, valueTypeProviderMock.Object, sensorServiceMock.Object);

            //Act & Assert

            requestController
                .WithCallTo(x => x.SensorsDropDown(sensorViewModel))
                .ShouldRenderDefaultPartialView()
                .WithModel<SensorViewModel>();

        }
    }
}
