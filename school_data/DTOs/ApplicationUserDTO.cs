using School_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace School_Data.DTOs
{
    public class ApplicationUserDTO : ApplicationUser
    {
        [JsonPropertyName("RolName")]
        public string RolName { get; set; }
    }
}
