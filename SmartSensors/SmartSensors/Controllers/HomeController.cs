using SmartSensors.Models;
using SmartSensors.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;


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

        [Authorize(Roles = "Admin")]
        public ActionResult Contact()
        {

            ViewBag.Message = "Your contact page.";

            return View();
        }

    
        public ActionResult PublicSensors()
        {
            ViewBag.message = "Table of all public sensors";
                   
            return View();
        }

    }
}