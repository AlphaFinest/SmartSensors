using Newtonsoft.Json;
using SmartSensors.Data;
using SmartSensors.Data.Models;
using SmartSensors.Service.Contracts;
using SmartSensors.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SmartSensors.Service
{
    public class SensorService : ISensorService
    {
        private readonly ApplicationDbContext dbContext;

        public SensorService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task UpdateSensors()
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
                            var responseContent = await content.ReadAsStringAsync();
                            var responseObject = JsonConvert.DeserializeObject<JsonSensorViewModel>(responseContent);

                            dbContext.Sensors.FindAsync(sensor.Id).Result.Value = responseObject.Value.ToString();
                            dbContext.Sensors.FindAsync(sensor.Id).Result.LastUpdated = DateTime.Now;
                            var historyToAdd = new History();
                            historyToAdd.Sensor = sensor;
                            historyToAdd.UpdateDate = DateTime.Now;
                            historyToAdd.Value = responseObject.Value.ToString();
                            dbContext.History.Add(historyToAdd);
                        }
                    }
                }
            }
            await dbContext.SaveChangesAsync();
        }
    }
}
