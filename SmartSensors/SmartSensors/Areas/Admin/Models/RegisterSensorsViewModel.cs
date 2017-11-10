using SmartSensors.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace SmartSensors.Areas.Admin.Models
{
    public class RegisterSensorsViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Owner")]
        public User Owner { get; set; }

        [Required]
        [Display(Name = "Value")]
        public string Value { get; set; }

    }
}