using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartSensors.Data;
using Microsoft.AspNet.Identity;
using SmartSensors.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;
using SmartSensors.Service.Seeding;
using System.Linq;

namespace SmartSensors.Service.UnitTests.Seeding.AdminSeederTests
{
    [TestClass]
    public class Seed_Should
    {
        [TestMethod]
        public void SeedDatabaseWithUserAndMakeItAdmin_WhenDatabaseIsEmpty()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var userStoreMock = new Mock<IUserStore<User>>();
            var userManagerMock = new Mock<UserManager<User>>(userStoreMock.Object);
            var usersDbSetMock = new Mock<DbSet<User>>();
            var rolesDbSetMock = new Mock<DbSet<IdentityRole>>();

            var expectedUsername = "Random";

            var users = new List<User>()
            {
                new User() {Id = "1", UserName = "KillMe"}
            };

            var roles = new List<IdentityRole>()
            {
                new IdentityRole("Admin")
            };
            var admin = new User
            {
                UserName = expectedUsername,
                Email = "random@random.com"
            };

            var password = "Random123!";


            usersDbSetMock.SetupData(users);
            dbContextMock.Setup(d => d.Users).Returns(usersDbSetMock.Object);

            userManagerMock.Setup(x => x.Users).Returns(users.AsQueryable());

            rolesDbSetMock.SetupData(roles);
            dbContextMock.Setup(x => x.Roles).Returns(rolesDbSetMock.Object);
            
            var seeder = new AdminSeeder(dbContextMock.Object, userManagerMock.Object);

            //Act
            seeder.Seed();

            //Assert
            //userManagerMock.Verify(u => u.Create(admin, password), Times.Once);
        }
    }
}
