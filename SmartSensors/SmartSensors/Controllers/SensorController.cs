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

        public SensorController(ApplicationDbContext dbContext, IUrlProvider urlProvider, IValueTypeProvider valueTypeProvider, ISensorService sensorService)
        {
            Guard.WhenArgument(dbContext, "dbContext").IsNull().Throw();
            Guard.WhenArgument(urlProvider, "urlProvider").IsNull().Throw();
            Guard.WhenArgument(valueTypeProvider, "valueTypeProvider").IsNull().Throw();
            Guard.WhenArgument(sensorService, "sensorService").IsNull().Throw();

            this.dbContext = dbContext;
            this.urlProvider = urlProvider;
            this.valueTypeProvider = valueTypeProvider;
            this.sensorService = sensorService;
        }

        [HttpGet]
        [ChildActionOnly]
        [OutputCache(Duration = 3600)]
        public async Task<ActionResult> SensorsDropDown(SensorViewModel viewModel)
        {
            viewModel.UrlCollection = await this.urlProvider.GetUrlPattern();
            viewModel.ValueTypeCollection = await this.valueTypeProvider.GetValueTypePattern();
            return this.PartialView(viewModel);
        }

        //[HttpGet]
        //public async Task<ActionResult> SensorsValueType()
        //{
        //    var viewModel = new SensorViewModel();
        //    viewModel.ValueTypeCollection = await this.valueTypeProvider.GetValueTypePattern();
        //    return this.PartialView(viewModel);
        //}

        public ActionResult OnReady()
        {
            return View();
        }

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
            this.sensorService.RegisterNewSensor(model, this.User.Identity.Name);

            return this.View(model);
        }

        public ActionResult PublicSensors()
        {
            var publicViewModel = this.dbContext.Sensors.Where(s => s.IsPublic.Equals(true))
              .Select(s => new PublicViewModel()
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