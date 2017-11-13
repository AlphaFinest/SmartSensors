﻿using SmartSensors.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSensors.Service.Contracts
{
    public interface ISensorService
    {
        Task UpdateSensors();

        List<AllSensorsViewModel> GetAllSensors();

        void RegisterSensor(RegisterSensorViewModel model);
    }
}
