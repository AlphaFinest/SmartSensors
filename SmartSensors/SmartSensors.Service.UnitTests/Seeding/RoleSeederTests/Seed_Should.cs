using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartSensors.Data;
using Moq;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;
using SmartSensors.Service.Seeding;
using System.Linq;

namespace SmartSensors.Service.UnitTests.Seeding.RoleSeederTests
{
    [TestClass]
    public class Seed_Should
    {
        [TestMethod]
        public void SeedDatabaseWithRole_WhenRolesTablesDoesNotHaveAdmin()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();

            var roles = new List<IdentityRole>();

            var rolesDbSet = new Mock<DbSet<IdentityRole>>().SetupData(roles);
            dbContextMock.Setup(d => d.Roles).Returns(rolesDbSet.Object);

            var seeder = new RoleSeeder(dbContextMock.Object);

            //Act
            seeder.Seed();

            //Assert
            dbContextMock.Verify(d => d.SaveChanges(), Times.Once);
            Assert.AreEqual(dbContextMock.Object.Roles.SingleOrDefault().Name, "Admin");

        }
    }
}
