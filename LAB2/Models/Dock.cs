using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LAB2.Models
{
    public class Dock
    {
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "WaterService")]
        public Boolean WaterService { get; set; }

        [Display(Name = "ElectricalService")]
        public Boolean ElectricalService { get; set; }
    }
}