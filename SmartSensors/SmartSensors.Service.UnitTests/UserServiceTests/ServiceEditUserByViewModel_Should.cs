using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartSensors.Data;
using SmartSensors.Data.Models;
using SmartSensors.Data.Models.Sensors;
using SmartSensors.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSensors.Service.UnitTests.UserServiceTests
{
    [TestClass]
    public class ServiceEditUserByViewModel_Should
    {
        [TestMethod]
        public void UpdateUser_WhenParrametersAreCorrect()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var mockUserStore = new Mock<IUserStore<User>>();
            var userManagerMock = new Mock<UserManager<User>>(mockUserStore.Object);
            var userDbSetMock = new Mock<DbSet<User>>();
            
            var usersList = new List<User>()
            {
                new User() {Id="1",UserName="DefaultUsername"}
            }.AsQueryable();


            var viewModel = new UserViewModel()
            {
                Id = "1",
                Username = "NewUsername"
            };

            userManagerMock.Setup(x => x.Users).Returns(usersList);



            var userService = new UserService(dbContextMock.Object, userManagerMock.Object);



            //Act
            var result = userService.ServiceEditUser(viewModel);

            //Assert
            Assert.AreEqual(userManagerMock.Object.Users.First(x => x.Id == "1").UserName, viewModel.Username);




        }
    }
}
