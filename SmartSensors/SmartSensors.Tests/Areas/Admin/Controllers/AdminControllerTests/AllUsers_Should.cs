using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartSensors.Data.Models;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using System.Linq;
using SmartSensors.Areas.Admin.Controllers;
using TestStack.FluentMVCTesting;
using SmartSensors.Service.ViewModels;
using SmartSensors.Service.Contracts;
using SmartSensors.Data;

namespace SmartSensors.Tests.Areas.Admin.Controllers.AdminControllerTests
{
    [TestClass]
    public class AllUsers_Should
    {
        [TestMethod]
        public void ReturnDefaultViewWithCorrectViewModel()
        {
            //Arange
            var sensorServiceMock = new Mock<ISensorService>();
            var userServiceMock = new Mock<IUserService>();
            var urlProviderMock = new Mock<IUrlProvider>();

            var users = new List<User>()
            {
                new User(){ UserName = "firstUser"},
                new User(){ UserName = "secondUser"},
            }.AsQueryable();

            var resultViewModel = users.Select(UserViewModel.Create).ToList();

            userServiceMock.Setup(x => x.GetAllUsers()).Returns(new List<UserViewModel>());
            AdminController controller = new AdminController(userServiceMock.Object, sensorServiceMock.Object, urlProviderMock.Object);

            //Act & Assert
            controller
                .WithCallTo(c => c.AllUsers())
                .ShouldRenderDefaultView()
                .WithModel<List<UserViewModel>>(viewModel =>
                {
                    for (int i = 0; i < viewModel.Count; i++)
                    {
                        Assert.AreEqual(resultViewModel[i].Username, viewModel[i].Username);
                    }
                });
        }
    }
}
