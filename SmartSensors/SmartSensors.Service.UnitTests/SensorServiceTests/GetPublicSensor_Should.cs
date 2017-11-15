using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSensors.Service.UnitTests.SensorServiceTests
{
    [TestClass]
    public class GetPublicSensor_Should
    {
        [TestMethod]
        public void ReturnCorrectViewModel_WhenParametersAreCorrect()
        {

        }
    }
}


//public List<PublicViewModel> GetPublicSensor()
//{
//    var publicViewModel = this.dbContext.Sensors.Where(s => s.IsPublic)
//      .Select(s => new PublicViewModel()
//      {
//          OwnerName = s.Owner.UserName,
//          SensorName = s.Name,
//          Value = s.Value,
//          ValueType = s.ValueType,
//          Url = s.Url
//      })
//      .ToList();

//    return publicViewModel;
//}