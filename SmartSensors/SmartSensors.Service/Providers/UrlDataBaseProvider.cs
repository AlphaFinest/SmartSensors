using Newtonsoft.Json;
using SmartSensors.Data;
using SmartSensors.Data.Models;
using SmartSensors.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SmartSensors.Service.Providers
{
    public class UrlDataBaseProvider
    {
        private readonly ApplicationDbContext dbContext;

        public UrlDataBaseProvider(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task ProvideUrls()
        {
            using (HttpClient client = new HttpClient())
            {
                var responseObject = await GetAllUrlViewModelFromService();
                foreach (var url in responseObject)
                {
                    if (!this.dbContext.Urls.Select(x => x.SensorUrl).Contains(url.SensorId))
                    {
                        this.dbContext.Urls.Add(new Url()
                        {
                            SensorUrl = url.SensorId,
                            Description = url.Description,
                            SensorType = url.Tag,
                            PollingInterval = url.MinPollingIntervalInSeconds,
                            ValueType = url.MeasureType
                        });
                    }
                }
                this.dbContext.SaveChanges();
            }
        }




        protected async virtual Task<List<UrlsDataBaseViewModel>> GetAllUrlViewModelFromService()
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
                        return JsonConvert.DeserializeObject<List<UrlsDataBaseViewModel>>(responseContent);
                    }
                }
            }
        }

    }
}

