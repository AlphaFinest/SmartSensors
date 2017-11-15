using SmartSensors.Data.Models;
using SmartSensors.Data.Models.Sensors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace SmartSensors.Service.ViewModels
{
    public class SensorViewModel
    {
        public int Id { get; set; }

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
        [Display(Name = "Public")]
        public bool IsPublic { get; set; }

        [Required]
        [Display(Name = "Minimal Range")]
        public int MinRange { get; set; }

        [Required]
        [Display(Name = "Maximal Range")]
        public int MaxRange { get; set; }
        
        public string Value { get; set; }

        public string ValueType { get; set; }

        [Display(Name = "Share With")]
        public string SharedWith { get; set; }

        public string Owner { get; set; }
        
        [Display(Name = "Urls")]
        public IEnumerable<SelectListItem> UrlCollection { get; set; }
        
        [Display(Name = "ValueType")]
        public IEnumerable<SelectListItem> ValueTypeCollection { get; set; }
        

    }

}