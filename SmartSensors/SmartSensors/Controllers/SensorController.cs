using Bytes2you.Validation;
using SmartSensors.Data;
using SmartSensors.Data.Models.Sensors;
using SmartSensors.Models;
using System.Collections.Generic;
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

        [Authorize]
        public ActionResult RegisterSensor()
        {
            var model = new SensorViewModel();

            model.UrlCollection = new List<SelectListItem>()
            {
             new SelectListItem{Value="f1796a28-642e-401f-8129-fd7465417061", Text="Temperature sensor with sensitivity range: 15-28 °C  Polling Time:40s" },
             new SelectListItem{Value="81a2e1b1-ea5d-4356-8266-b6b42471653e", Text="Temperature sensor with sensitivity range: 6-18 °C Polling Time:30s" },
             new SelectListItem{Value="92f7dc9a-f2fe-4b60-82f5-400e42f099b4", Text="Temperature sensor with sensitivity range: 19-23 °C Polling Time:70s" },
             new SelectListItem{Value="216fc1e7-1496-4532-b9ee-29565b865ad6", Text="Humidity sensor with sensitivity range: 0-60 % Polling Time:40s" },
             new SelectListItem{Value="61ff0614-64fd-4842-9a05-0b1541d2cc61", Text="Humidity sensor with sensitivity range: 10-90 % Polling Time:50s" },
             new SelectListItem{Value="08503c1c-963f-4106-9088-82fa67d34f9", Text="Electricity consumption sensor with sensitivity range: 1000-5000 W Polling Time:70s" },
             new SelectListItem{Value="1f0ef0ff-396b-40cb-ac3d-749196dee187", Text="Electricity consumption sensor with sensitivity range: 500-3500 W Polling Time:105s" },
             new SelectListItem{Value="4008e030-fd3a-4f8c-a8ca-4f7609ecdb1e", Text="Occupancy sensor with sensitivity range: true / false °C Polling Time:50s" },
             new SelectListItem{Value="7a3b1db5-959d-46ce-82b6-517773327427", Text="Occupancy sensor with sensitivity range: true / false °C Polling Time:80s" },
             new SelectListItem{Value="a3b8a078-0409-4365-ace6-6f8b5b93d592", Text="Door sensor with sensitivity range: true / false °C Polling Time:90s" },
             new SelectListItem{Value="ec3c4770-5d57-4d81-9c83-a02140b883a1", Text="Door sensor with sensitivity range: true / false °C Polling Time:50s" },
             new SelectListItem{Value="d5d37a46-8ab5-41ec-b7d5-d28c2fd68d3d", Text="Noise sensor with sensitivity range: 20-70 dB Polling Time:40s" },
             new SelectListItem{Value="65d98ff7-b524-49de-8d13-f85753d98858", Text="Noise sensor with sensitivity range: 40-90 dB Polling Time:85s" }
            };
            return this.View(model);
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
                Value = "12"
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
                  OwnerName = s.Owner.UserName,
                  SensorName = s.Name,
                  Value = s.Value,
                  ValueType = s.ValueType,
                  Url = s.Url
              })
              .ToList();

            return this.View(publicViewModel);

        }

    }
}