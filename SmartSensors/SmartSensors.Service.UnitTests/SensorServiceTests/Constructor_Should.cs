using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartSensors.Data;
using SmartSensors.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSensors.Service.UnitTests.SensorServiceTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void CreateInstance_WhenParametersAreCorrect()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var sensorValueProviderMock = new Mock<ISensorValueProvider>();
            var userSharingProviderMock = new Mock<IUserSharingProvider>();

            //Act
            var service = new SensorService(dbContextMock.Object,sensorValueProviderMock.Object,userSharingProviderMock.Object);

            //Assert
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void ThrowException_WhenContextIsNull()
        {
            //AAA
            Assert.ThrowsException<ArgumentNullException>(() => new SensorService(null,null,null));
        }
    }
}
