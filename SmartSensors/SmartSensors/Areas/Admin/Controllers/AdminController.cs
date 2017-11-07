using Bytes2you.Validation;
using SmartSensors.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartSensors.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationUserManager userManager;

        public AdminController(ApplicationUserManager userManager)
        {
            Guard.WhenArgument(userManager, "userManager").IsNull().Throw();
            this.userManager = userManager;
        }

        // GET: Admin/Admin
        public ActionResult AllUsers()
        {
            var usersViewModel = this.userManager.Users
                .Select(u => new UserViewModel()
                {
                    Username = u.UserName
                })
                .ToList();

            return this.View(usersViewModel);
        }
    }
}