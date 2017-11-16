using SmartSensors.Data;
using SmartSensors.Service.Providers;
using SmartSensors.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSensors.Service.UnitTests.MockHelpers
{
    public class UrlDataBaseProviderMock:UrlDataBaseProvider
    {
        private readonly ApplicationDbContext dbContext;
        private readonly List<UrlsDataBaseViewModel> viewModel = new List<UrlsDataBaseViewModel>();
        private int index = 0;

        public UrlDataBaseProviderMock(ApplicationDbContext dbContext) :base(dbContext)
        {
        }


        protected  override Task<List<UrlsDataBaseViewModel>> GetAllUrlViewModelFromService()
        {
            return Task.FromResult(this.viewModel);
        }



    }
}
