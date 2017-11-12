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
        List<PublicViewModel> GetMySensors(string username);
        List<PublicViewModel> GetSharedSensors(string username);
        void RegisterNewSensor(SensorViewModel sensor, string username);
    }
}
