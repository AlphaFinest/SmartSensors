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
        private readonly ApplicationDbContext db;
        // GET: Request


        public async Task GetSensors()
        {
            using (HttpClient something = new HttpClient())
            {
                something.DefaultRequestHeaders.Add("auth-token", "8e4c46fe-5e1d-4382-b7fc-19541f7bf3b0");
                using (HttpResponseMessage response = await something.GetAsync("http://telerikacademy.icb.bg/api/sensor/d5d37a46-8ab5-41ec-b7d5-d28c2fd68d3d"))
                {
                    using (HttpContent content = response.Content)
                    {
                        var json = content.ReadAsStringAsync().Result;
                    }
                }
            }
        }

    }
}
