using Microsoft.AspNet.Identity.EntityFramework;
using SmartSensors.Data.Models;
using SmartSensors.Data.Models.Sensors;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSensors.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().
                HasMany(u => u.SharedSensors)
                .WithMany(s => s.Users)
                .Map(us =>
                {
                    us.MapLeftKey("UserRefId");
                    us.MapRightKey("SensorRefId");
                    us.ToTable("UserSensor");
                });

            modelBuilder.Entity<Sensor>()
                .HasMany(s => s.History)
                .WithRequired(h => h.Sensor);
        }

        public DbSet<Sensor> Sensors { get; set; }

        public DbSet<History> History { get; set; }
    }
}
