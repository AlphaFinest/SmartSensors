using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartSensors.Data;
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

            //Act
            var service = new SensorService(dbContextMock.Object);

            //Assert
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void ThrowException_WhenContextIsNull()
        {
            //AAA
            Assert.ThrowsException<ArgumentNullException>(() => new SensorService(null));
        }
    }
}
