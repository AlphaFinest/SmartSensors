using SmartSensors.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSensors.Service.Contracts
{
    public interface IUserSharingProvider
    {
       Task<List<User>> GetSubscribers(string users);
    }
}
