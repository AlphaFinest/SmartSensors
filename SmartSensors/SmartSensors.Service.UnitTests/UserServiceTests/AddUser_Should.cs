using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartSensors.Data;
using SmartSensors.Data.Models;
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
    public class AddUser_Should
    {
        [TestMethod]
        public void AddUser_WhenParametersAreCorrect()
        {
            var dbContextMock = new Mock<ApplicationDbContext>();
            var mockUserStore = new Mock<IUserStore<User>>();
            var userManagerMock = new Mock<UserManager<User>>(mockUserStore.Object);
            var userDbSetMock = new Mock<DbSet<User>>();

            var usersList = new List<User>()
            {
                new User() {Id="1",UserName="DefaultUsername"}
            }.AsQueryable();


            var viewModel = new AddUserViewModel()
            {
                Username = "DefaultUsername",
                Email = "random@random.com",
                Password="Random123!"
            };

            userManagerMock.Setup(x => x.Users).Returns(usersList);



            var userService = new UserService(dbContextMock.Object, userManagerMock.Object);



            //Act
               userService.AddUser(viewModel);

            //Act & Assert
            Assert.AreEqual(userManagerMock.Object.Users.Count(),1);
        }
    }
}
