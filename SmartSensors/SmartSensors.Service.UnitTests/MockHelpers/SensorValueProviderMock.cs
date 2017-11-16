using SmartSensors.Service.Providers;
using SmartSensors.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSensors.Service.UnitTests.MockHelpers
{
    public class SensorValueProviderMock : SensorValueProvider
    {
        private readonly JsonSensorViewModel viewModel;

        public SensorValueProviderMock(JsonSensorViewModel viewModel)
        {
            this.viewModel = viewModel;
        }




        protected async override Task<JsonSensorViewModel> GetAllUrlViewModelFromService(string url)
        {
            return this.viewModel;
        }
    }

}
