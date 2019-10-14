using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Brander.Models
{
    public class Category
    {
        [Key]//indica que es la PK
        public int Id { get; set; }

        [Required]//indica que este dato no puede estar vacio
        [DisplayName("Nombre de Categoria")]//asigna el nombre que va a mostrar
        public string Name { get; set; }
    }
}
