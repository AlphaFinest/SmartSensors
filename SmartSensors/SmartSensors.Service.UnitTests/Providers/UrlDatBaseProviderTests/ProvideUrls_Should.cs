using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartSensors.Data;
using SmartSensors.Data.Models;
using SmartSensors.Service.UnitTests.MockHelpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSensors.Service.UnitTests.Providers.UrlDatBaseProviderTests
{
    [TestClass]
    public class ProvideUrls_Should
    {
        [TestMethod]
        public void SaveUrlsToDataBaseFromAPI()
        {
            var dbContexMock = new Mock<ApplicationDbContext>();
            var urlsDbSetMock = new Mock<DbSet<Url>>();

            var urlLists = new List<Url>()
            {
                new Url() {SensorType="DefaultType",SensorUrl="DefaultUrl",Description="DefaultDescription",Id=2,PollingInterval=20,ValueType="DefaultValueType"},
                new Url() {SensorType="DefaultType",SensorUrl="DefaultUrl",Description="DefaultDescription",Id=1,PollingInterval=20,ValueType="DefaultValueType"}
            };

            urlsDbSetMock.SetupData(urlLists);
            dbContexMock.Setup(x => x.Urls).Returns(urlsDbSetMock.Object);

            var urlDataBaseProvider = new UrlDataBaseProviderMock(dbContexMock.Object);

            //Act
            urlDataBaseProvider.ProvideUrls();

            //Assert
            dbContexMock.Verify(x => x.SaveChanges(), Times.Once);



        }
    }
}
