using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Brander.Models
{
    public class SubCategory
    {
        [Key]//indica que es la PK
        public int Id { get; set; }

        [Required]//indica que este dato no puede estar vacio
        [DisplayName("Nombre de Sub-Categoria")]//asigna el nombre que va a mostrar
        public string Name { get; set; }


        //es requerida una categoria a la cual pertenecer
        [Required]
        [Display(Name = "Categoria")]
        public int CategoryId { get; set; }

        //aca le decimos que es una foreign key
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }



    }
}
