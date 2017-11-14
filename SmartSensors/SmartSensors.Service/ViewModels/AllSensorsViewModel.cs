using SmartSensors.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace SmartSensors.Service.ViewModels
{
    public class AllSensorsViewModel
    {
        public string Owner { get; set; }

        public string SensorName { get; set; }

        public string Value { get; set; }

        public string ValueType { get; set; }

    }
}