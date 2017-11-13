using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SmartSensors.Tests.Helpers
{
    public static class ControllerExtension
    {
        public static void UserMocking(this Controller myController, string username)
        {
            var userMock = new Mock<IPrincipal>();
            userMock.SetupGet(x => x.Identity.Name).Returns(username);

            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(x => x.User).Returns(userMock.Object);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(x => x.HttpContext)
                                 .Returns(contextMock.Object);

            myController.ControllerContext = controllerContextMock.Object;
        }
    }
}
