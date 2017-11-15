using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartSensors.Data.Models;
using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Text;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Moq;
using SmartSensors.Data;
using SmartSensors.Service.ViewModels;
using System.Data.Entity;

namespace SmartSensors.Service.UnitTests.UserServiceTests
{
    [TestClass]
    public class ServiceEditUserByUsername_Should
    {
        [TestMethod]
        public void ReturnCorrectModel_WhenParrametersAreCorrect()
        {
            //Arrange
            var inputUsername = "DefaultUsername";
            var dbContextMock = new Mock<ApplicationDbContext>();
            var mockUserStore = new Mock<IUserStore<User>>();
            var userManagerMock = new Mock<UserManager<User>>(mockUserStore.Object);
            var userDbSetMock = new Mock<DbSet<User>>();

            userManagerMock.Setup(x => x.FindByNameAsync(inputUsername)).ReturnsAsync(new User() { UserName = "DefaultUsername", Email="example@example.com" });


            var userService = new UserService(dbContextMock.Object,userManagerMock.Object);

            var expectedViewModel = new UserViewModel()
            {
                Username = "DefaultUsername"
            };


            //Act
            var result = userService.ServiceEditUser(inputUsername);

            //Assert
            Assert.AreEqual(expectedViewModel.Username, result.Username);



        }
    }
}
