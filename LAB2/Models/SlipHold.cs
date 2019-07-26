using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LAB2.Models
{
    public class SlipHold
    {
        [Key]
        [Display(Name ="SlipHoldID")]
        public int ID { get; set; }

        [Required]
        [Display(Name ="CustomerID")]
        public int CustomerID { get; set; }

        [Required]
        [Display(Name ="SlipID")]
        public int SlipID { get; set; }

        [Required]
        [Display(Name ="DockID")]
        public int DockID { get; set; }
    }
}