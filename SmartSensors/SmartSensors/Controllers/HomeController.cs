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
        private readonly ApplicationUserManager userManager;
        private readonly ApplicationDbContext dbContext;

        public HomeController(ApplicationUserManager userManager, ApplicationDbContext dbContext)
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

            //this.dbContext.Roles.Add(new IdentityRole() { Name = "Admin" });
            //await this.dbContext.SaveChangesAsync();

            //var user = await this.userManager.FindByNameAsync(this.User.Identity.Name);
            //await this.userManager.AddToRoleAsync(user.Id, "Admin");

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}