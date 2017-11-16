using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartSensors.Service.UnitTests.MockHelpers;
using SmartSensors.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSensors.Service.UnitTests.Providers.SensorValueProviderTests
{
    [TestClass]
    public class GetValue_Should
    {
        [TestMethod]
        public async Task ReturnValueOfSensor_WhenParametersAreCorrect()
        {
            //Arrange
            var viewModel = new JsonSensorViewModel() { Value = "20" };

            var inputUrl = "DefaultUrl";


            var sensorValueProvider = new SensorValueProviderMock(viewModel);

            var expectedResult = "20";
            //Act
            var result = await sensorValueProvider.GetValue(inputUrl);

            //Assert
            Assert.AreEqual(result, expectedResult);




        }
    }
}
