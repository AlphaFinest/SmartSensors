﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartSensors.Service.ViewModels
{
    public class JsonSensorViewModel
    {
        public DateTime TimeStamp { get; set; }
        public string Value { get; set; }
        public string ValueType { get; set; }

    }
}