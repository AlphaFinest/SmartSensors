using SmartSensors.Data;
using SmartSensors.Data.Models.Sensors;
using SmartSensors.Service.Contracts;
using SmartSensors.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSensors.Service.UnitTests.MockHelpers
{
    class SensorServiceMock:SensorService
    {
        private readonly List<JsonSensorViewModel> viewModels;
        private readonly List<Sensor> sensors;
        private readonly DateTime date;
        private int index = 0;

        public SensorServiceMock(ApplicationDbContext dbContext,
            ISensorValueProvider valueProvider,
            IUserSharingProvider userSharingProvider,
            List<JsonSensorViewModel> viewModels,
            List<Sensor> sensors,
            DateTime date)
            : base(dbContext, valueProvider, userSharingProvider)
        {
            this.viewModels = viewModels;
            this.sensors = sensors;
            this.date = date;
        }

        protected override DateTime GetDateTime()
        {
            return this.date;
        }

        protected override Task<JsonSensorViewModel> GetSensorViewModelFromService(string sensorUrl)
        {
            return Task.FromResult(this.viewModels[index++]);
        }

        protected override IEnumerable<Sensor> GetSensors(string sqlQuery)
        {
            return this.sensors;
        }
    }
}
