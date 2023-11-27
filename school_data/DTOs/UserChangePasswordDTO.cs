

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace School_Data.DTOs
{
    public class UserChangePasswordDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordNew { get; set; }

    }
}
