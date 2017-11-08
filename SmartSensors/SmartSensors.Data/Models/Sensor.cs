using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSensors.Data.Models.Sensors
{
    public class Sensor
    {
        public Sensor()
        {
            this.Users = new HashSet<User>();
            this.History = new HashSet<History>();
        }

        public int Id { get; set; }

        [StringLength(50, MinimumLength = 5, ErrorMessage = "The name lenght must be between 5 and 50 symbols")]
        public string Name { get; set; }

        public string Description { get; set; }

        [StringLength(100, MinimumLength = 5, ErrorMessage = "The url lenght must be between 5 and 100 symbols")]
        public string Url { get; set; }

        [Range(0, 500, ErrorMessage = "The polling interval must be between 0 and 100")]
        public int PollingInterval { get; set; }

        [Range(0, 1000, ErrorMessage = "The minimal range must be between 0 and 1000")]
        public int MinRange { get; set; }

        [Range(0, 5000, ErrorMessage = "The minimal range must be between 0 and 5000")]
        public int MaxRange { get; set; }

        public bool IsPublic { get; set; }

        public DateTime LastUpdated { get; set; }

        public string Value { get; set; }

        [StringLength(100, MinimumLength = 1, ErrorMessage = "The value lenght must be between 1 and 100 symbols")]
        public string ValueType { get; set; }

        public virtual ICollection<User> Users { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public virtual User Owner { get; set; }

        public virtual ICollection<History> History { get; set; }
    }
}