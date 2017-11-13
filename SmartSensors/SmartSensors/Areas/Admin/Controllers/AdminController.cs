using Bytes2you.Validation;
using SmartSensors.Areas.Admin.Models;
using SmartSensors.Data;
using SmartSensors.Data.Models;
using SmartSensors.Data.Models.Sensors;
using SmartSensors.Service.Contracts;
using SmartSensors.Service.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace SmartSensors.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationUserManager userManager;
        private readonly ApplicationDbContext dbContext;

        private readonly IUserService userService;
        private readonly ISensorService sensorService;

        public AdminController(ApplicationUserManager userManager, ApplicationDbContext dbContext, IUserService userService, ISensorService sensorService)
        {
            Guard.WhenArgument(userManager, "userManager").IsNull().Throw();
            this.userManager = userManager;

            Guard.WhenArgument(dbContext, "dbContext").IsNull().Throw();
            this.dbContext = dbContext;

            Guard.WhenArgument(userService, "userService").IsNull().Throw();
            this.userService = userService;

            Guard.WhenArgument(sensorService, "sensorService").IsNull().Throw();
            this.sensorService = sensorService;
        }

        // GET: Admin/Admin
        public ActionResult AdminPage()
        {
            var usersViewModel = this.userService.AdminPage();

            return this.View(usersViewModel);
        }

        public ActionResult AllUsers()
        {
            var usersViewModel = this.userService.GetAllUsers();

            return this.View(usersViewModel);
        }

        public ActionResult AllSensors()
        {
            var allSensorsViewModel = this.sensorService.GetAllSensors();

            return this.View(allSensorsViewModel);
        }

        [Authorize]
        public ActionResult AddUser()
        {
            var model = new AddUserViewModel();

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult AddUser(AddUserViewModel model)
        {
            this.userService.AddUser(model);

            return RedirectToAction("AdminPage", "Admin");
        }

        public async Task<ActionResult> EditUser(string username)
        {
            var user = await this.userManager.FindByNameAsync(username);
            var userViewModel = UserViewModel.Create.Compile()(user);
            userViewModel.IsAdmin = await this.userManager.IsInRoleAsync(user.Id, "Admin");

            return this.PartialView("_EditUser", userViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUser(UserViewModel userViewModel)
        {
            if (userViewModel.IsAdmin)
            {
                await this.userManager.AddToRoleAsync(userViewModel.Id, "Admin");
            }
            else
            {
                await this.userManager.RemoveFromRoleAsync(userViewModel.Id, "Admin");
            }

            return this.RedirectToAction("AllUsers");
        }

        [Authorize]
        public ActionResult RegisterSensor()
        {
            var model = new RegisterSensorViewModel();

            return this.View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult RegisterSensor(RegisterSensorViewModel model)
        {
            this.sensorService.RegisterSensor(model);
            
            return this.View(model);
        }

    }
}