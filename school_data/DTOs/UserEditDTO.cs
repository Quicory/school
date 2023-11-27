
using System.ComponentModel.DataAnnotations;

namespace School_Data.DTOs
{
    public class UserEditDTO
    {
        [Required(ErrorMessage = "ID es requerido")]
        public string Id { get; set; }
        [Required(ErrorMessage = "Nombre de Usuario es requerido")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Correo es requerido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Nombre Completo es requerido")]
        public string CompleteName { get; set; }

        [Required(ErrorMessage = "Rol es requerido")]
        public string Role { get; set; }
    }
}
