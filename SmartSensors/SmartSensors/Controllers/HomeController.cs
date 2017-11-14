using SmartSensors.Models;
using SmartSensors.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using SmartSensors.Data.Models.Sensors;
using System;
using SmartSensors.Service.Seeding;
using SmartSensors.Service.Contracts;

namespace SmartSensors.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public HomeController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ActionResult Index()
        {

            ISeeder roleSeed = new RoleSeeder(this.dbContext);
            roleSeed.Seed();

            ISeeder adminSeed = new AdminSeeder(this.dbContext);
            adminSeed.Seed();

            //ISeeder urlSeed = new UrlsSeeder(this.dbContext);
            //urlSeed.Seed();

            return this.View();
        }
    }
}