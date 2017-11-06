using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSensors.Data.Models.Sensors
{
    public class Sensor
    {
        public Sensor(DateTime timeStamp, string value,string valueType)
        {
            this.TimeStamp = timeStamp;
            this.Value = value;
            this.ValueType = valueType;
        }

        public DateTime TimeStamp { get; set; }
        public string Value { get; set; }
        public string ValueType { get; set; }
        
    }
}
