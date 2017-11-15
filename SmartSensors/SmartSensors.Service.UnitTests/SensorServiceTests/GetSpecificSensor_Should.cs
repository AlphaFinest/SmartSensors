using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartSensors.Data;
using SmartSensors.Data.Models;
using SmartSensors.Data.Models.Sensors;
using SmartSensors.Service.Contracts;
using SmartSensors.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSensors.Service.UnitTests.SensorServiceTests
{
    [TestClass]
    public class GetSpecificSensor_Should
    {
        [TestMethod]
        public void ReturnCorrectSensor_WhenParametersAreCorrect()
        {
            //Arrange
            int input = 1;
            var dbContextMock = new Mock<ApplicationDbContext>();
            var sensorDbSetMock = new Mock<DbSet<Sensor>>();
            var sensorServiceMock = new Mock<ISensorService>();
            var valueTypeProviderMock = new Mock<ISensorValueProvider>();
            var userSharingProviderMock = new Mock<IUserSharingProvider>();

            var user = new User()
            {
                UserName = "DefaultUser"
            };

            var sensor = new Sensor()
            {
                Id = 1,
                Name = "DefaultName",
                Owner = user,
                Description = "DefaultDescription",
                Url = "DefaultUrl",
                PollingInterval = 12,
                IsPublic = true,
                MinRange = 1,
                MaxRange = 2
            };

            var sensorList = new List<Sensor>();
            sensorList.Add(sensor);

            sensorServiceMock.Setup(x => x.GetSharedWithLikeString(sensor)).Returns(String.Join(", ", sensor.Users.ToList()));


            sensorDbSetMock.SetupData(sensorList);
            sensorDbSetMock.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => sensorList.FirstOrDefault(d => d.Id == (int)ids[0]));
            dbContextMock.SetupGet(x => x.Sensors).Returns(sensorDbSetMock.Object);

            var sensorService = new SensorService(dbContextMock.Object, valueTypeProviderMock.Object, userSharingProviderMock.Object);

            var expectedResult = new SensorViewModel()
            {
                Id = 1,
                Name = "DefaultName",
                Owner = "DefaultUser",
                Description = "DefaultDescription",
                Url = "DefaultUrl",
                PollingInterval = 12,
                IsPublic = true,
                MinRange = 1,
                MaxRange = 2,
                SharedWith =  sensorService.GetSharedWithLikeString(sensor)
            };


            //Act
            var result = sensorService.GetSpecificSensor(input);

            //Assert
            Assert.AreEqual(expectedResult.Description, result.Description);
            Assert.AreEqual(expectedResult.Name, result.Name);
            Assert.AreEqual(expectedResult.Owner, result.Owner);
            Assert.AreEqual(expectedResult.PollingInterval, result.PollingInterval);
            Assert.AreEqual(expectedResult.SharedWith, result.SharedWith);
            Assert.AreEqual(expectedResult.MinRange, result.MinRange);
            Assert.AreEqual(expectedResult.MaxRange, result.MaxRange);
            Assert.AreEqual(expectedResult.IsPublic, result.IsPublic);
            Assert.AreEqual(expectedResult.Id, result.Id);


        }
    }
}


//public SensorViewModel GetSpecificSensor(int id)
//{
//    var model = this.dbContext.Sensors.Find(id);

//    var viewModel = new SensorViewModel()
//    {
//        Id = model.Id,
//        Owner = model.Owner.UserName,
//        Name = model.Name,
//        Description = model.Description,
//        Url = model.Url,
//        PollingInterval = model.PollingInterval,
//        IsPublic = model.IsPublic,
//        MinRange = model.MinRange,
//        MaxRange = model.MaxRange,
//        SharedWith = GetSharedWithLikeString(model)

//    };
//    return viewModel;
//}