using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Brander.Models
{
    public class Coupon
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Codigo")]
        public string Code { get; set; }

        [Display(Name = "Descuento")]
        public double Disccount { get; set; }

        [Display(Name = "EstaActivo")]
        public bool IsActive { get; set; }

    }
}
