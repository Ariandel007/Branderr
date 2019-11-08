using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Brander.Models
{
    public class Supplier
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nombre-Empresa")]
        public string CompanyName { get; set; }

        [Display(Name = "Correo")]
        public string Email { get; set; }

        [Display(Name = "Pais")]
        public string Country { get; set; }

    }
}
