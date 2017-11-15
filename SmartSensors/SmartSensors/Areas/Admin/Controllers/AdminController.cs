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

        public AdminController(ApplicationUserManager userManager, ApplicationDbContext dbContext, IUserService userService, ISensorService sensorService, IUrlProvider urlProvider)
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
            var usersViewModel = this.userService.AdminPage();

            return this.View(usersViewModel);
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
            var allSensorsViewModel = this.sensorService.GetAllSensors();

            return this.View(allSensorsViewModel);
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
        public async Task<ActionResult> EditUser(string username)
        {
            var userViewModel = await this.userService.ServiceEditUser(username);

            return this.PartialView("_EditUser", userViewModel);
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
            var model = new RegisterSensorViewModel();
            model.UrlCollection = await this.urlProvider.GetUrlPattern();

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult RegisterSensor(RegisterSensorViewModel model)
        {
            this.sensorService.GetRegisterSensor(model);

            return RedirectToAction("AdminPage", "Admin");
        }

    }
}