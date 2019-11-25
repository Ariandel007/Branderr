using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Brander.Models
{
    public class Key
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Precio")]
        public double Price { get; set; }



        [Display(Name = "Juego")]
        public int GameId { get; set; }

        [ForeignKey("GameId")]
        public virtual Game Game { get; set; }

        [Display(Name = "Stock")]
        public int StockId { get; set; }

        [ForeignKey("StockId")]
        public virtual Stock Stock { get; set; }
    }
}
