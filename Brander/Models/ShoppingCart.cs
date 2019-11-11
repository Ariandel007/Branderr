using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Brander.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            Count = 1;
        }


        public int Id { get; set; }

        public string ApplicationUserId { get; set; }

        [NotMapped]
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        public int GameId { get; set; }

        [NotMapped]
        [ForeignKey("MenuItemId")]
        public virtual Game Game { get; set; }



        [Range(1, int.MaxValue, ErrorMessage = "Por favor ingresar un valor que sea mayor o igual a {1}")]
        public int Count { get; set; }
    }
}
