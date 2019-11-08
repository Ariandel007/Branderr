using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Brander.Models
{
    public class OrderDetails
    {

        [Key]
        public int Id { get; set; }



        [Display(Name = "Orden")]
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

        [Display(Name = "Key")]
        public int KeyId { get; set; }

        [ForeignKey("KeyId")]
        public virtual Key Key { get; set; }
    }
}
