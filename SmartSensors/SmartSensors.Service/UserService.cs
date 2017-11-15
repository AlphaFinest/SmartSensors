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
using System.Web.Mvc;

namespace SmartSensors.Service
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> userManager;
        private readonly ApplicationDbContext dbContext;

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
            foreach (var item in usersViewModel)
            {
                item.IsAdmin = this.userManager.IsInRole(item.Id, "Admin");
            }
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

        
        public async Task<UserViewModel> ServiceEditUser(string username)
        {
            var user =  this.userManager.FindByName(username);
            var userViewModel = UserViewModel.Create.Compile()(user);
            userViewModel.IsAdmin = await this.userManager.IsInRoleAsync(user.Id, "Admin");

            return userViewModel;
        }

        public async Task ServiceEditUser(UserViewModel userViewModel)
        {
            var currentUser = this.userManager.Users.First(k => k.Id == userViewModel.Id);

            if (currentUser.UserName != userViewModel.Username) {
                currentUser.UserName = userViewModel.Username;
            }

            if (currentUser.Email != userViewModel.Email)
            {
                currentUser.Email = userViewModel.Email;
            }

            if (userViewModel.IsAdmin)
            {
                await this.userManager.AddToRoleAsync(userViewModel.Id, "Admin");
            }
            else
            {
                await this.userManager.RemoveFromRoleAsync(userViewModel.Id, "Admin");
            }

            userManager.Update(currentUser);


        }

    }
}
