﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSensors.Service.Contracts
{
    public interface ISensorValueProvider
    {
        Task<string> GetValue(string url);
    }
}
