using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SmartSensors.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationUserManager userManager;

        public HomeController(ApplicationUserManager userManager)
        {
            this.userManager = userManager;
        }

        public ActionResult Index()
        {
            return View();
        }

        //[Authorize]
        //public async task<actionresult> about()
        public ActionResult about()
        {
            ViewBag.message = "your application description page.";


            //var user = await this.usermanager.findbynameasync(this.user.identity.name);

           // await this.usermanager.addtoroleasync(user.id, "admin");
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}