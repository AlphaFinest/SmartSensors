using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartSensors.Controllers
{
    public class RequestController : Controller
    {
        // GET: Request
        //public ActionResult GetSensors()
        //{
        //    var webRequest = System.Net.WebRequest.Create("http://telerikacademy.icb.bg/api/sensor/d5d37a46-8ab5-41ec-b7d5-d28c2fd68d3d");
        //    if (webRequest != null)
        //    {
        //        webRequest.Method = "GET";
        //        webRequest.ContentType = "application/json";
        //        webRequest.Headers["auth-token"] = "8e4c46fe-5e1d-4382-b7fc-19541f7bf3b0";
        //        using (System.IO.Stream s = webRequest.GetResponse().GetResponseStream())
        //        {
        //            using (System.IO.StreamReader sr = new System.IO.StreamReader(s))
        //            {
        //                var jsonResponse = sr.ReadToEnd();
        //            }
        //        }
        //    }
        //    return View();
        //}
    }
}