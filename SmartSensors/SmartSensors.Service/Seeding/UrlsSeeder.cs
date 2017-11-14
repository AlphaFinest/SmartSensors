using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using SmartSensors.Data;
using SmartSensors.Data.Models;
using SmartSensors.Service.Contracts;
using SmartSensors.Service.Providers;
using SmartSensors.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SmartSensors.Service.Seeding
{
    public class UrlsSeeder : ISeeder
    {
        private readonly ApplicationDbContext dbContext;

        public UrlsSeeder(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Seed()
        {
            var urlSeeder = new UrlDataBaseProvider(this.dbContext);
            urlSeeder.ProvideUrls();
        }
    }
}
