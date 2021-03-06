﻿using Bytes2you.Validation;
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
        private readonly ISensorValueProvider valueProvider;
        private readonly IUserSharingProvider userSharingProvider;

        public SensorService(ApplicationDbContext dbContext, ISensorValueProvider valueProvider, IUserSharingProvider userSharingProvider)
        {
            Guard.WhenArgument(dbContext, "dbContext").IsNull().Throw();
            this.dbContext = dbContext;
            Guard.WhenArgument(valueProvider, "valueProvider").IsNull().Throw();
            this.valueProvider = valueProvider;
            Guard.WhenArgument(userSharingProvider, "userSharingProvider").IsNull().Throw();
            this.userSharingProvider = userSharingProvider;
        }

        public async Task UpdateSensors()
        {
            var sensorsToUpdate = this.GetSensors("SELECT * FROM Sensors s WHERE GETDATE() > DATEADD(ss, s.PollingInterval, S.LastUpdated)");
            
            foreach (var sensor in sensorsToUpdate)
            {
                JsonSensorViewModel viewModel = await this.GetSensorViewModelFromService(sensor.Url);

                sensor.Value = viewModel.Value;
                sensor.LastUpdated = this.GetDateTime();
                var historyToAdd = new History
                {
                    Sensor = sensor,
                    UpdateDate = this.GetDateTime(),
                    Value = viewModel.Value
                };
                this.dbContext.History.Add(historyToAdd);
            }
             dbContext.SaveChanges();
        }

        protected virtual IEnumerable<Sensor> GetSensors(string sqlQuery)
        {
            return this.dbContext.Sensors.SqlQuery(sqlQuery).ToList();
        }

        protected virtual DateTime GetDateTime()
        {
            return DateTime.Now;
        }

        protected async virtual Task<JsonSensorViewModel> GetSensorViewModelFromService(string sensorUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("auth-token", "8e4c46fe-5e1d-4382-b7fc-19541f7bf3b0");
                client.BaseAddress = new Uri("http://telerikacademy.icb.bg/api/sensor");

                using (HttpResponseMessage response = await client.GetAsync(client.BaseAddress + "/" + sensorUrl))
                {
                    using (HttpContent content = response.Content)
                    {
                        var responseContent = await content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<JsonSensorViewModel>(responseContent);
                    }
                }
            }
        }




        public List<FullSensorViewModel> GetMySensors(string username)
        {
            var mySensors = this.dbContext.Users.First(u => u.UserName == username).MySensors.AsQueryable();
            var sensors = mySensors.Select(FullSensorViewModel.Create).ToList();

            return sensors;
        }

        public List<PublicViewModel> GetSharedSensors(string username)
        {
            var sharedSensors = this.dbContext.Users.First(u => u.UserName == username).SharedSensors.AsQueryable();
            var sensors = sharedSensors.Select(PublicViewModel.Create).ToList();

            return sensors;
        }


        public async Task RegisterNewSensor(SensorViewModel model, string username)
        {
            Sensor sensor = new Sensor()
            {
                Name = model.Name,
                Description = model.Description,
                Url = model.Url,
                PollingInterval = model.PollingInterval,
                ValueType = this.dbContext.Urls.FirstOrDefault(x => x.SensorUrl == model.Url).ValueType,
                IsPublic = model.IsPublic,
                MinRange = model.MinRange,
                MaxRange = model.MaxRange,
                LastUpdated = DateTime.Now,
                Owner = this.dbContext.Users.First(u => u.UserName == username),
                Value = await this.valueProvider.GetValue(model.Url),
                Users = this.userSharingProvider.GetSubscribers(model.SharedWith)

            };

            this.dbContext.Sensors.Add(sensor);
            this.dbContext.SaveChanges();
        }

        public List<FullSensorViewModel> GetAllSensors()
        {
            var allSensors = this.dbContext.Sensors
              .Select(FullSensorViewModel.Create).ToList();

            return allSensors;
        }

        public async Task GetRegisterSensor(SensorViewModel model)
        {
            var sensor = new Sensor
            {
                Owner =  dbContext.Users.First(u => u.UserName == model.Owner),
                Name = model.Name,
                Description = model.Description,
                Url = model.Url,
                PollingInterval = model.PollingInterval,
                ValueType = this.dbContext.Urls.FirstOrDefault(x => x.SensorUrl == model.Url).ValueType,
                IsPublic = model.IsPublic,
                MinRange = model.MinRange,
                MaxRange = model.MaxRange,
                LastUpdated = System.DateTime.Now,
                Value = await this.valueProvider.GetValue(model.Url)
            };

            dbContext.Sensors.Add(sensor);
            dbContext.SaveChanges();
        }
        public List<PublicViewModel> GetPublicSensor()
        {
            var publicViewModel = this.dbContext.Sensors.Where(s => s.IsPublic)
              .Select(s => new PublicViewModel()
              {
                  OwnerName = s.Owner.UserName,
                  SensorName = s.Name,
                  Value = s.Value,
                  ValueType = s.ValueType,
                  Url = s.Url
              })
              .ToList();

            return publicViewModel;
        }

        public SensorViewModel GetSpecificSensor(int id)
        {
            var model = this.dbContext.Sensors.Find(id);

            var viewModel = new SensorViewModel()
            {
                Id = model.Id,
                Owner = model.Owner.UserName,
                Name = model.Name,
                Description = model.Description,
                Url = model.Url,
                PollingInterval = model.PollingInterval,
                IsPublic = model.IsPublic,
                MinRange = model.MinRange,
                MaxRange = model.MaxRange,
                SharedWith = GetSharedWithLikeString(model)

            };
            return viewModel;
        }

        public string GetSharedWithLikeString(Sensor sensor)
        {
            var strings = new List<string>();
            foreach (var sen in sensor.Users)
            {
                strings.Add(sen.UserName);
            }

            return (string.Join(", ", strings));
        }

        public async Task EditSensor(SensorViewModel model)
        {
            var sensor = this.dbContext.Sensors.Find(model.Id);

            if (sensor.Owner.UserName != model.Owner)
            {
                sensor.Owner = this.dbContext.Users.First(u => u.UserName == model.Owner);
            }
            sensor.Name = model.Name;
            sensor.Description = model.Description;
            if (model.Url != sensor.Url)
            {
                sensor.Url = model.Url;
                sensor.ValueType = this.dbContext.Urls.FirstOrDefault(x => x.SensorUrl == model.Url).ValueType;
                sensor.Value = await this.valueProvider.GetValue(model.Url);
            }
            sensor.PollingInterval = model.PollingInterval;
            sensor.IsPublic = model.IsPublic;
            sensor.MinRange = model.MinRange;
            sensor.MaxRange = model.MaxRange;
            if (GetSharedWithLikeString(sensor) != model.SharedWith)
            {
                sensor.Users = this.userSharingProvider.GetSubscribers(model.SharedWith);
            }

            dbContext.SaveChanges();
        }

        public async Task EditSensorOwner(SensorViewModel model)
        {
            var sensor = this.dbContext.Sensors.Find(model.Id);
            sensor.Name = model.Name;
            sensor.Description = model.Description;
            if (model.Url != sensor.Url)
            {
                sensor.Url = model.Url;
                sensor.ValueType = this.dbContext.Urls.FirstOrDefault(x => x.SensorUrl == model.Url).ValueType;
                sensor.Value = await this.valueProvider.GetValue(model.Url);
            }
            sensor.PollingInterval = model.PollingInterval;
            sensor.IsPublic = model.IsPublic;
            sensor.MinRange = model.MinRange;
            sensor.MaxRange = model.MaxRange;
            if (GetSharedWithLikeString(sensor) != model.SharedWith)
            {
                sensor.Users = this.userSharingProvider.GetSubscribers(model.SharedWith);
            }

            dbContext.SaveChanges();
        }
    }
}
