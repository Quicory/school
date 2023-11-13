

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace School_Data.DTOs
{
    public class UserDTO
    {
        [JsonPropertyName("username")]
        public string? Username { get; set; }
       
        [JsonPropertyName("completename")]
        public string CompleteName { get; set; }
    }
}
