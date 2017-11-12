using SmartSensors.Data.Models;
using System;
using System.Linq.Expressions;

namespace SmartSensors.Areas.Admin.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public bool IsAdmin { get; set; }

        public static Expression<Func<User, UserViewModel>> Create
        {
            get
            {
                return u => new UserViewModel()
                {
                    Id = u.Id,
                    Username = u.UserName
                };
            }
        }
    }
}