using Newtonsoft.Json;
using SmartSensors.Data;
using SmartSensors.Data.Models;
using SmartSensors.Data.Models.Sensors;
using SmartSensors.Models;
using SmartSensors.Service;
using SmartSensors.Service.Contracts;
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
        private readonly ISensorService service;

        public RequestController(ISensorService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task GetSensors()
        {
            await service.UpdateSensors();
        }
    }
}

