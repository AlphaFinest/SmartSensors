using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SmartSensors.Service.Contracts
{
    public interface IValueTypeProvider
    {
        Task<List<SelectListItem>> GetValueTypePattern();
    }
}
