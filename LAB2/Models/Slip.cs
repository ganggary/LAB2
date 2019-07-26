using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LAB2.Models
{
    public class Slip
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
    }

    public class SlipAndLease
    {
        //[Key]
        //[Display(Name ="SlipAndLeaseID")]
        //public int SlipAndLeaseID { get; set; }

        [Key]
        [Required]
        [Display(Name = "SlipID")]
        public int SlipID { get; set; }

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

        [Required]
        //[ForeignKey("Dock")]
        [Display(Name = "DockName")]
        public string DockName { get; set; }

        [Display(Name = "WaterService")]
        public Boolean WaterService { get; set; }

        [Display(Name = "ElectricalService")]
        public Boolean ElectricalService { get; set; }

        [Display(Name = "Hold for lease")]
        public bool HoldLease { get; set; }

        
        [Required]
        //[ForeignKey("Lease")]
        [Display(Name = "LeaseID")]
        public int LeaseID { get; set; }
        

        [Required]
        //[ForeignKey("Customer")]
        [Display(Name = "CustomerID")]
        public int CustomerID { get; set; }

        [Required]
        [Display(Name = "FullName")]
        public string FullName { get; set; }
    }
}