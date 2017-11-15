using SmartSensors.Data.Models;
using SmartSensors.Data.Models.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartSensors.Service.ViewModels
{
    public class FullSensorViewModel
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public string Url { get; set; }
        
        public int PollingInterval { get; set; }

        public string Value { get; set; }

        public string ValueType { get; set; }
        
        public bool IsPublic { get; set; }

        public string Owner { get; set; }
        
        public int MinRange { get; set; }
        
        public int MaxRange { get; set; }
        
        public ICollection<User> SharedWith { get; set; }

        public static Expression<Func<Sensor, FullSensorViewModel>> Create
        {
            get
            {
                return s => new FullSensorViewModel()
                {
                    Id = s.Id,
                    Owner = s.Owner.UserName,
                    Description = s.Description,
                    Url = s.Url,
                    PollingInterval = s.PollingInterval,
                    IsPublic = s.IsPublic,
                    Name = s.Name,
                    Value = s.Value,
                    ValueType = s.ValueType,
                    MaxRange = s.MaxRange,
                    MinRange = s.MinRange,
                    SharedWith = s.Users 
                };
            }
        }

    }
}
