//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using SmartSensors.Data;
//using SmartSensors.Data.Models.Sensors;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Threading.Tasks;
//using SmartSensors.Service;
//using SmartSensors.Service.Contracts;
//using System.Net.Http;
//using RichardSzalay.MockHttp;
//using SmartSensors.Service.ViewModels;
//using EntityFramework.Testing;
//using System.Data.Entity.Infrastructure;

//namespace SmartSensors.Service.UnitTests.SensorServiceTests
//{
//    [TestClass]
//    public class UpdateSensors_Should
//    {
//        [TestMethod]
//        public async Task UpdateAllRegisteredSensors_WhenActionMethodIsCalledAsync()
//        {
//            //Assert
//            var dbContextMock = new Mock<ApplicationDbContext>();
//            var valueProviderMock = new Mock<ISensorValueProvider>();
//            var userSharingProviderMock = new Mock<IUserSharingProvider>();
//            var sensorsDbSetMock = new Mock<DbSet<Sensor>>();

//            var sensorList = new List<Sensor>()
//            {
//                new Sensor(){Value="10",LastUpdated=DateTime.Now,ValueType="°C"}
//            };
//            sensorsDbSetMock.SetupData(sensorList).As<DbSqlQuery<Sensor>>();
//            sensorsDbSetMock.Setup(x=>x.SqlQuery("SELECT * FROM Sensors s WHERE GETDATE() > DATEADD(ss, s.PollingInterval, S.LastUpdated)")).Returns(sensorsDbSetMock.Object)



//            var mockHttp = new MockHttpMessageHandler();

//            mockHttp.When("http://telerikacademy.icb.bg/api/sensor/*")
//                    .Respond("application/json", "{'timeStamp':'2017-11-06T13:28:30.6645215+02:00','value':'14.7','valueType':'°C'}");
//            //.Respond("application/json", "{'timeStamp':'2017-11-06T13:29:50.5554315+02:00','value':'15','valueType':'%'}");

//            var jsonResponseList = new List<JsonSensorViewModel>()
//            {
//                new JsonSensorViewModel() {TimeStamp=DateTime.Parse("2017-11-06T13:28:30.6645215+02:00"), Value="14.7",ValueType="°C" }
//                //new JsonSensorViewModel() {TimeStamp=DateTime.Parse("2017-11-06T13:29:50.5554315+02:00"), Value="15",ValueType="%" }
//            };

//            //Act
//            var client = new HttpClient(mockHttp);
//            //var response = client.GetAsync("http://telerikacademy.icb.bg/api/sensor/f1796a28-642e-401f-8129-fd7465417061").Result;
//            //var json = await response.Content.ReadAsStringAsync();

//            var sensorService = new SensorService(dbContextMock.Object, valueProviderMock.Object, userSharingProviderMock.Object);
//            await sensorService.UpdateSensors();

//            //Assert



//        }
//    }
//}
