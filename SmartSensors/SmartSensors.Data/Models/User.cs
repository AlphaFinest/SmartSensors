﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using SmartSensors.Data.Models.Sensors;

namespace SmartSensors.Data.Models
{
    public class User : IdentityUser
    {
		public User()
        {
            this.MySensors = new HashSet<Sensor>();
            this.SharedSensors = new HashSet<Sensor>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public virtual ICollection<Sensor> SharedSensors { get; set; }

        public virtual ICollection<Sensor> MySensors { get; set; }

       
    }
}
