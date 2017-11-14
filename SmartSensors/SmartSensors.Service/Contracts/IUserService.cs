using SmartSensors.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSensors.Service.Contracts
{
    public interface IUserService
    {
        List<UserViewModel> GetAllUsers();

        List<UserViewModel> AdminPage();

        void AddUser(AddUserViewModel model);

        Task<UserViewModel> ServiceEditUser(string username);

        Task ServiceEditUser(UserViewModel userViewModel);
    }
}
