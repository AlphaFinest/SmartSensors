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

namespace SmartSensors.Service.UnitTests.UserServiceTests
{
    [TestClass]
    public class ServiceEditUserByUsername_Should
    {
        [TestMethod]
        public async Task ReturnCorrectModel_WhenParrametersAreCorrect()
        {
        //    //Arrange
        //    var inputUsername = "DefaultUsername";
        //    var dbContextMock = new Mock<ApplicationDbContext>();
        //    var userManagerMock = new Mock<UserManager<User>>();

        //    userManagerMock.Setup(x => x.Users).Returns(
        //        new List<User>()
        //        {
        //            new User() {UserName="DefaultUser"}
        //}.AsQueryable());

        //    userManagerMock.Setup(x => x.FindByNameAsync(inputUsername)).Returns(Task.FromResult(new User() { UserName = " DefaultUsername" }));

        //    var userService = new UserService(dbContextMock.Object);

        //    var expectedViewModel = new UserViewModel()
        //    {
        //        Username = "DefaultUsername"
        //    };


        //    //Act
        //    var result = await userService.ServiceEditUser(inputUsername);


        //    //Assert
        //    Assert.AreEqual(expectedViewModel.Username, result.Username);



        }
    }
}
