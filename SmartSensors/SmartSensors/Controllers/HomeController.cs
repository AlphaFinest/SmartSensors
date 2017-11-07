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
            var user = dbContext.Users.First();

            Sensor sensor = new Sensor();
            sensor.Name = "test123";
            sensor.Url = "url123";
            sensor.PollingInterval = 10;
            sensor.MinRange = 10;
            sensor.MaxRange = 20;
            sensor.Value = "200000";
            sensor.ValueType = "valuetype";
            sensor.LastUpdated = DateTime.Now;
            sensor.Owner = user;

            user.SharedSensors.Add(sensor);

            dbContext.SaveChanges();

            return View();
        }

        [Authorize]
        public async Task<ActionResult> About()
        {
            ViewBag.message = "your application description page.";

            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Contact()
        {

            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}