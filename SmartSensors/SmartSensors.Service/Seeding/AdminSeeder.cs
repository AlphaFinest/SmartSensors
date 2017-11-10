using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SmartSensors.Data;
using SmartSensors.Data.Models;
using SmartSensors.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSensors.Service.Seeding
{
    public class AdminSeeder : ISeeder
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<User> userManager;

        public AdminSeeder(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.userManager = new UserManager<User>(new UserStore<User>(this.dbContext));
        }

        public void Seed()
        {
            if (!this.dbContext.Users.Any(u => u.UserName == "Random"))
            {
                var admin = new User
                {
                    UserName = "Random",
                    Email = "random@random.com"
                };

                var password = "Random123!";

                this.userManager.Create(admin, password);
                this.userManager.AddToRoles(admin.Id, new[] { "Admin" });
            }
        }
    }
}
