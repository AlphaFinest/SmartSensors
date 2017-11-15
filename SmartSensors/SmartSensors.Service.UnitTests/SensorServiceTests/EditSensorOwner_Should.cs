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
    public class EditSensorOwner_Should
    {
        [TestMethod]
        public void SaveChangesToDataBase_WhenParametersAreCorrect()
        {
            //Arrange

            var dbContextMock = new Mock<ApplicationDbContext>();
            var urlDbSetMock = new Mock<DbSet<Url>>();
            var sensorDbSetMock = new Mock<DbSet<Sensor>>();
            var valueTypeProviderMock = new Mock<ISensorValueProvider>();
            var sensorServiceMock = new Mock<ISensorService>();
            var userSharingProviderMock = new Mock<IUserSharingProvider>();

            var jsonSensorViewModel = new JsonSensorViewModel() { Value = "DefaultValue" };
            var urlList = new List<Url>()
            {
                new Url() {SensorUrl="DefaultUrl"}
            };

            var sensor = new Sensor()
            {
                Id=1,
                Name="DefaultName",
                Url="NotDefaultUrl",
                Description= "DefaultDescription",
                IsPublic = true,
                MinRange = 1,
                MaxRange = 2,
                Users = new List<User>()
                {
                    new User() { UserName = "DefaultUser" },
                    new User() { UserName = "SecondDefaultUser" }
                }
            };

            var input = new SensorViewModel()
            {
                Id=1,
                Name = "DefaultName",
                Description = "DefaultDescription",
                Url = "DefaultUrl",
                IsPublic = true,
                MinRange = 1,
                MaxRange = 2,
                SharedWith = "DefaultUser"
            };

            var sensorList = new List<Sensor>();
            sensorList.Add(sensor);


            urlDbSetMock.SetupData(urlList);
            dbContextMock.SetupGet(x => x.Urls).Returns(urlDbSetMock.Object);

            sensorDbSetMock.SetupData(sensorList);
            sensorDbSetMock.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => sensorList.FirstOrDefault(d => d.Id == (int)ids[0]));
            dbContextMock.SetupGet(x => x.Sensors).Returns(sensorDbSetMock.Object);



            valueTypeProviderMock.Setup(x => x.GetValue(urlList[0].SensorUrl)).ReturnsAsync(jsonSensorViewModel.Value);

            sensorServiceMock.Setup(x => x.GetSharedWithLikeString(sensor)).Returns(String.Join(", ", sensor.Users));

            userSharingProviderMock.Setup(x => x.GetSubscribers(input.SharedWith)).Returns(sensor.Users.ToList());

            var sensorService = new SensorService(dbContextMock.Object, valueTypeProviderMock.Object, userSharingProviderMock.Object);


            //Act
             sensorService.EditSensorOwner(input);

            //Assert
            dbContextMock.Verify(x => x.SaveChanges(), Times.Once());
            Assert.AreEqual(input.Description, dbContextMock.Object.Sensors.Find(input.Id).Description);
            





        }

    }
}


