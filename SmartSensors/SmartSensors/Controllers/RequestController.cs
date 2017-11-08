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


        //public async Task GetSensors()
        //{
        //    //List<Sensor> sensorsToUpdate = new List<Sensor>();
        //    //db.Sensors.Where(x=>x.PollingTime<DateTime.Now-x.LastUpdate)
        //    //AppendAll
        //    using (HttpClient client = new HttpClient())
        //    {
        //        client.DefaultRequestHeaders.Add("auth-token", "8e4c46fe-5e1d-4382-b7fc-19541f7bf3b0");
        //        client.BaseAddress = new Uri("http://telerikacademy.icb.bg/api/sensor/");
        //        foreach (var sensor in sensorsToUpdate)
        //        {
        //            using (HttpResponseMessage response = await client.GetAsync(client.BaseAddress + sensor.Url))
        //            {
        //                using (HttpContent content = response.Content)
        //                {
        //                    var json = content.ReadAsStringAsync().Result;
        //                    //db.Sensors.Find(sensor.Id).Value=json.....
        //                    //db.Sensors.Find(sensor.Id).Date=json.....
        //                    //db.History.Add(sensor);
                            
        //                }
        //            }
        //        }
        //    }
         //   db.SaveChanges();
        //}
    }
}

