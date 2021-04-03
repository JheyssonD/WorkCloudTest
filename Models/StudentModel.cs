using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WorkCloudTest.Enums;

namespace WorkCloudTest.Models
{
    public class StudentModel
    {
        public Guid ID { get; set; }
        
        [Required]
        [StringLength(20, ErrorMessage = "Nombre debe tener máximo 20 caracteres.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Nombre solo debe contener caracteres alfabeticos")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Apellido debe tener máximo 20 caracteres.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Apellido solo debe contener caracteres alfabeticos")]
        public string Apellido { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Identificacion debe tener máximo 10 caracteres.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Identificacion solo debe contener números")]
        public string Identificacion { get; set; }

        [Required]
        public int Edad { get; set; }

        [Required]
        public string Casa { get; set; }
    }
}
