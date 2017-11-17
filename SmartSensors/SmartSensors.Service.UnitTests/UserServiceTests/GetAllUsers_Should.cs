using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartSensors.Data;
using SmartSensors.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace SmartSensors.Service.UnitTests.UserServiceTests
{
    [TestClass]
    public class GetAllUsers_Should
    {
        [TestMethod]
        public void ReturnAllUsersFromDataBase()
        {
            var dbContextMock = new Mock<ApplicationDbContext>();
            var mockUserStore = new Mock<IUserStore<User>>();
            var userManagerMock = new Mock<UserManager<User>>(mockUserStore.Object);
            var userDbSetMock = new Mock<DbSet<User>>();

            var usersList = new List<User>()
            {
                new User() {Id="1",UserName="DefaultUsername",}
            };

            userDbSetMock.SetupData(usersList);
            dbContextMock.Setup(x => x.Users).Returns(userDbSetMock.Object);


            var userService = new UserService(dbContextMock.Object, userManagerMock.Object);

            //Act
            var result =userService.GetAllUsers();

            //Assert
            Assert.AreEqual(result.Count,1);
        }
    }
}
