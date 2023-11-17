using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace School_Data.Models
{
    public class Teacher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
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
        public List<Subject> Subjects { get; set; } = new();
    }
}
