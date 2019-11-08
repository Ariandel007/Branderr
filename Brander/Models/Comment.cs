using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Brander.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Disponible")]
        public bool Avaible { get; set; }

        [Required]
        [Display(Name = "Descripcion")]
        public string Description { get; set; }

        [Display(Name = "Estrellas")]
        public int Stars { get; set; }

        [Display(Name = "Juego")]
        public int GameId { get; set; }

        
        
        [ForeignKey("GameId")]
        public virtual Game Game { get; set; }

    }
}
