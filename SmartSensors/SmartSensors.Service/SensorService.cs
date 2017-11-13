﻿using Newtonsoft.Json;
using SmartSensors.Areas.Admin.Models;
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
                            //var responseObject = await content.ReadAsAsync<JsonSensorViewModel>();
                            var r = await content.ReadAsStringAsync();
                            var responseObject = JsonConvert.DeserializeObject<JsonSensorViewModel>(r);
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
        public List<AllSensorsViewModel> GetAllSensors()
        {
            var allSensorsViewModel = this.dbContext.Sensors
             .Select(s => new AllSensorsViewModel()
             {
                 Owner = s.Owner,
                 SensorName = s.Name,
                 Value = s.Value,
                 ValueType = s.ValueType
             })
             .ToList();

            return allSensorsViewModel;
        }

        public void RegisterSensor(RegisterSensorViewModel model)
        {
            var sensor = new Sensor
            {
                Owner = dbContext.Users.First(u => u.UserName == model.Owner),
                Name = model.Name,
                Description = model.Description,
                Url = model.Url,
                PollingInterval = model.PollingInterval,
                ValueType = model.ValueType,
                IsPublic = model.IsPublic,
                MinRange = model.MinRange,
                MaxRange = model.MaxRange,
                LastUpdated = System.DateTime.Now,
                Value = "12"
            };

            dbContext.Sensors.Add(sensor);
            dbContext.SaveChanges();
           
           
        }
    }
}
