using SmartSensors.Service.Providers;
using SmartSensors.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SmartSensors.Service.UnitTests.MockHelpers
{
    public class UrlProviderMock:UrlProvider
    {
        private readonly List<JsonUrlViewModel> viewModel=new List<JsonUrlViewModel>();

        public UrlProviderMock()
        {
        }



        protected  override Task<List<JsonUrlViewModel>> GetUrlViewModelFromService()
        {
            return Task.FromResult(this.viewModel);
        }

    }
}
