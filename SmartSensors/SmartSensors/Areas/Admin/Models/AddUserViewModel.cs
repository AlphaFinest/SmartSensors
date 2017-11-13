using SmartSensors.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartSensors.Areas.Admin.Models
{
    public class AddUserViewModel
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

    }
}
