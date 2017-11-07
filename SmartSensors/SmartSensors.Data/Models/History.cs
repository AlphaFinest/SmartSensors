using SmartSensors.Data.Models.Sensors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSensors.Data.Models
{
    [Table("History")]
    public class History
    {
        public int Id { get; set; }

        public int? SensorId { get; set; }

        public virtual Sensor Sensor { get; set;}

        public DateTime UpdateDate { get; set; }

        public string Value { get; set; }
    }
}
