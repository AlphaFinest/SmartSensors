﻿using Bytes2you.Validation;
using SmartSensors.Areas.Admin.Models;
using SmartSensors.Data;
using SmartSensors.Data.Models;
using SmartSensors.Data.Models.Sensors;
using System.Linq;
using System.Threading.Tasks;
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
            var addUser = new User
            {
                UserName = model.Username,
                //Email = model.Email,
                //Password = model.Password

            };

            dbContext.Users.Add(addUser);
            dbContext.SaveChanges();

            return this.View(model);
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
            var sensor = new Sensor
            {
                //Owner = model.Owner,
                Name = model.Name,
                Description = model.Description,
                //Url = model.Url,
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

    }
}