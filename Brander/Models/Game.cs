using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Brander.Models
{
    public class Game
    {
        [Key]//indica que es la PK
        public int Id { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        public string Name { get; set; }
        [Display(Name = "Descripcion")]
        public string Description { get; set; }
     
        //lo de las imagenes oueden ser tratadas de dos amneras, 1. subir las imagenes a la base de datos o 2. subirlas al servidor real
        //mas tarde lo haremos de la base de datos pero de momento lo haremos en el servidor, asi que alamcenaremos solo el camino.. de momento
        public string Image { get; set; }

        [Display(Name = "Categoria")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        [Display(Name = "Sub-Categoria")]
        public int SubCategoryId { get; set; }

        //indicamos la FK
        [ForeignKey("SubCategoryId")]
        public virtual SubCategory SubCategory { get; set; }


        [Required]
        [Display(Name = "Precio")]
        public double Price { get; set; }

    }
}
