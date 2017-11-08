using SmartSensors.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SmartSensors.Controllers
{
    public class RequestController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        // GET: Request

        public RequestController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


    }    
}

