﻿using Newtonsoft.Json;
using SmartSensors.Service.Contracts;
using SmartSensors.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SmartSensors.Service.UrlProvider
{
    public class UrlProvider:IUrlProvider
    {
        public async Task<List<SelectListItem>> GetUrlPattern()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("auth-token", "8e4c46fe-5e1d-4382-b7fc-19541f7bf3b0");
                client.BaseAddress = new Uri("http://telerikacademy.icb.bg/api/sensor/all");

                using (HttpResponseMessage response = await client.GetAsync(client.BaseAddress))
                {
                    using (HttpContent content = response.Content)
                    {
                        var responseContent = await content.ReadAsStringAsync();
                        var responseObject = JsonConvert.DeserializeObject<List<JsonUrlViewModel>>(responseContent);
                        var listOfSensors = new List<SelectListItem>();
                        foreach(var sensor in responseObject)
                        {
                            listOfSensors.Add(new SelectListItem() { Text = sensor.Description+"with minimal pooling interval:"+sensor.MinPollingIntervalInSeconds.ToString(), Value = sensor.SensorId,});
                        }
                        return listOfSensors;
                    }
                }
            }
        }
    }
}