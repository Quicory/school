using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace School_Data.DTOs
{
    public class SubjectCreateDTO
    {
        [Required(ErrorMessage = "El nombre de la asignatura es requerido")]
        [StringLength(150, ErrorMessage = "El nombre debe tener al menos {2} de longitud.", MinimumLength = 2)]
        public string Name { get; set; }

        
    }
}
