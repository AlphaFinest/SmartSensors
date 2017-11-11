//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using SmartSensors.Data;
//using SmartSensors.Data.Models.Sensors;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using EntityFramework.Testing;
//using SmartSensors.Service.Contracts;

//namespace SmartSensors.Service.UnitTests.SensorServiceTests
//{
//    [TestClass]
//   public class RegisterNewSensor_Should
//    {
//        [TestMethod]
//       public void AddSensorToDbContextSensorTable()
//        {
//            //Arrange
//            var dbContextMock = new Mock<ApplicationDbContext>();
//            var sensor = new Sensor()
//            {
//                Name = "DefaultName",
//                Description = "DefaultDescription"
//            };

//            var listOfSensors = new List<Sensor>();

//            var dbSetMock = new Mock<DbSet<Sensor>>();
//            dbSetMock.SetupData(listOfSensors);

//            dbContextMock.Setup(x => x.Sensors).Returns(dbSetMock.Object);

//            var registerSensor = new SensorService(dbContextMock.Object);

//            //Act
//            registerSensor.RegisterNewSensor(sensor);

//            //Assert
//            Assert.AreEqual(dbContextMock.Object.Sensors, 1);


//        }

//    }
//}
