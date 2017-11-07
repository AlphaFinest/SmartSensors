using SmartSensors.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartSensors.Models
{
    public class SensorViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Url")]
        public string Url { get; set; }

        [Required]
        [Display(Name = "Polling Interval")]
        public string PollingInterval { get; set; }

        [Required]
        [Display(Name = "Value Type")]
        public string ValueType { get; set; }

        [Required]
        [Display(Name = "Public")]
        public bool IsPublic { get; set; }

        [Required]
        [Display(Name = "Minimal Range")]
        public string MinRange { get; set; }

        [Required]
        [Display(Name = "Maximal Range")]
        public string MaxRange { get; set; }

        [Required]
        [Display(Name = "Last Update")]
        public DateTime LastUpdate { get; set; }

        [Required]
        [Display(Name = "Owner")]
        public User Owner { get; set; }

    }

}