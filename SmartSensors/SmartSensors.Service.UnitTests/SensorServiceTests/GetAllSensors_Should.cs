using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartSensors.Data;
using SmartSensors.Data.Models;
using SmartSensors.Data.Models.Sensors;
using SmartSensors.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSensors.Service.UnitTests.SensorServiceTests
{
    [TestClass]
    public class GetAllSensors_Should
    {
        [TestMethod]
        public void ReturnAllSensorFromDataBaseAsViewModel()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var valueTypeProviderMock = new Mock<ISensorValueProvider>();
            var userSharingProviderMock = new Mock<IUserSharingProvider>();

            var sensorDbSetMock = new Mock<DbSet<Sensor>>();

            var user = new User() { UserName = "DefaultUser" };

            var usersList = new List<User>();
            usersList.Add(user);

            var sensorsList = new List<Sensor>()
            {
                new Sensor ()
                {
                    Id =1,Owner=user,Description="DefaultDescrition",Url="DefaultUrl",PollingInterval=10,Users=usersList,
                    IsPublic =true,Value="DefaultValue",ValueType="DefaultValueType",MinRange=1,MaxRange=2,Name="FirtSensor"
                },
                new Sensor ()
                {
                    Id =2,Owner=user,Description="DefaultDescrition",Url="DefaultUrl",PollingInterval=10,Users=usersList,
                    IsPublic =true,Value="DefaultValue",ValueType="DefaultValueType",MinRange=1,MaxRange=2,Name="FirtSensor"
                },
                new Sensor ()
                {
                     Id =3,Owner=user,Description="DefaultDescrition",Url="DefaultUrl",PollingInterval=10,Users=usersList,
                    IsPublic =true,Value="DefaultValue",ValueType="DefaultValueType",MinRange=1,MaxRange=2,Name="FirtSensor"
                },
            };

            sensorDbSetMock.SetupData(sensorsList);
            dbContextMock.Setup(x => x.Sensors).Returns(sensorDbSetMock.Object);

            var sensorService = new SensorService(dbContextMock.Object, valueTypeProviderMock.Object, userSharingProviderMock.Object);

            //Act
            var result = sensorService.GetAllSensors();

            //Assert
            Assert.AreEqual(result.Count, sensorDbSetMock.Object.Count());


        }

        [TestMethod]
        public void ThrowException_WhenDataBaseModelsAreIncomplete()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var valueTypeProviderMock = new Mock<ISensorValueProvider>();
            var userSharingProviderMock = new Mock<IUserSharingProvider>();

            var sensorDbSetMock = new Mock<DbSet<Sensor>>();

            var user = new User() { UserName = "DefaultUser" };

            var usersList = new List<User>();
            usersList.Add(user);


            var sensorsList = new List<Sensor>()
            {
                new Sensor ()
                {
                    Id =1,Owner=user,Description="DefaultDescrition",Url="DefaultUrl",PollingInterval=10,Users=usersList,
                    IsPublic =true,Value="DefaultValue"
                },
                new Sensor ()
                {
                    Id =2,IsPublic =true,Value="DefaultValue",ValueType="DefaultValueType",MinRange=1,MaxRange=2,Name="FirtSensor"
                },
                new Sensor ()
                {
                     Id =3,Owner=user,Description="DefaultDescrition",Url="DefaultUrl",PollingInterval=10,Users=usersList,
                    IsPublic =true,Value="DefaultValue",ValueType="DefaultValueType",MinRange=1,MaxRange=2,Name="FirtSensor"
                },
            };


            sensorDbSetMock.SetupData(sensorsList);
            dbContextMock.Setup(x => x.Sensors).Returns(sensorDbSetMock.Object);

            var sensorService = new SensorService(dbContextMock.Object, valueTypeProviderMock.Object, userSharingProviderMock.Object);


            //Act & Assert
            Assert.ThrowsException<NullReferenceException>(() => sensorService.GetAllSensors());


        }
    }
}



