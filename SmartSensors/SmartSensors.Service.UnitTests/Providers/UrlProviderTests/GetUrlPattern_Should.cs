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
        public async Task TransformAllJsonViewModelsToSelectListItems()
        {
            //Arrange
            var urlProviderMock = new UrlProviderMock();
            var listOfSelectItems = new List<SelectListItem>();



          //  urlProviderMock.GetUrlPattern
            //Act
            var result =await urlProviderMock.GetUrlPattern();

            //Assert
            Assert.AreEqual(typeof(List<SelectListItem>), result.GetType());
        }
    }
}
