using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Data.Models
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "Nombre de Usuario es requerido")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Nombre Completo is requerido")]
        public string CompleteName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Correo es requerido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Contraseña es requerida")]
        public string Password { get; set; }
    }
}
