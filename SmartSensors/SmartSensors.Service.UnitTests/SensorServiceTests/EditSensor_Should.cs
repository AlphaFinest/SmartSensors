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
    public class EditSensor_Should
    {
        [TestMethod]
        public void SaveChangesToDataBase_WhenParametersAreCorrect()
        {
            //Arrange

            var dbContextMock = new Mock<ApplicationDbContext>();
            var urlDbSetMock = new Mock<DbSet<Url>>();
            var sensorDbSetMock = new Mock<DbSet<Sensor>>();
            var userDbSetMock = new Mock<DbSet<User>>();
            var valueTypeProviderMock = new Mock<ISensorValueProvider>();
            var sensorServiceMock = new Mock<ISensorService>();
            var userSharingProviderMock = new Mock<IUserSharingProvider>();

            string username = "DefaultUser";
            int sensorId = 1;
            var urlList = new List<Url>()
            {
                new Url() {SensorUrl="DefaultUrl"}
            };

            var userList = new List<User>()
            {
                new User() {UserName=username}
            };

            var sensor = new Sensor()
            {
                Id = sensorId,
                Owner = userList[0]
            };

            var input = new SensorViewModel()
            {
                Id = sensorId,
                Name = "DefaultName",
                Owner = username,
                Description = "DefaultDescription",
                Url = "DefaultUrl",
                IsPublic = true,
                MinRange = 1,
                MaxRange = 2
            };

            var sensorList = new List<Sensor>();
            sensorList.Add(sensor);


            urlDbSetMock.SetupData(urlList);
            dbContextMock.SetupGet(x => x.Urls).Returns(urlDbSetMock.Object);

            sensorDbSetMock.SetupData(sensorList);
            sensorDbSetMock.Setup(m => m.Find(sensorId)).Returns(sensor);
            dbContextMock.SetupGet(x => x.Sensors).Returns(sensorDbSetMock.Object);

            userDbSetMock.SetupData(userList);
            dbContextMock.Setup(x => x.Users).Returns(userDbSetMock.Object);


            sensorServiceMock.Setup(x => x.GetSharedWithLikeString(sensor)).Returns(String.Join(", ", sensor.Users));

            userSharingProviderMock.Setup(x => x.GetSubscribers(input.SharedWith)).Returns(sensor.Users.ToList());

            var sensorService = new SensorService(dbContextMock.Object, valueTypeProviderMock.Object, userSharingProviderMock.Object);


            //Act
            sensorService.EditSensor(input);

            //Assert
            dbContextMock.Verify(x => x.SaveChanges(), Times.Once());
            Assert.AreEqual(input.Description, dbContextMock.Object.Sensors.Find(input.Id).Description);
            Assert.AreEqual(input.Name, dbContextMock.Object.Sensors.Find(input.Id).Name);
            Assert.AreEqual(input.IsPublic, dbContextMock.Object.Sensors.Find(input.Id).IsPublic);
            Assert.AreEqual(input.Url, dbContextMock.Object.Sensors.Find(input.Id).Url);
            Assert.AreEqual(input.MinRange, dbContextMock.Object.Sensors.Find(input.Id).MinRange);
            Assert.AreEqual(input.MaxRange, dbContextMock.Object.Sensors.Find(input.Id).MaxRange);






        }
    }
}
