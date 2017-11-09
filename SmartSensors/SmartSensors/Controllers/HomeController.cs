using SmartSensors.Models;
using SmartSensors.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using SmartSensors.Data.Models.Sensors;
using System;

namespace SmartSensors.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ApplicationUserManager userManager;

        public HomeController(ApplicationDbContext dbContext, ApplicationUserManager userManager)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
        }

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<ActionResult> About()
        {
            ViewBag.message = "your application description page.";
            
            var user = await this.userManager.FindByNameAsync(this.User.Identity.Name);
            await this.userManager.AddToRoleAsync(user.Id, "Admin");

            return View();
        }
   

    }
}