
using System.ComponentModel.DataAnnotations;

namespace School_Data.Models
{
    public class StudentCreateDTO
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(100, ErrorMessage = "El nombre debe tener al menos {0} de longitud.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "El apellido es requerido")]
        [StringLength(100, ErrorMessage = "El apellido debe tener al menos {0} de longitud.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "La Fecha de Nacimiento es requerida")]
        public DateOnly? Birthdate { get; set; }
        [Required(ErrorMessage = "La Matrícula es requerida")]
        [StringLength(12, ErrorMessage = "La matrícula debe tener al menos {0} de longitud.")]
        public string IDNumber { get; set; }
        [Required(ErrorMessage = "El Nombre del Padre es requerido")]
        public string FatherName { get; set; }
        [Required(ErrorMessage = "El Nombre de la Madre es requerida")]
        public string MotherName { get; set; }
        [Required(ErrorMessage = "La Dirección es requerida")]
        public string Address { get; set; }
    }
}
