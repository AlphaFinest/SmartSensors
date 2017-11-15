using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartSensors.Data;
using SmartSensors.Data.Models;
using SmartSensors.Service.Providers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSensors.Service.UnitTests.Providers.UserSharingProviderTests
{
    [TestClass]
    public class GetSubscribers_Should
    {
        [TestMethod]
        public void ReturnListOfUsersFromDataBase_WhenParametersAreCorrect()
        {
            //Arrange 
            var input = "FirstUser,SecondUser,ThirdUser";

            var users = new List<User>()
            {
                new User() {UserName="FirstUser"},
                new User() {UserName="SecondUser"},
                new User() {UserName="ThirdUser"}
            };

            var dbContextMock = new Mock<ApplicationDbContext>();

            var userDbSetMock = new Mock<DbSet<User>>().SetupData(users);

            dbContextMock.Setup(x => x.Users).Returns(userDbSetMock.Object);

            var userSharingProvider = new UserSharingProvider(dbContextMock.Object);

            //Act
            var result =  userSharingProvider.GetSubscribers(input);

            //Assert
            Assert.AreEqual(result.Count, 3);
        }

        [TestMethod]
        public void ThrowException_WhenParametersAreIncorrect()
        {
            //Arrange 
            string input = "Pesho";

            var users = new List<User>()
            {
                new User() {UserName="FirstUser"},
                new User() {UserName="SecondUser"},
                new User() {UserName="ThirdUser"},
                new User() {UserName="FourthUser"}
            };

            var dbContextMock = new Mock<ApplicationDbContext>();

            var userDbSetMock = new Mock<DbSet<User>>().SetupData(users);

            dbContextMock.Setup(x => x.Users).Returns(userDbSetMock.Object);

            var userSharingProvider = new UserSharingProvider(dbContextMock.Object);

            var expectedCount = 2;
            


            //Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() =>  userSharingProvider.GetSubscribers(input));
        }



    }
}
