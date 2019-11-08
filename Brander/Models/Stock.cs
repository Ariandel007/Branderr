using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Brander.Models
{
    public class Stock
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Fecha")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Precio")]
        public double Price { get; set; }

        [Required]
        [Display(Name = "Cantidad")]
        public int Quantity { get; set; }

        [Required]
        [Display(Name = "Region")]
        public string Region { get; set; }



        [Display(Name = "Proveedor")]
        public int SupplierId { get; set; }

        [ForeignKey("SupplierId")]
        public virtual Supplier Supplier { get; set; }

    }
}
