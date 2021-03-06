﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SmartSensors.Data;
using SmartSensors.Data.Models;
using SmartSensors.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSensors.Service.Providers
{
    public class UserSharingProvider : IUserSharingProvider
    {
        private readonly ApplicationDbContext dbContext;

        public UserSharingProvider(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<User> GetSubscribers(string users)
        {
            var result = new List<User>();
            if (users != null)
            {
                var allUsers = users.Split(',').ToList();
                foreach (var user in allUsers)
                {
                    var curentUser = this.dbContext.Users.First(x => x.UserName == user);
                    result.Add(curentUser);
                }
            }
            return result;
        }


    }
}
