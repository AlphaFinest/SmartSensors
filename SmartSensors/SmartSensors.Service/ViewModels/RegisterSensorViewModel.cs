using SmartSensors.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SmartSensors.Service.ViewModels
{
    public class RegisterSensorViewModel
    {
        //[Required]
        //[Display(Owner = "Owner")]
        //public string Owner { get; set; }

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
        [Range(1, 20, ErrorMessage = "SoMETHING")]
        public int PollingInterval { get; set; }

        [Required]
        [Display(Name = "Value Type")]
        public string ValueType { get; set; }

        [Required]
        [Display(Name = "Public")]
        public bool IsPublic { get; set; }

        [Required]
        [Display(Name = "Minimal Range")]
        public int MinRange { get; set; }

        [Required]
        [Display(Name = "Maximal Range")]
        public int MaxRange { get; set; }

        [Required]
        [Display(Name = "Last Update")]
        public DateTime LastUpdate { get; set; }

        [Required]
        [Display(Name = "Owner")]
        public string Owner { get; set; }

       
        [Display(Name = "Value")]
        public string Value { get; set; }

    }

}
