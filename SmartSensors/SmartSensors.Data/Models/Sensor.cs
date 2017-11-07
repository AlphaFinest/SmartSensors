using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSensors.Data.Models.Sensors
{
    public class Sensor
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 5, ErrorMessage = "The name lenght must be between 5 and 50 symbols")]
        public string Name { get; set; }

        [StringLength(100, MinimumLength = 5, ErrorMessage = "The description lenght must be between 5 and 100 symbols")]
        public string Description { get; set; }

        [StringLength(100, MinimumLength = 5, ErrorMessage = "The url lenght must be between 5 and 100 symbols")]
        public string Url { get; set; }

        [Range(0, 100, ErrorMessage = "The polling interval must be between 0 and 100")]
        public int PollingInterval { get; set; }

        [Range(0, 1000, ErrorMessage = "The minimal range must be between 0 and 1000")]
        public int MinRange { get; set; }

        [Range(0, 5000, ErrorMessage = "The minimal range must be between 0 and 5000")]
        public int MaxRange { get; set; }

        public bool IsPublic { get; set; }

        public DateTime TimeStamp { get; set; }

        [StringLength(5000, MinimumLength = 5, ErrorMessage = "The value lenght must be between 5 and 5000 symbols")]
        public string Value { get; set; }

        [StringLength(100, MinimumLength = 1, ErrorMessage = "The value lenght must be between 5 and 5000 symbols")]
        public string ValueType { get; set; }

        public ApplicationUser Owner { get; set; }

    }
}