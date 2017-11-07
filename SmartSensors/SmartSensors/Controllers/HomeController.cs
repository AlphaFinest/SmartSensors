using SmartSensors.Models;
using SmartSensors.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using SmartSensors.Data.Models.Sensors;
using System;

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
            return View();
        }

        [Authorize]
        public async Task<ActionResult> About()
        {
            ViewBag.message = "your application description page.";

            return View();
        }

        public ActionResult RegisterSensor()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterSensor(Sensor model)
        {
            var user = new Sensor
            {
                Name = model.Name,
                Description = model.Description,
                Url = model.Url,
                PollingInterval = model.PollingInterval,
                ValueType = model.ValueType,
                IsPublic = model.IsPublic,
                MinRange = model.MinRange,
                MaxRange = model.MaxRange,
                LastUpdated = System.DateTime.Now,
                Owner = dbContext.Users.Find(this.User.Identity.Name)
                
            };

            dbContext.Sensors.Add(user);
            dbContext.SaveChanges();
            if (true)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}