using Bytes2you.Validation;
using SmartSensors.Data;
using SmartSensors.Service.Contracts;
using SmartSensors.Service.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace SmartSensors.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUrlProvider urlProvider;
        private readonly IUserService userService;
        private readonly ISensorService sensorService;

        public AdminController(IUserService userService, ISensorService sensorService, IUrlProvider urlProvider)
        {
            Guard.WhenArgument(userService, "userService").IsNull().Throw();
            this.userService = userService;

            Guard.WhenArgument(sensorService, "sensorService").IsNull().Throw();
            this.sensorService = sensorService;

            Guard.WhenArgument(urlProvider, "urlProvider").IsNull().Throw();
            this.urlProvider = urlProvider;
        }

        // GET: Admin/Admin 
        [Authorize(Roles = "Admin")]
        public ActionResult AdminPage()
        {
            return this.View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AllUsers()
        {
            var usersViewModel = this.userService.GetAllUsers();

            return this.View(usersViewModel);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AllSensors()
        {
            var fullSensorsViewModel = this.sensorService.GetAllSensors();

            return this.View(fullSensorsViewModel);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AddUser()
        {
            var model = new AddUserViewModel();

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult AddUser(AddUserViewModel model)
        {
            this.userService.AddUser(model);

            return RedirectToAction("AdminPage", "Admin");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult EditUser(string username)
        {
            var userViewModel = this.userService.ServiceEditUser(username);

            return this.View("EditUser", userViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> EditUser(UserViewModel userViewModel)
        {
            await this.userService.ServiceEditUser(userViewModel);

            return this.RedirectToAction("AllUsers");
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> RegisterSensor()
        {
            var model = new SensorViewModel();
            model.UrlCollection = await this.urlProvider.GetUrlPattern();

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult RegisterSensor(SensorViewModel model)
        {
            this.sensorService.GetRegisterSensor(model);

            return RedirectToAction("AdminPage", "Admin");
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> EditSensor(int sensor)
        {
            var sensorViewModel = this.sensorService.GetSpecificSensor(sensor);
            sensorViewModel.UrlCollection = await this.urlProvider.GetUrlPattern();

            return this.View("EditSensor", sensorViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> EditSensor(SensorViewModel sensor)
        {
            await this.sensorService.EditSensor(sensor);

            return this.RedirectToAction("AllSensors");
        }
    }
}