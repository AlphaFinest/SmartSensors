using SmartSensors.Data;
using SmartSensors.Service.Contracts;
using SmartSensors.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using SmartSensors.Data.Models;
using Microsoft.AspNet.Identity;

namespace SmartSensors.Service
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<User> userManager;

        public UserService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.userManager = new UserManager<User>(new UserStore<User>(this.dbContext));
        }

        public List<UserViewModel> GetAllUsers()
        {
            var usersViewModel = this.dbContext
               .Users
               .Select(UserViewModel.Create).ToList();
            return usersViewModel;
        }

        public List<UserViewModel> AdminPage()
        {
            var usersViewModel = this.dbContext.Users
                .Select(u => new UserViewModel()
                {
                    Username = u.UserName
                })
                .ToList();

            return usersViewModel;
        }

        public void AddUser(AddUserViewModel model)
        {
            var addUser = new User
            {
                UserName = model.Username,
                Email = model.Email
            };
            var password = model.Password;
            this.userManager.Create(addUser, password);
        }



    }
}
