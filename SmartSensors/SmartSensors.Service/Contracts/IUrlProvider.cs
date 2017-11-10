using SmartSensors.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SmartSensors.Service.Contracts
{
    public interface IUrlProvider
    {
         Task<List<SelectListItem>> GetUrlPattern();
    }
}
