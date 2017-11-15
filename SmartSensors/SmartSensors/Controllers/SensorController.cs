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
using System.Web;
using System;
using System.Web.Caching;

namespace SmartSensors.Controllers
{
    public class SensorController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IUrlProvider urlProvider;
        private readonly ISensorService sensorService;

        public SensorController(ApplicationDbContext dbContext, IUrlProvider urlProvider, ISensorService sensorService)
        {
            Guard.WhenArgument(dbContext, "dbContext").IsNull().Throw();
            Guard.WhenArgument(urlProvider, "urlProvider").IsNull().Throw();
            Guard.WhenArgument(sensorService, "sensorService").IsNull().Throw();

            this.dbContext = dbContext;
            this.urlProvider = urlProvider;
            this.sensorService = sensorService;
        }

        public ActionResult ValueType()
        {
            var viewModel = this.dbContext.Urls.ToList();
            return View(viewModel);
        }


        [Authorize]
        public async Task<ActionResult> RegisterSensor()
        {
            SensorViewModel viewModel = new SensorViewModel();
            viewModel.UrlCollection = await this.urlProvider.GetUrlPattern();

            return this.View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> RegisterSensor(SensorViewModel model)
        {
            await this.sensorService.RegisterNewSensor(model, this.User.Identity.Name);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult PublicSensors()
        {
            var publicViewModel = this.sensorService.GetPublicSensor();

            return this.View(publicViewModel);
        }

        [Authorize]
        public ActionResult MySensors()
        {
            var mySensors = sensorService.GetMySensors(this.User.Identity.Name);

            return this.View(mySensors);
        }

        [Authorize]
        public ActionResult SharedSensors()
        {
            var sharedSensors = sensorService.GetSharedSensors(this.User.Identity.Name);

            return this.View(sharedSensors);
        }

        [Authorize]
        public async Task<ActionResult> EditSensor(int sensor)
        {
            var sensorViewModel = this.sensorService.GetSpecificSensor(sensor);
            sensorViewModel.UrlCollection = await this.urlProvider.GetUrlPattern();

            return this.View("EditSensor", sensorViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> EditSensor(SensorViewModel sensor)
        {
            await this.sensorService.EditSensorOwner(sensor);

            return this.RedirectToAction("MySensors");
        }
    }

    public class UrlProviderDecorator : IUrlProvider
    {
        private readonly IUrlProvider decorated;
        private readonly HttpContext context;

        public UrlProviderDecorator(IUrlProvider decorated, HttpContext context)
        {
            this.decorated = decorated;
            this.context = context;
        }

        public async Task<List<SelectListItem>> GetUrlPattern()
        {
            List<SelectListItem> result = null;
            result = this.context.Cache["UrlCollection"] as List<SelectListItem>;
            if (result == null)
            {
                result = await this.decorated.GetUrlPattern();
                this.context.Cache.Add("UrlCollection", result, null, DateTime.Now.AddHours(1), Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
            }

            return result;
        }

        
    }
}