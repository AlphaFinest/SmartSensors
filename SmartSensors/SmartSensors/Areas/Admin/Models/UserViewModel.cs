﻿using SmartSensors.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System;
using SmartSensors.Data.Models;

namespace SmartSensors.Areas.Admin.Models
{
    public class UserViewModel
    {
        public string Username { get; set; }

        public static Expression<Func<ApplicationUser, UserViewModel>> Create
        {
            get
            {
                return u => new UserViewModel()
                {
                    Username = u.UserName
                };
            }
        }
    }
}