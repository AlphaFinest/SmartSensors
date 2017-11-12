﻿using System;
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
            var valueTypeProviderMock = new Mock<IValueTypeProvider>();
            
            var sensors = new List<Sensor>()
            {
                new Sensor(){ Name = "Sensor1" },
                new Sensor(){ Name = "Sensor2" }
            };
                        
            var controller = new SensorController(dbContextMock.Object, urlProviderMock.Object, valueTypeProviderMock.Object, sensorServiceMock.Object);
            
            var resultViewModel = sensors.AsQueryable().Select(PublicViewModel.Create).ToList();

            //Act & Assert
            controller
                .WithCallTo(c => c.MySensors())
                .ShouldRenderDefaultView()
                .WithModel<List<PublicViewModel>>(viewModel =>
                {
                    for(int i = 0; i < viewModel.Count; i++)
                    {
                        Assert.AreEqual(viewModel[i].SensorName, resultViewModel[i].SensorName);
                    }
                });
        }
    }
}