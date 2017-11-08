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

        public HomeController(ApplicationDbContext dbContext)
        {
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

            //this.dbContext.Roles.Add(new IdentityRole() { Name = "Admin" });
            //await this.dbContext.SaveChangesAsync();

            return View();
        }
   

    }
}