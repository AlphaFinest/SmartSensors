using Bytes2you.Validation;
using SmartSensors.Data;
using SmartSensors.Data.Models.Sensors;
using SmartSensors.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SmartSensors.Service.Contracts;
using System.Threading.Tasks;
using SmartSensors.Service.ViewModels;

namespace SmartSensors.Controllers
{
    public class SensorController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IUrlProvider urlProvider;
        private readonly IValueTypeProvider valueTypeProvider;
        private readonly ISensorService sensorService;
        private readonly Sensor sensor;
        private readonly SensorViewModel viewModel;

        public SensorController(ApplicationDbContext dbContext, IUrlProvider urlProvider, IValueTypeProvider valueTypeProvider, ISensorService sensorService, Sensor sensor,SensorViewModel viewModel)
        {
            Guard.WhenArgument(dbContext, "dbContext").IsNull().Throw();
            Guard.WhenArgument(urlProvider, "urlProvider").IsNull().Throw();
            Guard.WhenArgument(valueTypeProvider, "valueTypeProvider").IsNull().Throw();
            Guard.WhenArgument(sensorService, "sensorService").IsNull().Throw();
            Guard.WhenArgument(sensor, "sensor").IsNull().Throw();
            this.dbContext = dbContext;
            this.urlProvider = urlProvider;
            this.valueTypeProvider = valueTypeProvider;
            this.sensorService = sensorService;
            this.sensor = sensor;
            this.viewModel = viewModel;
        }

        [HttpGet]
        [ChildActionOnly]
        [OutputCache(Duration = 3600)]
        public async Task<ActionResult> SensorsDropDown()
        {
            viewModel.UrlCollection = await this.urlProvider.GetUrlPattern();
            return this.PartialView(viewModel);
        }

        //[HttpGet]
        //[ChildActionOnly]
        //[OutputCache(Duration = 3600)]
        //public async Task<ActionResult> SensorsValueType()
        //{
        //    var viewModel = new SensorViewModel();
        //    viewModel.ValueTypeCollection = await this.valueTypeProvider.GetValueTypePattern();
        //    return this.PartialView(viewModel);
        //}

        [Authorize]
        public ActionResult RegisterSensor()
        {
            return this.View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult RegisterSensor(SensorViewModel model)
        {
            this.sensor.Name = model.Name;
            this.sensor.Description = model.Description;
            this.sensor.Url = model.Url;
            this.sensor.PollingInterval = model.PollingInterval;
            this.sensor.ValueType = model.ValueType;
            this.sensor.IsPublic = model.IsPublic;
            this.sensor.MinRange = model.MinRange;
            this.sensor.MaxRange = model.MaxRange;
            this.sensor.LastUpdated = System.DateTime.Now;
            this.sensor.Owner = dbContext.Users.First(u => u.UserName == this.User.Identity.Name);
            this.sensor.Value = "12";

            this.sensorService.RegisterNewSensor(sensor);

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

        public ActionResult MySensors()
        {
            var mySensors = sensorService.GetMySensors(this.User.Identity.Name);

            return this.View(mySensors);
        }

        public ActionResult SharedSensors()
        {
            var sharedSensors = sensorService.GetSharedSensors(this.User.Identity.Name);

            return this.View(sharedSensors);
        }

    }
}