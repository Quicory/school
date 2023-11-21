
using System.ComponentModel.DataAnnotations;

namespace School_Data.DTOs
{
    public class ClassroomUpdateDTO
    {
        [Required(ErrorMessage = "El Nombre del Aula es requerida")]
        [StringLength(150, ErrorMessage = "El Nombre del Aula debe tener al menos {2} de longitud.", MinimumLength = 2)]
        public string Name { get; set; }
        [Required(ErrorMessage = "La Capacidad es requerida")]
        public int Capacity { get; set; }
        [Required(ErrorMessage = "El Lugar es requerido")]
        public string Location { get; set; }
    }
}
