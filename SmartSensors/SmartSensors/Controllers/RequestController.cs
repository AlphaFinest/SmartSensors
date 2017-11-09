using Newtonsoft.Json;
using SmartSensors.Data;
using SmartSensors.Data.Models;
using SmartSensors.Data.Models.Sensors;
using SmartSensors.Models;
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
        [HttpGet]
        public async Task GetSensors()
        {
            var sensorsToUpdate = this.dbContext.Sensors.SqlQuery("SELECT * FROM Sensors s WHERE GETDATE() > DATEADD(ss, s.PollingInterval, S.LastUpdated)").ToList();
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("auth-token", "8e4c46fe-5e1d-4382-b7fc-19541f7bf3b0");
                client.BaseAddress = new Uri("http://telerikacademy.icb.bg/api/sensor");
                foreach (var sensor in sensorsToUpdate)
                {
                    using (HttpResponseMessage response = await client.GetAsync(client.BaseAddress + "/" + sensor.Url))
                    {
                        using (HttpContent content = response.Content)
                        {
                            var r = await content.ReadAsStringAsync();
                            var responseObject = JsonConvert.DeserializeObject<JsonSensorViewModel>(r);
                            dbContext.Sensors.SingleOrDefault(x => x.Id == sensor.Id).Value = responseObject.Value.ToString();
                            dbContext.Sensors.SingleOrDefault(x => x.Id == sensor.Id).LastUpdated = DateTime.Now;
                            var historyToAdd = new History();
                            historyToAdd.Sensor = sensor;
                            historyToAdd.UpdateDate = DateTime.Now;
                            historyToAdd.Value = responseObject.Value.ToString();
                            dbContext.History.Add(historyToAdd);
                        }
                    }
                }
            }
            dbContext.SaveChanges();
        }
    }
}

