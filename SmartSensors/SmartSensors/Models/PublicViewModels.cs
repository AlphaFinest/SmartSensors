using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using SmartSensors.Data.Models;

namespace SmartSensors.Models
{
    public class PublicViewModels
    {
        public string OwnerName { get; set; }

        public string SensorName { get; set; }

        public string Value { get; set; }

        public string ValueType { get; set; }

        public string Url { get; set; }

    }
}