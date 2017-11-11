using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using SmartSensors.Data;
using SmartSensors.Data.Models;
using SmartSensors.Data.Models.Sensors;
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

                            this.dbContext.Sensors.FindAsync(sensor.Id).Result.Value = responseObject.Value.ToString();
                            this.dbContext.Sensors.FindAsync(sensor.Id).Result.LastUpdated = DateTime.Now;
                            var historyToAdd = new History();
                            historyToAdd.Sensor = sensor;
                            historyToAdd.UpdateDate = DateTime.Now;
                            historyToAdd.Value = responseObject.Value.ToString();
                            this.dbContext.History.Add(historyToAdd);
                        }
                    }
                }
            }
            await dbContext.SaveChangesAsync();
        }

        public IEnumerable<PublicViewModels> GetMySensors(string name)
        {
            var sensors = this.dbContext.Users.First(u => u.UserName == name).MySensors
                .Select(s => new PublicViewModels()
                {
                    OwnerName = s.Owner.UserName,
                    SensorName = s.Name,
                    Value = s.Value,
                    ValueType = s.ValueType,
                    Url = s.Url
                }).ToList();

            return sensors;
        }

        public IEnumerable<PublicViewModels> GetSharedSensors(string name)
        {
            var sensors = this.dbContext.Users.First(u => u.UserName == name).SharedSensors
                .Select(s => new PublicViewModels()
                {
                    OwnerName = s.Owner.UserName,
                    SensorName = s.Name,
                    Value = s.Value,
                    ValueType = s.ValueType,
                    Url = s.Url
                }).ToList();

            return sensors;
        }


        public void RegisterNewSensor(Sensor sensor)
        {
            this.dbContext.Sensors.Add(sensor);
            this.dbContext.SaveChanges();
        }
    }
}
