using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartSensors.Service.UnitTests.MockHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SmartSensors.Service.UnitTests.Providers.UrlProviderTests
{
    [TestClass]
    public class GetUrlPattern_Should
    {
        [TestMethod]
        public async Task GetListOfAllUrlsFromAPI()
        {
            //Arrange
            var urlProviderMock = new UrlProviderMock();
            var listOfSelectItems = new List<SelectListItem>();

            var currentCountOfSensors = 13;

            //Act
            var result =await urlProviderMock.GetUrlPattern();

            //Assert
           // Assert.AreEqual(result.Count,currentCountOfSensors);
        }
    }
}
