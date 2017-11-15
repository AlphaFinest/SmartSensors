using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartSensors.Areas.Admin.Controllers;
using Moq;
using SmartSensors.Data;
using SmartSensors.Service.Contracts;
using SmartSensors.Service.ViewModels;
using TestStack.FluentMVCTesting;
using Microsoft.AspNet.Identity;
using SmartSensors.Data.Models;
using SmartSensors.Tests.Helpers;
using System.Collections.Generic;

namespace SmartSensors.Tests.Areas.Admin.Controllers.AdminControllerTests
{
    [TestClass]
    public class EditUser_Should
    {
        [TestMethod]
        public void ReturnTheCorrectViewModel_WhenTheCorrectUsernameIsPassed()
        {
            //Arrange
            var userServiceMock = new Mock<IUserService>();
            var sensorServiceMock = new Mock<ISensorService>();
            var urlProviderMock = new Mock<IUrlProvider>();

            var controller = new AdminController(userServiceMock.Object, sensorServiceMock.Object, urlProviderMock.Object);
            var username = "aasd";
            var userViewModel = new UserViewModel()
            {
                Id = "id",
                Username = username,
                Email = "kekeke",
                IsAdmin = true
            };

            userServiceMock.Setup(u => u.ServiceEditUser(username)).Returns(userViewModel);

            //Act & Assert
            controller
                .WithCallTo(c => c.EditUser(username))
                .ShouldRenderDefaultView()
                .WithModel<UserViewModel>(m => m.Email == userViewModel.Email);
        }

        [TestMethod]
        public void RedirectToAllUsers_WhenTheCorrectViewModelIsPassed()
        {
            //Arrange
            var userServiceMock = new Mock<IUserService>();
            var sensorServiceMock = new Mock<ISensorService>();
            var urlProviderMock = new Mock<IUrlProvider>();
            
            var username = "aasd";

            var users = new List<User>()
            {
                new User() { UserName="FirstUser"}
            };

            var userViewModel = new UserViewModel()
            {
                Id = "id",
                Username = username,
                Email = "kekeke",
                IsAdmin = true
            };

            userServiceMock.Setup(u => u.ServiceEditUser(username)).Returns(userViewModel);

            var controller = new AdminController(userServiceMock.Object, sensorServiceMock.Object, urlProviderMock.Object);

            controller.UserMocking(users[0].UserName);
            
            //Act & Assert
            controller
                .WithCallTo(x => x.EditUser(userViewModel))
                .ShouldRedirectTo<AdminController>(c => c.AllUsers());

            userServiceMock.Verify(s => s.ServiceEditUser(userViewModel), Times.Once);
        }
    }
}
