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
    public class SensorController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public SensorController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public ActionResult RegisterSensor()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult RegisterSensor(Sensor model)
        {
            var sensor = new Sensor
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
                Owner = dbContext.Users.First(u => u.UserName == this.User.Identity.Name),
                Value = "555555"
            };

            dbContext.Sensors.Add(sensor);
            dbContext.SaveChanges();
            if (true)
            {
                return RedirectToAction("Index", "Home");
            }
            return this.View(model);
        }
    }
}