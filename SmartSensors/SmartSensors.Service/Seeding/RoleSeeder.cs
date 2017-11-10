using Microsoft.AspNet.Identity.EntityFramework;
using SmartSensors.Data;
using SmartSensors.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSensors.Service.Seeding
{
    public class RoleSeeder : ISeeder
    {
        private readonly ApplicationDbContext dbContext;

        public RoleSeeder(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Seed()
        {
            if (!this.dbContext.Roles.Any(r => r.Name == "Admin"))
            {
                this.dbContext.Roles.Add(new IdentityRole("Admin"));
            }

            this.dbContext.SaveChanges();
        }
    }
}
