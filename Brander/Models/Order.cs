using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Brander.Models
{
    public class Order
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Fecha-Orden")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "IGV")]
        public double IGV { get; set; }

        [Display(Name = "TotalOrdenOriginal")]
        public double OrderTotalOriginal { get; set; }

        [Display(Name = "TotalOrden")]
        public double OrderTotal { get; set; }

        [Display(Name = "EstadoPago")]
        public bool PaymentStatus { get; set; }




        [Display(Name = "Cupon")]
        public int CouponId { get; set; }

        [ForeignKey("CouponId")]
        public virtual Coupon Coupon { get; set; }


    }
}
