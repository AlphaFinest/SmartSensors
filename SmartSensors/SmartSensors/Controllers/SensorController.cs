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

        public SensorController(ApplicationDbContext dbContext,IUrlProvider urlProvider, IValueTypeProvider valueTypeProvider)
        {
            Guard.WhenArgument(dbContext, "dbContext").IsNull().Throw();
            this.dbContext = dbContext;
            this.urlProvider = urlProvider;
            this.valueTypeProvider = valueTypeProvider;
        }

        [HttpGet]
        [ChildActionOnly]
        [OutputCache(Duration = 3600)]
        public async Task<ActionResult> SensorsDropDown()
        {
            var viewModel = new SensorViewModel();
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