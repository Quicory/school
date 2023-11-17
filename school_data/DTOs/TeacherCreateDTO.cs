using School_Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Data.DTOs
{
    public class TeacherCreateDTO
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(100, ErrorMessage = "El nombre debe tener al menos {0} de longitud.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "El apellido es requerido")]
        [StringLength(100, ErrorMessage = "El apellido debe tener al menos {0} de longitud.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "El correo es requerido")]
        [EmailAddress(ErrorMessage = "El correo debe ser válido.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "La profesión es requerida")]
        public string Profession { get; set; }
        //public List<SubjectsTeacherDTO> Subjects { get; set; } = new();
        public List<int> detail { get; set; }
    }
}
