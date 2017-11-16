using Newtonsoft.Json;
using SmartSensors.Service.Contracts;
using SmartSensors.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SmartSensors.Service.Providers
{
    public class SensorValueProvider : ISensorValueProvider
    {
        public async Task<string> GetValue(string url)
        {
            var responseObject =  await GetAllUrlViewModelFromService(url);
            return responseObject.Value;
        }



        protected async virtual Task<JsonSensorViewModel> GetAllUrlViewModelFromService(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("auth-token", "8e4c46fe-5e1d-4382-b7fc-19541f7bf3b0");
                client.BaseAddress = new Uri("http://telerikacademy.icb.bg/api/sensor");

                using (HttpResponseMessage response = await client.GetAsync(client.BaseAddress + "/" + url))
                {
                    using (HttpContent content = response.Content)
                    {
                        var responseContent = await content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<JsonSensorViewModel>(responseContent);
                    }
                }
            }
        }

    }
}

