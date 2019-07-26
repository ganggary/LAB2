using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LAB2.Models
{
    public class SlipDock
    {
        [Key]
        [Required]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Width")]
        public int Width { get; set; }

        [Required]
        [Display(Name = "Length")]
        public int Length { get; set; }

        [Required]
        //[ForeignKey("Dock")]
        [Display(Name = "DockID")]
        public int DockID { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [Display(Name = "Dock Name")]
        public string DockName { get; set; }

        [Display(Name = "WaterService")]
        public Boolean WaterService { get; set; }

        [Display(Name = "ElectricalService")]
        public Boolean ElectricalService { get; set; }

        [Display(Name = "Hold for Lease")]
        public Boolean HoldForLease { get; set; }

        [Required]
        [Display(Name = "CustomerID")]
        public int CustomerID { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        


    }
}