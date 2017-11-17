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
using Microsoft.AspNet.Identity;
using SmartSensors.Data.Models;

namespace SmartSensors.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<User> userManager;
        private readonly IUrlDataBaseProvider urlDataBaseProvider;

        public HomeController(ApplicationDbContext dbContext, UserManager<User> userManager, IUrlDataBaseProvider urlDataBaseProvider)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.urlDataBaseProvider = urlDataBaseProvider;
        }

        public ActionResult Index()
        {

            ISeeder roleSeed = new RoleSeeder(this.dbContext);
            roleSeed.Seed();

            ISeeder adminSeed = new AdminSeeder(this.dbContext, this.userManager);
            adminSeed.Seed();

            ISeeder urlSeed = new UrlsSeeder(this.dbContext, this.urlDataBaseProvider);
            urlSeed.Seed();

            return this.View();
        }
    }
}