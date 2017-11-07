using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using EntityFramework.Testing;
using SmartSensors.Data.Models;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using System.Linq;
using SmartSensors.Areas.Admin.Controllers;
using SmartSensors.Areas.Admin.Models;
using TestStack.FluentMVCTesting;

namespace SmartSensors.Tests.Areas.Admin.Controllers.AdminControllerTests
{
    [TestClass]
    public class AllUsers_Should
    {
        [TestMethod]
        public void ReturnDefaultViewWithCorrectViewModel()
        {
            //Arange
            var storeMock = new Mock<IUserStore<User>>();
            var userManagerMock = new Mock<ApplicationUserManager>(storeMock.Object);
            var users = new List<User>()
            {
                new User(){ UserName = "firstUser"},
                new User(){ UserName = "secondUser"},
            }.AsQueryable();

            userManagerMock.Setup(m => m.Users).Returns(users);

            var resultViewModel = users.Select(UserViewModel.Create).ToList();

            AdminController controller = new AdminController(userManagerMock.Object);

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
