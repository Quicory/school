using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Data.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Nombre de usuario es requerido")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Contraseña es requerida")]
        public string? Password { get; set; }
    }
}
