using Bytes2you.Validation;
using SmartSensors.Areas.Admin.Models;
using SmartSensors.Data.Models.Sensors;
using SmartSensors.Data;
using System.Linq;
using System.Web.Mvc;


namespace SmartSensors.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationUserManager userManager;
        private readonly ApplicationDbContext dbContext;

        public AdminController(ApplicationUserManager userManager, ApplicationDbContext dbContext)
        {
            Guard.WhenArgument(userManager, "userManager").IsNull().Throw();
            this.userManager = userManager;

            Guard.WhenArgument(dbContext, "dbContext").IsNull().Throw();
            this.dbContext = dbContext;
        }

        // GET: Admin/Admin
        public ActionResult AdminPage()
        {
            var usersViewModel = this.userManager.Users
                .Select(u => new UserViewModel()
                {
                    Username = u.UserName
                })
                .ToList();

            return this.View(usersViewModel);
        }

        public ActionResult AllUsers()
        {
            var usersViewModel = this.dbContext
                .Users
                .Select(UserViewModel.Create).ToList();

            return this.View(usersViewModel);
        }

        public ActionResult AllSensors()
        {
            var allSensorsViewModel = this.dbContext.Sensors
              .Select(s => new AllSensorsViewModel()
              {
                  Owner = s.Owner,
                  SensorName = s.Name,
                  Value = s.Value,
                  ValueType = s.ValueType
              })
              .ToList();

            return this.View(allSensorsViewModel);
        }
        public ActionResult RegisterSensors()
        {
            var registerSensorsViewModel = this.dbContext.Sensors
              .Select(s => new RegisterSensorsViewModel()
              {
                  Owner = s.Owner,
                  Name = s.Name,
                  Value = s.Value
              })
              .ToList();

            return this.View(registerSensorsViewModel);
        }

    }
}