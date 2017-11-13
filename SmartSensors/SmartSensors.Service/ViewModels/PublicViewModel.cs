using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using SmartSensors.Data.Models;
using SmartSensors.Data.Models.Sensors;

namespace SmartSensors.Service.ViewModels
{
    public class PublicViewModel
    {
        public string OwnerName { get; set; }

        public string SensorName { get; set; }

        public string Value { get; set; }

        public string ValueType { get; set; }

        public string Url { get; set; }

        public static Expression<Func<Sensor, PublicViewModel>> Create
        {
            get
            {
                return s => new PublicViewModel()
                {
                    OwnerName = s.Owner.UserName,
                    SensorName = s.Name,
                    Value = s.Value,
                    ValueType = s.ValueType,
                    Url = s.Url
                };
            }
        }

    }
}