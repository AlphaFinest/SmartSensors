using SmartSensors.Data.Models;
using SmartSensors.Data.Models.Sensors;
using SmartSensors.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSensors.Service.Contracts
{
    public interface ISensorService
    {
        Task UpdateSensors();
        IEnumerable<PublicViewModels> GetMySensors(string name);
        IEnumerable<PublicViewModels> GetSharedSensors(string name);
        void RegisterNewSensor(Sensor sensor);
    }
}
