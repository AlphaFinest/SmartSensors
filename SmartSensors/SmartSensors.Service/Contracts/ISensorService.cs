using SmartSensors.Data.Models;
using SmartSensors.Data.Models.Sensors;
using SmartSensors.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SmartSensors.Service.Contracts
{
    public interface ISensorService
    {
        Task UpdateSensors();

        List<PublicViewModel> GetMySensors(string username);

        List<PublicViewModel> GetSharedSensors(string username);

        List<AllSensorsViewModel> GetAllSensors();

        Task RegisterNewSensor(SensorViewModel model, string username);

        void GetRegisterSensor(RegisterSensorViewModel model);

        List<PublicViewModel> GetPublicSensor();

        
    }
}
