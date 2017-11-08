using Bytes2you.Validation;
using SmartSensors.Data;
using SmartSensors.Data.Models.Sensors;
using SmartSensors.Models;
using System.Linq;
using System.Web.Mvc;



namespace SmartSensors.Controllers
{
    public class SensorController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public SensorController(ApplicationDbContext dbContext)
        {
            Guard.WhenArgument(dbContext, "dbContext").IsNull().Throw();
            this.dbContext = dbContext;
            
        }

        public ActionResult RegisterSensor()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult RegisterSensor(SensorViewModel model)
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

        public ActionResult PublicSensors()
        {
            var publicViewModel = this.dbContext.Sensors.Where(s => s.IsPublic.Equals(true))
              .Select(s => new PublicViewModels()
              {
                  Owner = s.Owner,
                  SensorName = s.Name,
                  Value = s.Value,
                  ValueType = s.ValueType
              })
              .ToList();

            return this.View(publicViewModel);

        }

    }
}